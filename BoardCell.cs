using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B23_Ex05_Ori_307837096_Roy_316060151
{
    public struct BoardCell
    {
        public byte m_Row;
        public byte m_Col;
        public char m_Sign;
        public bool m_IsMark;
        

        public BoardCell(byte i_Row = 0, byte i_Col = 0)
        {
            m_Row = i_Row;
            m_Col = i_Col;
            m_Sign = ' ';
            m_IsMark = false;
        }

        public byte Row
        {
            get { return m_Row; }
            set { m_Row = value; }
        }

        public byte Col
        {
            get { return m_Col; }
            set { m_Col = value; }
        }

        public char Sign
        {
            get { return m_Sign; }
            set { m_Sign = value; }
        }

        public bool IsMark
        {
            get { return m_IsMark; }
            set { m_IsMark = value; }
        }

        public bool TheCellIsAlredyMarked()
        {
            return m_IsMark;
        }

    }
}
