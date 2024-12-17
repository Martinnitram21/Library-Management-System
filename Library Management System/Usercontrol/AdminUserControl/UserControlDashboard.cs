using Library_Management_System.Class;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Library_Management_System.Usercontrol
{
    public partial class UserControlDashboard : UserControl
    {
        public UserControlDashboard()
        {
            InitializeComponent();
            LoadDashboardData();
            LoadBookMetricsChart();
            LoadMemberTypesChart();
        }
        // Connection string (replace with your actual connection string)
        private readonly string connectionString = "Server=localhost;Database=librarydb;Uid=root;Pwd=martinjericho22@2002;";
        private void LoadDashboardData()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    // Fetch data from the database
                    int totalBooks = GetScalarValue(conn, "SELECT COUNT(*) FROM books_tbl");
                    int borrowedBooks = GetScalarValue(conn, "SELECT COUNT(*) FROM books_tbl WHERE book_status = 'Borrowed'");
                    int overdueBooks = GetScalarValue(conn, "SELECT COUNT(*) FROM dues_tbl WHERE dues_Status = 'Unpaid'");
                    int totalMembers = GetScalarValue(conn, "SELECT COUNT(*) FROM members_tbl");
                    int activeBorrowers = GetScalarValue(conn,
                    "SELECT COUNT(DISTINCT b.member_id) " +
                    "FROM dues_tbl d " +
                    "JOIN borrowers_tbl b ON d.borrow_id = b.borrower_id " +
                    "WHERE d.dues_Status = 'Unpaid'");
                    int lateReturns = GetScalarValue(conn, "SELECT COUNT(*) FROM dues_tbl WHERE dues_Status = 'Unpaid'");

                    // Set values to labels
                    lblTotalBooks.Text = totalBooks.ToString();
                    lblBorrowedBooks.Text = borrowedBooks.ToString();
                    lblOverdueBooks.Text = overdueBooks.ToString();
                    lblTotalMembers.Text = totalMembers.ToString();
                    lblActiveBorrowers.Text = activeBorrowers.ToString(); // Add this label in your form
                    lblLateReturns.Text = lateReturns.ToString(); // Add this label in your form
                }

                // Load member types chart
                LoadMemberTypesChart();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading dashboard data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        // --- Load Book Metrics Chart ---
        private void LoadBookMetricsChart()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    int borrowedBooks = GetScalarValue(conn, "SELECT COUNT(*) FROM books_tbl WHERE book_status = 'Borrowed'");
                    int availableBooks = GetScalarValue(conn, "SELECT COUNT(*) FROM books_tbl WHERE book_status = 'Available'");
                    int overdueBooks = GetScalarValue(conn, @"
                        SELECT COUNT(*) 
                        FROM dues_tbl d
                        JOIN borrowers_tbl b ON d.borrow_id = b.borrower_id
                        WHERE d.dues_Status = 'Unpaid' AND b.due_date < NOW()");

                    // Configure the chart
                    ConfigurePieChart(chartDashboard, "Library Book Metrics", new[]
                    {
                        new DataPoint(0, borrowedBooks) { AxisLabel = "Borrowed Books" },
                        new DataPoint(0, availableBooks) { AxisLabel = "Available Books" },
                        new DataPoint(0, overdueBooks) { AxisLabel = "Overdue Books" }
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading book metrics chart: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // --- Load Member Types Chart ---
        private void LoadMemberTypesChart()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    int teachersCount = GetScalarValue(conn, "SELECT COUNT(*) FROM members_tbl WHERE member_type = 'Teacher'");
                    int staffCount = GetScalarValue(conn, "SELECT COUNT(*) FROM members_tbl WHERE member_type = 'Staff'");
                    int studentsCount = GetScalarValue(conn, "SELECT COUNT(*) FROM members_tbl WHERE member_type = 'Student'");

                    // Configure the chart
                    ConfigurePieChart(chartMemberTypes, "Member Metrics", new[]
                    {
                        new DataPoint(0, teachersCount) { AxisLabel = "Teachers" },
                        new DataPoint(0, staffCount) { AxisLabel = "Staff" },
                        new DataPoint(0, studentsCount) { AxisLabel = "Students" }
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading member types chart: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // --- Helper Method: Configure a Pie Chart ---
        private void ConfigurePieChart(Chart chart, string title, DataPoint[] dataPoints)
        {
            // Clear previous settings
            chart.Series.Clear();
            chart.Titles.Clear();

            // Add a new series
            var series = new Series
            {
                ChartType = SeriesChartType.Pie,
                IsValueShownAsLabel = true
            };

            // Add data points
            foreach (var dataPoint in dataPoints)
            {
                series.Points.Add(dataPoint);
            }
            // Add series to chart
            chart.Series.Add(series);

            // Configure chart appearance
            chart.Titles.Add(title);
            chart.ChartAreas[0].Area3DStyle.Enable3D = true; // Optional: 3D effect
        }

        private int GetScalarValue(MySqlConnection conn, string query)
        {
            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                object result = cmd.ExecuteScalar();
                return result != null ? Convert.ToInt32(result) : 0;
            }
        }
    }
}
