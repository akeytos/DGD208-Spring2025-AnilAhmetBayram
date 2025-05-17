using System;
using System.Collections.Generic;

namespace Managers
{
    // Basit evcil hayvan sınıfı
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
    }
} 