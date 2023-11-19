using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace B23_Ex05_Ori_307837096_Roy_316060151
{
    public class ReverseTicTacToeGame
    {
        public const char k_FirstPlayerSign = 'X';
        public const char k_SecondPlayerSign = 'O';
        private readonly bool r_IsComputerPlaying;
        private ReverseTicTacToeBoard m_BoardGame;
        private Player m_FirstPlayer;
        private Player m_SecondPlayer;
        private Player m_CurrentPlayer;
        private eCurrentStatusOfTheGame m_StatusGame;
        private Random m_ComputerRandomChoice = new Random();

        public ReverseTicTacToeGame(bool i_IsPlayingAgainstComputer, string i_FirstPlayerName, string i_SecondPlayerName, byte i_BoardSize)
        {
            m_FirstPlayer = new Player(i_FirstPlayerName, k_FirstPlayerSign);
            m_SecondPlayer = new Player(i_SecondPlayerName, k_SecondPlayerSign);
            m_BoardGame = new ReverseTicTacToeBoard(i_BoardSize);
            r_IsComputerPlaying = i_IsPlayingAgainstComputer;
            m_CurrentPlayer = m_FirstPlayer;
            m_StatusGame = eCurrentStatusOfTheGame.PlayingGame;
        }

        public Player PlayerOne
        {
            get { return m_FirstPlayer; }
        }

        public Player PlayerTwo
        {
            get { return m_SecondPlayer; }
        }

        public Player CurrentPlayer
        {
            get { return m_CurrentPlayer; }
            set { m_CurrentPlayer = value; }
        }
        public Random ComputerRandomChoice
        {
            get { return m_ComputerRandomChoice; }
            set { m_ComputerRandomChoice = value; }
        }
        public ReverseTicTacToeBoard BoardGame
        {
            get { return m_BoardGame; }
        }

        public bool IsComputerPlaying
        {
            get { return r_IsComputerPlaying; }
        }

        public eCurrentStatusOfTheGame StatusGame
        {
            get { return m_StatusGame; }
            set { m_StatusGame = value; }
        }

        public void PlayTurn(BoardCell io_BoardCell)
        {
            eInputValidationCheck validationCheck;
            UpdateStatusGame(io_BoardCell, out validationCheck);
        }

        public void UpdateStatusGame(BoardCell i_PlayerMove, out eInputValidationCheck o_TurnStatusAtBoardGame)
        {
            o_TurnStatusAtBoardGame = doPlayerMove(i_PlayerMove);

            if (o_TurnStatusAtBoardGame == eInputValidationCheck.IsVaildInput)
            {
                if (ThePlayerLostTheGame(CurrentPlayer.Sign, i_PlayerMove))
                {
                    if (CurrentPlayer == m_FirstPlayer)
                    {
                        m_StatusGame = eCurrentStatusOfTheGame.PlayerOneLost;
                    }
                    else if (CurrentPlayer == m_SecondPlayer)
                    {
                        m_StatusGame = eCurrentStatusOfTheGame.PlayerTwoLost;
                    }

                    UpdatePlayerScore(CurrentPlayer);
                }
                else if (CheckIfGameIsOver())
                {
                    m_StatusGame = eCurrentStatusOfTheGame.ItIsTie;
                }
                else
                {
                    m_StatusGame = eCurrentStatusOfTheGame.PlayingGame;
                    SwitchPlayers();
                }
            }
        }

        public void ComputerMove(ref BoardCell io_ComputerSelectedCell)
        {
            io_ComputerSelectedCell.Row = Convert.ToByte(ComputerRandomChoice.Next(0, m_BoardGame.BoardSize));
            io_ComputerSelectedCell.Col = Convert.ToByte(ComputerRandomChoice.Next(0, m_BoardGame.BoardSize));
            while (m_BoardGame[io_ComputerSelectedCell.Row, io_ComputerSelectedCell.Col].IsMark)
            {
                io_ComputerSelectedCell.Row = Convert.ToByte(ComputerRandomChoice.Next(0, m_BoardGame.BoardSize));
                io_ComputerSelectedCell.Col = Convert.ToByte(ComputerRandomChoice.Next(0, m_BoardGame.BoardSize));
            }

            Thread.Sleep(100);
        }

        private eInputValidationCheck doPlayerMove(BoardCell io_PlayerMove)
        {
            eInputValidationCheck isMoveDoneProperly;

            isMoveDoneProperly = m_BoardGame.UpdateCellAtBoardGame(io_PlayerMove, CurrentPlayer.Sign);

            return isMoveDoneProperly;
        }

        public void SwitchPlayers()
        {
            if (CurrentPlayer == PlayerOne)
            {
                CurrentPlayer = PlayerTwo;
            }
            else
            {
                CurrentPlayer = PlayerOne;
            }
        }

        public void UpdatePlayerScore(Player i_CurrentPlayer)
        {
            if (i_CurrentPlayer == PlayerOne)
            {
                PlayerTwo.Score++;
            }
            else
            {
                PlayerOne.Score++;
            }
        }

        public bool ItIsTie()
        {
            bool itIsADraw = true;

            if (m_BoardGame.AmountOfFullCells < Math.Pow(m_BoardGame.BoardSize, 2))
            {
                itIsADraw = false;
            }

            return itIsADraw;
        }

        public bool ThePlayerLostTheGame(char io_Sign, BoardCell io_BoardCell)
        {
            bool thePlayerLostTheGame = false;

            if (m_BoardGame.ThereIsASequence(io_Sign, io_BoardCell))
            {
                thePlayerLostTheGame = true;
            }

            return thePlayerLostTheGame;
        }

        public bool CheckIfGameIsOver()
        {
            bool gameOver = false;

            if (ItIsTie())
            {
                gameOver = true;
            }

            return gameOver;
        }

        public void InitializationGameForNewRound()
        {
            m_BoardGame = new ReverseTicTacToeBoard(BoardGame.BoardSize);
            m_StatusGame = eCurrentStatusOfTheGame.PlayingGame;
            m_CurrentPlayer = PlayerOne;
        }
    }
}
