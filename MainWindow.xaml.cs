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
            //InitializeComponent();
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {

            CurrentLevelController.loadGame();
            DrawGameArea(CurrentLevelController.CurrentLevel.CurrentMap);
        }
        private void DrawGameArea(Map map)
        {
            //GameArea.Children.Clear();


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
                    Canvas.SetTop(image, pair.Key.Item2 * SquareSize);
                    Canvas.SetLeft(image, pair.Key.Item1 * SquareSize);
                    // Debug.WriteLine("x: {0}, y: {1}", b.Start_X, b.Start_Y);
                }

            }

            // list of objects (blocks?) (instantiate all of them, each needs their own reference)
            // each will have their own cooridnate??

        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Up)
            {
                CurrentLevelController.ArrowKeyDown("up");
            }
            else if (e.Key == Key.Down)
            {
                CurrentLevelController.ArrowKeyDown("down");
            }
            else if (e.Key == Key.Left)
            {
                CurrentLevelController.ArrowKeyDown("left");
            }
            else if (e.Key == Key.Right)
            {
                CurrentLevelController.ArrowKeyDown("right");
            }
            else if (e.Key == Key.Z)
            {
                CurrentLevelController.GoBack();
            }
            else if (e.Key == Key.R)
            {
                CurrentLevelController.Restart();
            }
            DrawGameArea(CurrentLevelController.CurrentLevel.CurrentMap);
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
