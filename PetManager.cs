using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Managers
{
    public enum StatType
    {
        Hunger,
        Sleep,
        Fun
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
    }

    public static class ItemDatabase
    {
        public static List<Item> Items { get; } = new()
        {
            new Item("Mama", StatType.Hunger, 30),
            new Item("Uyku Yastığı", StatType.Sleep, 40),
            new Item("Oyuncak Top", StatType.Fun, 25)
        };
    }

    public class PetManager
    {
        private List<Pet> pets = new();

        public void AdoptPet()
        {
            Console.Write("Evcil hayvan adı: ");
            string? name = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(name)) name = "İsimsiz";

            pets.Add(new Pet(name));
            Console.WriteLine($"{name} başarıyla sahiplenildi!\n");
        }

        public void DisplayPets()
        {
            if (pets.Count == 0)
            {
                Console.WriteLine("Hiç evcil hayvanınız yok.");
                return;
            }

            Console.WriteLine("\nEvcil Hayvanlar:");
            foreach (var pet in pets)
            {
                Console.WriteLine($"{pet.Name} | Açlık: {pet.Hunger} | Uykululuk: {pet.Sleep} | Eğlence: {pet.Fun}");
            }
            Console.WriteLine();
        }

        public async Task UseItemAsync()
        {
            if (pets.Count == 0)
            {
                Console.WriteLine("Önce bir evcil hayvan sahiplenmelisiniz!");
                return;
            }

            Console.WriteLine("\nKullanılabilir Eşyalar:");
            for (int i = 0; i < ItemDatabase.Items.Count; i++)
            {
                var item = ItemDatabase.Items[i];
                Console.WriteLine($"{i + 1}. {item.Name} (Etki: +{item.EffectAmount} {item.StatAffected})");
            }

            Console.Write("\nKullanmak istediğiniz eşya numarası: ");
            if (!int.TryParse(Console.ReadLine(), out int itemIndex) || itemIndex < 1 || itemIndex > ItemDatabase.Items.Count)
            {
                Console.WriteLine("Geçersiz eşya seçimi!");
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
                Console.WriteLine("Geçersiz evcil hayvan seçimi!");
                return;
            }

            var selectedItem = ItemDatabase.Items[itemIndex - 1];
            var selectedPet = pets[petIndex - 1];

            selectedPet.IncreaseStat(selectedItem.StatAffected, selectedItem.EffectAmount);
            Console.WriteLine($"\n{selectedItem.Name} başarıyla {selectedPet.Name} üzerinde kullanıldı!");
            
            await Task.Delay(1000);
            Console.WriteLine($"Yeni durum: Açlık: {selectedPet.Hunger} | Uykululuk: {selectedPet.Sleep} | Eğlence: {selectedPet.Fun}");
        }
    }
} 