namespace blogimage {
    partial class frmMain {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent() {
            this.btnprocess = new System.Windows.Forms.Button();
            this.clbblogposts = new System.Windows.Forms.CheckedListBox();
            this.btnrefresh = new System.Windows.Forms.Button();
            this.cball = new System.Windows.Forms.CheckBox();
            this.lvDetails = new System.Windows.Forms.ListView();
            this.lvDetailschFilename = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvDetailschType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.pbprocess = new System.Windows.Forms.ProgressBar();
            this.lbprocess = new System.Windows.Forms.Label();
            this.pythonProcess = new System.ComponentModel.BackgroundWorker();
            this.lbprocessout = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // btnprocess
            // 
            this.btnprocess.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnprocess.Location = new System.Drawing.Point(404, 322);
            this.btnprocess.Name = "btnprocess";
            this.btnprocess.Size = new System.Drawing.Size(80, 25);
            this.btnprocess.TabIndex = 1;
            this.btnprocess.Text = "process";
            this.btnprocess.UseVisualStyleBackColor = true;
            this.btnprocess.Click += new System.EventHandler(this.button1_Click);
            // 
            // clbblogposts
            // 
            this.clbblogposts.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.clbblogposts.FormattingEnabled = true;
            this.clbblogposts.Location = new System.Drawing.Point(12, 17);
            this.clbblogposts.Name = "clbblogposts";
            this.clbblogposts.Size = new System.Drawing.Size(300, 379);
            this.clbblogposts.TabIndex = 2;
            this.clbblogposts.SelectedIndexChanged += new System.EventHandler(this.clbblogposts_SelectedIndexChanged);
            // 
            // btnrefresh
            // 
            this.btnrefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnrefresh.Location = new System.Drawing.Point(318, 322);
            this.btnrefresh.Name = "btnrefresh";
            this.btnrefresh.Size = new System.Drawing.Size(80, 25);
            this.btnrefresh.TabIndex = 4;
            this.btnrefresh.Text = "refresh";
            this.btnrefresh.UseVisualStyleBackColor = true;
            this.btnrefresh.Click += new System.EventHandler(this.btnrefresh_Click);
            // 
            // cball
            // 
            this.cball.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cball.AutoSize = true;
            this.cball.Location = new System.Drawing.Point(318, 353);
            this.cball.Name = "cball";
            this.cball.Size = new System.Drawing.Size(69, 17);
            this.cball.TabIndex = 5;
            this.cball.Text = "check all";
            this.cball.ThreeState = true;
            this.cball.UseVisualStyleBackColor = true;
            this.cball.Click += new System.EventHandler(this.cball_Click);
            // 
            // lvDetails
            // 
            this.lvDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvDetails.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.lvDetailschFilename,
            this.lvDetailschType});
            this.lvDetails.FullRowSelect = true;
            this.lvDetails.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvDetails.Location = new System.Drawing.Point(318, 17);
            this.lvDetails.MultiSelect = false;
            this.lvDetails.Name = "lvDetails";
            this.lvDetails.ShowGroups = false;
            this.lvDetails.Size = new System.Drawing.Size(204, 299);
            this.lvDetails.TabIndex = 6;
            this.lvDetails.UseCompatibleStateImageBehavior = false;
            this.lvDetails.View = System.Windows.Forms.View.Details;
            // 
            // lvDetailschFilename
            // 
            this.lvDetailschFilename.Text = "Filename";
            this.lvDetailschFilename.Width = 91;
            // 
            // lvDetailschType
            // 
            this.lvDetailschType.Text = "Typ";
            this.lvDetailschType.Width = 43;
            // 
            // pbprocess
            // 
            this.pbprocess.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbprocess.Location = new System.Drawing.Point(318, 372);
            this.pbprocess.Name = "pbprocess";
            this.pbprocess.Size = new System.Drawing.Size(204, 24);
            this.pbprocess.Step = 1;
            this.pbprocess.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.pbprocess.TabIndex = 7;
            // 
            // lbprocess
            // 
            this.lbprocess.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbprocess.AutoEllipsis = true;
            this.lbprocess.AutoSize = true;
            this.lbprocess.Location = new System.Drawing.Point(401, 354);
            this.lbprocess.Name = "lbprocess";
            this.lbprocess.Size = new System.Drawing.Size(22, 13);
            this.lbprocess.TabIndex = 8;
            this.lbprocess.Text = "foo";
            // 
            // pythonProcess
            // 
            this.pythonProcess.WorkerReportsProgress = true;
            this.pythonProcess.DoWork += new System.ComponentModel.DoWorkEventHandler(this.pythonProcess_DoWork);
            this.pythonProcess.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.pythonProcess_ProgressChanged);
            this.pythonProcess.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.pythonProcess_RunWorkerCompleted);
            // 
            // lbprocessout
            // 
            this.lbprocessout.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbprocessout.FormattingEnabled = true;
            this.lbprocessout.Location = new System.Drawing.Point(12, 402);
            this.lbprocessout.Name = "lbprocessout";
            this.lbprocessout.Size = new System.Drawing.Size(510, 95);
            this.lbprocessout.TabIndex = 9;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(534, 511);
            this.Controls.Add(this.lbprocessout);
            this.Controls.Add(this.lbprocess);
            this.Controls.Add(this.pbprocess);
            this.Controls.Add(this.lvDetails);
            this.Controls.Add(this.cball);
            this.Controls.Add(this.btnrefresh);
            this.Controls.Add(this.clbblogposts);
            this.Controls.Add(this.btnprocess);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(550, 550);
            this.Name = "frmMain";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "BlogImg2Web";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnprocess;
        private System.Windows.Forms.CheckedListBox clbblogposts;
        private System.Windows.Forms.Button btnrefresh;
        private System.Windows.Forms.CheckBox cball;
        private System.Windows.Forms.ListView lvDetails;
        private System.Windows.Forms.ColumnHeader lvDetailschFilename;
        private System.Windows.Forms.ColumnHeader lvDetailschType;
        private System.Windows.Forms.ProgressBar pbprocess;
        private System.Windows.Forms.Label lbprocess;
        private System.ComponentModel.BackgroundWorker pythonProcess;
        private System.Windows.Forms.ListBox lbprocessout;
    }
}

