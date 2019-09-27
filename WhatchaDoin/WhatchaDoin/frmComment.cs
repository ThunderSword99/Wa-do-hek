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
    public partial class frmComment : Form
    {
        public static String textNote;
        public frmComment()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(940, 104);
            this.ShowInTaskbar = false;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.TopMost = true;
            textNote = textBox1.Text;
        }
        //Lưu comment items
        private void SaveNote()
        {
            //Kiểm tra xem nó có đang chọn items không
            if (frmTodoList.selectItem != -1)
            {
                string dirName = "Comment" + frmTodoList.selectItem + ".txt";
                //Kiểm tra text có khác rỗng
                if (textBox1.Text == "")
                {
                    File.WriteAllText(dirName, String.Empty);
                }
                else
                {
                    FileStream fs = new FileStream(dirName, FileMode.Open);
                    StreamWriter wt = new StreamWriter(fs, Encoding.UTF8);
                    wt.WriteLine(textBox1.Text);
                    wt.Close();
                }
                
            }
        }

        public void Form2_Load(object sender, EventArgs e)
        {
            textBox1.Text = null;
            String fileDir = "Comment" + frmTodoList.selectItem.ToString() + ".txt";
            try
            {   
                if (File.Exists(fileDir))
                {
                    String readFile = File.ReadAllText(fileDir);
                    textBox1.Text = readFile;
                }
                else
                {
                    MessageBox.Show("not ok");
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.ToString());
            }
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            textNote = textBox1.Text;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            SaveNote();
        }

    }
}
