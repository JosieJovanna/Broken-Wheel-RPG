using System.Collections.Generic;
using BrokenWheel.Core.Events.Stats;
using BrokenWheel.Core.Stats.Extensions;

namespace BrokenWheel.Core.Stats.Processing
{
    public class StatBox : IStatBox
    {
        public string EntityGuid { get; }

        private event StatEventHandlers.StatUpdateEventHandler _handlersForAnyStat; // TODO: stat setting

        private readonly IDictionary<string, StatEventHandlers.StatUpdateEventHandler> _handlersByStatCode =
            new Dictionary<string, StatEventHandlers.StatUpdateEventHandler>();

        public IStatistic GetStat(StatType stat) => GetStat<IStatistic>(stat);
        public IStatistic GetStat(string stat) => GetStat<IStatistic>(stat);

        public IComplexStatistic GetComplexStatIfExists(StatType stat) => GetStat<IComplexStatistic>(stat);
        public IComplexStatistic GetComplexStatIfExists(string stat) => GetStat<IComplexStatistic>(stat);

        public T GetStat<T>(StatType stat) where T : IStatistic => GetStat<T>(stat.GetName());

        public T GetStat<T>(string stat) where T : IStatistic
        {
            throw new System.NotImplementedException();

            // get the save file for the given thing
            // get the stat by name
            // JSON is tempting but inefficient; I think I'll need a string followed by ints in a row
            // (and so I'll probably have to specify format by type)
        }

        public void SubscribeToStatUpdates(StatType statType, StatEventHandlers.StatUpdateEventHandler handler)
        {
            var statCode = statType.GetCode();
            SubscribeToCustomStatUpdates(statCode, handler);
        }

        public void SubscribeToCustomStatUpdates(string statCode, StatEventHandlers.StatUpdateEventHandler handler)
        {
            if (!_handlersByStatCode.ContainsKey(statCode))
                _handlersByStatCode.Add(statCode, handler);
            else
                _handlersByStatCode[statCode] += handler;
        }

        public void SubscribeToAllStatUpdates(StatEventHandlers.StatUpdateEventHandler handler)
        {
            _handlersForAnyStat += handler;
        }

        public void UnsubscribeFromStatUpdates(StatType statType, StatEventHandlers.StatUpdateEventHandler handler)
        {
            var statCode = statType.GetCode();
            UnsubscribeFromCustomStatUpdates(statCode, handler);
        }

        public void UnsubscribeFromCustomStatUpdates(string statCode, StatEventHandlers.StatUpdateEventHandler handler)
        {
            if (_handlersByStatCode.ContainsKey(statCode))
                _handlersByStatCode[statCode] -= handler;
        }

        public void UnsubscribeFromAllStatUpdates(StatEventHandlers.StatUpdateEventHandler handler)
        {
            _handlersForAnyStat -= handler;
        }
    }
}
