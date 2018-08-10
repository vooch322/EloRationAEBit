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
using Telegram.Bot.Args;
using System.Diagnostics;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types;
using EloRationAEBit.Models;
using System.IO;
using Telegram.Bot.Types.ReplyMarkups;

namespace EloRationAEBit
{
    public partial class Form1 : Form
    {

        public static readonly TelegramBotClient Bot = new TelegramBotClient("664976586:AAF6swbfPU194idaQhK6m5zUlDrd94opUlM");
        public SQLiteConnection db;
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
            ratio1 = Convert.ToInt32(player1Points.Text);
            ratio2 = Convert.ToInt32(player2Points.Text);
            score1 = Convert.ToInt32(player1Goals.Text);
            score2 = Convert.ToInt32(player2Goals.Text);
            if (score1 > score2)
            {
                RatioAfter1.Text = "Рейтинг после = " + Convert.ToString(ELO(ratio1, ratio2, 1, k));
                RatioAfter2.Text = "Рейтинг после = " + Convert.ToString(ELO(ratio2, ratio1, 0, k));
            }
            if (score1 < score2)
            {
                RatioAfter1.Text = "Рейтинг после = " + Convert.ToString(ELO(ratio1, ratio2, 0, k));
                RatioAfter2.Text = "Рейтинг после = " + Convert.ToString(ELO(ratio2, ratio1, 1, k));
            }
            if (score1 == score2)
            {
                RatioAfter1.Text = "Рейтинг после = " + Convert.ToString(ELO(ratio1, ratio2, 0.5, k));
                RatioAfter2.Text = "Рейтинг после = " + Convert.ToString(ELO(ratio2, ratio1, 0.5, k));
            }
        }
        private void EloClickFutebol(double k)
        {
            double ratio1, ratio2;
            int score1, score2;
            ratio1 = Convert.ToInt32(player1Points.Text);
            ratio2 = Convert.ToInt32(player2Points.Text);
            score1 = Convert.ToInt32(player1Goals.Text);
            score2 = Convert.ToInt32(player2Goals.Text);
            if (score1 > score2)
            {
                RatioAfter1.Text = "Рейтинг после = " + Convert.ToString(EloFutebol(ratio1, ratio2, 1, k, score1, score2));
                RatioAfter2.Text = "Рейтинг после = " + Convert.ToString(EloFutebol(ratio2, ratio1, 0, k, score1, score2));
                WinnerJoinDb(player1.Text, score1, score2, EloFutebol(ratio1, ratio2, 1, k, score1, score2));
                LoseJoinDb(player2.Text, score2, score1, EloFutebol(ratio2, ratio1, 0, k, score1, score2));
            }
            if (score1 < score2)
            {
                RatioAfter1.Text = "Рейтинг после = " + Convert.ToString(EloFutebol(ratio1, ratio2, 0, k, score1, score2));
                RatioAfter2.Text = "Рейтинг после = " + Convert.ToString(EloFutebol(ratio2, ratio1, 1, k, score1, score2));
                WinnerJoinDb(player2.Text, score2, score1, EloFutebol(ratio2, ratio1, 1, k, score1, score2));
                LoseJoinDb(player1.Text, score1, score2, EloFutebol(ratio1, ratio2, 0, k, score1, score2));
            }
            if (score1 == score2)
            {
                RatioAfter1.Text = "Рейтинг после = " + Convert.ToString(EloFutebol(ratio1, ratio2, 0.5, k, score1, score2));
                RatioAfter2.Text = "Рейтинг после = " + Convert.ToString(EloFutebol(ratio2, ratio1, 0.5, k, score1, score2));
                DrawJoinDb(player1.Text, score1, score2, EloFutebol(ratio1, ratio2, 0.5, k, score1, score2));
                DrawJoinDb(player2.Text, score2, score1, EloFutebol(ratio2, ratio1, 0.5, k, score1, score2));
            }
        }
        private double ELO(double ratio1, double ratio2, double Sa, double k)
        {
            double E = 1 / (1 + Math.Pow(10, (ratio2 - ratio1) / 400));
            Console.WriteLine(Convert.ToString(E));
            return Math.Round(ratio1 + k * (Sa - E));
        }
        private double EloFutebol(double ratio1, double ratio2, double Sa, double k, double score1, double score2)
        {
            double g = Math.Abs(score1 - score2);
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
            return Math.Round(ratio1 + k * g * (Sa - E));
        }

        private void button5_Click(object sender, EventArgs e)
        {
            EloClickFutebol(50);

        }

        private void button6_Click(object sender, EventArgs e)
        {
            EloClickFutebol(40);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            db = new SQLiteConnection("Data Source =EloRatio.db; Version=3");
            db.Open();
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
            DbDataReader reader = new SQLiteCommand(sqlQuery, db).ExecuteReader();
            while (reader.Read())
            {
                player2.Items.Add((string)reader["name"]);
                player1.Items.Add((string)reader["name"]);
            }

            StartFifaBot();
        }

        private DataTable GetDataTable() {
            db = new SQLiteConnection("Data Source =EloRatio.db; Version=3");
            db.Open();
            DataTable dtable = new DataTable();
            String sqlQuery = "Select * from players";
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(sqlQuery, db);
            adapter.Fill(dtable);
            return dtable;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            db.Close();
        }

        private void comboBox2_TextChanged(object sender, EventArgs e)
        {
            String sqlquery = "select points from players where name=\"" + player1.Text+"\"";
            DbDataReader read = new SQLiteCommand(sqlquery, db).ExecuteReader();
            while (read.Read())
                player1Points.Text = (string)read["points"].ToString();
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            String sqlquery = "select points from players where name=\"" + player2.Text + "\"";
            DbDataReader read = new SQLiteCommand(sqlquery, db).ExecuteReader();
            while (read.Read())
                player2Points.Text = (string)read["points"].ToString();
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

        private void StartFifaBot()
        { 
            var me = Bot.GetMeAsync().Result;

            Bot.OnMessage += BotOnMessageReceived;

            Bot.StartReceiving();
            // Bot.StopReceiving();
        }

        private async void BotOnMessageReceived(object sender, MessageEventArgs messageEventArgs)
        {
            var message = messageEventArgs.Message;
            var chatId = messageEventArgs.Message.Chat.Id;

            if (message == null || message.Type != MessageType.Text) return;
            else
            {
                Debug.WriteLine("message = ", message.Text);
                Debug.WriteLine(message.Chat.Id);
                /*using (StreamWriter outputFile = new StreamWriter("match.txt", true))
                {
                    
                    outputFile.WriteLine("\n" + message.Chat.Id+" "+message.Text+" "+DateTime.Now);
                    

                }*/
                switch (message.Text.Split(' ').First())
                {
                    case "/rating":
                        var dtable = GetDataTable();
                        var fifaPlayers = new List<FifaPlayer>();
                        var result = "";
                        for (int i = 0; i < dtable.Rows.Count; i++)
                        {
                            var name = dtable.Rows[i].ItemArray[1];
                            var elo = dtable.Rows[i].ItemArray[2];
                            var fifaPlayer = new FifaPlayer
                            {
                                Name = Convert.ToString(name),
                                Elo = Convert.ToInt32(elo),
                            };
                            fifaPlayers.Add(fifaPlayer);
                        }
                        var count = 1;
                        fifaPlayers = fifaPlayers.OrderByDescending(v => v.Elo).ToList();
                        foreach (var fifaPlayer in fifaPlayers)
                        {
                            result = result + $"{count} {fifaPlayer.Name} {fifaPlayer.Elo} \n";
                            count++;
                        }
                        await Bot.SendTextMessageAsync(message.Chat.Id, $"РАНК ИМЯ РЕЙТИНГ \n" + result);
                        break;
                    case "/hi":
                        await Bot.SendChatActionAsync(message.Chat.Id, ChatAction.Typing);

                        /* await Task.Delay(500); // simulate longer running task

                         var inlineKeyboard = new InlineKeyboardMarkup(new[]
                         {
                         new [] // first row
                         {
                             InlineKeyboardButton.WithCallbackData("1.1"),
                             InlineKeyboardButton.WithCallbackData("1.2"),
                         },
                         new [] // second row
                         {
                             InlineKeyboardButton.WithCallbackData("2.1"),
                             InlineKeyboardButton.WithCallbackData("2.2"),
                         }
                     });*/
                        Random rnd = new Random();

                        int[] a = new int[3] {1,2,3}, b = new int[3] {4,5,6 }, c = new int[3] { 7,9,10}, d = new int[2] {12,13};
                        int k = 0;
                        
                        string grA="", grB="", grC="";
                        
                        for (int i = a.Length-1; i >1; i--)
                        {
                            int o;
                            k = rnd.Next(0, i);
                            o = b[k];
                            b[k] = b[i];
                            b[i] = o;
                        }
                        for (int i = b.Length - 1; i > 1; i--)
                        {
                            int o;
                            k = rnd.Next(0, i);
                            o = a[k];
                            a[k] = a[i];
                            a[i] = o;
                        }
                        for (int i = c.Length - 1; i > 1; i--)
                        {
                            int o;
                            k = rnd.Next(0, i);
                            o = c[k];
                            c[k] = c[i];
                            c[i] = o;
                        }
                        for (int i = d.Length - 1; i > 1; i--)
                        {
                            int o;
                            k = rnd.Next(0, i);
                            o = d[k];
                            d[k] = d[i];
                            d[i] = o;
                        }
                        grA = a[0].ToString()+ " "+ b[0].ToString()+" " + c[0].ToString()+" " + d[0].ToString();
                        grB= a[1].ToString() + " " + b[1].ToString() + " " + c[1].ToString() + " " + d[1].ToString();
                        grC = a[2].ToString() + " " + b[2].ToString() + " " + c[2].ToString();
                        /* await Bot.SendTextMessageAsync(
                             message.Chat.Id,
                             "Choose",
                             replyMarkup: inlineKeyboard);*/
                        //break;
                        await Bot.SendTextMessageAsync(message.Chat.Id, grA +"\n"+ grB+"\n" + grC);
                        Debug.WriteLine(grA);
                        Debug.WriteLine(message.Chat.Id);
                        using (StreamWriter outputFile=new StreamWriter("telegramId.txt",true))
                        {
                            outputFile.WriteLine("\n"+message.Chat.Id);
                            
                        }
                            //await Bot.SendTextMessageAsync(150712688, "ты пидор");

                            break;
                    case "/match":
                        if (message.Text.Length != 6)
                        {
                            using (StreamWriter outputFile = new StreamWriter("match.txt", true))
                            {

                                outputFile.WriteLine("\n" + message.Chat.Id + " " + message.Text + " " + DateTime.Now);


                            }
                            await Bot.SendTextMessageAsync(message.Chat.Id, "Ваш результат записан");
                            await Bot.SendTextMessageAsync(120086825, "Новый матч");

                        }
                        else
                            await Bot.SendTextMessageAsync(message.Chat.Id, "Запишите результат");

                        break;
                    case "/register":
                        if (message.Text != "/register")
                        {
                            var str="";
                            str=message.Text.Replace("/register ", "");
                            Debug.WriteLine(message.Chat.Id + "" + str);
                        }
                        break;
                }
            }
        }
    }
}
