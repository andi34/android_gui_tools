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
            label3.Text = "Screenshot_" + DateTime.Now.ToString("yyyy.MM.dd") + "_" + DateTime.Now.ToString("HH-mm-ss") + ".png";
            string currentDir = Environment.CurrentDirectory;
            label9.Text = currentDir + "\\fb2png";

        }

        private void button1_Click(object sender, EventArgs e)
        {
            const string quote = "\"";
 
            if (checkBox3.Checked == false)
            {
                string currentDir = Environment.CurrentDirectory;
                textBox3.Text = currentDir;
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

            if (checkBox1.Checked == false)
            {
                if (sw.BaseStream.CanWrite)
                    sw.WriteLine("adb shell screencap -p sdcard/screen.png");
                sw.WriteLine("adb pull sdcard/screen.png" + " " + quote + textBox3.Text + "\\" + label3.Text + quote);
            }

            if (checkBox1.Checked == true)
            {
                if (sw.BaseStream.CanWrite)
                    sw.WriteLine("adb shell screencap -p " + textBox1.Text + "/screen.png");
                sw.WriteLine("adb pull " + textBox1.Text + "/screen.png" + " " + quote + textBox3.Text + "\\" + label3.Text + quote);
            }
            sw.Close();
            p.WaitForExit();
            p.Close();    
        }

        private void button2_Click(object sender, EventArgs e)
        {
            const string quote = "\"";

            if (checkBox3.Checked == false)
            {
                string currentDir = Environment.CurrentDirectory;
                textBox3.Text = currentDir;
            }
            
            if (File.Exists(label9.Text))
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

                if (checkBox2.Checked == false)
                {
                    string extpath = "/sdcard";

                    if (sw.BaseStream.CanWrite)
                        sw.WriteLine("adb shell mkdir " + extpath + "/fb2png");
                    sw.WriteLine("adb shell mkdir " + extpath + "/fb2png/tool");
                    sw.WriteLine("adb shell rm -rf " + extpath + "/fb2png/screens");
                    sw.WriteLine("adb shell mkdir " + extpath + "/fb2png/screens");
                    sw.WriteLine("adb push fb2png " + extpath + "/fb2png/tool/");
                    sw.WriteLine("adb shell chmod 755 " + extpath + "/fb2png/tool/fb2png");
                    sw.WriteLine("adb shell " + extpath + "/fb2png/tool/fb2png " + extpath + "/fb2png/screens/fbdump.png");
                    sw.WriteLine("adb pull " + extpath + "/fb2png/screens/fbdump.png " + quote + textBox3.Text + "\\" + label3.Text + quote);
                    sw.WriteLine("adb shell rm " + extpath + "/fb2png/screens/fbdump.png");
                }
                if (checkBox2.Checked == true)
                {
                    string extpath = textBox2.Text;

                    if (sw.BaseStream.CanWrite)
                        sw.WriteLine("adb shell mkdir " + extpath + "/fb2png");
                    sw.WriteLine("adb shell mkdir " + extpath + "/fb2png/tool");
                    sw.WriteLine("adb shell rm -rf " + extpath + "/fb2png/screens");
                    sw.WriteLine("adb shell mkdir " + extpath + "/fb2png/screens");
                    sw.WriteLine("adb push fb2png " + extpath + "/fb2png/tool/");
                    sw.WriteLine("adb shell chmod 755 " + extpath + "/fb2png/tool/fb2png");
                    sw.WriteLine("adb shell " + extpath + "/fb2png/tool/fb2png " + extpath + "/fb2png/screens/fbdump.png");
                    sw.WriteLine("adb pull " + extpath + "/fb2png/screens/fbdump.png " + quote + textBox3.Text + "\\" + label3.Text + quote);
                    sw.WriteLine("adb shell rm " + extpath + "/fb2png/screens/fbdump.png");
                }
                sw.Close();
                p.WaitForExit();
                p.Close();
            }
            else
            { 
                MessageBox.Show("Could´nt find " + label9.Text + ". Copy fb2png to tool path and try again!","", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog F1 = new FolderBrowserDialog();
            F1.ShowDialog();
            textBox3.Text = F1.SelectedPath;
        }
    }
}
