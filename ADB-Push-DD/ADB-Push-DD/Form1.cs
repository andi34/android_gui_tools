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

            button1.Text = "Push File to Device";
            button1.Font = new Font(button1.Font, FontStyle.Bold);
            button2.Text = "Reset";
            button2.Font = new Font(button2.Font, FontStyle.Bold);
            button3.Text = "Browse file...";
            button3.Font = new Font(button3.Font, FontStyle.Bold);
            label3.Text = "Drag && Drop your File here";
            label3.Font = new Font(label3.Font, FontStyle.Bold);
            label3.Font = new Font(label3.Font.FontFamily, 16);
            textBox1.ReadOnly = true;
            label4.Text = "Path && Filename on PC:";
            textBox2.Visible = false;
            label6.Text = "Path && Filename on Device:";
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

            foreach (String fzip in (String[])e.Data.GetData(DataFormats.FileDrop))
            {
                label5.Text = (System.IO.Path.GetFileName(fzip));
                label7.Text = (System.IO.Path.GetFileName(fzip)) + " loaded";
            }
            foreach (String extention in (String[])e.Data.GetData(DataFormats.FileDrop))
                textBox2.Text = (System.IO.Path.GetExtension(extention));      
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox3.Text = "Enter Path on Device";
            label5.Text = "No File loaded";
            label7.Text = "No File loaded";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (label5.Text == "No File loaded")
            {
                MessageBox.Show("No File selected", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
            if (textBox3.Text == "Enter Path on Device")
            {
                MessageBox.Show("No Destination Path entered.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                const string quote = "\"";

                {
                    if (sw.BaseStream.CanWrite)
                        sw.WriteLine("adb push " + quote + textBox1.Text + quote + " " + quote + textBox3.Text + quote);
                }
                sw.Close();
                p.WaitForExit();
                p.Close();
                MessageBox.Show("File Pushed to device", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string currentDir = Environment.CurrentDirectory;
            openFileDialog1.InitialDirectory = currentDir;
            openFileDialog1.Title = "File to Push..";
            openFileDialog1.FileName = "Choose File..";
            openFileDialog1.CheckFileExists = true;
            openFileDialog1.CheckPathExists = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = openFileDialog1.FileName;

            }
        }
    }
}