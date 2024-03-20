using System;
using BrokenWheel.Core.Events;
using BrokenWheel.Core.Logging;
using BrokenWheel.Core.Settings;
using BrokenWheel.Core.Time;

namespace BrokenWheel.Core.DependencyInjection
{
    public interface IModule //TODO: parameterized services/modules
    {
        /// <summary>
        /// Conveniently gets the default <see cref="ILogger"/> implementation.
        /// This service must be registered first.
        /// </summary>
        ILogger GetLogger();

        /// <summary>
        /// Conveniently gets the default <see cref="IEventAggregator"/> implementation.
        /// This service must be registered first.
        /// </summary>
        IEventAggregator GetEventAggregator();

        /// <summary>
        /// Conveniently gets the default <see cref="ITimeService"/> implementation.
        /// This service must be registered first.
        /// </summary>
        ITimeService GetTimeService();

        /// <summary>
        /// Gets a singleton service that corresponds to the given parameter.
        /// 
        /// </summary>
        /// <typeparam name="TService"> The registered interface type. </typeparam>
        /// <returns> Instance of interface implementation. </returns>
        /// <exception cref="DependencyException"> If service is not registered, or if there is circular injection logic. </exception>
        TService GetService<TService>(string parameter = null);

        /// <summary>
        /// Instantiates all services marked for immediate building.
        /// </summary>
        void CompleteInitialRegistration();

        /// <summary>
        /// Registers a singleton service when needed via a passed function.
        /// This function takes the module as an argument, and returns an object inheriting/equalling the given type.
        /// Will not be called until the service is needed for injection.
        /// If dependencies have circular logic, this will not be apparent until injected.
        /// This function will also be used to instantiate new parameterized services.
        /// </summary>
        /// <typeparam name="TService">
        /// The interface of the service being implemented.
        /// </typeparam>
        /// <typeparam name="TImpl">
        /// The class implementing the service type.
        /// </typeparam>
        /// <param name="serviceConstructor">
        /// The function for constructing the service.
        /// </param>
        /// <param name="shouldBuildImmediately">
        /// False by default.
        /// If true, on <see cref="CompleteInitialRegistration"/> the service will be instantiated without <see cref="GetService{TService}()"/> being called -
        /// or, if registration is already completed, will call it without delay
        /// Used for services which operate purely off of listening. 
        /// </param>
        /// <param name="parameter">
        /// Null string by default.
        /// The parameter which should use the given function as a unique constructor.
        /// If parameter is null or white space, the service will <b>not</b> be parameterized.
        /// </param>
        /// <exception cref="DependencyException">
        /// If service is already registered (with the same parameter).
        /// </exception>
        /// <exception cref="ArgumentException">
        /// If the generic type is not an interface, or if function is null.
        /// </exception>
        IModule RegisterService<TService, TImpl>(
            Func<IModule, TImpl> serviceConstructor,
            bool shouldBuildImmediately = false,
            string parameter = null)
            where TImpl : class, TService;

        /// <summary>
        /// Registers a settings POCO (which may be changed unpredictably) to be accessed across the program.
        /// </summary>
        /// <typeparam name="TSettings"> The concrete <see cref="ISettings"/> class to get. </typeparam>
        /// <exception cref="InvalidOperationException"> If settings are not registered. </exception>
        TSettings GetSettings<TSettings>()
            where TSettings : class, ISettings;

        /// <summary>
        /// Registers a settings POCO to be accessed across the program.
        /// </summary>
        /// <typeparam name="TSettings"> The concrete type of <see cref="ISettings"/> object to register. </typeparam>
        /// <param name="settings"> The instantiated settings object to register. </param>
        /// <exception cref="InvalidOperationException"> If settings are already registered. </exception>
        /// <exception cref="ArgumentNullException"> If settings are null. </exception>
        IModule RegisterSettings<TSettings>(TSettings settings)
            where TSettings : class, ISettings;
    }
}
