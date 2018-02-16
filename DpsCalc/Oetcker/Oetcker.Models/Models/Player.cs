using System;
using System.Collections.Generic;

namespace Oetcker.Models.Models
{
    public class Player
    {
        #region Properties

        public Constants.ClassConstants.Class Class { get; set; }
        public Guid CurrentItemSet { get; set; }
        public string Name { get; set; }
        public Constants.RaceConstants.Race Race { get; set; }

        #endregion
    }
}
