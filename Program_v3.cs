using System;
using System.Threading.Tasks;
using Managers;

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("🐾 Evcil Hayvan Simülatörü v3.0 🐾\n");
        Console.WriteLine("✨ Yeni Özellikler:");
        Console.WriteLine("- Otomatik durum güncellemesi");
        Console.WriteLine("- Emoji desteği");
        Console.WriteLine("- Gelişmiş kullanıcı arayüzü\n");

        var petManager = new PetManager();

        try
        {
            while (true)
            {
                Console.WriteLine("Ne yapmak istersiniz?");
                Console.WriteLine("1. 🐱 Evcil Hayvan Sahiplen");
                Console.WriteLine("2. 📋 Evcil Hayvanları Görüntüle");
                Console.WriteLine("3. 🎁 Eşya Kullan");
                Console.WriteLine("4. 👋 Çıkış");

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
                        await petManager.UseItemAsync();
                        break;
                    case "4":
                        Console.WriteLine("Güle güle! 👋");
                        petManager.Cleanup();
                        return;
                    default:
                        Console.WriteLine("Geçersiz seçim! ❌");
                        break;
                }
            }
        }
        finally
        {
            petManager.Cleanup();
        }
    }
} 