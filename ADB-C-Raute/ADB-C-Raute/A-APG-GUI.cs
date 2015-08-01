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
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                var process = Process.Start("CMD.exe", "/c adb reboot");
                MessageBox.Show("Rebooting", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                process.WaitForExit(); 
            }
            if (radioButton2.Checked == true)
            {
                var process = Process.Start("CMD.exe", "/c adb reboot recovery");
                MessageBox.Show("Rebooting into Recovery", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                process.WaitForExit();
            }
            if (radioButton3.Checked == true)
            {
                var process = Process.Start("CMD.exe", "/c adb reboot download");
                MessageBox.Show("Rebooting into Downloadmode", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                process.WaitForExit();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var process = Process.Start("CMD.exe", "/c adb logcat *:E > logcat_DATUM_UHRZEIT.txt");
            process.WaitForExit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var process = Process.Start("CMD.exe", "/c adb kill-server");
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
    }
}
