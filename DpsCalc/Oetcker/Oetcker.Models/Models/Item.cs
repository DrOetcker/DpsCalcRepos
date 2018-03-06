using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace Oetcker.Models.Models
{
    public class Item
    {
        #region Properties

        [XmlElement(ElementName = "wc")]
        public Constants.ItemContants.WeaponClass WeaponClass { get; set; }

        [XmlElement(ElementName = "t")]
        public Constants.ItemContants.ItemType Type { get; set; }

        [XmlElement(ElementName = "q")]
        public Constants.ItemContants.Quality Quality { get; set; }

        [XmlElement(ElementName = "n")]
        public string Name { get; set; }

        [XmlElement(ElementName = "agi")]
        public int Agility { get; set; }

        [XmlElement(ElementName = "str")]
        public int Strength { get; set; }

        [XmlElement(ElementName = "stam")]
        public int Stamina { get; set; }

        [XmlElement(ElementName = "ap")]
        public int Attackpower { get; set; }

        [XmlElement(ElementName = "c")]
        public double Crit { get; set; }

        [XmlElement(ElementName = "h")]
        public double Hit { get; set; }

        [XmlElement(ElementName = "dmi")]
        public double DmgMin { get; set; }

        [XmlElement(ElementName = "dma")]
        public double DmgMax { get; set; }


        [XmlElement(ElementName = "s")]
        public double Speed { get; set; }

        [XmlElement(ElementName = "id")]
        public int Id { get; set; }

        [XmlElement(ElementName = "sp")]
        public List<Spell> Spells { get; set; } = new List<Spell>();

        #endregion

        #region Methods

        public override string ToString()
        {
            return $"{Name}\r\n{Quality}\r\n{Type}\r\n{Speed / 1000}\r\n{string.Join(",", Spells.Select(sp => sp.Name))}";
        }

        #endregion
    }
}
