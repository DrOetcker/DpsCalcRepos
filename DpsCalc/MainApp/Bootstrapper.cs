using System.Linq;
using System.Windows;
using Microsoft.Practices.Unity;
using Oetcker.Libs.Services;
using Oetcker.ServiceLocation;
using Prism.Events;
using Prism.Unity;

namespace DpsCalc.MainApp
{
    public class Bootstrapper : UnityBootstrapper
    {
        #region Fields

        /// <summary>
        /// Enthält die StartParameter
        /// </summary>
        private string[] _startArguments;

        #endregion

        #region Constructors

        public Bootstrapper(StartupEventArgs e)
        {
            ProcessStartupEventArgs(e);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Configures the <see cref="T:Microsoft.Practices.Unity.IUnityContainer" />. May be overwritten in a derived class to add specific
        /// type mappings required by the application.
        /// </summary>
        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();

            Container.Resolve<IEventAggregator>();
            Container.RegisterType<IServiceLocator, ServiceLocatorService>(new ContainerControlledLifetimeManager());
        }

        /// <summary>
        /// Liest die StartupEventArgs aus und wertet diese aus
        /// </summary>
        /// <param name="e"></param>
        private void ProcessStartupEventArgs(StartupEventArgs e)
        {
            _startArguments = new string[0];
            if (null == e || !e.Args.Any())
                return;
        }

        #endregion
    }
}
