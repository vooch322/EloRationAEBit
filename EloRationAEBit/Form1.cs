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
            double ratio1, ratio2;
            int score1, score2;
            ratio1 = Convert.ToInt32(textBox1.Text);
            ratio2 = Convert.ToInt32(textBox2.Text);
            score1 = Convert.ToInt32(textBox3.Text);
            score2 = Convert.ToInt32(textBox4.Text);
            if (score1 > score2)
            {
                RatioAfter1.Text = Convert.ToString(ELO(ratio1,ratio2,1,50));
                RatioAfter2.Text = Convert.ToString(ELO(ratio2, ratio1, 0,50));
            }
            if (score1 < score2)
            {
                RatioAfter1.Text = Convert.ToString(ELO(ratio1, ratio2, 0,50));
                RatioAfter2.Text = Convert.ToString(ELO(ratio2, ratio1, 1,50));
            }
            if(score1 == score2)
            {
                RatioAfter1.Text = Convert.ToString(ELO(ratio1, ratio2, 0.5,50));
                RatioAfter2.Text = Convert.ToString(ELO(ratio2, ratio1, 0.5,50));
            }
        }
        private double ELO(double ratio1,double ratio2,double Sa,double k)
        {
            double E = 1 / (1 + Math.Pow(10, (ratio2 - ratio1)/400));
            return Math.Round(ratio1 + k * (Sa - E));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            double ratio1, ratio2;
            int score1, score2;
            ratio1 = Convert.ToInt32(textBox1.Text);
            ratio2 = Convert.ToInt32(textBox2.Text);
            score1 = Convert.ToInt32(textBox3.Text);
            score2 = Convert.ToInt32(textBox4.Text);
            if (score1 > score2)
            {
                RatioAfter1.Text = Convert.ToString(ELO(ratio1, ratio2, 1,20));
                RatioAfter2.Text = Convert.ToString(ELO(ratio2, ratio1, 0,20));
            }
            if (score1 < score2)
            {
                RatioAfter1.Text = Convert.ToString(ELO(ratio1, ratio2, 0,20));
                RatioAfter2.Text = Convert.ToString(ELO(ratio2, ratio1, 1,20));
            }
            if (score1 == score2)
            {
                RatioAfter1.Text = Convert.ToString(ELO(ratio1, ratio2, 0.5,20));
                RatioAfter2.Text = Convert.ToString(ELO(ratio2, ratio1, 0.5,20));
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            double ratio1, ratio2;
            int score1, score2;
            ratio1 = Convert.ToInt32(textBox1.Text);
            ratio2 = Convert.ToInt32(textBox2.Text);
            score1 = Convert.ToInt32(textBox3.Text);
            score2 = Convert.ToInt32(textBox4.Text);
            if (score1 > score2)
            {
                RatioAfter1.Text = Convert.ToString(ELO(ratio1, ratio2, 1,70));
                RatioAfter2.Text = Convert.ToString(ELO(ratio2, ratio1, 0,70));
            }
            if (score1 < score2)
            {
                RatioAfter1.Text = Convert.ToString(ELO(ratio1, ratio2, 0,70));
                RatioAfter2.Text = Convert.ToString(ELO(ratio2, ratio1, 1,70));
            }
            if (score1 == score2)
            {
                RatioAfter1.Text = Convert.ToString(ELO(ratio1, ratio2, 0.5,70));
                RatioAfter2.Text = Convert.ToString(ELO(ratio2, ratio1, 0.5,70));
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            double ratio1, ratio2;
            int score1, score2;
            ratio1 = Convert.ToInt32(textBox1.Text);
            ratio2 = Convert.ToInt32(textBox2.Text);
            score1 = Convert.ToInt32(textBox3.Text);
            score2 = Convert.ToInt32(textBox4.Text);
            if (score1 > score2)
            {
                RatioAfter1.Text = Convert.ToString(ELO(ratio1, ratio2, 1,40));
                RatioAfter2.Text = Convert.ToString(ELO(ratio2, ratio1, 0,40));
            }
            if (score1 < score2)
            {
                RatioAfter1.Text = Convert.ToString(ELO(ratio1, ratio2, 0,40));
                RatioAfter2.Text = Convert.ToString(ELO(ratio2, ratio1, 1,40));
            }
            if (score1 == score2)
            {
                RatioAfter1.Text = Convert.ToString(ELO(ratio1, ratio2, 0.5,40));
                RatioAfter2.Text = Convert.ToString(ELO(ratio2, ratio1, 0.5,40));
            }
        }
    }
}
