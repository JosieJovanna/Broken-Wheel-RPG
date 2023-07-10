using BrokenWheel.Core.Events.Stats;

namespace BrokenWheel.Core.Stats.Processing
{
    public interface IStatBox
    {
        string EntityGuid { get; }

        T GetStat<T>(StatType stat) where T : IStatistic;
        T GetStat<T>(string stat) where T : IStatistic;

        IStatistic GetStat(StatType stat);
        IStatistic GetStat(string stat);

        IComplexStatistic GetComplexStat(StatType stat);

        /// <summary>
        /// Gets a 
        /// </summary>
        IComplexStatistic GetComplexStat(string stat);

        /// <summary>
        /// Subscribes to receive <see cref="StatUpdateEventArgs"/> of the specified type whenever any of the stat values change.
        /// </summary>
        void SubscribeToStatUpdates(StatType statType, StatEventHandlers.StatUpdateEventHandler handler);

        /// <summary>
        /// Subscribes to receive <see cref="StatUpdateEventArgs"/> of the specified type whenever any of the stat values change.
        /// </summary>
        void SubscribeToCustomStatUpdates(string statCode, StatEventHandlers.StatUpdateEventHandler handler);

        /// <summary>
        /// Subscribes to receive <see cref="StatUpdateEventArgs"/> whenever any stat values change.
        /// </summary>
        void SubscribeToAllStatUpdates(StatEventHandlers.StatUpdateEventHandler handler);

        /// <summary>
        /// Unsubscribes from receiving an event when the specified type of stat changes.
        /// </summary>
        void UnsubscribeFromStatUpdates(StatType statType, StatEventHandlers.StatUpdateEventHandler handler);

        /// <summary>
        /// Unsubscribes from receiving an event when the specified type of stat changes.
        /// </summary>
        void UnsubscribeFromCustomStatUpdates(string statCode, StatEventHandlers.StatUpdateEventHandler handler);

        /// <summary>
        /// Unsubscribes from receiving an event for general stat changes, but does not unsubscribe from individual stat type subscriptions.
        /// </summary>
        void UnsubscribeFromAllStatUpdates(StatEventHandlers.StatUpdateEventHandler handler);
    }
}
