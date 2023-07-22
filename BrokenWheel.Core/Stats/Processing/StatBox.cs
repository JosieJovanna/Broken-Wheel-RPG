/*
using System.Collections.Generic;
using BrokenWheel.Core.Event;
using BrokenWheel.Core.Stats.Extensions;

namespace BrokenWheel.Core.Stats.Processing
{
    public class StatBox : IStatBox
    {
        public string EntityGuid { get; }

        private event Handlers.StatUpdateEventHandler _handlersForAnyStat; // TODO: stat setting

        private readonly IDictionary<string, Handlers.StatUpdateEventHandler> _handlersByStatCode =
            new Dictionary<string, Handlers.StatUpdateEventHandler>();

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

        public void SubscribeToStatUpdates(StatType statType, Handlers.StatUpdateEventHandler handler)
        {
            var statCode = statType.GetCode();
            SubscribeToCustomStatUpdates(statCode, handler);
        }

        public void SubscribeToCustomStatUpdates(string statCode, Handlers.StatUpdateEventHandler handler)
        {
            if (!_handlersByStatCode.ContainsKey(statCode))
                _handlersByStatCode.Add(statCode, handler);
            else
                _handlersByStatCode[statCode] += handler;
        }

        public void SubscribeToAllStatUpdates(Handlers.StatUpdateEventHandler handler)
        {
            _handlersForAnyStat += handler;
        }

        public void UnsubscribeFromStatUpdates(StatType statType, Handlers.StatUpdateEventHandler handler)
        {
            var statCode = statType.GetCode();
            UnsubscribeFromCustomStatUpdates(statCode, handler);
        }

        public void UnsubscribeFromCustomStatUpdates(string statCode, Handlers.StatUpdateEventHandler handler)
        {
            if (_handlersByStatCode.ContainsKey(statCode))
                _handlersByStatCode[statCode] -= handler;
        }

        public void UnsubscribeFromAllStatUpdates(Handlers.StatUpdateEventHandler handler)
        {
            _handlersForAnyStat -= handler;
        }
    }
}
*/
