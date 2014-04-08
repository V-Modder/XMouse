using System;
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
        #region Global objects
        //Vars
        private bool bRunThread = true;
        private Thread tContrllers;
        private ImageList iList;
        
        private int iSpeed;
        #endregion

        public Form1()
        {
            try
            {
                InitializeComponent();
                iList = new ImageList();
                gla_Apps.ImageList = iList;
                tContrllers = new Thread(CheckControllers);
                tContrllers.Start();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            System.Collections.ArrayList al = gla_Apps.Items.SelectedItems;
            foreach (GLItem o in al)
            {
                gla_Apps.Items.Remove(o);
                XMouse.Program.PrgList.Remove(o.Text);
            }
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            Add_App AddForm = new Add_App();
            AddForm.ShowDialog(this);
            if (File.Exists(AddForm.Path))
            {
                XMouse.Program.PrgList.Add(AddForm.Path);
                GLSubItem sub1 = new GLSubItem();
                iList.Images.Add(AddForm.Path, AddForm.appIcon.ToBitmap());
                sub1.ImageIndex = iList.Images.IndexOfKey(AddForm.Path);
                GLSubItem sub2 = new GLSubItem();
                sub2.Text = AddForm.Path;
                GLItem itm = new GLItem();
                itm.SubItems.AddRange(new GlacialComponents.Controls.GLSubItem[] { sub1, sub2 });
                gla_Apps.Items.Add(itm);
                gla_Apps.Update();
            }
        }

        private void cb_Controllers_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            bRunThread = false;
        }

        private void CheckControllers()
        {
            GamepadState gps;
            bool bstart = true;
            while (bRunThread)
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

                //if (cb_Controllers.Items.Count >= 1)
                //    bCheckController = false;
                //else
                //    bCheckController = true;

                if (bstart && cb_Controllers.Items.Count >= 1)
                {
                    bstart = false;
                    MethodInvoker LabelUpdate = delegate
                    {
                        cb_Controllers.SelectedIndex = 0;
                    };
                    Invoke(LabelUpdate);
                }
                Thread.Sleep(500);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pib_Status.Image = XMouse.Properties.Resources.green;
            GLSubItem sub1, sub2;
            GLItem gli;
            Icon ic;
            trb_Speed.Value = XMouse.Program.ISpeed;
            trb_Speed.Update();
            lbl_Speed.Text = trb_Speed.Value.ToString();
            foreach(string s in XMouse.Program.PrgList)
            {
                try
                {
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
                        gla_Apps.Items.Add(gli);
                        sub1 = null;
                        sub2 = null;
                        gli = null;
                    }
                    this.WindowState = FormWindowState.Minimized;
                }
                catch (Exception ee)
                {
                    MessageBox.Show(ee.Message + Environment.NewLine + ee.Source);
                }
            }
        }         

        private void trb_Speed_Scroll(object sender, EventArgs e)
        {
            if(trb_Speed.Value < 10)
                lbl_Speed.Text = "0" + trb_Speed.Value.ToString();
            else
                lbl_Speed.Text = trb_Speed.Value.ToString();
            XMouse.Program.ISpeed = trb_Speed.Value;
        }
    }
}
