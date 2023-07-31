using System;
using System.Collections.Generic;
using System.Linq;
using BrokenWheel.Core.Event.Listening;
using BrokenWheel.Core.Settings;
using BrokenWheel.Core.Settings.Registration;
using BrokenWheel.Core.Stats;
using BrokenWheel.Core.Stats.Events;

namespace BrokenWheel.UI.HUD.StatBar.Implementation
{
    public class StatBarSuite : IStatBarSuite
    {
        private readonly StatBarSettings _settings;
        private readonly IStatBarSuiteDisplay _groupDisplay;
        private readonly ICustomCategorizedEventListener<StatUpdatedEvent, StatType> _simpleListener;
        private readonly ICustomCategorizedEventListener<ComplexStatUpdatedEvent, StatType> _complexListener;
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
            ICustomCategorizedEventListener<StatUpdatedEvent, StatType> simpleListener, 
            ICustomCategorizedEventListener<ComplexStatUpdatedEvent, StatType> complexListener, 
            IStatBarSuiteDisplay suiteDisplay)
        {
            _settings = SettingsRegistry.GetSettings<StatBarSettings>();
            _groupDisplay = suiteDisplay ?? throw new ArgumentNullException(nameof(suiteDisplay));
            _simpleListener = simpleListener ?? throw new ArgumentNullException(nameof(simpleListener));
            _complexListener = complexListener ?? throw new ArgumentNullException(nameof(complexListener));

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

        public void AddStat(StatInfo info)
        {
            if (info == null)
                throw new ArgumentNullException(nameof(info));
            
            if (!HasStat(info.Code)) 
                AddStatBar(info);
        }

        public void AddStat(StatType type)
        {
            if (type == StatType.Custom)
                throw new InvalidOperationException("Cannot add custom stat bar without a code being given.");
            
            if (!HasStat(type)) 
                AddStatBar(new StatInfo(type));
        }

        public void AddCustomStat(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
                throw new ArgumentException($"{nameof(code)} cannot be null or whitespace.");
            
            if (!HasStat(code)) 
                AddStatBar(StatInfo.FromCode(code));
        }
        
        public void RemoveStat(StatInfo info)
        {
            if (info == null)
                throw new ArgumentNullException(nameof(info));
            
            if (!HasStat(info.Code))
                return;
            
            var toRemove = _statBars.First(_ => _.Info.Code == info.Code);
            RemoveStatBar(toRemove);
        }
        
        public void RemoveStat(StatType type)
        {
            if (type == StatType.Custom)
                throw new InvalidOperationException("Cannot remove custom stat bar without a code being given.");
            
            if (!HasStat(type))
                return;
            
            var toRemove = _statBars.First(_ => _.Info.Type == type);
            RemoveStatBar(toRemove);
        }

        public void RemoveCustomStat(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
                throw new ArgumentException($"{nameof(code)} cannot be null or whitespace.");
            
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
                _complexListener.UnsubscribeFromCategory(statBar.Info.Code, statBar);
            else
                _complexListener.UnsubscribeFromCategory(statBar.Info.Type, statBar);
        }

        private void RemoveSimpleStatBar(SimpleStatBar statBar)
        {
            _groupDisplay.RemoveStatBarElement(statBar.Display);
            if (statBar.Info.IsCustom)
                _simpleListener.UnsubscribeFromCategory(statBar.Info.Code, statBar);
            else
                _simpleListener.UnsubscribeFromCategory(statBar.Info.Type, statBar);
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
                _complexListener.SubscribeToCategory(statInfo.Code, statBar);
            else
                _complexListener.SubscribeToCategory(statInfo.Type, statBar);
            _statBars.Add(statBar);
        }

        private void AddSimpleStatBar(StatInfo statInfo, StatBarColorSettings colors, int order)
        {
            var display = _groupDisplay.CreateStatBarElement<IStatBarDisplay>(statInfo.Name, colors);
            var statBar = new SimpleStatBar(_settings, display, statInfo, ReportPpp, HighestPpp, order);
            if (statInfo.IsCustom)
                _simpleListener.SubscribeToCategory(statInfo.Code, statBar);
            else
                _simpleListener.SubscribeToCategory(statInfo.Type, statBar);
            _statBars.Add(statBar);
        }
    }
}
