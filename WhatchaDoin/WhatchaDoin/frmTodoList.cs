using System;
using System.Text;
using System.Windows.Forms;
using System.IO;
using OfficeOpenXml;

namespace WhatchaDoin
{

    public partial class frmTodoList : Form
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
        private int lucky;
        // Các thành tựu
        private int totalIncompleteTargets;// Tổng các chỉ tiêu chưa hoàn thành
        private int relaxingDays;// Tổng ngày nghỉ
        private int perfectScore;// Tổng số ngày đạt điểm tuyệt đối liên tiếp
        private int totalPoint;// Tổng điểm đã làm được
        private int luckyPoint;// Điểm lucky
        private int challengePoint; // Số điểm challenge
        private int workingDays;
        // Tự thêm
        private Label label3;
        frmComment CommentForm;
        private string nowDate;
        private string previousDate;

        // Kết nối với dữ liệu excel
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
        // Kiểm tra xem có phải ngày hôm nay ko
        public bool IfTodayIsToday()
        {
            nowDate = DateTime.Now.ToString("MM/dd/yyyy");
            try
            {
                previousDate = firstWorksheet.Cells[6, 2].Value.ToString();
            }
            catch (Exception ex)
            {
                previousDate = nowDate;
                firstWorksheet.Cells[6, 2].Value = nowDate;
            }
            if (nowDate.Equals(previousDate))
            {
                return true;
            }
            return false;
        }
        // Tạo file History.txt để lưu lịch sử, tạo file detail lưu giữ liệu tổng
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
        // Đặt các thông số cho frmTodoList
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
            totalPoint = int.Parse(firstWorksheet.Cells[13, 2].Value.ToString());
            lucky = 0;
            challengePoint = 0;
        }
        // Đặt text cho Label trong frmTodoList
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
            label12.Text = text12 + totalPoint.ToString();

        }
        // Đặt mặc định cho frm 
        private void SetDefault()
        {
            SetParameter();
            SetTextToLabel();
        }
        // Chỉnh lại lucky point = false
        private void ResetLuckyStatus()
        {
            firstWorksheet.Cells[1, 4].Value = "False";
        }
        // Load các target lên checkListBox
        private void LoadingTodayTargets()
        {
            if (!IfTodayIsToday())
            {
                ClearTodayTargets();
                ResetLuckyStatus();
            }
            else
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
            
        }
        // Tạo mặc định cho frmTodoList
        public frmTodoList()
        {
            InitializeComponent();
            timer1.Enabled = true;
            timer2.Enabled = true;
            this.CenterToScreen();
        }
        // Xử lí khi nhấn button thêm
        private void Button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                textBox1.Text = textBox1.Text.Substring(0, 1).ToString().ToUpper() + textBox1.Text.Substring(1).ToString();
                checkedListBox1.Items.Add(textBox1.Text);
                textBox1.Text = null;
            }

        }
        // Đếm thời gian còn lại
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
        // Load form mỗi 100 milisecond
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
                
            }
        }
        // Click chuột phải để remove chỉ tiêu
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
        // Sự kiện remove item trong checkListBox
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
        // Ẩn ứng dụng dưới dạng notify và không chiểm quyền alt + tab
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
        // Sự kiện ấn vào notyfy icon trên desktop bar
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
        // Lưu nội dung các mục tiêu 
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
        // Hàm kiểu tra số cột không rỗng trả về int
        private int NullColumn(int r)
        {
            int c = 1;
            while (firstWorksheet.Cells[r, c].Value != null)
            {
                c++;
            }
            return c;
        }
        // Update các thành tựu
        private void UpdateAchievement()
        {
            // Lấy giữ liệu các thành tựu từ excel
            totalPoint = int.Parse(firstWorksheet.Cells[13, 2].Value.ToString());
            workingDays = int.Parse(firstWorksheet.Cells[14, 2].Value.ToString());
            relaxingDays = int.Parse(firstWorksheet.Cells[15, 2].Value.ToString());
            perfectScore = int.Parse(firstWorksheet.Cells[16, 2].Value.ToString());
            challengePoint = int.Parse(firstWorksheet.Cells[17, 2].Value.ToString());
            luckyPoint = int.Parse(firstWorksheet.Cells[18, 2].Value.ToString());
            totalIncompleteTargets = int.Parse(firstWorksheet.Cells[19, 2].Value.ToString());
            // Update thành tựu
            totalPoint += targets;
            if (targets != 0){
                workingDays++;
            }
            else
            {
                relaxingDays++;
            }
            if (targets == completeTargets)
            {
                perfectScore += 2 * targets;
            }
            // Challenge point
            luckyPoint += int.Parse(firstWorksheet.Cells[1, 5].Value.ToString());
            totalIncompleteTargets += incompleteTargets;
            // Lưu giá trị cho các thành tựu
            firstWorksheet.Cells[13, 2].Value = totalPoint;
            firstWorksheet.Cells[14, 2].Value = workingDays;
            firstWorksheet.Cells[15, 2].Value = relaxingDays;
            firstWorksheet.Cells[16, 2].Value = perfectScore;
            firstWorksheet.Cells[17, 2].Value = challengePoint;
            firstWorksheet.Cells[18, 2].Value = luckyPoint;
            firstWorksheet.Cells[19, 2].Value = totalIncompleteTargets;
        }
        // Xóa các mục tiêu khi chuyển ngày
        private void ClearTodayTargets()
        {
            string path = "TodayTargets.txt";
            File.WriteAllText(path, String.Empty);
        }
        // Lưu và update các thành tựu
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
                // Cập nhật mới các thành tựu
                UpdateAchievement();
                // Lưu giữ liệu ngày hôm qua
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
        // Sự kiện đóng form
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            SaveTodayTargets();
            SaveData();
        }
        // Nhấn Enter trong textbox1 sẽ gửi lên checklistbox
        private void TextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Button1_Click(this, new EventArgs());
            }
        }
        // Add comment cho mỗi item trong checkListBox
        private void AddCommentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            checkedListBox1.SelectedIndex = selectItem;
            String fileDir ="Comment" + frmTodoList.selectItem.ToString() + ".txt";
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
        // 
        private void CheckedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            String fileDir = "Comment" + frmTodoList.selectItem.ToString() + ".txt";
            try
            {
                if (File.Exists(fileDir))
                {
                    CommentForm = new frmComment();
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
        // Load form
        private void Form1_Load(object sender, EventArgs e)
        {
            ConnectToExcelFile();
            SetDefault();
            LoadingTodayTargets();
        }
        // Vào form Details
        private void HistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDetails abc = new frmDetails();
            abc.Show();
            this.Hide();
        }
        // Nhận điểm may mắn mỗi ngày
        private void PictureBox1_Click(object sender, EventArgs e)
        {
            if (firstWorksheet.Cells[1,4].Value.ToString().Equals("False"))
            {
                Random rnd = new Random();
                int luckyNumber = rnd.Next(1, 20);
                MessageBox.Show("Your lucky number is: " + luckyNumber.ToString(), "Have a good day!!");
                lucky = luckyNumber;
                firstWorksheet.Cells[1, 4].Value = "True";
                firstWorksheet.Cells[1, 5].Value = lucky;
                pkgDetails.Save();
            }
            else
            {
                MessageBox.Show("You have got lucky today.", "Alert");
            }
            
        }
    }
}
