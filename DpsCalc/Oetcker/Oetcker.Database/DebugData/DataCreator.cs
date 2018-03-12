using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using Oetcker.Models.Constants;
using Oetcker.Models.Models;

namespace Oetcker.Data.DebugData
{
    public static class DataCreator
    {
        #region Methods

        public static void CreateDebugData()
        {
            var rand = new Random(DateTime.Now.Millisecond);
            PlayerService.WritePlayer(CreatePlayer("Oetcker", rand));
            PlayerService.WritePlayer(CreatePlayer("Colle", rand));
            PlayerService.WritePlayer(CreatePlayer("Ché", rand));
            ItemService.ResetCache();
            PlayerService.ResetCache();
        }

        private static Player CreatePlayer(string name, Random rand)
        {
            var allItems = ItemService.GetAllItems();
            var currentGuid = Guid.NewGuid();
            var player = new Player
            {
                Class = ClassConstants.Class.Rogue,
                Race = RaceConstants.Race.Undead,
                Name = name,
                CurrentItemSet = new PlayerItemSet
                {
                    Name = "Current",
                    Id = currentGuid,
                    PlayerItems = new List<Item>
                        {
                            allItems.Where(item => item.Type == ItemConstants.ItemType.Head).ToList()[rand.Next(0, allItems.Where(item => item.Type == ItemConstants.ItemType.Head).ToList().Count - 1)],
                            allItems.Where(item => item.Type == ItemConstants.ItemType.Neck).ToList()[rand.Next(0, allItems.Where(item => item.Type == ItemConstants.ItemType.Neck).ToList().Count - 1)],
                            allItems.Where(item => item.Type == ItemConstants.ItemType.Shoulder).ToList()[rand.Next(0, allItems.Where(item => item.Type == ItemConstants.ItemType.Shoulder).ToList().Count - 1)],
                            allItems.Where(item => item.Type == ItemConstants.ItemType.Back).ToList()[rand.Next(0, allItems.Where(item => item.Type == ItemConstants.ItemType.Back).ToList().Count - 1)],
                            allItems.Where(item => item.Type == ItemConstants.ItemType.Chest).ToList()[rand.Next(0, allItems.Where(item => item.Type == ItemConstants.ItemType.Chest).ToList().Count - 1)],
                            allItems.Where(item => item.Type == ItemConstants.ItemType.Wrists).ToList()[rand.Next(0, allItems.Where(item => item.Type == ItemConstants.ItemType.Wrists).ToList().Count - 1)],
                            allItems.Where(item => item.Type == ItemConstants.ItemType.Hands).ToList()[rand.Next(0, allItems.Where(item => item.Type == ItemConstants.ItemType.Hands).ToList().Count - 1)],
                            allItems.Where(item => item.Type == ItemConstants.ItemType.Waist).ToList()[rand.Next(0, allItems.Where(item => item.Type == ItemConstants.ItemType.Waist).ToList().Count - 1)],
                            allItems.Where(item => item.Type == ItemConstants.ItemType.Legs).ToList()[rand.Next(0, allItems.Where(item => item.Type == ItemConstants.ItemType.Legs).ToList().Count - 1)],
                            allItems.Where(item => item.Type == ItemConstants.ItemType.Feet).ToList()[rand.Next(0, allItems.Where(item => item.Type == ItemConstants.ItemType.Feet).ToList().Count - 1)],
                            allItems.Where(item => item.Type == ItemConstants.ItemType.Finger).ToList()[rand.Next(0, allItems.Where(item => item.Type == ItemConstants.ItemType.Finger).ToList().Count - 1)],
                            allItems.Where(item => item.Type == ItemConstants.ItemType.Finger).ToList()[rand.Next(0, allItems.Where(item => item.Type == ItemConstants.ItemType.Finger).ToList().Count - 1)],
                            allItems.Where(item => item.Type == ItemConstants.ItemType.Trinket).ToList()[rand.Next(0, allItems.Where(item => item.Type == ItemConstants.ItemType.Trinket).ToList().Count - 1)],
                            allItems.Where(item => item.Type == ItemConstants.ItemType.Trinket).ToList()[rand.Next(0, allItems.Where(item => item.Type == ItemConstants.ItemType.Trinket).ToList().Count - 1)],
                            allItems.Where(item => item.Type == ItemConstants.ItemType.MainHand).ToList()[rand.Next(0, allItems.Where(item => item.Type == ItemConstants.ItemType.MainHand).ToList().Count - 1)],
                            allItems.Where(item => item.Type == ItemConstants.ItemType.OffHand).ToList()[rand.Next(0, allItems.Where(item => item.Type == ItemConstants.ItemType.OffHand).ToList().Count - 1)],
                            allItems.Where(item => item.Type == ItemConstants.ItemType.Ranged).ToList()[rand.Next(0, allItems.Where(item => item.Type == ItemConstants.ItemType.Ranged).ToList().Count - 1)]
                        }
                }
            };
            return player;
        }

        #endregion
    }
}
