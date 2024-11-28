namespace Library_Management_System.Usercontrol
{
    partial class UserControlReports
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
            this.tabControlReports = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.dataGridViewBooks = new System.Windows.Forms.DataGridView();
            this.dataGridViewMembers = new System.Windows.Forms.DataGridView();
            this.dataGridViewTransactions = new System.Windows.Forms.DataGridView();
            this.dataGridViewOverdueBooks = new System.Windows.Forms.DataGridView();
            this.tabControlReports.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewBooks)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMembers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTransactions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewOverdueBooks)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(31, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 40);
            this.label1.TabIndex = 11;
            this.label1.Text = "Reports";
            // 
            // tabControlReports
            // 
            this.tabControlReports.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.tabControlReports.Controls.Add(this.tabPage1);
            this.tabControlReports.Controls.Add(this.tabPage2);
            this.tabControlReports.Controls.Add(this.tabPage3);
            this.tabControlReports.Controls.Add(this.tabPage4);
            this.tabControlReports.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControlReports.Location = new System.Drawing.Point(3, 48);
            this.tabControlReports.Multiline = true;
            this.tabControlReports.Name = "tabControlReports";
            this.tabControlReports.SelectedIndex = 0;
            this.tabControlReports.Size = new System.Drawing.Size(715, 331);
            this.tabControlReports.TabIndex = 0;
            this.tabControlReports.SelectedIndexChanged += new System.EventHandler(this.tabControlReports_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dataGridViewBooks);
            this.tabPage1.Location = new System.Drawing.Point(32, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(679, 323);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Books";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dataGridViewMembers);
            this.tabPage2.Location = new System.Drawing.Point(32, 4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(679, 323);
            this.tabPage2.TabIndex = 0;
            this.tabPage2.Text = "Members";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.dataGridViewTransactions);
            this.tabPage3.Location = new System.Drawing.Point(32, 4);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(679, 323);
            this.tabPage3.TabIndex = 1;
            this.tabPage3.Text = "Transaction";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.dataGridViewOverdueBooks);
            this.tabPage4.Location = new System.Drawing.Point(32, 4);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(679, 323);
            this.tabPage4.TabIndex = 2;
            this.tabPage4.Text = "Overdue";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // dataGridViewBooks
            // 
            this.dataGridViewBooks.AllowUserToAddRows = false;
            this.dataGridViewBooks.AllowUserToDeleteRows = false;
            this.dataGridViewBooks.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewBooks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewBooks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewBooks.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewBooks.Name = "dataGridViewBooks";
            this.dataGridViewBooks.ReadOnly = true;
            this.dataGridViewBooks.Size = new System.Drawing.Size(679, 323);
            this.dataGridViewBooks.TabIndex = 0;
            // 
            // dataGridViewMembers
            // 
            this.dataGridViewMembers.AllowUserToAddRows = false;
            this.dataGridViewMembers.AllowUserToDeleteRows = false;
            this.dataGridViewMembers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewMembers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewMembers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewMembers.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewMembers.Name = "dataGridViewMembers";
            this.dataGridViewMembers.ReadOnly = true;
            this.dataGridViewMembers.Size = new System.Drawing.Size(679, 323);
            this.dataGridViewMembers.TabIndex = 1;
            this.dataGridViewMembers.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewMembers_CellContentClick);
            // 
            // dataGridViewTransactions
            // 
            this.dataGridViewTransactions.AllowUserToAddRows = false;
            this.dataGridViewTransactions.AllowUserToDeleteRows = false;
            this.dataGridViewTransactions.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewTransactions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewTransactions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewTransactions.Location = new System.Drawing.Point(3, 3);
            this.dataGridViewTransactions.Name = "dataGridViewTransactions";
            this.dataGridViewTransactions.ReadOnly = true;
            this.dataGridViewTransactions.Size = new System.Drawing.Size(673, 317);
            this.dataGridViewTransactions.TabIndex = 1;
            // 
            // dataGridViewOverdueBooks
            // 
            this.dataGridViewOverdueBooks.AllowUserToAddRows = false;
            this.dataGridViewOverdueBooks.AllowUserToDeleteRows = false;
            this.dataGridViewOverdueBooks.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewOverdueBooks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewOverdueBooks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewOverdueBooks.Location = new System.Drawing.Point(3, 3);
            this.dataGridViewOverdueBooks.Name = "dataGridViewOverdueBooks";
            this.dataGridViewOverdueBooks.ReadOnly = true;
            this.dataGridViewOverdueBooks.Size = new System.Drawing.Size(673, 317);
            this.dataGridViewOverdueBooks.TabIndex = 1;
            // 
            // UserControlReports
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControlReports);
            this.Controls.Add(this.label1);
            this.Name = "UserControlReports";
            this.Size = new System.Drawing.Size(721, 379);
            this.Load += new System.EventHandler(this.UserControlReports_Load);
            this.tabControlReports.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewBooks)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMembers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTransactions)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewOverdueBooks)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabControl tabControlReports;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.DataGridView dataGridViewBooks;
        private System.Windows.Forms.DataGridView dataGridViewMembers;
        private System.Windows.Forms.DataGridView dataGridViewTransactions;
        private System.Windows.Forms.DataGridView dataGridViewOverdueBooks;
    }
}
