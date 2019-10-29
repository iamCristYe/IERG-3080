using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;

namespace BabaIsYou
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public class Block
    {
        public bool IsPush { get; protected set; }
        public bool IsStop { get; protected set; }
        public bool IsYou { get; protected set; }
        public bool IsWin { get; protected set; }
        public string imgsrc { get; protected set; }
        public Block()
        {
            IsWin = false;
            IsYou = false;
        }

        public class Text : Block
        {
            public Text()
            {
                IsPush = true;
                IsStop = true;
            }

            public class TextBaba : Text
            {
                public TextBaba()
                {
                    imgsrc = "/Resources/icons/BABA_TEXT.jpg";
                }
            }
            public class TextRock : Text
            {
                public TextRock()
                {
                    imgsrc = "/Resources/icons/ROCK_TEXT.jpg";
                }
            }
            public class TextFlag : Text
            {
                public TextFlag()
                {
                    imgsrc = "/Resources/icons/FLAG_TEXT.jpg";
                }
            }
            public class TextWall : Text
            {
                public TextWall()
                {
                    imgsrc = "/Resources/icons/WALL_TEXT.jpg";
                }
            }
            public class TextIs : Text
            {
                public TextIs()
                {
                    imgsrc = "/Resources/icons/IS_TEXT.jpg";
                }
            }
            public class TextYou : Text
            {
                public TextYou()
                {
                    imgsrc = "/Resources/icons/YOU_TEXT.jpg";
                }
            }
            public class TextWin : Text
            {
                public TextWin()
                {
                    imgsrc = "/Resources/icons/WIN_TEXT.jpg";
                }
            }
            public class TextPush : Text
            {
                public TextPush()
                {
                    imgsrc = "/Resources/icons/PUSH_TEXT.jpg";
                }
            }
            public class TextStop : Text
            {
                public TextStop()
                {
                    imgsrc = "/Resources/icons/STOP_TEXT.jpg";
                }
            }
        }

        public class Thing : Block
        {
            public Thing()
            {
                IsPush = false;
                IsStop = false;
            }

            public class Baba : Thing
            {
                public Baba()
                {
                    imgsrc = "/Resources/icons/BABA_THING.jpg";
                }
            }
            public class Rock : Thing
            {
                public Rock()
                {
                    imgsrc = "/Resources/icons/ROCK_THING.jpg";
                }
            }
            public class Flag : Thing
            {
                public Flag()
                {
                    imgsrc = "/Resources/icons/FLAG_THING.jpg";
                }
            }
            public class Wall : Thing
            {
                public Wall()
                {
                    imgsrc = "/Resources/icons/WALL_THING.jpg";
                }
            }
        }
    }


    public class Map
    {
        public Dictionary<(int, int), List<Block>> PointBlockPairs;     //For usage of tuple, refer to https://www.tutorialsteacher.com/csharp/valuetuple
    }

    public class Level
    {
        int LevelNumber;
        int MapHeight = 0;
        int MapWidth = 0;

        //We store data in History->Map->Block
        //Map is a Dictionary of Point and Blocks on that point, indicating all current Blocks on the map
        //History is a List of Map, allow us to go backward in time
        List<Map> History = new List<Map>();
        Map CurrentMap = new Map();

        public Level(int LevelNumber)
        {
            this.LevelNumber = LevelNumber;
        }

        public void loadGame()
        {
            //load blocks from data into CurrentMap
            //... 
            //MapHeight=
            //MapWidth=
            MapHeight = 18;
            MapWidth = 18;
            this.CurrentMap.PointBlockPairs.Add((5, 6), new List<Block> { new Block.Text.TextBaba() });
            this.CurrentMap.PointBlockPairs.Add((6, 6), new List<Block> { new Block.Text.TextIs() });
            this.CurrentMap.PointBlockPairs.Add((7, 6), new List<Block> { new Block.Text.TextYou() });
            this.CurrentMap.PointBlockPairs.Add((12, 6), new List<Block> { new Block.Text.TextFlag() });
            this.CurrentMap.PointBlockPairs.Add((13, 6), new List<Block> { new Block.Text.TextIs() });
            this.CurrentMap.PointBlockPairs.Add((14, 6), new List<Block> { new Block.Text.TextWin() });

            this.CurrentMap.PointBlockPairs.Add((9, 9), new List<Block> { new Block.Thing.Rock() });


            this.CurrentMap.PointBlockPairs.Add((6, 10), new List<Block> { new Block.Thing.Baba() });






            AddToHistory();
        }
        public bool CheckWin()
        {
            //...   
            return false;
        }
        public void MoveBlocks(string direction) { }
        public void UpdateRules()
        {
            //first reset the rules and then find the new rules and apply them
        }
        public void UpdateBlocks() { }
        public void AddToHistory()
        {
            History.Add(CurrentMap);
        }
        public void GoBack()
        {
            if (History.Count > 1)
            {
                History.RemoveAt(History.Count - 1);
                CurrentMap = History[History.Count - 1];
            }
        }
        public void Draw()
        {
            MessageBox.Show("Test");
        }


    }

    public partial class MainWindow : Window
    {
        Level CurrentLevel = new Level(1);
        const int SquareSize = 20;

        Image baba = new Image();
        BitmapImage bababitmap = new BitmapImage();

        //public MainWindow()
        //{
        //    InitializeComponent();
        //    CurrentLevel.loadGame();
        //    CurrentLevel.Draw();
        //}



        //private void Window_KeyDown(object sender, KeyEventArgs e)
        //{
        //    //for up/down/left/right
        //    CurrentLevel.MoveBlocks("up/down/left/right");//Move blocks accroding to keydown
        //    CurrentLevel.UpdateRules();//Update rules by finding sentences
        //    CurrentLevel.UpdateBlocks();//Update blocks, like sink/defeat/kill
        //    CurrentLevel.CheckWin();
        //    CurrentLevel.AddToHistory();
        //    CurrentLevel.Draw();

        //    //for z(redo)
        //    CurrentLevel.GoBack();
        //    CurrentLevel.Draw();

        //    //for r(restart)
        //    CurrentLevel.loadGame();
        //    CurrentLevel.Draw();
        //}

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            DrawGameArea();
        }

        private void DrawGameArea()
        {
            bool doneDrawingBackground = false;
            int nextX = 0, nextY = 0;
            int rowCounter = 0;
            bool nextIsOdd = false;

            // initialize baba
            baba.Width = SquareSize;
            baba.Height = SquareSize;

            bababitmap.BeginInit();
            bababitmap.UriSource = new Uri(@"/Resources/icons/BABA_THING.jpg", UriKind.RelativeOrAbsolute);
            bababitmap.EndInit();

            baba.Source = bababitmap;

            while (doneDrawingBackground == false)
            {
                Rectangle rect = new Rectangle
                {
                    Width = SquareSize,
                    Height = SquareSize,
                    // Fill = nextIsOdd ? Brushes.White : Brushes.Black
                    Fill = Brushes.Black
                };
                GameArea.Children.Add(rect);
                Canvas.SetTop(rect, nextY);
                Canvas.SetLeft(rect, nextX);

                Debug.WriteLine("x: {0}, y: {1}", nextX, nextY);

                nextIsOdd = !nextIsOdd;
                nextX += SquareSize;
                if (nextX >= GameArea.ActualWidth)
                {
                    nextX = 0;
                    nextY += SquareSize;
                    rowCounter++;
                    nextIsOdd = (rowCounter % 2 != 0);
                }

                if (nextY >= GameArea.ActualHeight)
                    doneDrawingBackground = true;
            }

            GameArea.Children.Add(baba);
            Canvas.SetTop(baba, 40);
            Canvas.SetLeft(baba, 160);
        }


    }
}
