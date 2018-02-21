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
            var currentGuid = Guid.NewGuid();
            CreateDebugPlayerItemSets(currentGuid);
            var player = new Player
            {
                Class = ClassConstants.Class.Rogue,
                Race = RaceConstants.Race.Undead,
                Name = "Oetcker",
                CurrentItemSet = currentGuid
            };
            XmlSerializer<Player>.ExportToXml(player, "Player");
        }

        private static void CreateDebugPlayerItemSets(Guid currentGuid)
        {
            var allItems = XmlSerializer<List<Item>>.GetContent("Items");
            var playerItemSets = new List<PlayerItemSet>
            {
                new PlayerItemSet
                {
                    Name = "Current",
                    Id = currentGuid,
                    PlayerItems = new List<Item>
                    {
                        allItems.FirstOrDefault(item => item.Type == ItemContants.ItemType.Head),
                        allItems.FirstOrDefault(item => item.Type == ItemContants.ItemType.Neck),
                        allItems.FirstOrDefault(item => item.Type == ItemContants.ItemType.Shoulder),
                        allItems.FirstOrDefault(item => item.Type == ItemContants.ItemType.Back),
                        allItems.FirstOrDefault(item => item.Type == ItemContants.ItemType.Chest),
                        allItems.FirstOrDefault(item => item.Type == ItemContants.ItemType.Wrists),
                        allItems.FirstOrDefault(item => item.Type == ItemContants.ItemType.Hands),
                        allItems.FirstOrDefault(item => item.Type == ItemContants.ItemType.Waist),
                        allItems.FirstOrDefault(item => item.Type == ItemContants.ItemType.Legs),
                        allItems.FirstOrDefault(item => item.Type == ItemContants.ItemType.Feet),
                        allItems.FirstOrDefault(item => item.Type == ItemContants.ItemType.Finger),
                        allItems.FirstOrDefault(item => item.Type == ItemContants.ItemType.Finger),
                        allItems.FirstOrDefault(item => item.Type == ItemContants.ItemType.Trinket),
                        allItems.FirstOrDefault(item => item.Type == ItemContants.ItemType.Trinket),
                        allItems.FirstOrDefault(item => item.Type == ItemContants.ItemType.MainHand),
                        allItems.FirstOrDefault(item => item.Type == ItemContants.ItemType.OffHand),
                        allItems.FirstOrDefault(item => item.Type == ItemContants.ItemType.Ranged)
                    }
                },
                new PlayerItemSet
                {
                    Name = "End",
                    Id = Guid.NewGuid(),
                    PlayerItems = new List<Item>
                    {
                        allItems.LastOrDefault(item => item.Type == ItemContants.ItemType.Head),
                        allItems.LastOrDefault(item => item.Type == ItemContants.ItemType.Neck),
                        allItems.LastOrDefault(item => item.Type == ItemContants.ItemType.Shoulder),
                        allItems.LastOrDefault(item => item.Type == ItemContants.ItemType.Back),
                        allItems.LastOrDefault(item => item.Type == ItemContants.ItemType.Chest),
                        allItems.LastOrDefault(item => item.Type == ItemContants.ItemType.Wrists),
                        allItems.LastOrDefault(item => item.Type == ItemContants.ItemType.Hands),
                        allItems.LastOrDefault(item => item.Type == ItemContants.ItemType.Waist),
                        allItems.LastOrDefault(item => item.Type == ItemContants.ItemType.Legs),
                        allItems.LastOrDefault(item => item.Type == ItemContants.ItemType.Feet),
                        allItems.LastOrDefault(item => item.Type == ItemContants.ItemType.Finger),
                        allItems.LastOrDefault(item => item.Type == ItemContants.ItemType.Finger),
                        allItems.LastOrDefault(item => item.Type == ItemContants.ItemType.Trinket),
                        allItems.LastOrDefault(item => item.Type == ItemContants.ItemType.Trinket),
                        allItems.LastOrDefault(item => item.Type == ItemContants.ItemType.MainHand),
                        allItems.LastOrDefault(item => item.Type == ItemContants.ItemType.OffHand),
                        allItems.LastOrDefault(item => item.Type == ItemContants.ItemType.Ranged)
                    }
                }
            };
            XmlSerializer<List<PlayerItemSet>>.ExportToXml(playerItemSets, "PlayerItemSets");
        }

        #endregion
    }
}
