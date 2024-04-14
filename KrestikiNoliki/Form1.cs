using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KrestikiNoliki
{
    public partial class Forma : System.Windows.Forms.Form
    {
        List<Button> buttons = new List<Button>();
        string KN;
        public Forma()
        {
            InitializeComponent();
            Init();
        }

        void RandomKN()
        {
            var p = new Random();
            KN = p.Next(0, 2) == 0 ? "X" : "O";
        }

        void ButtonsInit()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    buttons.Add(new Button
                    {
                        Size = new Size(200, 200),
                        Location = new Point(i * 200, j * 200),
                        TabStop = false,
                        Font = new Font("Timse New Roman", 80),
                    });
                    buttons[buttons.Count - 1].Click += ButtonClick;
                    Controls.Add(buttons[buttons.Count - 1]);
                }
            }
        }

        private void ButtonClick(object sender, EventArgs e)
        {
            var currentButton = sender as Button;
            currentButton.Text = KN;
            currentButton.Click -= ButtonClick;
            KN = KN == "X" ? "O" : "X";
            if (CheckForWinner("X"))
            {
                MessageBox.Show("Победили крестики!");
                ResetGame();
            }
            else if (CheckForWinner("O"))
            {
                MessageBox.Show("Победили нолики!");
                ResetGame();
            }
            else if (CheckForWinner("X") || CheckForWinner("O"))
            {
                MessageBox.Show("Ничья!");
                ResetGame();
            }
        }

        void Init()
        {
            ClientSize = new Size(600, 600);
            ButtonsInit();
            RandomKN();
        }

       
        private bool CheckForWinner(string symbol)
        {
            for (int i = 0; i < 3; i++)
            {
                if (buttons[i * 3].Text == symbol && buttons[i * 3 + 1].Text == symbol && buttons[i * 3 + 2].Text == symbol)
                    return true;
                if (buttons[i].Text == symbol && buttons[i + 3].Text == symbol && buttons[i + 6].Text == symbol)
                    return true;
            }
            if (buttons[0].Text == symbol && buttons[4].Text == symbol && buttons[8].Text == symbol)
                return true;
            if (buttons[2].Text == symbol && buttons[4].Text == symbol && buttons[6].Text == symbol)
                return true;

            return false;
        }

        private void ResetGame()
        {
            foreach (Button button in buttons)
            {
                button.Text = "";
                button.Click += ButtonClick;
            }
            RandomKN();
        }
    }
}
