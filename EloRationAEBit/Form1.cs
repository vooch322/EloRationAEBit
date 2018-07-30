using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Data.Common;

namespace EloRationAEBit
{
    public partial class Form1 : Form
    {
        private SQLiteConnection db;
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
                WinnerJoinDb(comboBox2.Text,score1,score2,EloFutebol(ratio1,ratio2,1,k,score1,score2));
                LoseJoinDb(comboBox1.Text, score2, score1, EloFutebol(ratio2, ratio1, 0, k, score1, score2));
            }
            if (score1 < score2)
            {
                RatioAfter1.Text = "Рейтинг после = " + Convert.ToString(EloFutebol(ratio1, ratio2, 0, k,score1,score2));
                RatioAfter2.Text = "Рейтинг после = " + Convert.ToString(EloFutebol(ratio2, ratio1, 1, k,score1,score2));
                WinnerJoinDb(comboBox1.Text, score2, score1, EloFutebol(ratio1, ratio2, 1, k, score1, score2));
                LoseJoinDb(comboBox2.Text, score1, score2, EloFutebol(ratio2, ratio1, 0, k, score1, score2));
            }
            if (score1 == score2)
            {
                RatioAfter1.Text = "Рейтинг после = " + Convert.ToString(EloFutebol(ratio1, ratio2, 0.5, k,score1,score2));
                RatioAfter2.Text = "Рейтинг после = " + Convert.ToString(EloFutebol(ratio2, ratio1, 0.5, k,score1,score2));
                DrawJoinDb(comboBox2.Text, score1, score2, EloFutebol(ratio1, ratio2, 0.5, k, score1, score2));
                DrawJoinDb(comboBox1.Text, score2, score1, EloFutebol(ratio2, ratio1, 0.5, k, score1, score2));
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

        private void button6_Click(object sender, EventArgs e)
        {
            EloClickFutebol(80);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            db = new SQLiteConnection("Data Source =EloRatio.db; Version=3");
            db.Open();
            DataTable dtable = new DataTable();
            String  sqlQuery="Select * from players";
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(sqlQuery, db);
            adapter.Fill(dtable);
            if (dtable.Rows.Count > 0)
            {
                dataGridView1.Rows.Clear();
                for (int i = 0; i < dtable.Rows.Count; i++)
                    dataGridView1.Rows.Add(dtable.Rows[i].ItemArray);
            }
            DbDataReader reader = new SQLiteCommand(sqlQuery,db).ExecuteReader();
            while (reader.Read())
            {
                comboBox1.Items.Add((string)reader["name"]);
                comboBox2.Items.Add((string)reader["name"]);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            db.Close();
        }

        private void comboBox2_TextChanged(object sender, EventArgs e)
        {
            String sqlquery = "select points from players where name=\"" + comboBox2.Text+"\"";
            DbDataReader read = new SQLiteCommand(sqlquery, db).ExecuteReader();
            while (read.Read())
                textBox1.Text = (string)read["points"].ToString();
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            String sqlquery = "select points from players where name=\"" + comboBox1.Text + "\"";
            DbDataReader read = new SQLiteCommand(sqlquery, db).ExecuteReader();
            while (read.Read())
                textBox2.Text = (string)read["points"].ToString();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            DataTable dtable = new DataTable();
            String sqlQuery = "Select * from players";
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(sqlQuery, db);
            adapter.Fill(dtable);
            if (dtable.Rows.Count > 0)
            {
                dataGridView1.Rows.Clear();
                for (int i = 0; i < dtable.Rows.Count; i++)
                    dataGridView1.Rows.Add(dtable.Rows[i].ItemArray);
            }
        }
        private void WinnerJoinDb(String name,int score1,int score2,double elo)
        {
            String SqlQuery = "update players set points=" + elo.ToString() + " ,win=win+1, gs=gs+"+score1.ToString()+" ,gm=gm+"+score2.ToString()+" where name=\""+name+"\"";
            SQLiteCommand cmd = db.CreateCommand();
            cmd.CommandText = SqlQuery;
            cmd.ExecuteNonQuery();
            Console.Write("SQL");
        }
        private void DrawJoinDb(String name, int score1, int score2, double elo)
        {
            String SqlQuery = "update players set points=" + elo.ToString() + " ,draw=draw+1, gs=gs+" + score1.ToString() + " ,gm=gm+" + score2.ToString() + " where name=\"" + name + "\"";
            SQLiteCommand cmd = db.CreateCommand();
            cmd.CommandText = SqlQuery;
            cmd.ExecuteNonQuery();
            Console.Write("SQL");
        }
        private void LoseJoinDb(String name, int score1, int score2, double elo)
        {
            String SqlQuery = "update players set points=" + elo.ToString() + " ,looses=looses+1 ,gs=gs+" + score1.ToString() + ", gm=gm+" + score2.ToString() + " where name=\"" + name + "\"";
            SQLiteCommand cmd = db.CreateCommand();
            cmd.CommandText = SqlQuery;
            cmd.ExecuteNonQuery();
        }
    }
}
