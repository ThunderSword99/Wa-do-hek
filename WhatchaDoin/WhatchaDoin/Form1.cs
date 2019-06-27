﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace WhatchaDoin
{

    public partial class Form1 : Form
    {
        private string text3 = "- Số chỉ tiêu: ";
        private string text4 = "- Chỉ tiêu đã hoàn thành: ";
        private string text5 = "- Chỉ tiêu chưa hoàn thành: ";
        private string text6 = "- Thời gian bắt đầu: ";
        private string text7 = "- Thời gian còn lại: ";
        private string text8 = "- Điểm ngày hôm nay: ";
        private string text9 = "- Số nhiệm vụ đã bỏ: ";
        private string text10 = "- Số ngày nghỉ ngơi: ";
        private string text11 = "- Điểm tuyệt đối liên tiếp: ";
        private string text12 = "- Tổng điểm: ";

        private int targets = 0; // Số chỉ tiêu đề ra
        private int completeTargets = 0;// Số chỉ tiêu đã hoàn thành 
        private int uncompleteTargets = 0;// Số chỉ tiêu chưa hoàn thành

        
        private string timeBeginning = DateTime.Now.ToString("h:mm tt");// Thời gian bắt đầu thực hiện
        private TimeSpan timeRemaining = new TimeSpan(24, 0, 0) - DateTime.Now.TimeOfDay;// Thời gian còn lại

        private int todayScore = 0;// Điểm ngày hôm nay 

        private int totalUncompleteTargets = 0;// Tổng các chỉ tiêu chưa hoàn thành
        private int relaxingDay = 0;// Tổng ngày nghỉ
        private int perfectScoreInARow = 0;// Tổng số ngày đạt điểm tuyệt đối liên tiếp
        private int totalScore = 0;// Tổng điểm đã làm được

        private Label label3;
        public class CustomCheckedListBox : CheckedListBox
        {

        }

        private void LoadingTodayData()
        {
            string[] lines = File.ReadAllLines(@"C:\Users\ADMIN\source\repos\WhatchaDoin\WhatchaDoin\TodayTargets.txt");
            foreach (string s in lines)
            {
                if (s[s.Length - 1] == '1')
                {
                    checkedListBox1.Items.Add(s.Substring(0, s.Length - 1), true);
                }
                else
                {
                    checkedListBox1.Items.Add(s.Substring(0, s.Length - 1), false);
                }
            }
            //Thay đổi thời gian bắt đầu
            string TBF = @"C:\Users\ADMIN\source\repos\WhatchaDoin\WhatchaDoin\TimeBeginning.txt";// Đường dẫn đến TodayData  
            string[] TimeBeginning = File.ReadAllLines(TBF);// Lưu từng dòng trong txt
            string DAT = DateTime.Now.ToString("dd/MM/yyyy");
            if (DAT != TimeBeginning[0])
            {
                timeBeginning = DateTime.Now.ToString("h:mm tt");
                FileStream fs = new FileStream("C:\\Users\\ADMIN\\source\\repos\\WhatchaDoin\\WhatchaDoin\\TimeBeginning.txt", FileMode.Open);
                StreamWriter wt = new StreamWriter(fs, Encoding.UTF8);
                wt.WriteLine(DAT);
                wt.WriteLine(timeBeginning);
                wt.Close();
            }
            else
            {
                timeBeginning = TimeBeginning[1];
            }

            string DF = @"C:\Users\ADMIN\source\repos\WhatchaDoin\WhatchaDoin\Data.txt";// Đường dẫn đến Data  
            string[] Statistic = File.ReadAllLines(DF);// Lưu từng dòng trong txt
            totalUncompleteTargets = Statistic[1][0] - '0';// Tổng chỉ tiêu chưa đạt
            relaxingDay = Statistic[2][0] - '0';// Tổng số ngày thư giãn
            perfectScoreInARow = Statistic[3][0] - '0';// Tổng số ngày làm hết liên tiếp
            totalScore = Statistic[4][0] - '0';// Tổng số điểm
            string TD = @"C:\Users\ADMIN\source\repos\WhatchaDoin\WhatchaDoin\TodayData.txt";// Đường dẫn đến Data  
            string[] TodayData = File.ReadAllLines(TD);// Lưu từng dòng trong txt
            DAT = TodayData[0];// Lưu ngày
            FileStream fx = new FileStream("C:\\Users\\ADMIN\\source\\repos\\WhatchaDoin\\WhatchaDoin\\Data.txt", FileMode.Open);
            StreamWriter w = new StreamWriter(fx, Encoding.UTF8);         
            if (DAT != Statistic[0]) //Khác ngày
            {
                checkedListBox1.Items.Clear();
            }
            fx.Flush();
            fx.Close();
        }
        private void SettingFormDefault()
        {
            this.ShowInTaskbar = false; // Ẩn không hiển thị ứng dụng dưới taskbar
            this.CenterToScreen();// Cho form ra giữa màn hình
            timer2.Start();// Chạy load form liên tục
            //Đặt các thông số
            circularProgressBar1.Value = 0;// Đặt mức làm nhiệm vụ = 0
            label3.Text = text3 + targets.ToString();
            label4.Text = text4 + completeTargets.ToString();
            label5.Text = text5 + uncompleteTargets.ToString();
            label6.Text = text6 + timeBeginning;
            timer1.Start();//Update label 7
            label8.Text = text8 + todayScore.ToString();

            string DF = @"C:\Users\ADMIN\source\repos\WhatchaDoin\WhatchaDoin\Data.txt";// Đường dẫn đến Data  
            string[] Statistic = File.ReadAllLines(DF);// Lưu từng dòng trong txt
            totalUncompleteTargets = Statistic[1][0] - '0';
            relaxingDay = Statistic[2][0] - '0';
            perfectScoreInARow = Statistic[3][0] - '0';
            totalScore = Statistic[4][0] - '0';
            label9.Text = text9 + totalUncompleteTargets.ToString();
            label10.Text = text10 + relaxingDay.ToString();
            label11.Text = text11 + perfectScoreInARow.ToString();
            label12.Text = text12 + totalScore.ToString();


        }

        public Form1()
        {
            InitializeComponent();
            LoadingTodayData();//Load giữ liệu đã lưu ngày hôm nay
            SettingFormDefault();//Cài đặt mặc định
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                textBox1.Text = textBox1.Text.Substring(0, 1).ToString().ToUpper() + textBox1.Text.Substring(1).ToString();
                checkedListBox1.Items.Add(textBox1.Text);
                textBox1.Text = null;
            }

        }

        private void SaveStatistic()
        {
            string DF = @"C:\Users\ADMIN\source\repos\WhatchaDoin\WhatchaDoin\Data.txt";// Đường dẫn đến Data  
            string[] Statistic = File.ReadAllLines(DF);// Lưu từng dòng trong txt
            totalUncompleteTargets = Statistic[1][0] - '0';// Tổng chỉ tiêu chưa đạt
            relaxingDay = Statistic[2][0]- '0';// Tổng số ngày thư giãn
            perfectScoreInARow = Statistic[3][0]-'0';// Tổng số ngày làm hết liên tiếp
            totalScore = Statistic[4][0]-'0';// Tổng số điểm
            string TD = @"C:\Users\ADMIN\source\repos\WhatchaDoin\WhatchaDoin\TodayData.txt";// Đường dẫn đến Data  
            string[] TodayData = File.ReadAllLines(TD);// Lưu từng dòng trong txt
            string DAT = TodayData[0];// Lưu ngày
            FileStream fs = new FileStream("C:\\Users\\ADMIN\\source\\repos\\WhatchaDoin\\WhatchaDoin\\Data.txt", FileMode.Open);
            StreamWriter wt = new StreamWriter(fs, Encoding.UTF8);
            int cnt1 = 0; // Biến đếm số chỉ tiêu đã hoàn thành
            int cnt2 = 0; // Biến đếm số chỉ tiêu chưa hoàn thành
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                if (checkedListBox1.GetItemCheckState(i) == CheckState.Checked)
                {
                    cnt1++;
                }
                else
                {
                    cnt2++;
                }
            }
            if (DAT!=Statistic[0]) //Khác ngày
            {
                checkedListBox1.Items.Clear();
                totalUncompleteTargets += cnt2; // Cập nhật số chỉ tiêu không hoàn thành
                totalScore += cnt1; // Cập nhật tổng điểm
                wt.WriteLine(DateTime.Now.ToString("dd/MM/yyyy"));
                wt.WriteLine(totalUncompleteTargets.ToString());
                if (cnt2 == checkedListBox1.Items.Count)
                {
                    relaxingDay++;
                    wt.WriteLine(relaxingDay.ToString());
                }
                else
                {
                    wt.WriteLine(relaxingDay.ToString());
                }          
                if (cnt1 == checkedListBox1.Items.Count)
                {
                    perfectScoreInARow++;
                    wt.WriteLine(perfectScoreInARow.ToString());
                }
                else
                {
                    perfectScoreInARow = 0;
                    wt.WriteLine(perfectScoreInARow);
                }
                wt.WriteLine(totalScore);
                wt.Close();
            }
            else
            {

            }

        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (timeRemaining.TotalSeconds <= 0)
            {
                timer1.Stop();
                timer2.Stop();
            }
            else
            {
                timeRemaining = timeRemaining.Subtract(new TimeSpan(0, 0, 1));
                label7.Text = text7 + timeRemaining.ToString(@"hh\:mm\:ss");
            }
        }

        private void Timer2_Tick(object sender, EventArgs e)
        {
            targets = checkedListBox1.Items.Count;
            label3.Text = text3 + targets.ToString();
            completeTargets = checkedListBox1.CheckedItems.Count;
            label4.Text = text4 + completeTargets.ToString();
            uncompleteTargets = checkedListBox1.Items.Count - checkedListBox1.CheckedItems.Count;
            label5.Text = text5 + uncompleteTargets.ToString();
            todayScore = completeTargets;
            label8.Text = text8 + todayScore.ToString();
            if (targets != 0)
            {
                circularProgressBar1.Value = (int)(((float)completeTargets / targets) * 100);
                circularProgressBar1.Text = circularProgressBar1.Value.ToString() + "%";
            }
            else
            {
                circularProgressBar1.Value = 0;
            }


        }

        private int selectItem;// Item trong check list box được chọn
        private void CheckedListBox1_MouseDown(object sender, MouseEventArgs e)
        {
            selectItem = checkedListBox1.IndexFromPoint(e.X, e.Y);
            checkedListBox1.SelectedIndex = selectItem;
            if (e.Button == MouseButtons.Right)
            {
                selectItem = checkedListBox1.IndexFromPoint(e.Location);
                if (selectItem >= 0)
                {
                    checkedListBox1.SelectedIndex = selectItem;
                    contextMenuStrip1.Show(checkedListBox1, e.Location);

                }
            }
        }

        private void RemoveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            checkedListBox1.Items.RemoveAt(selectItem);
        }


        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            bool MousePoiterNotOnTaskBar = Screen.GetWorkingArea(this).Contains(Cursor.Position);//Chỉ 

            if (this.WindowState == FormWindowState.Minimized && MousePoiterNotOnTaskBar)
            {
                notifyIcon1.Visible = true;
                this.Hide();
            }

        }

        private void NotifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            if (this.WindowState != FormWindowState.Normal)
            {
                notifyIcon1.Visible = false;
                this.Show();
                this.WindowState = FormWindowState.Normal;
                this.CenterToScreen();
            }
        }

        private void ClearTodayDataText()
        {
            string TodayData = @"C:\Users\ADMIN\source\repos\WhatchaDoin\WhatchaDoin\TodayTargets.txt";
            File.WriteAllText(TodayData, String.Empty);
            FileStream fs = new FileStream("C:\\Users\\ADMIN\\source\\repos\\WhatchaDoin\\WhatchaDoin\\TimeBeginning.txt", FileMode.Open);
            StreamWriter wt = new StreamWriter(fs, Encoding.UTF8);
            wt.WriteLine("0");
            fs.Close();
        }

        private void SaveTodayData()
        {
            FileStream fs = new FileStream("C:\\Users\\ADMIN\\source\\repos\\WhatchaDoin\\WhatchaDoin\\TodayTargets.txt", FileMode.Open);
            StreamWriter wt = new StreamWriter(fs, Encoding.UTF8);
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                if (checkedListBox1.GetItemCheckState(i) == CheckState.Checked)
                {
                    wt.WriteLine(checkedListBox1.Items[i].ToString() + "1");
                }
                else
                {
                    wt.WriteLine(checkedListBox1.Items[i].ToString() + "0");
                }
            }
            wt.Flush();
            wt.Close();
            fs = new FileStream("C:\\Users\\ADMIN\\source\\repos\\WhatchaDoin\\WhatchaDoin\\TodayData.txt", FileMode.Open);
            wt = new StreamWriter(fs, Encoding.UTF8);
            wt.WriteLine(DateTime.Now.ToString("dd/MM/yyyy"));
            int cnt1 = 0;
            int cnt2 = 0;
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                if (checkedListBox1.GetItemCheckState(i) == CheckState.Checked)
                {
                    cnt1++;
                }
                else
                {
                    cnt2++;
                }
            }
            wt.WriteLine(cnt1);
            wt.WriteLine(cnt2);
            wt.Flush();
            wt.Close();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            ClearTodayDataText();
            SaveTodayData();
            SaveStatistic();
        }

        private void TextBox1_DragEnter(object sender, DragEventArgs e)
        {
            MessageBox.Show("ok");
        }

        private void TextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Button1_Click(this, new EventArgs());
            }
        }
    }
}
