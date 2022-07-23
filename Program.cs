using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Second_Project_attempt
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
    public class randomNumbers //Genarates random numbers for randomized questions
    {
       public int[] arrNum = new int[20];
       Random rand = new Random();
       public int temp;

        public void GenerateRandom()
        {
            for (int i = 0; i < 20; i++)
            {
                temp = rand.Next(21);
                while (IsDup(temp, arrNum))
                {
                    temp = rand.Next(21);
                }
                arrNum[i] = temp;
            }
        }

        private bool IsDup(int temp, int[] arrNum)
        {
            foreach (var item in arrNum)
            {
                if (item == temp)
                {
                    return true;
                }
            }
            return false;   
        }
    }
}