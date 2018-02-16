using System;
using System.Collections.Generic;
using System.Windows.Documents;
using MySql.Data.MySqlClient;
using Oetcker.Gui;
using Oetcker.Libs.Interfaces;
using Oetcker.ServiceLocation;
using Prism.Commands;

namespace DpsCalc.MainApp.ViewModels
{
    public class ContentViewModel : ViewModelBase
    {
        private int _test;

        #region Constructors

        /// <summary>
        /// Konstruktor
        /// </summary>
        public ContentViewModel()
        {
            Test = 7;
            ServiceLocator.Current.GetInstance<IDatabaseService>().ConnectionChange += asdf;
        }

        private void asdf()
        {
            Test = Test + 1;
        }

        #endregion

        #region Properties

        public int Test
        {
            get => _test;
            set
            {
                _test = value;
                RaisePropertyChanged(() => Test );
            }
        }

        #endregion

        #region Methods


        #endregion
    }
}
