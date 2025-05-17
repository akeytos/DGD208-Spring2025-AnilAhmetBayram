namespace UI
{
    public class Menu
    {
        private string title;
        private string[] options;

        public Menu(string title, string[] options)
        {
            this.title = title;
            this.options = options;
        }

        public void Show()
        {
            Console.Clear();
            Console.WriteLine(title);
            Console.WriteLine(new string('-', title.Length));

            foreach (var option in options)
            {
                Console.WriteLine(option);
            }

            Console.WriteLine();
        }
    }
}


