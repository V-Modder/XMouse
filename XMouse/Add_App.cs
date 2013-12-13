using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace XMouse
{
    public partial class Add_App : Form
    {
        public Icon appIcon;
        public string Path;

        public Add_App()
        {
            InitializeComponent();
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog(this) == DialogResult.OK)
            {
                txt_AppPath.Text = openFileDialog1.FileName;
                appIcon = Icon.ExtractAssociatedIcon(txt_AppPath.Text);
                Path = txt_AppPath.Text;
            }
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
