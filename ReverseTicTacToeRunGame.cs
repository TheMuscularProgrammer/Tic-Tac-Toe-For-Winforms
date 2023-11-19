using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace B23_Ex05_Ori_307837096_Roy_316060151
{
    internal class ReverseTicTacToeRunGame
    {
        public static void StartGame()
        {
            FormLogin formLogin = new FormLogin();

            formLogin.ShowDialog();

            if (formLogin.DialogResult == DialogResult.OK)
            {
                formLogin.FormGameBoard.ShowDialog();
            }
        }
    }
}
