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

            button1.Text = "Install APK";
            button1.Font = new Font(button1.Font, FontStyle.Bold);
            button2.Text = "Reset";
            button2.Font = new Font(button2.Font, FontStyle.Bold);
            button3.Text = "Browse file...";
            button3.Font = new Font(button3.Font, FontStyle.Bold);

            label3.Text = "Drag && Drop APK here";
            label3.Font = new Font(label3.Font, FontStyle.Bold);
            label3.Font = new Font(label3.Font.FontFamily, 16);
            textBox1.ReadOnly = true;
            label4.Text = "Filename and Path:";
            textBox2.Visible = false;
        }

        private void tick(object sender, EventArgs e)
        {
            label1.Text = "Date: " + DateTime.Now.ToString("dd.MM.yyyy") + "     Time: " + DateTime.Now.ToString("HH:mm:ss");
            label2.Text = "Brought to you by Android-Andi@XDA";
        }

        private void panel1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        private void panel1_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            foreach (string file in files)
                textBox1.Text = file;

            foreach (String apk in (String[])e.Data.GetData(DataFormats.FileDrop))
                label5.Text = (System.IO.Path.GetFileName(apk)) + " loaded";

            foreach (String extention in (String[])e.Data.GetData(DataFormats.FileDrop))
                textBox2.Text = (System.IO.Path.GetExtension(extention));

            if (textBox2.Text == ".apk")
            {
            }
            else if (textBox2.Text == ".APK")
            {
            }
            else
            {
                MessageBox.Show("Sorry, no APK-File!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox1.Text = "No APK loaded";
                label5.Text = "No APK loaded";
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == ".apk")
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
                const string quote = "\"";

                {
                    if (sw.BaseStream.CanWrite)
                        sw.WriteLine("adb install " + quote + textBox1.Text + quote);
                }
                sw.Close();
                p.WaitForExit();
                p.Close();
                MessageBox.Show("APK Installed", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            else if (textBox2.Text == ".APK")
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
                const string quote = "\"";

                {
                    if (sw.BaseStream.CanWrite)
                        sw.WriteLine("adb install " + quote + textBox1.Text + quote);
                }
                sw.Close();
                p.WaitForExit();
                p.Close();
                MessageBox.Show("APK Installed", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Sorry, no APK-File loaded!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "No APK loaded";
            label5.Text = "No APK loaded";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string currentDir = Environment.CurrentDirectory;
            openFileDialog1.InitialDirectory = currentDir;
            openFileDialog1.Title = "Choose APK..";
            openFileDialog1.FileName = "Choose File..";
            openFileDialog1.CheckFileExists = true;
            openFileDialog1.CheckPathExists = true;
            openFileDialog1.Filter = " .apk|*.APK";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = openFileDialog1.FileName;
                textBox2.Text = (System.IO.Path.GetExtension(openFileDialog1.FileName));
                label5.Text = (System.IO.Path.GetFileName(openFileDialog1.FileName)) +" loaded";
            }
            if (textBox2.Text == ".apk")
            {
            }
            else if (textBox2.Text == ".APK")
            {
            }
            else
            {
                MessageBox.Show("Sorry, no APK-File!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox1.Text = "No APK loaded";
                label5.Text = "No APK loaded";
            }
        }
    }
}