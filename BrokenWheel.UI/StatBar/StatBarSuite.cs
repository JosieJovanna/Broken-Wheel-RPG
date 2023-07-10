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
        private readonly StatBarSettings _settings;
        private readonly IStatBox _statBox;
        private readonly IStatBarSuiteDisplay _groupDisplay;
        
        private readonly IList<StatBarRelationship> _statBars = new List<StatBarRelationship>();

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
            InstantiateMainStatBars();
        }

        private void InstantiateMainStatBars()
        {
            var hp = InstantiateStatBar(StatType.HP, 0); // TODO: order. array index as order, string as code.
            var sp = InstantiateStatBar(StatType.SP, 1);
            var wp = InstantiateStatBar(StatType.WP, 2);
        }

        private StatBarRelationship InstantiateStatBar(StatType type, int order)
        {
            var stat = _statBox.GetComplexStatIfExists(type);
            if (stat == null)
                throw new InvalidOperationException(
                    $"Complex stat {type.GetCode()} does not exist - can only make stat bars for complex stats.");
            return InstantiateStatBar(stat, order);

        }

        private StatBarRelationship InstantiateStatBar(IComplexStatistic statistic, int order)
        {
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
                statBarRelationship.Bar.UpdateDisplay();
        }

        public void AddStat(StatType statType)
        {
            throw new NotImplementedException();
        }

        public void AddCustomStat(string customStatName)
        {
            throw new NotImplementedException();
        }

        public void RemoveStat(StatType statType)
        {
            throw new NotImplementedException();
        }

        public void RemoveCustomStat(StatType customStatName)
        {
            throw new NotImplementedException();
        }
    }
}
