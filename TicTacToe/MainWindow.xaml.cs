using System;
using System.Drawing;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace TicTacToe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private int playerTurn;
        private string playerOne;
        private string playerTwo;

        //Represents the current game board state
        //0 represents empty, 1 for player one and 2 for player two
        private int[,] gameBoard = new int[3, 3];
        
        private void coinToss()
        {
            Random rand = new Random();
            int turn = rand.Next(0, 2);
            if(turn == 0)
            {
                playerOne = "X";
                playerTwo = "O";
                playerTurn = 1;
                System.Windows.MessageBox.Show("Heads:\n Player one wins\n coin toss");
                Text(207.0, 430.0, "Player 1's\n Turn", System.Windows.Media.Colors.Black);
            }
            if (turn == 1)
            {
                playerTwo = "X";
                playerOne = "O";
                playerTurn = 2;
                System.Windows.MessageBox.Show("Tails:\n Player two wins\n coin toss");
                Text(207.0, 430.0, "Player 2's \n Turn", System.Windows.Media.Colors.Black);
            }
        }

        private void DrawX(int x1, int x2, int y1, int y2)
        {
            Line left = new Line();
            Line right = new Line();
            Line leftArrowHead = new Line();
            Line rightArrowHead = new Line();
            //Draws the left side of the X
            left.Stroke = System.Windows.Media.Brushes.Black;
            left.X1 = x1;    left.Y1 = y1;
            left.X2 = x2;   left.Y2 = y2;
            left.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            left.VerticalAlignment = VerticalAlignment.Center;
            left.StrokeThickness = 5;
            pieces.Children.Add(left);
            //Draws the right side of the X
            right.Stroke = System.Windows.Media.Brushes.Black;
            right.X1 = x2; right.Y1 = y1;
            right.X2 = x1; right.Y2 = y2;
            right.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
            right.VerticalAlignment = VerticalAlignment.Center;
            right.StrokeThickness = 5;
            pieces.Children.Add(right);
            //Draws the Right side of the arrow head
            rightArrowHead.Stroke = System.Windows.Media.Brushes.Black;
            rightArrowHead.X1 = x1 + 75; rightArrowHead.Y1 = y1 + 100;
            rightArrowHead.X2 = x2; rightArrowHead.Y2 = y2;
            rightArrowHead.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            rightArrowHead.VerticalAlignment = VerticalAlignment.Center;
            rightArrowHead.StrokeThickness = 3;
            pieces.Children.Add(rightArrowHead);
            //Draws the left side of the arrow head
            leftArrowHead.Stroke = System.Windows.Media.Brushes.Black;
            leftArrowHead.X1 = x1 + 97; leftArrowHead.Y1 = y1 + 81;
            leftArrowHead.X2 = x2; leftArrowHead.Y2 = y2;
            leftArrowHead.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            leftArrowHead.VerticalAlignment = VerticalAlignment.Center;
            leftArrowHead.StrokeThickness = 3;
            pieces.Children.Add(leftArrowHead);
        }

        private void DrawCircle(double centerX, double centerY)
        {
            Ellipse outerCircle = new Ellipse();
            double leftOuterCircle = circlePosition(centerX, 115);
            double topOuterCircle = circlePosition(centerY, 115); 
            outerCircle.Stroke = System.Windows.Media.Brushes.DarkRed;
            outerCircle.Width = 115;
            outerCircle.Height = 115;
            outerCircle.Margin = new Thickness(leftOuterCircle, topOuterCircle, 0, 0);
            outerCircle.StrokeThickness = 7;
            pieces.Children.Add(outerCircle);

            Ellipse secondCircle = new Ellipse();
            double leftSecondCircle = circlePosition(centerX, 75);
            double topSecondCircle = circlePosition(centerY, 75);
            secondCircle.Stroke = System.Windows.Media.Brushes.DarkRed;
            secondCircle.Width = 75;
            secondCircle.Height = 75;
            secondCircle.Margin = new Thickness(leftSecondCircle, topSecondCircle, 0, 0);
            secondCircle.StrokeThickness = 7;
            pieces.Children.Add(secondCircle);

            Ellipse bulls = new Ellipse();
            double leftBulls = circlePosition(centerX, 35);
            double topBulls = circlePosition(centerY, 35);
            bulls.Stroke = System.Windows.Media.Brushes.DarkRed;
            bulls.Width = 35;
            bulls.Height = 35;
            bulls.Margin = new Thickness(leftBulls, topBulls, 0, 0);
            bulls.StrokeThickness = 7;
            pieces.Children.Add(bulls);
        }
        private double circlePosition(double centerPoint, int size)
        {
            return centerPoint - (size / 2);
        }
        private void Text(double x ,double y, String text, System.Windows.Media.Color color)
        {
            TextBlock textBlock = new TextBlock();
            textBlock.Text = text;
            textBlock.Foreground = new SolidColorBrush(color);
            textBlock.FontSize = 32;
            textBlock.FontFamily = new System.Windows.Media.FontFamily("Avenir Next");
            textBlock.Name = "playerTurn";
            Canvas.SetLeft(textBlock, x);
            Canvas.SetTop(textBlock, y);
            pieces.Children.Add(textBlock);
        }

        private void RemoveTextByName()
        {
            var text = (UIElement)LogicalTreeHelper.FindLogicalNode(pieces, "playerTurn");
            pieces.Children.Remove(text);
        }
        private bool CheckWinner()
        {
            int i;
            for (i = 0; i < 3; i++)
            {
                if (gameBoard[i, 0] != 0 && gameBoard[i, 0] == gameBoard[i, 1] && gameBoard[i, 0] == gameBoard[i, 2])
                {
                    System.Windows.MessageBox.Show("Player " + playerTurn + " wins");
                    return true;
                }
            }

            for (i = 0; i < 3; i++)
            {
                if (gameBoard[0, i] != 0 && gameBoard[0, i] == gameBoard[1, i] && gameBoard[0, i] == gameBoard[2, i])
                {
                    System.Windows.MessageBox.Show("Player " + playerTurn + " wins");
                    return true;
                }   
            }

            if (gameBoard[0, 0] != 0 && gameBoard[0, 0] == gameBoard[1, 1] && gameBoard[1, 1] == gameBoard[2,2])
            {
                System.Windows.MessageBox.Show("Player " + playerTurn + " wins");
                return true;
            }

            if (gameBoard[0, 2] != 0 && gameBoard[0, 2] == gameBoard[1, 1] && gameBoard[1, 1] == gameBoard[2, 0])
            {
                System.Windows.MessageBox.Show("Player " + playerTurn + " wins");
                return true;
            }

            if (CheckDraw())
            {
                System.Windows.MessageBox.Show("Draw");
                return true;
            }

            else
            {
                return false;
            } 
        }

        private bool CheckDraw()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (gameBoard[i, j] == 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        private void topLeftBox_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        { 
            if (gameBoard[0, 0] != 0)
            {
                System.Windows.MessageBox.Show("The square has been filled.");
                return;
            }
            gameBoard[0, 0] = playerTurn;
            if (playerTurn == 1)
            {
                int x1 = 75, x2 = 175,
                y1 = 45, y2 = 150;
                DrawX(x1, x2, y1, y2);
                if (CheckWinner())
                {
                    ResetButton_Click(sender, e);
                }
                else
                {
                    playerTurn = 2;
                    RemoveTextByName();
                    Text(207.0, 430.0, "Player 2's\n Turn", System.Windows.Media.Colors.Black);
                    return;
                }
            }
            if(playerTurn == 2)
            {
                double centerX = 126, centerY = 95;
                DrawCircle(centerX, centerY);
                if (CheckWinner())
                {
                    ResetButton_Click(sender, e);
                }
                else
                {
                    playerTurn = 1;
                    RemoveTextByName();
                    Text(207.0, 430.0, "Player 1's\n Turn", System.Windows.Media.Colors.Black);
                    return;
                }
            }
        }

        private void topCenterBox_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (gameBoard[0, 1] != 0)
            {
                System.Windows.MessageBox.Show("The square has been filled.");
                return;
            }
            gameBoard[0, 1] = playerTurn;
            if (playerTurn == 1)
            {
                int x1 = 207, x2 = 307,
                y1 = 45, y2 = 150;
                DrawX(x1, x2, y1, y2);
                if (CheckWinner())
                {
                    ResetButton_Click(sender, e);
                }
                else
                {
                    playerTurn = 2;
                    RemoveTextByName();
                    Text(207.0, 430.0, "Player 2's\n Turn", System.Windows.Media.Colors.Black);
                    return;
                }
            }
            if (playerTurn == 2)
            {
                double centerX = 257, centerY = 95;
                DrawCircle(centerX, centerY);
                if (CheckWinner())
                {
                    ResetButton_Click(sender, e);
                }
                else
                {
                    playerTurn = 1;
                    RemoveTextByName();
                    Text(207.0, 430.0, "Player 1's\n Turn", System.Windows.Media.Colors.Black);
                    return;
                }

            }
        }

        private void topRightBox_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (gameBoard[0, 2] != 0)
            {
                System.Windows.MessageBox.Show("The square has been filled.");
                return;
            }
            gameBoard[0, 2] = playerTurn;
            if (playerTurn == 1)
            {
                int x1 = 340, x2 = 440,
                    y1 = 45,  y2 = 150;
                DrawX(x1, x2, y1, y2);
                if (CheckWinner())
                {
                    ResetButton_Click(sender, e);
                }
                else
                {
                    playerTurn = 2;
                    RemoveTextByName();
                    Text(207.0, 430.0, "Player 2's\n Turn", System.Windows.Media.Colors.Black);
                    return;
                }
            }
            if (playerTurn == 2)
            {
                double centerX = 387, centerY = 95;
                DrawCircle(centerX, centerY);
                if (CheckWinner())
                {
                    ResetButton_Click(sender, e);
                }
                else
                {
                    playerTurn = 1;
                    RemoveTextByName();
                    Text(207.0, 430.0, "Player 1's\n Turn", System.Windows.Media.Colors.Black);
                    return;
                }
            }
        }

        private void centerLeftBox_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (gameBoard[1, 0] != 0)
            {
                System.Windows.MessageBox.Show("The square has been filled.");
                return;
            }
            gameBoard[1, 0] = playerTurn;
            if (playerTurn == 1)
            {
                int x1 = 75, x2 = 175,
                y1 = 173, y2 = 278;
                DrawX(x1, x2, y1, y2);
                if (CheckWinner())
                {
                    ResetButton_Click(sender, e);
                }
                else
                {
                    playerTurn = 2;
                    RemoveTextByName();
                    Text(207.0, 430.0, "Player 2's\n Turn", System.Windows.Media.Colors.Black);
                    return;
                }
            }
            if (playerTurn == 2)
            {
                double centerX = 126, centerY = 225.5;
                DrawCircle(centerX, centerY);
                if (CheckWinner())
                {
                    ResetButton_Click(sender, e);
                }
                else
                {
                    playerTurn = 1;
                    RemoveTextByName();
                    Text(207.0, 430.0, "Player 1's\n Turn", System.Windows.Media.Colors.Black);
                    return;
                }
            }
        }

        private void centerBox_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (gameBoard[1, 1] != 0)
            {
                System.Windows.MessageBox.Show("The square has been filled.");
                return;
            }
            gameBoard[1, 1] = playerTurn;
            if (playerTurn == 1)
            {
                int x1 = 207, x2 = 307,
                y1 = 173, y2 = 278;
                DrawX(x1, x2, y1, y2);
                if (CheckWinner())
                {
                    ResetButton_Click(sender, e);
                }
                else
                {
                    playerTurn = 2;
                    RemoveTextByName();
                    Text(207.0, 430.0, "Player 2's\n Turn", System.Windows.Media.Colors.Black);
                    return;
                }
            }
            if (playerTurn == 2)
            {
                double centerX = 257, centerY = 225.5;
                DrawCircle(centerX, centerY);
                if (CheckWinner())
                {
                    ResetButton_Click(sender, e);
                }
                else
                {
                    playerTurn = 1;
                    RemoveTextByName();
                    Text(207.0, 430.0, "Player 1's\n Turn", System.Windows.Media.Colors.Black);
                    return;
                }
            }
        }

        private void centeRightrBox_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (gameBoard[1, 2] != 0)
            {
                System.Windows.MessageBox.Show("The square has been filled.");
                return;
            }
            gameBoard[1, 2] = playerTurn;
            if (playerTurn == 1)
            {
                int x1 = 340, x2 = 440,
                y1 = 173, y2 = 278;
                DrawX(x1, x2, y1, y2);
                if (CheckWinner())
                {
                    ResetButton_Click(sender, e);
                }
                else
                {
                    playerTurn = 2;
                    RemoveTextByName();
                    Text(207.0, 430.0, "Player 2's\n Turn", System.Windows.Media.Colors.Black);
                    return;
                }
            }
            if (playerTurn == 2)
            {
                double centerX = 387, centerY = 225.5;
                DrawCircle(centerX, centerY);
                if (CheckWinner())
                {
                    ResetButton_Click(sender, e);
                }
                else
                {
                    playerTurn = 1;
                    RemoveTextByName();
                    Text(207.0, 430.0, "Player 1's\n Turn", System.Windows.Media.Colors.Black);
                    return;
                }
            }
        }

        private void bottomLeftBox_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (gameBoard[2, 0] != 0)
            {
                System.Windows.MessageBox.Show("The square has been filled.");
                return;
            }
            gameBoard[2, 0] = playerTurn;
            if (playerTurn == 1)
            {
                int x1 = 75, x2 = 175,
                y1 = 305, y2 = 410;
                DrawX(x1, x2, y1, y2);
                if (CheckWinner())
                {
                    ResetButton_Click(sender, e);
                }
                else
                {
                    playerTurn = 2;
                    RemoveTextByName();
                    Text(207.0, 430.0, "Player 2's\n Turn", System.Windows.Media.Colors.Black);
                    return;
                }
            }
            if (playerTurn == 2)
            {
                double centerX = 126, centerY = 357.5;
                DrawCircle(centerX, centerY);
                if (CheckWinner())
                {
                    ResetButton_Click(sender, e);
                }
                else
                {
                    playerTurn = 1;
                    RemoveTextByName();
                    Text(207.0, 430.0, "Player 1's\n Turn", System.Windows.Media.Colors.Black);
                    return;
                }
            }
        }

        private void bottomCenterBox_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (gameBoard[2, 1] != 0)
            {
                System.Windows.MessageBox.Show("The square has been filled.");
                return;
            }
            gameBoard[2, 1] = playerTurn;
            if (playerTurn == 1)
            {
                int x1 = 207, x2 = 307,
                y1 = 305, y2 = 410;
                DrawX(x1, x2, y1, y2);
                if (CheckWinner())
                {
                    ResetButton_Click(sender, e);
                }
                else
                {
                    playerTurn = 2;
                    RemoveTextByName();
                    Text(207.0, 430.0, "Player 2's\n Turn", System.Windows.Media.Colors.Black);
                    return;
                }
            }
            if (playerTurn == 2)
            {
                double centerX = 257, centerY = 357.5;
                DrawCircle(centerX, centerY);
                if (CheckWinner())
                {
                    ResetButton_Click(sender, e);
                }
                else
                {
                    playerTurn = 1;
                    RemoveTextByName();
                    Text(207.0, 430.0, "Player 1's\n Turn", System.Windows.Media.Colors.Black);
                    return;
                }
            }
        }

        private void bottomRightBox_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (gameBoard[2, 2] != 0)
            {
                System.Windows.MessageBox.Show("The square has been filled.");
                return;
            }
            gameBoard[2, 2] = playerTurn;
            if (playerTurn == 1)
            {
                int x1 = 340, x2 = 440,
                y1 = 305, y2 = 410;
                DrawX(x1, x2, y1, y2);
                if (CheckWinner())
                {
                    ResetButton_Click(sender, e);
                }
                else
                {
                    playerTurn = 2;
                    RemoveTextByName();
                    Text(207.0, 430.0, "Player 2's\n Turn", System.Windows.Media.Colors.Black);
                    return;
                }
            }
            if (playerTurn == 2)
            {
                double centerX = 387, centerY = 357.5;
                DrawCircle(centerX, centerY);
                if (CheckWinner())
                {
                    ResetButton_Click(sender, e);
                }
                else
                {
                    playerTurn = 1;
                    RemoveTextByName();
                    Text(207.0, 430.0, "Player 1's\n Turn", System.Windows.Media.Colors.Black);
                    return;
                }
            }
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            pieces.Children.Clear();
            int i, j;
            for (i = 0; i < 3; i++)
            {
                for (j = 0; j < 3; j++)
                {
                    gameBoard[i, j] = 0;
                }
            }
            board.IsEnabled = false;   
            PlayMenuItem.IsEnabled = true;
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void PlayMenuItem_Click(object sender, RoutedEventArgs e)
        {
            board.IsEnabled = true;
            coinToss();
            PlayMenuItem.IsEnabled = false;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.MessageBox.Show("The object is to get three X's or O's in a row\n" + 
                                            "Players alternate turns until there is a winner\n" +
                                            "The game ends in draw if there are no spots left to fill");
        }
    }
}