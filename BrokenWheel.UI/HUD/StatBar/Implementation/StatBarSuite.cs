using System;
using System.Collections.Generic;
using System.Linq;
using BrokenWheel.Core.DependencyInjection;
using BrokenWheel.Core.Events.Observables;
using BrokenWheel.Core.Logging;
using BrokenWheel.Core.Settings.Events;
using BrokenWheel.Core.Stats.Enum;
using BrokenWheel.Core.Stats.Events;
using BrokenWheel.Core.Stats.Info;
using BrokenWheel.UI.Settings.StatBar;

namespace BrokenWheel.UI.HUD.StatBar.Implementation
{
    public class StatBarSuite : IStatBarSuite
    {
        private const string CATEGORY = "Display";

        private readonly IModule _module;
        private readonly ILogger _logger;
        private readonly StatBarSettings _settings;
        private readonly IEventObservable<SettingsUpdateEvent<StatBarSettings>> _settingsUpdates;
        private readonly IEventObservable<StatUpdatedEvent> _simpleListener;
        private readonly IEventObservable<ComplexStatUpdatedEvent> _complexListener;
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
            IModule module,
            IEventObservable<StatUpdatedEvent> simpleListener,
            IEventObservable<ComplexStatUpdatedEvent> complexListener,
            IStatBarSuiteDisplay suiteDisplay)
        {
            _module = module;
            _logger = _module.GetLogger();
            _settings = _module.GetSettings<StatBarSettings>();
            _groupDisplay = suiteDisplay ?? throw new ArgumentNullException(nameof(suiteDisplay));
            _simpleListener = simpleListener ?? throw new ArgumentNullException(nameof(simpleListener));
            _complexListener = complexListener ?? throw new ArgumentNullException(nameof(complexListener));

            for (var i = 0; i < _settings.MainStatsInOrder.Count; i++)
                AddStatBar(StatInfoFactory.FromEnum(_settings.MainStatsInOrder[i].Stat), i);
            RepositionDisplays();
            _settingsUpdates.Subscribe((settingUpdate) => UpdateFromSettings(settingUpdate));
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

        public void UpdateFromSettings(SettingsUpdateEvent<StatBarSettings> gameEvent)
        {
            _logger.LogCategory(CATEGORY, $"{nameof(StatBarSettings)} updated - repositioning and recoloring...");
            RepositionDisplays();
            UpdateDisplayColorsFromSettings();
        }

        public void RepositionDisplays()
        {
            _logger.LogCategory(CATEGORY, $"{nameof(StatBarSuite)} repositioning...");
            StatBarPositioner
                .PositionBars(_settings, _statBars
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
            return _settings?.MainStatsInOrder?
                .FirstOrDefault(_ => _.Stat == statInfo.Stat)?
                .ColorSettings
                ?? ColorSettingsForStat(statInfo.Code);
        }

        private StatBarColorSettings ColorSettingsForStat(string customStatCode)
        {
            return _settings.ColorsByCode.Any(kvp => kvp.Key == customStatCode)
                ? _settings.ColorsByCode.First(kvp => kvp.Key == customStatCode).Value
                : _settings.DefaultColors;
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
                _complexListener.UnsubscribeFromCategory(statBar.Info.Id(), (ComplexStatBar)statBar);
            else
                _simpleListener.UnsubscribeFromCategory(statBar.Info.Id(), (SimpleStatBar)statBar);
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
            var statBar = new ComplexStatBar(_settings, display, statInfo, ReportPpp, HighestPpp, order);
            _complexListener.SubscribeToCategory(statInfo.Id(), statBar);
            _statBars.Add(statBar);
        }

        private void AddSimpleStatBar(StatInfo statInfo, StatBarColorSettings colors, int order)
        {
            var display = _groupDisplay.CreateStatBarElement<IStatBarDisplay>(statInfo.Name, colors);
            var statBar = new SimpleStatBar(_settings, display, statInfo, ReportPpp, HighestPpp, order);
            _simpleListener.SubscribeToCategory(statInfo.Id(), statBar);
            _statBars.Add(statBar);
        }

        #endregion

        private bool HasStat(Stat stat) => _statBars.Any(_ => _.Info.Stat == stat);

        private bool HasStat(string code) => _statBars.Any(_ => _.Info.Code == code);
    }
}
