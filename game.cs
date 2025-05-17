using System;
using System.Threading.Tasks;
using Managers;
using UI;

public class Game
{
    private PetManager petManager;
    private Menu mainMenu;

    public Game()
    {
        petManager = new PetManager();
        mainMenu = new Menu("🌟 Ana Menü 🌟", new[]
        {
            "1. Evcil Hayvan Sahiplen 🐶",
            "2. Evcil Hayvanları Görüntüle 📋",
            "3. Eşyaları Kullan 🎁",
            "4. Geliştirici Bilgileri 👤",
            "0. Oyunu Kapat ❌"
        });
    }

    public async Task RunAsync()
    {
        bool isRunning = true;
        while (isRunning)
        {
            mainMenu.Show();
            Console.Write("Seçiminiz: ");
            var input = Console.ReadLine();

            switch (input)
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
                    Console.WriteLine("\n👨‍💻 Tasarlayan: Your Name - YourStudentNumber");
                    break;
                case "0":
                    Console.WriteLine("Çıkış yapılıyor...");
                    isRunning = false;
                    break;
                default:
                    Console.WriteLine("Geçersiz seçim.");
                    break;
            }

            if (isRunning)
            {
                Console.WriteLine("\nDevam etmek için bir tuşa bas...");
                Console.ReadKey();
            }
        }

        // Cleanup when game exits
        petManager.Cleanup();
    }
}




