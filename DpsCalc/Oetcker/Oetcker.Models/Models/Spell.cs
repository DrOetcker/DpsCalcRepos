using System.Xml.Serialization;

namespace Oetcker.Models.Models
{
    public class Spell
    {
        #region Constructors

        public Spell()
        {

        }


        public Spell(int spellId, string name, int effectAura, int effectBasePoints)
        {
            Id = spellId;
            Name = name;
            EffectAura = effectAura;
            EffectBasePoints = effectBasePoints;
        }

        public Spell(int spellId)
        {
            Id = spellId;
        }

        #endregion

        #region Properties

        [XmlElement(ElementName = "ebp")]
        public int EffectBasePoints { get; set; }

        [XmlElement(ElementName = "ea")]
        public int EffectAura { get; set; }


        [XmlElement(ElementName = "n")]
        public string Name { get; set; }

        [XmlElement(ElementName = "id")]
        public int Id { get; set; }

        #endregion
    }
}
