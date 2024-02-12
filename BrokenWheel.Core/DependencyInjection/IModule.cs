using System;
using BrokenWheel.Core.Logging;
using BrokenWheel.Core.Settings;

namespace BrokenWheel.Core.DependencyInjection
{
    public interface IModule
    {
        /// <summary>
        /// Gets the <see cref="ILogger"/>.
        /// </summary>
        ILogger GetLogger();

        /// <summary>
        /// Gets singleton interfaced services to be statically accessible through <see cref="Injection"/>.
        /// </summary>
        /// <typeparam name="TInterface"> The registered interface type. </typeparam>
        /// <returns> Instance of interface implementation. </returns>
        /// <exception cref="InvalidOperationException"> If service is not registered. </exception>
        TInterface GetService<TInterface>();

        /// <summary>
        /// Registers singleton interfaced services to be statically accessible through <see cref="Injection"/>.
        /// </summary>
        /// <typeparam name="TInterface"> The interface that the implementation inherits. </typeparam>
        /// <typeparam name="TImplementation"> The implementation of the interface type. </typeparam>
        /// <param name="implementation"> The object to be registered. </param>
        /// <exception cref="InvalidOperationException"> If service is already registered. </exception>
        /// <exception cref="ArgumentException"> If the generic type is not an interface, or if implementation is null. </exception>
        IModule RegisterService<TInterface, TImplementation>(TImplementation implementation) where TImplementation : class, TInterface;

        /// <summary>
        /// Registers a settings POCO (which may be changed) to be accessed across the program.
        /// </summary>
        /// <typeparam name="TSettings"> The concrete <see cref="ISettings"/> class to get. </typeparam>
        /// <exception cref="InvalidOperationException"> If settings are not registered. </exception>
        TSettings GetSettings<TSettings>() where TSettings : class, ISettings;

        /// <summary>
        /// Registers a settings POCO (which may be changed) to be accessed across the program.
        /// </summary>
        /// <typeparam name="TSettings"> The concrete type of <see cref="ISettings"/> object to register. </typeparam>
        /// <param name="settings"> The instantiated settings object to register. </param>
        /// <exception cref="InvalidOperationException"> If settings are already registered. </exception>
        /// <exception cref="ArgumentNullException"> If settings are null. </exception>
        IModule RegisterSettings<TSettings>(TSettings settings) where TSettings : class, ISettings;
    }
}
