namespace Library_Management_System.Usercontrol.StaffUserControl
{
    partial class StaffDashBoardUserControl
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
            this.label1 = new System.Windows.Forms.Label();
            this.btnFindBook = new System.Windows.Forms.Button();
            this.btnFindMember = new System.Windows.Forms.Button();
            this.pnlDynamicContent = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(4, 10);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(417, 30);
            this.label1.TabIndex = 2;
            this.label1.Text = "IIC Automated Library Management System";
            // 
            // btnFindBook
            // 
            this.btnFindBook.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFindBook.Location = new System.Drawing.Point(428, 17);
            this.btnFindBook.Name = "btnFindBook";
            this.btnFindBook.Padding = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.btnFindBook.Size = new System.Drawing.Size(93, 23);
            this.btnFindBook.TabIndex = 12;
            this.btnFindBook.Text = "Find Book";
            this.btnFindBook.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnFindBook.UseVisualStyleBackColor = true;
            this.btnFindBook.Click += new System.EventHandler(this.btnFindBook_Click);
            // 
            // btnFindMember
            // 
            this.btnFindMember.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFindMember.Location = new System.Drawing.Point(536, 17);
            this.btnFindMember.Name = "btnFindMember";
            this.btnFindMember.Padding = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.btnFindMember.Size = new System.Drawing.Size(110, 23);
            this.btnFindMember.TabIndex = 12;
            this.btnFindMember.Text = "Find Member";
            this.btnFindMember.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnFindMember.UseVisualStyleBackColor = true;
            this.btnFindMember.Click += new System.EventHandler(this.btnFindMember_Click);
            // 
            // pnlDynamicContent
            // 
            this.pnlDynamicContent.AutoScroll = true;
            this.pnlDynamicContent.AutoSize = true;
            this.pnlDynamicContent.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlDynamicContent.Location = new System.Drawing.Point(3, 49);
            this.pnlDynamicContent.Name = "pnlDynamicContent";
            this.pnlDynamicContent.Size = new System.Drawing.Size(715, 327);
            this.pnlDynamicContent.TabIndex = 13;
            // 
            // StaffDashBoardUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pnlDynamicContent);
            this.Controls.Add(this.btnFindMember);
            this.Controls.Add(this.btnFindBook);
            this.Name = "StaffDashBoardUserControl";
            this.Size = new System.Drawing.Size(721, 379);
            this.Load += new System.EventHandler(this.StaffDashBoard_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnFindBook;
        private System.Windows.Forms.Button btnFindMember;
        private System.Windows.Forms.Panel pnlDynamicContent;
    }
}
