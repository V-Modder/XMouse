﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Runtime.InteropServices;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Diagnostics;
using Whatever;
using GlacialComponents.Controls;

namespace XMouse
{
    public partial class Form1 : Form
    {
        #region Globel objects
        //Vars
        Thread tSticks, tButtons, tCheckApps;
        bool bRunThreads = true;
        bool bCheckAppsRunning = false;
        ImageList iList;
        string sFileName = Application.StartupPath + @"\App-List.gl";
        int iSpeed;
        // P/Invoke Funktion u.a. zum Steuern der Maus
        [DllImport("user32.dll")]
        public static extern void mouse_event(uint dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        // Konstanten für Mausflags
        const uint MOUSEEVENTF_LEFTDOWN = 0x0002; // linken Mausbutton drücken
        const uint MOUSEEVENTF_LEFTUP = 0x0004; // linken Mausbutton loslassen
        const uint MOUSEEVENTF_RIGHTDOWN = 0x0008;  //rechten Mausbutton drücken
        const uint MOUSEEVENTF_RIGHTUP = 0x0010;  //rechten Mausbutton loslassen
        const uint MOUSEEVENTF_WHEEL = 0x0800;     //Mausrad vertikal
        const uint MOUSEEVENTF_HWHEEL = 0x1000;     //Mausrad horizontal
        #endregion

        public Form1()
        {
            InitializeComponent();
            this.Icon = Properties.Resources.xbox360;
            ntf_Icon.Icon = Properties.Resources.xbox360;
            ntf_Icon.Visible = false;
            iList = new ImageList();
            glacialList1.ImageList = iList;
            tSticks = new Thread(CheckControllerSticks);
            tSticks.Start();
            tButtons = new Thread(CheckControllerButtons);
            tButtons.Start();
            tCheckApps = new Thread(CheckAppsRunning);
            tCheckApps.Start();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (File.Exists(sFileName))
            {
                StreamReader sr = new StreamReader(sFileName);
                try
                {
                    string s;
                    pictureBox1.Image = XMouse.Properties.Resources.green;
                    GLSubItem sub1, sub2;
                    GLItem gli;
                    Icon ic;
                    iSpeed = Convert.ToInt32(sr.ReadLine());
                    trackBar1.Value = iSpeed;
                    trackBar1.Update();
                    label3.Text = trackBar1.Value.ToString();
                    s = sr.ReadLine();
                    while (s != "" && s != null)
                    {
                        sub2 = new GLSubItem();
                        sub2.Text = s;
                        ic = Icon.ExtractAssociatedIcon(s);
                        iList.Images.Add(sub2.Text, ic.ToBitmap());
                        sub1 = new GLSubItem();
                        sub1.ImageIndex = iList.Images.IndexOfKey(sub2.Text);
                        gli = new GLItem();
                        gli.SubItems.AddRange(new GLSubItem[] { sub1, sub2 });
                        glacialList1.Items.Add(gli);
                        sub1 = null;
                        sub2 = null;
                        gli = null;
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

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                bRunThreads = false;
                if (File.Exists(sFileName))
                {
                    File.Delete(sFileName);
                }
                if (glacialList1.Items.Count >= 1)
                {
                    StreamWriter sw = new StreamWriter(sFileName, false);
                    sw.WriteLine(iSpeed.ToString());
                    for (int i = 0; i < glacialList1.Items.Count; i++)
                        sw.WriteLine(glacialList1.Items[i].SubItems[1].Text);
                    sw.Close();
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            System.Collections.ArrayList al = glacialList1.Items.SelectedItems;
            foreach (GLItem o in al)
                glacialList1.Items.Remove(o);
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            Add_App AddForm = new Add_App();
            AddForm.ShowDialog(this);
            if (File.Exists(AddForm.Path))
            {
                GLSubItem sub1 = new GLSubItem();
                iList.Images.Add(AddForm.Path, AddForm.appIcon.ToBitmap());
                sub1.ImageIndex = iList.Images.IndexOfKey(AddForm.Path);
                GLSubItem sub2 = new GLSubItem();
                sub2.Text = AddForm.Path;
                GLItem itm = new GLItem();
                itm.SubItems.AddRange(new GlacialComponents.Controls.GLSubItem[] { sub1, sub2 });
                glacialList1.Items.Add(itm);
                glacialList1.Update();
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == WindowState)
            {
                ntf_Icon.Visible = true;
                Hide();
                ntf_Icon.ShowBalloonTip(2000);
                ShowInTaskbar = false;
            }
            else if (FormWindowState.Normal == this.WindowState)
            {
                ntf_Icon.Visible = false;
                ShowInTaskbar = true;
            }
        }

        private void ntf_Icon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
            
        }

        private void schließenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void öffnenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == WindowState)
            {
                Show();
                WindowState = FormWindowState.Normal;
            }
        }

        private void SimulateLeftMouseClick()
        {
            mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
            mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
        }

        private void SimulateRightMouseClick()
        {
            mouse_event(MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, 0);
            mouse_event(MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);
        }

        private void CheckControllerSticks()
        {
            GamepadState GPS = new GamepadState(SlimDX.XInput.UserIndex.One);
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

        private void CheckControllerButtons()
        {
            GamepadState GPS = new GamepadState(SlimDX.XInput.UserIndex.One);
            while (bRunThreads)
            {
                if (!bCheckAppsRunning)
                {
                    GPS.Update();
                    if (GPS.A)
                        SimulateLeftMouseClick();
                    if (GPS.B)
                        SimulateRightMouseClick();
                    if (GPS.RightStick.Position.Y != 0)
                    {
                        mouse_event(MOUSEEVENTF_WHEEL, 0, 0, Convert.ToInt32(GPS.RightStick.Position.Y * 120), 0);
                    }
                    if (GPS.RightStick.Position.X != 0)
                    {
                        mouse_event(MOUSEEVENTF_HWHEEL, 0, 0, Convert.ToInt32(GPS.RightStick.Position.X * 120), 0);
                    }
                }
                if (bCheckAppsRunning)
                        pictureBox1.Image = XMouse.Properties.Resources.yellow;
                    else
                        pictureBox1.Image = XMouse.Properties.Resources.green;
                Thread.Sleep(10);
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

        private void CheckAppsRunning()
        {
            bool bCheck;
            while (bRunThreads)
            {
                bCheck = false;
                for (int i = 0; i < glacialList1.Items.Count; i++)
                {
                    String s = glacialList1.Items[i].SubItems[1].Text;
                    if (IsProcessOpen(s.Substring(s.LastIndexOf(@"\")+1, (s.LastIndexOf(".")-1) - s.LastIndexOf(@"\"))))
                       bCheck = true;
                }
                if (bCheck)
                    bCheckAppsRunning = true;
                else
                    bCheckAppsRunning = false;
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            if(trackBar1.Value < 10)
                label3.Text = "0" + trackBar1.Value.ToString();
            else
                label3.Text = trackBar1.Value.ToString();
            iSpeed = trackBar1.Value;
        }        
    }
}
