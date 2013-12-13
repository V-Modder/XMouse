namespace XMouse
{
    partial class Add_App
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txt_AppPath = new System.Windows.Forms.TextBox();
            this.btn_search = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.btn_Save = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txt_AppPath
            // 
            this.txt_AppPath.Location = new System.Drawing.Point(13, 13);
            this.txt_AppPath.Name = "txt_AppPath";
            this.txt_AppPath.Size = new System.Drawing.Size(198, 20);
            this.txt_AppPath.TabIndex = 0;
            // 
            // btn_search
            // 
            this.btn_search.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_search.Location = new System.Drawing.Point(217, 11);
            this.btn_search.Name = "btn_search";
            this.btn_search.Size = new System.Drawing.Size(41, 23);
            this.btn_search.TabIndex = 1;
            this.btn_search.Text = "...";
            this.btn_search.UseVisualStyleBackColor = true;
            this.btn_search.Click += new System.EventHandler(this.btn_search_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "<FileName>.exe";
            this.openFileDialog1.Filter = "Executeable|*.exe";
            // 
            // btn_Save
            // 
            this.btn_Save.Location = new System.Drawing.Point(93, 39);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(75, 23);
            this.btn_Save.TabIndex = 2;
            this.btn_Save.Text = "Save";
            this.btn_Save.UseVisualStyleBackColor = true;
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // Add_App
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 68);
            this.Controls.Add(this.btn_Save);
            this.Controls.Add(this.btn_search);
            this.Controls.Add(this.txt_AppPath);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Add_App";
            this.ShowInTaskbar = false;
            this.Text = "Add_App";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_AppPath;
        private System.Windows.Forms.Button btn_search;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btn_Save;
    }
}