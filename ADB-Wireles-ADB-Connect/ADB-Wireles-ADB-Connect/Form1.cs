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

            button1.Text = "Connect";
            button1.Font = new Font(button1.Font, FontStyle.Bold);
            button2.Text = "Refresh";
            button2.Font = new Font(button2.Font, FontStyle.Bold);
            button3.Text = "Disconnect";
            button3.Font = new Font(button3.Font, FontStyle.Bold);
            button4.Text = "Start ADB-Server";
            button4.Font = new Font(button4.Font, FontStyle.Bold);
            button5.Text = "Kill ADB-Server";
            button5.Font = new Font(button5.Font, FontStyle.Bold);
            label3.Text = "IP-Adress";
            label4.Text = "Port";
            label5.Text = ":";


            textBox3.ReadOnly = true;
            textBox3.Multiline = true;

        }

        private void tick(object sender, EventArgs e)
        {
            label1.Text = "Date: " + DateTime.Now.ToString("dd.MM.yyyy") + "     Time: " + DateTime.Now.ToString("HH:mm:ss");
            label2.Text = "Brought to you by Android-Andi@XDA";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Enter IP-Address", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (textBox2.Text == "")
            {
                MessageBox.Show("Enter Port", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            else
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
                        sw.WriteLine("del adbdevices.txt");
                    sw.WriteLine("adb connect " + textBox1.Text + label5.Text + textBox2.Text);
                    sw.WriteLine("echo Connection: > adbdevices.txt");
                    sw.WriteLine("adb devices >> adbdevices.txt");
                    sw.Close();
                    p.WaitForExit();
                    p.Close();

                    string currentDir = Environment.CurrentDirectory;
                        if (File.Exists("adbdevices.txt"))
                        {
                            using (FileStream fileStream = File.OpenRead("adbdevices.txt"))
                            using (StreamReader streamReader = new StreamReader(fileStream))
                            {
                                string fileContent = streamReader.ReadToEnd();
                                textBox3.Text = fileContent;
                        }
                    }
                }
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
                    sw.WriteLine("del adbdevices.txt");
                sw.WriteLine("echo Connection: > adbdevices.txt");
                sw.WriteLine("adb devices >> adbdevices.txt");
                sw.Close();
                p.WaitForExit();
                p.Close();

                    if (File.Exists("adbdevices.txt"))
                    {
                        System.Threading.Thread.Sleep(5000);
                        using (FileStream fileStream = File.OpenRead("adbdevices.txt"))
                        using (StreamReader streamReader = new StreamReader(fileStream))
                        {
                            string fileContent = streamReader.ReadToEnd();
                            textBox3.Text = fileContent;
                            MessageBox.Show("Refreshed", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
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
                    sw.WriteLine("adb disconnect");
                sw.WriteLine("del adbdevices.txt");
                sw.WriteLine("adb devices > adbdevices.txt");
                sw.WriteLine("echo Disconnected >> adbdevices.txt");
                sw.Close();
                p.WaitForExit();
                p.Close();
                if (File.Exists("adbdevices.txt"))
                {
                    using (FileStream fileStream = File.OpenRead("adbdevices.txt"))
                    using (StreamReader streamReader = new StreamReader(fileStream))
                    {
                        string fileContent = streamReader.ReadToEnd();
                        textBox3.Text = fileContent;
                    }
                }
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
                    sw.WriteLine("del adbdevices.txt");
                sw.WriteLine("adb start-server");
                sw.WriteLine("echo starting Server... > adbdevices.txt");
                sw.WriteLine("adb devices >> adbdevices.txt");
                sw.Close();
                p.WaitForExit();
                p.Close();
                string currentDir = Environment.CurrentDirectory;
                if (File.Exists("adbdevices.txt"))
                {
                    using (FileStream fileStream = File.OpenRead("adbdevices.txt"))
                    using (StreamReader streamReader = new StreamReader(fileStream))
                    {
                        string fileContent = streamReader.ReadToEnd();
                        textBox3.Text = fileContent;
                    }
                }
               
            }
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
                    sw.WriteLine("del adbdevices.txt");
                sw.WriteLine("adb kill-server");
                sw.WriteLine("echo Server Killed > adbdevices.txt");
                sw.Close();
                p.WaitForExit();
                p.Close();
                if (File.Exists("adbdevices.txt"))
                {
                    using (FileStream fileStream = File.OpenRead("adbdevices.txt"))
                    using (StreamReader streamReader = new StreamReader(fileStream))
                    {
                        string fileContent = streamReader.ReadToEnd();
                        textBox3.Text = fileContent;
                    }
                }
            }
        }
    }
}