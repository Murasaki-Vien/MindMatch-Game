using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Runtime.InteropServices;

namespace Second_Project_attempt
{
    public partial class TopLeads : Form
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

        static string dataInfo = "server=localhost;"
                           + "password=password;"
                           + "user=root;"
                           + "database=questions;"
                           + "port=3306;";

        private MySqlConnection Sqlconn = new MySqlConnection(dataInfo);   // Connection between C# and MySQL
        private MySqlCommand Sqlcmd;       // To Execute queries 
        private MySqlDataAdapter SqlDataAdptr;
        public DataTable DataTbl;
        string Sortby = "";
        int mouseX = 0 , mouseY = 0;
        bool mousePressed;

        public TopLeads()
        {
            InitializeComponent();
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 25, 25));
        } 
        
        private void TopLeads_Load(object sender, EventArgs e)
        {
            this.Round();
            this.transparent();
            this.buttonHideSortPanel.Hide();
            this.panelChooseCateg.Show();
            this.TopPLayersDataGrid.Hide();
        }

        private void Round()   //removes pointy sides
        {
            ClearDataLeads.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, ClearDataLeads.Width, ClearDataLeads.Height, 10, 10));

            buttonSortBy.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, ClearDataLeads.Width, ClearDataLeads.Height, 10, 10));

            buttonHideSortPanel.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, ClearDataLeads.Width, ClearDataLeads.Height, 10, 10));
        }

        private void transparent()
        {
            TopPLayersDataGrid.BackgroundColor = Color.FromArgb(94, 23, 235);
            pictureBox1.Parent = this.backgroundPictureBox; // making the background tranparent
            pictureBox1.BackColor = Color.Transparent;

            ClearDataLeads.Parent = this.backgroundPictureBox;
            ClearDataLeads.BackColor = Color.Transparent;

            huTaoPictureBox.Parent = this.backgroundPictureBox;
            huTaoPictureBox.BackColor = Color.Transparent;

            huTaoDialogueBox.Parent = this.backgroundPictureBox;
            huTaoDialogueBox.BackColor = Color.Transparent;

            buttonSortBy.Parent = this.backgroundPictureBox;
            buttonSortBy.BackColor = Color.Transparent;
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
            this.Close();
        }

        private void ClearDataLeads_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("YOU ARE ABOUT TO DELETE THE LEADERBOARD DATA.\nDO YOU STILL WANT TO CONTINUE", "WARNING MUST KNOW!", MessageBoxButtons.YesNo) ==DialogResult.Yes)
            {
                Sqlconn = new MySqlConnection(dataInfo);
                Sqlconn.Open();
                string DeleteLead = "TRUNCATE TABLE TopPlayers;"; //deletes the information from the table
                Sqlcmd = new MySqlCommand(DeleteLead, Sqlconn);   //executes the query
                Sqlcmd.ExecuteNonQuery();
                
                MessageBox.Show("The Program will SHUTDOWN PLease re-open the GAME","DONE DELETING THE DATA");
                Sqlconn.Close();
                System.Environment.Exit(1);
            }
        }

        private void buttonSortBy_Click(object sender, EventArgs e)
        {
            this.panelChooseCateg.Show();
            this.TopPLayersDataGrid.Hide();
            this.buttonSortBy.Hide();
            this.buttonHideSortPanel.Show();
        }

        private void buttonHideSortPanel_Click(object sender, EventArgs e)
        {
            this.buttonHideSortPanel.Hide();
            this.buttonSortBy.Show();
            this.panelChooseCateg.Hide();
        }

        public void diffCategories()
        {
            string topPlayers = "";
            SqlDataAdptr = new MySqlDataAdapter();
            DataTbl = new DataTable();
            Sqlconn.Open();

            if (this.Sortby == "Overall"){
                topPlayers = "SELECT Player_Name, Player_Score, Category FROM TopPlayers ORDER BY Player_Score DESC LIMIT 5; ";
                Sqlcmd = new MySqlCommand(topPlayers, Sqlconn);
                SqlDataAdptr.SelectCommand = Sqlcmd;
                //fill the datagridview
                SqlDataAdptr.Fill(DataTbl);
                //bind datagrid view
                TopPLayersDataGrid.DataSource = DataTbl;
            }else if (this.Sortby == "Random Quiz"){
                topPlayers = "SELECT Player_Name, Player_Score, Category FROM TopPlayers WHERE Category = 'Random Quiz' ORDER BY Player_Score DESC LIMIT 5; ";
                Sqlcmd = new MySqlCommand(topPlayers, Sqlconn);
                SqlDataAdptr.SelectCommand = Sqlcmd;
                //fill the datagridview
                SqlDataAdptr.Fill(DataTbl);
                //bind datagrid view
                TopPLayersDataGrid.DataSource = DataTbl;
            }
            else if(this.Sortby =="Sports"){
                topPlayers = "SELECT Player_Name, Player_Score, Category FROM TopPlayers WHERE Category = 'Sports' ORDER BY Player_Score DESC LIMIT 5; ";
                Sqlcmd = new MySqlCommand(topPlayers, Sqlconn);
                SqlDataAdptr.SelectCommand = Sqlcmd;
                //fill the datagridview
                SqlDataAdptr.Fill(DataTbl);
                //bind datagrid view
                TopPLayersDataGrid.DataSource = DataTbl;
            }
            else if(this.Sortby== "Geography"){
                topPlayers = "SELECT Player_Name, Player_Score, Category FROM TopPlayers WHERE Category = 'Geography' ORDER BY Player_Score DESC LIMIT 5; ";
                Sqlcmd = new MySqlCommand(topPlayers, Sqlconn);
                SqlDataAdptr.SelectCommand = Sqlcmd;
                //fill the datagridview
                SqlDataAdptr.Fill(DataTbl);
                //bind datagrid view
                TopPLayersDataGrid.DataSource = DataTbl;
            }
            else if (this.Sortby == "History"){
                topPlayers = "SELECT Player_Name, Player_Score, Category FROM TopPlayers WHERE Category = 'History' ORDER BY Player_Score DESC LIMIT 5; ";
                Sqlcmd = new MySqlCommand(topPlayers, Sqlconn);
                SqlDataAdptr.SelectCommand = Sqlcmd;
                //fill the datagridview
                SqlDataAdptr.Fill(DataTbl);
                //bind datagrid view
                TopPLayersDataGrid.DataSource = DataTbl;
            }
            else if (this.Sortby == "Animated Films"){
                topPlayers = "SELECT Player_Name, Player_Score, Category FROM TopPlayers WHERE Category = 'Animated Films' ORDER BY Player_Score DESC LIMIT 5; ";
                Sqlcmd = new MySqlCommand(topPlayers, Sqlconn);
                SqlDataAdptr.SelectCommand = Sqlcmd;
                //fill the datagridview
                SqlDataAdptr.Fill(DataTbl);
                //bind datagrid view
                TopPLayersDataGrid.DataSource = DataTbl;
            }
            else if (this.Sortby == "PCnCpGames"){
                topPlayers = "SELECT Player_Name, Player_Score, Category FROM TopPlayers WHERE Category = 'Games' ORDER BY Player_Score DESC LIMIT 5; ";
                Sqlcmd = new MySqlCommand(topPlayers, Sqlconn);
                SqlDataAdptr.SelectCommand = Sqlcmd;
                //fill the datagridview
                SqlDataAdptr.Fill(DataTbl);
                //bind datagrid view
                TopPLayersDataGrid.DataSource = DataTbl;
            }
            Sqlconn.Close();
            this.buttonSortBy.Show();
        }
       
        private void buttonOverall_Click(object sender, EventArgs e)
        {
            this.Sortby = "Overall";
            diffCategories();
            this.panelChooseCateg.Hide();
            this.buttonHideSortPanel.Hide();
            this.TopPLayersDataGrid.Show();
        }

        private void buttonRandomCat_Click(object sender, EventArgs e)
        {
            this.Sortby = "Random Quiz";
            diffCategories();
            this.panelChooseCateg.Hide();
            this.buttonHideSortPanel.Hide();
            this.TopPLayersDataGrid.Show();
        }

        private void buttonSportsCat_Click(object sender, EventArgs e)
        {
            this.Sortby = "Sports";
            diffCategories();
            this.panelChooseCateg.Hide();
            this.buttonHideSortPanel.Hide();
            this.TopPLayersDataGrid.Show();
        }

        private void buttonGeoCat_Click(object sender, EventArgs e)
        {
            this.Sortby = "Geography";
            diffCategories();
            this.panelChooseCateg.Hide();
            this.buttonHideSortPanel.Hide();
            this.TopPLayersDataGrid.Show();
        }

        private void buttonHistoryCat_Click(object sender, EventArgs e)
        {
            this.Sortby = "History";
            diffCategories();
            this.panelChooseCateg.Hide();
            this.buttonHideSortPanel.Hide();
            this.TopPLayersDataGrid.Show();
        }

        private void buttonAnimFilmsCat_Click(object sender, EventArgs e)
        {
            this.Sortby = "Animated Films";
            diffCategories();
            this.panelChooseCateg.Hide();
            this.buttonHideSortPanel.Hide();
            this.TopPLayersDataGrid.Show();
        }

        private void buttonPCnCPgCat_Click(object sender, EventArgs e)
        {
            this.Sortby = "PCnCpGames";
            diffCategories();
            this.panelChooseCateg.Hide();
            this.buttonHideSortPanel.Hide();
            this.TopPLayersDataGrid.Show();
        }

        private void backgroundPictureBox_MouseDown(object sender, MouseEventArgs e) //makes the windows movable
        {
            mousePressed = true;
        }

        private void backgroundPictureBox_MouseMove(object sender, MouseEventArgs e) //makes the windows movable
        {
            if (mousePressed)
            {
                mouseX = MousePosition.X-300;
                mouseY = MousePosition.Y-10;

                this.SetDesktopLocation(mouseX, mouseY);
            }
        }

        private void backgroundPictureBox_MouseUp(object sender, MouseEventArgs e) //makes the windows movable
        {
            mousePressed=false;
        }
    }
}