﻿using System;
using BrokenWheel.Core.Logging;
using BrokenWheel.Core.Settings;

namespace BrokenWheel.Core.DependencyInjection
{
    public interface IModule //TODO: parameterized services/modules
    {
        /// <summary>
        /// Gets the <see cref="ILogger"/>.
        /// </summary>
        ILogger GetLogger();

        /// <summary>
        /// Gets a singleton service.
        /// </summary>
        /// <typeparam name="TService"> The registered interface type. </typeparam>
        /// <returns> Instance of interface implementation. </returns>
        /// <exception cref="DependencyException"> If service is not registered, or if there is circular injection logic. </exception>
        TService GetService<TService>();

        /// <summary>
        /// Registers a singleton service to a concrete implementation.
        /// The service should be registered as an interface, but can be any type.
        /// </summary>
        /// <typeparam name="TService"> The interface (or class) that the implementation inherits. </typeparam>
        /// <param name="implementation"> The object to be registered. </param>
        /// <exception cref="DependencyException"> If service is already registered. </exception>
        /// <exception cref="ArgumentException"> If the generic type is not an interface, or if implementation is null. </exception>
        IModule RegisterService<TService>(TService implementation);

        /// <summary>
        /// Registers a singleton service when needed via a passed function.
        /// This function takes the module as an argument, and returns an object inheriting/equalling the given type.
        /// Will not be called until the service is needed for injection.
        /// If dependencies have circular logic, this will not be apparent until injected.
        /// </summary>
        /// <typeparam name="TService"> The interface of the service being implemented. </typeparam>
        /// <typeparam name="shouldBuildImmediately"> 
        /// False by default. If true, calls the function immediately. 
        /// Used for services which operate purely off of listening. 
        /// </typeparam>
        /// <param name="serviceConstructor"> The function for constructing the service. </param>
        /// <exception cref="DependencyException"> If service is already registered. </exception>
        /// <exception cref="ArgumentException"> If the generic type is not an interface, or if function is null. </exception>
        IModule RegisterService<TService, TImpl>(Func<IModule, TImpl> serviceConstructor, bool shouldBuildImmediately = false)
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
