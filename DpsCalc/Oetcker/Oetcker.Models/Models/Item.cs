using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Windows.Media;
using System.Xml.Serialization;
using Oetcker.Models.Constants;
using Oetcker.Models.Converters;

namespace Oetcker.Models.Models
{
    public class Item
    {
        #region Fields

        private SolidColorBrush _qualityColor;

        #endregion

        #region Properties

        [XmlElement(ElementName = "dma")]
        public double DmgMax { get; set; }

        [XmlElement(ElementName = "s")]
        public double Speed { get; set; }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [XmlElement(ElementName = "id")]
        public int ItemIdent { get; set; }

        [XmlElement(ElementName = "did")]
        public int DisplayIdent { get; set; }


        [XmlElement(ElementName = "n")]
        public string Name { get; set; }

        [XmlElement(ElementName = "st")]
        public List<StatKeyValuePair<ItemConstants.Stat, int>> Stats { get; set; } = new List<StatKeyValuePair<ItemConstants.Stat, int>>();

        [XmlElement(ElementName = "dmi")]
        public double DmgMin { get; set; }

        public double Dps => Math.Round((DmgMax + DmgMin) / 2 / (Speed / 1000), 1,MidpointRounding.AwayFromZero);


        [XmlElement(ElementName = "wc")]
        public ItemConstants.WeaponClass WeaponClass { get; set; }

        [XmlElement(ElementName = "t")]
        public ItemConstants.ItemType Type { get; set; }

        [XmlElement(ElementName = "q")]
        public ItemConstants.Quality Quality { get; set; }

        public SolidColorBrush QualityColor => _qualityColor ?? (_qualityColor = new QualityToColorConverter().Convert(Quality, null, null, null) as SolidColorBrush);

        public string SmallSummary
        {
            get { return ToStringSmallest(); }
        }


        [XmlElement(ElementName = "sp")]
        public List<Spell> Spells { get; set; } = new List<Spell>();


        public string Summary
        {
            get { return ToStringSmall(); }
        }

        #endregion

        #region Methods

        public bool IsWeapon()
        {
            return WeaponClass != ItemConstants.WeaponClass.NoWeapon;
        }

        public override string ToString()
        {
            return $"Name:\t{Name}\r\n" +
                   $"Type:\t{Type}\r\n" +
                   (WeaponClass == ItemConstants.WeaponClass.NoWeapon ? string.Empty : $"WeaponClass:\t{WeaponClass}\r\n") +
                   (Math.Abs(DmgMin) < double.Epsilon || Math.Abs(DmgMin) < double.Epsilon ? string.Empty : $"DMG:\t{DmgMin} - {DmgMax}\r\n") +
                   (Math.Abs(Speed) < double.Epsilon ? string.Empty : $"Speed:\t{Speed / 1000}\r\n") +
                   (Math.Abs(DmgMin) < double.Epsilon || Math.Abs(DmgMin) < double.Epsilon ? string.Empty : $"DPS:\t({Dps} damage per second)\r\n") +
                   $"Stats:{string.Join("", Stats.Select(st => (st.Value == 0) ? string.Empty : (st.Key == ItemConstants.Stat.Armor ? $"{st.Value} {st.Key}\r\n" : $"+ {st.Value} {st.Key}\r\n")))}" +
                   $"Spells:\t{string.Join("\r\n\t", Spells.Select(sp => $"ID: {sp.SpellIdent} - Name: {sp.Name} - Aura: {sp.EffectAura} - BasePoints: {sp.EffectBasePoints}"))}\r\n" +
                   $"Quality:\t{Quality}\r\n\r\n\r\n";
        }

        private string ToStringSmall()
        {
            return $"{Type}\r\n" +
                   (WeaponClass == ItemConstants.WeaponClass.NoWeapon ? string.Empty : $"{WeaponClass}\r\n") +
                   (Math.Abs(DmgMin) < double.Epsilon || Math.Abs(DmgMin) < double.Epsilon ? string.Empty : $"{DmgMin} - {DmgMax} Damage\r\n") +
                   (Math.Abs(Speed) < double.Epsilon ? string.Empty : $"Speed {Speed / 1000}\r\n") +
                   (Math.Abs(DmgMin) < double.Epsilon || Math.Abs(DmgMin) < double.Epsilon ? string.Empty : $"({Dps} damage per second)\r\n") +
                   $"{string.Join("", Stats.Select(st => (st.Value == 0) ? string.Empty : (st.Key == ItemConstants.Stat.Armor ? $"{st.Value} {st.Key}\r\n" : $"+ {st.Value} {st.Key}\r\n")))}" +
                   $"{string.Join("", Spells.Select(sp => $"{sp.Name} - Aura: {sp.EffectAura} - BasePoints: {sp.EffectBasePoints}\r\n"))}";
        }

        private string ToStringSmallest()
        {
            return $"{Type}\r\n" +
                   (WeaponClass == ItemConstants.WeaponClass.NoWeapon ? string.Empty : $"{WeaponClass}\r\n") +
                   (Math.Abs(DmgMin) < double.Epsilon || Math.Abs(DmgMin) < double.Epsilon ? string.Empty : $"{DmgMin} - {DmgMax} Damage\r\n") +
                   (Math.Abs(Speed) < double.Epsilon ? string.Empty : $"Speed {Speed / 1000}\r\n") +
                   (Math.Abs(DmgMin) < double.Epsilon || Math.Abs(DmgMin) < double.Epsilon ? string.Empty : $"({Dps} damage per second)\r\n") +
                   $"{string.Join("", Stats.Select(st => (st.Value == 0) ? string.Empty : (st.Key == ItemConstants.Stat.Armor ? $"{st.Value} {st.Key}\r\n" : $"+ {st.Value} {st.Key}\r\n")))}" +
                   $"{string.Join("", Spells.Select(sp => $"{sp.Name} - {sp.EffectAura} - {sp.EffectBasePoints}\r\n"))}";
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
