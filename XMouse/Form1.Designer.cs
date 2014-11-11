namespace XMouse
{
    partial class Form1
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            GlacialComponents.Controls.GLColumn glColumn1 = new GlacialComponents.Controls.GLColumn();
            GlacialComponents.Controls.GLColumn glColumn2 = new GlacialComponents.Controls.GLColumn();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.btn_add = new System.Windows.Forms.Button();
            this.btn_delete = new System.Windows.Forms.Button();
            this.gla_Apps = new GlacialComponents.Controls.GlacialList();
            this.trb_Speed = new System.Windows.Forms.TrackBar();
            this.lbl_Speed_min = new System.Windows.Forms.Label();
            this.lbl_speed_max = new System.Windows.Forms.Label();
            this.lbl_Speed = new System.Windows.Forms.Label();
            this.pib_Status = new System.Windows.Forms.PictureBox();
            this.cb_Controllers = new ComboBoxes.AdvancedComboBox();
            this.ntf_Icon = new System.Windows.Forms.NotifyIcon(this.components);
            this.cms_Icon = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.öffnenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.schließenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.trb_Speed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pib_Status)).BeginInit();
            this.cms_Icon.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_add
            // 
            this.btn_add.BackColor = System.Drawing.SystemColors.Control;
            this.btn_add.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btn_add.FlatAppearance.BorderSize = 0;
            this.btn_add.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.btn_add.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_add.ForeColor = System.Drawing.Color.Black;
            this.btn_add.Location = new System.Drawing.Point(628, 12);
            this.btn_add.Name = "btn_add";
            this.btn_add.Size = new System.Drawing.Size(106, 31);
            this.btn_add.TabIndex = 0;
            this.btn_add.Text = "Hinzufügen";
            this.btn_add.UseVisualStyleBackColor = true;
            this.btn_add.Click += new System.EventHandler(this.btn_add_Click);
            // 
            // btn_delete
            // 
            this.btn_delete.Location = new System.Drawing.Point(642, 58);
            this.btn_delete.Name = "btn_delete";
            this.btn_delete.Size = new System.Drawing.Size(75, 23);
            this.btn_delete.TabIndex = 2;
            this.btn_delete.Text = "Löschen";
            this.btn_delete.UseVisualStyleBackColor = true;
            this.btn_delete.Click += new System.EventHandler(this.btn_delete_Click);
            // 
            // gla_Apps
            // 
            this.gla_Apps.AllowColumnResize = false;
            this.gla_Apps.AllowMultiselect = false;
            this.gla_Apps.AlternateBackground = System.Drawing.Color.DarkGreen;
            this.gla_Apps.AlternatingColors = false;
            this.gla_Apps.AutoHeight = true;
            this.gla_Apps.BackColor = System.Drawing.SystemColors.Control;
            this.gla_Apps.BackgroundStretchToFit = true;
            glColumn1.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.UserType;
            glColumn1.CheckBoxes = false;
            glColumn1.ImageIndex = -1;
            glColumn1.Name = "Icon";
            glColumn1.NumericSort = false;
            glColumn1.Text = "Icon";
            glColumn1.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            glColumn1.Width = 30;
            glColumn2.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
            glColumn2.CheckBoxes = false;
            glColumn2.ImageIndex = -1;
            glColumn2.Name = "Pfad";
            glColumn2.NumericSort = false;
            glColumn2.Text = "Pfad";
            glColumn2.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            glColumn2.Width = 570;
            this.gla_Apps.Columns.AddRange(new GlacialComponents.Controls.GLColumn[] {
            glColumn1,
            glColumn2});
            this.gla_Apps.ControlStyle = GlacialComponents.Controls.GLControlStyles.Normal;
            this.gla_Apps.FullRowSelect = true;
            this.gla_Apps.GridColor = System.Drawing.Color.LightGray;
            this.gla_Apps.GridLines = GlacialComponents.Controls.GLGridLines.gridBoth;
            this.gla_Apps.GridLineStyle = GlacialComponents.Controls.GLGridLineStyles.gridSolid;
            this.gla_Apps.GridTypes = GlacialComponents.Controls.GLGridTypes.gridOnExists;
            this.gla_Apps.HeaderHeight = 22;
            this.gla_Apps.HeaderVisible = true;
            this.gla_Apps.HeaderWordWrap = false;
            this.gla_Apps.HotColumnTracking = false;
            this.gla_Apps.HotItemTracking = false;
            this.gla_Apps.HotTrackingColor = System.Drawing.Color.LightGray;
            this.gla_Apps.HoverEvents = false;
            this.gla_Apps.HoverTime = 1;
            this.gla_Apps.ImageList = null;
            this.gla_Apps.ItemHeight = 17;
            this.gla_Apps.ItemWordWrap = false;
            this.gla_Apps.Location = new System.Drawing.Point(12, 12);
            this.gla_Apps.Name = "gla_Apps";
            this.gla_Apps.Selectable = true;
            this.gla_Apps.SelectedTextColor = System.Drawing.Color.White;
            this.gla_Apps.SelectionColor = System.Drawing.Color.DarkBlue;
            this.gla_Apps.ShowBorder = true;
            this.gla_Apps.ShowFocusRect = false;
            this.gla_Apps.Size = new System.Drawing.Size(607, 301);
            this.gla_Apps.SortType = GlacialComponents.Controls.SortTypes.InsertionSort;
            this.gla_Apps.SuperFlatHeaderColor = System.Drawing.Color.White;
            this.gla_Apps.TabIndex = 5;
            this.gla_Apps.Text = "glacialList1";
            // 
            // trb_Speed
            // 
            this.trb_Speed.BackColor = System.Drawing.SystemColors.Control;
            this.trb_Speed.Location = new System.Drawing.Point(625, 103);
            this.trb_Speed.Maximum = 50;
            this.trb_Speed.Minimum = 1;
            this.trb_Speed.Name = "trb_Speed";
            this.trb_Speed.Size = new System.Drawing.Size(109, 45);
            this.trb_Speed.TabIndex = 6;
            this.trb_Speed.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.trb_Speed.Value = 1;
            this.trb_Speed.Scroll += new System.EventHandler(this.trb_Speed_Scroll);
            // 
            // lbl_Speed_min
            // 
            this.lbl_Speed_min.AutoSize = true;
            this.lbl_Speed_min.Location = new System.Drawing.Point(625, 135);
            this.lbl_Speed_min.Name = "lbl_Speed_min";
            this.lbl_Speed_min.Size = new System.Drawing.Size(19, 13);
            this.lbl_Speed_min.TabIndex = 7;
            this.lbl_Speed_min.Text = "  1";
            // 
            // lbl_speed_max
            // 
            this.lbl_speed_max.AutoSize = true;
            this.lbl_speed_max.Location = new System.Drawing.Point(715, 135);
            this.lbl_speed_max.Name = "lbl_speed_max";
            this.lbl_speed_max.Size = new System.Drawing.Size(19, 13);
            this.lbl_speed_max.TabIndex = 8;
            this.lbl_speed_max.Text = "00";
            // 
            // lbl_Speed
            // 
            this.lbl_Speed.AutoSize = true;
            this.lbl_Speed.Location = new System.Drawing.Point(670, 135);
            this.lbl_Speed.Name = "lbl_Speed";
            this.lbl_Speed.Size = new System.Drawing.Size(19, 13);
            this.lbl_Speed.TabIndex = 9;
            this.lbl_Speed.Text = "01";
            // 
            // pib_Status
            // 
            this.pib_Status.Location = new System.Drawing.Point(703, 281);
            this.pib_Status.Name = "pib_Status";
            this.pib_Status.Size = new System.Drawing.Size(32, 32);
            this.pib_Status.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pib_Status.TabIndex = 10;
            this.pib_Status.TabStop = false;
            // 
            // cb_Controllers
            // 
            this.cb_Controllers.FormattingEnabled = true;
            this.cb_Controllers.HighlightColor = System.Drawing.Color.Green;
            this.cb_Controllers.Location = new System.Drawing.Point(628, 181);
            this.cb_Controllers.MaxDropDownItems = 4;
            this.cb_Controllers.Name = "cb_Controllers";
            this.cb_Controllers.Size = new System.Drawing.Size(106, 21);
            this.cb_Controllers.Sorted = true;
            this.cb_Controllers.TabIndex = 11;
            this.cb_Controllers.SelectedIndexChanged += new System.EventHandler(this.cb_Controllers_SelectedIndexChanged);
            // 
            // ntf_Icon
            // 
            this.ntf_Icon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.ntf_Icon.BalloonTipText = "Controlls the mouse through pad. Double click to open config.";
            this.ntf_Icon.BalloonTipTitle = "XMouse";
            this.ntf_Icon.ContextMenuStrip = this.cms_Icon;
            this.ntf_Icon.Text = "XMouse";
            this.ntf_Icon.Visible = true;
            this.ntf_Icon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ntf_Icon_MouseDoubleClick);
            // 
            // cms_Icon
            // 
            this.cms_Icon.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.öffnenToolStripMenuItem,
            this.schließenToolStripMenuItem});
            this.cms_Icon.Name = "cms_Icon";
            this.cms_Icon.Size = new System.Drawing.Size(126, 48);
            // 
            // öffnenToolStripMenuItem
            // 
            this.öffnenToolStripMenuItem.Name = "öffnenToolStripMenuItem";
            this.öffnenToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.öffnenToolStripMenuItem.Text = "Öffnen";
            this.öffnenToolStripMenuItem.Click += new System.EventHandler(this.öffnenToolStripMenuItem_Click);
            // 
            // schließenToolStripMenuItem
            // 
            this.schließenToolStripMenuItem.Name = "schließenToolStripMenuItem";
            this.schließenToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.schließenToolStripMenuItem.Text = "Schließen";
            this.schließenToolStripMenuItem.Click += new System.EventHandler(this.schließenToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(746, 337);
            this.Controls.Add(this.cb_Controllers);
            this.Controls.Add(this.pib_Status);
            this.Controls.Add(this.lbl_Speed);
            this.Controls.Add(this.lbl_speed_max);
            this.Controls.Add(this.lbl_Speed_min);
            this.Controls.Add(this.trb_Speed);
            this.Controls.Add(this.gla_Apps);
            this.Controls.Add(this.btn_delete);
            this.Controls.Add(this.btn_add);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Padding = new System.Windows.Forms.Padding(350, 150, 350, 150);
            this.ShowInTaskbar = false;
            this.Text = "XMouse";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.trb_Speed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pib_Status)).EndInit();
            this.cms_Icon.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_add;
        private System.Windows.Forms.Button btn_delete;
        private GlacialComponents.Controls.GlacialList gla_Apps;
        private System.Windows.Forms.TrackBar trb_Speed;
        private System.Windows.Forms.Label lbl_Speed_min;
        private System.Windows.Forms.Label lbl_speed_max;
        private System.Windows.Forms.Label lbl_Speed;
        private System.Windows.Forms.PictureBox pib_Status;
        private ComboBoxes.AdvancedComboBox cb_Controllers;
        private System.Windows.Forms.NotifyIcon ntf_Icon;
        private System.Windows.Forms.ContextMenuStrip cms_Icon;
        private System.Windows.Forms.ToolStripMenuItem öffnenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem schließenToolStripMenuItem;

    }
}

