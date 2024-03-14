using System;
using BrokenWheel.Core.Events;
using BrokenWheel.Core.Logging;
using BrokenWheel.Core.Time;

namespace BrokenWheel.Core.DependencyInjection.Implementation
{
    public partial class Module : IModule
    {
        private readonly ILogger _logger;

        /// <summary>
        /// The default implementation for <see cref="IModule"/> which only needs a <see cref="ILogger"/>.
        /// </summary>
        /// <exception cref="ArgumentNullException"> If logger is null. </exception>
        public Module(ILogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _serviceRegistry.Add(typeof(ILogger), _logger);
            _logger.LogCategory(LogCategory.DEPENDENCY_INJECTION, $"Initialized {typeof(Module)}.");
        }


        public ILogger GetLogger() => _logger;
        public IEventAggregator GetEventAggregator() => GetService<IEventAggregator>();
        public ITimeService GetTimeService() => GetService<ITimeService>();

        /// <summary>
        /// Tries casting the given object to the specified type. Wraps exception and logs if cannot cast.
        /// </summary>
        private TCast Cast<TCast>(object instance)
        {
            try
            {
                return (TCast)instance;
            }
            catch (Exception e)
            {
                LogAndThrow($"Could not cast {instance.GetType().Name} to {typeof(TCast).Name}.", e);
                throw;
            }
        }

        /// <summary>
        /// Throws an exception after logging it.
        /// </summary>
        private void LogAndThrow(string message, Exception innerException = null)
        {
            _logger.LogCategoryError(LogCategory.DEPENDENCY_INJECTION, message);
            throw new DependencyException(message, innerException);
        }
    }
}
