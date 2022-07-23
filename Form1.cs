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
using System.Media;
using System.IO;
using MySql.Data.MySqlClient;

namespace Second_Project_attempt
{
    public partial class Form1 : Form
    {
        
        SoundPlayer soundPlayer = new SoundPlayer();
        int rightAns;
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

        int mouseX = 0, mouseY = 0;
        bool mousePressed;

        string dataInfo = "server=localhost;"
                           + "password=password;"
                           + "user=root;"
                           + "database=questions;"
                           + "port=3306;";

        private int counter;
        private MySqlConnection Sqlconn;   // Connection between C# and MySQL
        private MySqlCommand Sqlcmd;       // To Execute queries
        private MySqlDataReader Sqlrdr;    // To read the values of the table 
        private randomNumbers rndNum;
        private int ctr, score;
        public static string Pname;
        public static int Pscore;
        private string quizMode;

        public Form1()
        {
            InitializeComponent();
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 25, 25));
            Game_menu.Show();
            PlayerNamelbl.Text = "Enter New Player Name";
            this.panelSelectCateg.Hide();
        }

       
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to exit game?", "Exit Mind Match", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void NewplayerBtn_Click(object sender, EventArgs e)
        {
            gameTimer.Stop();
            gameTimer.Dispose();
            this.panelSelectCateg.Hide();
            PlayerNamelbl.Text = "Enter New Player Name";
            textBoxCategory.Text = "";
            PlayGameBtn.Enabled = true;
            this.counter = 0;
            Game_menu.Show();
            this.pictureBoxSelectCateg.Hide();
        }

        private void LeaderboardsBtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            TopLeads topLeads = new TopLeads();
            topLeads.ShowDialog();
            this.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.pictureBoxSelectCateg.Hide();
            this.rndButtons();
            soundPlayer.SoundLocation = @".\Jeremy Blake Powerup.wav";
            soundPlayer.Play();
            soundPlayer.PlayLooping();
        }

        private void rndButtons()   //removes pointy sides of buttons
        {
            PlayGameBtn.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, PlayGameBtn.Width, PlayGameBtn.Height, 10, 10));
            NewplayerBtn.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, NewplayerBtn.Width, NewplayerBtn.Height, 10, 10));
            LeaderboardsBtn.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, LeaderboardsBtn.Width, LeaderboardsBtn.Height, 10, 10));
            btnAboutGame.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, btnAboutGame.Width, btnAboutGame.Height, 10, 10));
        }
        private void pictureBox9_Click(object sender, EventArgs e)
        {

        }

        private void NoBtn_Click(object sender, EventArgs e)
        {
            soundPlayer.Stop();
        }

        private void btnYes_Click(object sender, EventArgs e)
        {
            soundPlayer.Play();
        }

        private void btnAboutGame_Click(object sender, EventArgs e)
        {
            NewplayerBtn.Enabled = false;
            LeaderboardsBtn.Enabled = false;
            btnAboutGame.Enabled = false;

            AboutGame aboutGame = new AboutGame();
            aboutGame.Owner = this;
            aboutGame.Show();
        }
        public void butttons()
        {
            NewplayerBtn.Enabled = true;
            LeaderboardsBtn.Enabled = true;
            btnAboutGame.Enabled = true;
        }
        private void PlayGameBtn_Click(object sender, EventArgs e)
        {
            this.PlayGameBtn.Enabled = false;
            this.panelSelectCateg.Show();
            this.pictureBoxSelectCateg.Visible = true;
            
            if (string.IsNullOrWhiteSpace(PlayerNametxtBox.Text))
            {
                PlayerNametxtBox.Text = "Anonymous";
                Pname = this.PlayerNametxtBox.Text;
                PlayerNamelbl.Text = PlayerNametxtBox.Text;
            }
            else
            {
                PlayerNamelbl.Text = PlayerNametxtBox.Text;
                Pname = this.PlayerNametxtBox.Text;
            }
            PlayerNametxtBox.Text = " ";
        }

        public void mySQLDataQuestions()
        {
            string a1 = "", a2 = "", a3 = "", a4 = "";
            string table = "";
            string column = "";

            if (this.quizMode == "Random Quiz"){
                table = "testQuestions";
                column = "Q_ID";
            } else if (this.quizMode == "Animated Films"){
                table = "animatedfilms";
                column = "animFilm_ID";
            } else if (this.quizMode == "Geography") {
                table = "geographycateg";
                column = "geoQ_ID";
            }else if (this.quizMode == "History"){
                table = "historycateg";
                column = "historyQ_ID";
            }else if(this.quizMode == "Sports"){
                table = "sportscateg";
                column = "sportsQ_ID";
            }else if(this.quizMode== "Games"){
                table = "gamecateg";
                column = "gameQ_ID";
            }

            this.rndNum.GenerateRandom();
            Sqlconn = new MySqlConnection(dataInfo);
            Sqlconn.Open();
            bool isOkay = false;
            
            string sqlSelectQuery1 = $"SELECT * FROM {table} WHERE {column} = 1;";
            string sqlSelectQuery2 = $"SELECT * FROM {table} WHERE {column} = 2;";
            string sqlSelectQuery3 = $"SELECT * FROM {table} WHERE {column} = 3;";
            string sqlSelectQuery4 = $"SELECT * FROM {table} WHERE {column} = 4;";
            string sqlSelectQuery5 = $"SELECT * FROM {table} WHERE {column} = 5;";
            string sqlSelectQuery6 = $"SELECT * FROM {table} WHERE {column} = 6;";
            string sqlSelectQuery7 = $"SELECT * FROM {table} WHERE {column} = 7;";
            string sqlSelectQuery8 = $"SELECT * FROM {table} WHERE {column} = 8;";
            string sqlSelectQuery9 = $"SELECT * FROM {table} WHERE {column} = 9;";
            string sqlSelectQuery10 = $"SELECT * FROM {table} WHERE {column} = 10;";
            string sqlSelectQuery11 = $"SELECT * FROM {table} WHERE {column} = 11;";
            string sqlSelectQuery12 = $"SELECT * FROM {table} WHERE {column} = 12;";
            string sqlSelectQuery13 = $"SELECT * FROM {table} WHERE {column} = 13;";
            string sqlSelectQuery14 = $"SELECT * FROM {table} WHERE {column} = 14;";
            string sqlSelectQuery15 = $"SELECT * FROM {table} WHERE {column} = 15;";
            string sqlSelectQuery16 = $"SELECT * FROM {table} WHERE {column} = 16;";
            string sqlSelectQuery17 = $"SELECT * FROM {table} WHERE {column} = 17;";
            string sqlSelectQuery18 = $"SELECT * FROM {table} WHERE {column} = 18;";
            string sqlSelectQuery19 = $"SELECT * FROM {table} WHERE {column} = 19;";
            string sqlSelectQuery20 = $"SELECT * FROM {table} WHERE {column} = 20;";

            do
            {
                switch (this.rndNum.temp)
                {
                    case 1: //set of row 1 Questions from diff categ. 
                        {
                            Sqlcmd = new MySqlCommand(sqlSelectQuery1, Sqlconn);
                            if (this.quizMode == "Random Quiz")  //Random Q1
                            {

                                pictureBoxImages.Image = Properties.Resources._1__Genshin_Impact_Jean_Art;
                                Sqlrdr = Sqlcmd.ExecuteReader();
                                if (Sqlrdr.Read())
                                {
                                    textBoxQuestion.Text = (Sqlrdr["Question"].ToString());
                                }
                                a1 = "Jean";
                                a2 = "Diluc";
                                a3 = "Xiangling";
                                a4 = "Barbara";
                            } 
                            else if (this.quizMode == "Animated Films") //animated films Q1
                            {
                                pictureBoxImages.Image = Properties.Resources._1olaf;
                                Sqlrdr = Sqlcmd.ExecuteReader();
                                if (Sqlrdr.Read())
                                {
                                    textBoxQuestion.Text = (Sqlrdr["Question"].ToString());
                                }
                                a1 = "Frozen";
                                a2 = "Proben";
                                a3 = "Tangled";
                                a4 = "Rapunzel";
                            }
                            else if (this.quizMode == "Geography") //Geography Q1
                            {
                                pictureBoxImages.Image = Properties.Resources._1Nevada;
                                Sqlrdr = Sqlcmd.ExecuteReader();
                                if (Sqlrdr.Read())
                                {
                                    textBoxQuestion.Text = (Sqlrdr["Question"].ToString());
                                }
                                a1 = "Nevada";
                                a2 = "Colorado";
                                a3 = "United States";
                                a4 = "Alabama";
                            }
                            else if (this.quizMode == "History")
                            {
                                pictureBoxImages.Image = Properties.Resources._1Thomas_Jefferson;
                                Sqlrdr = Sqlcmd.ExecuteReader();
                                if (Sqlrdr.Read())
                                {
                                    textBoxQuestion.Text = (Sqlrdr["Question"].ToString());
                                }
                                a1 = "Thomas Jefferson";
                                a2 = "Thomas Gabriel";
                                a3 = "Thomas Marumas";
                                a4 = "Thomas Alcaria";
                            }
                            else if (this.quizMode == "Sports")
                            {
                                pictureBoxImages.Image = Properties.Resources._1Soccer;
                                Sqlrdr = Sqlcmd.ExecuteReader();
                                if (Sqlrdr.Read())
                                {
                                    textBoxQuestion.Text = (Sqlrdr["Question"].ToString());
                                }
                                a1 = "Soccer";
                                a2 = "Tennis";
                                a3 = "Baseball";
                                a4 = "Basketball";
                            }
                            else if (this.quizMode == "Games")
                            {
                                pictureBoxImages.Image = Properties.Resources._1Monster_Strike;
                                Sqlrdr = Sqlcmd.ExecuteReader();
                                if (Sqlrdr.Read())
                                {
                                    textBoxQuestion.Text = (Sqlrdr["Question"].ToString());
                                }
                                a1 = "Monster Strike";
                                a2 = "Monster Legends";
                                a3 = "Monster Go";
                                a4 = "Monster Bash";
                            }

                                button1.Text = a1;
                                button2.Text = a2;
                                button3.Text = a3;
                                button4.Text = a4;
                                                        
                            rightAns = 1;
                            Sqlrdr.Close();
                        }
                        ctr++;
                        isOkay = true;
                        break;
                    case 2: // set of row 2 questions from diff categ.
                        {
                            Sqlcmd = new MySqlCommand(sqlSelectQuery2, Sqlconn);
                            if (this.quizMode == "Random Quiz")
                            {
                                pictureBoxImages.Image = Properties.Resources._2__Venti_Barbatos;
                                Sqlrdr = Sqlcmd.ExecuteReader();
                                if (Sqlrdr.Read())
                                {
                                    textBoxQuestion.Text = (Sqlrdr["Question"].ToString());
                                }
                                a1 = "Morax/Zhongli";
                                a2 = "Barbatos";
                                a3 = "Bennett";
                                a4 = "Eula";
                            }
                            else if (this.quizMode == "Animated Films")
                            {
                                pictureBoxImages.Image = Properties.Resources._2Up;
                                Sqlrdr = Sqlcmd.ExecuteReader();
                                if (Sqlrdr.Read())
                                {
                                    textBoxQuestion.Text = (Sqlrdr["Question"].ToString());
                                }
                                a1 = "Helicopter";
                                a2 = "Balloon/Balloons";
                                a3 = "Blimp/Blimps";
                                a4 = "Bird/Birds";
                            }
                            else if (this.quizMode == "Geography")
                            {
                                pictureBoxImages.Image = Properties.Resources._2India;
                                Sqlrdr = Sqlcmd.ExecuteReader();
                                if (Sqlrdr.Read())
                                {
                                    textBoxQuestion.Text = (Sqlrdr["Question"].ToString());
                                }
                                a1 = "Arabia";
                                a2 = "India";
                                a3 = "Bengal";
                                a4 = "None of the Choices";
                            }
                            else if (this.quizMode == "History")
                            {
                                pictureBoxImages.Image = Properties.Resources._2Genghis_Khan;
                                Sqlrdr = Sqlcmd.ExecuteReader();
                                if (Sqlrdr.Read())
                                {
                                    textBoxQuestion.Text = (Sqlrdr["Question"].ToString());
                                }
                                a1 = "Genghis Khes";
                                a2 = "Genghis Khan";
                                a3 = "Christopher Columbus";
                                a4 = "Kaiser Wilhelm II";
                            }
                            else if (this.quizMode == "Sports")
                            {
                                pictureBoxImages.Image = Properties.Resources._2Lacrosse;
                                Sqlrdr = Sqlcmd.ExecuteReader();
                                if (Sqlrdr.Read())
                                {
                                    textBoxQuestion.Text = (Sqlrdr["Question"].ToString());
                                }
                                a1 = "Sucrosse";
                                a2 = "Lacrosse";
                                a3 = "Bacrosse";
                                a4 = "Sacrosse";
                            }
                            else if (this.quizMode == "Games")
                            {
                                pictureBoxImages.Image = Properties.Resources._2Arena_of_Valor;
                                Sqlrdr = Sqlcmd.ExecuteReader();
                                if (Sqlrdr.Read())
                                {
                                    textBoxQuestion.Text = (Sqlrdr["Question"].ToString());
                                }
                                a1 = "Mobile Legends";
                                a2 = "Arena of Valor";
                                a3 = "Onmyoji Arena";
                                a4 = "League Of Legends";
                            }

                                button1.Text = a1;
                                button2.Text = a2;
                                button3.Text = a3;
                                button4.Text = a4;

                            rightAns = 2;
                            Sqlrdr.Close();
                        }
                        ctr++;
                        isOkay = true;
                        break;
                    case 3: //set of row 3 questions from diff categ.
                        {
                            Sqlcmd = new MySqlCommand(sqlSelectQuery3, Sqlconn);
                            if (this.quizMode == "Random Quiz")
                            {
                                pictureBoxImages.Image = Properties.Resources._3__Zhongli;
                                Sqlrdr = Sqlcmd.ExecuteReader();
                                if (Sqlrdr.Read())
                                {
                                    textBoxQuestion.Text = (Sqlrdr["Question"].ToString());
                                }
                                a1 = "Venti";
                                a2 = "Lisa";
                                a3 = "Morax/Zhongli";
                                a4 = "Ganyu";
                            }
                            else if (this.quizMode == "Animated Films")
                            {
                                pictureBoxImages.Image = Properties.Resources._3Shrek;
                                Sqlrdr = Sqlcmd.ExecuteReader();
                                if (Sqlrdr.Read())
                                {
                                    textBoxQuestion.Text = (Sqlrdr["Question"].ToString());
                                }
                                a1 = "Brown";
                                a2 = "White";
                                a3 = "Green";
                                a4 = "White";
                            }
                            else if (this.quizMode == "Geography")
                            {
                                pictureBoxImages.Image = Properties.Resources._3Vatican_City;
                                Sqlrdr = Sqlcmd.ExecuteReader();
                                if (Sqlrdr.Read())
                                {
                                    textBoxQuestion.Text = (Sqlrdr["Question"].ToString());
                                }
                                a1 = "Nauru";
                                a2 = "Monaco";
                                a3 = "Vatican City";
                                a4 = "Cebu City";
                            }
                            else if (this.quizMode == "History")
                            {
                                pictureBoxImages.Image = Properties.Resources._3Christopher_Columbus;
                                Sqlrdr = Sqlcmd.ExecuteReader();
                                if (Sqlrdr.Read())
                                {
                                    textBoxQuestion.Text = (Sqlrdr["Question"].ToString());
                                }
                                a1 = "Leif Erikson";
                                a2 = "Christopher Gab";
                                a3 = "Christopher Columbus";
                                a4 = "Leif Jefferson";
                            }
                            else if (this.quizMode == "Sports")
                            {
                                pictureBoxImages.Image = Properties.Resources._3Liechtenstein;
                                Sqlrdr = Sqlcmd.ExecuteReader();
                                if (Sqlrdr.Read())
                                {
                                    textBoxQuestion.Text = (Sqlrdr["Question"].ToString());
                                }
                                a1 = "Liechtenestein";
                                a2 = "Liechtenstine";
                                a3 = "Liechtenstein";
                                a4 = "Liechtensteine";
                            }
                            else if (this.quizMode == "Games")
                            {
                                pictureBoxImages.Image = Properties.Resources._3Puzzles_and_Dragons;
                                Sqlrdr = Sqlcmd.ExecuteReader();
                                if (Sqlrdr.Read())
                                {
                                    textBoxQuestion.Text = (Sqlrdr["Question"].ToString());
                                }
                                a1 = "Mario Bros";
                                a2 = "Yu-Gi-oH";
                                a3 = "Puzzles and Dragons";
                                a4 = "Legends of Zelda";
                            }

                            button1.Text = a1;
                                button2.Text = a2;
                                button3.Text = a3;
                                button4.Text = a4;

                            rightAns = 3;
                            Sqlrdr.Close();
                        }
                        ctr++;
                        isOkay = true;
                        break;
                    case 4: //set of row 4 questions from diff categ.
                        {
                            Sqlcmd = new MySqlCommand(sqlSelectQuery4, Sqlconn);
                            if (this.quizMode == "Random Quiz")
                            {
                                pictureBoxImages.Image = Properties.Resources._4__Eula_Genshin_Impact;
                                Sqlrdr = Sqlcmd.ExecuteReader();
                                if (Sqlrdr.Read())
                                {
                                    textBoxQuestion.Text = (Sqlrdr["Question"].ToString());
                                }
                                a1 = "Mona";
                                a2 = "Morax";
                                a3 = "Jean";
                                a4 = "Eula";
                            }
                            else if (this.quizMode == "Animated Films")
                            {
                                pictureBoxImages.Image = Properties.Resources._4Dumbo;
                                Sqlrdr = Sqlcmd.ExecuteReader();
                                if (Sqlrdr.Read())
                                {
                                    textBoxQuestion.Text = (Sqlrdr["Question"].ToString());
                                }
                                a1 = "Simba";
                                a2 = "Panda";
                                a3 = "Elephant";
                                a4 = "Dumbo";
                            }
                            else if (this.quizMode == "Geography")
                            {
                                pictureBoxImages.Image = Properties.Resources._4Russia;
                                Sqlrdr = Sqlcmd.ExecuteReader();
                                if (Sqlrdr.Read())
                                {
                                    textBoxQuestion.Text = (Sqlrdr["Question"].ToString());
                                }
                                a1 = "America";
                                a2 = "Cuba";
                                a3 = "Germany";
                                a4 = "Russia";
                            }
                            else if (this.quizMode == "History")
                            {
                                pictureBoxImages.Image = Properties.Resources._4Portuguese;
                                Sqlrdr = Sqlcmd.ExecuteReader();
                                if (Sqlrdr.Read())
                                {
                                    textBoxQuestion.Text = (Sqlrdr["Question"].ToString());
                                }
                                a1 = "Americans";
                                a2 = "Japanese";
                                a3 = "Chinese";
                                a4 = "Portuguese";
                            }
                            else if (this.quizMode == "Sports")
                            {
                                pictureBoxImages.Image = Properties.Resources._4National_Basketball_Association;
                                Sqlrdr = Sqlcmd.ExecuteReader();
                                if (Sqlrdr.Read())
                                {
                                    textBoxQuestion.Text = (Sqlrdr["Question"].ToString());
                                }
                                a1 = "National Basket Association";
                                a2 = "Nation Basketball Association";
                                a3 = "National Baseball Association";
                                a4 = "National Basketball Association";
                            }
                            else if (this.quizMode == "Games")
                            {
                                pictureBoxImages.Image = Properties.Resources._4Pokemon_Go;
                                Sqlrdr = Sqlcmd.ExecuteReader();
                                if (Sqlrdr.Read())
                                {
                                    textBoxQuestion.Text = (Sqlrdr["Question"].ToString());
                                }
                                a1 = "Pokemon Emerald";
                                a2 = "Pokemon Sapphire";
                                a3 = "Pokemon Gold";
                                a4 = "Pokemon Go";
                            }

                                button1.Text = a1;
                                button2.Text = a2;
                                button3.Text = a3;
                                button4.Text = a4;

                            rightAns = 4;
                            Sqlrdr.Close();
                        }
                        ctr++;
                        isOkay = true;
                        break;
                    case 5: //set of row 5 questions from diff categ.
                        {
                            Sqlcmd = new MySqlCommand(sqlSelectQuery5, Sqlconn);
                            if (this.quizMode == "Random Quiz")
                            {
                                pictureBoxImages.Image = Properties.Resources._5__Bennett;
                                Sqlrdr = Sqlcmd.ExecuteReader();
                                if (Sqlrdr.Read())
                                {
                                    textBoxQuestion.Text = (Sqlrdr["Question"].ToString());
                                }
                                a1 = "Diluc";
                                a2 = "Bennett";
                                a3 = "Albaedo";
                                a4 = "Kaeya";
                            }
                            else if (this.quizMode == "Animated Films")
                            {
                                pictureBoxImages.Image = Properties.Resources._5KungfuPanda;
                                Sqlrdr = Sqlcmd.ExecuteReader();
                                if (Sqlrdr.Read())
                                {
                                    textBoxQuestion.Text = (Sqlrdr["Question"].ToString());
                                }
                                a1 = "Kung Fu Mantis";
                                a2 = "Kung Fu Panda";
                                a3 = "Kung Fu Tigres";
                                a4 = "Kung Fu Monkey";
                            }
                            else if (this.quizMode == "Geography")
                            {
                                pictureBoxImages.Image = Properties.Resources._5Cuba;
                                Sqlrdr = Sqlcmd.ExecuteReader();
                                if (Sqlrdr.Read())
                                {
                                    textBoxQuestion.Text = (Sqlrdr["Question"].ToString());
                                }
                                a1 = "Russia";
                                a2 = "Cuba";
                                a3 = "Saudi Arabia";
                                a4 = "India";
                            }
                            else if (this.quizMode == "History")
                            {
                                pictureBoxImages.Image = Properties.Resources._5Year1521;
                                Sqlrdr = Sqlcmd.ExecuteReader();
                                if (Sqlrdr.Read())
                                {
                                    textBoxQuestion.Text = (Sqlrdr["Question"].ToString());
                                }
                                a1 = "1522";
                                a2 = "1521";
                                a3 = "1421";
                                a4 = "1422";
                            }
                            else if (this.quizMode == "Sports")
                            {
                                pictureBoxImages.Image = Properties.Resources._5Checkered_flag;
                                Sqlrdr = Sqlcmd.ExecuteReader();
                                if (Sqlrdr.Read())
                                {
                                    textBoxQuestion.Text = (Sqlrdr["Question"].ToString());
                                }
                                a1 = "Any styled flag";
                                a2 = "Checkered flag";
                                a3 = "Square dotted flag";
                                a4 = "White stripped black flag";
                            }
                            else if (this.quizMode == "Games")
                            {
                                pictureBoxImages.Image = Properties.Resources._5PlayerUnknowns_Battlegrounds_Mobile;
                                Sqlrdr = Sqlcmd.ExecuteReader();
                                if (Sqlrdr.Read())
                                {
                                    textBoxQuestion.Text = (Sqlrdr["Question"].ToString());
                                }
                                a1 = "Call of Duty Mobile";
                                a2 = "PlayerUnknown's Battlegrounds Mobile";
                                a3 = "Apex Mobile";
                                a4 = "LOL Mobile";
                            }

                                button1.Text = a1;
                                button2.Text = a2;
                                button3.Text = a3;
                                button4.Text = a4;

                            rightAns = 2;
                            Sqlrdr.Close();
                        }
                        ctr++;
                        isOkay = true;
                        break;
                    case 6: //set of 6 questions from diff categ.
                        {
                            Sqlcmd = new MySqlCommand(sqlSelectQuery6, Sqlconn);
                            if (this.quizMode == "Random Quiz")
                            {
                                pictureBoxImages.Image = Properties.Resources._6__Tony_Stark;
                                Sqlrdr = Sqlcmd.ExecuteReader();
                                if (Sqlrdr.Read())
                                {
                                    textBoxQuestion.Text = (Sqlrdr["Question"].ToString());
                                }
                                a1 = "Captain America";
                                a2 = "Tony Stark";
                                a3 = "Thor";
                                a4 = "Loki";
                            }
                            else if (this.quizMode == "Animated Films")
                            {
                                pictureBoxImages.Image = Properties.Resources._6Ratatouille;
                                Sqlrdr = Sqlcmd.ExecuteReader();
                                if (Sqlrdr.Read())
                                {
                                    textBoxQuestion.Text = (Sqlrdr["Question"].ToString());
                                }
                                a1 = "Ratatille";
                                a2 = "Ratatouille";
                                a3 = "Ratatouile";
                                a4 = "Ratatuille";
                            }
                            else if (this.quizMode == "Geography")
                            {
                                pictureBoxImages.Image = Properties.Resources._6Canada;
                                Sqlrdr = Sqlcmd.ExecuteReader();
                                if (Sqlrdr.Read())
                                {
                                    textBoxQuestion.Text = (Sqlrdr["Question"].ToString());
                                }
                                a1 = "Singapore";
                                a2 = "Canada";
                                a3 = "Philippines";
                                a4 = "United States of America";
                            }
                            else if (this.quizMode == "History")
                            {
                                pictureBoxImages.Image = Properties.Resources._6Cyrus_the_Great;
                                Sqlrdr = Sqlcmd.ExecuteReader();
                                if (Sqlrdr.Read())
                                {
                                    textBoxQuestion.Text = (Sqlrdr["Question"].ToString());
                                }
                                a1 = "Shing the Great";
                                a2 = "Cyrus the Great";
                                a3 = "Lloyd the Great";
                                a4 = "Jeff the Great";
                            }
                            else if (this.quizMode == "Sports")
                            {
                                pictureBoxImages.Image = Properties.Resources._6Yellow;
                                Sqlrdr = Sqlcmd.ExecuteReader();
                                if (Sqlrdr.Read())
                                {
                                    textBoxQuestion.Text = (Sqlrdr["Question"].ToString());
                                }
                                a1 = "Green";
                                a2 = "Yellow";
                                a3 = "Silver";
                                a4 = "Brown";
                            }
                            else if (this.quizMode == "Games")
                            {
                                pictureBoxImages.Image = Properties.Resources._6Fate_Grand_Order;
                                Sqlrdr = Sqlcmd.ExecuteReader();
                                if (Sqlrdr.Read())
                                {
                                    textBoxQuestion.Text = (Sqlrdr["Question"].ToString());
                                }
                                a1 = "Sword Art Online: Alicization";
                                a2 = "Fate/Grand Order";
                                a3 = "Legend of Zelda";
                                a4 = "Elden Ring";
                            }

                            button1.Text = a1;
                                button2.Text = a2;
                                button3.Text = a3;
                                button4.Text = a4;

                            rightAns = 2;
                            Sqlrdr.Close();
                        }
                        ctr++;
                        isOkay = true;
                        break;
                    case 7: //set of row 7 questions from diff categ.
                        {
                            Sqlcmd = new MySqlCommand(sqlSelectQuery7, Sqlconn);
                            if (this.quizMode == "Random Quiz")
                            {
                                pictureBoxImages.Image = Properties.Resources._7__thanos;
                                Sqlrdr = Sqlcmd.ExecuteReader();
                                if (Sqlrdr.Read())
                                {
                                    textBoxQuestion.Text = (Sqlrdr["Question"].ToString());
                                }
                                a1 = "Groot";
                                a2 = "Peter Parker";
                                a3 = "Thanos";
                                a4 = "Loki";
                            }
                            else if (this.quizMode == "Animated Films")
                            {
                                pictureBoxImages.Image = Properties.Resources._7Mulan;
                                Sqlrdr = Sqlcmd.ExecuteReader();
                                if (Sqlrdr.Read())
                                {
                                    textBoxQuestion.Text = (Sqlrdr["Question"].ToString());
                                }
                                a1 = "Mulane";
                                a2 = "General Sheng";
                                a3 = "Mulan";
                                a4 = "Molane";
                            }
                            else if (this.quizMode == "Geography")
                            {
                                pictureBoxImages.Image = Properties.Resources._7Saudi_Arabia;
                                Sqlrdr = Sqlcmd.ExecuteReader();
                                if (Sqlrdr.Read())
                                {
                                    textBoxQuestion.Text = (Sqlrdr["Question"].ToString());
                                }
                                a1 = "Angola";
                                a2 = "India";
                                a3 = "Saudi Arabia";
                                a4 = "Bangladesh";
                            }
                            else if (this.quizMode == "History")
                            {
                                pictureBoxImages.Image = Properties.Resources._7Olmec;
                                Sqlrdr = Sqlcmd.ExecuteReader();
                                if (Sqlrdr.Read())
                                {
                                    textBoxQuestion.Text = (Sqlrdr["Question"].ToString());
                                }
                                a1 = "Navajo";
                                a2 = "Chippewa";
                                a3 = "Olmec";
                                a4 = "Cherokee";
                            }
                            else if (this.quizMode == "Sports")
                            {
                                pictureBoxImages.Image = Properties.Resources._7Tennis;
                                Sqlrdr = Sqlcmd.ExecuteReader();
                                if (Sqlrdr.Read())
                                {
                                    textBoxQuestion.Text = (Sqlrdr["Question"].ToString());
                                }
                                a1 = "Table Tennis";
                                a2 = "Badminton";
                                a3 = "Tennis";
                                a4 = "Baseball";
                            }
                            else if (this.quizMode == "Games")
                            {
                                pictureBoxImages.Image = Properties.Resources._7Roblox_Mobile;
                                Sqlrdr = Sqlcmd.ExecuteReader();
                                if (Sqlrdr.Read())
                                {
                                    textBoxQuestion.Text = (Sqlrdr["Question"].ToString());
                                }
                                a1 = "Soul Knight";
                                a2 = "Minecraft";
                                a3 = "Roblox Mobile";
                                a4 = "Dude Theft Wars: Offline";
                            }

                                button1.Text = a1;
                                button2.Text = a2;
                                button3.Text = a3;
                                button4.Text = a4;

                            rightAns = 3;
                            Sqlrdr.Close();

                        }
                        ctr++;
                        isOkay = true;
                        break;
                    case 8: //set of row 8 questions from diff categ.
                        {
                            Sqlcmd = new MySqlCommand(sqlSelectQuery8, Sqlconn);
                            if (this.quizMode == "Random Quiz")
                            {
                                pictureBoxImages.Image = Properties.Resources._8__The_Pyramid_of_Giza;
                                Sqlrdr = Sqlcmd.ExecuteReader();
                                if (Sqlrdr.Read())
                                {
                                    textBoxQuestion.Text = (Sqlrdr["Question"].ToString());
                                }
                                a1 = "Mount Fuji";
                                a2 = "Eiffel Tower";
                                a3 = "Great Pyramid of Giza";
                                a4 = "Grand Palace";
                            }
                            else if (this.quizMode == "Animated Films")
                            {
                                pictureBoxImages.Image = Properties.Resources._8Tangled;
                                Sqlrdr = Sqlcmd.ExecuteReader();
                                if (Sqlrdr.Read())
                                {
                                    textBoxQuestion.Text = (Sqlrdr["Question"].ToString());
                                }
                                a1 = "Mulan 2";
                                a2 = "Mulan";
                                a3 = "Tangled";
                                a4 = "Tanglaed";
                            }
                            else if (this.quizMode == "Geography")
                            {
                                pictureBoxImages.Image = Properties.Resources._8Asia;
                                Sqlrdr = Sqlcmd.ExecuteReader();
                                if (Sqlrdr.Read())
                                {
                                    textBoxQuestion.Text = (Sqlrdr["Question"].ToString());
                                }
                                a1 = "Africa";
                                a2 = "South America";
                                a3 = "Asia";
                                a4 = "North America";
                            }
                            else if (this.quizMode == "History")
                            {
                                pictureBoxImages.Image = Properties.Resources._8Adolf_Hitler;
                                Sqlrdr = Sqlcmd.ExecuteReader();
                                if (Sqlrdr.Read())
                                {
                                    textBoxQuestion.Text = (Sqlrdr["Question"].ToString());
                                }
                                a1 = "Adolfy Hetlir";
                                a2 = "Shielo Fitler";
                                a3 = "Adolf Hitler";
                                a4 = "Dave Adolf";
                            }
                            else if (this.quizMode == "Sports")
                            {
                                pictureBoxImages.Image = Properties.Resources._8Baseball;
                                Sqlrdr = Sqlcmd.ExecuteReader();
                                if (Sqlrdr.Read())
                                {
                                    textBoxQuestion.Text = (Sqlrdr["Question"].ToString());
                                }
                                a1 = "Soccer";
                                a2 = "Football";
                                a3 = "Baseball";
                                a4 = "Tennis ball";
                            }
                            else if (this.quizMode == "Games")
                            {
                                pictureBoxImages.Image = Properties.Resources._8Gardenscapes;
                                Sqlrdr = Sqlcmd.ExecuteReader();
                                if (Sqlrdr.Read())
                                {
                                    textBoxQuestion.Text = (Sqlrdr["Question"].ToString());
                                }
                                a1 = "Talking Ben";
                                a2 = "Gardenescapes";
                                a3 = "Gardenscapes";
                                a4 = "Granny in Paradise";
                            }

                            button1.Text = a1;
                                button2.Text = a2;
                                button3.Text = a3;
                                button4.Text = a4;

                            rightAns = 3;
                            Sqlrdr.Close();
                        }
                        ctr++;
                        isOkay = true;
                        break;
                    case 9: //set of row 9 questions from diff categ.
                        {
                            Sqlcmd = new MySqlCommand(sqlSelectQuery9, Sqlconn);
                            if (this.quizMode == "Random Quiz")
                            {
                                pictureBoxImages.Image = Properties.Resources._9__Eiffel_Tower;
                                Sqlrdr = Sqlcmd.ExecuteReader();
                                if (Sqlrdr.Read())
                                {
                                    textBoxQuestion.Text = (Sqlrdr["Question"].ToString());
                                }
                                a1 = "Great Pyramid of Giza";
                                a2 = "Dubai, United Arab Emirates";
                                a3 = "Mount Fuji";
                                a4 = "Eiffel Tower";
                            }
                            else if (this.quizMode == "Animated Films")
                            {
                                pictureBoxImages.Image = Properties.Resources._9Dory;
                                Sqlrdr = Sqlcmd.ExecuteReader();
                                if (Sqlrdr.Read())
                                {
                                    textBoxQuestion.Text = (Sqlrdr["Question"].ToString());
                                }
                                a1 = "Dowry";
                                a2 = "Dary";
                                a3 = "Nemo";
                                a4 = "Dory";
                            }
                            else if (this.quizMode == "Geography")
                            {
                                pictureBoxImages.Image = Properties.Resources._9Sahara;
                                Sqlrdr = Sqlcmd.ExecuteReader();
                                if (Sqlrdr.Read())
                                {
                                    textBoxQuestion.Text = (Sqlrdr["Question"].ToString());
                                }
                                a1 = "Arabian Desert";
                                a2 = "Arctic Desert";
                                a3 = "Antarctic Desert";
                                a4 = "Sahara Desert";
                            }
                            else if (this.quizMode == "History")
                            {
                                pictureBoxImages.Image = Properties.Resources._9Kaiser_Wilhelm_II;
                                Sqlrdr = Sqlcmd.ExecuteReader();
                                if (Sqlrdr.Read())
                                {
                                    textBoxQuestion.Text = (Sqlrdr["Question"].ToString());
                                }
                                a1 = "Kaiser Wilhelm I";
                                a2 = "Kaiser Wilhelm III";
                                a3 = "Kaiser Wilhelm V";
                                a4 = "Kaiser Wilhelm II";
                            }
                            else if (this.quizMode == "Sports")
                            {
                                pictureBoxImages.Image = Properties.Resources._9Hands;
                                Sqlrdr = Sqlcmd.ExecuteReader();
                                if (Sqlrdr.Read())
                                {
                                    textBoxQuestion.Text = (Sqlrdr["Question"].ToString());
                                }
                                a1 = "Mouth";
                                a2 = "Head";
                                a3 = "Knees";
                                a4 = "Hands";
                            }
                            else if (this.quizMode == "Games")
                            {
                                pictureBoxImages.Image = Properties.Resources._9Stardew_Valley;
                                Sqlrdr = Sqlcmd.ExecuteReader();
                                if (Sqlrdr.Read())
                                {
                                    textBoxQuestion.Text = (Sqlrdr["Question"].ToString());
                                }
                                a1 = "Farming Game";
                                a2 = "Farming Simulator";
                                a3 = "Stardew Alley";
                                a4 = "Stardew Valley";
                            }

                                button1.Text = a1;
                                button2.Text = a2;
                                button3.Text = a3;
                                button4.Text = a4;

                            rightAns = 4;
                            Sqlrdr.Close();
                        }
                        ctr++;
                        isOkay = true;
                        break;
                    case 10: //set of row 10 questions from diff categ.
                        {
                            Sqlcmd = new MySqlCommand(sqlSelectQuery10, Sqlconn);
                            if (this.quizMode == "Random Quiz")
                            {
                                pictureBoxImages.Image = Properties.Resources._10__Grand_Palace;
                                Sqlrdr = Sqlcmd.ExecuteReader();
                                if (Sqlrdr.Read())
                                {
                                    textBoxQuestion.Text = (Sqlrdr["Question"].ToString());
                                }
                                a1 = "Eiffel Tower";
                                a2 = "Great Pyramid of Giza";
                                a3 = "Grand Palace";
                                a4 = "Mount Fuji";
                            }
                            else if (this.quizMode == "Animated Films")
                            {
                                pictureBoxImages.Image = Properties.Resources._10Minions;
                                Sqlrdr = Sqlcmd.ExecuteReader();
                                if (Sqlrdr.Read())
                                {
                                    textBoxQuestion.Text = (Sqlrdr["Question"].ToString());
                                }
                                a1 = "Yellow Ranger";
                                a2 = "Mike";
                                a3 = "Minions";
                                a4 = "Bob the builder";
                            }
                            else if (this.quizMode == "Geography")
                            {
                                pictureBoxImages.Image = Properties.Resources._10Amazon;
                                Sqlrdr = Sqlcmd.ExecuteReader();
                                if (Sqlrdr.Read())
                                {
                                    textBoxQuestion.Text = (Sqlrdr["Question"].ToString());
                                }
                                a1 = "Nile River";
                                a2 = "Mississipi River";
                                a3 = "Amazon";
                                a4 = "Yinesei";
                            }
                            else if (this.quizMode == "History")
                            {
                                pictureBoxImages.Image = Properties.Resources._10Thales_of_Melitus;
                                Sqlrdr = Sqlcmd.ExecuteReader();
                                if (Sqlrdr.Read())
                                {
                                    textBoxQuestion.Text = (Sqlrdr["Question"].ToString());
                                }
                                a1 = "Aristotle";
                                a2 = "Confucius";
                                a3 = "Thales of Melitus";
                                a4 = "Saint Thomas Aquinas";
                            }
                            else if (this.quizMode == "Sports")
                            {
                                pictureBoxImages.Image = Properties.Resources._10Tennis_racquet;
                                Sqlrdr = Sqlcmd.ExecuteReader();
                                if (Sqlrdr.Read())
                                {
                                    textBoxQuestion.Text = (Sqlrdr["Question"].ToString());
                                }
                                a1 = "Ping-Pong Rakcet";
                                a2 = "Baseball Racket";
                                a3 = "Tennis Racquet";
                                a4 = "Badminton Racket";
                            }
                            else if (this.quizMode == "Games")
                            {
                                pictureBoxImages.Image = Properties.Resources._10Square_Enix;
                                Sqlrdr = Sqlcmd.ExecuteReader();
                                if (Sqlrdr.Read())
                                {
                                    textBoxQuestion.Text = (Sqlrdr["Question"].ToString());
                                }
                                a1 = "Square Black";
                                a2 = "Square Cube";
                                a3 = "Square Enix";
                                a4 = "Square Stripe";
                            }

                            button1.Text = a1;
                                button2.Text = a2;
                                button3.Text = a3;
                                button4.Text = a4;

                            rightAns = 3;
                            Sqlrdr.Close();
                        }
                        ctr++;
                        isOkay = true;
                        break;
                    case 11: //set of row 11 questions from diff categ.
                        {
                            Sqlcmd = new MySqlCommand(sqlSelectQuery11, Sqlconn);
                            if (this.quizMode == "Random Quiz")
                            {
                                pictureBoxImages.Image = Properties.Resources._11__Mount_Fuji;
                                Sqlrdr = Sqlcmd.ExecuteReader();
                                if (Sqlrdr.Read())
                                {
                                    textBoxQuestion.Text = (Sqlrdr["Question"].ToString());
                                }
                                a1 = "Great Pyramid of Giza";
                                a2 = "Eiffel Tower";
                                a3 = "Mount Fuji";
                                a4 = "Dubai, United Arab Emirates";
                            }
                            else if (this.quizMode == "Animated Films")
                            {
                                pictureBoxImages.Image = Properties.Resources._11red;
                                Sqlrdr = Sqlcmd.ExecuteReader();
                                if (Sqlrdr.Read())
                                {
                                    textBoxQuestion.Text = (Sqlrdr["Question"].ToString());
                                }
                                a1 = "White";
                                a2 = "Black";
                                a3 = "Red";
                                a4 = "Blue";
                            }
                            else if (this.quizMode == "Geography")
                            {
                                pictureBoxImages.Image = Properties.Resources._11Istanbul;
                                Sqlrdr = Sqlcmd.ExecuteReader();
                                if (Sqlrdr.Read())
                                {
                                    textBoxQuestion.Text = (Sqlrdr["Question"].ToString());
                                }
                                a1 = "Halifax";
                                a2 = "Frankfurt";
                                a3 = "Istanbul";
                                a4 = "Galway";
                            }
                            else if (this.quizMode == "History")
                            {
                                pictureBoxImages.Image = Properties.Resources._11King_Philip_II;
                                Sqlrdr = Sqlcmd.ExecuteReader();
                                if (Sqlrdr.Read())
                                {
                                    textBoxQuestion.Text = (Sqlrdr["Question"].ToString());
                                }
                                a1 = "King Philip III";
                                a2 = "King Philip IV";
                                a3 = "King Philip II";
                                a4 = "King Philip I";
                            }
                            else if (this.quizMode == "Sports")
                            {
                                pictureBoxImages.Image = Properties.Resources._11Badminton;
                                Sqlrdr = Sqlcmd.ExecuteReader();
                                if (Sqlrdr.Read())
                                {
                                    textBoxQuestion.Text = (Sqlrdr["Question"].ToString());
                                }
                                a1 = "Ping-Pong";
                                a2 = "Baseball";
                                a3 = "Badminton";
                                a4 = "Tennis";
                            }
                            else if (this.quizMode == "Games")
                            {
                                pictureBoxImages.Image = Properties.Resources._11Sky_Children_of_Light;
                                Sqlrdr = Sqlcmd.ExecuteReader();
                                if (Sqlrdr.Read())
                                {
                                    textBoxQuestion.Text = (Sqlrdr["Question"].ToString());
                                }
                                a1 = "Adventure Academy";
                                a2 = "Genshin Imapact";
                                a3 = "Sky: Children of Light";
                                a4 = "The Witch Hunter";
                            }

                                button1.Text = a1;
                                button2.Text = a2;
                                button3.Text = a3;
                                button4.Text = a4;

                            rightAns = 3;
                            Sqlrdr.Close();
                        }
                        ctr++;
                        isOkay = true;
                        break;
                    case 12: //set of row 12 questions from diff categ.
                        {
                            Sqlcmd = new MySqlCommand(sqlSelectQuery12, Sqlconn);
                            if (this.quizMode == "Random Quiz")
                            {
                                pictureBoxImages.Image = Properties.Resources._12__dubai_united_arab_emirates;
                                Sqlrdr = Sqlcmd.ExecuteReader();
                                if (Sqlrdr.Read())
                                {
                                    textBoxQuestion.Text = (Sqlrdr["Question"].ToString());
                                }
                                a1 = "Dubai, United Arab Emirates";
                                a2 = "America";
                                a3 = "Philippines";
                                a4 = "Agra, India";
                            }
                            else if (this.quizMode == "Animated Films")
                            {
                                pictureBoxImages.Image = Properties.Resources._12fox;
                                Sqlrdr = Sqlcmd.ExecuteReader();
                                if (Sqlrdr.Read())
                                {
                                    textBoxQuestion.Text = (Sqlrdr["Question"].ToString());
                                }
                                a1 = "Fox";
                                a2 = "Rabbit";
                                a3 = "Sloth";
                                a4 = "Wolf";
                            }
                            else if (this.quizMode == "Geography")
                            {
                                pictureBoxImages.Image = Properties.Resources._12Angel_Falls;
                                Sqlrdr = Sqlcmd.ExecuteReader();
                                if (Sqlrdr.Read())
                                {
                                    textBoxQuestion.Text = (Sqlrdr["Question"].ToString());
                                }
                                a1 = "Angel Falls";
                                a2 = "Victoria Falls";
                                a3 = "Vermillion Falls";
                                a4 = "Tugera Falls";
                            }
                            else if (this.quizMode == "History")
                            {
                                pictureBoxImages.Image = Properties.Resources._12Neil_Armstrong;
                                Sqlrdr = Sqlcmd.ExecuteReader();
                                if (Sqlrdr.Read())
                                {
                                    textBoxQuestion.Text = (Sqlrdr["Question"].ToString());
                                }
                                a1 = "Neil Armstrong";
                                a2 = "Valentina Tereshkova";
                                a3 = "John Glenn";
                                a4 = "Yuri Gagarin";
                            }
                            else if (this.quizMode == "Sports")
                            {
                                pictureBoxImages.Image = Properties.Resources._12Hockey;
                                Sqlrdr = Sqlcmd.ExecuteReader();
                                if (Sqlrdr.Read())
                                {
                                    textBoxQuestion.Text = (Sqlrdr["Question"].ToString());
                                }
                                a1 = "Hockey";
                                a2 = "Rollerskate";
                                a3 = "Jockey";
                                a4 = "Rugby";
                            }
                            else if (this.quizMode == "Games")
                            {
                                pictureBoxImages.Image = Properties.Resources._12The_Room;
                                Sqlrdr = Sqlcmd.ExecuteReader();
                                if (Sqlrdr.Read())
                                {
                                    textBoxQuestion.Text = (Sqlrdr["Question"].ToString());
                                }
                                a1 = "The Room";
                                a2 = "The Door";
                                a3 = "The Key";
                                a4 = "The Knob";
                            }

                            button1.Text = a1;
                                button2.Text = a2;
                                button3.Text = a3;
                                button4.Text = a4;

                            rightAns = 1;
                            Sqlrdr.Close();
                        }
                        ctr++;
                        isOkay = true;
                        break;
                    case 13: //set of row 13 questions from diff categ.
                        {
                            Sqlcmd = new MySqlCommand(sqlSelectQuery13, Sqlconn);
                            if (this.quizMode == "Random Quiz")
                            {
                                pictureBoxImages.Image = Properties.Resources._13__tower_of_Pisa;
                                Sqlrdr = Sqlcmd.ExecuteReader();
                                if (Sqlrdr.Read())
                                {
                                    textBoxQuestion.Text = (Sqlrdr["Question"].ToString());
                                }
                                a1 = "America";
                                a2 = "Italy";
                                a3 = "Philippines";
                                a4 = "Agra, India";
                            }
                            else if (this.quizMode == "Animated Films")
                            {
                                pictureBoxImages.Image = Properties.Resources._13Big_Hero_6;
                                Sqlrdr = Sqlcmd.ExecuteReader();
                                if (Sqlrdr.Read())
                                {
                                    textBoxQuestion.Text = (Sqlrdr["Question"].ToString());
                                }
                                a1 = "Big Hero 7";
                                a2 = "Big Hero 6";
                                a3 = "Big Hero 8";
                                a4 = "Big Hero 1";
                            }
                            else if (this.quizMode == "Geography")
                            {
                                pictureBoxImages.Image = Properties.Resources._13Greenland;
                                Sqlrdr = Sqlcmd.ExecuteReader();
                                if (Sqlrdr.Read())
                                {
                                    textBoxQuestion.Text = (Sqlrdr["Question"].ToString());
                                }
                                a1 = "New Guinea";
                                a2 = "Greenland";
                                a3 = "Borneo";
                                a4 = "Sumatra";
                            }
                            else if (this.quizMode == "History")
                            {
                                pictureBoxImages.Image = Properties.Resources._13Balangiga_Massacre;
                                Sqlrdr = Sqlcmd.ExecuteReader();
                                if (Sqlrdr.Read())
                                {
                                    textBoxQuestion.Text = (Sqlrdr["Question"].ToString());
                                }
                                a1 = "Mamasapano Massacre";
                                a2 = "Balangiga Massacre";
                                a3 = "Katyn massacre";
                                a4 = "St. Bartholomew’s Day massacre";
                            }
                            else if (this.quizMode == "Sports")
                            {
                                pictureBoxImages.Image = Properties.Resources._13Bicycle_race;
                                Sqlrdr = Sqlcmd.ExecuteReader();
                                if (Sqlrdr.Read())
                                {
                                    textBoxQuestion.Text = (Sqlrdr["Question"].ToString());
                                }
                                a1 = "Bicycle speed";
                                a2 = "Bicycle race";
                                a3 = "Bicycle marathon";
                                a4 = "Bicycle Iku";
                            }
                            else if (this.quizMode == "Games")
                            {
                                pictureBoxImages.Image = Properties.Resources._13Monument_Valley;
                                Sqlrdr = Sqlcmd.ExecuteReader();
                                if (Sqlrdr.Read())
                                {
                                    textBoxQuestion.Text = (Sqlrdr["Question"].ToString());
                                }
                                a1 = "Stardew Alley";
                                a2 = "Monument Valley ";
                                a3 = "Stardew Valley ";
                                a4 = "Monument Alley";
                            }

                                button1.Text = a1;
                                button2.Text = a2;
                                button3.Text = a3;
                                button4.Text = a4;

                            rightAns = 2;
                            Sqlrdr.Close();
                        }
                        ctr++;
                        isOkay = true;
                        break;
                    case 14: //set of row 14 questions from diff categ.
                        {
                            Sqlcmd = new MySqlCommand(sqlSelectQuery14, Sqlconn);
                            if (this.quizMode == "Random Quiz")
                            {
                                pictureBoxImages.Image = Properties.Resources._14__Taj_Mahal;
                                Sqlrdr = Sqlcmd.ExecuteReader();
                                if (Sqlrdr.Read())
                                {
                                    textBoxQuestion.Text = (Sqlrdr["Question"].ToString());
                                }
                                a1 = "Europe";
                                a2 = "Agra, India";
                                a3 = "Russia";
                                a4 = "China";
                            }
                            else if (this.quizMode == "Animated Films")
                            {
                                pictureBoxImages.Image = Properties.Resources._14mice;
                                Sqlrdr = Sqlcmd.ExecuteReader();
                                if (Sqlrdr.Read())
                                {
                                    textBoxQuestion.Text = (Sqlrdr["Question"].ToString());
                                }
                                a1 = "Lizards";
                                a2 = "Mice/Mouse";
                                a3 = "Cat/Cats";
                                a4 = "Dog/Dogs";
                            }
                            else if (this.quizMode == "Geography")
                            {
                                pictureBoxImages.Image = Properties.Resources._14Sudan;
                                Sqlrdr = Sqlcmd.ExecuteReader();
                                if (Sqlrdr.Read())
                                {
                                    textBoxQuestion.Text = (Sqlrdr["Question"].ToString());
                                }
                                a1 = "Congo";
                                a2 = "Sudan";
                                a3 = "Verde";
                                a4 = "Comoros";
                            }
                            else if (this.quizMode == "History")
                            {
                                pictureBoxImages.Image = Properties.Resources._14Decmber7;
                                Sqlrdr = Sqlcmd.ExecuteReader();
                                if (Sqlrdr.Read())
                                {
                                    textBoxQuestion.Text = (Sqlrdr["Question"].ToString());
                                }
                                a1 = "December 7, 1951";
                                a2 = "December 7, 1941";
                                a3 = "December 8, 1941";
                                a4 = "December 8, 1951";
                            }
                            else if (this.quizMode == "Sports")
                            {
                                pictureBoxImages.Image = Properties.Resources._14A_round_of_golf;
                                Sqlrdr = Sqlcmd.ExecuteReader();
                                if (Sqlrdr.Read())
                                {
                                    textBoxQuestion.Text = (Sqlrdr["Question"].ToString());
                                }
                                a1 = "A round of base";
                                a2 = "A round of golf";
                                a3 = "A round of hole";
                                a4 = "A round of flag";
                            }
                            else if (this.quizMode == "Games")
                            {
                                pictureBoxImages.Image = Properties.Resources._14Solar_ExplorerNew_Dawn;
                                Sqlrdr = Sqlcmd.ExecuteReader();
                                if (Sqlrdr.Read())
                                {
                                    textBoxQuestion.Text = (Sqlrdr["Question"].ToString());
                                }
                                a1 = "Outer Space";
                                a2 = "Solar Explorer: New Dawn";
                                a3 = "Space Explorer";
                                a4 = "Outer Space: The Dawn";
                            }

                                button1.Text = a1;
                                button2.Text = a2;
                                button3.Text = a3;
                                button4.Text = a4;

                            rightAns = 2;
                            Sqlrdr.Close();
                        }
                        ctr++;
                        isOkay = true;
                        break;
                    case 15: //set of row 15 questions from diff categ.
                        {
                            Sqlcmd = new MySqlCommand(sqlSelectQuery15, Sqlconn);
                            if (this.quizMode == "Random Quiz")
                            {
                                pictureBoxImages.Image = Properties.Resources._15__genshin_impact;
                                Sqlrdr = Sqlcmd.ExecuteReader();
                                if (Sqlrdr.Read())
                                {
                                    textBoxQuestion.Text = (Sqlrdr["Question"].ToString());
                                }
                                a1 = "Mobile Impact";
                                a2 = "PUBG Impact";
                                a3 = "Genshin Impact";
                                a4 = "Honkai Impact";
                            }
                            else if (this.quizMode == "Animated Films")
                            {
                                pictureBoxImages.Image = Properties.Resources._15bear;
                                Sqlrdr = Sqlcmd.ExecuteReader();
                                if (Sqlrdr.Read())
                                {
                                    textBoxQuestion.Text = (Sqlrdr["Question"].ToString());
                                }
                                a1 = "Bare";
                                a2 = "Baer";
                                a3 = "Bear";
                                a4 = "Snake";
                            }
                            else if (this.quizMode == "Geography")
                            {
                                pictureBoxImages.Image = Properties.Resources._15Chile;
                                Sqlrdr = Sqlcmd.ExecuteReader();
                                if (Sqlrdr.Read())
                                {
                                    textBoxQuestion.Text = (Sqlrdr["Question"].ToString());
                                }
                                a1 = "Russia";
                                a2 = "Norway";
                                a3 = "Chile";
                                a4 = "Peru";
                            }
                            else if (this.quizMode == "History")
                            {
                                pictureBoxImages.Image = Properties.Resources._15John_Wilkes_Booth;
                                Sqlrdr = Sqlcmd.ExecuteReader();
                                if (Sqlrdr.Read())
                                {
                                    textBoxQuestion.Text = (Sqlrdr["Question"].ToString());
                                }
                                a1 = "Lee Harvey Oswald";
                                a2 = "Marcus Julius Brutus";
                                a3 = "John Wilkes Booth";
                                a4 = "Felix Yusupov";
                            }
                            else if (this.quizMode == "Sports")
                            {
                                pictureBoxImages.Image = Properties.Resources._15Snowboarding;
                                Sqlrdr = Sqlcmd.ExecuteReader();
                                if (Sqlrdr.Read())
                                {
                                    textBoxQuestion.Text = (Sqlrdr["Question"].ToString());
                                }
                                a1 = "Snowboards";
                                a2 = "Skiboards";
                                a3 = "Snowboarding";
                                a4 = "Skiboarding";
                            }
                            else if (this.quizMode == "Games")
                            {
                                pictureBoxImages.Image = Properties.Resources._15Hearthstone;
                                Sqlrdr = Sqlcmd.ExecuteReader();
                                if (Sqlrdr.Read())
                                {
                                    textBoxQuestion.Text = (Sqlrdr["Question"].ToString());
                                }
                                a1 = "HeatStone";
                                a2 = "HeathStone";
                                a3 = "Hearthstone";
                                a4 = "Street Fighter";
                            }

                                button1.Text = a1;
                                button2.Text = a2;
                                button3.Text = a3;
                                button4.Text = a4;

                            rightAns = 3;
                            Sqlrdr.Close();
                        }
                        ctr++;
                        isOkay = true;
                        break;
                    case 16: //set of row 16 questions from diff categ.
                        {
                            Sqlcmd = new MySqlCommand(sqlSelectQuery16, Sqlconn);
                            if (this.quizMode == "Random Quiz")
                            {
                                pictureBoxImages.Image = Properties.Resources._16__Mobile_Legends;
                                Sqlrdr = Sqlcmd.ExecuteReader();
                                if (Sqlrdr.Read())
                                {
                                    textBoxQuestion.Text = (Sqlrdr["Question"].ToString());
                                }
                                a1 = "Mobile Apex";
                                a2 = "Mobile Legends";
                                a3 = "League of Legends";
                                a4 = "League of Tetris";
                            }
                            else if (this.quizMode == "Animated Films")
                            {
                                pictureBoxImages.Image = Properties.Resources._16The_Little_Mermaid;
                                Sqlrdr = Sqlcmd.ExecuteReader();
                                if (Sqlrdr.Read())
                                {
                                    textBoxQuestion.Text = (Sqlrdr["Question"].ToString());
                                }
                                a1 = "The Little Merman";
                                a2 = "The Little Mermaid";
                                a3 = "The Little Pony";
                                a4 = "The Little Mermade";
                            }
                            else if (this.quizMode == "Geography")
                            {
                                pictureBoxImages.Image = Properties.Resources._16Canada;
                                Sqlrdr = Sqlcmd.ExecuteReader();
                                if (Sqlrdr.Read())
                                {
                                    textBoxQuestion.Text = (Sqlrdr["Question"].ToString());
                                }
                                a1 = "Indonesia";
                                a2 = "Canada";
                                a3 = "Russia";
                                a4 = "Norway";
                            }
                            else if (this.quizMode == "History")
                            {
                                pictureBoxImages.Image = Properties.Resources._16LapuLapu;
                                Sqlrdr = Sqlcmd.ExecuteReader();
                                if (Sqlrdr.Read())
                                {
                                    textBoxQuestion.Text = (Sqlrdr["Question"].ToString());
                                }
                                a1 = "Jose Rizal";
                                a2 = "Lapu-Lapu";
                                a3 = "Andres Bonifacio";
                                a4 = "Emilio Aguinaldo";
                            }
                            else if (this.quizMode == "Sports")
                            {
                                pictureBoxImages.Image = Properties.Resources._16Silver_medals;
                                Sqlrdr = Sqlcmd.ExecuteReader();
                                if (Sqlrdr.Read())
                                {
                                    textBoxQuestion.Text = (Sqlrdr["Question"].ToString());
                                }
                                a1 = "Stainless Steel Medal";
                                a2 = "Silver medals";
                                a3 = "Iron medals";
                                a4 = "None Medals Given";
                            }
                            else if (this.quizMode == "Games")
                            {
                                pictureBoxImages.Image = Properties.Resources._16Among_Us;
                                Sqlrdr = Sqlcmd.ExecuteReader();
                                if (Sqlrdr.Read())
                                {
                                    textBoxQuestion.Text = (Sqlrdr["Question"].ToString());
                                }
                                a1 = "Among Them";
                                a2 = "Among Us";
                                a3 = "Among U";
                                a4 = "Among Jeff";
                            }

                                button1.Text = a1;
                                button2.Text = a2;
                                button3.Text = a3;
                                button4.Text = a4;

                            rightAns = 2;
                            Sqlrdr.Close();
                        }
                        ctr++;
                        isOkay = true;
                        break;
                    case 17: //set of row 17 questions from diff categ.
                        {
                            Sqlcmd = new MySqlCommand(sqlSelectQuery17, Sqlconn);
                            if (this.quizMode == "Random Quiz")
                            {
                                pictureBoxImages.Image = Properties.Resources._17__Tetris;
                                Sqlrdr = Sqlcmd.ExecuteReader();
                                if (Sqlrdr.Read())
                                {
                                    textBoxQuestion.Text = (Sqlrdr["Question"].ToString());
                                }
                                a1 = "Pokemon Unite";
                                a2 = "Talking Tom";
                                a3 = "Tetris";
                                a4 = "League of Tetris";
                            }
                            else if (this.quizMode == "Animated Films")
                            {
                                pictureBoxImages.Image = Properties.Resources._17Lion_King;
                                Sqlrdr = Sqlcmd.ExecuteReader();
                                if (Sqlrdr.Read())
                                {
                                    textBoxQuestion.Text = (Sqlrdr["Question"].ToString());
                                }
                                a1 = "Lion Queen";
                                a2 = "Cat King";
                                a3 = "Lion King";
                                a4 = "Dog King";
                            }
                            else if (this.quizMode == "Geography")
                            {
                                pictureBoxImages.Image = Properties.Resources._17Brazil;
                                Sqlrdr = Sqlcmd.ExecuteReader();
                                if (Sqlrdr.Read())
                                {
                                    textBoxQuestion.Text = (Sqlrdr["Question"].ToString());
                                }
                                a1 = "Chile";
                                a2 = "Peru";
                                a3 = "Brazil";
                                a4 = "Ecuador";
                            }
                            else if (this.quizMode == "History")
                            {
                                pictureBoxImages.Image = Properties.Resources._17Crusade;
                                Sqlrdr = Sqlcmd.ExecuteReader();
                                if (Sqlrdr.Read())
                                {
                                    textBoxQuestion.Text = (Sqlrdr["Question"].ToString());
                                }
                                a1 = "Krushade";
                                a2 = "Crowsade";
                                a3 = "Crusade";
                                a4 = "Crushade";
                            }
                            else if (this.quizMode == "Sports")
                            {
                                pictureBoxImages.Image = Properties.Resources._17Green_Bay_Packers;
                                Sqlrdr = Sqlcmd.ExecuteReader();
                                if (Sqlrdr.Read())
                                {
                                    textBoxQuestion.Text = (Sqlrdr["Question"].ToString());
                                }
                                a1 = "Green Pea Packers";
                                a2 = "Green Bay Pacers";
                                a3 = "Green Bay Packers";
                                a4 = "Green Pea Pacers";
                            }
                            else if (this.quizMode == "Games")
                            {
                                pictureBoxImages.Image = Properties.Resources._17Civilization_VI;
                                Sqlrdr = Sqlcmd.ExecuteReader();
                                if (Sqlrdr.Read())
                                {
                                    textBoxQuestion.Text = (Sqlrdr["Question"].ToString());
                                }
                                a1 = "Dota 2";
                                a2 = "Albeon Online";
                                a3 = "Civilization VI";
                                a4 = "Dota 1";
                            }

                                button1.Text = a1;
                                button2.Text = a2;
                                button3.Text = a3;
                                button4.Text = a4;

                            rightAns = 3;
                            Sqlrdr.Close();
                        }
                        ctr++;
                        isOkay = true;
                        break;
                    case 18: //set of row 18 questions from diff categ.
                        {
                            Sqlcmd = new MySqlCommand(sqlSelectQuery18, Sqlconn);
                            if (this.quizMode == "Random Quiz")
                            {
                                pictureBoxImages.Image = Properties.Resources._18__Candy_Crush;
                                Sqlrdr = Sqlcmd.ExecuteReader();
                                if (Sqlrdr.Read())
                                {
                                    textBoxQuestion.Text = (Sqlrdr["Question"].ToString());
                                }
                                a1 = "Crush Candy";
                                a2 = "Candy Crush";
                                a3 = "Sweet Crush Candy";
                                a4 = "Mobile Candy";
                            }
                            else if (this.quizMode == "Animated Films")
                            {
                                pictureBoxImages.Image = Properties.Resources._18yellow;
                                Sqlrdr = Sqlcmd.ExecuteReader();
                                if (Sqlrdr.Read())
                                {
                                    textBoxQuestion.Text = (Sqlrdr["Question"].ToString());
                                }
                                a1 = "Red";
                                a2 = "Yellow";
                                a3 = "Purple";
                                a4 = "Yeelow";
                            }
                            else if (this.quizMode == "Geography")
                            {
                                pictureBoxImages.Image = Properties.Resources._18Antartica;
                                Sqlrdr = Sqlcmd.ExecuteReader();
                                if (Sqlrdr.Read())
                                {
                                    textBoxQuestion.Text = (Sqlrdr["Question"].ToString());
                                }
                                a1 = "Asia";
                                a2 = "Antartica";
                                a3 = "Australia";
                                a4 = "Africa";
                            }
                            else if (this.quizMode == "History")
                            {
                                pictureBoxImages.Image = Properties.Resources._18AkkadianEmpire;
                                Sqlrdr = Sqlcmd.ExecuteReader();
                                if (Sqlrdr.Read())
                                {
                                    textBoxQuestion.Text = (Sqlrdr["Question"].ToString());
                                }
                                a1 = "Mongol Empire";
                                a2 = "Akkadia Empire";
                                a3 = "Spanish Empire";
                                a4 = "Achaemenid Empire";
                            }
                            else if (this.quizMode == "Sports")
                            {
                                pictureBoxImages.Image = Properties.Resources._18The_New_York_Knicks;
                                Sqlrdr = Sqlcmd.ExecuteReader();
                                if (Sqlrdr.Read())
                                {
                                    textBoxQuestion.Text = (Sqlrdr["Question"].ToString());
                                }
                                a1 = "The New York Knights";
                                a2 = "The New York Knicks";
                                a3 = "The New York Keccks";
                                a4 = "The New York Kings";
                            }
                            else if (this.quizMode == "Games")
                            {
                                pictureBoxImages.Image = Properties.Resources._18Florence;
                                Sqlrdr = Sqlcmd.ExecuteReader();
                                if (Sqlrdr.Read())
                                {
                                    textBoxQuestion.Text = (Sqlrdr["Question"].ToString());
                                }
                                a1 = "Joemar";
                                a2 = "Florence";
                                a3 = "Shielo";
                                a4 = "Maureen";
                            }

                            button1.Text = a1;
                                button2.Text = a2;
                                button3.Text = a3;
                                button4.Text = a4;

                            rightAns = 2;
                            Sqlrdr.Close();
                        }
                        ctr++;
                        isOkay = true;
                        break;
                    case 19: //set of row 19 questions from diff categ.
                        {
                            Sqlcmd = new MySqlCommand(sqlSelectQuery19, Sqlconn);
                            if (this.quizMode == "Random Quiz")
                            {
                                pictureBoxImages.Image = Properties.Resources._19__Minecraft;
                                Sqlrdr = Sqlcmd.ExecuteReader();
                                if (Sqlrdr.Read())
                                {
                                    textBoxQuestion.Text = (Sqlrdr["Question"].ToString());
                                }
                                a1 = "MinersCraft";
                                a2 = "MineCart";
                                a3 = "Minecraft";
                                a4 = "None of The Choices";
                            }
                            else if (this.quizMode == "Animated Films")
                            {
                                pictureBoxImages.Image= Properties.Resources._19Simba;
                                Sqlrdr = Sqlcmd.ExecuteReader();
                                if (Sqlrdr.Read())
                                {
                                    textBoxQuestion.Text = (Sqlrdr["Question"].ToString());
                                }
                                a1 = "Mufasa";
                                a2 = "Nemo";
                                a3 = "Simba";
                                a4 = "Mulan";
                            }
                            else if (this.quizMode == "Geography")
                            {
                                pictureBoxImages.Image = Properties.Resources._19Sahara;
                                Sqlrdr = Sqlcmd.ExecuteReader();
                                if (Sqlrdr.Read())
                                {
                                    textBoxQuestion.Text = (Sqlrdr["Question"].ToString());
                                }
                                a1 = "Sohora Desert";
                                a2 = "Gobi Desert";
                                a3 = "Sahara";
                                a4 = "Rub'al Khali";
                            }
                            else if (this.quizMode == "History")
                            {
                                pictureBoxImages.Image = Properties.Resources._19Antonio_Luna;
                                Sqlrdr = Sqlcmd.ExecuteReader();
                                if (Sqlrdr.Read())
                                {
                                    textBoxQuestion.Text = (Sqlrdr["Question"].ToString());
                                }
                                a1 = "Antonio del Pilar";
                                a2 = "Dr. Jose Rizal";
                                a3 = "Antonio Luna";
                                a4 = "Manuel Roxas";
                            }
                            else if (this.quizMode == "Sports")
                            {
                                pictureBoxImages.Image = Properties.Resources._19Los_Angeles_Lakers;
                                Sqlrdr = Sqlcmd.ExecuteReader();
                                if (Sqlrdr.Read())
                                {
                                    textBoxQuestion.Text = (Sqlrdr["Question"].ToString());
                                }
                                a1 = "Loston Ball Celtics";
                                a2 = "Los Toronto Lakers";
                                a3 = "Los Angeles Lakers";
                                a4 = "Los Angeles Raptors";
                            }
                            else if (this.quizMode == "Games")
                            {
                                pictureBoxImages.Image = Properties.Resources._19Grindstone;
                                Sqlrdr = Sqlcmd.ExecuteReader();
                                if (Sqlrdr.Read())
                                {
                                    textBoxQuestion.Text = (Sqlrdr["Question"].ToString());
                                }
                                a1 = "GrimStone";
                                a2 = "Heartstone";
                                a3 = "Grindstone";
                                a4 = "HeatStone";
                            }

                            button1.Text = a1;
                                button2.Text = a2;
                                button3.Text = a3;
                                button4.Text = a4;

                            rightAns = 3;
                            Sqlrdr.Close();
                        }
                        ctr++;
                        isOkay = true;
                        break;
                    case 20: //set of row 20 questions from diff categ.
                        {
                            Sqlcmd = new MySqlCommand(sqlSelectQuery20, Sqlconn);
                            if (this.quizMode == "Random Quiz")
                            {
                                pictureBoxImages.Image = Properties.Resources._20__Clash_Of_Clans;
                                Sqlrdr = Sqlcmd.ExecuteReader();
                                if (Sqlrdr.Read())
                                {
                                    textBoxQuestion.Text = (Sqlrdr["Question"].ToString());
                                }
                                a1 = "Clash Of Clans ";
                                a2 = "Clash Royals";
                                a3 = "Albeon Online";
                                a4 = "Clash of Pros";
                            }
                            else if (this.quizMode == "Animated Films")
                            {
                                pictureBoxImages.Image= Properties.Resources._20Coco;
                                Sqlrdr = Sqlcmd.ExecuteReader();
                                if (Sqlrdr.Read())
                                {
                                    textBoxQuestion.Text = (Sqlrdr["Question"].ToString());
                                }
                                a1 = "Coco";
                                a2 = "Coca";
                                a3 = "Caca";
                                a4 = "Lola CoCo";
                            }
                            else if (this.quizMode == "Geography")
                            {
                                pictureBoxImages.Image = Properties.Resources._20Antartica;
                                Sqlrdr = Sqlcmd.ExecuteReader();
                                if (Sqlrdr.Read())
                                {
                                    textBoxQuestion.Text = (Sqlrdr["Question"].ToString());
                                }
                                a1 = "Antartica";
                                a2 = "North America";
                                a3 = "South America";
                                a4 = "Asia";
                            }
                            else if (this.quizMode == "History")
                            {
                                pictureBoxImages.Image = Properties.Resources._20EmilioAguinaldo;
                                Sqlrdr = Sqlcmd.ExecuteReader();
                                if (Sqlrdr.Read())
                                {
                                    textBoxQuestion.Text = (Sqlrdr["Question"].ToString());
                                }
                                a1 = "Emilio Aguinaldo";
                                a2 = "Manuel L. Quezon";
                                a3 = "Jose P. Laurel";
                                a4 = "Sergio Osmeña";
                            }
                            else if (this.quizMode == "Sports")
                            {
                                pictureBoxImages.Image = Properties.Resources._20Michael_Jordan;
                                Sqlrdr = Sqlcmd.ExecuteReader();
                                if (Sqlrdr.Read())
                                {
                                    textBoxQuestion.Text = (Sqlrdr["Question"].ToString());
                                }
                                a1 = "Michael Jordan";
                                a2 = "Michael Jackson";
                                a3 = "Lebron James";
                                a4 = "Charles Henricks";
                            }
                            else if (this.quizMode == "Games")
                            {
                                pictureBoxImages.Image = Properties.Resources._20Inside;
                                Sqlrdr = Sqlcmd.ExecuteReader();
                                if (Sqlrdr.Read())
                                {
                                    textBoxQuestion.Text = (Sqlrdr["Question"].ToString());
                                }
                                a1 = "Inside";
                                a2 = "Outside";
                                a3 = "Beside";
                                a4 = "Seaside";
                            }

                                button1.Text = a1;
                                button2.Text = a2;
                                button3.Text = a3;
                                button4.Text = a4;

                            rightAns = 1;
                            Sqlrdr.Close();
                        }
                        ctr++;
                        isOkay = true;
                        break;
                }
            }while (!isOkay);
            if (this.ctr == 11)
            {
                Pscore = this.score;
                PlayerScore PS = new PlayerScore();
                PS.Show();
                
                Button [] btns = {button1, button2, button3, button4};
                    for (int i=0; i<btns.Length; i++)
                    {
                        btns[i].Enabled = false;
                    }
                this.gameTimer.Stop();
                Sqlconn = new MySqlConnection(dataInfo);
                Sqlconn.Open();
                string command = $"INSERT INTO TopPlayers(Player_Name, Player_Score, Category) VALUES('{this.PlayerNamelbl.Text.ToString()}', {this.score}, '{this.quizMode.ToString()}')";
                this.Sqlcmd.CommandText = command;
                this.Sqlcmd.ExecuteNonQuery();
            }
        }

        private void answerCheckEvent(object sender, EventArgs e)
        {
            var objSender = (Button)sender;
            int buttonTag = Convert.ToInt32(objSender.Tag);

            if (buttonTag == rightAns)
            {
                this.score++;
            }
            mySQLDataQuestions();
            return;
        }

        private void labelTimer_Click(object sender, EventArgs e)
        {

        }

        private void gameTimerEvent(object sender, EventArgs e)
        {
            this.counter++;
            labelTimer.Text = Convert.ToString(this.counter); 

        }
        private void PlayerNametxtBox_TextChanged(object sender, EventArgs e)
        {

        }

        public void startAndHideMenu() //starts game and hides the game menu Method
        {
            Game_menu.Hide();
            gameTimer.Start();
            gameTimer.Interval = 1000;
            this.ctr = 0;
            this.score = 0;
            this.rndNum = new randomNumbers();

            Button[] btns = { button1, button2, button3, button4 };
            for (int i = 0; i < btns.Length; i++)
            {
                btns[i].Enabled = true;
            }
        }

        private void buttonRandom_Click(object sender, EventArgs e) //Random Category
        {
            this.quizMode = "Random Quiz";
            startAndHideMenu();
            mySQLDataQuestions();
            textBoxCategory.Text = this.quizMode.ToUpper();
            textBoxCategory.ForeColor = Color.White;
        }

        private void buttonAnimFilm_Click(object sender, EventArgs e) //Animation Film Category
        {
            this.quizMode = "Animated Films";
            startAndHideMenu();
            mySQLDataQuestions();
            textBoxCategory.Text = this.quizMode.ToUpper();
            textBoxCategory.ForeColor = Color.Green;
        }

        private void buttonGeography_Click(object sender, EventArgs e) //Geography category
        {
            this.quizMode = "Geography";
            startAndHideMenu();
            mySQLDataQuestions();
            textBoxCategory.Text = this.quizMode.ToUpper();
            textBoxCategory.ForeColor = Color.Brown;
        }

        private void buttonHistory_Click(object sender, EventArgs e) //History category
        {
            this.quizMode = "History";
            startAndHideMenu();
            mySQLDataQuestions();
            textBoxCategory.Text = this.quizMode.ToUpper();
            textBoxCategory.ForeColor = Color.White;
        }

        private void moveForm1_MouseDown(object sender, MouseEventArgs e)
        {
            mousePressed = true;
        }


        private void buttonSports_Click(object sender, EventArgs e) //Sports category
        {
            this.quizMode = "Sports";
            startAndHideMenu();
            mySQLDataQuestions();
            textBoxCategory.Text = this.quizMode.ToUpper();
            textBoxCategory.ForeColor= Color.Red;
        }

        private void buttonCpNPcGames_Click(object sender, EventArgs e) //Computer and Molibe Games category
        {
            this.quizMode = "Games";
            startAndHideMenu();
            mySQLDataQuestions();
            textBoxCategory.Text= this.quizMode.ToUpper();
            textBoxCategory.ForeColor = Color.AliceBlue;
        }

        private void moveForm1_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.mousePressed == true)
            {
                mouseX = MousePosition.X - 500;
                mouseY = MousePosition.Y - 10;

                this.SetDesktopLocation(mouseX, mouseY);
            }
        }
        private void moveForm1_MouseUp(object sender, MouseEventArgs e)
        {
            this.mousePressed = false;
        }

        private void pictureBoxImages_Click(object sender, EventArgs e)
        {

        }
    }

}