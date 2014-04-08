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
            GlacialComponents.Controls.GLColumn glColumn3 = new GlacialComponents.Controls.GLColumn();
            GlacialComponents.Controls.GLColumn glColumn4 = new GlacialComponents.Controls.GLColumn();
            this.btn_add = new System.Windows.Forms.Button();
            this.btn_delete = new System.Windows.Forms.Button();
            this.gla_Apps = new GlacialComponents.Controls.GlacialList();
            this.trb_Speed = new System.Windows.Forms.TrackBar();
            this.lbl_Speed_min = new System.Windows.Forms.Label();
            this.lbl_speed_max = new System.Windows.Forms.Label();
            this.lbl_Speed = new System.Windows.Forms.Label();
            this.pib_Status = new System.Windows.Forms.PictureBox();
            this.cb_Controllers = new ComboBoxes.AdvancedComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.trb_Speed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pib_Status)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_add
            // 
            this.btn_add.Location = new System.Drawing.Point(659, 12);
            this.btn_add.Name = "btn_add";
            this.btn_add.Size = new System.Drawing.Size(75, 23);
            this.btn_add.TabIndex = 0;
            this.btn_add.Text = "Hinzufügen";
            this.btn_add.UseVisualStyleBackColor = true;
            this.btn_add.Click += new System.EventHandler(this.btn_add_Click);
            // 
            // btn_delete
            // 
            this.btn_delete.Location = new System.Drawing.Point(659, 42);
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
            this.gla_Apps.BackColor = System.Drawing.SystemColors.Window;
            this.gla_Apps.BackgroundStretchToFit = true;
            glColumn3.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.UserType;
            glColumn3.CheckBoxes = false;
            glColumn3.ImageIndex = -1;
            glColumn3.Name = "Icon";
            glColumn3.NumericSort = false;
            glColumn3.Text = "Icon";
            glColumn3.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            glColumn3.Width = 30;
            glColumn4.ActivatedEmbeddedType = GlacialComponents.Controls.GLActivatedEmbeddedTypes.None;
            glColumn4.CheckBoxes = false;
            glColumn4.ImageIndex = -1;
            glColumn4.Name = "Pfad";
            glColumn4.NumericSort = false;
            glColumn4.Text = "Pfad";
            glColumn4.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            glColumn4.Width = 570;
            this.gla_Apps.Columns.AddRange(new GlacialComponents.Controls.GLColumn[] {
            glColumn3,
            glColumn4});
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
            this.trb_Speed.Location = new System.Drawing.Point(625, 103);
            this.trb_Speed.Maximum = 20;
            this.trb_Speed.Minimum = 1;
            this.trb_Speed.Name = "trb_Speed";
            this.trb_Speed.Size = new System.Drawing.Size(109, 45);
            this.trb_Speed.TabIndex = 6;
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
            this.lbl_speed_max.Text = "20";
            // 
            // lbl_Speed
            // 
            this.lbl_Speed.AutoSize = true;
            this.lbl_Speed.Location = new System.Drawing.Point(672, 135);
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
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "XMouse";
            this.TransparencyKey = System.Drawing.Color.Blue;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.trb_Speed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pib_Status)).EndInit();
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

    }
}

