using System;
using BrokenWheel.Core.Events;
using BrokenWheel.Core.Logging;
using BrokenWheel.Core.Time;

namespace BrokenWheel.Core.DependencyInjection.Implementation
{
    public partial class Module : IModule
    {
        protected readonly ILogger Logger;

        /// <summary>
        /// The default implementation for <see cref="IModule"/> which only needs a <see cref="ILogger"/>.
        /// </summary>
        /// <exception cref="ArgumentNullException"> If logger is null. </exception>
        public Module(ILogger logger)
        {
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
            Logger.LogCategory(LogCategory.DI, $"Initialized {typeof(Module)}.");
        }

        /// <inheritdoc/>
        public ILogger GetLogger() => Logger;

        /// <inheritdoc/>
        public IEventAggregator GetEventAggregator() => GetService<IEventAggregator>();

        /// <inheritdoc/>
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
            Logger.LogCategoryError(LogCategory.DI, message);
            throw new DependencyException(message, innerException);
        }
    }
}
