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
            label3.Text = "Zeit: " + DateTime.Now.ToString("HH:mm:ss");
            label4.Text = "Datum: " + DateTime.Now.ToString("dd.MM.yyyy");
            label5.Text = DateTime.Now.ToString("yyyy.MM.dd") + "_" + DateTime.Now.ToString("HH-mm-ss") + "_Logcat.txt";
            label6.Text = DateTime.Now.ToString("yyyy.MM.dd") + "_" + DateTime.Now.ToString("HH-mm-ss") + "_dmesg.txt";
            label7.Text = DateTime.Now.ToString("yyyy.MM.dd") + "_" + DateTime.Now.ToString("HH-mm-ss") + "_screenshot.png";

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
                sw.WriteLine("adb logcat *:E > " + label5.Text);
            }
           
        }

         private void button15_Click(object sender, EventArgs e)
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
                    sw.WriteLine("adb logcat *:F > " + label5.Text);
            }
        }

         private void button16_Click(object sender, EventArgs e)
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
                     sw.WriteLine("adb logcat > " + label5.Text);
             }
         }

        private void button3_Click(object sender, EventArgs e)
        {
            Process p = new Process();
            ProcessStartInfo info = new ProcessStartInfo();
            info.CreateNoWindow = false;
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

        private void button4_Click(object sender, EventArgs e)
        {
            var process = Process.Start("CMD.exe", "/c adb shell dmesg > " + label6.Text);
            process.WaitForExit();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = @"C:\";
            openFileDialog1.Title = "Select your APK..";
            openFileDialog1.FileName = "Choose File..";
            openFileDialog1.CheckFileExists = true;
            openFileDialog1.CheckPathExists = true;
            openFileDialog1.Filter = " .APK|*.apk";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {

                textBoxIAPK.Text = openFileDialog1.FileName;

            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            var process = Process.Start("CMD.exe", "/c adb install " + textBoxIAPK.Text);
            process.WaitForExit();
            MessageBox.Show(".APK is Installed", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            var process = Process.Start("CMD.exe", "/c adb pull " + textBoxPF.Text);
            process.WaitForExit();
            MessageBox.Show("file pulled", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            openFileDialog2.InitialDirectory = @"C:\";
            openFileDialog2.Title = "File to Push..";
            openFileDialog2.FileName = "Choose File..";
            openFileDialog2.CheckFileExists = true;
            openFileDialog2.CheckPathExists = true;

            if (openFileDialog2.ShowDialog() == DialogResult.OK)
            {

                textBoxFP1.Text = openFileDialog2.FileName;

            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            var process = Process.Start("CMD.exe", "/c adb push " + textBoxFP1.Text + " " + textBoxFP2.Text);
            process.WaitForExit();
            MessageBox.Show("File Pushed to device", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (checkBox3.Checked == true)
            {
                var process = Process.Start("CMD.exe", "/c adb shell " + textBoxADBC.Text);
                process.WaitForExit();
            }
            if (checkBox3.Checked == false)
            {
                var process = Process.Start("CMD.exe", "/c adb " + textBoxADBC.Text);
                process.WaitForExit();
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button13_Click(object sender, EventArgs e)
        {
            openFileDialog3.InitialDirectory = @"C:\";
            openFileDialog3.Title = "File to Push for Sideload..";
            openFileDialog3.FileName = "Choose File..";
            openFileDialog3.CheckFileExists = true;
            openFileDialog3.CheckPathExists = true;

            if (openFileDialog3.ShowDialog() == DialogResult.OK)
            {

                textBoxSL.Text = openFileDialog3.FileName;

            }
        }


        private void button14_Click(object sender, EventArgs e)
        {
            var process = Process.Start("CMD.exe", "/c adb sideload " + textBoxSL.Text);
            process.WaitForExit();
            MessageBox.Show("Pushed file to Device for Sideload", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void textBoxSL_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxPF_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxADBC_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxFP1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxFP2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked == false)
            {
                var process = Process.Start("CMD.exe", "/c adb shell screencap -p sdcard/screen.png");
                System.Threading.Thread.Sleep(3000);
                var process2 = Process.Start("CMD.exe", "/c adb pull sdcard/screen.png" + " " + label7.Text);
            }

            if (checkBox1.Checked == true)
            {
                var process = Process.Start("CMD.exe", "/c adb shell screencap -p " + textBox1.Text + "/screen.png");
                System.Threading.Thread.Sleep(3000);
                var process2 = Process.Start("CMD.exe", "/c adb pull " + textBox1.Text + "/screen.png" + " " + label7.Text);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
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
                sw.WriteLine("adb pull " + extpath + "/fb2png/screens/fbdump.png " + label7.Text);
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
                sw.WriteLine("adb pull " + extpath + "/fb2png/screens/fbdump.png " + label7.Text);
                sw.WriteLine("adb shell rm " + extpath + "/fb2png/screens/fbdump.png");
            }

            sw.Close();
            p.WaitForExit();
            p.Close();
        }
    }
}
