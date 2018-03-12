using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Oetcker.Models.Models
{
    public class Player
    {
        #region Properties

        public Constants.ClassConstants.Class Class { get; set; }
        public PlayerItemSet CurrentItemSet { get; set; }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Name { get; set; }

        public Constants.RaceConstants.Race Race { get; set; }

        #endregion
    }
}
