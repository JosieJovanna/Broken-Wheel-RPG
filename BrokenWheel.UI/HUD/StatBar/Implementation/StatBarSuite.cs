using System;
using System.Collections.Generic;
using System.Linq;
using BrokenWheel.Core.DependencyInjection;
using BrokenWheel.Core.Events.Listening;
using BrokenWheel.Core.Logging;
using BrokenWheel.Core.Settings;
using BrokenWheel.Core.Stats.Enum;
using BrokenWheel.Core.Stats.Events;
using BrokenWheel.Core.Stats.Info;

namespace BrokenWheel.UI.HUD.StatBar.Implementation
{
    public class StatBarSuite : IStatBarSuite
    {
        private readonly IModule _module;
        private readonly ILogger _logger;
        private readonly StatBarSettings _settings;
        private readonly IStatBarSuiteDisplay _groupDisplay;
        private readonly ICustomCategorizedEventListener<StatUpdatedEvent, Stat> _simpleListener;
        private readonly ICustomCategorizedEventListener<ComplexStatUpdatedEvent, Stat> _complexListener;
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
            ICustomCategorizedEventListener<StatUpdatedEvent, Stat> simpleListener,
            ICustomCategorizedEventListener<ComplexStatUpdatedEvent, Stat> complexListener,
            IStatBarSuiteDisplay suiteDisplay)
        {
            _module = Injection.GetModule();
            _logger = _module.GetLogger();
            _settings = _module.GetSettings<StatBarSettings>();
            _groupDisplay = suiteDisplay ?? throw new ArgumentNullException(nameof(suiteDisplay));
            _simpleListener = simpleListener ?? throw new ArgumentNullException(nameof(simpleListener));
            _complexListener = complexListener ?? throw new ArgumentNullException(nameof(complexListener));

            for (var i = 0; i < _settings.MainStatCodesInOrder.Count; i++)
                AddStatBar(StatInfoFactory.FromEnum(_settings.MainStatCodesInOrder[i]), i);
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
            _logger.LogCategory("Display", $"{nameof(StatBarSuite)} shows");
        }

        public void Hide()
        {
            if (_isHiding)
                return;

            _groupDisplay.Hide();
            _isHiding = true;
            _logger.LogCategory("Display", $"{nameof(StatBarSuite)} hides");
        }

        public void RepositionDisplays()
        {
            StatBarPositioner
                .PositionBars(_settings, _statBars
                    .Where(_ => !_.Display.IsHidden)
                    .OrderBy(_ => _.Order)
                    .ToList());
            _logger.LogCategory("Display", $"{nameof(StatBarSuite)} repositioned");
        }

        public void RemoveStat(StatInfo statInfo)
        {
            if (statInfo == null)
                throw new ArgumentNullException(nameof(statInfo));

            if (!HasStat(statInfo.Code))
                return;

            var toRemove = _statBars.First(_ => _.Info.Code == statInfo.Code);
            RemoveStatBar(toRemove);
        }

        public void RemoveStat(Stat stat)
        {
            if (stat == Stat.Custom)
                throw new InvalidOperationException("Cannot remove custom stat bar without a code being given.");

            if (!HasStat(stat))
                return;

            var toRemove = _statBars.First(_ => _.Info.Stat == stat);
            RemoveStatBar(toRemove);
        }

        public void RemoveCustomStat(string customStatCode)
        {
            if (string.IsNullOrWhiteSpace(customStatCode))
                throw new ArgumentException($"{nameof(customStatCode)} cannot be null or whitespace.");

            if (!HasStat(customStatCode))
                return;

            var toRemove = _statBars.First(_ => _.Info.Code == customStatCode);
            RemoveStatBar(toRemove);
        }

        private void RemoveStatBar(StatBar statBar)
        {
            _groupDisplay.RemoveStatBarElement(statBar.Display);
            if (statBar.Info.IsComplex)
                RemoveComplexStatBar((ComplexStatBar)statBar);
            else
                RemoveSimpleStatBar((SimpleStatBar)statBar);
            _statBars.Remove(statBar);
            _logger.LogCategory("Display", $"{nameof(StatBarSuite)} removed {statBar.Info.Code}");
        }

        private void RemoveComplexStatBar(ComplexStatBar statBar)
        {
            if (statBar.Info.IsCustom)
                _complexListener.UnsubscribeFromCategory(statBar.Info.Code, statBar);
            else
                _complexListener.UnsubscribeFromCategory(statBar.Info.Stat, statBar);
        }

        private void RemoveSimpleStatBar(SimpleStatBar statBar)
        {
            if (statBar.Info.IsCustom)
                _simpleListener.UnsubscribeFromCategory(statBar.Info.Code, statBar);
            else
                _simpleListener.UnsubscribeFromCategory(statBar.Info.Stat, statBar);
        }

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
            _logger.LogCategory("Display", $"{nameof(StatBarSuite)} added {statInfo.Code}");
        }

        private StatBarColorSettings ColorSettingsForStat(StatInfo statInfo)
        {
            switch (statInfo.Stat)
            {
                case Stat.HP: // TODO: change settings to include enum and colors in same object, then refactor.
                    return _settings.HpColors;
                case Stat.SP:
                    return _settings.SpColors;
                case Stat.WP:
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
                _complexListener.SubscribeToCategory(statInfo.Stat, statBar);
            _statBars.Add(statBar);
        }

        private void AddSimpleStatBar(StatInfo statInfo, StatBarColorSettings colors, int order)
        {
            var display = _groupDisplay.CreateStatBarElement<IStatBarDisplay>(statInfo.Name, colors);
            var statBar = new SimpleStatBar(_settings, display, statInfo, ReportPpp, HighestPpp, order);
            if (statInfo.IsCustom)
                _simpleListener.SubscribeToCategory(statInfo.Code, statBar);
            else
                _simpleListener.SubscribeToCategory(statInfo.Stat, statBar);
            _statBars.Add(statBar);
        }

        private bool HasStat(Stat stat) => _statBars.Any(_ => _.Info.Stat == stat);

        private bool HasStat(string code) => _statBars.Any(_ => _.Info.Code == code);
    }
}
