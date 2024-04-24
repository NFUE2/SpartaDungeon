using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaDungeon
{
    public class Status
    {
        public int level, hp, gold, att, def;
        public string major, name;
    }

    public class Character
    {
        public static Character instance = null;
        public Status status { get; private set; }
        public List<Item> inventory { get; set; }
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
            inventory = new List<Item>();
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

        public bool IsEquip(Item i)
        {
            return equip.Contains(i);
        }
    }
}
