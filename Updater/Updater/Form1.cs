using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Diagnostics;

namespace Updater
{
    public partial class Form1 : Form
    {
        string[] FilesForDeleting;
        string[] FilesForDownload;
        string[] FilesForDownloadNames;
        int BotVersion = 2603;
        int NewBotVersion;
        string reportLog = "";
        string labelMessage = "";
        int percentage = 0;

        //Proxy
        string webProxy = string.Empty;
        string domaindAndUsername = string.Empty;
        string proxyPassword = string.Empty;

        public Form1()
        {
            InitializeComponent();
            timer1.Start();
            ClosingSimpleBotProcces();
            ProxyReader();
        }

        private void UpdateFileReader()
        {
            WebClient client = new WebClient();
            Stream stream = WebClientCreation().OpenRead("https://dl.dropbox.com/s/gixvi3853twwks3/UpdateFile.csv?dl=1");
            StreamReader reader = new StreamReader(stream);
            string[] FileContent = reader.ReadToEnd().Split(';');
            NewBotVersion = Convert.ToInt32(FileContent[0]);
            FilesForDeleting = FileContent[1].Split(',');
            FilesForDownload = FileContent[2].Split(',');
            FilesForDownloadNames = FileContent[3].Split(',');
            percentage += 5;
        }

        private WebClient WebClientCreation()
        {
            WebClient client = new WebClient();
            if (!string.IsNullOrEmpty(webProxy))
            {
                WebProxy wp = new WebProxy(webProxy);
                if (!string.IsNullOrEmpty(domaindAndUsername))
                {
                    wp.Credentials = new NetworkCredential(domaindAndUsername, proxyPassword);
                    WebRequest.DefaultWebProxy = wp;
                }
                client.Proxy = wp;
            }
            return client;
        }

        private void ProxyReader()
        {
            try
            {
                StreamReader reader = new StreamReader(File.OpenRead("Proxy.txt"));
                string[] FileContent = reader.ReadToEnd().Split(';');
                reader.Close();
                webProxy = FileContent[0];
                domaindAndUsername = FileContent[1];
                proxyPassword = FileContent[2];
            }
            catch { }
        }

        private void FilesDeleting()
        {
            try
            {
                //Deliting files
                for (int i = 0; i < FilesForDeleting.Length; i++)
                {
                    try
                    {
                        if (File.Exists( FilesForDeleting[i]) == true)
                        {
                            File.Delete(FilesForDeleting[i]);
                            reportLog += "Файл " + FilesForDeleting[i] + " удален" + "\r\n";
                            //textBox1.Text += "Файл " + FilesForDeleting[i] + " удален" + "\r\n";
                        }
                        else
                        {
                            reportLog += "Файл " + FilesForDeleting[i] + " не найде" + "\r\n";
                            //textBox1.Text += "Файл " + FilesForDeleting[i] + " не найде" + "\r\n";
                        }
                    }
                    catch
                    {
                        reportLog += "Не удалось удалить " + FilesForDeleting[i] + " файл" + "\r\n";
                        //textBox1.Text += "Не удалось удалить " + FilesForDeleting[i] + " файл" + "\r\n";
                    }
                    if (percentage < 40)
                    {
                        percentage += 5;
                    }
                }
            }
            catch
            {
                MessageBox.Show("Unexpected error appears", "Error during Simple Bot updating ",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FilesDownloading()
        {
            try
            {
                //Downloading files
                WebClient Client = new WebClient();
                for (int y = 0; y < FilesForDownload.Length; y++)
                {
                    Client.DownloadFile(FilesForDownload[y], FilesForDownloadNames[y]);
                    reportLog += "Файл " + FilesForDownloadNames[y] + " обновлен" + "\r\n";
                    //textBox1.Text += "Файл " + FilesForDownloadNames[y] + " обновлен" + "\r\n";
                    if (percentage < 89)
                    {
                        percentage += 10;
                    }
                }
                labelMessage = "Simple Bot успешно обновлен";
            }
            catch
            {
                MessageBox.Show("Unexpected error appears", "Error during Simple Bot updating ",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SimpleBotRestart ()
        {
            try
            {
                Process SimpleBotProcess = new Process();
                SimpleBotProcess.StartInfo.FileName = "Simple Bot.exe";
                SimpleBotProcess.Start();
                this.Close();
            }
            catch
            {
                MessageBox.Show("Simple Bot can't be started", "Error during Simple Bot launching ",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CheckForUpdates()
        {
            percentage += 5;
            try
            {

                if (NewBotVersion <= BotVersion)
                {
                    MessageBox.Show("You use latest version of Simple Bot", "Checking for updates",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Environment.Exit(0);
                }
            }
            catch
            {
                MessageBox.Show("Simple Bot can't check latest version", "Error during updates checking ",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClosingSimpleBotProcces()
        {
            foreach (Process clsProcess in Process.GetProcesses())
            {
                if (clsProcess.ProcessName.Contains("Simple Bot"))
                {
                    clsProcess.Kill();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            percentage = 5;
            reportLog = "Началось обновление Simple Bot'a...\r\n";
            button1.Enabled = false;
            if (backgroundWorker1.IsBusy != true)
            {
                // Start the asynchronous operation.
                backgroundWorker1.RunWorkerAsync();
            }
            //label1.Text = labelMessage;
            //button2.Visible = true;
            
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SimpleBotRestart();
            this.Close();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            //читаем контент файла
            UpdateFileReader();
            //проверяем нужно ли обновлять
            CheckForUpdates();
            FilesDeleting();
            FilesDownloading();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            textBox1.Text = reportLog;
            textBox1.SelectionStart = textBox1.Text.Length;
            textBox1.ScrollToCaret();
            progressBar1.Value = percentage;
            if (percentage == 100)
            {
                timer1.Stop();
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            label1.Text = labelMessage;
            button2.Visible = true;
            percentage = 100;
        }
    }
}
