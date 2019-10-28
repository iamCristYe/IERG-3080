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
using BabaIsYou.Model;
using BabaIsYou.Controller;

namespace BabaIsYou
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        LevelController CurrentLevel = new LevelController(1);
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
