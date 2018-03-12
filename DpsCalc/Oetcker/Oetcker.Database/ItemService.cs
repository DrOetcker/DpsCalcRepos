using System;
using System.Collections.Generic;
using System.Linq;
using Oetcker.Models.Models;

namespace Oetcker.Data
{
    public static class ItemService
    {
        private static List<Item> _items;

        #region Methods

        /// <summary>
        /// Diese Methode liefert das aktuelle Equip zurück
        /// </summary>
        /// <returns></returns>
        public static PlayerItemSet GetCurrentItemSet(Guid currentItemSetGuid)
        {
            var playerItemSets = XmlSerializer<List<PlayerItemSet>>.GetContent("PlayerItemSets");
            return playerItemSets.FirstOrDefault(pis => pis.Id == currentItemSetGuid);
        }

        public static List<Item> GetAllItems()
        {
            return _items ?? (_items = XmlSerializer<List<Item>>.GetContent("Items").OrderBy(item => item.Name).ToList());
        }

        public static void ResetCache()
        {
            _items = null;
        }

        #endregion
    }
}
