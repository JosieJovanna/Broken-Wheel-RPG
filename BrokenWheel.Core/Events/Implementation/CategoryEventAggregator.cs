using System;
using System.Collections.Generic;

namespace BrokenWheel.Core.Events.Implementation
{
    public partial class EventAggregator
    {
        private readonly IDictionary<Type, IDictionary<string, object>> _subjectsByCategoryByType = new Dictionary<Type, IDictionary<string, object>>();

        /// <inheritdoc/>
        public void Subscribe<TEvent>(EventHandlerFunction<TEvent> function, string category)
            => GetObservable<TEvent>(category).Subscribe(function);

        /// <inheritdoc/>
        public void Unsubscribe<TEvent>(EventHandlerFunction<TEvent> function, string category)
            => GetObservable<TEvent>(category).Unsubscribe(function);

        /// <inheritdoc/>
        public IEventObservable<TEvent> GetObservable<TEvent>(string category)
            => GetSubject<TEvent>(category).AsObservable();

        /// <inheritdoc/>
        public IEventSubject<TEvent> GetSubject<TEvent>(string category)
            => FindOrCreateCategorizedSubject<TEvent>(category);

        private IEventSubject<TEvent> FindOrCreateCategorizedSubject<TEvent>(string category)
        {
            if (TryGetCategorizedSubject<TEvent>(category, out var subject))
                return subject;
            else
                return CreateAndKeepCategorizedSubject<TEvent>(category);
        }

        private bool TryGetCategorizedSubject<TEvent>(string category, out IEventSubject<TEvent> subject)
        {
            subject = default;
            try
            {
                subject = GetCategorizedSubject<TEvent>(category);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private IEventSubject<TEvent> GetCategorizedSubject<TEvent>(string category)
        {
            var eventType = typeof(TEvent);
            if (!_subjectsByCategoryByType.ContainsKey(eventType))
                return default;
            var subjectsByCategory = _subjectsByCategoryByType[eventType];
            if (subjectsByCategory.ContainsKey(category))
                return (IEventSubject<TEvent>)subjectsByCategory[category];
            return default;
        }

        private IEventSubject<TEvent> CreateAndKeepCategorizedSubject<TEvent>(string category)
        {
            var eventType = typeof(TEvent);
            _logger.LogCategory("Events", $"Creating categorized subject for event `{eventType.Name}`, category `{category}`...");
            var subject = new EventSubject<TEvent>();
            if (!_subjectsByCategoryByType.ContainsKey(eventType))
                _subjectsByCategoryByType.Add(eventType, new Dictionary<string, object>());
            _subjectsByCategoryByType[eventType].Add(category, subject);
            return subject;
        }
    }
}
