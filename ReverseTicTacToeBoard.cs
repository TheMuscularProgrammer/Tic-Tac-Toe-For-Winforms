using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B23_Ex05_Ori_307837096_Roy_316060151
{
    public class ReverseTicTacToeBoard
    {
        private const byte k_BoardMinimumSize = 3;
        private const byte k_BoardMaximumSize = 9;
        private readonly BoardCell[,] r_BoardGame;
        private byte m_BoardSize;
        private byte m_AmountOfFullCells;
        private const char k_Blank = ' ';

        public ReverseTicTacToeBoard(byte i_BoardSize)
        {
            m_BoardSize = i_BoardSize;
            m_AmountOfFullCells = 0;
            r_BoardGame = new BoardCell[i_BoardSize, i_BoardSize];
        }

        public BoardCell this[int row, int col]
        {
            get { return r_BoardGame[row, col]; }
            private set { r_BoardGame[row, col] = value; }
        }

        public byte BoardSize
        {
            get { return m_BoardSize; }
            set { m_BoardSize = value; }
        }

        public byte AmountOfFullCells
        {
            get { return m_AmountOfFullCells; }
        }

        public eInputValidationCheck UpdateCellAtBoardGame(BoardCell io_BoardCell, char i_Sign)
        {
            eInputValidationCheck inputValidationCheckResult;

            if (!cellIsInBoardRange(io_BoardCell))
            {
                inputValidationCheckResult = eInputValidationCheck.OutOfRange;
            }
            else if (io_BoardCell.TheCellIsAlredyMarked())
            {
                inputValidationCheckResult = eInputValidationCheck.OccupiedCell;
            }
            else
            {
                r_BoardGame[io_BoardCell.Row, io_BoardCell.Col].Sign = i_Sign;
                r_BoardGame[io_BoardCell.Row, io_BoardCell.Col].IsMark = true;
                m_AmountOfFullCells++;
                inputValidationCheckResult = eInputValidationCheck.IsVaildInput;
            }

            return inputValidationCheckResult;
        }

        private bool cellIsInBoardRange(BoardCell io_BoardCell)
        {
            bool cellIsInRange = false;

            if (legalInputForCell(io_BoardCell.Row) && legalInputForCell(io_BoardCell.Col))
            {
                cellIsInRange = true;
            }

            return cellIsInRange;
        }

        private bool legalInputForCell(byte i_RowOrColCell)
        {
            bool isLegalInput = false;

            if (i_RowOrColCell >= 0 && i_RowOrColCell < m_BoardSize)
            {
                isLegalInput = true;
            }

            return isLegalInput;
        }

        public bool ThereIsASequence(char i_sign, BoardCell i_BoardCell)
        {
            bool thereIsASequence = false;

            if (i_BoardCell.Row == i_BoardCell.Col)
            {
                thereIsASequence = thereIsASequenceAtStraightDiagonal(i_sign);
            }
            else if (i_BoardCell.Row + i_BoardCell.Col == m_BoardSize - 1)
            {
                thereIsASequence = thereIsASequenceAtReverseDiagonal(i_sign);
            }

            if (!thereIsASequence)
            {
                thereIsASequence = thereIsSequenceAtRow(i_sign, i_BoardCell.Row) || thereIsSequenceAtCol(i_sign, i_BoardCell.Col);
            }

            return thereIsASequence;
        }

        private bool thereIsSequenceAtRow(char i_sign, byte i_RelevantRow)
        {
            bool sequenceAtRow = true;

            for (int col = 0; col < m_BoardSize && sequenceAtRow; col++)
            {
                if (r_BoardGame[i_RelevantRow, col].Sign != i_sign)
                {
                    sequenceAtRow = false;
                }
            }

            return sequenceAtRow;
        }

        private bool thereIsSequenceAtCol(char i_sign, byte i_RelevantCol)
        {
            bool sequenceAtCol = true;

            for (int row = 0; row < m_BoardSize && sequenceAtCol; row++)
            {
                if (r_BoardGame[row, i_RelevantCol].Sign != i_sign)
                {
                    sequenceAtCol = false;
                }
            }

            return sequenceAtCol;
        }

        private bool thereIsASequenceAtStraightDiagonal(char i_sign)
        {
            bool sequenceAtStraightDiagonal = true;

            for (int diagonal = 0; diagonal < m_BoardSize && sequenceAtStraightDiagonal; diagonal++)
            {
                if (r_BoardGame[diagonal, diagonal].Sign != i_sign)
                {
                    sequenceAtStraightDiagonal = false;
                }
            }

            return sequenceAtStraightDiagonal;
        }

        private bool thereIsASequenceAtReverseDiagonal(char i_sign)
        {
            bool sequenceAtReverseDiagonalm = true;

            for (int row = 0, col = m_BoardSize - 1; row < m_BoardSize && col >= 0 && sequenceAtReverseDiagonalm; row++, col--)
            {
                if (r_BoardGame[row, col].Sign != i_sign)
                {
                    sequenceAtReverseDiagonalm = false;
                }
            }

            return sequenceAtReverseDiagonalm;
        }

        public static eInputValidationCheck IsTheBoardSizeInRange(byte i_BoardSize)
        {
            eInputValidationCheck inputValidationCheckResult = eInputValidationCheck.IsVaildInput;

            if (i_BoardSize < k_BoardMinimumSize || i_BoardSize > k_BoardMaximumSize)
            {
                inputValidationCheckResult = eInputValidationCheck.OutOfRange;
            }

            return inputValidationCheckResult;
        }
    }
}
