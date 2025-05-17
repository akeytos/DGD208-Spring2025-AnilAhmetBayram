using System;
using Managers;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("🐾 Evcil Hayvan Simülatörü v1.0 🐾\n");
        var petManager = new PetManager();

        while (true)
        {
            Console.WriteLine("Ne yapmak istersiniz?");
            Console.WriteLine("1. Evcil Hayvan Sahiplen");
            Console.WriteLine("2. Evcil Hayvanları Görüntüle");
            Console.WriteLine("3. Çıkış");

            string? choice = Console.ReadLine();
            Console.WriteLine();

            switch (choice)
            {
                case "1":
                    petManager.AdoptPet();
                    break;
                case "2":
                    petManager.DisplayPets();
                    break;
                case "3":
                    Console.WriteLine("Güle güle!");
                    return;
                default:
                    Console.WriteLine("Geçersiz seçim!");
                    break;
            }
        }
    }
} 