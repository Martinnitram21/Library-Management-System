namespace Library_Management_System.Usercontrol.StaffUserControl
{
    partial class FindMemberUserControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.flowLayoutPanelMembers = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSearchMember = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnAddMember = new System.Windows.Forms.Button();
            this.btnDeleteMember = new System.Windows.Forms.Button();
            this.btnEditMember = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // flowLayoutPanelMembers
            // 
            this.flowLayoutPanelMembers.AutoScroll = true;
            this.flowLayoutPanelMembers.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanelMembers.Location = new System.Drawing.Point(0, 149);
            this.flowLayoutPanelMembers.Name = "flowLayoutPanelMembers";
            this.flowLayoutPanelMembers.Size = new System.Drawing.Size(1006, 463);
            this.flowLayoutPanelMembers.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(228, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(147, 21);
            this.label1.TabIndex = 1;
            this.label1.Text = "Search For Member";
            // 
            // txtSearchMember
            // 
            this.txtSearchMember.Location = new System.Drawing.Point(381, 53);
            this.txtSearchMember.Name = "txtSearchMember";
            this.txtSearchMember.Size = new System.Drawing.Size(210, 29);
            this.txtSearchMember.TabIndex = 2;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(597, 55);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(81, 27);
            this.btnSearch.TabIndex = 3;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnAddMember
            // 
            this.btnAddMember.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnAddMember.FlatAppearance.BorderSize = 0;
            this.btnAddMember.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddMember.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddMember.Location = new System.Drawing.Point(384, 98);
            this.btnAddMember.Name = "btnAddMember";
            this.btnAddMember.Size = new System.Drawing.Size(94, 20);
            this.btnAddMember.TabIndex = 26;
            this.btnAddMember.Text = "Add";
            this.btnAddMember.UseVisualStyleBackColor = false;
            this.btnAddMember.Visible = false;
            this.btnAddMember.Click += new System.EventHandler(this.btnAddMember_Click);
            // 
            // btnDeleteMember
            // 
            this.btnDeleteMember.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnDeleteMember.FlatAppearance.BorderSize = 0;
            this.btnDeleteMember.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteMember.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeleteMember.Location = new System.Drawing.Point(584, 98);
            this.btnDeleteMember.Name = "btnDeleteMember";
            this.btnDeleteMember.Size = new System.Drawing.Size(94, 20);
            this.btnDeleteMember.TabIndex = 27;
            this.btnDeleteMember.Text = "Delete";
            this.btnDeleteMember.UseVisualStyleBackColor = false;
            this.btnDeleteMember.Visible = false;
            this.btnDeleteMember.Click += new System.EventHandler(this.btnDeleteMember_Click);
            // 
            // btnEditMember
            // 
            this.btnEditMember.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnEditMember.FlatAppearance.BorderSize = 0;
            this.btnEditMember.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditMember.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditMember.Location = new System.Drawing.Point(484, 98);
            this.btnEditMember.Name = "btnEditMember";
            this.btnEditMember.Size = new System.Drawing.Size(94, 20);
            this.btnEditMember.TabIndex = 28;
            this.btnEditMember.Text = "Edit";
            this.btnEditMember.UseVisualStyleBackColor = false;
            this.btnEditMember.Visible = false;
            this.btnEditMember.Click += new System.EventHandler(this.btnEditMember_Click);
            // 
            // FindMemberUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnAddMember);
            this.Controls.Add(this.btnDeleteMember);
            this.Controls.Add(this.btnEditMember);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.txtSearchMember);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.flowLayoutPanelMembers);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FindMemberUserControl";
            this.Size = new System.Drawing.Size(1006, 612);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelMembers;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSearchMember;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnAddMember;
        private System.Windows.Forms.Button btnDeleteMember;
        private System.Windows.Forms.Button btnEditMember;
    }
}
