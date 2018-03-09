using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using DpsCalc.MainApp.Converters;
using MySql.Data.MySqlClient;
using Oetcker.Data;
using Oetcker.Data.DebugData;
using Oetcker.Database;
using Oetcker.Gui;
using Oetcker.Libs.Interfaces;
using Oetcker.Models.Constants;
using Oetcker.Models.Converters;
using Oetcker.Models.Models;
using Oetcker.ServiceLocation;
using Prism.Commands;

namespace DpsCalc.MainApp.ViewModels
{
    public class MenuBarViewModel : ViewModelBase
    {
        #region Fields

        private List<Player> _players;

        #endregion

        #region Constructors

        /// <summary>
        /// Konstruktor
        /// </summary>
        public MenuBarViewModel()
        {
            CreateCommands();
            LoadPlayerFiles();
        }

        #endregion

        #region Properties

        public DelegateCommand CreateDebugDataCommand { get; private set; }
        public DelegateCommand CreatePlayerCommand { get; private set; }
        public DelegateCommand<string> LoadPlayerCommand { get; private set; }

        public List<Player> Players { get; set; }

        /// <summary>
        /// Verbindet die Datenbank neu
        /// </summary>
        public DelegateCommand RebuildItemDatabaseCommand { get; private set; }

        public DelegateCommand ReloadDatabaseCommand { get; private set; }

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
            ReloadDatabaseCommand = new DelegateCommand(OnReloadDatabaseCommand, () => true);
        }

        private void GetSpells(List<Item> items, List<Spell> spells)
        {
            if (null == spells)
                return;
            foreach (var item in items)
            {
                if (!item.Spells.Any())
                    continue;
                item.Spells = item.Spells.Select(sp =>
                {
                    var foundSpell = spells.FirstOrDefault(spell => spell.Id == sp.Id);
                    return foundSpell;
                }).ToList();
            }
        }

        private List<Spell> GetSpellsContent(DbConnection conn)
        {
            var query = "SELECT * FROM dbc_spell";
            var cmd = new MySqlCommand(query, conn.Connection);
            const string effectauraColumn = "effectAura";
            const string effectBasePointsColumn = "effectBasePoints";
            const string nameColumn = "Name";
            var result = new List<Spell>();
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    var id = reader.GetInt32("Id");
                    var name = reader.GetString(nameColumn);
                    for (var i = 1; i <= 3; i++)
                    {
                        var effectAura = reader.GetInt32(effectauraColumn + i);
                        var effectBasePoints = reader.GetInt32(effectBasePointsColumn + i);
                        if (effectAura != 0)
                            result.Add(new Spell(id, name, effectAura, effectBasePoints + 1));
                    }
                }
            }
            return result;

        }

        private void GetStats(Item item, MySqlDataReader reader)
        {
            //Armor
            item.Stats.Add(new StatKeyValuePair<ItemConstants.Stat, int>(ItemConstants.Stat.Armor, reader.GetInt32("armor")));

            //Stats
            const string type = "stat_type";
            const string value = "stat_value";
            for (var i = 1; i <= 10; i++)
            {
                var redValue = reader.GetInt32(value + i);
                if (redValue == 0)
                    continue;
                var redType = reader.GetInt32(type + i);
                item.Stats.Add(new StatKeyValuePair<ItemConstants.Stat, int>((ItemConstants.Stat)redType, redValue));
            }

            //DMG
            if (item.IsWeapon())
            {
                item.DmgMin = reader.GetInt32("dmg_min1");
                item.DmgMax = reader.GetInt32("dmg_max1");
            }

            //Resistance
            item.Stats.Add(new StatKeyValuePair<ItemConstants.Stat, int>(ItemConstants.Stat.ResistanceArcane, reader.GetInt32("arcane_res")));
            item.Stats.Add(new StatKeyValuePair<ItemConstants.Stat, int>(ItemConstants.Stat.ResistanceFire, reader.GetInt32("fire_res")));
            item.Stats.Add(new StatKeyValuePair<ItemConstants.Stat, int>(ItemConstants.Stat.ResistanceFrost, reader.GetInt32("frost_res")));
            item.Stats.Add(new StatKeyValuePair<ItemConstants.Stat, int>(ItemConstants.Stat.ResistanceHoly, reader.GetInt32("holy_res")));
            item.Stats.Add(new StatKeyValuePair<ItemConstants.Stat, int>(ItemConstants.Stat.ResistanceNature, reader.GetInt32("nature_res")));
            item.Stats.Add(new StatKeyValuePair<ItemConstants.Stat, int>(ItemConstants.Stat.ResistanceShadow, reader.GetInt32("shadow_res")));

            //Spells:
            const string spellidColumn = "spellid_";
            const string spelltriggerColumn = "spelltrigger_";
            for (var i = 1; i <= 3; i++)
            {
                //Kein Instant -> Wayne
                var spelltrigger = reader.GetInt32(spelltriggerColumn + i);
                if (spelltrigger != 1)
                    continue;
                var spellId = reader.GetInt32(spellidColumn + i);
                if (spellId == 0)
                    continue;
                item.Spells.Add(new Spell(spellId));
            }
        }

        private void LoadPlayerFiles()
        {
            Players = XmlSerializer<List<Player>>.GetContent("Players");
            RaisePropertyChanged(() => Players);

        }

        private void OnCreatePlayerCommand()
        {
        }

        private void OnLoadPlayerCommand(string s)
        {
            var loadedPlayer = Players.FirstOrDefault(player => player.Name == s);
            PlayerService.SetCurrentPlayer(loadedPlayer);
        }

        private void OnReloadDatabaseCommand()
        {
            var dbService = ServiceLocator.Current.GetInstance<IDatabaseService>();
            dbService.ConnectionChange?.Invoke();
        }

        /// <summary>
        /// Diese Methode wird aufgerufen, wenn der ReconnectDatabaseCommand gestartet wird
        /// </summary>
        private void RebuildWeaponDatabaseExecute()
        {

            var dbService = ServiceLocator.Current.GetInstance<IDatabaseService>();
            var conn = dbService.GetDbConnection();
            var spells = GetSpellsContent(conn);
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
                        DisplayId = reader.GetInt32("displayid"),
                        Name = reader.GetString("Name"),
                        Speed = reader.GetInt32("delay"),
                        Quality = (ItemConstants.Quality)quality,
                        WeaponClass = itemClass != 2 ? 0 : (ItemConstants.WeaponClass)itemSubClass,
                        Type = itemClass != 2 ? (ItemConstants.ItemType)inventoryType : (inventoryType == 15 || inventoryType == 26 ? ItemConstants.ItemType.Ranged : (ItemConstants.ItemType)inventoryType)
                    };

                    GetStats(item, reader);
                    result.Add(item);
                }

            }
            GetSpells(result, spells);
            XmlSerializer<List<Item>>.ExportToXml(result, "Items");
            ItemService.ResetCache();
            dbService.ConnectionChange?.Invoke();
        }

        #endregion
    }
}
