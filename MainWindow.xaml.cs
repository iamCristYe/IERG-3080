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
        public int[] location { get; protected set; }
        public Block(int[] location)
        {
            IsWin = false;
            IsYou = false;
            this.location = new int[2];
            Array.Copy(this.location, location, 2);
        }

        public class Text : Block
        {
            public Text(int[] location) : base(location)
            {
                IsPush = true;
                IsStop = true;
            }

            public class TextBaba : Text
            {
                public TextBaba(int[] location) : base(location)
                {
                    imgsrc = "";
                }
            }
            public class TextRock : Text
            {
                public TextRock(int[] location) : base(location)
                {
                    imgsrc = "";
                }
            }
            public class TextFlag : Text
            {
                public TextFlag(int[] location) : base(location)
                {
                    imgsrc = "";
                }
            }
            public class TextWall : Text
            {
                public TextWall(int[] location) : base(location)
                {
                    imgsrc = "";
                }
            }
            public class TextIs : Text
            {
                public TextIs(int[] location) : base(location)
                {
                    imgsrc = "";
                }
            }
            public class TextYou : Text
            {
                public TextYou(int[] location) : base(location)
                {
                    imgsrc = "";
                }
            }
            public class TextWin : Text
            {
                public TextWin(int[] location) : base(location)
                {
                    imgsrc = "";
                }
            }
            public class TextPush : Text
            {
                public TextPush(int[] location) : base(location)
                {
                    imgsrc = "";
                }
            }
            public class TextStop : Text
            {
                public TextStop(int[] location) : base(location)
                {
                    imgsrc = "";
                }
            }
        }

        public class Thing : Block
        {
            public Thing(int[] location) : base(location)
            {
                IsPush = false;
                IsStop = false;
            }

            public class Baba : Thing
            {
                public Baba(int[] location) : base(location)
                {
                    imgsrc = "";
                }
            }
            public class Rock : Thing
            {
                public Rock(int[] location) : base(location)
                {
                    imgsrc = "";
                }
            }
            public class Flag : Thing
            {
                public Flag(int[] location) : base(location)
                {
                    imgsrc = "";
                }
            }
            public class Wall : Thing
            {
                public Wall(int[] location) : base(location)
                {
                    imgsrc = "";
                }
            }
        }
    }


    public class Level
    {
        List<List<Block>> History = new List<List<Block>>();
        List<Block> CurrentBlocks = new List<Block>();
        int LevelNumber;
        public Level(int LevelNumber)
        {
            this.LevelNumber = LevelNumber;
        }
        public void loadGame()
        {
            //load blocks from data into CurrentBlocks
            //... 

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
            History.Add(CurrentBlocks);
        }
        public void GoBack()
        {
            if (History.Count > 1)
            {
                History.RemoveAt(History.Count - 1);
                CurrentBlocks = History[History.Count - 1];
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
        public MainWindow()
        {
            InitializeComponent();
            CurrentLevel.loadGame();
            CurrentLevel.Draw();
        }
        
        

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            //for up/down/left/right
            CurrentLevel.MoveBlocks("up/down/left/right");//Move blocks accroding to keydown
            CurrentLevel.UpdateRules();//Update rules by finding sentences
            CurrentLevel.UpdateBlocks();//Update blocks, like sink/defeat/kill
            CurrentLevel.CheckWin();
            CurrentLevel.AddToHistory();
            CurrentLevel.Draw();

            //for z(redo)
            CurrentLevel.GoBack();
            CurrentLevel.Draw();

            //for r(restart)
            CurrentLevel.loadGame();
            CurrentLevel.Draw();
        }



    }
}
