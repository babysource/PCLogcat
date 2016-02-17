using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Management;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace PCLogcat
{
    public partial class LogMain : Form
    {

        private static Regex LEVEL_REGEX = new Regex("\\[-*\\d+\\] \\| .+ - ([V|D|I|W|E])/.+: .+ \\r?\\n");

        private int linecount;

        private int drawindex;

        private int findindex;

        private bool isListen;

        private bool isSearch;

        private bool isLocker;

        private string filestore;

        private string cachefile;

        private string crashfile;

        private string droidpath;

        private Process droidplay;

        private Queue<string> queueShow = new Queue<string>();

        public LogMain()
        {
            InitializeComponent();
        }

        private void LogMain_Load(object sender, EventArgs e)
        {
            this.droidpath = ConfigurationManager.AppSettings["adb"];
            if (File.Exists(this.droidpath))
            {
                if (!Directory.Exists(this.filestore = Environment.CurrentDirectory + "\\logcat\\"))
                {
                    Directory.CreateDirectory(this.filestore);
                }
                return;
            }
            else
            {
                MessageBox.Show("请正确配置ADB程序路径！");
            }
            Environment.Exit(0);
        }

        private void startListen(string filename, string filetime)
        {
            this.linecount = 0;
            this.drawindex = 0;
            this.findindex = 0;
            this.writeLog.Clear();
            try
            {
                if (this.droidplay == null || this.droidplay.HasExited)
                {
                    this.droidplay = new Process();
                    this.droidplay.StartInfo.FileName = this.droidpath;
                    this.droidplay.StartInfo.Arguments = "logcat";
                    this.droidplay.EnableRaisingEvents = true;
                    this.droidplay.StartInfo.CreateNoWindow = true;
                    this.droidplay.StartInfo.UseShellExecute = false;
                    this.droidplay.StartInfo.RedirectStandardError = true;
                    this.droidplay.StartInfo.RedirectStandardOutput = true;
                    this.droidplay.StartInfo.StandardErrorEncoding = Encoding.UTF8;
                    this.droidplay.StartInfo.StandardOutputEncoding = Encoding.UTF8;
                    this.droidplay.ErrorDataReceived += new DataReceivedEventHandler(adb_ErrorDataReceived);
                    this.droidplay.OutputDataReceived += new DataReceivedEventHandler(adb_OutputDataReceived);
                }
            }
            catch (Exception e)
            {
                this.closeListen();
                Console.WriteLine("Start Listen init error: " + e.ToString());
            }
            finally
            {
                if (this.droidplay != null)
                {
                    try
                    {
                        this.droidplay.Start();
                        this.droidplay.BeginErrorReadLine();
                        this.droidplay.BeginOutputReadLine();
                    }
                    catch (Exception e)
                    {
                        this.closeListen();
                        Console.WriteLine("Start Listen todo error: " + e.ToString());
                    }
                    finally
                    {
                        if (this.droidplay != null)
                        {
                            this.isListen = true;
                            this.lockBtn.Enabled = true;
                            this.logcatBtn.Enabled = true;
                            this.writeTimer.Enabled = true;
                            this.logcatBtn.Text = "停 止 监 听";
                            this.logcatBtn.BackColor = Color.Red;
                            this.logcatBtn.ForeColor = Color.Yellow;
                            this.cachefile = this.filestore + filename + ".stdout_" + filetime + ".log";
                            this.crashfile = this.filestore + filename + ".stderr_" + filetime + ".log";
                        }
                    }
                }
            }
        }

        private void clearListen()
        {
            this.logcatBtn.Enabled = false;
            try
            {
                if (this.droidplay == null || this.droidplay.HasExited)
                {
                    this.droidplay = new Process();
                    this.droidplay.StartInfo.FileName = this.droidpath;
                    this.droidplay.StartInfo.Arguments = "logcat -c";
                    this.droidplay.EnableRaisingEvents = true;
                    this.droidplay.StartInfo.CreateNoWindow = true;
                    this.droidplay.StartInfo.UseShellExecute = false;
                }
            }
            catch (Exception e)
            {
                this.closeListen();
                Console.WriteLine("Clear Listen init error: " + e.ToString());
            }
            finally
            {
                if (this.droidplay != null)
                {
                    try
                    {
                        this.droidplay.Start();
                    }
                    catch (Exception e)
                    {
                        this.closeListen();
                        Console.WriteLine("Clear Listen todo error: " + e.ToString());
                    }
                    finally
                    {
                        if (this.droidplay != null)
                        {
                            try
                            {
                                this.droidplay.WaitForExit(30000);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("Clear Listen wait error: " + e.ToString());
                            }
                            finally
                            {
                                this.closeListen();
                            }
                        }
                    }
                }
            }
        }

        private void closeListen()
        {
            if (this.droidplay != null)
            {
                try
                {
                    this.killTreeProcess(this.droidplay.Id);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Close Listen todo error: " + e.ToString());
                }
                finally
                {
                    this.droidplay = null;
                }
            }
            this.isListen = false;
            this.queueShow.Clear();
            this.lockBtn.Enabled = false;
            this.logcatBtn.Enabled = true;
            this.writeTimer.Enabled = false;
            this.logcatBtn.Text = "开 始 监 听";
            this.logcatBtn.BackColor = Color.Green;
            this.logcatBtn.ForeColor = Color.White;
        }

        private void killTreeProcess(int pid)
        {
            try
            {
                Process cmd = Process.GetProcessById(pid);
                if (cmd != null)
                {
                    cmd.Kill();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Close Listen tree error: " + e.ToString());
            }
            finally
            {
                ManagementObjectSearcher mos = new ManagementObjectSearcher("Select ProcessID From Win32_Process Where ParentProcessID=" + pid);
                if (mos != null)
                {
                    ManagementObjectCollection moc = mos.Get();
                    if (moc != null && moc.Count > 0)
                    {
                        foreach (ManagementObject moo in moc)
                        {
                            if (moo != null)
                            {
                                this.killTreeProcess(Convert.ToInt32(moo["ProcessID"]));
                            }
                        }
                    }
                }
            }
        }

        private void lockBtn_Click(object sender, EventArgs e)
        {
            if (!this.isLocker)
            {
                this.isLocker = true;
                this.lockBtn.Image = global::PCLogcat.Properties.Resources.onlock;
            }
            else
            {
                this.isLocker = false;
                this.lockBtn.Image = global::PCLogcat.Properties.Resources.unlock;
            }
        }

        private void logcatBtn_Click(object sender, EventArgs e)
        {
            if (this.isListen)
            {
                if (this.isLocker)
                {
                    this.lockBtn.PerformClick();
                }
                this.closeListen();
            }
            else
            {
                String filename = Interaction.InputBox("请输入名称：", "日志名称", "", -1, -1);
                if (!"".Equals(filename))
                {
                    this.closeListen();
                    this.clearListen();
                    this.startListen(filename, DateTime.Now.ToFileTime() + "");
                }
            }
        }

        private void queryBtn_Click(object sender, EventArgs e)
        {
            if (this.isSearch)
            {
                this.findindex = 0;
                this.drawindex = 0;
                this.queryTxt.Text = "";
                this.queryTxt.Cursor = Cursors.IBeam;
                this.queryTxt.ReadOnly = (this.isSearch = false);
                this.adb_ColourLogDataReceived();
            }
            else
            {
                if (!String.IsNullOrWhiteSpace(this.queryTxt.Text))
                {
                    this.queryTxt.Cursor = Cursors.Default;
                    this.queryTxt.ReadOnly = (this.isSearch = true);
                }
                this.adb_SearchLogDataReceived();
            }
            this.queryBtn.Text = this.isSearch ? "停 止 查 找" : "开 始 查 找";
        }

        private void writeLog_Focus(object sender, EventArgs e)
        {
            this.queryTxt.Focus();
        }

        private void adb_ColourLogDataReceived()
        {
            MatchCollection match = LEVEL_REGEX.Matches(this.writeLog.Text, this.drawindex);
            if (match != null && match.Count > 0)
            {
                foreach (Match rs in match)
                {
                    if (rs != null && rs.Success)
                    {
                        this.drawindex = rs.Index + rs.Length;
                        this.writeLog.Select(rs.Index, rs.Length);
                        this.writeLog.SelectionBackColor = Color.Black;
                        this.writeLog.SelectionFont = new Font(
                            this.writeLog.Font, FontStyle.Regular
                        );
                        switch (rs.Groups[1].Value)
                        {
                            case "W":
                                {
                                    this.writeLog.SelectionColor = Color.Yellow;
                                    break;
                                }
                            case "D":
                                {
                                    this.writeLog.SelectionColor = Color.Green;
                                    break;
                                }
                            case "E":
                                {
                                    this.writeLog.SelectionColor = Color.Red;
                                    break;
                                }
                            default:
                                {
                                    this.writeLog.SelectionColor = Color.White;
                                    break;
                                }
                        }
                    }
                }
            }
        }

        private void adb_SearchLogDataReceived()
        {
            if (this.isSearch)
            {
                Regex regex = new Regex(this.queryTxt.Text, RegexOptions.IgnoreCase);
                if (regex != null)
                {
                    MatchCollection match = regex.Matches(this.writeLog.Text, this.findindex);
                    if (match != null && match.Count > 0)
                    {
                        foreach (Match rs in match)
                        {
                            if (rs != null && rs.Success)
                            {
                                this.findindex = rs.Index + rs.Length;
                                this.writeLog.Select(rs.Index, rs.Length);
                                this.writeLog.SelectionColor = Color.Black;
                                this.writeLog.SelectionBackColor = Color.Yellow;
                                this.writeLog.SelectionFont = new Font(this.writeLog.Font, FontStyle.Bold);
                            }
                        }
                    }
                }
            }
        }

        private string adb_FormatOutDataReceived(string data)
        {
            StringBuilder format = new StringBuilder(2048);
            if (!String.IsNullOrWhiteSpace(data))
            {
                format.Append("[").Append(((++this.linecount) + "").PadLeft(10, '-')).Append("] | ").Append(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff")).Append(" - ").Append(data.Trim()).Append(" ").Append(Environment.NewLine);
            }
            return format.ToString();
        }

        private void adb_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (!this.InvokeRequired)
            {
                string stderr = this.adb_FormatOutDataReceived(e.Data);
                if (!String.IsNullOrWhiteSpace(stderr))
                {
                    File.AppendAllText(this.crashfile, stderr);
                }
            }
            else
            {
                this.Invoke(new Action<object, DataReceivedEventArgs>(adb_ErrorDataReceived), sender, e);
            }
        }

        private void adb_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (!this.InvokeRequired)
            {
                string stdout = this.adb_FormatOutDataReceived(e.Data);
                if (!String.IsNullOrWhiteSpace(stdout))
                {
                    this.queueShow.Enqueue(stdout);
                }
            }
            else
            {
                this.Invoke(new Action<object, DataReceivedEventArgs>(adb_OutputDataReceived), sender, e);
            }
        }

        private void writeTimer_Tick(object sender, EventArgs e)
        {
            if (this.isListen && this.queueShow.Count > 0)
            {
                StringBuilder output = new StringBuilder(5120);
                for (int i = 0; i < 300; i++)
                {
                    if (this.queueShow.Count > 0)
                    {
                        output.Append(this.queueShow.Dequeue());
                        continue;
                    }
                    break;
                }
                // 文本输出
                string append = output.ToString();
                try
                {
                    File.AppendAllText(this.cachefile, append);
                }
                catch (Exception)
                {
                    append = null;
                }
                finally
                {
                    if (append != null)
                    {
                        this.writeLog.AppendText(append);
                        this.adb_ColourLogDataReceived();
                        this.adb_SearchLogDataReceived();
                        if (!this.isLocker)
                        {
                            this.writeLog.ScrollToCaret();
                        }
                    }
                }
            }
        }

    }
}
