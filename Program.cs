using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        Game game = new Game();
        await game.RunAsync();
    }
}


