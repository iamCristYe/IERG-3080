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
        private const int SquareSize = 20;
        private bool IsNormalMode = true;
        LevelController CurrentLevelController = new LevelController(0);
        public MainWindow()
        {
            // this.Background = Brushes.Black;
            //InitializeComponent();
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            // TODO: choose the levels first
            MessageBoxResult option = MessageBox.Show("Do you want to enable the Textbox mode?", "Game Mode Selection", MessageBoxButton.YesNo);
            if (option == MessageBoxResult.No)
            {
                this.Background = Brushes.Black;
            }
            else
            {
                this.Background = Brushes.White;
                IsNormalMode = false;
                GameArea.Height = 500;
            }

            CurrentLevelController.LoadGame();
            DrawGameArea(CurrentLevelController.CurrentLevel.CurrentMap);
        }
        private void DrawGameArea(Map map)
        {
            GameArea.Children.Clear();

            if (IsNormalMode)
            {
                Dictionary<(int, int), List<Model.Block>> dict = map.PointBlockPairs;
                foreach (KeyValuePair<(int, int), List<Model.Block>> pair in dict)
                {
                    // for each block in the list
                    for (int i = pair.Value.Count() - 1; i >= 0; i--)
                    //foreach (Model.Block block in pair.Value)
                    {
                        Image image = GetNewImage(pair.Value[i].imgsrc);
                        GameArea.Children.Add(image);
                        Canvas.SetTop(image, pair.Key.Item2 * SquareSize);
                        Canvas.SetLeft(image, pair.Key.Item1 * SquareSize);
                    }
                }
            }
            else //if (IsNormalMode == false)
            {
                string TextBoxString = "";
                for (int Row = 0; Row < CurrentLevelController.CurrentLevel.MapHeight; Row++)
                {
                    for (int Column = 0; Column < CurrentLevelController.CurrentLevel.MapWidth; Column++)
                    {
                        if (CurrentLevelController.CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)].Count > 0)
                        {
                            TextBoxString += CurrentLevelController.CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)][0].text;
                        }
                        else
                        {
                            TextBoxString += "    ";
                        }
                        TextBoxString += "|";
                    }
                    TextBoxString += "\n";
                    for (int Column = 0; Column < CurrentLevelController.CurrentLevel.MapWidth; Column++)
                    {
                        if (CurrentLevelController.CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)].Count > 1)
                        {
                            TextBoxString += CurrentLevelController.CurrentLevel.CurrentMap.PointBlockPairs[(Column, Row)][1].text;
                        }
                        else
                        {
                            TextBoxString += "    ";
                        }
                        TextBoxString += "|";
                    }
                    TextBoxString += "\n";
                    TextBoxString += "----------------------------------------------------------------------------------------------------\n";
                }
                TextBox textBox = GetLargeTextBox(TextBoxString);
                GameArea.Children.Add(textBox);
                Canvas.SetTop(textBox, 0);
                Canvas.SetLeft(textBox, 0);

                AddControlsToScreen();
            }
        }

        private Button GetNewButton(string content, Thickness margins)
        {
            Button button = new Button
            {
                Content = content,
                Background = Brushes.Gray,
                Margin = margins,
                Height = 20,
                Width = 40,
                FontSize = 10
            };

            return button;
        }

        // add event handlers, up down left right, back
        private void AddControlsToScreen()
        {
            Button leftButton = GetNewButton("<", new Thickness(40, 420, 0, 0));
            leftButton.Click += LeftButton_Click;
            GameArea.Children.Add(leftButton);

            Button rightButton = GetNewButton(">", new Thickness(90, 420, 0, 0));
            rightButton.Click += RightButton_Click;
            GameArea.Children.Add(rightButton);

            Button upButton = GetNewButton("^", new Thickness(140, 420, 0, 0));
            upButton.Click += UpButton_Click;
            GameArea.Children.Add(upButton);

            Button downButton = GetNewButton("v", new Thickness(190, 420, 0, 0));
            downButton.Click += DownButton_Click;
            GameArea.Children.Add(downButton);

            Button undoButton = GetNewButton("Undo", new Thickness(240, 420, 0, 0));
            undoButton.Click += UndoButton_Click;
            GameArea.Children.Add(undoButton);
        }
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape || e.Key == Key.Q)
            {
                this.Close();
            }
            if (IsNormalMode)
            {
                CurrentLevelController.KeyDown(e.Key);
                DrawGameArea(CurrentLevelController.CurrentLevel.CurrentMap);
            }
        }

        private TextBox GetTextBox(string text)
        {
            TextBox textBox = new TextBox();
            textBox.Width = SquareSize;
            textBox.Height = SquareSize;
            textBox.IsEnabled = false;
            textBox.FontSize = 6;
            textBox.TextWrapping = TextWrapping.WrapWithOverflow;
            textBox.Text = text;
            textBox.Foreground = Brushes.Black;
            textBox.BorderThickness = new Thickness(0, 0, 0, 0);

            return textBox;
        }

        private TextBox GetLargeTextBox(string text)
        {
            TextBox textBox = new TextBox
            {
                FontSize = 6,
                FontFamily = new FontFamily("Consolas"),
                Text = text,
                Foreground = Brushes.Black,
                BorderThickness = new Thickness(0, 0, 0, 0)
            };

            return textBox;
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

        private void LeftButton_Click(object sender, RoutedEventArgs e)
        {
            CurrentLevelController.KeyDown(Key.Left);
            DrawGameArea(CurrentLevelController.CurrentLevel.CurrentMap);

            // CurrentLevelController.HandleDirectionButtonClicked("LEFT");
        }

        private void RightButton_Click(object sender, RoutedEventArgs e)
        {
            CurrentLevelController.KeyDown(Key.Right);
            DrawGameArea(CurrentLevelController.CurrentLevel.CurrentMap);

            // CurrentLevelController.HandleDirectionButtonClicked("RIGHT");
        }

        private void UpButton_Click(object sender, RoutedEventArgs e)
        {
            CurrentLevelController.KeyDown(Key.Up);
            DrawGameArea(CurrentLevelController.CurrentLevel.CurrentMap);

            // CurrentLevelController.HandleDirectionButtonClicked("UP");
        }

        private void DownButton_Click(object sender, RoutedEventArgs e)
        {
            CurrentLevelController.KeyDown(Key.Down);
            DrawGameArea(CurrentLevelController.CurrentLevel.CurrentMap);

            // CurrentLevelController.HandleDirectionButtonClicked("DOWN");
        }

        private void UndoButton_Click(object sender, RoutedEventArgs e)
        {
            CurrentLevelController.KeyDown(Key.Z);
            DrawGameArea(CurrentLevelController.CurrentLevel.CurrentMap);

            // CurrentLevelController.HandleDirectionButtonClicked("UNDO");
        }
    }
}
