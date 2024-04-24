using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaDungeon
{
    public class Scene
    {
        protected List<string> actionList;
        protected bool escape = false;

        public virtual int Display()
        {
            if (actionList != null)
                for (int i = 0; i < actionList.Count; i++)
                    Console.WriteLine($"{i + 1}. {actionList[i]}");

            if (escape) Escape();

            return Input();
        }

        public void Escape()
        {
            Console.WriteLine("0. 나가기");
        }

        public int Input()
        {
            Console.WriteLine("\n원하시는 행동을 입력해주세요.");
            Console.Write(">>");
            int num = int.Parse(Console.ReadLine());

            return num;
        }
    }

    public class MainScene : Scene //메인 화면
    {
        public MainScene()
        {
            actionList = new List<string>() { "상태 보기", "인벤토리", "상점" };
            escape = false;
        }

        public override int Display()
        {
            Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.\n");

            return base.Display();
        }
    }

    public class StatusScene : Scene //상태 화면
    {
        public StatusScene()
        {
            escape = true;
        }
        public override int Display()
        {
            //캐릭터의 정보를 가져와야할듯. 싱글톤을 쓰는게 좋을것 같음
            Console.WriteLine("상태 보기");
            Console.WriteLine("캐릭터의 정보가 표시됩니다.\n");

            Status s = Character.instance.status;

            Console.WriteLine($"Lv. {s.level}");
            Console.WriteLine($"이름 : {s.name}");
            Console.WriteLine($"Chad : ( {s.major} )");
            Console.WriteLine($"공격력 : {s.att}");
            Console.WriteLine($"방어력 : {s.def}");
            Console.WriteLine($"체 력 : {s.hp}");
            Console.WriteLine($"Gold : {s.gold} G\n");

            Escape();

            return Input();
        }
    }

    public class InventoryScene : Scene //인벤토리 화면
    {
        public InventoryScene()
        {
            actionList = new List<string> { "장착관리" };
            escape = true;
        }

        public override int Display()
        {
            List<Item> items = Character.instance.inventory;

            Console.WriteLine("인벤토리");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");
            Console.WriteLine("[아이템 목록]");

            for (int i = 0; i < items.Count; i++)
            {
                Console.Write("- ");
                items[i].Display();
            }

            int index = base.Display();

            while(index == 1)
            {
                Console.Clear();
                Console.WriteLine("인벤토리 - 장착관리");
                Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");
                Console.WriteLine("[아이템 목록]");

                for (int i = 0; i < items.Count; i++)
                {
                    Console.Write($"- {i+1} ");
                    items[i].Display();

                }

                Escape();
                if (Input() == 0) break;
            }

            return 0;
        }
    }

    public class ShopScene : Scene //상점화면
    {
        List<Item> items;

        public static ShopScene instance;

        public ShopScene Instance
        {
            get
            {
                if(instance == null)
                    instance = new ShopScene();
                
                return instance;
            }
        } //싱글톤

        public ShopScene()
        {
            if (instance == null) instance = this;

            actionList = new List<string> { "아이템 구매" };

            items = new List<Item>()
            {
                new NewBieArmor(),
                new IronArmor(),
                new SpartaArmor(),
                new OldSword(),
                new BronzeAxe(),
                new SpartaSpear()
            };

            escape = true;
        }

        public override int Display()
        {
            Console.WriteLine("상점");
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.\n");

            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{Character.instance.status.gold} G\n");

            Console.WriteLine("[아이템 목록]");
            for (int i = 0; i < items.Count; i++)
            {
                Console.Write("- ");
                items[i].Display();
            }
            Console.WriteLine();

            int index = base.Display();

            while(index == 1)
            {
                Console.Clear();
                Console.WriteLine("상점 - 아이템 구매");
                Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.\n");

                Console.WriteLine("[보유 골드]");
                Console.WriteLine($"{Character.instance.status.gold} G\n");

                Console.WriteLine("[아이템 목록]");
                for (int i = 0; i < items.Count; i++)
                {
                    Console.Write($"- {i + 1} ");
                    items[i].ShowCaseDisplay();
                }

                Escape();
                int num = Input();

                if (num == 0) break;
                else if(1 <= num && num <= items.Count) Trade(items[num - 1]);
                else Console.WriteLine("잘못된 입력입니다.");
            }

            return 0;
        }

        private void Trade(Item i)
        {
            int gold = Character.instance.status.gold;
            int value = i.value;


            if (i.ea == 0)
                Console.WriteLine("이미 구매한 아이템입니다.");

            else if(gold > value)
            {
                gold -= value; 
                i.ea = 0;
                Character.instance.inventory.Add(i);
                Console.WriteLine($"{i.name} 구매를 완료했습니다.");
            }

            else if(gold < value)
                Console.WriteLine("Gold가 부족합니다.");

            Thread.Sleep(1000);
        }
    }
}
