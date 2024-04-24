using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SpartaDungeon
{
    public class Item
    {
        public enum Type
        {
            무기,
            갑옷,
        }

        public string name,info;
        public int stat, ea, value;
        public Type type;

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

    #region 아이템들

    public class NewBieArmor : Item
    {
        public NewBieArmor()
        {
            type = Type.갑옷;
            name = "수련자 갑옷";
            info = "수련에 도움을 주는 갑옷입니다.";
            stat = 5;
            value = 1000;
            ea = 1;
        }
    }

    public class IronArmor : Item
    {
        public IronArmor()
        {
            type = Type.갑옷;
            name = "무쇠갑옷";
            info = "무쇠로 만들어져 튼튼한 갑옷입니다.";
            stat = 9;
            value = 2000;
            ea = 1;
        }
    }

    public class SpartaArmor : Item
    {
        public SpartaArmor()
        {
            type = Type.갑옷;
            name = "스파르타의 갑옷";
            info = "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.";
            stat = 15;
            value = 3500;
            ea = 1;
        }
    }

    public class OldSword : Item
    {
        public OldSword()
        {
            type = Type.무기;
            name = "낡은 검";
            info = "쉽게 볼 수 있는 낡은 검 입니다.";
            stat = 2;
            value = 600;
            ea = 1;
        }
    }

    public class BronzeAxe : Item
    {
        public BronzeAxe()
        {
            type = Type.무기;
            name = "청동 도끼";
            info = "어디선가 사용됐던거 같은 도끼입니다. ";
            stat = 5;
            value = 1500;
            ea = 1;
        }
    }

    public class SpartaSpear : Item
    {
        public SpartaSpear()
        {
            type = Type.무기;
            name = "스파르타의 창";
            info = "스파르타의 전사들이 사용했다는 전설의 창입니다.";
            stat = 7;
            value = 2500;
            ea = 1;
        }
    }

    #endregion
}
