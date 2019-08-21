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
    public partial class HistoryForm : Form
    {
        public HistoryForm()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(6, 150);
            this.ShowInTaskbar = false;
        }

        public void SetText()
        {
            string HistoryDir = @"C:\Users\ADMIN\source\repos\WhatchaDoin\WhatchaDoin\History.txt";// Đường dẫn đến History
            string HistoryData = File.ReadAllText(HistoryDir);// Lưu từng dòng trong txt
            textBox1.Text = HistoryData;
        }

        private void HistoryForm_Load(object sender, EventArgs e)
        {
            SetText();
        }

        private void VScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {

        }
    }
}
