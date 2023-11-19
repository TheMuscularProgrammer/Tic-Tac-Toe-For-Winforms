using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace B23_Ex05_Ori_307837096_Roy_316060151
{
    using System;
    using System.Windows.Forms;

    public partial class FormLogin : Form
    {
        private ReverseTicTacToeGame m_ReverseTicTocToeGame;
        private FormGameBoard m_FormGameBoard;

        public FormLogin()
        {
            InitializeComponent();
            checkBoxPlayer2.Click += CheckBoxPlayer2_Click;
            buttonStart.Click += ButtonStart_Click;
            numericUpDownRows.KeyUp += numericUpDownRowsOrCols_Click;
            numericUpDownRows.KeyDown += numericUpDownRowsOrCols_Click;
            numericUpDownCols.KeyUp += numericUpDownRowsOrCols_Click;
            numericUpDownCols.KeyDown += numericUpDownRowsOrCols_Click;
            numericUpDownRows.Click += numericUpDownRowsOrCols_Click;
            numericUpDownCols.Click += numericUpDownRowsOrCols_Click;
        }

        private void ButtonStart_Click(object sender, EventArgs e)
        {
            if (isPlayerNameValid(textBoxPlayer1.Text) == false)
            {
                MessageBox.Show(@"First player name is illegal! It must have 1-15 letters without spaces.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (checkBoxPlayer2.Checked == true && playerNumber2IsNotAComputerValidationCheck() == false)
            {
                MessageBox.Show(@"Second player name is illegal! If you play against another person, he must has a name. It must have 1-15 letters without spaces.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                bool isComputerPlaying = textBoxPlayer2.Enabled == true ? false : true;
                string player2Name;

                if (isComputerPlaying == true)
                {
                    player2Name = "Computer";
                }
                else
                {
                    player2Name = textBoxPlayer2.Text;
                }

                m_ReverseTicTocToeGame = new ReverseTicTacToeGame(isComputerPlaying, textBoxPlayer1.Text, player2Name, (byte)numericUpDownRows.Value);

                m_FormGameBoard = new FormGameBoard(m_ReverseTicTocToeGame.BoardGame.BoardSize, m_ReverseTicTocToeGame);
            }

            DialogResult = DialogResult.OK;
            this.Close();
        }
        
        private void numericUpDownRowsOrCols_Click(object sender, EventArgs e)
        {
            byte currentValueOfBoardSize = (byte)(sender as NumericUpDown).Value;

            numericUpDownRows.Value = (byte)currentValueOfBoardSize;
            numericUpDownCols.Value = (byte)currentValueOfBoardSize;
        }

        private static bool isPlayerNameValid(string i_PlayerName)
        {
            bool playerNameIsValid = true;

            if (string.IsNullOrEmpty(i_PlayerName) == true || i_PlayerName.Contains(" ") || i_PlayerName.Length > 15)
            {
                playerNameIsValid = false;
            }

            return playerNameIsValid;
        }

        private bool playerNumber2IsNotAComputerValidationCheck()
        {
            bool playerNumber2IsValid = true;

            if (textBoxPlayer2.Text.Equals("[Computer]") == false && isPlayerNameValid(textBoxPlayer2.Text) == false)
            {
                playerNumber2IsValid = false;
            }

            return playerNumber2IsValid;
        }

        private void CheckBoxPlayer2_Click(object sender, EventArgs e)
        {
            if ((sender as CheckBox).Checked)
            {
                textBoxPlayer2.Enabled = true;
                textBoxPlayer2.Text = "";
            }
            else
            {
                textBoxPlayer2.Enabled = false;
                textBoxPlayer2.Text = "[Computer]";
            }
        }

        public string Player1
        {
            get { return lablePlayer1.Text; }
        }

        public string Player2
        {
            get { return checkBoxPlayer2.Text; }
        }

        public FormGameBoard FormGameBoard
        {
            get { return m_FormGameBoard; }
        }

        public ReverseTicTacToeGame ReverseTicTacToeGame
        {
            get { return m_ReverseTicTocToeGame; }
        }
    }
}
