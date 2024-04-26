using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace SpartaDungeon
{
    public abstract class Scene 
    {
        #region 주석

        //protected int count = 0;
        //protected bool action = true;
        //protected bool escape = false;

        //private delegate void Handler();
        //private delegate int Handler2(bool action = true);

        //protected abstract void Top();
        //protected abstract void Mid();
        //protected virtual void Top2() { }
        //protected virtual void Mid2() { }
        //protected virtual int Bot2(bool action = true) { return 0; }


        //protected virtual int Bot(bool action = true)
        //{
        //    int num;
        //    while(true)
        //    {
        //        if (actionList != null && action)
        //            for (int i = 0; i < actionList.Count; i++)
        //                Console.WriteLine($"{i + 1}. {actionList[i]}");

        //        if(escape) Console.WriteLine("0. 나가기");

        //        Console.WriteLine("\n원하시는 행동을 입력해주세요.");
        //        Console.Write(">>");

        //        try
        //        {
        //            num = int.Parse(Console.ReadLine());
        //        }
        //        catch(FormatException e)
        //        {
        //            return -1;
        //        }

        //        if (!escape && num == 0) return -1;
        //        if (num < 0 || num > count) return -1;
        //        break;
        //    }

        //    return num;
        //}

        //public int Display() 
        //{
        //    if (actionList != null) count += actionList.Count;
        //    Handler handler = new Handler(Top);
        //    handler += Mid;
        //    Handler2 handler2 = new Handler2(Bot);
        //    while (true)
        //    {
        //        Console.Clear();
        //        //Top();
        //        //Mid();

        //        handler();
        //        int index = handler2();

        //        if(index == 10)
        //        {
        //            handler = Top2;
        //            handler += Mid2;
        //            handler2 = Bot2;
        //            continue;
        //        }

        //        if (index == -1)
        //        {
        //            Console.WriteLine("\n잘못된 입력입니다.");
        //            Thread.Sleep(1000);
        //            continue;
        //        }

        //        return index;
        //    }
        //}

        //public virtual void SceneInfo(params string[] str)
        //{
        //    if (str.Length > 0)
        //        foreach (string s in str)
        //            Console.WriteLine(s);
        //}

        //public virtual void Action() { }

        //public virtual int Display()
        //{
        //    int num = -1;

        //    while(num == -1)
        //    {
        //        if (actionList != null)
        //            for (int i = 0; i < actionList.Count; i++)
        //                Console.WriteLine($"{i + 1}. {actionList[i]}");

        //        num = Input(actionList.Count);
        //    }

        //    return num;
        //}

        //public int Input(int cnt = 0)
        //{
        //    if (escape) Console.WriteLine("0. 나가기");

        //    Console.WriteLine("\n원하시는 행동을 입력해주세요.");
        //    Console.Write(">>");

        //    int num = int.Parse(Console.ReadLine());

        //    if ((!escape && num == 0) || num < 0 || num > cnt)
        //    {
        //        Console.WriteLine("\n잘못된 입력입니다.");
        //        Thread.Sleep(1000);
        //        Console.Clear();
        //        return -1;
        //    }

        //    return num;
        //}
        #endregion
        protected int index,count;
        //protected bool escape = false;
        protected string msg;
        protected List<string> actionList;
        public abstract int Display();

        protected void PrintAction()
        {
            if(actionList != null)
                for(int i = 0; i < actionList.Count; i++)
                    Console.WriteLine($"{i + 1}. { actionList[i]}");
            Console.WriteLine("0. 나가기\n");
        }

        protected int Input()
        {
            //if (escape) Console.WriteLine("0. 나가기\n");
            Console.WriteLine("\n원하시는 행동을 입력해주세요.");
            Console.Write(">>");

            try
            {
                index = int.Parse(Console.ReadLine());
            }
            catch(FormatException e)
            {
                index =  -1;
            }

            //if (!escape && index == 0) index = -1;
            if (index < 0 || index > count) index = -1;

            if(index == -1)
            {
                Console.WriteLine("\n잘못된 입력입니다");
                Thread.Sleep(1000);
            }

            return index;
        }
    }

    public class MainScene : Scene //메인 화면
    {
        public MainScene()
        {
            msg = "스파르타 마을에 오신 여러분 환영합니다.\n이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.\n";
            actionList = new List<string>() { "상태 보기", "인벤토리", "상점", "던전입장","휴식하기" };
            count = actionList.Count;
        }

        public override int Display()
        {

            while (true)
            {
                Console.Clear();
                Console.WriteLine(msg);

                PrintAction();
                index = Input();

                if (index == -1) continue;
                return index;
            }
        }
    }

    public class StatusScene : Scene //상태 화면
    {
        public StatusScene()
        {
            msg = "상태 보기\n캐릭터의 정보가 표시됩니다.\n";
            //escape = true;
        }

        public override int Display()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine(msg);

                Character.instance.Display();
                index = Input();

                if (index == -1) continue;
                return index;
            }
        }
    }

    public class InventoryScene : Scene //인벤토리 화면
    {
        Storage inventory;
        string msg2;
        public InventoryScene()
        {
            msg = "인벤토리\n보유 중인 아이템을 관리할 수 있습니다.\n";
            msg2 = "인벤토리 - 장착 관리\n보유 중인 아이템을 관리할 수 있습니다.\n";

            actionList = new List<string>() { "장착 관리"};
            inventory = Character.instance.inventory;
            count = actionList.Count;
            //escape = true;
        }
        public override int Display()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine(msg);

                inventory.Display(4);
                PrintAction();
                index = Input();

                switch (index)
                {
                    case -1:
                        break;
                    case 0: 
                        return 0;
                    case 1:
                        Equip();
                        break;
                }
            }
        }

        private void Equip()
        {
            count = inventory.items.Count;

            while (true)
            {
                Console.Clear();
                Console.WriteLine(msg2);

                inventory.Display(12);
                index = Input();

                switch (index)
                {
                    case -1:
                        break;
                    case 0:
                        return;
                    default:
                        Item i = inventory.items[index - 1];
                        if(Character.instance.IsEquip(i))
                        {
                            Character.instance.equip.Remove(i);
                            if ((int)i.type == 0) Character.instance.status.att -= i.stat;
                            else Character.instance.status.def -= i.stat;
                            Console.WriteLine($"{i.name} 장착해제");
                        }
                        else
                        {
                            foreach(Item item in Character.instance.equip)
                            {
                                if(i.type == item.type)
                                {
                                    Character.instance.equip.Remove(item);
                                    if ((int)i.type == 0) Character.instance.status.att -= item.stat;
                                    else Character.instance.status.def -= item.stat;
                                    break;
                                }
                            }

                            Character.instance.equip.Add(i);
                            if ((int)i.type == 0) Character.instance.status.att += i.stat;
                            else Character.instance.status.def += i.stat;
                            Console.WriteLine($"{i.name} 장착");
                        }
                        Thread.Sleep(1000);
                        break;
                }
            }
        }
    }

    public class ShopScene : Scene //상점화면
    {
        Storage storage;
        string bmsg, smsg;
        public ShopScene()
        {
            storage = new Storage();

            storage.items.Add(new Club());
            storage.items.Add(new NewBieArmor());
            storage.items.Add(new IronArmor());
            storage.items.Add(new SpartaArmor());
            storage.items.Add(new OldSword());
            storage.items.Add(new BronzeAxe());
            storage.items.Add(new SpartaSpear());

            actionList = new List<string> { "아이템 구매", "아이템 판매" };

            //escape = true;

            msg = "상점\n필요한 아이템을 얻을 수 있는 상점입니다.\n";
            bmsg = "상점 - 아이템 구매\n필요한 아이템을 얻을 수 있는 상점입니다.\n";
            smsg = "상점 - 아이템 판매\n필요한 아이템을 얻을 수 있는 상점입니다.\n";
            
        }

        private void PrintGold()
        {
            ref int gold = ref Character.instance.status.gold;
            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{gold} G\n");
        }

        public override int Display()
        {
            count = actionList.Count;
            while (true)
            {
                Console.Clear();
                Console.WriteLine(msg);

                PrintGold();
                storage.Display(0);
                PrintAction();
                index = Input();

                switch (index)
                {
                    case -1:
                        break;
                    case 0:
                        return 0;
                    case 1:
                        Buy();
                        break;
                    case 2:
                        Sell();
                        break;
                }
            }
        }

        private void Buy()
        {
            count = storage.items.Count;

            while (true)
            {
                Console.Clear();
                Console.WriteLine(bmsg);

                PrintGold();
                storage.Display(10);
                index = Input();

                switch (index)
                {
                    case -1: break;
                    case 0: return;
                    default:
                        Item i = storage.items[index - 1];
                        ref int gold = ref Character.instance.status.gold;

                        if (i.ea == 0) Console.WriteLine("이미 구매한 아이템입니다");
                        else if(gold >= i.value)
                        {
                            Character.instance.inventory.Add(i);
                            gold -= i.value;
                            i.ea = 0;
                            Console.WriteLine("구매를 완료했습니다.");
                        }
                        else if(gold < i.value)
                        {
                            Console.WriteLine("Gold 가 부족합니다. ");
                        }

                        Thread.Sleep(1000);

                        break;
                }

                if (index == -1) continue;

            }
        }
        private void Sell()
        {
            count = Character.instance.inventory.items.Count;

            while (true)
            {
                Console.Clear();
                Console.WriteLine(bmsg);

                Character.instance.inventory.Display(13);
                index = Input();

                if (index == -1) continue;
                return;
            }
        }
    }

    public class DungeonScene : Scene
    {
        List<int> level;
        List<int> reward;
        public DungeonScene()
        {
            level = new List<int>() { 5, 11, 7 };
            reward = new List<int>() { 1000,1700,2500};
            //escape = true;
            actionList = new List<string> 
            { $"쉬운 던전     | 방어력 {level[0]} 이상 권장", 
              $"일반 던전     | 방어력 {level[1]} 이상 권장", 
              $"어려운 던전    | 방어력 {level[2]} 이상 권장"
            };

            msg = "던전입장\n이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.\n";
            count = actionList.Count; 
        }
        public override int Display()
        {
            while(true)
            {
                Console.Clear();
                Console.WriteLine(msg);

                PrintAction();
                index = Input();

                switch (index)
                {
                    case -1:
                        break;
                    case 0:
                        return 0;
                    default:
                        Result(index - 1);
                        return 0;
                }
            }
        }

        public void Result(int l)
        {
            Console.Clear();

            int p;
            bool clear= true;
            Random random = new Random();
            Status status = Character.instance.status;

            if (status.def < level[l])
            {
                p = random.Next(0, 100);

                if (p < 40) clear = false;

            }

            if (clear)
            {
                Console.WriteLine("던전 클리어");
                Console.WriteLine("축하합니다!!");
                Console.WriteLine("던전을 클리어 하였습니다.!!");

                Console.WriteLine("[탐험 결과]");

                int min = level[l] - status.def;

                int damage = p = random.Next(20 + min, 36 + min);
                float addReward = random.Next((int)status.att, (int)(status.att * 2)) / 100;

                int gold = reward[l] + (int)(reward[l] * addReward);

                Console.WriteLine($"체력 {status.hp} -> {status.hp - damage}");
                Console.WriteLine($"Gold {status.gold} -> {status.gold + gold}");

                status.level++;
                status.def += 1;
                status.att += 0.5f;
                status.gold += gold;
                status.hp -= damage;
            }
            else
            {
                Console.WriteLine("던전 실패");
                Console.WriteLine("[탐험 결과]");
                Console.WriteLine($"{status.hp} -> {status.hp / 2}");

                status.hp /= 2;
            }
            Thread.Sleep(1000);
        }
    }

    public class Rest : Scene
    {
        public Rest()
        {
            actionList = new List<string>() { "휴식하기"};
            count = actionList.Count;
            //escape = true;
        }

        public override int Display()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"휴식하기\n500 G 를 내면 체력을 회복할 수 있습니다. (보유 골드 : {Character.instance.status.gold} G)");

                PrintAction();
                index = Input();

                switch (index)
                {
                    case -1: break;
                    case 0: return 0;
                    default:
                        RestAction();
                        break;
                }
            }
        }

        private void RestAction()
        {
            ref int gold = ref Character.instance.status.gold;

            if(gold >= 500)
            {
                gold -= 500;
                Character.instance.status.hp = 100;
                Console.WriteLine("휴식을 완료했습니다.");

            }
            else
                Console.WriteLine("Gold 가 부족합니다.");

            Thread.Sleep(1000);
        }
    }
}
