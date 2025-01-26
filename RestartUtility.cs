using System;
using System.Diagnostics;
using System.Windows.Forms;
using System.Media;
using System.IO;

namespace RestartUtility
{
    public partial class RestartDialog : Form 
    {
        public RestartDialog()
        {
            JUtil.LogInfo("RSU.Dialog", "RestartUtility initiated by: " + Environment.UserName);
            InitializeComponent();
            SystemSounds.Exclamation.Play();
        }


        private void btnNo_Click(object sender, EventArgs e)
        {
            JUtil.LogInfo("RSU.NoBtnClick", "Restart was canceled!");
            Application.Exit();
        }

        private void btnYes_Click(object sender, EventArgs e)
        {
            JUtil.LogInfo("RSU.YesBtnClick", "Restart process started...");
            string s = Environment.ExpandEnvironmentVariables("%windir%\\Media\\Windows Shutdown.wav");
            try {
                new SoundPlayer(s).PlaySync();
            } catch(Exception ex) {
                JUtil.LogError("RSU.PlayShutdownWav", "Error:" + ex.Message);
            }

            ProcessStartInfo p = new ProcessStartInfo();            
            p.WindowStyle = ProcessWindowStyle.Hidden;
            p.CreateNoWindow = true;
            string x = Environment.ExpandEnvironmentVariables("%windir%\\System32\\shutdown.exe");
            string a = "-r -t 0";
            if (File.Exists(x))
            {
                p.FileName = x;
                if (chkForce.Checked) a = "-f " + a;
            }
            else
            {
                p.FileName = "cmd";
                a = "/C shutdown " + a;
            }            
            p.Arguments = a;
            JUtil.LogInfo("RSU.ProcInfo", "FileName= " + p.FileName);
            JUtil.LogInfo("RSU.ProcInfo", "Arguments= " + p.Arguments);
            JUtil.LogInfo("RSU.Process", "Calling ProcessStart()...");
            Process proc = Process.Start(p);
            proc.WaitForExit();
            if (proc.HasExited)
                JUtil.LogInfo("RSU.Process", "ExitCode=" + proc.ExitCode);
            Application.Exit();
            JUtil.LogInfo("RSU.Dialog", "Application.Exit() called...");
        }
    }

    internal class JUtil
    {
        static string _INFO = "[info]";
        static string _ERROR = "[error]";
        private static JUtil _instance = null;
        static JUtil Logger { get { if (_instance == null) _instance = new JUtil(); return _instance; } }
        public static void LogInfo(string caller, string remarks) { JUtil.Logger.Write("{0}\t{1}\t{2}\t{3}", DateTime.Now, _INFO, caller, remarks); }
        public static void LogError(string caller, string remarks) { JUtil.Logger.Write("{0}\t{1}\t{2}\t{3}", DateTime.Now, _ERROR, caller, remarks); }

        private string currDir = null;
        private string logFilePath = null;
        private TextWriter logWriter = null;
        JUtil(string logFP = null)
        {
            this.currDir = AppDomain.CurrentDomain.BaseDirectory;
            this.logFilePath = (logFP == null) ? currDir + "rsu_debug.log" : logFP;
            this.logWriter = new StreamWriter(this.logFilePath, true);
        }

        void Write(string fmtdString, params object[] args)
        {
            this.logWriter.WriteLine(fmtdString, args);
            this.logWriter.Flush();
        }

    }
}
