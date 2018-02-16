using System.Linq;
using System.Windows;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using Oetcker.Libs.Interfaces;
using Oetcker.Libs.Services;
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
            Container.RegisterType<Oetcker.ServiceLocation.IServiceLocator, ServiceLocatorService>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IDatabaseService, DatabaseService>(new ContainerControlledLifetimeManager());
        }

        /// <summary>
        /// Configures the LocatorProvider for the <see cref="T:Microsoft.Practices.ServiceLocation.ServiceLocator" />.
        /// </summary>
        protected override void ConfigureServiceLocator()
        {
            base.ConfigureServiceLocator();
            Oetcker.ServiceLocation.ServiceLocator.SetAction(() => ServiceLocator.Current.GetInstance<Oetcker.ServiceLocation.IServiceLocator>());
            //var dbService = Oetcker.ServiceLocation.ServiceLocator.Current.GetInstance<IDatabaseService>();
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
