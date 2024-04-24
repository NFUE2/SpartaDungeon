using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaDungeon
{
    public class Item
    {
        public enum Type
        {
            공격력,
            방어력,
        }

        public string name, value, info;
        private Type type;

        public void Display()
        {
            Console.Write($"{name} | ");
            Console.Write($"{type.ToString()} +{value} | ");
            Console.WriteLine($"{info}");
        }
    }
}
