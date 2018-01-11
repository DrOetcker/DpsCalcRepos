using System;

namespace Oetcker.ServiceLocation
{
    public static class ServiceLocator
    {
        #region Staticfields and Constants

        private static Func<IServiceLocator> _delegate;

        #endregion

        #region Properties

        /// <summary>
        /// Gibt den akutellen ServiceLocator zurück.
        /// </summary>
        public static IServiceLocator Current
        {
            get
            {
                if (null == _delegate)
                    throw new ServiceLocatorException("ServiceLocatorHandler not set");

                return _delegate();
            }
        }

        /// <summary>
        /// Gibt an, ob ein aktueller ServiceLocator vorhanden ist.
        /// </summary>
        public static bool HasCurrent => null != _delegate;

        #endregion

        #region Methods

        /// <summary>
        /// Methode des externen Service Locators setzen.
        /// </summary>
        /// <code>SetAction(() => ServiceLocator.Current.GetInstance&lt;ServiceLocation.IServiceLocator&gt;());</code>
        /// <param name="delegate"></param>
        public static void SetAction(Func<IServiceLocator> @delegate)
        {
            _delegate = @delegate;
        }

        #endregion
    }
}
