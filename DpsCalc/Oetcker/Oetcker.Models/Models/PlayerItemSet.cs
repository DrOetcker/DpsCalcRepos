using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Oetcker.Models.Models
{
    public class PlayerItemSet
    {
        #region Properties

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }
        public IList<Item> PlayerItems { get; set; }
        public string Name { get; set; }

        #endregion
    }
}