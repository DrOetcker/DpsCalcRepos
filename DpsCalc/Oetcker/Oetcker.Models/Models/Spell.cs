using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Policy;
using System.Xml.Serialization;

namespace Oetcker.Models.Models
{
    public class Spell
    {
        #region Constructors

        public Spell()
        {

        }


        public Spell(int spellSpellIdent, string name, int effectAura, int effectBasePoints)
        {
            SpellIdent = spellSpellIdent;
            Name = name;
            EffectAura = effectAura;
            EffectBasePoints = effectBasePoints;
        }

        public Spell(int spellSpellIdent)
        {
            SpellIdent = spellSpellIdent;
        }

        #endregion

        #region Properties

        [XmlElement(ElementName = "ebp")]
        public int EffectBasePoints { get; set; }

        public int Id { get; set; }

        [XmlElement(ElementName = "ea")]
        public int EffectAura { get; set; }


        [XmlElement(ElementName = "n")]
        public string Name { get; set; }

        [XmlElement(ElementName = "id")]
        public int SpellIdent { get; set; }

        #endregion
    }
}
