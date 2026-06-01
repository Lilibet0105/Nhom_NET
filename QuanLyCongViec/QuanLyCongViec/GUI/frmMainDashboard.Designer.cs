namespace GUI
{
    partial class frmMainDashboard
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
            this.pnlSidebar = new System.Windows.Forms.Panel();
            this.btnLogout = new System.Windows.Forms.Button();
            this.btnReport = new System.Windows.Forms.Button();
            this.btnCalendar = new System.Windows.Forms.Button();
            this.btnTeam = new System.Windows.Forms.Button();
            this.btnKanban = new System.Windows.Forms.Button();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.btnProject = new System.Windows.Forms.Button();
            this.btnUser = new System.Windows.Forms.Button();
            this.pnlSidebar.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlSidebar
            // 
            this.pnlSidebar.BackColor = System.Drawing.Color.DarkGray;
            this.pnlSidebar.Controls.Add(this.btnUser);
            this.pnlSidebar.Controls.Add(this.btnProject);
            this.pnlSidebar.Controls.Add(this.btnLogout);
            this.pnlSidebar.Controls.Add(this.btnReport);
            this.pnlSidebar.Controls.Add(this.btnCalendar);
            this.pnlSidebar.Controls.Add(this.btnTeam);
            this.pnlSidebar.Controls.Add(this.btnKanban);
            this.pnlSidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlSidebar.Location = new System.Drawing.Point(0, 0);
            this.pnlSidebar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pnlSidebar.Name = "pnlSidebar";
            this.pnlSidebar.Size = new System.Drawing.Size(196, 515);
            this.pnlSidebar.TabIndex = 0;
            // 
            // btnLogout
            // 
            this.btnLogout.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnLogout.Location = new System.Drawing.Point(0, 475);
            this.btnLogout.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(196, 40);
            this.btnLogout.TabIndex = 4;
            this.btnLogout.Text = "Đăng xuất";
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // btnReport
            // 
            this.btnReport.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnReport.Location = new System.Drawing.Point(0, 120);
            this.btnReport.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(196, 45);
            this.btnReport.TabIndex = 3;
            this.btnReport.Text = "Báo cáo thống kê";
            this.btnReport.UseVisualStyleBackColor = true;
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            // 
            // btnCalendar
            // 
            this.btnCalendar.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnCalendar.Location = new System.Drawing.Point(0, 80);
            this.btnCalendar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCalendar.Name = "btnCalendar";
            this.btnCalendar.Size = new System.Drawing.Size(196, 40);
            this.btnCalendar.TabIndex = 2;
            this.btnCalendar.Text = "Lịch công việc";
            this.btnCalendar.UseVisualStyleBackColor = true;
            this.btnCalendar.Click += new System.EventHandler(this.btnCalendar_Click);
            // 
            // btnTeam
            // 
            this.btnTeam.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnTeam.Location = new System.Drawing.Point(0, 40);
            this.btnTeam.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnTeam.Name = "btnTeam";
            this.btnTeam.Size = new System.Drawing.Size(196, 40);
            this.btnTeam.TabIndex = 1;
            this.btnTeam.Text = "Quản lý nhóm";
            this.btnTeam.UseVisualStyleBackColor = true;
            this.btnTeam.Click += new System.EventHandler(this.btnTeam_Click);
            // 
            // btnKanban
            // 
            this.btnKanban.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnKanban.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnKanban.Location = new System.Drawing.Point(0, 0);
            this.btnKanban.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnKanban.Name = "btnKanban";
            this.btnKanban.Size = new System.Drawing.Size(196, 40);
            this.btnKanban.TabIndex = 0;
            this.btnKanban.Text = "Bảng Kanban";
            this.btnKanban.UseVisualStyleBackColor = true;
            this.btnKanban.Click += new System.EventHandler(this.btnKanban_Click);
            // 
            // pnlContent
            // 
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(196, 0);
            this.pnlContent.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Size = new System.Drawing.Size(851, 515);
            this.pnlContent.TabIndex = 1;
            // 
            // btnProject
            // 
            this.btnProject.Location = new System.Drawing.Point(0, 161);
            this.btnProject.Name = "btnProject";
            this.btnProject.Size = new System.Drawing.Size(196, 46);
            this.btnProject.TabIndex = 5;
            this.btnProject.Text = "Quản lý dự án";
            this.btnProject.UseVisualStyleBackColor = true;
            this.btnProject.Click += new System.EventHandler(this.btnProject_Click);
            // 
            // btnUser
            // 
            this.btnUser.Location = new System.Drawing.Point(0, 203);
            this.btnUser.Name = "btnUser";
            this.btnUser.Size = new System.Drawing.Size(196, 43);
            this.btnUser.TabIndex = 7;
            this.btnUser.Text = "Quản lý tài khoản";
            this.btnUser.UseVisualStyleBackColor = true;
            this.btnUser.Click += new System.EventHandler(this.btnUser_Click);
            // 
            // frmMainDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1047, 515);
            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.pnlSidebar);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "frmMainDashboard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hệ thống Quản lý Công việc";
            this.Load += new System.EventHandler(this.frmMainDashboard_Load);
            this.pnlSidebar.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlSidebar;
        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Button btnReport;
        private System.Windows.Forms.Button btnCalendar;
        private System.Windows.Forms.Button btnTeam;
        private System.Windows.Forms.Button btnKanban;
        private System.Windows.Forms.Button btnProject;
        private System.Windows.Forms.Button btnUser;
    }
}
