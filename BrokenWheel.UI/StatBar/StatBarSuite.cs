using System;
using System.Collections.Generic;
using System.Linq;
using BrokenWheel.Core.Events.Listening;
using BrokenWheel.Core.Settings;
using BrokenWheel.Core.Settings.Registration;
using BrokenWheel.Core.Stats;
using BrokenWheel.Core.Stats.Enum;
using BrokenWheel.Core.Stats.Events;
using BrokenWheel.UI.StatBar.Implementation;

namespace BrokenWheel.UI.StatBar
{
    public class StatBarSuite : IStatBarSuite
    {
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

            for (var i = 0; i < _settings.MainStatCodesInOrder.Length; i++)
                _statBars.Add(NewStatBarRelationship(new StatInfo(_settings.MainStatCodesInOrder[i]), i));
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
                .Where(_ => !_.StatBar.IsHidden)
                .OrderBy(_ => _.Order)
                .ToList());
        }

        public void AddStat(StatType type)
        {
            if (HasStat(type))
                return;

            var sbr = NewStatBarRelationship(new StatInfo(type));
            _eventNexus.SubscribeToEnumeratedEvent(sbr.StatInfo.Type, sbr.StatBar);
            _statBars.Add(sbr);
        }

        public void AddCustomStat(string code)
        {
            if (HasStat(code))
                return;

            var sbr = NewStatBarRelationship(new StatInfo(code));
            _eventNexus.SubscribeToEnumeratedEvent<StatType, ComplexStatUpdatedEvent>(sbr.StatInfo.Code, sbr.StatBar);
            _statBars.Add(sbr);
        }

        public void RemoveStat(StatType type)
        {
            if (!HasStat(type))
                return;
            
            var toRemove = _statBars.First(_ => _.StatInfo.Type != type);
            _eventNexus.UnsubscribeFromEnumeratedEvent(toRemove.StatInfo.Type, toRemove.StatBar);
            _groupDisplay.RemoveStatBarElement(toRemove.StatBar.UIElement);
            _statBars.Remove(toRemove);
        }

        public void RemoveCustomStat(string code)
        {
            if (!HasStat(code))
                return;
            
            var toRemove = _statBars.First(_ => _.StatBar.Info.Code == code);
            _eventNexus.UnsubscribeFromEnumeratedEvent<StatType, ComplexStatUpdatedEvent>(toRemove.StatInfo.Code, toRemove.StatBar);
            _groupDisplay.RemoveStatBarElement(toRemove.StatBar.UIElement);
            _statBars.Remove(toRemove);
        }

        private bool HasStat(StatType type) => _statBars.Any(_ => _.StatInfo.Type == type);

        private bool HasStat(string code) => _statBars.Any(_ => _.StatBar.Info.Code == code);

        private StatBarRelationship NewStatBarRelationship(StatInfo statInfo, int order = -1)
        {
            if (order < 0)
                order = _statBars.Count;
            var colors = ColorSettingsForStat(statInfo);
            var display = _groupDisplay.CreateStatBarElement<IStatBarUIElement>(statInfo.Name);
            display.SetColorProfile(colors);
            return new StatBarRelationship(_settings, statInfo, display, ReportPpp, HighestPpp, order);
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
