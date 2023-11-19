using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;


namespace B23_Ex05_Ori_307837096_Roy_316060151
{
    public partial class FormGameBoard : Form
    {
        private ReverseTicTacToeGame m_ReverseTicTacToeGame;
        private BoardGameButton[,] m_BoardGameButtons;

        public FormGameBoard(byte i_BoardSize, ReverseTicTacToeGame i_ReverseTicTacToeGame)
        {
            int rightLocation, bottomLocation;

            m_ReverseTicTacToeGame = i_ReverseTicTacToeGame;
            m_BoardGameButtons = new BoardGameButton[i_BoardSize, i_BoardSize];
            setButtonsOnBoardGame(m_ReverseTicTacToeGame.BoardGame.BoardSize, out rightLocation, out bottomLocation);

            foreach (BoardGameButton button in BoardGameButtons)
            {
                button.BackColor = Color.Gray;
                button.ForeColor = Color.Black;
                button.Click += Button_Click;
                button.TabStop = false;
            }

            InitializeComponent();
            updateFormGameBoard(rightLocation, bottomLocation, i_ReverseTicTacToeGame.PlayerOne.Name, i_ReverseTicTacToeGame.PlayerTwo.Name);
        }

        private void changeFontLabelPlayer(Label i_PlayerLabelToBold, Label i_PlayerLabelToRegular)
        {
            i_PlayerLabelToBold.Font = new Font(i_PlayerLabelToBold.Font, FontStyle.Bold);
            i_PlayerLabelToRegular.Font = new Font(i_PlayerLabelToRegular.Font, FontStyle.Regular);
        }

        private BoardCell chooseMoveToPlay(BoardGameButton i_ButtonClicked)
        {
            BoardCell playerSelectedCell = new BoardCell();

            if (m_ReverseTicTacToeGame.IsComputerPlaying && m_ReverseTicTacToeGame.CurrentPlayer == m_ReverseTicTacToeGame.PlayerTwo)
            {
                m_ReverseTicTacToeGame.ComputerMove(ref playerSelectedCell);
            }
            else
            {
                playerSelectedCell.Row = i_ButtonClicked.Row;
                playerSelectedCell.Col = i_ButtonClicked.Col;
            }

            return playerSelectedCell;
        }

        private void playCurrentPlayerTurn(BoardGameButton i_ButtonClicked)
        {
            BoardCell boardCell = new BoardCell();

            if (m_ReverseTicTacToeGame.IsComputerPlaying && m_ReverseTicTacToeGame.CurrentPlayer == m_ReverseTicTacToeGame.PlayerTwo)
            {
                m_ReverseTicTacToeGame.ComputerMove(ref boardCell);
            }
            else
            {
                boardCell = chooseMoveToPlay(i_ButtonClicked);
            }

            if (m_ReverseTicTacToeGame.BoardGame[boardCell.Row, boardCell.Col].IsMark == false)
            {
                BoardGameButtons[boardCell.Row, boardCell.Col].Text = char.ToString(m_ReverseTicTacToeGame.CurrentPlayer.Sign);
                BoardGameButtons[boardCell.Row, boardCell.Col].Enabled = false;
                m_ReverseTicTacToeGame.PlayTurn(boardCell);
                gameStatusMessage();

                if (m_ReverseTicTacToeGame.CurrentPlayer == m_ReverseTicTacToeGame.PlayerOne)
                {
                    changeFontLabelPlayer(this.labelPlayer1Name, this.labelPlayer2Name);
                }
                else
                {
                    changeFontLabelPlayer(this.labelPlayer2Name, this.labelPlayer1Name);
                }
            }
        }

        private void Button_Click(object sender, EventArgs e)
        {
            BoardGameButton button = sender as BoardGameButton;
            int turnNumberToPlay = m_ReverseTicTacToeGame.IsComputerPlaying == true ? 2 : 1;

            playCurrentPlayerTurn(button);
            if (m_ReverseTicTacToeGame.CurrentPlayer == m_ReverseTicTacToeGame.PlayerTwo && m_ReverseTicTacToeGame.IsComputerPlaying == true)
            {
                playCurrentPlayerTurn(button);
            }
        }
       
        private void gameStatusMessage()
        {
            DialogResult dialogResult = DialogResult.None;
            string AnotherRound = "Would you like to play another round?";

            switch (ReverseTicTacToeGame.StatusGame)
            {
                case eCurrentStatusOfTheGame.PlayerOneLost:
                    dialogResult = MessageBox.Show(string.Format("The winner is {0}!{1}{2}", ReverseTicTacToeGame.PlayerTwo.Name, Environment.NewLine, AnotherRound), "A Win!", MessageBoxButtons.YesNo);
                    break;

                case eCurrentStatusOfTheGame.PlayerTwoLost:
                    dialogResult = MessageBox.Show(string.Format("The winner is {0}!{1}{2}", ReverseTicTacToeGame.PlayerOne.Name, Environment.NewLine, AnotherRound), "A Win!", MessageBoxButtons.YesNo);
                    break;

                case eCurrentStatusOfTheGame.ItIsTie:
                    dialogResult = MessageBox.Show(string.Format("Tie!{0}{1}", Environment.NewLine, AnotherRound), "A Tie!", MessageBoxButtons.YesNo);
                    break;

                default:
                    break;
            }

            if (dialogResult == DialogResult.Yes)
            {
                resetGame();
                labelScore.Text = currentScore();
            }
            else if (dialogResult == DialogResult.No)
            {
                MessageBox.Show(string.Format("See you next time!{0}The score is {1} {2}:{3} {4}", Environment.NewLine, ReverseTicTacToeGame.PlayerOne.Name, ReverseTicTacToeGame.PlayerOne.Score, ReverseTicTacToeGame.PlayerTwo.Score, ReverseTicTacToeGame.PlayerTwo.Name), "Bye Bye!", MessageBoxButtons.OK);
                DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void updateFormGameBoard(int i_FormWidth, int i_FormHeight, string i_Player1Name, string i_Player2Name)
        {
            updateFormSize(i_FormWidth, i_FormHeight);
            updatePlayersLabels(i_Player1Name, i_Player2Name, i_FormHeight - 10);
        }

        private void updateFormSize(int i_FormWidth, int i_FormHeight)
        {
            this.ClientSize = new Size(i_FormWidth, i_FormHeight + 10);
        }

        private void updatePlayersLabels(string i_Player1Name, string i_Player2Name, int io_FormHeight)
        {
            this.labelPlayer1Name = new Label();
            this.labelPlayer2Name = new Label();
            this.labelScore = new Label();

            string player1 = string.Format("{0}", ReverseTicTacToeGame.PlayerOne.Name);
            string player2 = string.Format("{0}", ReverseTicTacToeGame.PlayerTwo.Name);
            string score = currentScore();

            this.labelPlayer1Name.AutoSize = true;
            this.labelPlayer2Name.AutoSize = true;
            this.labelScore.AutoSize = true;
            labelPlayer1Name.Text = player1;
            labelPlayer2Name.Text = player2;
            labelScore.Text = score;
            this.labelPlayer1Name.Font = new Font(this.labelPlayer1Name.Font, FontStyle.Bold);
            this.Controls.Add(labelPlayer1Name);
            this.Controls.Add(labelPlayer2Name);
            this.Controls.Add(labelScore);
            updatePlayersLabelsLocation(io_FormHeight);
        }

        private string currentScore()
        {
            return string.Format("{0} : {1}", ReverseTicTacToeGame.PlayerOne.Score, ReverseTicTacToeGame.PlayerTwo.Score);
        }

        private void updatePlayersLabelsLocation(int i_FormHeigt)
        {
            int boardCenter = (this.Size.Width - labelScore.Size.Width) / 2;

            labelScore.Location = new Point(boardCenter - (labelScore.Size.Width / 2), i_FormHeigt);
            labelPlayer1Name.Location = new Point(labelScore.Left - (labelPlayer1Name.Size.Width + 5), i_FormHeigt);
            labelPlayer2Name.Location = new Point(labelScore.Right + 5, i_FormHeigt);
        }

        private void setButtonsOnBoardGame(byte i_BoardSize, out int io_RightLocation, out int io_BottomLocation)
        {
            int leftLocation = this.Left;
            io_BottomLocation = this.Top;

            for (byte row = 0; row < i_BoardSize; row++)
            {
                for (byte col = 0; col < i_BoardSize; col++)
                {
                    BoardGameButtons[row, col] = new BoardGameButton(row, col);
                    BoardGameButtons[row, col].Size = new Size(70, 70);
                    BoardGameButtons[row, col].Left = leftLocation + 15;
                    BoardGameButtons[row, col].Top = io_BottomLocation + 15;
                    BoardGameButtons[row, col].Enabled = true;
                    this.Controls.Add(BoardGameButtons[row, col]);
                    leftLocation = BoardGameButtons[row, col].Right;
                }

                leftLocation = this.Left;
                io_BottomLocation = BoardGameButtons[row, i_BoardSize - 1].Bottom;
            }

            this.Width = BoardGameButtons[i_BoardSize - 1, i_BoardSize - 1].Right;
            io_RightLocation = this.Width + 20;
            io_BottomLocation += 20;
        }

        private void resetGame()
        {
            m_ReverseTicTacToeGame.InitializationGameForNewRound();

            foreach (BoardGameButton button in BoardGameButtons)
            {
                button.Text = "";
                button.Enabled = true;
            }
        }

        private void Computer_Turn(object sender, EventArgs e)
        {
            BoardCell playerSelectedCell = new BoardCell();

            (sender as System.Windows.Forms.Timer).Stop();
            m_ReverseTicTacToeGame.ComputerMove(ref playerSelectedCell);
        }

        public ReverseTicTacToeGame ReverseTicTacToeGame
        {
            get { return m_ReverseTicTacToeGame; }
        }

        public BoardGameButton[,] BoardGameButtons
        {
            get { return m_BoardGameButtons; }
        }
    }
}

    


