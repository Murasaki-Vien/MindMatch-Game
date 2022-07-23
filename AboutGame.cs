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
    public partial class AboutGame : Form
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

        public AboutGame()
        {
            InitializeComponent();
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 25, 25));
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            (this.Owner as Form1).NewplayerBtn.Enabled = true;
            (this.Owner as Form1).LeaderboardsBtn.Enabled = true;
            (this.Owner as Form1).btnAboutGame.Enabled = true;
            this.Close();
        }
    }
}
