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
            Process p = new Process();
            ProcessStartInfo info = new ProcessStartInfo();
            info.CreateNoWindow = true;
            info.FileName = "cmd.exe";
            info.RedirectStandardInput = true;
            info.UseShellExecute = false;

            p.StartInfo = info;
            p.Start();

            StreamWriter sw = p.StandardInput;

            if (radioButton1.Checked == true)
            {
                if (sw.BaseStream.CanWrite)
                    sw.WriteLine("adb reboot");
                MessageBox.Show("Rebooting", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            if (radioButton2.Checked == true)
            {
                if (sw.BaseStream.CanWrite)
                    sw.WriteLine("adb reboot recovery");
                MessageBox.Show("Rebooting into Recovery", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            if (radioButton3.Checked == true)
            {
                if (sw.BaseStream.CanWrite)
                    sw.WriteLine("adb reboot");
                MessageBox.Show("Rebooting into Odin- / Download-Mode", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            sw.Close();
            p.WaitForExit();
            p.Close();
        }
    }
}
