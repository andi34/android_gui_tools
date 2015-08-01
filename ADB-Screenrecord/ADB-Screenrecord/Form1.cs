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
        }

        private void button1_Click(object sender, EventArgs e)
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

            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        class VirtualConsole
        {
            delegate void EditLabelDelegate(Label lbl, string str);

            // Ausgeführte Kommandos
            string[] cmd_history_list;
            int cmd_history_pointer;
            int cmd_scroll_index;

            // Konsolen Log Bildschirm
            string[] con_log_list;
            int con_log_pointer;
            int scroll_index;

            // Sonstiges
            int displayed_line;
            Form parent_form;

            #region Form Controls

            System.Windows.Forms.Panel ConsolePanel;
            System.Windows.Forms.Label ConsoleLabel;
            System.Windows.Forms.TextBox ConsoleCommandTextBox;
            System.Windows.Forms.Label[] ConsoleTextBox;

            private void InitializeConsoleComponent()
            {
                ConsolePanel = new System.Windows.Forms.Panel();
                ConsoleTextBox = new System.Windows.Forms.Label[displayed_line];
                ConsoleCommandTextBox = new System.Windows.Forms.TextBox();
                ConsoleLabel = new System.Windows.Forms.Label();
                ConsolePanel.SuspendLayout();

                // 
                // ConsolePanel
                // 
                this.ConsolePanel.BackColor = System.Drawing.Color.MediumBlue;
                this.ConsolePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

                for (int i = 0; i < ConsoleTextBox.Length; i++)
                {
                    this.ConsoleTextBox[i] = new Label();
                    this.ConsolePanel.Controls.Add(ConsoleTextBox[i]);
                }

                this.ConsolePanel.Controls.Add(this.ConsoleCommandTextBox);
                this.ConsolePanel.Controls.Add(this.ConsoleLabel);
                this.ConsolePanel.Dock = System.Windows.Forms.DockStyle.Top;
                this.ConsolePanel.Location = new System.Drawing.Point(0, 410);
                this.ConsolePanel.Name = "ConsolePanel";
                this.ConsolePanel.Size = new System.Drawing.Size(818, 35 + displayed_line * 18);
                this.ConsolePanel.TabIndex = 0;
                this.ConsolePanel.Visible = false;
                // 
                // ConsoleTextBox's
                // 
                for (int i = 0; i < ConsoleTextBox.Length; i++)
                {
                    this.ConsoleTextBox[i].BackColor = System.Drawing.Color.MediumBlue;
                    this.ConsoleTextBox[i].Dock = System.Windows.Forms.DockStyle.Top;
                    this.ConsoleTextBox[i].Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    this.ConsoleTextBox[i].ForeColor = System.Drawing.Color.White;
                    this.ConsoleTextBox[i].Location = new System.Drawing.Point(0, i * 18);
                    this.ConsoleTextBox[i].Name = "ConsoleLabel" + i;
                    this.ConsoleTextBox[i].Size = new System.Drawing.Size(816, 18);
                    this.ConsoleTextBox[i].Text = "" + i;
                    this.ConsoleTextBox[i].Click += new System.EventHandler(this.ConsoleLabel_Click);
                }

                // 
                // ConsoleCommandTextBox
                // 
                this.ConsoleCommandTextBox.BackColor = System.Drawing.Color.Navy;
                this.ConsoleCommandTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
                this.ConsoleCommandTextBox.Dock = System.Windows.Forms.DockStyle.Bottom;
                this.ConsoleCommandTextBox.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.ConsoleCommandTextBox.ForeColor = System.Drawing.Color.White;
                this.ConsoleCommandTextBox.Location = new System.Drawing.Point(0, 89);
                this.ConsoleCommandTextBox.Name = "ConsoleCommandTextBox";
                this.ConsoleCommandTextBox.Size = new System.Drawing.Size(816, 15);
                this.ConsoleCommandTextBox.TabIndex = 2;
                this.ConsoleCommandTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ConsoleCommandTextBox_KeyDown);
                // 
                // ConsoleLabel
                // 
                this.ConsoleLabel.BackColor = System.Drawing.Color.Navy;
                this.ConsoleLabel.Dock = System.Windows.Forms.DockStyle.Top;
                this.ConsoleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.ConsoleLabel.ForeColor = System.Drawing.Color.White;
                this.ConsoleLabel.Location = new System.Drawing.Point(0, 0);
                this.ConsoleLabel.Name = "ConsoleLabel";
                this.ConsoleLabel.Size = new System.Drawing.Size(816, 18);
                this.ConsoleLabel.TabIndex = 0;
                this.ConsoleLabel.Text = "- Konsole -";
                this.ConsoleLabel.Click += new System.EventHandler(this.ConsoleLabel_Click);

                this.ConsolePanel.ResumeLayout(false);
                this.ConsolePanel.PerformLayout();

                parent_form.Controls.Add(this.ConsolePanel);
            }

            private void ConsoleTextBox_Click(object sender, EventArgs e)
            {
                ConsoleCommandTextBox.Focus();
            }

            private void ConsoleLabel_Click(object sender, EventArgs e)
            {
                ConsoleCommandTextBox.Focus();
            }

            private void ConsoleCommandTextBox_KeyDown(object sender, KeyEventArgs e)
            {
                if (e.KeyValue == 13)
                {
                    //Wenn ENTER gedrückt wurde, führe das Kommando aus
                    string cmd = ConsoleCommandTextBox.Text;
                    ConsoleCommandTextBox.Text = "";
                    ExecuteCommand(cmd);
                }
                else if (e.KeyValue == 38)
                {
                    // Go down in the history list
                    cmd_scroll_index--;
                    if (cmd_scroll_index < 0) cmd_scroll_index = cmd_history_list.Length - 1;


                    // Display command in testbox
                    int projected_index = (cmd_history_list.Length + cmd_history_pointer + cmd_scroll_index) % (cmd_history_list.Length);
                    ConsoleCommandTextBox.Text = cmd_history_list[projected_index];
                }
                else if (e.KeyValue == 40)
                {
                    // Go down in the history list
                    cmd_scroll_index++;
                    if (cmd_scroll_index > cmd_history_list.Length - 1) cmd_scroll_index = 0;

                    // Display command in testbox
                    int projected_index = (cmd_history_list.Length + cmd_history_pointer + cmd_scroll_index) % (cmd_history_list.Length);
                    ConsoleCommandTextBox.Text = cmd_history_list[projected_index];
                }
            }


            #endregion

            /// <summary>
            /// In welcher Form soll die Konsole angezeigt werden
            /// </summary>
            /// <param name="parent">Das Parent-Form mitteilen</param>
            public VirtualConsole(Form1 parent)
            {
                parent_form = parent;

                // Konstruktor mit Standardwerten
                con_log_list = new String[100];
                con_log_pointer = 0;
                scroll_index = 0;

                // Ausgeführte Kommandos
                cmd_history_list = new String[50];
                cmd_history_pointer = 0;
                cmd_scroll_index = 0;

                displayed_line = 6;

                InitializeConsoleComponent();

                WriteLine("Geben Sie help ein, um eine Liste der Kommandos anzuzeigen");
                WriteLine("----------------------------------------------------------");
                WriteLine(" - Benutze ^ um die Konsole anzuzeigen/zu verstecken");
                WriteLine(" - Benutze Hoch/Runter um die letzten Kommandos zu sehen");
                WriteLine(" - Benutze Bildhoch/Bildrunter um in der Konsolenlog zu scrollen");
                WriteLine("- © 2007 Konstantin Gross - www.texturenland.de -");
            }

            /// <summary>
            /// Setzt die Sichtbarkeit der Konsole.
            /// </summary>
            /// <param name="visible">Soll die Konsole sichtbar sein?</param>
            public void SetVisibility(bool visible)
            {
                ConsolePanel.Visible = visible;

                if (visible == true)
                {
                    ConsoleCommandTextBox.Focus();
                }
                else
                {
                    parent_form.Focus();
                }
            }

            /// <summary>
            /// Wird ausgeführt wenn man hochscrollt im Log Bildschirm.
            /// </summary>
            public void Scroll_Up()
            {
                if (scroll_index >= con_log_list.Length - 1 - displayed_line) return;
                scroll_index++;

                RefreshLayout();
            }

            /// <summary>
            /// Wird ausgeführt wenn man runterscrollt im Log Bildschirm.
            /// </summary>
            public void Scroll_Down()
            {
                if (scroll_index <= 0) return;

                scroll_index--;

                RefreshLayout();
            }

            /// <summary>
            /// Rufe den aktuellen Sichtbarkeitsstauts der Konsole ab.
            /// </summary>
            /// <returns>Ist die Konsole sichtbar?</returns>
            public bool GetVisibility()
            {
                return ConsolePanel.Visible;
            }

            /// <summary>
            /// Führt die eingegebenen Kommandos aus.
            /// </summary>
            /// <param name="command">Kommando</param>
            private void ExecuteCommand(String command)
            {
                cmd_scroll_index = 0;

                // Wenn kein Kommando angegeben wurde, egal =)
                if (command == "") return;

                scroll_index = 0;

                // Speichere die Kommandis in der History
                cmd_history_list[cmd_history_pointer] = command;
                cmd_history_pointer++;

                if (cmd_history_pointer > cmd_history_list.Length - 1)
                    cmd_history_pointer = 0;

                WriteLine(">" + command);
                command = command.ToLower();

                // Führe das Kommando aus, wenn es existiert
                switch (command)
                {
                    case "exit":
                        Application.Exit();
                        break;

                    case "help":
                        WriteLine("Das ist die Liste der vorhandenen Kommandos:");
                        WriteLine(" - help : Dieser Bildschirm.");
                        WriteLine(" - version : Erhält die Programmversion.");
                        WriteLine(" - hide : Verstecke die Konsole.");
                        WriteLine(" - clear : Lösche den Text in der Konsole.");
                        WriteLine(" - message : Zeigt eine MessageBox an.");
                        WriteLine(" - test : Teste eine interne Funktion.");
                        WriteLine(" - exit : Beende die Anwendung.");
                        break;

                    case "hide":
                        SetVisibility(false);
                        break;

                    case "clear":
                        Clear();
                        break;

                    case "version":
                        WriteLine(Application.ProductName + " - v" + Application.ProductVersion);
                        break;

                    case "message":
                        MessageBox.Show("ICH LEBE!!!!!");
                        break;

                    case "test":
                        for (int i = 0; i <= 100; i++)
                        {
                            for (int j = 0; j <= 500; j++)
                            {
                                Application.DoEvents();
                            }

                            WriteLineForProgressive("WoW ein Fortschritt", i, 100);
                        }
                        break;


                    default:
                        WriteLine("Unbekanntes Kommando '" + command + "'.");
                        break;
                }
            }

            /// <summary>
            /// Schreibe eine Text in die Konsole.
            /// </summary>
            /// <param name="str">Zu übergebender Text</param>
            public void WriteLine(string str)
            {
                con_log_list[con_log_pointer] = str;

                con_log_pointer++;

                if (con_log_pointer > con_log_list.Length - 1)
                    con_log_pointer = 0;

                RefreshLayout();
            }

            /// <summary>
            /// Schreibe einen Text mit Fortschrittsanzeige
            /// </summary>
            /// <param name="str">Zu übergebender Text</param>
            /// <param name="progress">Fortschritt</param>
            /// <param name="total">Maximal</param>
            public void WriteLineForProgressive(string str, int progress, int total)
            {
                // Message in more states
                if ((ReadlastLine() != null) && (ReadlastLine().Length >= str.Length) && ((ReadlastLine()).Substring(0, str.Length) == str))
                {
                    int prozent = (progress * 100) / total;
                    WriteErasingCurrentLine(str + " [" + prozent + "%]");
                }
                else
                {
                    int prozent = (progress * 100) / total;
                    WriteLine(str + " [" + prozent + "%]");
                }
            }

            /// <summary>
            /// Lese letzte Zeile in der Konsole.
            /// </summary>
            /// <returns>Letze Zeile in der Konsole</returns>
            private String ReadlastLine()
            {
                return con_log_list[(con_log_pointer == 0 ? con_log_list.Length - 1 : con_log_pointer - 1)];
            }

            /// <summary>
            /// Schreibe einen Text in die Konsole.
            /// </summary>
            /// <param name="str">Zu übergebender Text</param>
            public void Write(String str)
            {
                con_log_list[con_log_pointer] = con_log_list[con_log_pointer] + str;
                RefreshLayout();
            }

            /// <summary>
            /// Lösche aktuelle Zeile
            /// </summary>
            /// <param name="str">Zu löschender Text</param>
            private void WriteErasingCurrentLine(String str)
            {

                con_log_list[(con_log_pointer == 0 ? con_log_list.Length - 1 : con_log_pointer - 1)] = str;
                RefreshLayout();
            }

            /// <summary>
            /// Löscht den Text in der Konsolenanzeige.
            /// </summary>
            public void Clear()
            {
                con_log_list = new String[50];
                con_log_pointer = 0;

                RefreshLayout();
            }

            /// <summary>
            /// Aktualisiere alle Controls.
            /// </summary>
            private void RefreshLayout()
            {
                int projected_index = 0;
                for (int line_counter = 0; line_counter < displayed_line; line_counter++)
                {
                    projected_index = ((con_log_list.Length - 1) + con_log_pointer - line_counter - scroll_index) % (con_log_list.Length);
                    parent_form.Invoke(new EditLabelDelegate(EditLabel), ConsoleTextBox[line_counter], con_log_list[projected_index]);
                }
            }

            private void EditLabel(Label lbl, String str)
            {
                lbl.Text = str;
            }
        }
    }
}