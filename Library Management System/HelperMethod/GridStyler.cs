using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Library_Management_System
{
    internal class GridStyler
    {
        public static void ApplyStyle(DataGridView dgv)
        {
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.Gainsboro;
            dgv.DefaultCellStyle.Padding = new Padding(5, 3, 5, 3);
            dgv.RowTemplate.Height = 30;
            
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10);
            //dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.LightSteelBlue;
            //dgv.EnableHeadersVisualStyles = false;
            
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;
        }
    }
}
