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
            label3.Text = "Logcat_" + DateTime.Now.ToString("yyyy.MM.dd") + "_" + DateTime.Now.ToString("HH-mm-ss") + ".txt";
            label4.Text = "dmesg_" + DateTime.Now.ToString("yyyy.MM.dd") + "_" + DateTime.Now.ToString("HH-mm-ss") + ".txt";
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
            MessageBox.Show("Use the Stop Logcat Button to Stop Logcat. The command runs in background.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

            {
                if (sw.BaseStream.CanWrite)
                    sw.WriteLine("adb logcat *:E > " + label3.Text);
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
            MessageBox.Show("Use the Stop Logcat Button to Stop Logcat. The command runs in background.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

            {
                if (sw.BaseStream.CanWrite)
                    sw.WriteLine("adb logcat *:F > " + label3.Text);
            }
        }

        private void button3_Click(object sender, EventArgs e)
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
            MessageBox.Show("Use the Stop Logcat Button to Stop Logcat. The command runs in background.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

            {
                if (sw.BaseStream.CanWrite)
                    sw.WriteLine("adb logcat > " + label3.Text);
            }
        }

        private void button4_Click(object sender, EventArgs e)
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
                sw.WriteLine("adb kill-server");
                sw.WriteLine("adb start-server");
            }
            sw.Close();
            p.WaitForExit();
            p.Close();
        }

        private void button5_Click(object sender, EventArgs e)
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
                    sw.WriteLine("adb shell dmesg > " + label4.Text);
            }
            sw.Close();
            p.WaitForExit();
            p.Close();
        }
    }
}
