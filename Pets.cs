using System;
using Enums;

namespace Models
{
    public class Pets
    {
        public string Name { get; }
        public int Hunger { get; private set; }
        public int Sleep { get; private set; }
        public int Fun { get; private set; }
        public PetStatus Status { get; private set; }

        private const int MAX_STAT = 100;
        private const int MIN_STAT = 0;

        public Pets(string name)
        {
            Name = name;
            Hunger = 50;
            Sleep = 50;
            Fun = 50;
            Status = PetStatus.Normal;
            UpdateStatus();
        }

        public void DecreaseStats(int amount)
        {
            Hunger = Math.Max(MIN_STAT, Hunger - amount);
            Sleep = Math.Max(MIN_STAT, Sleep - amount);
            Fun = Math.Max(MIN_STAT, Fun - amount);
            UpdateStatus();
        }

        public void IncreaseStats(PetStat stat, int amount)
        {
            switch (stat)
            {
                case PetStat.Hunger:
                    Hunger = Math.Min(MAX_STAT, Hunger + amount);
                    break;
                case PetStat.Sleep:
                    Sleep = Math.Min(MAX_STAT, Sleep + amount);
                    break;
                case PetStat.Fun:
                    Fun = Math.Min(MAX_STAT, Fun + amount);
                    break;
            }
            UpdateStatus();
        }

        private void UpdateStatus()
        {
            if (Hunger <= 20 || Sleep <= 20 || Fun <= 20)
            {
                Status = PetStatus.Unhappy;
            }
            else if (Hunger <= 10 || Sleep <= 10 || Fun <= 10)
            {
                Status = PetStatus.Sick;
            }
            else if (Hunger >= 80 && Sleep >= 80 && Fun >= 80)
            {
                Status = PetStatus.Happy;
            }
            else
            {
                Status = PetStatus.Normal;
            }
        }

        public string GetStatusEmoji()
        {
            return Status switch
            {
                PetStatus.Happy => "😊",
                PetStatus.Normal => "😐",
                PetStatus.Unhappy => "😢",
                PetStatus.Sick => "🤒",
                _ => "❓"
            };
        }

        // Daha sonra stat azaltma ve eşya etkileşimi eklenebilir
    }
}



