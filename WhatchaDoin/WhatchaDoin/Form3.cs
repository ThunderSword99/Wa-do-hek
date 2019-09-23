using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OfficeOpenXml;

namespace WhatchaDoin
{
    public partial class frmDetails : Form
    {
        ExcelPackage pkgDetails;// Tạo Package excel để chứa file excel
        string path = string.Empty;// Tạo đường dẫn rỗng
        ExcelWorksheet firstWorksheet;// Tạo worksheet để lấy worksheet trong excel

        public void LoadDataFromExcelToChart()
        {
            try
            {
                path = "Details.xlsx";// Link đường đẫn
                pkgDetails = new ExcelPackage(new FileInfo(path));// Khởi tạo pkg
                firstWorksheet = pkgDetails.Workbook.Worksheets[1];// Tạo worksheet lưu worksheet đầu tiên
            }
            catch(Exception ex)
            {
                MessageBox.Show("Đường dẫn bị lỗi");
            }
            
        }

        public void CreateDetailFiles()
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

        public frmDetails()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(6, 150);
            this.ShowInTaskbar = false;
            this.WindowState = FormWindowState.Maximized;
            LoadDataFromExcelToChart();
            AddValueToLineChart();
            AddValueToCircleChart();
        }

        public void SetText()
        {
            string HistoryDir = @"History.txt";// Đường dẫn đến History
            string HistoryData = File.ReadAllText(HistoryDir);
            textBox1.Text = HistoryData;
        }

        private void HistoryForm_Load(object sender, EventArgs e)
        {
            CreateDetailFiles();
            SetText();
        }

        private void VScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {

        }

        public void AddValueToLineChart()
        {
            try
            {
                for (int i = firstWorksheet.Dimension.Start.Column + 1; i <= firstWorksheet.Dimension.End.Column; i++)
                {
                    string targets = firstWorksheet.Cells[1, i].Value.ToString();
                    string points = firstWorksheet.Cells[2, i].Value.ToString();
                    string unpoints = firstWorksheet.Cells[3, i].Value.ToString();
                    string dat = DateTime.Parse(firstWorksheet.Cells[4, i].Value.ToString()).ToString("dd/MM/yyyy");
                    chartMain.Series["Targets"].Points.AddXY(dat, targets);
                    chartMain.Series["Completing"].Points.AddXY(dat, points);
                    chartMain.Series["Uncompleting"].Points.AddXY(dat, unpoints);
                }
            }
            catch (Exception ex)
            {
                
            }
        }

        public void AddValueToCircleChart()
        {
            try
            {
                // Lấy giữ liệu từ excel
                string totalPoints = firstWorksheet.Cells[6, 2].Value.ToString();
                string consecutivePoints = firstWorksheet.Cells[7, 2].Value.ToString();
                string relaxDay = firstWorksheet.Cells[8, 2].Value.ToString();
                string perfectPoint = firstWorksheet.Cells[9, 2].Value.ToString();
                string challenge = firstWorksheet.Cells[10,2].Value.ToString();
                string attendance = firstWorksheet.Cells[11, 2].Value.ToString();
                radarChart.Series["achieve"].Points.AddXY("Total", totalPoints);
                radarChart.Series["achieve"].Points.AddXY("Relax", relaxDay);
                radarChart.Series["achieve"].Points.AddXY("Perfect", perfectPoint);
                radarChart.Series["achieve"].Points.AddXY("Challenge", challenge);
                radarChart.Series["achieve"].Points.AddXY("Consecutive", consecutivePoints);
                radarChart.Series["achieve"].Points.AddXY("Attendance", attendance);
                
            }
            catch (Exception ex)
            {

            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            

        }

        private void Chart1_Click(object sender, EventArgs e)
        {

        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }
    }
}
