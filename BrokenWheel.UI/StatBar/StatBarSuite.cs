using System;
using System.Collections.Generic;
using System.Linq;
using BrokenWheel.Core.Settings;
using BrokenWheel.Core.Settings.Registration;
using BrokenWheel.Core.Stats;
using BrokenWheel.Core.Stats.Enum;
using BrokenWheel.Core.Stats.Extensions;
using BrokenWheel.Core.Stats.Processing;

namespace BrokenWheel.UI.StatBar
{
    public class StatBarSuite : IStatBarSuite
    {
        private const string STAT_NOT_FOUND_FORMAT = 
            "Complex stat {0} does not exist - can only make stat bars for complex stats.";
        
        private readonly StatBarSettings _settings;
        private readonly IStatBox _statBox;
        private readonly IStatBarSuiteDisplay _groupDisplay;
        
        private IList<StatBarRelationship> _statBars = new List<StatBarRelationship>();

        private bool _isHiding;

        /// <summary>
        /// Creates a group of stat bars, automatically populating 
        /// </summary>
        /// <param name="statBox"></param>
        /// <param name="suiteDisplay"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public StatBarSuite(IStatBox statBox, IStatBarSuiteDisplay suiteDisplay)
        {
            _settings = SettingsRegistry.GetSettings<StatBarSettings>();
            _statBox = statBox ?? throw new ArgumentNullException(nameof(statBox));
            _groupDisplay = suiteDisplay ?? throw new ArgumentNullException(nameof(suiteDisplay));
            // main stat displays
            for (var i = 0; i < _settings.MainStatOrder.Length; i++)
                _statBars.Add(NewStatBarRelationship(_settings.MainStatOrder[i], i));
        }

        public void Show()
        {
            if (!_isHiding)
                return;
            UpdateDisplays();
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

        public void UpdateDisplays()
        {
            foreach (var statBarRelationship in _statBars)
                statBarRelationship.StatBar.UpdateDisplay();
        }

        public void AddStat(StatType type)
        {
            if (AnyMatchingBarsForStat(type))
                return;
            _statBars.Add(NewStatBarRelationship(type));
        }

        public void AddCustomStat(string code)
        {
            if (AnyMatchingBarsForStat(code))
                return;
            _statBars.Add(NewStatBarRelationship(code));
        }

        public void RemoveStat(StatType type)
        {
            if (AnyMatchingBarsForStat(type))
                return;
            _statBars = _statBars
                .Where(_ => _.Type != type)
                .ToList();
        }

        public void RemoveCustomStat(string code)
        {
            if (AnyMatchingBarsForStat(code))
                return;
            _statBars = _statBars
                .Where(_ => _.StatBar.Info.Code != code)
                .ToList();
        }

        private bool AnyMatchingBarsForStat(StatType type) => _statBars.Any(_ => _.Type == type);

        private bool AnyMatchingBarsForStat(string code) => _statBars.Any(_ => _.StatBar.Info.Code == code);

        private StatBarRelationship NewStatBarRelationship(StatType statType, int order = -1)
        {
            var stat = _statBox.GetComplexStatIfExists(statType);
            if (stat == null)
                throw new InvalidOperationException(string.Format(STAT_NOT_FOUND_FORMAT, statType.GetCode()));
            return NewStatBarRelationship(stat, order);
        }

        private StatBarRelationship NewStatBarRelationship(string statCode, int order = -1)
        {
            var stat = _statBox.GetComplexStatIfExists(statCode);
            if (stat == null)
                throw new InvalidOperationException(string.Format(STAT_NOT_FOUND_FORMAT, statCode));
            return NewStatBarRelationship(stat, order);
        }

        private StatBarRelationship NewStatBarRelationship(IComplexStatistic statistic, int order = -1)
        {
            if (order < 0)
                order = _statBars.Count;
            var colors = ColorSettingsForStat(statistic.Info);
            var display = _groupDisplay.CreateStatBarDisplay(colors);
            return new StatBarRelationship(statistic, display, order, _settings);
        }

        private StatBarColorSettings ColorSettingsForStat(StatTypeInfo statInfo)
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
    }
}
