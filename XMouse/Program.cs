using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.IO;
using System.Drawing;
using Whatever;

namespace XMouse
{
    static class Program
    {
        #region Globale Variable
        private static GamepadState GPS;
        private static string sFileName = Application.StartupPath + @"\App-List.ini";
        private static Thread tSticks, tButtons, tCheckApps;
        private static bool bRunThreads = true;
        private static bool bCheckAppsRunning = false;
        private static bool bLeftKey = false;
        private static bool bRightKey = false;
        private static bool bRightShoulder = false;
        private static List<string> prgList = new List<string>();
        private static bool is64BitProcess = (IntPtr.Size == 8);
        private static bool is64BitOperatingSystem = is64BitProcess || InternalCheckIsWow64();
        private static NotifyIcon ntf_Icon;
        private static ContextMenuStrip cms_Icon;
        private static ToolStripMenuItem öffnenToolStripMenuItem;
        private static ToolStripMenuItem schließenToolStripMenuItem;
        private static int iSpeed = 8;
        // Konstanten für Mausflags
        private const uint MOUSEEVENTF_LEFTDOWN = 0x0002; // linken Mausbutton drücken
        private const uint MOUSEEVENTF_LEFTUP = 0x0004; // linken Mausbutton loslassen
        private const uint MOUSEEVENTF_RIGHTDOWN = 0x0008;  //rechten Mausbutton drücken
        private const uint MOUSEEVENTF_RIGHTUP = 0x0010;  //rechten Mausbutton loslassen
        private const uint MOUSEEVENTF_WHEEL = 0x0800;     //Mausrad vertikal
        private const uint MOUSEEVENTF_HWHEEL = 0x1000;     //Mausrad horizontal
        private static Form1 startForm;
        #endregion

        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                if (is64BitOperatingSystem)
                {
                    RegistryKey reg = Registry.ClassesRoot;
                    reg = reg.OpenSubKey(@"Installer\Products\60A9912A4C987814AAA4A36967BF97D9");
                    if((int)reg.GetValue("Version") != 33554445)
                        throw new SlimDxNotFoundException("Es wurde keine Version von SlimDX(x64) gefunden!");
                }
                else
                {
                    RegistryKey reg = Registry.ClassesRoot;
                    reg = reg.OpenSubKey(@"Installer\Products\34E0DBE70CA68AC49909005E0096DA92");
                    if((int)reg.GetValue("Version") != 33554445)
                        throw new SlimDxNotFoundException("Es wurde keine Version von SlimDX(x86) gefunden!");
                }
            }
            catch (SlimDxNotFoundException e)
            {
                MessageBox.Show(e.Message);
                Application.Exit();
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            start();
            while (bRunThreads) {Application.DoEvents();}
            closing();
        }

        #region API imports
        [DllImport("user32.dll")]
        public static extern void mouse_event(uint dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        [DllImport("kernel32.dll", SetLastError = true, CallingConvention = CallingConvention.Winapi)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool IsWow64Process(
        [In] IntPtr hProcess,
        [Out] out bool wow64Process
        );
        #endregion

        public static List<string> PrgList
        {
            get { return Program.prgList; }
            set { Program.prgList = value; }
        }

        public static int ISpeed
        {
            get { return Program.iSpeed; }
            set { Program.iSpeed = value; }
        }

        private static void load()
        {
            if (File.Exists(sFileName))
            {
                StreamReader sr = new StreamReader(sFileName);
                try
                {
                    string s;
                    iSpeed = Convert.ToInt32(sr.ReadLine());
                    s = sr.ReadLine();
                    while (s != "" && s != null)
                    {
                        if (File.Exists(s))
                        {
                            PrgList.Add(s);
                        }
                        s = sr.ReadLine();
                    }
                }
                catch (Exception ee)
                {
                    MessageBox.Show(ee.Message + Environment.NewLine + ee.Source);
                }
                sr.Close();
            }
        }

        public static bool InternalCheckIsWow64()
        {
            if ((Environment.OSVersion.Version.Major == 5 && Environment.OSVersion.Version.Minor >= 1) ||
                Environment.OSVersion.Version.Major >= 6)
            {
                using (Process p = Process.GetCurrentProcess())
                {
                    bool retVal;
                    if (!IsWow64Process(p.Handle, out retVal))
                    {
                        return false;
                    }
                    return retVal;
                }
            }
            else
            {
                return false;
            }
        }

        private static void start()
        {
            GPS = new GamepadState(SlimDX.XInput.UserIndex.One);
            ntf_Icon = new System.Windows.Forms.NotifyIcon();
            cms_Icon = new System.Windows.Forms.ContextMenuStrip();
            öffnenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            schließenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ntf_Icon.Icon = XMouse.Properties.Resources.xbox360;
            ntf_Icon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            ntf_Icon.BalloonTipText = "Controlls the mouse through pad. Double click to open config.";
            ntf_Icon.BalloonTipTitle = "XMouse";
            ntf_Icon.ContextMenuStrip = cms_Icon;
            ntf_Icon.Text = "XMouse";
            ntf_Icon.Visible = true;
            ntf_Icon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(ntf_Icon_MouseDoubleClick);
            cms_Icon.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            öffnenToolStripMenuItem,
            schließenToolStripMenuItem});
            cms_Icon.Name = "cms_Icon";
            cms_Icon.Size = new System.Drawing.Size(126, 48);
            öffnenToolStripMenuItem.Name = "öffnenToolStripMenuItem";
            öffnenToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            öffnenToolStripMenuItem.Text = "Öffnen";
            öffnenToolStripMenuItem.Click += new System.EventHandler(öffnenToolStripMenuItem_Click);
            schließenToolStripMenuItem.Name = "schließenToolStripMenuItem";
            schließenToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            schließenToolStripMenuItem.Text = "Schließen";
            schließenToolStripMenuItem.Click += new System.EventHandler(schließenToolStripMenuItem_Click);
            GPS = new GamepadState(SlimDX.XInput.UserIndex.One);
            tSticks = new Thread(CheckControllerSticks);
            tSticks.Start();
            tButtons = new Thread(CheckControllerButtons);
            tButtons.Start();
            tCheckApps = new Thread(CheckAppsRunning);
            tCheckApps.Start();
        }

        private static void ntf_Icon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (startForm == null)
                startForm = new Form1();
            ntf_Icon.Visible = false;
        }

        private static void öffnenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (startForm == null)
            {
                startForm = new Form1();
                startForm.Show();
            }
        }

        private static void schließenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (startForm != null)
                startForm.Close();
            bRunThreads = false;
            Application.Exit();
        }

        private static void CheckAppsRunning()
        {
            bool bCheck;
            while (bRunThreads)
            {
                bCheck = false;
                for (int i = 0; i < prgList.Count; i++)
                {
                    String s = prgList[i];
                    if (IsProcessOpen(s.Substring(s.LastIndexOf(@"\") + 1, (s.LastIndexOf(".") - 1) - s.LastIndexOf(@"\"))))
                        bCheck = true;
                }
                if (bCheck)
                    bCheckAppsRunning = true;
                else
                    bCheckAppsRunning = false;
                Thread.Sleep(800);
            }
        }

        private static void CheckControllerButtons()
        {
            while (bRunThreads)
            {
                if (!bCheckAppsRunning)
                {
                    GPS.Update();
                    SimulateLeftMouseClick(GPS.A);
                    SimulateRightMouseClick(GPS.B);
                    if (GPS.RightStick.Position.Y != 0)
                    {
                        mouse_event(MOUSEEVENTF_WHEEL, 0, 0, Convert.ToInt32(GPS.RightStick.Position.Y * 120), 0);
                    }
                    if (GPS.RightStick.Position.X != 0)
                    {
                        mouse_event(MOUSEEVENTF_HWHEEL, 0, 0, Convert.ToInt32(GPS.RightStick.Position.X * 120), 0);
                    }
                    if (GPS.RightShoulder)
                    {
                        if (!bRightShoulder)
                        {
                            bRightShoulder = true;
                            StartOSK();
                        }
                    }
                    else
                        bRightShoulder = false;
                }
                Thread.Sleep(10);
            }
        }

        private static void CheckControllerSticks()
        {
            while (bRunThreads)
            {
                if (!bCheckAppsRunning)
                {
                    GPS.Update();
                    Cursor.Position = new Point(Cursor.Position.X + (Convert.ToInt32(GPS.LeftStick.Position.X * iSpeed)),
                                                Cursor.Position.Y + (Convert.ToInt32(GPS.LeftStick.Position.Y * -iSpeed)));
                    Thread.Sleep(8);
                }
            }
        }

        private static void closing()
        {
            try
            {
                if (startForm != null)
                    startForm.Close();
                if (File.Exists(sFileName))
                    File.Delete(sFileName);
                StreamWriter sw = new StreamWriter(sFileName, false);
                sw.WriteLine(iSpeed.ToString());
                if (prgList.Count >= 1)
                    foreach (string s in prgList)
                        sw.WriteLine(s);
                sw.Close();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }

        private static bool IsProcessOpen(string name)
        {
            foreach (Process clsProcess in Process.GetProcesses())
            {
                if (clsProcess.ProcessName.Contains(name))
                {
                    return true;
                }
            }
            return false;
        }

        private static void SimulateLeftMouseClick(bool b)
        {
            if (!bLeftKey && b)
            {
                mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
                bLeftKey = true;
            }
            if (bLeftKey && !b)
            {
                mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                bLeftKey = false;
            }
        }

        private static void SimulateRightMouseClick(bool b)
        {
            if (!bRightKey && b)
            {
                mouse_event(MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, 0);
                bRightKey = true;
            }
            if (bRightKey && !b)
            {
                mouse_event(MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);
                bRightKey = false;
            }
        }

        private static void StartOSK()
        {
            string windir = Environment.GetEnvironmentVariable("WINDIR");
            string osk = null;

            if (Process.GetProcessesByName("osk").Length > 0)
            {
                Process.GetProcessesByName("osk")[0].Kill();
            }
            else
            {
                if (osk == null)
                {
                    osk = Path.Combine(Path.Combine(windir, "sysnative"), "osk.exe");
                    if (!File.Exists(osk))
                        osk = null;
                }

                if (osk == null)
                {
                    osk = Path.Combine(Path.Combine(windir, "system32"), "osk.exe");
                    if (!File.Exists(osk))
                    {
                        osk = null;
                    }
                }

                if (osk == null)
                    osk = "osk.exe";

                Process.Start(osk);
            }
        }
    }
}
