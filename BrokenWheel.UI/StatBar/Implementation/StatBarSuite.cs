using System;
using System.Collections.Generic;
using System.Linq;
using BrokenWheel.Core.Events.Listening;
using BrokenWheel.Core.Settings;
using BrokenWheel.Core.Settings.Registration;
using BrokenWheel.Core.Stats;
using BrokenWheel.Core.Stats.Enum;
using BrokenWheel.Core.Stats.Extensions;

namespace BrokenWheel.UI.StatBar.Implementation
{
    public class StatBarSuite : IStatBarSuite
    {
        private const string STAT_NOT_FOUND_FORMAT = 
            "Complex stat {0} does not exist - can only make stat bars for complex stats.";
        
        private readonly StatBarSettings _settings;
        private readonly IEntityEventNexus _eventNexus;
        private readonly IStatBarSuiteDisplay _groupDisplay;
        private readonly IList<StatBarRelationship> _statBars = new List<StatBarRelationship>();
        
        private bool _isHiding;
        private double _highestPpp;

        /// <summary>
        /// Creates a group of stat bars, automatically populating 
        /// </summary>
        /// <param name="eventNexus"> The event tracker for the entity the stat bars belong to. </param>
        /// <param name="suiteDisplay"> The object in charge of creating and displaying the stat bars in GUI. </param>
        /// <exception cref="ArgumentNullException"> If any argument is null. </exception>
        public StatBarSuite(IEntityEventNexus eventNexus, IStatBarSuiteDisplay suiteDisplay)
        {
            _settings = SettingsRegistry.GetSettings<StatBarSettings>();
            _eventNexus = eventNexus ?? throw new ArgumentNullException(nameof(eventNexus));
            _groupDisplay = suiteDisplay ?? throw new ArgumentNullException(nameof(suiteDisplay));

            for (var i = 0; i < _settings.MainStatOrder.Length; i++)
                _statBars.Add(NewStatBarRelationship(_settings.MainStatOrder[i], i));
            RepositionDisplays();
        }

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
            StatBarDisplayUpdater.PositionAndUpdateStatBars(_settings, _statBars
                .Where(_ => !_.StatBar.IsHidden)
                .OrderBy(_ => _.Order)
                .ToList());
        }

        public void AddStat(StatType type)
        {
            if (!HasStat(type))
                _statBars.Add(NewStatBarRelationship(type));
        }

        public void AddCustomStat(string code)
        {
            if (!HasStat(code))
                _statBars.Add(NewStatBarRelationship(code));
        }

        public void RemoveStat(StatType type)
        {
            if (!HasStat(type))
                return;
            
            var toRemove = _statBars.First(_ => _.StatInfo.Type != type);
            _groupDisplay.RemoveDisplay(toRemove.StatBar.Display);
            _statBars.Remove(toRemove);
        }

        public void RemoveCustomStat(string code)
        {
            if (!HasStat(code))
                return;
            
            var toRemove = _statBars.First(_ => _.StatBar.Info.Code == code);
            _groupDisplay.RemoveDisplay(toRemove.StatBar.Display);
            _statBars.Remove(toRemove);
        }

        private bool HasStat(StatType type) => _statBars.Any(_ => _.StatInfo.Type == type);

        private bool HasStat(string code) => _statBars.Any(_ => _.StatBar.Info.Code == code);

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

        private void ReportPpp(double ratio) => _highestPpp = System.Math.Max(_highestPpp, ratio);

        private double HighestPpp() => _highestPpp;

        private StatBarRelationship NewStatBarRelationship(IComplexStatistic statistic, int order = -1)
        {
            if (order < 0)
                order = _statBars.Count;
            var colors = ColorSettingsForStat(statistic.Info);
            var display = _groupDisplay.AddDisplay(statistic.Info.Name);
            display.SetColorProfile(colors);
            return new StatBarRelationship(_settings, statistic, display, ReportPpp, HighestPpp, order);
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
    }
}
