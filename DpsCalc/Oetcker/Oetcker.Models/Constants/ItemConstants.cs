namespace Oetcker.Models.Constants
{
    public static class ItemConstants
    {
        #region Enums

        public enum ItemType
        {
            NonEquipable = 0,
            Head = 1,
            Neck = 2,
            Shoulder = 3,
            Shirt = 4,
            Chest = 5,
            Waist = 6,
            Legs = 7,
            Feet = 8,
            Wrists = 9,
            Hands = 10,
            Finger = 11,
            Trinket = 12,
            Weapon = 13,
            Shield = 14,
            Ranged = 15,
            Back = 16,
            TwoHand = 17,
            MainHand = 21,
            OffHand = 22,
            RangedRight = 26
        }

        public enum Quality
        {
            Poor = 0,
            Common = 1,
            Uncommon = 2,
            Rare = 3,
            Epic = 4,
            Legendary = 5,
            Artifact = 6
        }

        public enum Stat
        {
            Mana = 0,
            Health = 1,
            Agility = 3,
            Strength = 4,
            Intellect = 5,
            Spirit = 6,
            Stamina = 7,
            DefenseRating = 12,
            DodgeRating = 13,
            ParryRating = 14,
            BlockRating = 15,
            Armor=16,
            ResistanceHoly=17,
            ResistanceFire=18,
            ResistanceNature=19,
            ResistanceFrost=20,
            ResistanceShadow=21,
            ResistanceArcane=22,
        }

        public enum WeaponClass
        {
            NoWeapon = 0,
            Bow = 2,
            Gun = 3,
            Mac = 4,
            Sword = 7,
            FistWeapon = 13,
            Dagger = 15,
            Crossbow = 18
        }

        #endregion
    }
}
