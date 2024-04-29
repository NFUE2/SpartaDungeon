using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace SpartaDungeon
{

    internal class Program
    {
        static string path = Directory.GetCurrentDirectory();

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
            Character c = new Character();

            string json = JsonConvert.SerializeObject(Character.instance.status);
            File.WriteAllText(path + "/status.txt", json);

            json = JsonConvert.SerializeObject(Character.instance.inventory);
            File.WriteAllText(path + "/inventory.txt", json);

            json = JsonConvert.SerializeObject(Character.instance.equip);
            File.WriteAllText(path + "/equip.txt", json);
        }
        static bool FileLoad()
        {
            if (!File.Exists(path + "/status.txt"))
                return false;

            string str = File.ReadAllText(path + "/equip.txt");

            List<Item> equip = JsonConvert.DeserializeObject<List<Item>>
               (str);

            str = File.ReadAllText(path + "/status.txt");
            Status status = JsonConvert.DeserializeObject<Status>
                (str);

            str = File.ReadAllText(path + "/inventory.txt");
            Storage inventory = JsonConvert.DeserializeObject<Storage>
               (str);

            for (int i = 0; i < equip.Count; i++)
                for(int j = 0; i < inventory.items.Count; i++)
                {
                    equip[i] = inventory.items[j];
                    break;
                }

            Character c = new Character(ref status,ref inventory,ref equip);

            return true;
        }

        static void Main(string[] args)
        {
            if (!FileLoad())
                FileSave();

            List<Scene> scenes = SettingScenes();
            int index = 0,sceneNum;

            while (true)
            {
                sceneNum = scenes[index].Display();
                if (sceneNum == 0 && index == 0)
                {
                    Console.WriteLine("게임을 종료합니다.");
                    break;
                }
                index = sceneNum;
                Console.WriteLine("\n이동합니다.\n");
                Thread.Sleep(1000);
            }

            FileSave();
            Environment.Exit(0);
        }


    }
}
