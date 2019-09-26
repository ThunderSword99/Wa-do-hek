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
using OfficeOpenXml;

namespace WhatchaDoin
{

    public partial class Form1 : Form
    {
        ExcelPackage pkgDetails;// Tạo Package excel để chứa file excel
        string path = string.Empty;// Tạo đường dẫn rỗng
        ExcelWorksheet firstWorksheet;// Tạo worksheet để lấy worksheet trong excel

        // Đặt text mặc định để hiển thị
        private string text3 = "- Số chỉ tiêu: ";
        private string text4 = "- Chỉ tiêu đã hoàn thành: ";
        private string text5 = "- Chỉ tiêu chưa hoàn thành: ";
        private string text6 = "- Thời gian bắt đầu: ";
        private string text7 = "- Thời gian còn lại: ";
        private string text8 = "- Điểm ngày hôm nay: ";
        private string text9 = "- Số nhiệm vụ đã bỏ: ";
        private string text10 = "- Số ngày nghỉ ngơi: ";
        private string text11 = "- Điểm tuyệt đối: ";
        private string text12 = "- Tổng điểm: ";
        // Đặt thông số
        private int targets; // Số chỉ tiêu đề ra
        private int completeTargets;// Số chỉ tiêu đã hoàn thành 
        private int incompleteTargets;// Số chỉ tiêu chưa hoàn thành
        private string startTimeline;// Thời gian bắt đầu thực hiện
        private TimeSpan timeLeft;// Thời gian còn lại
        private int todayScore;// Điểm ngày hôm nay 
        private int totalIncompleteTargets;// Tổng các chỉ tiêu chưa hoàn thành
        private int relaxingDays;// Tổng ngày nghỉ
        private int perfectScore;// Tổng số ngày đạt điểm tuyệt đối liên tiếp
        private int totalScore;// Tổng điểm đã làm được
        // Tự thêm
        private Label label3;
        Form2 CommentForm;

        public void ConnectToExcelFile()
        {
            try
            {
                path = "Details.xlsx";// Link đường đẫn
                pkgDetails = new ExcelPackage(new FileInfo(path));// Khởi tạo pkg
                firstWorksheet = pkgDetails.Workbook.Worksheets[1];// Tạo worksheet lưu worksheet đầu tiên
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đường dẫn bị lỗi");
            }

        }

        public bool IfTodayIsToday()
        {
            string date = DateTime.Now.ToString("MM/dd/yyyy");
            String historyDate = firstWorksheet.Cells[6, 2].Value.ToString();
            if (date.Equals(historyDate))
            {
                return true;
            }
            return false;
        }

        public void CreateFiles()
        {
            string fileName = @"History.txt";
            try
            {
                if (File.Exists(fileName))
                {

                }
                else
                {
                    FileStream fs = File.Create(fileName);
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.ToString());
            }
            fileName = @"Details.xlsx";
            try
            {
                if (File.Exists(fileName))
                {

                }
                else
                {
                    FileStream fs = File.Create(fileName);
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.ToString());
            }
        }

        private void LoadingTotalData()
        {
            string TotalDF = @"TodayData.txt";// Đường dẫn đến TodayData
            string[] TotalData = File.ReadAllLines(TotalDF);// Lưu từng dòng trong txt
            totalIncompleteTargets = (int)TotalData[0][0];
            relaxingDays = (int)TotalData[1][0];
            perfectScore = (int)TotalData[2][0];
            totalScore = (int)TotalData[3][0];
        }

        private void SetParameter()
        {
            targets = checkedListBox1.Items.Count;
            completeTargets = checkedListBox1.CheckedItems.Count;
            incompleteTargets = targets - completeTargets;
            startTimeline = (DateTime.Parse(firstWorksheet.Cells[1, 2].Value.ToString())).ToString("h:mm tt");
            timeLeft = new TimeSpan(24, 0, 0) - DateTime.Now.TimeOfDay;
            todayScore = completeTargets;
            totalIncompleteTargets = int.Parse(firstWorksheet.Cells[19, 2].Value.ToString());
            relaxingDays = int.Parse(firstWorksheet.Cells[15, 2].Value.ToString());
            perfectScore = int.Parse(firstWorksheet.Cells[16, 2].Value.ToString());
            totalScore = int.Parse(firstWorksheet.Cells[13, 2].Value.ToString());
        }
        
        private void SetTextToLabel()
        {
            label3.Text = text3 + targets.ToString();
            label4.Text = text4 + completeTargets.ToString();
            label5.Text = text5 + incompleteTargets.ToString();
            label6.Text = text6 + startTimeline.ToString();
            label7.Text = text7 + timeLeft.ToString();
            label8.Text = text8 + todayScore.ToString();
            label9.Text = text9 + totalIncompleteTargets.ToString();
            label10.Text = text10 + relaxingDays.ToString();
            label11.Text = text11 + perfectScore.ToString();
            label12.Text = text12 + totalScore.ToString();

        }

        private void SetDefault()
        {
            SetParameter();
            SetTextToLabel();
        }

        private void LoadingTodayTargets()
        {
            string[] lines = File.ReadAllLines("TodayTargets.txt");
            foreach (string s in lines)
            {
                try
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
                catch
                {

                }
            }
        }

        private void UpdateTotalData()
        {         
            ////Lấy giữ liệu tổng từ TodayData.txt lên Codesources
            //string TotalDF = "TotalData.txt";// Đường dẫn đến TodayData
            //string[] TotalData = File.ReadAllLines(TotalDF);// Lưu từng dòng trong txt
            ////lấy dữ liệu tổng
            //totalUncompleteTargets = Int32.Parse(TotalData[0].ToString()); 
            //relaxingDay = Int32.Parse(TotalData[1].ToString());
            //perfectScoreInARow = Int32.Parse(TotalData[2].ToString());
            //totalScore = Int32.Parse(TotalData[3].ToString());
            ////Lấy giữ liệu từ TodayData.txt lên Codesouces
            //string TodayDF = "TodayData.txt";// Đường dẫn đến TodayData
            //string[] TodayData = File.ReadAllLines(TodayDF);// Lưu từng dòng trong txt
            ////Cập nhật dữ liệu tổng
            //totalUncompleteTargets += Int32.Parse(TodayData[3].ToString());
            //if (TodayData[2][0] == '0')
            //{
            //    relaxingDay += 1;
            //    perfectScoreInARow = 0;
            //}
            //else
            //{
            //    relaxingDay = 0;
            //}
            //if (TodayData[2][0] == TodayData[1][0] && TodayData[2][0]!='0')
            //{
            //    perfectScoreInARow += 1;
            //}
            //else
            //{
            //    perfectScoreInARow = 0;
            //}
            //totalScore += Int32.Parse(TodayData[2].ToString());
            ////Ghi lại giữ liệu tổng vào TotalData.txt
            //FileStream fs = new FileStream("TotalData.txt", FileMode.Open);
            //StreamWriter wt = new StreamWriter(fs, Encoding.UTF8);
            //wt.WriteLine(totalUncompleteTargets);
            //wt.WriteLine(relaxingDay);
            //wt.WriteLine(perfectScoreInARow);
            //wt.WriteLine(totalScore);
            //wt.Flush();
            //wt.Close();
        }
        
        private void ChangeTodayDataTxt()
        {
            FileStream fs = new FileStream("TodayData.txt", FileMode.Open);
            StreamWriter wt = new StreamWriter(fs, Encoding.UTF8);
            wt.WriteLine(DateTime.Now.ToString("dd/MM/yyyy"));
            wt.WriteLine(0);
            wt.WriteLine(0);
            wt.WriteLine(0);
            wt.WriteLine(timeLeft);
            wt.Flush();
            wt.Close();
        }

        private void LoadingTodayData()
        {
            string[] TodayData = File.ReadAllLines("TodayData.txt");// Lưu từng dòng trong txt
            string TodayDAT = DateTime.Now.ToString("dd/MM/yyyy");
            if (TodayDAT != TodayData[0])// Kiểm tra xem thời gian bắt đầu là của hôm qua hay hôm nay
            {
                UpdateTotalData();
                ChangeTodayDataTxt();
                LoadingTotalData();
                checkedListBox1.Items.Clear();
                //Xóa hết comment items
                try
                {
                    for (int i=0;i<=100;i++)
                    {
                        string dirName = "Comment" + i + ".txt";
                        File.Delete(dirName);
                    }
                }
                catch
                {

                }
               
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
            timer1.Enabled = true;
            timer2.Enabled = true;
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
            if (timeLeft.TotalSeconds <= 100)
            {
                timer1.Stop();
                timer2.Stop();
                this.Close();
            }
            else
            {
                timeLeft = timeLeft.Subtract(new TimeSpan(0, 0, 1));
                label7.Text = text7 + timeLeft.ToString(@"hh\:mm\:ss");
            }
        }
        //Load form mỗi 100 milisecond
        private void Timer2_Tick(object sender, EventArgs e)
        {
            targets = checkedListBox1.Items.Count;
            label3.Text = text3 + targets.ToString();
            completeTargets = checkedListBox1.CheckedItems.Count;
            label4.Text = text4 + completeTargets.ToString();
            incompleteTargets = checkedListBox1.Items.Count - checkedListBox1.CheckedItems.Count;
            label5.Text = text5 + incompleteTargets.ToString();
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
            if (selectItem == -1)
            {
                CommentForm.Hide();
            }
        }

        //Click chuột phải để remove chỉ tiêu
        public static int selectItem;// Item trong check list box được chọn
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
            try
            {
                string dirName = "Comment" + selectItem + ".txt";
                File.Delete(dirName);

                string path = "History.txt";
                File.AppendAllText(path, "*Adding new target" + Environment.NewLine);
            }
            catch
            {

            }
            CommentForm.Hide();
        }
        //Ẩn ứng dụng dưới dạng notify và không chiểm quyền alt + tab
        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            bool MousePoiterNotOnTaskBar = Screen.GetWorkingArea(this).Contains(Cursor.Position);//Chỉ 

            if (this.WindowState == FormWindowState.Minimized && MousePoiterNotOnTaskBar)
            {
                notifyIcon1.Visible = true;
                this.Hide();
                CommentForm.Hide();
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
                string path = "TodayTargets.txt";
                File.WriteAllText(path, String.Empty);
            }
            else
            {
                string path = "TodayTargets.txt";
                File.WriteAllText(path, String.Empty);
                FileStream fs = new FileStream("TodayTargets.txt", FileMode.Open);
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

        private int NullColumn(int r)
        {
            int c = 1;
            while (firstWorksheet.Cells[r,c].Value != null)
            {
                c++;
            }
            return c;
        }

        private void SaveData()
        {
            if (IfTodayIsToday())
            {
                firstWorksheet.Cells[3, 2].Value = targets;
                firstWorksheet.Cells[4, 2].Value = completeTargets;
                firstWorksheet.Cells[5, 2].Value = incompleteTargets;
                pkgDetails.Save();
            }
            else
            {
                int nextColumn = NullColumn(8);
                for (int i = 3; i <= 6; i++)
                {    
                    firstWorksheet.Cells[i, 2].Copy(firstWorksheet.Cells[i+5, nextColumn]);
                }
                string date = DateTime.Now.ToString("MM/dd/yyyy");
                string time = DateTime.Now.ToString("h:mm tt");
                firstWorksheet.Cells[1, 2].Value = time;
                firstWorksheet.Cells[3, 2].Value = targets;
                firstWorksheet.Cells[4, 2].Value = completeTargets;
                firstWorksheet.Cells[5, 2].Value = incompleteTargets;
                firstWorksheet.Cells[6, 2].Value = date;
                pkgDetails.Save();

            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            SaveTodayTargets();
            SaveData();
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
            checkedListBox1.SelectedIndex = selectItem;
            String fileDir ="Comment" + Form1.selectItem.ToString() + ".txt";
            try
            {
                if (File.Exists(fileDir))
                {

                }
                else
                {
                    File.Create(fileDir);
                    CommentForm.Form2_Load(this, null);
                    CommentForm.Show();
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.ToString());
            }
        }

        private void Form1_Click(object sender, EventArgs e)
        {

        }

        private void CheckedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            String fileDir = "Comment" + Form1.selectItem.ToString() + ".txt";
            try
            {
                if (File.Exists(fileDir))
                {
                    CommentForm = new Form2();
                    CommentForm.Form2_Load(this,null);
                    CommentForm.Show();
                    this.Select();
                }
                else
                {
                    CommentForm.Hide();
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.ToString());
            }
        }

        private void Label1_DoubleClick(object sender, EventArgs e)
        {
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ConnectToExcelFile();
            SetDefault();
        }

        private void HistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            int luckyNumber = rnd.Next(1, 11);
            MessageBox.Show("Your lucky number is: " + luckyNumber.ToString(),"Have a good day!!");
        }
    }
}
