using System;
using System.Collections.Generic;

namespace Oetcker.Models.Models
{
    public class PlayerItemSet
    {
        #region Properties

        public Guid Id { get; set; }
        public List<PlayerItem> PlayerItems { get; set; }
        public string Name { get; set; }

        #endregion
    }
}