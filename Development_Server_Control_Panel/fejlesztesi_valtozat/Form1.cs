using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        static List<string> msg = new List<string>();

        private void Form1_Load(object sender, EventArgs e)
        {
            //ListView listView1 = new ListView();

            // When the enclosing form loads, add three string items to the ListView.
            // ... Use an array of strings.
            //string[] array = { "cat", "dog", "mouse" };
            //var items = listView1.Items;
            //foreach (var value in array)
            //{
            //    items.Add(value);
            //}
            //this.Controls.Add(listView1);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //System.Diagnostics.Process.Start("cmd.exe", @"/k ""cd /d W:\DataStore\Devs\oazis-react-dev""");

            //Console.In.Read()

            //    string cmdexePath = @"C:\Windows\System32\cmd.exe";
            //    //notice the quotes around the below string...
            //    string myApplication = "\"W:\\DataStore\\Devs\\oazis-react-dev npm run start\"";
            //    //the /K keeps the CMD window open - even if your windows app closes
            //    string cmdArguments = String.Format("/K {0}", myApplication);
            //    ProcessStartInfo psi = new ProcessStartInfo(cmdexePath, cmdArguments);
            //    Process p = new Process();
            //    p.StartInfo = psi;
            //    p.Start();


            //string strCmdLine =
            //    "W:\\DataStore\\Devs\\oazis-react-dev  " +
            //    "npm run start\"";

            //System.Diagnostics.Process.Start("CMD.exe", strCmdLine);
            //process1.Close();


            //var proc1 = new ProcessStartInfo();
            //        string anyCommand;
            //        proc1.UseShellExecute = true;

            //        proc1.WorkingDirectory = @"W:\DataStore\Devs\oazis-react-dev";
            //        //proc1.FileName = @"npm run start";

            //        proc1.FileName = @"C:\Windows\System32\cmd.exe";
            //        proc1.Verb = "runas";
            //        proc1.Arguments = "npm run start";
            //        proc1.WindowStyle = ProcessWindowStyle.Normal;
            //        Process.Start(proc1);




            RunWithRedirect(@"C:\Windows\System32\cmd.exe");


            //Process p = new Process();
            //p.StartInfo.FileName = "C:\\Windows\\system32\\cmd.exe";
            //p.StartInfo.WorkingDirectory = @"W:\DataStore\Devs\oazis-react-dev";
            //p.StartInfo.Arguments = "dir";

            //p.Start();



            // Működik
            //Process process = new Process();
            //ProcessStartInfo startInfo = new ProcessStartInfo();
            //startInfo.FileName = "C:\\Windows\\system32\\cmd.exe";
            //startInfo.WorkingDirectory = @"W:\DataStore\Devs\oazis-react-dev";
            //startInfo.Arguments = "/c npm run start";
            //process.StartInfo = startInfo;
            //process.Start();
            //process.WaitForExit();





            void RunWithRedirect(string cmdPath)
            {

                var proc = new Process();
                ProcessStartInfo startInfo = new ProcessStartInfo();


                proc.StartInfo.FileName = @"C:\Windows\System32\cmd.exe";
                proc.StartInfo.WorkingDirectory = @"W:\DataStore\Devs\oazis-react-dev";
                proc.StartInfo.Arguments = "/c npm run start";
                //proc.StartInfo = startInfo;
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

                //proc.StandardInput.WriteLine(@"cd W:\DataStore\Devs\oazis-react-dev");

                proc.BeginErrorReadLine();
                proc.BeginOutputReadLine();

                //proc.WaitForExit();
            }
        }


        void proc_DataReceived(object sender, DataReceivedEventArgs e)
        {
            // output will be in string e.Data


            if (e.Data != null)
            {
                //
                if (e.Data.Contains("asset"))
                {
                    
                    BeginInvoke(new Action(() => {
                        listBox1.ForeColor = Color.DarkGreen;
                        listBox1.Items.Add(Environment.NewLine + e.Data);
                        
                    }));
                }
                else
                {
                    BeginInvoke(new Action(() => {
                        
                        listBox1.ForeColor = Color.Black;
                    }));
                }
                
                //BeginInvoke(new Action(() => listBox1.Items.Add(Environment.NewLine + e.Data)));

                //msg.Add(e.Data);


                //string message = string.Join(Environment.NewLine, msg);

                //BeginInvoke(new Action(() => listBox1.Items.Add(message)));


                //BeginInvoke(new Action(() => textBox1.Text += (Environment.NewLine + e.Data)));


                //MessageBox.Show(message);
            }
            


            //MessageBox.Show(e.Data + Environment.NewLine);

            //textBox1.Text = "http://csharp.net-informations.com";

            //string text = "http://csharp.net-informations.com";

            //if(textBox1.InvokeRequired)
            //{
            //    textBox1.Invoke(new MethodInvoker(delegate { text = textBox1.Text; }));
            //}
            //else
            //{
            //    this.textBox1.Text = e.Data;
            //}

            //Invoke(new Action(() =>
            //{
            //    textBox1.Text = "http://csharp.net-informations.com";
            //}
            //));


            //Action<string> DelegateTeste_ModifyText = THREAD_MOD;
            //Invoke(DelegateTeste_ModifyText, "MODIFY BY THREAD");













            //ListBox mylist = new ListBox();
            //mylist.Items.Add("GeeksForGeeks");
            //Controls.Add(mylist);

            //ListView listView1 = new ListView();
            //string[] array = { "cat", "dog", "mouse" };
            //var items = listView1.Items;
            //foreach (var value in array)
            //{
            //    items.Add(value);
            //}
            //this.Controls.Add(listView1);


            //    StringBuilder MessageText = new StringBuilder();
            //    MessageText.AppendLine(e.Data);
            //    DialogResult result1 = MessageBox.Show(MessageText.ToString() + "Information is correct?",
            //"Double Check Form Information",
            //MessageBoxButtons.YesNoCancel,
            //MessageBoxIcon.Question);


            //msg.Add(e.Data);



            //string message = string.Join(Environment.NewLine, msg);

            //MessageBox.Show(message);



        }

        //private void THREAD_MOD(string teste)
        //{
        //    textBox1.Text = teste;
        //}








    }
}
