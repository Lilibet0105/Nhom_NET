namespace GUI
{
    partial class frmTeamManager
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
            this.dgvTeam = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtMemberID = new System.Windows.Forms.TextBox();
            this.txtFullname = new System.Windows.Forms.TextBox();
            this.cboRole = new System.Windows.Forms.ComboBox();
            this.btnThem = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTeam)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvTeam
            // 
            this.dgvTeam.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTeam.Dock = System.Windows.Forms.DockStyle.Left;
            this.dgvTeam.Location = new System.Drawing.Point(0, 0);
            this.dgvTeam.MultiSelect = false;
            this.dgvTeam.Name = "dgvTeam";
            this.dgvTeam.RowHeadersWidth = 62;
            this.dgvTeam.RowTemplate.Height = 28;
            this.dgvTeam.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTeam.Size = new System.Drawing.Size(344, 450);
            this.dgvTeam.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(387, 106);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Mã thành viên";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(387, 161);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Họ tên";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(387, 212);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 20);
            this.label3.TabIndex = 3;
            this.label3.Text = "Chức vụ";
            // 
            // txtMemberID
            // 
            this.txtMemberID.Location = new System.Drawing.Point(501, 106);
            this.txtMemberID.Name = "txtMemberID";
            this.txtMemberID.Size = new System.Drawing.Size(147, 26);
            this.txtMemberID.TabIndex = 4;
            // 
            // txtFullname
            // 
            this.txtFullname.Location = new System.Drawing.Point(501, 155);
            this.txtFullname.Name = "txtFullname";
            this.txtFullname.Size = new System.Drawing.Size(147, 26);
            this.txtFullname.TabIndex = 5;
            // 
            // cboRole
            // 
            this.cboRole.FormattingEnabled = true;
            this.cboRole.Location = new System.Drawing.Point(501, 209);
            this.cboRole.Name = "cboRole";
            this.cboRole.Size = new System.Drawing.Size(147, 28);
            this.cboRole.TabIndex = 6;
            // 
            // btnThem
            // 
            this.btnThem.Location = new System.Drawing.Point(391, 286);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(82, 26);
            this.btnThem.TabIndex = 7;
            this.btnThem.Text = "Thêm";
            this.btnThem.UseVisualStyleBackColor = true;
            // 
            // btnSua
            // 
            this.btnSua.Location = new System.Drawing.Point(523, 286);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(82, 26);
            this.btnSua.TabIndex = 8;
            this.btnSua.Text = "Sửa";
            this.btnSua.UseVisualStyleBackColor = true;
            // 
            // btnXoa
            // 
            this.btnXoa.Location = new System.Drawing.Point(648, 286);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(86, 24);
            this.btnXoa.TabIndex = 9;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.UseVisualStyleBackColor = true;
            // 
            // frmTeamManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnXoa);
            this.Controls.Add(this.btnSua);
            this.Controls.Add(this.btnThem);
            this.Controls.Add(this.cboRole);
            this.Controls.Add(this.txtFullname);
            this.Controls.Add(this.txtMemberID);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvTeam);
            this.Name = "frmTeamManager";
            this.Text = "frmTeamManager";
            ((System.ComponentModel.ISupportInitialize)(this.dgvTeam)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvTeam;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtMemberID;
        private System.Windows.Forms.TextBox txtFullname;
        private System.Windows.Forms.ComboBox cboRole;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnXoa;
    }
}