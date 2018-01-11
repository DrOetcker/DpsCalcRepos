using System;
using System.Collections.Generic;

namespace Oetcker.ServiceLocation
{
    /// <summary>
    /// Generisches Service Locator Interface. Es wird benutzt um auf Instanzen eines Typs,
    /// die in einem Container liegen, zugreifen zu können.
    /// </summary>
    public interface IServiceLocator : IServiceProvider
    {
        #region Methods

        /// <summary>
        /// Get all instances of the given <paramref name="serviceType"/> currently
        /// registered in the container.
        /// </summary>
        /// <param name="serviceType">Type of object requested.</param>
        /// <returns>A sequence of instances of the requested <paramref name="serviceType"/>.</returns>
        IEnumerable<object> GetAllInstances(Type serviceType);

        /// <summary>
        /// Get all instances of the given <typeparamref name="TService"/> currently
        /// registered in the container.
        /// </summary>
        /// <typeparam name="TService">Type of object requested.</typeparam>
        /// <returns>A sequence of instances of the requested <typeparamref name="TService"/>.</returns>
        IEnumerable<TService> GetAllInstances<TService>();

        /// <summary>
        /// Gibt eine Instanz eines gegebenen <paramref name="serviceType"/> zurück.
        /// </summary>
        /// <param name="serviceType">Typ des angeforderten Objekts.</param>
        /// <returns>Die geforderte Objektinstanz.</returns>
        object GetInstance(Type serviceType);

        /// <summary>
        /// Get an instance of the given named <paramref name="serviceType"/>.
        /// </summary>
        /// <param name="serviceType">Type of object requested.</param>
        /// <param name="key">Name the object was registered with.</param>
        /// <returns>The requested service instance.</returns>
        object GetInstance(Type serviceType, string key);

        /// <summary>
        /// Get an instance of the given <typeparamref name="TService"/>.
        /// </summary>
        /// <typeparam name="TService">Type of object requested.</typeparam>
        /// <returns>The requested service instance.</returns>
        TService GetInstance<TService>();

        /// <summary>
        /// Get an instance of the given named <typeparamref name="TService"/>.
        /// </summary>
        /// <typeparam name="TService">Type of object requested.</typeparam>
        /// <param name="key">Name the object was registered with.</param>
        /// <returns>The requested service instance.</returns>
        TService GetInstance<TService>(string key);

        /// <summary>
        /// Diese Methode liefert eine Instanz des angegeben <typeparamref name="TService"/>. kann keine 
        /// gefunden werden, wird null geliefert.
        /// </summary>
        /// <typeparam name="TService">Typ des gewünschten Service</typeparam>
        /// <returns>Instanz des gewünschten Service</returns>
        /// <author>Seiler</author> 
        TService TryGetInstance<TService>();

        /// <summary>
        /// Diese Methode liefert eine Instanz des angegeben <typeparamref name="TService"/>. kann keine 
        /// gefunden werden, wird null geliefert.
        /// </summary>
        /// <typeparam name="TService">Typ des gewünschten Service</typeparam>
        /// <param name="key">Name des der Instanz unter der sie registiert wurde.</param>
        /// <returns>Instanz des gewünschten Service</returns>
        /// <author>Seiler</author> 
        TService TryGetInstance<TService>(string key);

        #endregion
    }
}
