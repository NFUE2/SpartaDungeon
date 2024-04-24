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

    public class MainScene : Scene
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

    public class StatusScene : Scene
    {
        public StatusScene()
        {
            escape = true;
        }
        public override int Display()
        {
            //캐릭터의 정보를 가져와야할듯. 싱글톤을 쓰는게 좋을것 같음
            Console.WriteLine("상태 보기");
            Console.WriteLine("캐릭터의 정보가 표시됩니다.");

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

    public class InventoryScene : Scene
    {
        public InventoryScene()
        {
            actionList = new List<string> { "장착관리" };
            escape = true;
        }

        public override int Display()
        {
            List<Item> list = Character.instance.inventory;
            Console.WriteLine("[아이템 목록]\n");

            foreach (Item i in list)
                i.Display();

            return base.Display();
        }
    }

    public class Shop : Scene
    {

    }
}
