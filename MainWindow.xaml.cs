﻿using System;
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
        const int SquareSize = 20;
        LevelController CurrentLevelController = new LevelController(1);
        public MainWindow()
        {
            InitializeComponent();
            //CurrentLevelController.loadGame();
            //CurrentLevelController.Draw();
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            CurrentLevelController.CurrentLevel.CurrentMap = GetLevelOneObjects();



            DrawGameArea(CurrentLevelController.CurrentLevel.CurrentMap);
        }
        private void DrawGameArea(Map map)
        {
            GameArea.Children.Clear();


            Dictionary<Tuple<int, int>, List<Model.Block>> dict = map.PointBlockPairs;


            bool doneDrawingBackground = false;
            int nextX = 0, nextY = 0;
            int rowCounter = 0;
            bool nextIsOdd = false;

            while (doneDrawingBackground == false)
            {
                Rectangle rect = new Rectangle
                {
                    Width = SquareSize,
                    Height = SquareSize,
                    Fill = Brushes.Black
                };
                GameArea.Children.Add(rect);
                Canvas.SetTop(rect, nextY);
                Canvas.SetLeft(rect, nextX);

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


            // foreach (Block b in blocks)
            foreach (KeyValuePair<Tuple<int, int>, List<Model.Block>> pair in dict)
            {
                // for each block in the list
                foreach (Model.Block block in pair.Value)
                {
                    Image image = GetNewImage(block.imgsrc);
                    GameArea.Children.Add(image);
                    Canvas.SetTop(image, pair.Key.Item2*SquareSize);
                    Canvas.SetLeft(image, pair.Key.Item1*SquareSize);
                    // Debug.WriteLine("x: {0}, y: {1}", b.Start_X, b.Start_Y);
                }

            }

            // list of objects (blocks?) (instantiate all of them, each needs their own reference)
            // each will have their own cooridnate??

        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            //for up/down/left/right
            CurrentLevelController.MoveBlocks("up/down/left/right");//Move blocks accroding to keydown
            //CurrentLevelController.UpdateRules();//Update rules by finding sentences
            //CurrentLevelController.UpdateBlocks();//Update blocks, like sink/defeat/kill
            //CurrentLevelController.CheckWin();
            //CurrentLevelController.AddToHistory();
            //CurrentLevelController.Draw();
            
            DrawGameArea(CurrentLevelController.CurrentLevel.CurrentMap);


            //for z(redo)
            //CurrentLevel.GoBack();
            //CurrentLevel.Draw();

            //for r(restart)
            //CurrentLevel.loadGame();
            //CurrentLevel.Draw();
        }

        private void SafeAddDictionary(Dictionary<Tuple<int, int>, List<Model.Block>> dict, Tuple<int, int> tuple, List<Model.Block> list)
        {
            if (dict.ContainsKey(tuple))
            {
                List<Model.Block> new_list = dict[tuple];
                foreach (Model.Block block in list)
                {
                    new_list.Add(block);
                }
            }
            else
            {
                dict[tuple] = list;
            }
        }

        private Map GetLevelOneObjects()
        {
            Map map = new Map();
            Dictionary<Tuple<int, int>, List<Model.Block>> dict = map.PointBlockPairs;

            // vertical columns of grass (left most and right most column)
            for (int i = 0; i < 20; i += 1)
            {
                SafeAddDictionary(dict, Tuple.Create(0, i), new List<Model.Block> { new Model.Block.Thing.Grass() });
                SafeAddDictionary(dict, Tuple.Create(19, i), new List<Model.Block> { new Model.Block.Thing.Grass() });
            }

            // horizontal row of grass (top most and bottom most row)
            for (int i = 0; i < 20; i += 1)
            {
                SafeAddDictionary(dict, Tuple.Create(i, 0), new List<Model.Block> { new Model.Block.Thing.Grass() });
                SafeAddDictionary(dict, Tuple.Create(i, 19), new List<Model.Block> { new Model.Block.Thing.Grass() });
            }

            // wall text
            SafeAddDictionary(dict, Tuple.Create(4, 3), new List<Model.Block> { new Model.Block.ThingText.TextWall() });

            // is text
            SafeAddDictionary(dict, Tuple.Create(5, 3), new List<Model.Block> { new Model.Block.SpecialText.TextIs() });

            // stop text
            SafeAddDictionary(dict, Tuple.Create(6, 3), new List<Model.Block> { new Model.Block.SpecialText.TextStop() });

            // wall thing (a long series of wall)
            for (int i = 4; i < 15; i += 1)
            {
                SafeAddDictionary(dict, Tuple.Create(i, 5), new List<Model.Block> { new Model.Block.Thing.Wall() });
                SafeAddDictionary(dict, Tuple.Create(i, 9), new List<Model.Block> { new Model.Block.Thing.Wall() });
            }

            // fake wall thing
            for (int i = 4; i < 15; i += 1)
            {
                SafeAddDictionary(dict, Tuple.Create(i, 6), new List<Model.Block> { new Model.Block.Thing.FakeWall() });
                if (i != 6 && i != 13)
                {
                    SafeAddDictionary(dict, Tuple.Create(i, 7), new List<Model.Block> { new Model.Block.Thing.FakeWall() });
                }
                SafeAddDictionary(dict, Tuple.Create(i, 8), new List<Model.Block> { new Model.Block.Thing.FakeWall() });
            }

            // baba thing
            SafeAddDictionary(dict, Tuple.Create(6, 7), new List<Model.Block> { new Model.Block.Thing.Baba() });

            // flag thing
            SafeAddDictionary(dict, Tuple.Create(13, 7), new List<Model.Block> { new Model.Block.Thing.Flag() });

            // rock thing
            for (int i = 6; i < 9; i += 1)
            {
                SafeAddDictionary(dict, Tuple.Create(9, i), new List<Model.Block> { new Model.Block.Thing.Rock() });
            }

            // baba text
            SafeAddDictionary(dict, Tuple.Create(5, 11), new List<Model.Block> { new Model.Block.ThingText.TextBaba() });

            // is text
            SafeAddDictionary(dict, Tuple.Create(6, 11), new List<Model.Block> { new Model.Block.SpecialText.TextIs() });

            // you text
            SafeAddDictionary(dict, Tuple.Create(7, 11), new List<Model.Block> { new Model.Block.SpecialText.TextYou() });

            // flag text
            SafeAddDictionary(dict, Tuple.Create(12, 11), new List<Model.Block> { new Model.Block.ThingText.TextFlag() });

            // is text
            SafeAddDictionary(dict, Tuple.Create(13, 11), new List<Model.Block> { new Model.Block.SpecialText.TextIs() });

            // win text
            SafeAddDictionary(dict, Tuple.Create(14, 11), new List<Model.Block> { new Model.Block.SpecialText.TextWin() });

            return map;
        }

        private Image GetNewImage(string uri)
        {
            Image image = new Image();
            image.Width = SquareSize;
            image.Height = SquareSize;

            BitmapImage bitmapImage = GetBitmapImage(uri);
            image.Source = bitmapImage;

            return image;
        }

        private BitmapImage GetBitmapImage(string uri)
        {
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.UriSource = new Uri(@uri, UriKind.RelativeOrAbsolute);
            bitmapImage.EndInit();

            return bitmapImage;
        }


    }
}
