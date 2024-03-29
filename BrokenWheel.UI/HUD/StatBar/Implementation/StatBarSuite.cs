﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using BrokenWheel.Core.Events;
using BrokenWheel.Core.Logging;
using BrokenWheel.Core.Settings;
using BrokenWheel.Core.Stats.Enum;
using BrokenWheel.Core.Stats.Events;
using BrokenWheel.Core.Stats.Info;
using BrokenWheel.UI.Display;
using BrokenWheel.UI.Settings.StatBar;

namespace BrokenWheel.UI.HUD.StatBar.Implementation
{
    public class StatBarSuite
        : IStatBarSuite
        , IEventHandler<SettingsUpdateEvent<StatBarSettings>>
    {
        private const string CATEGORY = "Display";

        private readonly ILogger _logger;
        private readonly StatBarSettings _statBarSettings;
        private readonly IDisplayTool _displayTool;
        private readonly IEventObservable<StatUpdatedEvent> @_simpleObservable;
        private readonly IEventObservable<ComplexStatUpdatedEvent> @_complexObservable;
        private readonly IStatBarSuiteDisplay _groupDisplay;
        private readonly IList<StatBar> _statBars = new List<StatBar>();

        private bool _isHiding;
        private double _highestPpp;

        /// <summary>
        /// Creates a group of stat bars, automatically populating the main vitals.
        /// </summary>
        /// <param name="simpleListener"> The event listener for simple stat update events. </param>
        /// <param name="complexListener"> The event listener for complex stat update events. </param>
        /// <param name="suiteDisplay"> The object in charge of creating and displaying the stat bars in GUI. </param>
        /// <exception cref="ArgumentNullException"> If any argument is null. </exception>
        public StatBarSuite(
            ILogger logger,
            StatBarSettings displaySettings,
            IDisplayTool displayTool,
            IEventObservable<StatUpdatedEvent> simpleListener,
            IEventObservable<ComplexStatUpdatedEvent> complexListener,
            IStatBarSuiteDisplay suiteDisplay)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _statBarSettings = displaySettings ?? throw new ArgumentNullException(nameof(displaySettings));
            _displayTool = displayTool ?? throw new ArgumentNullException(nameof(displayTool));
            _groupDisplay = suiteDisplay ?? throw new ArgumentNullException(nameof(suiteDisplay));
            @_simpleObservable = simpleListener ?? throw new ArgumentNullException(nameof(simpleListener));
            @_complexObservable = complexListener ?? throw new ArgumentNullException(nameof(complexListener));

            for (var i = 0; i < _statBarSettings.MainStatsInOrder.Count; i++)
                AddStatBar(StatInfoFactory.FromEnum(_statBarSettings.MainStatsInOrder[i].Stat), i);
            RepositionDisplays();
            _logger.LogCategory(CATEGORY, $"{nameof(StatBarSuite)} subscribed to {nameof(SettingsUpdateEvent<StatBarSettings>)}s");
        }

        private void ReportPpp(double ratio) => _highestPpp = System.Math.Max(_highestPpp, ratio);

        private double HighestPpp() => _highestPpp;

        public void Show()
        {
            if (!_isHiding)
                return;
            _logger.LogCategory(CATEGORY, $"{nameof(StatBarSuite)} showing...");

            RepositionDisplays();
            _groupDisplay.Show();
            _isHiding = false;
        }

        public void Hide()
        {
            if (_isHiding)
                return;
            _logger.LogCategory(CATEGORY, $"{nameof(StatBarSuite)} hiding...");

            _groupDisplay.Hide();
            _isHiding = true;
        }

        public void HandleEvent(SettingsUpdateEvent<StatBarSettings> gameEvent)
        {
            _logger.LogCategory(CATEGORY, $"{nameof(StatBarSettings)} updated - repositioning and recoloring...");
            RepositionDisplays();
            UpdateDisplayColorsFromSettings();
        }

        public void RepositionDisplays()
        {
            _logger.LogCategory(CATEGORY, $"{nameof(StatBarSuite)} repositioning...");
            StatBarPositioner
                .PositionBars(_statBars
                    .Where(_ => !_.Display.IsHidden)
                    .OrderBy(_ => _.Order)
                    .ToList());
        }

        public void UpdateDisplayColorsFromSettings()
        {
            foreach (var statBar in _statBars)
            {
                var color = ColorSettingsForStat(statBar.Info);
                statBar.Display.SetColorProfile(color);
            }
        }

        private StatBarColorSettings ColorSettingsForStat(StatInfo statInfo)
        {
            if (statInfo.Stat == Stat.Custom)
                return ColorSettingsForStat(statInfo.Code);
            return _statBarSettings?.MainStatsInOrder?
                .FirstOrDefault(_ => _.Stat == statInfo.Stat)?
                .ColorSettings
                ?? ColorSettingsForStat(statInfo.Code);
        }

        private StatBarColorSettings ColorSettingsForStat(string customStatCode)
        {
            return _statBarSettings.ColorsByCode.Any(kvp => kvp.Key == customStatCode)
                ? _statBarSettings.ColorsByCode.First(kvp => kvp.Key == customStatCode).Value
                : _statBarSettings.DefaultColors;
        }

        #region Remove

        public void RemoveStat(StatInfo statInfo)
        {
            if (statInfo == null)
                throw new ArgumentNullException(nameof(statInfo));

            if (!HasStat(statInfo.Code))
                return;

            var toRemove = _statBars.FirstOrDefault(_ => _.Info.Code == statInfo.Code);
            if (toRemove != null)
                RemoveStatBar(toRemove);
        }

        public void RemoveStat(Stat stat)
        {
            if (stat == Stat.Custom)
                throw new InvalidOperationException("Cannot remove custom stat bar without a code being given.");

            if (!HasStat(stat))
                return;

            var toRemove = _statBars.FirstOrDefault(_ => _.Info.Stat == stat);
            if (toRemove != null)
                RemoveStatBar(toRemove);
        }

        public void RemoveCustomStat(string customStatCode)
        {
            if (string.IsNullOrWhiteSpace(customStatCode))
                throw new ArgumentException($"{nameof(customStatCode)} cannot be null or whitespace.");

            if (!HasStat(customStatCode))
                return;

            var toRemove = _statBars.FirstOrDefault(_ => _.Info.Code == customStatCode);
            if (toRemove != null)
                RemoveStatBar(toRemove);
        }

        private void RemoveStatBar(StatBar statBar)
        {
            _groupDisplay.RemoveStatBarElement(statBar.Display);
            if (statBar.Info.IsComplex)
                @_complexObservable.UnsubscribeConditional(statBar.HandlerID);
            else
                @_simpleObservable.UnsubscribeConditional(statBar.HandlerID);
            _statBars.Remove(statBar);
            _logger.LogCategory(CATEGORY, $"{nameof(StatBarSuite)} removed {statBar.Info.Code}");
        }

        #endregion
        #region Add

        public void AddStat(StatInfo statInfo)
        {
            if (statInfo == null)
                throw new ArgumentNullException(nameof(statInfo));

            if (!HasStat(statInfo.Code))
                AddStatBar(statInfo);
        }

        public void AddStat(Stat stat)
        {
            if (stat == Stat.Custom)
                throw new InvalidOperationException("Cannot add custom stat bar without a code being given.");

            if (!HasStat(stat))
                AddStatBar(StatInfoFactory.FromEnum(stat));
        }

        public void AddCustomStat(string customStatCode)
        {
            if (string.IsNullOrWhiteSpace(customStatCode))
                throw new ArgumentException($"{nameof(customStatCode)} cannot be null or whitespace.");

            if (!HasStat(customStatCode))
                AddStatBar(StatInfoFactory.FromCode(customStatCode));
        }

        private void AddStatBar(StatInfo statInfo, int order = -1)
        {
            if (order < 0)
                order = _statBars.Count;
            var colors = ColorSettingsForStat(statInfo);
            if (statInfo.IsComplex)
                AddComplexStatBar(statInfo, colors, order);
            else
                AddSimpleStatBar(statInfo, colors, order);
            _logger.LogCategory(CATEGORY, $"{nameof(StatBarSuite)} added {statInfo.Code}");
        }

        private void AddComplexStatBar(StatInfo statInfo, StatBarColorSettings colors, int order)
        {
            var display = _groupDisplay.CreateStatBarElement<IComplexStatBarDisplay>(statInfo.Name, colors);
            var statBar = new ComplexStatBar(_statBarSettings, _displayTool, display, statInfo, ReportPpp, HighestPpp, order);
            var handler = @_complexObservable.SubscribeConditional(statBar.HandleEvent, _ => _.StatInfo.Id() == statInfo.Id());
            statBar.HandlerID = handler;
            _statBars.Add(statBar);
        }

        private void AddSimpleStatBar(StatInfo statInfo, StatBarColorSettings colors, int order)
        {
            var display = _groupDisplay.CreateStatBarElement<IStatBarDisplay>(statInfo.Name, colors);
            var statBar = new SimpleStatBar(_statBarSettings, _displayTool, display, statInfo, ReportPpp, HighestPpp, order);
            var handler = @_simpleObservable.SubscribeConditional(statBar.HandleEvent, _ => _.StatInfo.Id() == statInfo.Id());
            statBar.HandlerID = handler;
            _statBars.Add(statBar);
        }

        #endregion

        private bool HasStat(Stat stat) => _statBars.Any(_ => _.Info.Stat == stat);

        private bool HasStat(string code) => _statBars.Any(_ => _.Info.Code == code);
    }
}
