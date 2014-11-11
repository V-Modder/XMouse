using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using GlacialComponents.Controls;
using ULTRAMONLib;
using Whatever;

namespace XMouse
{
    public partial class Form1 : Form
    {
        #region Global objects
        //Vars
        private Thread tContrllers;
        private ImageList iList;

        private MySettings settings;
        private GamepadState gPS;
        private Thread tSticks, tButtons, tCheckApps;
        private bool bRunThreads = true;
        private bool bCheckAppsRunning = false;
        private bool bLeftKey = false;
        private bool bRightKey = false;
        private bool bRightShoulder = false;
        private bool bRightTrigger = false;
        private bool bLeftShoulder = false;
        private bool bLeftTrigger = false;
        private bool bClose = false;
        private bool bCheckController = false;

        // Konstanten für Mausflags
        private const uint MOUSEEVENTF_LEFTDOWN = 0x0002; // linken Mausbutton drücken
        private const uint MOUSEEVENTF_LEFTUP = 0x0004; // linken Mausbutton loslassen
        private const uint MOUSEEVENTF_RIGHTDOWN = 0x0008;  //rechten Mausbutton drücken
        private const uint MOUSEEVENTF_RIGHTUP = 0x0010;  //rechten Mausbutton loslassen
        private const uint MOUSEEVENTF_WHEEL = 0x0800;     //Mausrad vertikal
        private const uint MOUSEEVENTF_HWHEEL = 0x1000;     //Mausrad horizontal
        private const int WM_QUERYENDSESSION = 0x11;
        #endregion

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

        protected override void WndProc(ref Message m)
        {
            // Listen for operating system messages.
            switch (m.Msg)
            {
                case WM_QUERYENDSESSION:
                    bClose = true;
                    this.Close();
                    break;
                //case WM_ENDSESSION:
                //    break;
            }
            base.WndProc(ref m);
        }

        public Form1()
        {
            try
            {
                settings = MySettings.Load();
                InitializeComponent();
                this.lbl_speed_max.Text = this.trb_Speed.Maximum.ToString();
                start();
                ntf_Icon.Icon = XMouse.Properties.Resources.xbox360;
                iList = new ImageList();
                gla_Apps.ImageList = iList;
                this.WindowState = FormWindowState.Minimized;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void start()
        {
            gPS = new GamepadState(SlimDX.XInput.UserIndex.One);
            gPS = new GamepadState(SlimDX.XInput.UserIndex.One);
            tSticks = new Thread(CheckControllerSticks);
            tSticks.Start();
            tButtons = new Thread(CheckControllerButtons);
            tButtons.Start();
            tCheckApps = new Thread(CheckAppsRunning);
            tCheckApps.Start();
        }

        private void ntf_Icon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
            this.Show();
        }

        private void öffnenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
            this.Show();
        }

        private void schließenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bClose = true;
            this.Close();
        }

        private void CheckAppsRunning()
        {
            bool bCheck;
            while (bRunThreads)
            {
                bCheck = false;
                foreach (string s in settings.PrgList)
                {
                    if (IsProcessOpen(s.Substring(s.LastIndexOf(@"\") + 1, (s.LastIndexOf(".") - 1) - s.LastIndexOf(@"\"))))
                        bCheck = true;
                }
                if (bCheck)
                {
                    bCheckAppsRunning = true;
                }
                else
                {
                    bCheckAppsRunning = false;
                }
                Thread.Sleep(800);
            }
        }

        private void CheckControllerButtons()
        {
            while (bRunThreads)
            {
                if (!bCheckAppsRunning)
                {
                    gPS.Update();
                    SimulateLeftMouseClick(gPS.A);
                    SimulateRightMouseClick(gPS.B);
                    if (gPS.RightStick.Position.Y != 0)
                    {
                        mouse_event(MOUSEEVENTF_WHEEL, 0, 0, Convert.ToInt32(gPS.RightStick.Position.Y * 120), 0);
                    }
                    if (gPS.RightStick.Position.X != 0)
                    {
                        mouse_event(MOUSEEVENTF_HWHEEL, 0, 0, Convert.ToInt32(gPS.RightStick.Position.X * 120), 0);
                    }
                    if (gPS.RightTrigger >= 0.75)
                    {
                        if (!bRightTrigger)
                        {
                            bRightTrigger = true;
                            StartOSK();
                        }
                    }
                    else
                        bRightTrigger = false;
                    if (gPS.LeftTrigger >= 0.75)
                    {
                        if (!bLeftTrigger)
                        {
                            bLeftTrigger = true;
                            ChangePrimaryMonitor();
                        }
                    }
                    else
                        bLeftTrigger = false;
                    if (gPS.RightShoulder)
                    {
                        if (!bRightShoulder)
                        {
                            bRightShoulder = true;
                            SendKeys.SendWait("%{RIGHT}");
                        }
                    }
                    else
                        bRightShoulder = false;
                    if (gPS.LeftShoulder)
                    {
                        if (!bLeftShoulder)
                        {
                            bLeftShoulder = true;
                            SendKeys.SendWait("{BACKSPACE}");
                        }
                    }
                    else
                        bLeftShoulder = false;
                }
                Thread.Sleep(10);
            }
        }

        private void ChangePrimaryMonitor()
        {
            IUltraMonSystem2 sys = new UltraMonSystem();
            foreach (IUltraMonMonitor mon in sys.Monitors)
            {
                if (mon.Primary == false)
                {
                    mon.Primary = true;
                    break;
                }
            }
            sys.ApplyMonitorChanges();
        }

        private void CheckControllerSticks()
        {
            while (bRunThreads)
            {
                if (!bCheckAppsRunning)
                {
                    gPS.Update();
                    Cursor.Position = new Point(Cursor.Position.X + (Convert.ToInt32(gPS.LeftStick.Position.X * settings.ISpeed)),
                                                Cursor.Position.Y + (Convert.ToInt32(gPS.LeftStick.Position.Y * -settings.ISpeed)));
                    Thread.Sleep(8);
                }
            }
        }

        private bool IsProcessOpen(string name)
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

        private void SimulateLeftMouseClick(bool b)
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

        private void SimulateRightMouseClick(bool b)
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

        private void StartOSK()
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

        private void btn_delete_Click(object sender, EventArgs e)
        {
            System.Collections.ArrayList al = gla_Apps.Items.SelectedItems;
            foreach (GLItem o in al)
            {
                settings.PrgList.Remove(o.SubItems[1].Text);
                gla_Apps.Items.Remove(o);
                gla_Apps.Invalidate();
            }
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            Add_App AddForm = new Add_App();
            AddForm.ShowDialog(this);
            if (File.Exists(AddForm.Path))
            {
                settings.PrgList.Add(AddForm.Path);
                GLSubItem sub1 = new GLSubItem();
                iList.Images.Add(AddForm.Path, AddForm.appIcon.ToBitmap());
                sub1.ImageIndex = iList.Images.IndexOfKey(AddForm.Path);
                GLSubItem sub2 = new GLSubItem();
                sub2.Text = AddForm.Path;
                GLItem itm = new GLItem();
                itm.SubItems.AddRange(new GlacialComponents.Controls.GLSubItem[] { sub1, sub2 });
                gla_Apps.Items.Add(itm);
                gla_Apps.Update();
                gla_Apps.Invalidate();
            }
        }

        private void cb_Controllers_SelectedIndexChanged(object sender, EventArgs e)
        {

            switch (cb_Controllers.SelectedIndex)
            {
                case 0:
                    gPS = new GamepadState(SlimDX.XInput.UserIndex.One);
                    break;
                case 1:
                    gPS = new GamepadState(SlimDX.XInput.UserIndex.Two);
                    break;
                case 2:
                    gPS = new GamepadState(SlimDX.XInput.UserIndex.Three);
                    break;
                case 3:
                    gPS = new GamepadState(SlimDX.XInput.UserIndex.Four);
                    break;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (bClose)
            {
                bRunThreads = false;
                this.settings.Save();
                e.Cancel = false;
            }
            else if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.WindowState = FormWindowState.Minimized;
            }
        }

        private void CheckControllers()
        {
            GamepadState gps;
            bool bstart = true;
            while (bRunThreads)
            {
                //One
                gps = new GamepadState(SlimDX.XInput.UserIndex.One);
                if (gps.Connected)
                {
                    if (!cb_Controllers.Items.Contains(gps.UserIndex.ToString()))
                    {
                        MethodInvoker LabelUpdate = delegate
                        {
                            cb_Controllers.Items.Add(gps.UserIndex.ToString());
                            if (cb_Controllers.Items.Count == 1)
                                cb_Controllers.SelectedIndex = 0;
                        };
                        Invoke(LabelUpdate);
                    }
                }
                else
                {
                    if (cb_Controllers.Items.Contains(gps.UserIndex.ToString()))
                    {
                        MethodInvoker LabelUpdate = delegate
                        {
                            cb_Controllers.Items.Remove(gps.UserIndex.ToString());
                            if (cb_Controllers.Items.Count == 0)
                            {
                                cb_Controllers.Text = "No controller";
                            }
                        };
                        Invoke(LabelUpdate);
                    }
                }

                //Two
                gps = new GamepadState(SlimDX.XInput.UserIndex.Two);
                if (gps.Connected)
                {
                    if (!cb_Controllers.Items.Contains(gps.UserIndex.ToString()))
                    {
                        MethodInvoker LabelUpdate = delegate
                        {
                            cb_Controllers.Items.Add(gps.UserIndex.ToString());
                            if (cb_Controllers.Items.Count == 1)
                                cb_Controllers.SelectedIndex = 0;
                        };
                        Invoke(LabelUpdate);
                    }
                }
                else
                {
                    if (cb_Controllers.Items.Contains(gps.UserIndex.ToString()))
                    {
                        MethodInvoker LabelUpdate = delegate
                        {
                            cb_Controllers.Items.Remove(gps.UserIndex.ToString());
                            if (cb_Controllers.Items.Count == 0)
                                cb_Controllers.Text = "No controller";
                        };
                        Invoke(LabelUpdate);
                    }
                }

                //Three
                gps = new GamepadState(SlimDX.XInput.UserIndex.Three);
                if (gps.Connected)
                {
                    if (!cb_Controllers.Items.Contains(gps.UserIndex.ToString()))
                    {
                        MethodInvoker LabelUpdate = delegate
                        {
                            cb_Controllers.Items.Add(gps.UserIndex.ToString());
                            if (cb_Controllers.Items.Count == 1)
                                cb_Controllers.SelectedIndex = 0;
                        };
                        Invoke(LabelUpdate);
                    }
                }
                else
                {
                    if (cb_Controllers.Items.Contains(gps.UserIndex.ToString()))
                    {
                        MethodInvoker LabelUpdate = delegate
                        {
                            cb_Controllers.Items.Remove(gps.UserIndex.ToString());
                            if (cb_Controllers.Items.Count == 0)
                                cb_Controllers.Text = "No controller";
                        };
                        Invoke(LabelUpdate);
                    }
                }

                //Four
                gps = new GamepadState(SlimDX.XInput.UserIndex.Four);
                if (gps.Connected)
                {
                    if (!cb_Controllers.Items.Contains(gps.UserIndex.ToString()))
                    {
                        MethodInvoker LabelUpdate = delegate
                        {
                            cb_Controllers.Items.Add(gps.UserIndex.ToString());
                            if (cb_Controllers.Items.Count == 1)
                                cb_Controllers.SelectedIndex = 0;
                        };
                        Invoke(LabelUpdate);
                    }
                }
                else
                {
                    if (cb_Controllers.Items.Contains(gps.UserIndex.ToString()))
                    {
                        MethodInvoker LabelUpdate = delegate
                        {
                            cb_Controllers.Items.Remove(gps.UserIndex.ToString());
                            if (cb_Controllers.Items.Count == 0)
                                cb_Controllers.Text = "No controller";
                        };
                        Invoke(LabelUpdate);
                    }
                }

                if (cb_Controllers.Items.Count >= 1)
                    bCheckController = false;
                else
                    bCheckController = true;

                if (bstart && cb_Controllers.Items.Count >= 1)
                {
                    bstart = false;
                    MethodInvoker LabelUpdate = delegate
                    {
                        cb_Controllers.SelectedIndex = 0;
                    };
                    Invoke(LabelUpdate);
                }
                if (bCheckAppsRunning || bCheckController)
                    pib_Status.Image = XMouse.Properties.Resources.yellow;
                else
                    pib_Status.Image = XMouse.Properties.Resources.green;
                Thread.Sleep(500);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pib_Status.Image = XMouse.Properties.Resources.green;
            GLSubItem sub1, sub2;
            GLItem gli;
            Icon ic;
            trb_Speed.Value = settings.ISpeed;
            trb_Speed.Update();
            lbl_Speed.Text = trb_Speed.Value.ToString();
            foreach (string s in settings.PrgList)
            {
                try
                {
                    if (s != "" && s != null)
                    {
                        sub2 = new GLSubItem();
                        sub2.Text = s;
                        ic = Icon.ExtractAssociatedIcon(s);
                        iList.Images.Add(sub2.Text, ic.ToBitmap());
                        sub1 = new GLSubItem();
                        sub1.ImageIndex = iList.Images.IndexOfKey(sub2.Text);
                        gli = new GLItem();
                        gli.SubItems.AddRange(new GLSubItem[] { sub1, sub2 });
                        gla_Apps.Items.Add(gli);
                        sub1 = null;
                        sub2 = null;
                        gli = null;
                    }
                }
                catch (Exception ee)
                {
                    MessageBox.Show(ee.Message + Environment.NewLine + ee.Source);
                }
            }
            Graphics g = this.CreateGraphics();
            Double startingPoint = (this.Width / 2) - (g.MeasureString(this.Text.Trim(), this.Font).Width / 2);
            Double widthOfASpace = g.MeasureString(" ", this.Font).Width;
            String tmp = " ";
            Double tmpWidth = 0;

            while ((tmpWidth + widthOfASpace) < startingPoint)
            {
                tmp += " ";
                tmpWidth += widthOfASpace;
            }

            this.Text = tmp + this.Text.Trim();
            tContrllers = new Thread(CheckControllers);
            tContrllers.Start();
        }         

        private void trb_Speed_Scroll(object sender, EventArgs e)
        {
            if(trb_Speed.Value < 10)
                lbl_Speed.Text = "0" + trb_Speed.Value.ToString();
            else
                lbl_Speed.Text = trb_Speed.Value.ToString();
            settings.ISpeed = trb_Speed.Value;
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.ShowInTaskbar = false;
                this.Hide();
                this.ntf_Icon.ShowBalloonTip(2000);
            }
        }
    }
}
