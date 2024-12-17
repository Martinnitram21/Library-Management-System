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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblTotalBooks = new System.Windows.Forms.Label();
            this.lblTotalMembers = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblBorrowedBooks = new System.Windows.Forms.Label();
            this.lblOverdueBooks = new System.Windows.Forms.Label();
            this.chartDashboard = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.label4 = new System.Windows.Forms.Label();
            this.lblActiveBorrowers = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblLateReturns = new System.Windows.Forms.Label();
            this.chartMemberTypes = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.chartDashboard)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartMemberTypes)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(71, 38);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(179, 45);
            this.label1.TabIndex = 0;
            this.label1.Text = "Dashboard";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(95, 126);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 21);
            this.label2.TabIndex = 1;
            this.label2.Text = "Total Books";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(466, 126);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(156, 21);
            this.label3.TabIndex = 1;
            this.label3.Text = "Number of Members";
            // 
            // lblTotalBooks
            // 
            this.lblTotalBooks.AutoSize = true;
            this.lblTotalBooks.Location = new System.Drawing.Point(128, 147);
            this.lblTotalBooks.Name = "lblTotalBooks";
            this.lblTotalBooks.Size = new System.Drawing.Size(17, 21);
            this.lblTotalBooks.TabIndex = 1;
            this.lblTotalBooks.Text = "?";
            // 
            // lblTotalMembers
            // 
            this.lblTotalMembers.AutoSize = true;
            this.lblTotalMembers.Location = new System.Drawing.Point(524, 147);
            this.lblTotalMembers.Name = "lblTotalMembers";
            this.lblTotalMembers.Size = new System.Drawing.Size(17, 21);
            this.lblTotalMembers.TabIndex = 1;
            this.lblTotalMembers.Text = "?";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(231, 126);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(193, 21);
            this.label6.TabIndex = 1;
            this.label6.Text = "Books Currently Borrowed";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(657, 126);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(106, 21);
            this.label7.TabIndex = 1;
            this.label7.Text = "Unpaid Books";
            // 
            // lblBorrowedBooks
            // 
            this.lblBorrowedBooks.AutoSize = true;
            this.lblBorrowedBooks.Location = new System.Drawing.Point(306, 147);
            this.lblBorrowedBooks.Name = "lblBorrowedBooks";
            this.lblBorrowedBooks.Size = new System.Drawing.Size(17, 21);
            this.lblBorrowedBooks.TabIndex = 1;
            this.lblBorrowedBooks.Text = "?";
            // 
            // lblOverdueBooks
            // 
            this.lblOverdueBooks.AutoSize = true;
            this.lblOverdueBooks.Location = new System.Drawing.Point(704, 147);
            this.lblOverdueBooks.Name = "lblOverdueBooks";
            this.lblOverdueBooks.Size = new System.Drawing.Size(17, 21);
            this.lblOverdueBooks.TabIndex = 1;
            this.lblOverdueBooks.Text = "?";
            // 
            // chartDashboard
            // 
            chartArea1.Name = "ChartArea1";
            this.chartDashboard.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chartDashboard.Legends.Add(legend1);
            this.chartDashboard.Location = new System.Drawing.Point(79, 204);
            this.chartDashboard.Name = "chartDashboard";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chartDashboard.Series.Add(series1);
            this.chartDashboard.Size = new System.Drawing.Size(513, 300);
            this.chartDashboard.TabIndex = 2;
            this.chartDashboard.Text = "chart1";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(779, 126);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(128, 21);
            this.label4.TabIndex = 1;
            this.label4.Text = "Active Borrowers";
            // 
            // lblActiveBorrowers
            // 
            this.lblActiveBorrowers.AutoSize = true;
            this.lblActiveBorrowers.Location = new System.Drawing.Point(839, 147);
            this.lblActiveBorrowers.Name = "lblActiveBorrowers";
            this.lblActiveBorrowers.Size = new System.Drawing.Size(17, 21);
            this.lblActiveBorrowers.TabIndex = 1;
            this.lblActiveBorrowers.Text = "?";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(924, 126);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(90, 21);
            this.label8.TabIndex = 1;
            this.label8.Text = "Late Return";
            // 
            // lblLateReturns
            // 
            this.lblLateReturns.AutoSize = true;
            this.lblLateReturns.Location = new System.Drawing.Point(963, 147);
            this.lblLateReturns.Name = "lblLateReturns";
            this.lblLateReturns.Size = new System.Drawing.Size(17, 21);
            this.lblLateReturns.TabIndex = 1;
            this.lblLateReturns.Text = "?";
            // 
            // chartMemberTypes
            // 
            chartArea2.Name = "ChartArea1";
            this.chartMemberTypes.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.chartMemberTypes.Legends.Add(legend2);
            this.chartMemberTypes.Location = new System.Drawing.Point(598, 204);
            this.chartMemberTypes.Name = "chartMemberTypes";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.chartMemberTypes.Series.Add(series2);
            this.chartMemberTypes.Size = new System.Drawing.Size(416, 300);
            this.chartMemberTypes.TabIndex = 3;
            this.chartMemberTypes.Text = "chart1";
            // 
            // UserControlDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chartMemberTypes);
            this.Controls.Add(this.chartDashboard);
            this.Controls.Add(this.lblLateReturns);
            this.Controls.Add(this.lblActiveBorrowers);
            this.Controls.Add(this.lblOverdueBooks);
            this.Controls.Add(this.lblTotalMembers);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.lblBorrowedBooks);
            this.Controls.Add(this.label4);
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
            ((System.ComponentModel.ISupportInitialize)(this.chartDashboard)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartMemberTypes)).EndInit();
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
        private System.Windows.Forms.DataVisualization.Charting.Chart chartDashboard;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblActiveBorrowers;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblLateReturns;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartMemberTypes;
    }
}
