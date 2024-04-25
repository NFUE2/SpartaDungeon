using System.Diagnostics;
using System.IO;

namespace SpartaDungeon
{
    internal class Program
    {
        private static List<Scene> SettingScenes()
        {
            List<Scene> scenes = new List<Scene>();

            //MainScene main = new MainScene();
            //StatusScene status = new StatusScene();
            //InventoryScene inventory = new InventoryScene();
            //ShopScene shop = new ShopScene();
            //DungeonScene dungeon = new DungeonScene();
            scenes.Add(new MainScene());
            scenes.Add(new StatusScene());
            scenes.Add(new InventoryScene());
            scenes.Add(new ShopScene());
            scenes.Add(new DungeonScene());
            scenes.Add(new Rest());

            return scenes;
        }

        static void FileSave()
        { 

        }

        static void Main(string[] args)
        {
            int index = 0;
            Character c = new Character();
            List<Scene> scenes = SettingScenes();

            while (true)
            {
                index = scenes[index].Display();
                Console.WriteLine("\n이동합니다.\n");
                Thread.Sleep(1000);
            }
        }
    }
}
