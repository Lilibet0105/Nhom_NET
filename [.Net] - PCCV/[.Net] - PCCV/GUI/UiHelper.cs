using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace GUI
{
    public static class UiHelper
    {
        public static string FormatCode(string prefix, object value)
        {
            if (value == null || value == DBNull.Value)
            {
                return prefix + "0000";
            }

            int number;
            return int.TryParse(value.ToString(), out number)
                ? prefix + number.ToString("D4")
                : value.ToString();
        }

        public static int ParseCode(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return 0;
            }

            Match match = Regex.Match(value, @"\d+");
            return match.Success && int.TryParse(match.Value, out int number) ? number : 0;
        }

        public static void StyleGrid(DataGridView grid)
        {
            grid.AllowUserToAddRows = false;
            grid.AllowUserToDeleteRows = false;
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grid.BackgroundColor = Color.White;
            grid.BorderStyle = BorderStyle.None;
            grid.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            grid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            grid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(41, 53, 65);
            grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            grid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            grid.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            grid.ColumnHeadersHeight = 38;
            grid.DefaultCellStyle.BackColor = Color.White;
            grid.DefaultCellStyle.ForeColor = Color.FromArgb(31, 41, 55);
            grid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(219, 234, 254);
            grid.DefaultCellStyle.SelectionForeColor = Color.FromArgb(17, 24, 39);
            grid.EnableHeadersVisualStyles = false;
            grid.GridColor = Color.FromArgb(226, 232, 240);
            grid.MultiSelect = false;
            grid.ReadOnly = true;
            grid.RowHeadersVisible = false;
            grid.RowTemplate.Height = 32;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);
        }

        public static DataGridViewTextBoxColumn TextColumn(string name, string property, string header, int width, float fillWeight = 100)
        {
            return new DataGridViewTextBoxColumn
            {
                Name = name,
                DataPropertyName = property,
                HeaderText = header,
                Width = width,
                FillWeight = fillWeight,
                MinimumWidth = Math.Min(width, 80)
            };
        }
    }
}
