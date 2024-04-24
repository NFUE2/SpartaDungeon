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

        public string name,info;
        public int stat, ea, value;
        private Type type;

        public void Display()
        {
            Console.Write("\n");
            if(Character.instance.IsEquip(this)) Console.Write("[E]");
            Console.Write($"{name} | ");
            Console.Write($"{type.ToString()} +{stat} | ");
            Console.Write($"{info}");
        }

        public void ShowCaseDisplay()
        {
            Console.Write($"{name} | ");
            Console.Write($"{type.ToString()} +{stat} | ");
            Console.Write($"{info} | ");
            if(ea > 0) Console.Write($"{value} G");
            else Console.Write("구매완료");
        }
    }
}
