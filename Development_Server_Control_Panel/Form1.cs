using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Development_Server_Control_Panel
{
    public partial class Form1 : Form
    {
        MySqlConnection conn = null;
        MySqlCommand sql = null;

        private List<DevServers> devServersList = new List<DevServers>();


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            DatabaseConnection();
            ShowDevServers();

            lbl_Dev1.Text = devServersList[0].ProjectName;
            lbl_Port1.Text = devServersList[0].PortNumber;
            lnk_Dev1.Text = "http://localhost:" + devServersList[0].PortNumber + "/";

            lbl_Dev2.Text = devServersList[1].ProjectName;
            lbl_Port2.Text = devServersList[1].PortNumber;
            lnk_Dev2.Text = "http://localhost:" + devServersList[1].PortNumber + "/";

            lbl_Dev3.Text = devServersList[2].ProjectName;
            lbl_Port3.Text = devServersList[2].PortNumber;
            lnk_Dev3.Text = "http://localhost:" + devServersList[2].PortNumber + "/";


            int asd = 0;

            for (int i = 0; i < devServersList.Count; i++)
            {
                asd++;
                
            }
            //MessageBox.Show("Rekords: " + num.ToString());
        }

        private void btn_Dev1_Start_Click(object sender, EventArgs e)
        {
            StartDevServer(@"W:\DataStore\Devs\laravel-sanctum-auth-api", @"/c php artisan serve");
            StartDevServer("@" + devServersList[0].ProjectDirectory + '"', @"/c php artisan serve");
        }

        private void btn_Dev1_Stop_Click(object sender, EventArgs e)
        {

        }

        private void lnk_Dev1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://localhost:8000/");
        }

        private void btn_Dev1_Start_Console_Click(object sender, EventArgs e)
        {
            StartDevConsoleServer(@"W:\DataStore\Devs\laravel-sanctum-auth-api", @"/c php artisan serve");
        }

        private void btn_Dev2_Start_Click(object sender, EventArgs e)
        {
            StartDevServer(@"W:\DataStore\Devs\oazis-react-dev", @"/c npm run start");
        }

        private void btn_Dev2_Stop_Click(object sender, EventArgs e)
        {

        }

        private void lnk_Dev2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://localhost:8000/");
        }

        private void btn_Dev2_Start_Console_Click(object sender, EventArgs e)
        {
            StartDevConsoleServer(@"W:\DataStore\Devs\oazis-react-dev", @"/c npm run start");
        }

        private void btn_Dev3_Start_Click(object sender, EventArgs e)
        {
            StartDevServer(@"W:\DataStore\Devs\slim4-loginapp\public", @"/c php -S 127.0.0.1:8000");
        }

        private void btn_Dev3_Stop_Click(object sender, EventArgs e)
        {

        }

        private void lnk_Dev3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://localhost:8000/");
        }

        private void btn_Dev3_Start_Console_Click(object sender, EventArgs e)
        {
            StartDevConsoleServer(@"W:\DataStore\Devs\slim4-loginapp\public", @"/c php -S 127.0.0.1:8000");
        }


        private void StartDevServer(string WorkingDirectory, string Command)
        {
            var proc = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            proc.StartInfo.FileName = @"C:\Windows\System32\cmd.exe";
            proc.StartInfo.WorkingDirectory = WorkingDirectory;
            proc.StartInfo.Arguments = Command;
            //proc.WaitForExit();

            // set up output redirection
            proc.StartInfo.RedirectStandardOutput = true;
            proc.StartInfo.RedirectStandardError = true;
            proc.EnableRaisingEvents = true;
            proc.StartInfo.CreateNoWindow = true;

            // see below for output handler
            proc.ErrorDataReceived += proc_DataReceived;
            proc.OutputDataReceived += proc_DataReceived;
            proc.StartInfo.UseShellExecute = false;

            proc.Start();

            proc.BeginErrorReadLine();
            proc.BeginOutputReadLine();
        }


        private void proc_DataReceived(object sender, DataReceivedEventArgs e)
        {
            // output will be in string e.Data

            if (e.Data != null)
            {
                BeginInvoke(new Action(() =>
                {
                    listBox_console.Items.Add(Environment.NewLine + e.Data);
                }));

                //if (e.Data != null)
                //BeginInvoke(new Action(() => textBox1.Text += (Environment.NewLine + e.Data)));

                //ListBox listBox_console_2 = new ListBox();
            }
        }



        private void StartDevConsoleServer(string WorkingDirectory, string Command)
        {
            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "C:\\Windows\\system32\\cmd.exe";
            startInfo.WorkingDirectory = WorkingDirectory;
            startInfo.Arguments = Command;
            process.StartInfo = startInfo;
            process.Start();
            //process.WaitForExit();
        }



        // ------- Csatlakozás az adatbázishoz -------

        private void DatabaseConnection()
        {
            MySqlConnectionStringBuilder sb = new MySqlConnectionStringBuilder();
            sb.Server = "localhost";
            sb.UserID = "root";
            sb.Password = "";
            sb.Database = "dev_server_cp";
            sb.CharacterSet = "utf8";

            conn = new MySqlConnection(sb.ToString());


            try
            {
                conn.Open();
                sql = conn.CreateCommand();

            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Database connection error!\n\n" + ex.Message + "\n\nPlease start MySQL server.",
                    "Database connection error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
            }

            //CreateDatabase();
        }


        // ------- Adatbázis táblák létrehozása -------

        private void CreateDatabase()
        {
            //sql.CommandText = @"CREATE DATABASE IF NOT EXISTS Development_Server_Control_Panel; USE Development_Server_Control_Panel;";
            //sql.ExecuteNonQuery();

            sql.CommandText = @"CREATE TABLE IF NOT EXISTS `Development_Server_Control_Panel`.`DevServers` (
                                `id` INT NOT NULL AUTO_INCREMENT,
                                `nev` VARCHAR(255) NOT NULL,
                                
                                PRIMARY KEY(`id`), UNIQUE(`nev`)) ENGINE = InnoDB;";
            sql.ExecuteNonQuery();
        }


        // ------- Show DevServers -------

        private void ShowDevServers()
        {
            sql.CommandText = "SELECT id, project_name, project_directory, start_command, port_number FROM `DevServers`";

            using (MySqlDataReader dr = sql.ExecuteReader())
            {
                while (dr.Read())
                {
                    int id = dr.GetInt32("id");
                    string projectName = dr.GetString("project_name");
                    string projectDirectory = dr.GetString("project_directory");
                    string startCommand = dr.GetString("start_command");
                    string portNumber = dr.GetString("port_number");

                    DevServers devServers = new DevServers(id, projectName, projectDirectory, startCommand, portNumber);

                    devServersList.Add(devServers);
                }
            }

            //MessageBox.Show(devServersList[0].ProjectName);
        }



        private void Save(string result)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter("console.log"))
                {
                    sw.WriteLine(result);

                    MessageBox.Show("Sikeres mentés!");
                }
            }
            catch (IOException ex)
            {
                MessageBox.Show("Hiba a fájlba mentéskor!");
            }
        }
    }
}
