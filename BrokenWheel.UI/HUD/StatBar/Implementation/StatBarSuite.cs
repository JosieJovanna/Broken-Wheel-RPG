﻿using System;
using System.Collections.Generic;
using System.Linq;
using BrokenWheel.Core.Event.Listening;
using BrokenWheel.Core.Settings;
using BrokenWheel.Core.Settings.Registration;
using BrokenWheel.Core.Stats;
using BrokenWheel.Core.Stats.Enum;
using BrokenWheel.Core.Stats.Events;

namespace BrokenWheel.UI.HUD.StatBar.Implementation
{
    public class StatBarSuite : IStatBarSuite
    {
        private readonly StatBarSettings _settings;
        private readonly IEntityEventListener _eventListener;
        private readonly IStatBarSuiteDisplay _groupDisplay;
        private readonly IList<StatBar> _statBars = new List<StatBar>();
        
        private bool _isHiding;
        private double _highestPpp;

        /// <summary>
        /// Creates a group of stat bars, automatically populating 
        /// </summary>
        /// <param name="eventListener"> The event tracker for the entity the stat bars belong to. </param>
        /// <param name="suiteDisplay"> The object in charge of creating and displaying the stat bars in GUI. </param>
        /// <exception cref="ArgumentNullException"> If any argument is null. </exception>
        public StatBarSuite(IEntityEventListener eventListener, IStatBarSuiteDisplay suiteDisplay)
        {
            _settings = SettingsRegistry.GetSettings<StatBarSettings>();
            _eventListener = eventListener ?? throw new ArgumentNullException(nameof(eventListener));
            _groupDisplay = suiteDisplay ?? throw new ArgumentNullException(nameof(suiteDisplay));

            for (var i = 0; i < _settings.MainStatCodesInOrder.Length; i++)
                AddStatBar(new StatInfo(_settings.MainStatCodesInOrder[i]), i);
            RepositionDisplays();
        }

        private void ReportPpp(double ratio) => _highestPpp = System.Math.Max(_highestPpp, ratio);

        private double HighestPpp() => _highestPpp;

        public void Show()
        {
            if (!_isHiding)
                return;
            
            RepositionDisplays();
            _groupDisplay.Show();
            _isHiding = false;
        }

        public void Hide()
        {
            if (_isHiding)
                return;
            
            _groupDisplay.Hide();
            _isHiding = true;
        }

        public void RepositionDisplays()
        {
            StatBarPositioner.PositionBars(_settings, _statBars
                .Where(_ => !_.Display.IsHidden)
                .OrderBy(_ => _.Order)
                .ToList());
        }

        public void AddStat(StatType type)
        {
            if (!HasStat(type)) AddStatBar(new StatInfo(type));
        }

        public void AddCustomStat(string code)
        {
            if (!HasStat(code)) AddStatBar(new StatInfo(code));
        }
        
        public void RemoveStat(StatType type)
        {
            if (!HasStat(type))
                return;
            
            var toRemove = _statBars.First(_ => _.Info.Type != type);
            RemoveStatBar(toRemove);
        }

        public void RemoveCustomStat(string code)
        {
            if (!HasStat(code))
                return;
            
            var toRemove = _statBars.First(_ => _.Info.Code == code);
            RemoveStatBar(toRemove);
        }

        private bool HasStat(StatType type) => _statBars.Any(_ => _.Info.Type == type);

        private bool HasStat(string code) => _statBars.Any(_ => _.Info.Code == code);

        private void RemoveStatBar(StatBar statBar)
        {
            if (statBar.Info.IsComplex)
                RemoveComplexStatBar((ComplexStatBar)statBar);
            else
                RemoveSimpleStatBar((SimpleStatBar)statBar);
            _statBars.Remove(statBar);
        }

        private void RemoveComplexStatBar(ComplexStatBar statBar)
        {
            _groupDisplay.RemoveStatBarElement(statBar.Display);
            if (statBar.Info.IsCustom)
                _eventListener.UnsubscribeFromEnumeratedEvent<StatType, ComplexStatUpdatedEvent>(statBar.Info.Code, statBar);
            else
                _eventListener.UnsubscribeFromEnumeratedEvent(statBar.Info.Type, statBar);
        }

        private void RemoveSimpleStatBar(SimpleStatBar statBar)
        {
            _groupDisplay.RemoveStatBarElement(statBar.Display);
            if (statBar.Info.IsCustom)
                _eventListener.UnsubscribeFromEnumeratedEvent<StatType, StatUpdatedEvent>(statBar.Info.Code, statBar);
            else
                _eventListener.UnsubscribeFromEnumeratedEvent(statBar.Info.Type, statBar);
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
        }

        private StatBarColorSettings ColorSettingsForStat(StatInfo statInfo)
        {
            switch (statInfo.Type)
            {
                case StatType.HP:
                    return _settings.HpColors;
                case StatType.SP:
                    return _settings.SpColors;
                case StatType.WP:
                    return _settings.WpColors;
                default:
                    return ColorSettingForStat(statInfo.Code);
            }
        }

        private StatBarColorSettings ColorSettingForStat(string customStatCode)
        {
            return _settings.ColorsByCode.Any(kvp => kvp.Key == customStatCode)
                ? _settings.ColorsByCode.First(kvp => kvp.Key == customStatCode).Value 
                : _settings.DefaultColors;
        }

        private void AddComplexStatBar(StatInfo statInfo, StatBarColorSettings colors, int order)
        {
            var display = _groupDisplay.CreateStatBarElement<IComplexStatBarDisplay>(statInfo.Name, colors);
            var statBar = new ComplexStatBar(_settings, display, statInfo, ReportPpp, HighestPpp, order);
            if (statInfo.IsCustom)
                _eventListener.SubscribeToEnumeratedEvent<StatType, ComplexStatUpdatedEvent>(statInfo.Code, statBar);
            else
                _eventListener.SubscribeToEnumeratedEvent(statInfo.Type, statBar);
            _statBars.Add(statBar);
        }

        private void AddSimpleStatBar(StatInfo statInfo, StatBarColorSettings colors, int order)
        {
            var display = _groupDisplay.CreateStatBarElement<IStatBarDisplay>(statInfo.Name, colors);
            var statBar = new SimpleStatBar(_settings, display, statInfo, ReportPpp, HighestPpp, order);
            if (statInfo.IsCustom)
                _eventListener.SubscribeToEnumeratedEvent<StatType, StatUpdatedEvent>(statInfo.Code, statBar);
            else
                _eventListener.SubscribeToEnumeratedEvent(statInfo.Type, statBar);
            _statBars.Add(statBar);
        }
    }
}
