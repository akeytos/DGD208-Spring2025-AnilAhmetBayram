using System.Collections.Generic;
using Enums;

namespace Items
{
    public static class ItemDatabase
    {
        public static List<Item> Items = new List<Item>
        {
            new Item("Kedi Maması", PetStat.Hunger, 20, 2),
            new Item("Köpek Oyuncağı", PetStat.Fun, 15, 3),
            new Item("Kuş Yatağı", PetStat.Sleep, 25, 4)
        };
    }
}
