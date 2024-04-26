using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SpartaDungeon
{
    public class Status
    {
        public int level, hp, gold, def;
        public string major, name;
        public float att;
    }
    [Serializable]
    public class Character
    {

        public static Character instance = null;
        public Status status { get; private set; }
        public Storage inventory { get; set; }
        public List<Item> equip { get; set; }

        public Character Instance
        {
            
            get
            {
                if (instance == null)
                {
                    instance = new Character();
                }
                return instance;
            }
        } //싱글톤
        public Character()
        {
            if (instance == null) instance = this;
            inventory = new Storage();
            status = new Status();
            equip = new List<Item>();
            status.level = 1;
            status.name = "김마법사";
            status.major = "전사";
            status.att = 10;
            status.def = 5;
            status.hp = 100;
            status.gold = 1500;
        }

        public Character(ref Status status,ref Storage inventory, ref List<Item> equip)
        {
            if (instance == null) instance = this;

            this.status = status;
            this.inventory = inventory;
            this.equip = equip;
        }

        public bool IsEquip(Item i)
        {
            return equip.Contains(i);
        }

        public int Display()
        {
            Status s = status;

            int addAtt = 0;
            int addDef = 0;

            foreach(var i in equip)
            {
                if ((int)i.type == 0) addAtt += i.stat;
                else addDef += i.stat;
            }

            Console.WriteLine($"Lv. {s.level}");
            Console.WriteLine($"이름 : {s.name}");
            Console.WriteLine($"Chad : ( {s.major} )");

            Console.Write($"공격력 : {s.att}");
            if (addAtt > 0) Console.WriteLine($" (+{addAtt})");
            else Console.WriteLine();

            Console.Write($"방어력 : {s.def}");
            if (addDef > 0) Console.WriteLine($" (+{addDef})");
            else Console.WriteLine();

            Console.WriteLine($"체 력 : {s.hp}");
            Console.WriteLine($"Gold : {s.gold} G\n");

            return 0;
        }
    }
}
