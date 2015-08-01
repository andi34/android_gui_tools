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
            label3.Text = "Time: " + DateTime.Now.ToString("HH:mm:ss");
            label4.Text = "Date: " + DateTime.Now.ToString("dd.MM.yyyy");
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
            MessageBox.Show("Use the Stop Logcat Button to Stop Logcat. The command runs in background.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
            MessageBox.Show("Use the Stop Logcat Button to Stop Logcat. The command runs in background.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
             MessageBox.Show("Use the Stop Logcat Button to Stop Logcat. The command runs in background.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
             
             {
                 if (sw.BaseStream.CanWrite) 
                     sw.WriteLine("adb logcat > " + label5.Text);
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
                    sw.WriteLine("adb shell dmesg > " + label6.Text);
            }
            sw.Close();
            p.WaitForExit();
            p.Close();
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
                const string quote = "\"";
                textBoxIAPK.Text = quote + openFileDialog1.FileName + quote;

            }
        }

        private void button8_Click(object sender, EventArgs e)
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
                    sw.WriteLine("adb install " + textBoxIAPK.Text);
            }
            sw.Close();
            p.WaitForExit();
            p.Close();         
            MessageBox.Show(".APK is Installed", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button11_Click(object sender, EventArgs e)
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
                    sw.WriteLine("adb pull " + textBoxPF.Text);
            }
            sw.Close();
            p.WaitForExit();
            p.Close();         
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
                const string quote = "\"";
                textBoxFP1.Text = quote + openFileDialog2.FileName + quote;

            }
        }

        private void button10_Click(object sender, EventArgs e)
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
                    sw.WriteLine("adb push " + textBoxFP1.Text + " " + textBoxFP2.Text);
            }
            sw.Close();
            p.WaitForExit();
            p.Close();    
            MessageBox.Show("File Pushed to device", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button12_Click(object sender, EventArgs e)
       
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

            if (checkBox3.Checked == false)
            {
                if (sw.BaseStream.CanWrite)
                    sw.WriteLine("adb " + textBoxADBC.Text);

            }
             if (checkBox3.Checked == true)
            {
               
                 if (sw.BaseStream.CanWrite)
                    sw.WriteLine("adb ahell " + textBoxADBC.Text);
 
            }

            sw.Close();
            p.WaitForExit();
            p.Close();
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
                const string quote = "\"";
                textBoxSL.Text = quote + openFileDialog3.FileName + quote;

            }
        }

        private void button14_Click(object sender, EventArgs e)
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
                    sw.WriteLine("adb sideload " + textBoxSL.Text);
            }
            sw.Close();
            p.WaitForExit();
            p.Close();    
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
                            System.Threading.Thread.Sleep(3000);
                sw.WriteLine("adb pull sdcard/screen.png" + " " + label7.Text);
                }      

            if (checkBox1.Checked == true)
            {
                if (sw.BaseStream.CanWrite)
                        sw.WriteLine("adb shell screencap -p " + textBox1.Text + "/screen.png");
                            System.Threading.Thread.Sleep(3000);
                sw.WriteLine("adb pull " + textBox1.Text + "/screen.png" + " " + label7.Text);
            }
            sw.Close();
            p.WaitForExit();
            p.Close();    
              
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

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
