using System;
using System.Collections.Generic;
using System.Linq;
using Oetcker.Data;
using Oetcker.Gui;
using Oetcker.Libs.Interfaces;
using Oetcker.Models.Models;
using Oetcker.ServiceLocation;

namespace DpsCalc.MainApp.ViewModels
{
    public class ContentViewModel : ViewModelBase
    {
        #region Fields

        private Player _currentPlayer;

        private string _test;
        private PlayerItemSet _currentItemSet;
        private List<Item> _allItems;

        #endregion

        #region Constructors

        /// <summary>
        /// Konstruktor
        /// </summary>
        public ContentViewModel()
        {
            ServiceLocator.Current.GetInstance<IDatabaseService>().ConnectionChange += LoadItems;
            PlayerService.CurrentPlayerChanged += OnPlayerChanged;
            AllItems = ItemService.GetAllItems();
        }

        #endregion

        #region Properties

        public List<Item> AllItems
        {
            get => _allItems;
            set
            {
                _allItems = value;
                RaisePropertyChanged(() => AllItems);
            }
        }

        public Player CurrentPlayer
        {
            get => _currentPlayer;
            set
            {
                _currentPlayer = value;
                RaisePropertyChanged(() => CurrentPlayer);
            }
        }

        public PlayerItemSet CurrentItemSet
        {
            get => _currentItemSet;
            set
            {
                _currentItemSet = value;
                RaisePropertyChanged(() => CurrentItemSet);
            }
        }

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

        private void LoadItems()
        {
            AllItems = ItemService.GetAllItems();
            var rand = new Random(DateTime.Now.Millisecond);
            Test += AllItems[rand.Next(0, AllItems.Count - 1)].ToString();
            Test += AllItems[rand.Next(0, AllItems.Count - 1)].ToString();
            Test += AllItems.First(item => item.Id == 18823).ToString();
        }

        private void OnPlayerChanged(Player player)
        {
            if (null == player)
                return;
            CurrentPlayer = player;
            CurrentItemSet = ItemService.GetCurrentItemSet(player.CurrentItemSet);
            Test = player.Name + Environment.NewLine;
            Test += CurrentItemSet.Name + Environment.NewLine;
            //currentItemSet.PlayerItems.ForEach(pi => { Test += pi.ToString(); });

        }

        #endregion
    }
}
