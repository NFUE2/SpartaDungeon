namespace SpartaDungeon
{
    internal class Program
    {
        private static List<Scene> SettingScenes()
        {
            List<Scene> scenes = new List<Scene>();

            MainScene main = new MainScene();
            StatusScene status = new StatusScene();
            InventoryScene inventory = new InventoryScene();
            ShopScene shop = new ShopScene();

            scenes.Add(main);
            scenes.Add(status);
            scenes.Add(inventory);
            scenes.Add(shop);

            return scenes;
        }


        static void Main(string[] args)
        {
            int index = 0;
            Character c = new Character();
            List<Scene> scenes = SettingScenes();

            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");

            while (true)
            {
                index = scenes[index].Display();
                Console.WriteLine("\n이동합니다.\n");
                Thread.Sleep(1000);
                Console.Clear();
            }
        }
    }
}
