using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SpartaDungeon
{
    public class Storage
    {
        public List<Item> items { get; private set; }

        public Storage() 
        {
            items = new List<Item>();
        }


        /*
        레이어
        1000 8 인덱스번호 출력
        0100 4 캐릭터 출력
        0010 2 상점 출력
        0001 1 판매 출력
        */

        public void Display(int layer)
        {
            Console.WriteLine("[아이템 목록]");

            for (int i = 0; i < items.Count; i++)
            {
                Item item = items[i];
                string msg = $"- {item.name} | {item.type.ToString()} +{item.stat} | {item.info}";

                if ((layer >> 2 & 1) == 1 && Character.instance.IsEquip(item))
                    msg = msg.Insert(1, " [E]"); //캐릭터가 장착했는지

                if ((layer >> 3 & 1) == 1) msg = msg.Insert(1, $" {i + 1}"); //인덱스 번호가 필요한지

                //상점에서의 호출
                if ((layer & 1) == 1 ||((layer >> 1 & 1) == 1 && item.ea > 0))
                {
                    int value = item.value;
                    if ((layer & 1) == 1) value = (int)(value * 0.85f);
                    
                    msg += $" | {value} G";
                }
                else if ((layer >> 1 & 1) == 1 && item.ea == 0) msg += " | 구매완료";


                Console.WriteLine(msg);
            }
            Console.WriteLine();
        }

        public void Add(Item i)
        {
            items.Add(i);
        }

        public void Remove(Item i)
        {
            items.Remove(i);
        }
    }
    public enum ItemType
    {
        공격력,
        방어력,
    }

    public class Item
    {

        public string name,info;
        public int stat, ea, value;
        public ItemType type;

        public void ShowCaseDisplay()
        {
            Console.Write($"{name} | ");
            Console.Write($"{type.ToString()} +{stat} | ");
            Console.Write($"{info} | ");
            if(ea > 0) Console.WriteLine($"{value} G");
            else Console.WriteLine("구매완료");
        }
    }

    #region 아이템들

   

    public class NewBieArmor : Item
    {
        public NewBieArmor()
        {
            type = ItemType.방어력;
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
            type = ItemType.방어력;
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
            type = ItemType.방어력;
            name = "스파르타의 갑옷";
            info = "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.";
            stat = 15;
            value = 3500;
            ea = 1;
        }
    }
    public class Club : Item
    {
        public Club()
        {
            type = ItemType.공격력;
            name = "몽둥이";
            info = "나무 몽둥이 입니다.";
            stat = 1;
            value = 300;
            ea = 1;
        }
    }
    public class OldSword : Item
    {
        public OldSword()
        {
            type = ItemType.공격력;
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
            type = ItemType.공격력;
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
            type = ItemType.공격력;
            name = "스파르타의 창";
            info = "스파르타의 전사들이 사용했다는 전설의 창입니다.";
            stat = 7;
            value = 2500;
            ea = 1;
        }
    }



    #endregion
}
