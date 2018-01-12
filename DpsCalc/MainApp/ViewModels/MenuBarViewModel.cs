using System.Collections.Generic;
using System.Windows.Documents;
using MySql.Data.MySqlClient;
using Oetcker.Gui;
using Oetcker.Libs.Interfaces;
using Oetcker.ServiceLocation;
using Prism.Commands;

namespace DpsCalc.MainApp.ViewModels
{
    public class MenuBarViewModel : ViewModelBase
    {
        #region Constructors

        /// <summary>
        /// Konstruktor
        /// </summary>
        public MenuBarViewModel()
        {
            CreateCommands();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Verbindet die Datenbank neu
        /// </summary>
        public DelegateCommand ReconnectDatabaseCommand { get; private set; }

        #endregion

        #region Methods

        /// <summary>
        /// Erstellt alle Commands für das ViewModel
        /// </summary>
        private void CreateCommands()
        {
            ReconnectDatabaseCommand = new DelegateCommand(ReconnectDatabaseExecute, () => true);
        }

        /// <summary>
        /// Diese Methode wird aufgerufen, wenn der ReconnectDatabaseCommand gestartet wird
        /// </summary>
        private void ReconnectDatabaseExecute()
        {
            var dbService = ServiceLocator.Current.GetInstance<IDatabaseService>();
            var conn = dbService.GetDbConnection();
            var query = "SELECT * FROM item_template";
            var cmd = new MySqlCommand(query, conn.Connection);
            var reader = cmd.ExecuteReader();
            var result = new List<string>();
            while (reader.Read())
            {
                result.Add(reader.GetString("name"));
            }

        }

        #endregion
    }
}
