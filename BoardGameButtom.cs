namespace B23_Ex05_Ori_307837096_Roy_316060151
{
    using System.Windows.Forms;
    public partial class BoardGameButton : Button
    {
        private const byte k_MinVal = 3;
        private const byte k_MaxVal = 9;
        private byte m_Row;
        private byte m_Col;

        public BoardGameButton(byte i_Row, byte i_Col)
        {
            m_Row = i_Row;
            m_Col = i_Col;
        }

        public byte Row
        {
            get { return m_Row; }
            set
            {
                if (value >= k_MinVal && value <= k_MaxVal)
                {
                    m_Row = value;
                }
            }
        }

        public byte Col
        {
            get { return m_Col; }
            set
            {
                if (value >= k_MinVal && value <= k_MaxVal)
                {
                    m_Col = value;
                }
            }
        }
    }
}