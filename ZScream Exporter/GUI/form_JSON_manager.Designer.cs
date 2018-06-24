namespace ZScream_Exporter.GUI
{
    partial class ZscreamForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ZscreamForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openROMToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fromROMToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fromJsonFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportLoadedProjectToROMToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toJsonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toROMToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.clearlogsButton = new System.Windows.Forms.Button();
            this.logTextbox = new System.Windows.Forms.RichTextBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.label2 = new System.Windows.Forms.Label();
            this.logPanel = new System.Windows.Forms.Panel();
            this.projectPanel = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.labelbytesInfos = new System.Windows.Forms.Label();
            this.labelInfos = new System.Windows.Forms.Label();
            this.projectinfoLabel = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.propertyGridexportSetting = new System.Windows.Forms.PropertyGrid();
            this.menuStrip1.SuspendLayout();
            this.logPanel.SuspendLayout();
            this.projectPanel.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(684, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openROMToolStripMenuItem,
            this.exportLoadedProjectToROMToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openROMToolStripMenuItem
            // 
            this.openROMToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fromROMToolStripMenuItem,
            this.fromJsonFilesToolStripMenuItem});
            this.openROMToolStripMenuItem.Name = "openROMToolStripMenuItem";
            this.openROMToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.openROMToolStripMenuItem.Text = "Load Project";
            // 
            // fromROMToolStripMenuItem
            // 
            this.fromROMToolStripMenuItem.Name = "fromROMToolStripMenuItem";
            this.fromROMToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.fromROMToolStripMenuItem.Text = "From ROM";
            this.fromROMToolStripMenuItem.Click += new System.EventHandler(this.FromROMToolStripMenuItem_Click);
            // 
            // fromJsonFilesToolStripMenuItem
            // 
            this.fromJsonFilesToolStripMenuItem.Name = "fromJsonFilesToolStripMenuItem";
            this.fromJsonFilesToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.fromJsonFilesToolStripMenuItem.Text = "From Json Files";
            this.fromJsonFilesToolStripMenuItem.Click += new System.EventHandler(this.FromJsonFilesToolStripMenuItem_Click);
            // 
            // exportLoadedProjectToROMToolStripMenuItem
            // 
            this.exportLoadedProjectToROMToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toJsonToolStripMenuItem,
            this.toROMToolStripMenuItem});
            this.exportLoadedProjectToROMToolStripMenuItem.Name = "exportLoadedProjectToROMToolStripMenuItem";
            this.exportLoadedProjectToROMToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exportLoadedProjectToROMToolStripMenuItem.Text = "Export Project";
            // 
            // toJsonToolStripMenuItem
            // 
            this.toJsonToolStripMenuItem.Name = "toJsonToolStripMenuItem";
            this.toJsonToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.toJsonToolStripMenuItem.Text = "To Json Files";
            // 
            // toROMToolStripMenuItem
            // 
            this.toROMToolStripMenuItem.Name = "toROMToolStripMenuItem";
            this.toROMToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.toROMToolStripMenuItem.Text = "To ROM";
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Logs : ";
            // 
            // clearlogsButton
            // 
            this.clearlogsButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.clearlogsButton.Location = new System.Drawing.Point(0, 339);
            this.clearlogsButton.Name = "clearlogsButton";
            this.clearlogsButton.Size = new System.Drawing.Size(684, 23);
            this.clearlogsButton.TabIndex = 2;
            this.clearlogsButton.Text = "Clear Logs";
            this.clearlogsButton.UseVisualStyleBackColor = true;
            this.clearlogsButton.Click += new System.EventHandler(this.clearlogsButton_Click);
            // 
            // logTextbox
            // 
            this.logTextbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logTextbox.Location = new System.Drawing.Point(0, 13);
            this.logTextbox.Name = "logTextbox";
            this.logTextbox.Size = new System.Drawing.Size(372, 279);
            this.logTextbox.TabIndex = 3;
            this.logTextbox.Text = "";
            // 
            // progressBar1
            // 
            this.progressBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBar1.Location = new System.Drawing.Point(0, 316);
            this.progressBar1.Maximum = 10;
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(684, 23);
            this.progressBar1.Step = 8;
            this.progressBar1.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Location = new System.Drawing.Point(372, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Loaded Project :";
            // 
            // logPanel
            // 
            this.logPanel.Controls.Add(this.logTextbox);
            this.logPanel.Controls.Add(this.label1);
            this.logPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.logPanel.Location = new System.Drawing.Point(0, 24);
            this.logPanel.Name = "logPanel";
            this.logPanel.Size = new System.Drawing.Size(372, 292);
            this.logPanel.TabIndex = 6;
            // 
            // projectPanel
            // 
            this.projectPanel.Controls.Add(this.tabControl1);
            this.projectPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.projectPanel.Enabled = false;
            this.projectPanel.Location = new System.Drawing.Point(372, 37);
            this.projectPanel.Name = "projectPanel";
            this.projectPanel.Size = new System.Drawing.Size(312, 279);
            this.projectPanel.TabIndex = 7;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(312, 279);
            this.tabControl1.TabIndex = 8;
            // 
            // tabPage1
            // 
            this.tabPage1.AutoScroll = true;
            this.tabPage1.AutoScrollMinSize = new System.Drawing.Size(0, 280);
            this.tabPage1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tabPage1.Controls.Add(this.labelbytesInfos);
            this.tabPage1.Controls.Add(this.labelInfos);
            this.tabPage1.Controls.Add(this.projectinfoLabel);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(304, 253);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Project Infos.";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // labelbytesInfos
            // 
            this.labelbytesInfos.AutoSize = true;
            this.labelbytesInfos.Dock = System.Windows.Forms.DockStyle.Left;
            this.labelbytesInfos.Location = new System.Drawing.Point(107, 3);
            this.labelbytesInfos.Name = "labelbytesInfos";
            this.labelbytesInfos.Size = new System.Drawing.Size(13, 273);
            this.labelbytesInfos.TabIndex = 8;
            this.labelbytesInfos.Text = "0\r\n0\r\n0\r\n0\r\n0\r\n0\r\n0\r\n0\r\n0\r\n\r\n0\r\n0\r\n0\r\n0\r\n0\r\n0\r\n0\r\n0\r\n0\r\n\r\n0";
            // 
            // labelInfos
            // 
            this.labelInfos.AutoSize = true;
            this.labelInfos.Dock = System.Windows.Forms.DockStyle.Left;
            this.labelInfos.Location = new System.Drawing.Point(94, 3);
            this.labelInfos.Name = "labelInfos";
            this.labelInfos.Size = new System.Drawing.Size(13, 273);
            this.labelInfos.TabIndex = 7;
            this.labelInfos.Text = "0\r\n0\r\n0\r\n0\r\n0\r\n0\r\n0\r\n0\r\n0\r\n\r\n0\r\n0\r\n0\r\n0\r\n0\r\n0\r\n0\r\n0\r\n0\r\n\r\n0";
            // 
            // projectinfoLabel
            // 
            this.projectinfoLabel.AutoSize = true;
            this.projectinfoLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.projectinfoLabel.Location = new System.Drawing.Point(3, 3);
            this.projectinfoLabel.Name = "projectinfoLabel";
            this.projectinfoLabel.Size = new System.Drawing.Size(91, 273);
            this.projectinfoLabel.TabIndex = 6;
            this.projectinfoLabel.Text = resources.GetString("projectinfoLabel.Text");
            // 
            // tabPage2
            // 
            this.tabPage2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tabPage2.Controls.Add(this.propertyGridexportSetting);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(304, 253);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Export Settings";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // propertyGridexportSetting
            // 
            this.propertyGridexportSetting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGridexportSetting.Location = new System.Drawing.Point(3, 3);
            this.propertyGridexportSetting.Name = "propertyGridexportSetting";
            this.propertyGridexportSetting.Size = new System.Drawing.Size(294, 243);
            this.propertyGridexportSetting.TabIndex = 0;
            // 
            // ZscreamForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(684, 362);
            this.Controls.Add(this.projectPanel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.logPanel);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.clearlogsButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(700, 400);
            this.Name = "ZscreamForm";
            this.Text = "ZScream Magic - No project loaded";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.logPanel.ResumeLayout(false);
            this.logPanel.PerformLayout();
            this.projectPanel.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openROMToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button clearlogsButton;
        private System.Windows.Forms.RichTextBox logTextbox;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.ToolStripMenuItem exportLoadedProjectToROMToolStripMenuItem;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel logPanel;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.Panel projectPanel;
        private System.Windows.Forms.Label projectinfoLabel;
        private System.Windows.Forms.ToolStripMenuItem fromROMToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fromJsonFilesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toJsonToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toROMToolStripMenuItem;
        private System.Windows.Forms.Label labelInfos;
        private System.Windows.Forms.Label labelbytesInfos;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.PropertyGrid propertyGridexportSetting;
    }
}

