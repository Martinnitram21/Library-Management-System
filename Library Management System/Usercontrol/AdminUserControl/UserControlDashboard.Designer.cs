namespace Library_Management_System.Usercontrol
{
    partial class UserControlDashboard
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
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblTotalBooks = new System.Windows.Forms.Label();
            this.lblTotalMembers = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblBorrowedBooks = new System.Windows.Forms.Label();
            this.lblOverdueBooks = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(71, 38);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(174, 45);
            this.label1.TabIndex = 0;
            this.label1.Text = "Dashboard";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(324, 218);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 21);
            this.label2.TabIndex = 1;
            this.label2.Text = "Total Books";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(608, 218);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(156, 21);
            this.label3.TabIndex = 1;
            this.label3.Text = "Number of Members";
            // 
            // lblTotalBooks
            // 
            this.lblTotalBooks.AutoSize = true;
            this.lblTotalBooks.Location = new System.Drawing.Point(357, 239);
            this.lblTotalBooks.Name = "lblTotalBooks";
            this.lblTotalBooks.Size = new System.Drawing.Size(17, 21);
            this.lblTotalBooks.TabIndex = 1;
            this.lblTotalBooks.Text = "?";
            // 
            // lblTotalMembers
            // 
            this.lblTotalMembers.AutoSize = true;
            this.lblTotalMembers.Location = new System.Drawing.Point(671, 239);
            this.lblTotalMembers.Name = "lblTotalMembers";
            this.lblTotalMembers.Size = new System.Drawing.Size(17, 21);
            this.lblTotalMembers.TabIndex = 1;
            this.lblTotalMembers.Text = "?";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(269, 296);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(193, 21);
            this.label6.TabIndex = 1;
            this.label6.Text = "Books Currently Borrowed";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(626, 296);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(116, 21);
            this.label7.TabIndex = 1;
            this.label7.Text = "Overdue Books";
            // 
            // lblBorrowedBooks
            // 
            this.lblBorrowedBooks.AutoSize = true;
            this.lblBorrowedBooks.Location = new System.Drawing.Point(357, 334);
            this.lblBorrowedBooks.Name = "lblBorrowedBooks";
            this.lblBorrowedBooks.Size = new System.Drawing.Size(17, 21);
            this.lblBorrowedBooks.TabIndex = 1;
            this.lblBorrowedBooks.Text = "?";
            // 
            // lblOverdueBooks
            // 
            this.lblOverdueBooks.AutoSize = true;
            this.lblOverdueBooks.Location = new System.Drawing.Point(668, 317);
            this.lblOverdueBooks.Name = "lblOverdueBooks";
            this.lblOverdueBooks.Size = new System.Drawing.Size(17, 21);
            this.lblOverdueBooks.TabIndex = 1;
            this.lblOverdueBooks.Text = "?";
            // 
            // UserControlDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblOverdueBooks);
            this.Controls.Add(this.lblTotalMembers);
            this.Controls.Add(this.lblBorrowedBooks);
            this.Controls.Add(this.lblTotalBooks);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "UserControlDashboard";
            this.Size = new System.Drawing.Size(1080, 591);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblTotalBooks;
        private System.Windows.Forms.Label lblTotalMembers;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblBorrowedBooks;
        private System.Windows.Forms.Label lblOverdueBooks;
    }
}
