using System;
using System.Collections.Generic;
using BrokenWheel.Core.Stats;
using BrokenWheel.Core.Stats.Processing;

namespace BrokenWheel.UI.StatBar
{
    public class StatBarSuite : IStatBarSuite
    {
        private readonly IStatBox _statBox;
        private readonly IList<StatBarRelationship> _statBars;
        private readonly IStatBarSuiteDisplay _groupSuiteDisplay;

        private bool _isHiding;

        public StatBarSuite(IStatBox statBox)
        {
            _statBox = statBox ?? throw new ArgumentNullException(nameof(statBox));
        }

        public void Show()
        {
            if (!_isHiding)
                return;
            UpdateDisplays();
            _groupSuiteDisplay.Show();
            _isHiding = false;
        }

        public void Hide()
        {
            if (_isHiding)
                return;
            _groupSuiteDisplay.Hide();
            _isHiding = true;
        }

        public void UpdateDisplays()
        {
            throw new NotImplementedException();
        }

        public void AddStat(StatType statType, IStatBarDisplay display)
        {
            throw new NotImplementedException();
        }

        public void AddCustomStat(string customStatName, IStatBarDisplay display)
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
