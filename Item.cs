using Enums;

namespace Items
{
    public class Item
    {
        public string Name { get; set; } = string.Empty;
        public PetStat StatAffected { get; set; }
        public int EffectAmount { get; set; }
        public int DurationInSeconds { get; set; }

        public Item(string name, PetStat statAffected, int effectAmount, int durationInSeconds)
        {
            Name = name;
            StatAffected = statAffected;
            EffectAmount = effectAmount;
            DurationInSeconds = durationInSeconds;
        }
    }
}

