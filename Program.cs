using System.Drawing;
using System.Runtime.InteropServices;

namespace ConsoleGameProject
{
    internal class Program
    {
        public struct GameData
        {
            public bool gamePlaying;
            public bool[,] map;
            public ConsoleKey inputKey;
            public Point playerPoint;
            public Point item1Point;
            public Point item2Point;
            public Point altarPoint;
            public bool getItem1;
            public bool getItem2;
            public bool OpenInven;
        }
        public struct Point
        {
            public int x; public int y; 
        }
        static GameData data;
        static void Main(string[] args)
        {
            /*캐릭터 이동 시키기 사전 기획? 예상?
                미로에서 길을 찾아 아이템 두개를 획득하고 제물대에 간다.
            
                2차원 배열을 통해 2차원적인 필드를 만든다.
                
                필드 표현은 빈 공간은 여백으로 표현, 캐릭터는 빨간색의 @로 표현하여 캐릭터의 위치를 나타낸다.
                캐릭터를 이동할 때마다 필드를 새로 그려야한다.
                캐릭터를 이동하는 방식은 wasd 혹은 화살표로 한다.
                게임을 종료하고 싶다면 0을 입력하여 종료한다.

                조건문은 아마 캐릭터를 이동가능/불가능을 판가름 할때 사용할 예정

                반복문은 캐릭터를 계속 이동시키기 위해 사용할 예정 (while문)

                코드 제어를 위해 함수를 최대한 제작하여 사용할 예정
            */
            GameStartScreen();
            while (data.gamePlaying)
            {
                Render();
                Input();
                Update();
            }
            GameEndScreen();
        }
        static void GameStartScreen()
        {
            Console.Clear();
            Console.CursorVisible = false;
            Console.WriteLine("\n\n\t\t==============================================================\n\n\n");
            Console.WriteLine("\t\t\t\t공물을 모아 제물을 바쳐라\n\n\n");
            Console.WriteLine("\t\t==============================================================");
            Console.WriteLine("\n\n\t\t<조작법>\n\t\t이동:화살표키 or W(위) A(오른쪽) S(아래) D(왼쪽)");
            Console.WriteLine("\t\t인벤토리 열기:I");
            Console.WriteLine("\t\t게임 강제 종료:0");
            Console.ReadKey();
            data = new GameData();
            data.gamePlaying = true;
            data.OpenInven=false;
            data.map = new bool[,]
            {
                { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false },
                { false, true, true, true, true, true, true, true, false, true, true, true, true, true, true, false },
                { false, false, false, false, true, false, false, false, false, false, false, false, false, false, true, false },
                { false, false, false, false, true, false, true, true, true, true, true, true, true, true, true, false },
                { false, true, true, true, true, false, true, false, false, false, false, false, false, false, true, false },
                { false, true, false, false, false, false, true, false, false, false, false, false, false, false, true, false },
                { false, true, false, false, false, false, true, false, true, false, true, true, true, false, true, false },
                { false, true, true, true, true, true, true, false, true, false, true, false, true, false, true, false },
                { false, true, false, false, false, false, true, false, true, false, true, false, true, false, true, false },
                { false, true, false, true, false, false, true, false, true, false, true, false, false, false, true, false },
                { false, true, true, true, false, false, true, true, true, false, true, true, true, true, true, false, },
                { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false }
            };
            data.playerPoint=new Point() { x=1,y=1 };
            data.item1Point = new Point() { x = 3, y = 9 };
            data.item2Point = new Point() {x=12,y=8 };
            data.altarPoint=new Point() { x=8,y=6 };
            data.getItem1 = false;
            data.getItem2 = false;

        }
        static void GameEndScreen()
        {
            Console.Clear();
            Console.WriteLine("\n\n\t\t==============================================================\n\n\n");
            Console.WriteLine("\t\t\t\t\t 게임 종료\n\n\n");
            Console.WriteLine("\t\t==============================================================\n\n\n\n");
        }
        static void InputKey() 
        {
            if (!data.OpenInven)
            {
                switch (data.inputKey)
                {
                    case ConsoleKey.W:
                    case ConsoleKey.UpArrow:
                        MoveUp();
                        break;
                    case ConsoleKey.A:
                    case ConsoleKey.LeftArrow:
                        MoveLeft();
                        break;
                    case ConsoleKey.S:
                    case ConsoleKey.DownArrow:
                        MoveDown();
                        break;
                    case ConsoleKey.D:
                    case ConsoleKey.RightArrow:
                        MoveRight();
                        break;
                    case ConsoleKey.I:
                        data.OpenInven = true;
                        break;
                    case ConsoleKey.D0:
                        data.gamePlaying = false;
                        break;

                }
            }
            else
            {
                switch (data.inputKey)
                {
                    case ConsoleKey.I:
                    case ConsoleKey.Backspace:
                    case ConsoleKey.Spacebar:
                    case ConsoleKey.Escape:
                        data.OpenInven=false;
                        break;
                }
            }
        } 
        static void MoveUp()
        {
            Point nextPoint = new Point() { x = data.playerPoint.x, y = data.playerPoint.y - 1 };
            if (data.map[nextPoint.y, nextPoint.x])
            {
                data.playerPoint = nextPoint;
            }
        }
        static void MoveLeft()
        {
            Point nextPoint = new Point() { x = data.playerPoint.x-1, y = data.playerPoint.y };
            if (data.map[nextPoint.y, nextPoint.x])
            {
                data.playerPoint = nextPoint;
            }
        }
        static void MoveRight()
        {
            Point nextPoint = new Point() { x = data.playerPoint.x+1, y = data.playerPoint.y };
            if (data.map[nextPoint.y, nextPoint.x])
            {
                data.playerPoint = nextPoint;
            }
        }
        static void MoveDown()
        {
            Point nextPoint = new Point() { x = data.playerPoint.x, y = data.playerPoint.y + 1 };
            if (data.map[nextPoint.y, nextPoint.x])
            {
                data.playerPoint = nextPoint;
            }
        }
        static void GetItem1()
        {
            if (data.playerPoint.x==data.item1Point.x && data.playerPoint.y==data.item1Point.y && !data.getItem1)
            {
                data.getItem1 = true;
            }
        }
        static void GetItem2()
        {
            if (data.playerPoint.x == data.item2Point.x && data.playerPoint.y == data.item2Point.y && !data.getItem2)
            {
                data.getItem2 = true;
            }
        }
        static void GameClear()
        {
            if (data.playerPoint.x == data.altarPoint.x && data.playerPoint.y == data.altarPoint.y && data.getItem1 && data.getItem2) 
            {
                data.gamePlaying = false;
            }
        }
        static void OpenInventory()
        {
            Console.WriteLine("\n\n\t\t==============================================================\n\n");
            if (data.getItem1)
            {
                Console.WriteLine("\t\t\t역병의 공물을 소유 중입니다.");
            }
            if (data.getItem2)
            {
                Console.WriteLine("\t\t\t그림자의 공물을 소유 중입니다.");
            }
            if (!data.getItem1 && !data.getItem2)
            {
                Console.WriteLine("\t\t소유 중인 아이템이 없습니다.");
            }
            Console.WriteLine("\n\n\t\t==============================================================\n\n\n");
            Console.WriteLine("\t\t인벤토리 닫기:I / ESC / Space바 / BackSpace");
        }
        static void ExitInventory()
        {

        }
        static void Render()
        {
            Console.Clear();
            if (!data.OpenInven)
            {
                PrintMap();
                if (!data.getItem1)
                {
                    PrintItem1();
                }
                if (!data.getItem2)
                {
                    PrintItem2();
                }
                PrintAltar();
                PrintPlayer();
            }
            else
            {
                OpenInventory();
            }
            
        }
        static void PrintMap()
        {
            for (int y = 0; y < data.map.GetLength(0); y++) 
            { 
                for (int x = 0; x < data.map.GetLength(1); x++)
                {
                    if (data.map[y, x])
                    {
                        Console.Write(" ");
                    }
                    else
                    {
                        Console.Write("@");
                    }
                }
                Console.WriteLine();
            }
        }
        static void PrintPlayer()
        {
            Console.SetCursorPosition(data.playerPoint.x, data.playerPoint.y);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("P");
            Console.ResetColor();
        }
        static void PrintItem1()
        {
            Console.SetCursorPosition(data.item1Point.x, data.item1Point.y);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("I");
            Console.ResetColor();
        }
        static void PrintItem2()
        {
            Console.SetCursorPosition(data.item2Point.x, data.item2Point.y);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("I");
            Console.ResetColor();
        }
        static void PrintAltar()
        {
            Console.SetCursorPosition(data.altarPoint.x, data.altarPoint.y);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("#");
            Console.ResetColor();
        }
        static void Update()
        {
            InputKey();
            GetItem1();
            GetItem2();
            GameClear();
        }
        static void Input()
        {
            data.inputKey=Console.ReadKey(true).Key;
        }
    }
}
