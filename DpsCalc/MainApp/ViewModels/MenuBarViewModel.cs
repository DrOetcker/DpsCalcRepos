using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using MySql.Data.MySqlClient;
using Oetcker.Data;
using Oetcker.Data.DebugData;
using Oetcker.Database;
using Oetcker.Gui;
using Oetcker.Libs.Interfaces;
using Oetcker.Models.Constants;
using Oetcker.Models.Models;
using Oetcker.ServiceLocation;
using Prism.Commands;

namespace DpsCalc.MainApp.ViewModels
{
    public class MenuBarViewModel : ViewModelBase
    {
        private List<Player> _players;

        #region Constructors

        /// <summary>
        /// Konstruktor
        /// </summary>
        public MenuBarViewModel()
        {
            CreateCommands();
            LoadPlayerFiles();
        }

        private void LoadPlayerFiles()
        {
            Players = XmlSerializer<List<Player>>.GetContent("Players");
            RaisePropertyChanged(() => Players);

        }

        #endregion

        #region Properties

        public DelegateCommand CreateDebugDataCommand { get; private set; }
        public DelegateCommand LoadDbDataCommand { get; private set; }
        public DelegateCommand<string> LoadPlayerCommand { get; private set; }
        public DelegateCommand CreatePlayerCommand { get; private set; }

        /// <summary>
        /// Verbindet die Datenbank neu
        /// </summary>
        public DelegateCommand RebuildItemDatabaseCommand { get; private set; }

        #endregion

        #region Methods

        /// <summary>
        /// Erstellt alle Commands für das ViewModel
        /// </summary>
        private void CreateCommands()
        {
            RebuildItemDatabaseCommand = new DelegateCommand(RebuildWeaponDatabaseExecute, () => true);
            CreateDebugDataCommand = new DelegateCommand(DataCreator.CreateDebugData, () => true);
            LoadPlayerCommand = new DelegateCommand<string>(OnLoadPlayerCommand, (string name) => true);
            CreatePlayerCommand = new DelegateCommand(OnCreatePlayerCommand, () => true);
            LoadDbDataCommand = new DelegateCommand(OnLoadDbDataCommand, () => true);
        }

        private void OnLoadDbDataCommand()
        {
            var dbService = ServiceLocator.Current.GetInstance<IDatabaseService>();
            var conn = dbService.GetDbConnection();
            var query = "select * from item_template Where entry = 18823";
            var cmd = new MySqlCommand(query, conn.Connection);
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                }
            }
        }

        public List<Player> Players { get; set; }
        private void OnCreatePlayerCommand()
        {
        }

        private void OnLoadPlayerCommand(string s)
        {
        }

        /// <summary>
        /// Diese Methode wird aufgerufen, wenn der ReconnectDatabaseCommand gestartet wird
        /// </summary>
        private void RebuildWeaponDatabaseExecute()
        {
            var dbService = ServiceLocator.Current.GetInstance<IDatabaseService>();
            var conn = dbService.GetDbConnection();
            var query = "SELECT * FROM item_template";
            var cmd = new MySqlCommand(query, conn.Connection);
            var result = new List<Item>();
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    var itemClass = reader.GetInt32("class");
                    var itemSubClass = reader.GetInt32("subclass");
                    var inventoryType = reader.GetInt32("InventoryType");
                    var quality = reader.GetInt32("Quality");
                    if (itemClass != 2 && itemClass != 4)
                        continue;
                    if (itemClass == 2 && (itemSubClass != 2 && itemSubClass != 3 && itemSubClass != 4 && itemSubClass != 7 && itemSubClass != 13 && itemSubClass != 15 && itemSubClass != 18))
                        continue;
                    if (itemClass == 4 && (inventoryType == 0 || inventoryType == 4 || inventoryType == 14 || inventoryType == 17 || inventoryType == 18 || inventoryType == 19 || inventoryType == 20 || inventoryType >= 23))
                        continue;
                    if (quality != 3 && quality != 4 && quality != 5)
                        continue;
                    var item = new Item
                    {
                        Id = reader.GetInt32("entry"),
                        Name = reader.GetString("Name"),
                        Speed = reader.GetInt32("delay"),
                        Quality = (ItemContants.Quality)quality,
                        WeaponClass = itemClass != 2 ? 0 : (ItemContants.WeaponClass)itemSubClass,
                        Type = (ItemContants.ItemType)inventoryType
                    };

                    GetStats(item, reader);
                    result.Add(item);
                }

            }
            GetSpells(result, conn);
            XmlSerializer<List<Item>>.ExportToXml(result, "Items");
            dbService.ConnectionChange?.Invoke();
        }

        private void GetSpells(List<Item> items, DbConnection conn)
        {
            var query = "SELECT * FROM dbc_spell";
            var cmd = new MySqlCommand(query, conn.Connection);
            const string effectauraColumn = "effectAura";
            const string effectBasePointsColumn = "effectBasePoints";
            const string nameColumn = "Name";
            using (var reader = cmd.ExecuteReader())
            {
                foreach (var item in items)
                {
                    while (reader.Read())
                    {
                        var id = reader.GetInt32("Id");
                        for (var i = 1; i <= 3; i++)
                        {
                            var effectAura = reader.GetInt32(effectauraColumn + i);
                            var effectBasePoints = reader.GetInt32(effectBasePointsColumn + i);
                            var name = reader.GetString(nameColumn + i);
                            var itemToEdit = item.Spells.FirstOrDefault(sp => sp.Id == id);
                            if (itemToEdit != null)
                            {
                                itemToEdit.Name = name;
                                itemToEdit.EffectAura = effectAura;
                                itemToEdit.EffectBasePoints = effectBasePoints;
                            }
                        }
                    }
                }
            }
        }

        private void GetStats(Item item, MySqlDataReader reader)
        {
            const string type = "stat_type";
            const string value = "stat_value";
            for (var i = 1; i <= 10; i++)
            {
                var redValue = reader.GetInt32(value + i);
                if (redValue == 0)
                    continue;
                var redType = reader.GetInt32(type + i);
                if (redType == 3)
                    item.Agility = redValue;
                else if (redType == 4)
                    item.Strength = redValue;
                else if (redType == 7)
                    item.Stamina = redValue;
            }

            //Spells:
            const string spellidColumn = "spellid_";
            const string spelltriggerColumn = "spelltrigger_";
            for (var i = 1; i <= 3; i++)
            {
                //Kein Instant -> Wayne
                var redValue = reader.GetInt32(spelltriggerColumn + i);
                if (redValue != 1)
                    continue;
                var spellId = reader.GetInt32(spellidColumn + i);
                if (redValue == 0)
                    continue;
                item.Spells.Add(new Spell(spellId));
            }
        }

        #endregion
    }
}
