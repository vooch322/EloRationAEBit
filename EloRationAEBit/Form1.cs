using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EloRationAEBit
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            EloClick(50);            
        }
        

        private void button2_Click(object sender, EventArgs e)
        {
            EloClick(20);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            EloClick(70);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            EloClick(40);
        }
        private void EloClick(double k)
        {
            
            double ratio1, ratio2;
            int score1, score2;
            ratio1 = Convert.ToInt32(textBox1.Text);
            ratio2 = Convert.ToInt32(textBox2.Text);
            score1 = Convert.ToInt32(textBox3.Text);
            score2 = Convert.ToInt32(textBox4.Text);
            if (score1 > score2)
            {
                RatioAfter1.Text = "Рейтинг после = "+ Convert.ToString(ELO(ratio1, ratio2, 1, k));
                RatioAfter2.Text = "Рейтинг после = "+ Convert.ToString(ELO(ratio2, ratio1, 0, k));
            }
            if (score1 < score2)
            {
                RatioAfter1.Text = "Рейтинг после = " + Convert.ToString(ELO(ratio1, ratio2, 0, k));
                RatioAfter2.Text = "Рейтинг после = " + Convert.ToString(ELO(ratio2, ratio1, 1, k));
            }
            if (score1 == score2)
            {
                RatioAfter1.Text = "Рейтинг после = " +Convert.ToString(ELO(ratio1, ratio2, 0.5, k));
                RatioAfter2.Text = "Рейтинг после = " + Convert.ToString(ELO(ratio2, ratio1, 0.5, k));
            }
        }
        private void EloClickFutebol(double k)
        {
            double ratio1, ratio2;
            int score1, score2;
            ratio1 = Convert.ToInt32(textBox1.Text);
            ratio2 = Convert.ToInt32(textBox2.Text);
            score1 = Convert.ToInt32(textBox3.Text);
            score2 = Convert.ToInt32(textBox4.Text);
            if (score1 > score2)
            {
                RatioAfter1.Text = "Рейтинг после = " + Convert.ToString(EloFutebol(ratio1, ratio2, 1, k,score1,score2));
                RatioAfter2.Text = "Рейтинг после = " + Convert.ToString(EloFutebol(ratio2, ratio1, 0, k,score1,score2));
            }
            if (score1 < score2)
            {
                RatioAfter1.Text = "Рейтинг после = " + Convert.ToString(EloFutebol(ratio1, ratio2, 0, k,score1,score2));
                RatioAfter2.Text = "Рейтинг после = " + Convert.ToString(EloFutebol(ratio2, ratio1, 1, k,score1,score2));
            }
            if (score1 == score2)
            {
                RatioAfter1.Text = "Рейтинг после = " + Convert.ToString(EloFutebol(ratio1, ratio2, 0.5, k,score1,score2));
                RatioAfter2.Text = "Рейтинг после = " + Convert.ToString(EloFutebol(ratio2, ratio1, 0.5, k,score1,score2));
            }
        }
        private double ELO(double ratio1,double ratio2,double Sa,double k)
        {
            double E = 1 / (1 + Math.Pow(10, (ratio2 - ratio1)/400));
            Console.WriteLine(Convert.ToString(E));
            return Math.Round(ratio1 + k * (Sa - E));
        }
        private double EloFutebol(double ratio1, double ratio2, double Sa, double k,double score1,double score2)
        {
            double g =Math.Abs(score1 - score2);
            if (g == 2)
            {
                g = 3 / 2;
            }
            if (g >= 3)
            {
                g = (11 + g) / 8;
            }
            if (g == 0)
            {
                g = 1;
            }
            double E = 1 / (1 + Math.Pow(10, (ratio2 - ratio1) / 400));
            return Math.Round(ratio1 + k *g* (Sa - E));
        }

        private void button5_Click(object sender, EventArgs e)
        {
            EloClickFutebol(50);
        }
    }
}
