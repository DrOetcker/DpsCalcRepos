using System;
using System.Collections.Generic;
using Microsoft.Practices.Unity;
using Oetcker.ServiceLocation;

namespace Oetcker.Libs.Services
{
    public class ServiceLocatorService : IServiceLocator
    {
        #region Fields

        private readonly IUnityContainer _container;

        #endregion

        #region Constructors

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="container"></param>
        public ServiceLocatorService(IUnityContainer container)
        {
            _container = container ?? throw new ArgumentNullException(nameof(container));
        }

        #endregion

        #region Methods

        /// <summary>
        /// Get all instances of the given <paramref name="serviceType"/> currently
        /// registered in the container.
        /// </summary>
        /// <param name="serviceType">Type of object requested.</param>
        /// <returns>A sequence of instances of the requested <paramref name="serviceType"/>.</returns>
        public IEnumerable<object> GetAllInstances(Type serviceType)
        {
            return _container.ResolveAll(serviceType);
        }

        /// <summary>
        /// Get all instances of the given <typeparamref name="TService"/> currently
        /// registered in the container.
        /// </summary>
        /// <typeparam name="TService">Type of object requested.</typeparam>
        /// <returns>A sequence of instances of the requested <typeparamref name="TService"/>.</returns>
        public IEnumerable<TService> GetAllInstances<TService>()
        {
            return _container.ResolveAll<TService>();
        }

        /// <summary>
        /// Get an instance of the given <paramref name="serviceType"/>.
        /// </summary>
        /// <param name="serviceType">Type of object requested.</param>
        /// <returns>The requested service instance.</returns>
        public object GetInstance(Type serviceType)
        {
            return _container.Resolve(serviceType);
        }

        /// <summary>
        /// Get an instance of the given named <paramref name="serviceType"/>.
        /// </summary>
        /// <param name="serviceType">Type of object requested.</param>
        /// <param name="key">Name the object was registered with.</param>
        /// <returns>The requested service instance.</returns>
        public object GetInstance(Type serviceType, string key)
        {
            return _container.Resolve(serviceType, key);
        }

        /// <summary>
        /// Get an instance of the given <typeparamref name="TService"/>.
        /// </summary>
        /// <typeparam name="TService">Type of object requested.</typeparam>
        /// <returns>The requested service instance.</returns>
        public TService GetInstance<TService>()
        {
            return _container.Resolve<TService>();
        }

        /// <summary>
        /// Get an instance of the given named <typeparamref name="TService"/>.
        /// </summary>
        /// <typeparam name="TService">Type of object requested.</typeparam>
        /// <param name="key">Name the object was registered with.</param>
        /// <returns>The requested service instance.</returns>
        public TService GetInstance<TService>(string key)
        {
            return _container.Resolve<TService>(key);
        }

        /// <summary>
        /// Gets the service object of the specified type.
        /// </summary>
        /// <returns>
        /// A service object of type <paramref name="serviceType"/>.-or- null if there is no service object of type <paramref name="serviceType"/>.
        /// </returns>
        /// <param name="serviceType">An object that specifies the type of service object to get. </param><filterpriority>2</filterpriority>
        public object GetService(Type serviceType)
        {
            return _container.Resolve(serviceType);
        }

        /// <summary>
        /// Diese Methode liefert eine Instanz des angegeben <typeparamref name="TService"/>. Kann keine 
        /// passende Instanz gefunden werden, wird null geliefert.
        /// </summary>
        /// <typeparam name="TService">Typ des gewünschten Service</typeparam>
        /// <returns>Instanz des gewünschten Service oder null</returns>
        public TService TryGetInstance<TService>()
        {
            try
            {
                var enumarator = _container.Registrations.GetEnumerator();
                while (enumarator.MoveNext())
                {
                    if (enumarator.Current.RegisteredType == typeof(TService))
                        return GetInstance<TService>();
                }
                return default(TService);
            }
            catch (Exception exc)
            {
                return default(TService);
            }
        }

        /// <summary>
        /// Diese Methode liefert eine Instanz des angegeben <typeparamref name="TService"/>. kann keine 
        /// gefunden werden, wird null geliefert.
        /// </summary>
        /// <typeparam name="TService">Typ des gewünschten Service</typeparam>
        /// <param name="key">Name des der Instanz unter der sie registiert wurde.</param>
        /// <returns>Instanz des gewünschten Service</returns>
        public TService TryGetInstance<TService>(string key)
        {
            try
            {
                return GetInstance<TService>(key);
            }
            catch (Exception exc)
            {
                return default(TService);
            }
        }

        #endregion
    }
}
