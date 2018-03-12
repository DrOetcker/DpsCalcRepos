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
            return _items ?? (_items = ItemService.GetAllItemsFromDatabase().OrderBy(item => item.Name).ToList());
        }

        private static List<Item> GetAllItemsFromDatabase()
        {
            using (var dbContext = new Database())
            {
                return dbContext.Items.ToList().ToList();
            }
        }

        public static void ResetCache()
        {
            _items = null;
        }

        #endregion

        public static List<Item> WriteItems(List<Item> items)
        {
            using (var dbContext = new Database())
            {
                dbContext.Items.RemoveRange(dbContext.Items);
                dbContext.Items.AddRange(items);
                dbContext.SaveChanges();
                return dbContext.Items.ToList();
            }
        }
    }
}
