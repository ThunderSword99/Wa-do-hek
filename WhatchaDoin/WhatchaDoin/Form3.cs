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

namespace WhatchaDoin
{
    public partial class frmDetails : Form
    {
        public frmDetails()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(6, 150);
            this.ShowInTaskbar = false;
            this.WindowState = FormWindowState.Maximized;
        }

        public void SetText()
        {
            string HistoryDir = @"History.txt";// Đường dẫn đến History
            string HistoryData = File.ReadAllText(HistoryDir);
            textBox1.Text = HistoryData;
        }

        private void HistoryForm_Load(object sender, EventArgs e)
        {
            SetText();
        }

        private void VScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            chartMain.Series["Completing"].Points.AddXY("1", 7);
            chartMain.Series["Completing"].Points.AddXY("2", 5);
            chartMain.Series["Completing"].Points.AddXY("3", 8);
            chartMain.Series["Completing"].Points.AddXY("4", 2);
            chartMain.Series["Completing"].Points.AddXY("5", 0);
            chartMain.Series["Completing"].Points.AddXY("6", 20);

            chartMain.Series["Uncompleting"].Points.AddXY("5", 2);
            chartMain.Series["Uncompleting"].Points.AddXY("5", 4);
            chartMain.Series["Uncompleting"].Points.AddXY("5", 1);
            chartMain.Series["Uncompleting"].Points.AddXY("5", 3);
            chartMain.Series["Uncompleting"].Points.AddXY("5", 7);

            radarChart.Series["Test"].Points.AddXY("1", 2);
            radarChart.Series["Test"].Points.AddXY("2", 7);
            radarChart.Series["Test"].Points.AddXY("3", 10);
            radarChart.Series["Test"].Points.AddXY("4", 4);
            radarChart.Series["Test"].Points.AddXY("5", 5);
            radarChart.Series["Test"].Points.AddXY("6", 8);
        }

        private void Chart1_Click(object sender, EventArgs e)
        {

        }
    }
}
