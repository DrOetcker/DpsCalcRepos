using System;
using System.IO;
using System.Windows;
using Oetcker.Libs.Interfaces;
using Oetcker.ServiceLocation;

namespace DpsCalc.MainApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        /// <summary>
        ///     Löst das "Application.Startup"-Ereignis aus.
        /// </summary>
        /// <param name="e">Eine "StartupEventArgs"-Klasse, die die Ereignisdaten enthält.</param>
        protected override void OnStartup(StartupEventArgs e)
        {
            try
            {
                base.OnStartup(e);
                var boot = new Bootstrapper(e);
                boot.Run(true);
            }
            catch (Exception ex)
            {
                // Exceptions beim StartAsync landen oft nicht im Log - Special Handling.
                File.AppendAllText(
                    Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "DpsCalcError.TXT"),
                    $@"Startup Exception:  {ex.Message}{Environment.NewLine}{ex.StackTrace}{Environment.NewLine}");

            }
        }
    }
}
