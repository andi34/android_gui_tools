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
            openFileDialog1.InitialDirectory = @"C:\";
            openFileDialog1.Title = "File for Sideload..";
            openFileDialog1.FileName = "Choose File..";
            openFileDialog1.CheckFileExists = true;
            openFileDialog1.CheckPathExists = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                const string quote = "\"";
                textBox1.Text = quote + openFileDialog1.FileName + quote;

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Process p = new Process();
            ProcessStartInfo info = new ProcessStartInfo();
            info.CreateNoWindow = true;
            info.FileName = "cmd.exe";
            info.RedirectStandardInput = true;
            info.UseShellExecute = false;

            p.StartInfo = info;
            p.Start();

            StreamWriter sw = p.StandardInput;

            {
                if (sw.BaseStream.CanWrite)
                    sw.WriteLine("adb sideload " + textBox1.Text);
            }
            sw.Close();
            p.WaitForExit();
            p.Close();    
            MessageBox.Show("Pushed file to Device for Sideload", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
