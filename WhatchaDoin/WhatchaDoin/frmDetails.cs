﻿using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using OfficeOpenXml;

namespace WhatchaDoin
{
    public partial class frmDetails : Form
    {
        ExcelPackage pkgDetails;// Tạo Package excel để chứa file excel
        string path = string.Empty;// Tạo đường dẫn rỗng
        ExcelWorksheet firstWorksheet;// Tạo worksheet để lấy worksheet trong excel

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

        public void GetDateFromDateTimePicker(DateTime dat)
        {
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
            dateTimePicker1.Select();
            
        }

        private void VScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {

        }

        public void AddValueToLineChart()
        {
            try
            {
                for (int i = 2; i < NullColumn(8); i++)
                {
                    string targets = firstWorksheet.Cells[8, i].Value.ToString();
                    string points = firstWorksheet.Cells[9, i].Value.ToString();
                    string unpoints = firstWorksheet.Cells[10, i].Value.ToString();
                    string dat = DateTime.Parse(firstWorksheet.Cells[11, i].Value.ToString()).ToString("dd/MM/yyyy");
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
                //Lấy giữ liệu từ excel
                string totalPoints = firstWorksheet.Cells[13, 2].Value.ToString();
                string consecutivePoints = firstWorksheet.Cells[14, 2].Value.ToString();
                string relaxDay = firstWorksheet.Cells[15, 2].Value.ToString();
                string perfectPoint = firstWorksheet.Cells[16, 2].Value.ToString();
                string challenge = firstWorksheet.Cells[17, 2].Value.ToString();
                string lucky = firstWorksheet.Cells[18, 2].Value.ToString();
                radarChart.Series["achieve"].Points.AddXY("Total", totalPoints);
                radarChart.Series["achieve"].Points.AddXY("Relax", relaxDay);
                radarChart.Series["achieve"].Points.AddXY("Perfect", perfectPoint);
                radarChart.Series["achieve"].Points.AddXY("Challenge", challenge);
                radarChart.Series["achieve"].Points.AddXY("Consecutive", consecutivePoints);
                radarChart.Series["achieve"].Points.AddXY("Lucky", lucky);

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

        private void TodolistToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmTodoList frmTodoList = new frmTodoList();
            frmTodoList.Closed += (s, args) => this.Close();
            frmTodoList.Show();
        }

        private void ResetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Reset will delete all your data. Do you want to save changes?", "Confirmation", MessageBoxButtons.OKCancel);
            if (result == DialogResult.OK)
            {
                for (int i=8;i<=11;i++)
                {
                    for (int j=2;j<=16384;j++)
                    {
                        if (firstWorksheet.Cells[i, j].Value!=null)
                        {
                            firstWorksheet.Cells[i, j].Value = null;
                        }                        
                    }
                }
                for (int i=13;i<=19;i++)
                {
                    if (firstWorksheet.Cells[i, 2].Value != null)
                    {
                        firstWorksheet.Cells[i, 2].Value = 0;
                    }
                }
                pkgDetails.Save();
                string path = "History.txt";
                File.WriteAllText(path, ">>>>>>>>>>HISTORY<<<<<<<<<<");
            }
            else if (result == DialogResult.Cancel)
            {
                //...
            }
            else
            {
                //...
            }
        }
    }
}
