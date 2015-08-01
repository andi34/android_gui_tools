using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Timer timer = new Timer();
            timer.Enabled = true;
            timer.Interval = 500;
            timer.Tick += new EventHandler(tick);
        }

        private void tick(object sender, EventArgs e)
        {
            label1.Text = "Time: " + DateTime.Now.ToString("HH:mm:ss");
            label2.Text = "Date: " + DateTime.Now.ToString("dd.MM.yyyy");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked == false)
            {
                string currentDir = Environment.CurrentDirectory;
                textBox2.Text = currentDir;
            }
            Process p = new Process();
            ProcessStartInfo info = new ProcessStartInfo();
            info.CreateNoWindow = true;
            info.FileName = "cmd.exe";
            info.RedirectStandardInput = true;
            info.UseShellExecute = false;

            p.StartInfo = info;
            p.Start();

            StreamWriter sw = p.StandardInput;
            const string quote = "\"";

            {
                if (sw.BaseStream.CanWrite)
                    sw.WriteLine("adb pull " + quote + textBox1.Text + quote + " " + quote + textBox2.Text + quote);
            }
            sw.Close();
            p.WaitForExit();
            p.Close();
            MessageBox.Show("file pulled", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            {
                FolderBrowserDialog F1 = new FolderBrowserDialog();
                F1.ShowDialog();
                textBox2.Text = F1.SelectedPath;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
