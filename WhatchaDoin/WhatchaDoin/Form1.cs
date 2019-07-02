using System;
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
        // Đặt text mặc định để hiển thị
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
        // Đặt thông số
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
        // Tự thêm
        private Label label3;
        public class CustomCheckedListBox : CheckedListBox
        {

        }

        private void LoadingTotalData()
        {
            string TotalDF = @"C:\Users\ADMIN\source\repos\WhatchaDoin\WhatchaDoin\TodayData.txt";// Đường dẫn đến TodayData
            string[] TotalData = File.ReadAllLines(TotalDF);// Lưu từng dòng trong txt
            totalUncompleteTargets = (int)TotalData[0][0];
            relaxingDay = (int)TotalData[1][0];
            perfectScoreInARow = (int)TotalData[2][0];
            totalScore = (int)TotalData[3][0];
        }
        private void SettingFormDefault()
        {
            string TodayDF = @"C:\Users\ADMIN\source\repos\WhatchaDoin\WhatchaDoin\TodayData.txt";// Đường dẫn đến TodayData
            string[] TodayData = File.ReadAllLines(TodayDF);// Lưu từng dòng trong txt
            timeBeginning = TodayData[4];
            //Đặt mặc định
            this.ShowInTaskbar = false; // Ẩn không hiển thị ứng dụng dưới taskbar
            this.CenterToScreen();// Cho form ra giữa màn hình
            timer2.Start();// Chạy load form liên tục
            //Đặt các thông số cho label mặc định
            circularProgressBar1.Value = 0;// Đặt mức làm nhiệm vụ = 0
            label3.Text = text3 + targets.ToString();
            label4.Text = text4 + completeTargets.ToString();         
            label5.Text = text5 + uncompleteTargets.ToString();
            label6.Text = text6 + timeBeginning;
            timer1.Start();//Update label 7
            label8.Text = text8 + todayScore.ToString();
            //Đặt mặc định thông số tổng
            string DF = @"C:\Users\ADMIN\source\repos\WhatchaDoin\WhatchaDoin\TotalData.txt";// Đường dẫn đến Data  
            string[] TotalData = File.ReadAllLines(DF);// Lưu từng dòng trong txt
            totalUncompleteTargets = Int32.Parse(TotalData[0].ToString());
            relaxingDay = Int32.Parse(TotalData[1].ToString());
            perfectScoreInARow = Int32.Parse(TotalData[2].ToString());
            totalScore = Int32.Parse(TotalData[3].ToString());
            label9.Text = text9 + totalUncompleteTargets.ToString();
            label10.Text = text10 + relaxingDay.ToString();
            label11.Text = text11 + perfectScoreInARow.ToString();
            label12.Text = text12 + totalScore.ToString();
        }

        private void LoadingTodayTargets()
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
        }

        private void UpdateTotalData()
        {         
            //Lấy giữ liệu tổng từ TodayData.txt lên Codesources
            string TotalDF = @"C:\Users\ADMIN\source\repos\WhatchaDoin\WhatchaDoin\TotalData.txt";// Đường dẫn đến TodayData
            string[] TotalData = File.ReadAllLines(TotalDF);// Lưu từng dòng trong txt
            //lấy dữ liệu tổng
            totalUncompleteTargets = Int32.Parse(TotalData[0].ToString()); 
            relaxingDay = Int32.Parse(TotalData[1].ToString());
            perfectScoreInARow = Int32.Parse(TotalData[2].ToString());
            totalScore = Int32.Parse(TotalData[3].ToString());
            //Lấy giữ liệu từ TodayData.txt lên Codesouces
            string TodayDF = @"C:\Users\ADMIN\source\repos\WhatchaDoin\WhatchaDoin\TodayData.txt";// Đường dẫn đến TodayData
            string[] TodayData = File.ReadAllLines(TodayDF);// Lưu từng dòng trong txt
            //Cập nhật dữ liệu tổng
            totalUncompleteTargets += Int32.Parse(TodayData[3].ToString());
            if (TodayData[2][0] == '0')
            {
                relaxingDay += 1;
            }
            else
            {
                relaxingDay = 0;
            }
            if (TodayData[2][0] == TodayData[1][0])
            {
                perfectScoreInARow += 1;
            }
            else
            {
                perfectScoreInARow = 0;
            }
            totalScore += Int32.Parse(TodayData[2].ToString());
            //Ghi lại giữ liệu tổng vào TotalData.txt
            FileStream fs = new FileStream("C:\\Users\\ADMIN\\source\\repos\\WhatchaDoin\\WhatchaDoin\\TotalData.txt", FileMode.Open);
            StreamWriter wt = new StreamWriter(fs, Encoding.UTF8);
            wt.WriteLine(totalUncompleteTargets);
            wt.WriteLine(relaxingDay);
            wt.WriteLine(perfectScoreInARow);
            wt.WriteLine(totalScore);
            wt.Flush();
            wt.Close();
        }
        
        private void ChangeTodayDataTxt()
        {
            FileStream fs = new FileStream("C:\\Users\\ADMIN\\source\\repos\\WhatchaDoin\\WhatchaDoin\\TodayData.txt", FileMode.Open);
            StreamWriter wt = new StreamWriter(fs, Encoding.UTF8);
            wt.WriteLine(DateTime.Now.ToString("dd/MM/yyyy"));
            wt.WriteLine(0);
            wt.WriteLine(0);
            wt.WriteLine(0);
            wt.WriteLine(timeBeginning);
            wt.Flush();
            wt.Close();
        }

        private void LoadingTodayData()
        {
            string TodayDF = @"C:\Users\ADMIN\source\repos\WhatchaDoin\WhatchaDoin\TodayData.txt";// Đường dẫn đến TodayData
            string[] TodayData = File.ReadAllLines(TodayDF);// Lưu từng dòng trong txt
            string TodayDAT = DateTime.Now.ToString("dd/MM/yyyy");
            if (TodayDAT != TodayData[0])// Kiểm tra xem thời gian bắt đầu là của hôm qua hay hôm nay
            {
                UpdateTotalData();
                ChangeTodayDataTxt();
                LoadingTotalData();
                checkedListBox1.Items.Clear();
            }
            else
            {
                LoadingTodayTargets();
                LoadingTotalData();
            }
        }

        public Form1()
        {
            InitializeComponent();
            LoadingTodayData();
            SettingFormDefault();
           
        }

        //Button submit
        private void Button1_Click(object sender, EventArgs e)
        {
            // Text khác rỗng thì cập nhật chỉ tiêu lên trên checkboxlist
            if (textBox1.Text != "")
            {
                textBox1.Text = textBox1.Text.Substring(0, 1).ToString().ToUpper() + textBox1.Text.Substring(1).ToString();
                checkedListBox1.Items.Add(textBox1.Text);
                textBox1.Text = null;
            }

        }
        //Đếm thời gian còn lại
        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (timeRemaining.TotalSeconds <= 10)
            {
                timer1.Stop();
                timer2.Stop();
                this.Close();
            }
            else
            {
                timeRemaining = timeRemaining.Subtract(new TimeSpan(0, 0, 1));
                label7.Text = text7 + timeRemaining.ToString(@"hh\:mm\:ss");
            }
        }
        //Load form mỗi 100 milisecond
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
                circularProgressBar1.Text = "0%";
            }


        }

        //Click chuột phải để remove chỉ tiêu
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
        //Ẩn ứng dụng dưới dạng notify và không chiểm quyền alt + tab
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

        private void SaveTodayTargets()
        {
            if (checkedListBox1.Items.Count == 0)
            {
                string path = @"C:\Users\ADMIN\source\repos\WhatchaDoin\WhatchaDoin\TodayTargets.txt";
                File.WriteAllText(path, String.Empty);
            }
            else
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
            }      
        }

        private void SaveTodayData()
        {
            //Lưu thời gian bắt đầu
            string TodayDF = @"C:\Users\ADMIN\source\repos\WhatchaDoin\WhatchaDoin\TodayData.txt";// Đường dẫn đến TodayData
            string[] TodayData = File.ReadAllLines(TodayDF);// Lưu từng dòng trong txt
            timeBeginning = TodayData[4];
            //Lưu thông số
            FileStream fs = new FileStream("C:\\Users\\ADMIN\\source\\repos\\WhatchaDoin\\WhatchaDoin\\TodayData.txt", FileMode.Open);
            StreamWriter wt = new StreamWriter(fs, Encoding.UTF8);
            wt.WriteLine(DateTime.Now.ToString("dd/MM/yyyy"));
            wt.WriteLine(checkedListBox1.Items.Count);
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
            wt.WriteLine(timeBeginning);
            wt.Flush();
            wt.Close();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            SaveTodayTargets();
            SaveTodayData();
        }
        //Nhấn Enter trong textbox1 sẽ gửi lên checklistbox
        private void TextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Button1_Click(this, new EventArgs());
            }
        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void AddCommentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 a = new Form2();
            a.Show();
        }
    }
}
