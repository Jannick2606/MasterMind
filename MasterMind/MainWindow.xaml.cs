using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace MasterMind
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private GameManager gm = new GameManager();
        private Button[,] rows;
        private Ellipse[] code;
        private bool colorSelected = false;
        private int activeRow;
        private int guessesAvailable = 5;
        private bool[] filled = new bool[4];
        private Brush[] currentColors = new Brush[4];
        private Button[,] pins;
        private int buttonsRevealed = 0;

        //I initialize the arrays when the game opens
        public MainWindow()
        {
            InitializeComponent();
            rows = new Button[5,4]
            {
                { Row1Button1, Row1Button2, Row1Button3, Row1Button4 },
                { Row2Button1, Row2Button2, Row2Button3, Row2Button4 },
                { Row3Button1, Row3Button2, Row3Button3, Row3Button4 },
                { Row4Button1, Row4Button2, Row4Button3, Row4Button4 },
                { Row5Button1, Row5Button2, Row5Button3, Row5Button4 } 
            };
            pins = new Button[5, 4]
            {
                { Row1Pin1, Row1Pin2, Row1Pin3, Row1Pin4 },
                { Row2Pin1, Row2Pin2, Row2Pin3, Row2Pin4 },
                { Row3Pin1, Row3Pin2, Row3Pin3, Row3Pin4 },
                { Row4Pin1, Row4Pin2, Row4Pin3, Row4Pin4 },
                { Row5Pin1, Row5Pin2, Row5Pin3, Row5Pin4 }
            };
            code = new Ellipse[4] { Code1, Code2, Code3, Code4 };
        }

        //This method gets called when you click on one of the colored circles
        private void ColorButton_Click(object sender, RoutedEventArgs e)
        {
            Button btn = e.Source as Button;
            gm.SetColor(btn.Background);
            colorSelected = true;
        }

        //This class gives the gray buttons color
        //If you haven't selected a color first it won't do anything
        private void EmptyButton_Click(object sender, RoutedEventArgs e)
        {
            if (colorSelected == true)
            {
                Button btn = e.Source as Button;
                btn.Background = gm.GetColor();
                    SetFilledTrue(btn);
            }
        }

        //Is used when the user submits his guess
        private void GuessButton_Click(object sender, RoutedEventArgs e)
        {
            if (filled[0] == true && filled[1] == true && filled[2] == true && filled[3] == true)
            {
                for (int i = 0; i < currentColors.Length; i++)
                {
                    currentColors[i] = rows[activeRow, i].Background;
                }
                gm.CheckCode(currentColors);
                PaintPins();
                RemoveClick();
                if (guessesAvailable - 2 >= activeRow && CheckIfPlayerWon() == false)
                {
                    activeRow++;
                    AddClick();
                    SetFilledFalse();
                }
                else
                {
                    PaintCode();
                    ShowEndScreen(CheckIfPlayerWon());
                }
            }
        }

        //Checks if the player has won
        private bool CheckIfPlayerWon()
        {
            if (gm.GetReds() == 4)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //Paints the pins
        private void PaintPins()
        {
            for (int i = 0; i < gm.GetReds(); i++)
            {
                pins[activeRow, i].Background = Brushes.Red;
            }
            for (int i = gm.GetReds(); i < gm.GetReds()+gm.GetWhites(); i++)
            {
                pins[activeRow, i].Background = Brushes.White;
            }
        }
        //Adds the click event
        private void AddClick()
        {
            for (int i = 0; i < 4; i++)
            {
                rows[activeRow, i].Click += EmptyButton_Click;
            }
        }
        //Removes the click event
        private void RemoveClick()
        {
            for (int i = 0; i < 4; i++)
            {
                rows[activeRow, i].Click -= EmptyButton_Click;
            }
        }
        private void SetFilledTrue(Button btn)
        {
            for (int i = 0; i < filled.Length; i++)
            {
                if (btn == rows[activeRow, i])
                {
                    filled[i] = true;
                }
            }
        }
        private void SetFilledFalse()
        {
            for (int i = 0; i < filled.Length; i++)
            {
                filled[i] = false;
            }
        }

        //This is attached to the start and the restart button
        //It will change all the variables that are changed during the game
        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            SetDefaultColors();
            gm.GenerateCode();
            StartBox.Visibility = Visibility.Hidden;
            StartButton.Visibility = Visibility.Hidden;
            EndBox.Visibility = Visibility.Hidden;
            RestartButton.Visibility = Visibility.Hidden;
            activeRow = 0;
            buttonsRevealed = 0;
            SetFilledFalse();
            AddClick();
        }
        private void ShowEndScreen(bool won)
        {
            if(won == true)
            {
                ShowWinScreen();
            }
            else
            {
                ShowDefeatScreen();
            }
            EndBox.Visibility = Visibility.Visible;
            RestartButton.Visibility = Visibility.Visible;
        }
        private void ShowWinScreen()
        {
            EndBox.Text = "Du gættede koden";
        }
        private void ShowDefeatScreen()
        {
            EndBox.Text = "Du gættede ikke koden";
        }
        private void PaintCode()
        {
            for (int i = buttonsRevealed; i < code.Length; i++)
            {
                code[i].Fill = gm.GetCode()[i];
            }
        }
        private void SetDefaultColors()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    rows[j, i].Background = Brushes.LightGray;
                    pins[j, i].Background = Brushes.Transparent;
                }
                code[i].Fill = Brushes.LightGray;
            }
        }
        //Reveals the first color in the code
        //Then it reveals the next one and the next one
        private void HintButton_Click(object sender, RoutedEventArgs e)
        {
            if (buttonsRevealed < 4)
            {
                code[buttonsRevealed].Fill = gm.GetCode()[buttonsRevealed];
                buttonsRevealed++;
            }
        }
    }
}
