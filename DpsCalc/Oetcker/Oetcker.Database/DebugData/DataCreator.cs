using System;
using System.Collections.Generic;
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
            var playerItemSets = new List<PlayerItemSet>
            {
                new PlayerItemSet
                {
                    Name = "Current",
                    Id = currentGuid,
                    PlayerItems = new List<PlayerItem>
                    {
                        new PlayerItem
                        {
                            Type = ItemContants.ItemType.WeaponOffhand,
                            WeaponSlot = ItemContants.WeaponSlot.OffHand
                        },
                        new PlayerItem
                        {
                            Type = ItemContants.ItemType.WeaponOneHand,
                            WeaponSlot = ItemContants.WeaponSlot.MainHand
                        },
                        new PlayerItem
                        {
                            Type = ItemContants.ItemType.Head,
                            WeaponSlot = ItemContants.WeaponSlot.OffHand
                        }
                    }
                },
                new PlayerItemSet
                {
                    Name = "End",
                    Id = Guid.NewGuid(),
                    PlayerItems = new List<PlayerItem>
                    {
                        new PlayerItem
                        {
                            Type = ItemContants.ItemType.WeaponOffhand,
                            WeaponSlot = ItemContants.WeaponSlot.OffHand
                        },
                        new PlayerItem
                        {
                            Type = ItemContants.ItemType.WeaponOneHand,
                            WeaponSlot = ItemContants.WeaponSlot.MainHand
                        },
                        new PlayerItem
                        {
                            Type = ItemContants.ItemType.Head,
                            WeaponSlot = ItemContants.WeaponSlot.OffHand
                        }
                    }
                }
            };
            XmlSerializer<List<PlayerItemSet>>.ExportToXml(playerItemSets, "PlayerItemSets");
        }

        #endregion
    }
}
