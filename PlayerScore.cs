using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Second_Project_attempt
{
    public partial class PlayerScore : Form
    {
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]

        private static extern IntPtr CreateRoundRectRgn
            (
            int LeftRgn,
            int RightRgn,
            int TopRgn,
            int BottomRgn,
            int Width,
            int Height
            );

        public PlayerScore()
        {
            InitializeComponent();
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 25, 25));
        }

        private void PlayGameBtn_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void PlayerScore_Load(object sender, EventArgs e)
        {
            PlayerScoretxtBox.ReadOnly = false;
            PlayerScoretxtBox.ReadOnly = false;
            PlayerNameBox.Text = Form1.Pname;
            PlayerScoretxtBox.Text = $"{Form1.Pscore} Out of 10";
        }
    }
}