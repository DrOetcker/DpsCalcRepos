using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using Oetcker.Models.Constants;

namespace Oetcker.Models.Models
{
    public class Item
    {
        #region Properties

        [XmlElement(ElementName = "n")]
        public string Name { get; set; }

        [XmlElement(ElementName = "st")]
        public List<StatKeyValuePair<ItemContants.Stat, int>> Stats { get; set; } = new List<StatKeyValuePair<ItemContants.Stat, int>>();

        [XmlElement(ElementName = "dmi")]
        public double DmgMin { get; set; }

        [XmlElement(ElementName = "dma")]
        public double DmgMax { get; set; }


        [XmlElement(ElementName = "s")]
        public double Speed { get; set; }

        [XmlElement(ElementName = "id")]
        public int Id { get; set; }

        [XmlElement(ElementName = "wc")]
        public ItemContants.WeaponClass WeaponClass { get; set; }

        [XmlElement(ElementName = "t")]
        public ItemContants.ItemType Type { get; set; }

        [XmlElement(ElementName = "q")]
        public ItemContants.Quality Quality { get; set; }


        [XmlElement(ElementName = "sp")]
        public List<Spell> Spells { get; set; } = new List<Spell>();

        #endregion

        #region Methods

        public bool IsWeapon()
        {
            return WeaponClass != ItemContants.WeaponClass.NoWeapon;
        }

        public override string ToString()
        {
            return $"Name:\t{Name}\r\n" +
                   $"Type:\t{Type}\r\n" +
                   (Math.Abs(DmgMin) < double.Epsilon || Math.Abs(DmgMin) < double.Epsilon ? string.Empty : $"DMG:\t{DmgMin} - {DmgMax}\r\n") +
                   (Math.Abs(Speed) < double.Epsilon ? string.Empty : $"Speed:\t{Speed / 1000}\r\n") +
                   $"Stats:{string.Join("", Stats.Select(st =>  (st.Value == 0) ? string.Empty : $"\t+ {st.Value} {st.Key}\r\n"))}" +
                   $"Spells:\t{string.Join("\r\n\t", Spells.Select(sp => $"ID: {sp.Id} - Name: {sp.Name} - Aura: {sp.EffectAura} - BasePoints: {sp.EffectBasePoints}"))}\r\n" +
                   $"Quality:\t{Quality}\r\n\r\n\r\n";
        }

        #endregion
    }

    [Serializable]
    [XmlType(TypeName = "skvp")]
    public struct StatKeyValuePair<K, V>
    {
        #region Constructors

        public StatKeyValuePair(K key, V value) : this()
        {
            this.Key = key;
            this.Value = value;
        }

        #endregion

        #region Properties

        public K Key
        { get; set; }

        public V Value
        { get; set; }

        #endregion
    }
}
