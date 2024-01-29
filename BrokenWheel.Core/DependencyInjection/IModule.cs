using BrokenWheel.Core.Logging;

namespace BrokenWheel.Core.DependencyInjection
{
    public interface IModule
    {
        /// <summary>
        /// Gets the <see cref="ILogger"/>.
        /// </summary>
        ILogger GetLogger();

        /// <summary>
        /// Gets singleton interfaced services to be statically accessible through <see cref="DI"/>.
        /// </summary>
        /// <typeparam name="TInterface"> The registered interface type. </typeparam>
        /// <returns> Instance of interface implementation. </returns>
        TInterface Get<TInterface>();

        /// <summary>
        /// Registers singleton interfaced services to be statically accessible through <see cref="DI"/>.
        /// </summary>
        /// <typeparam name="TInterface"> The interface that the implementation inherits. </typeparam>
        /// <typeparam name="TImplementation"> The implementation of the interface type. </typeparam>
        /// <param name="implementation"> The object to be registered. </param>
        void Register<TInterface, TImplementation>(TImplementation implementation) where TImplementation : class;

        /// <typeparam name="TInterface"> The interface which should have a registered service. </typeparam>
        /// <returns> Whether the specified interface has a registered service. </returns>
        bool IsRegistered<TInterface>();
    }
}
