using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

namespace Managers
{
    public enum StatType
    {
        Hunger,
        Sleep,
        Fun
    }

    public class BackgroundTaskManager
    {
        private readonly List<Pet> _pets;
        private CancellationTokenSource _cancellationTokenSource;
        private bool _isRunning;

        public BackgroundTaskManager(List<Pet> pets)
        {
            _pets = pets;
            _cancellationTokenSource = new CancellationTokenSource();
        }

        public async Task StartStatDecayTask()
        {
            if (_isRunning) return;
            _isRunning = true;

            while (!_cancellationTokenSource.Token.IsCancellationRequested)
            {
                foreach (var pet in _pets)
                {
                    pet.DecreaseStat(StatType.Hunger, 5);
                    pet.DecreaseStat(StatType.Sleep, 3);
                    pet.DecreaseStat(StatType.Fun, 4);
                }
                await Task.Delay(10000, _cancellationTokenSource.Token);
            }
        }

        public void StopTasks()
        {
            _cancellationTokenSource.Cancel();
            _isRunning = false;
        }
    }

    public class Item
    {
        public string Name { get; set; }
        public StatType StatAffected { get; set; }
        public int EffectAmount { get; set; }

        public Item(string name, StatType statAffected, int effectAmount)
        {
            Name = name;
            StatAffected = statAffected;
            EffectAmount = effectAmount;
        }
    }

    public class Pet
    {
        public string Name { get; set; }
        public int Hunger { get; set; } = 100;
        public int Sleep { get; set; } = 100;
        public int Fun { get; set; } = 100;

        public Pet(string name)
        {
            Name = name;
        }

        public void IncreaseStat(StatType stat, int amount)
        {
            switch (stat)
            {
                case StatType.Hunger:
                    Hunger = Math.Min(100, Hunger + amount);
                    break;
                case StatType.Sleep:
                    Sleep = Math.Min(100, Sleep + amount);
                    break;
                case StatType.Fun:
                    Fun = Math.Min(100, Fun + amount);
                    break;
            }
        }

        public void DecreaseStat(StatType stat, int amount)
        {
            switch (stat)
            {
                case StatType.Hunger:
                    Hunger = Math.Max(0, Hunger - amount);
                    break;
                case StatType.Sleep:
                    Sleep = Math.Max(0, Sleep - amount);
                    break;
                case StatType.Fun:
                    Fun = Math.Max(0, Fun - amount);
                    break;
            }
        }

        public string GetStatusEmoji()
        {
            if (Hunger < 30 || Sleep < 30 || Fun < 30)
                return "😢";
            if (Hunger < 50 || Sleep < 50 || Fun < 50)
                return "😐";
            return "😊";
        }
    }

    public static class ItemDatabase
    {
        public static List<Item> Items { get; } = new()
        {
            new Item("Mama 🍖", StatType.Hunger, 30),
            new Item("Uyku Yastığı 😴", StatType.Sleep, 40),
            new Item("Oyuncak Top 🎾", StatType.Fun, 25)
        };
    }

    public class PetManager
    {
        private List<Pet> pets = new();
        private readonly BackgroundTaskManager _backgroundTaskManager;

        public PetManager()
        {
            _backgroundTaskManager = new BackgroundTaskManager(pets);
            _ = _backgroundTaskManager.StartStatDecayTask();
        }

        public void AdoptPet()
        {
            Console.Write("Evcil hayvan adı: ");
            string? name = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(name)) name = "İsimsiz";

            pets.Add(new Pet(name));
            Console.WriteLine($"{name} başarıyla sahiplenildi! 🎉\n");
        }

        public void DisplayPets()
        {
            if (pets.Count == 0)
            {
                Console.WriteLine("Hiç evcil hayvanınız yok 😔");
                return;
            }

            Console.WriteLine("\n📋 Evcil Hayvanlar:");
            foreach (var pet in pets)
            {
                Console.WriteLine($"{pet.GetStatusEmoji()} {pet.Name} | Açlık: {pet.Hunger} | Uykululuk: {pet.Sleep} | Eğlence: {pet.Fun}");
            }
            Console.WriteLine();
        }

        public async Task UseItemAsync()
        {
            if (pets.Count == 0)
            {
                Console.WriteLine("Önce bir evcil hayvan sahiplenmelisiniz! 🐾");
                return;
            }

            Console.WriteLine("\n🎁 Kullanılabilir Eşyalar:");
            for (int i = 0; i < ItemDatabase.Items.Count; i++)
            {
                var item = ItemDatabase.Items[i];
                Console.WriteLine($"{i + 1}. {item.Name} (Etki: +{item.EffectAmount} {item.StatAffected})");
            }

            Console.Write("\nKullanmak istediğiniz eşya numarası: ");
            if (!int.TryParse(Console.ReadLine(), out int itemIndex) || itemIndex < 1 || itemIndex > ItemDatabase.Items.Count)
            {
                Console.WriteLine("Geçersiz eşya seçimi! ❌");
                return;
            }

            Console.WriteLine("\nHangi evcil hayvana kullanmak istersiniz?");
            for (int i = 0; i < pets.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {pets[i].Name}");
            }

            Console.Write("\nEvcil hayvan numarası: ");
            if (!int.TryParse(Console.ReadLine(), out int petIndex) || petIndex < 1 || petIndex > pets.Count)
            {
                Console.WriteLine("Geçersiz evcil hayvan seçimi! ❌");
                return;
            }

            var selectedItem = ItemDatabase.Items[itemIndex - 1];
            var selectedPet = pets[petIndex - 1];

            selectedPet.IncreaseStat(selectedItem.StatAffected, selectedItem.EffectAmount);
            Console.WriteLine($"\n{selectedItem.Name} başarıyla {selectedPet.Name} üzerinde kullanıldı! ✨");
            
            await Task.Delay(1000);
            Console.WriteLine($"Yeni durum: Açlık: {selectedPet.Hunger} | Uykululuk: {selectedPet.Sleep} | Eğlence: {selectedPet.Fun}");
        }

        public void Cleanup()
        {
            _backgroundTaskManager.StopTasks();
        }
    }
} 