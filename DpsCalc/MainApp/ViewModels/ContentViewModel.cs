using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Documents;
using MySql.Data.MySqlClient;
using Oetcker.Data;
using Oetcker.Gui;
using Oetcker.Libs.Interfaces;
using Oetcker.ServiceLocation;
using Prism.Commands;

namespace DpsCalc.MainApp.ViewModels
{
    public class ContentViewModel : ViewModelBase
    {
        private string _test;

        #region Constructors

        /// <summary>
        /// Konstruktor
        /// </summary>
        public ContentViewModel()
        {
            ServiceLocator.Current.GetInstance<IDatabaseService>().ConnectionChange += LoadPlayer;
        }

        private void LoadPlayer()
        {
            var player = PlayerService.GetPlayers()?.First();
            if (null == player)
                return;
            Test = player.Name + Environment.NewLine;
            Test += ItemService.GetCurrentItemSet(player.CurrentItemSet).Name + Environment.NewLine;
            var allItems = ItemService.GetAllItems();
            var rand = new Random(DateTime.Now.Millisecond);
            Test += allItems[rand.Next(0, allItems.Count - 1)].ToString();
            Test += allItems[rand.Next(0, allItems.Count - 1)].ToString();
            Test += allItems.First(item => item.Id== 18823).ToString();
        }

        #endregion

        #region Properties

        public string Test
        {
            get => _test;
            set
            {
                _test = value;
                RaisePropertyChanged(() => Test);
            }
        }

        #endregion

        #region Methods


        #endregion
    }
}
