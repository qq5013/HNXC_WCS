using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace THOK.XC.Dispatching.OperateView
{
    public partial class SelectDialog : Form
    {
        private List<Dictionary<string, string>> selectedValues = new List<Dictionary<string, string>>();

        public string this[string key]
        {
            get { return selectedValues[0][key]; }
        }
 
        public Dictionary<string, string> SelectedValue
        {
            get { return selectedValues[0]; }
        }

        public List<Dictionary<string, string>> SelectedValues
        {
            get { return selectedValues; }
        }

        public SelectDialog(DataTable table, Dictionary<string, string> fields, bool isMultiSelect)
        {
            InitializeComponent();
            foreach (string columnName in fields.Keys)
            {
                if (table.Columns.Contains(columnName))
                {
                    DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn c = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
                    c.HeaderText = fields[columnName];
                    c.DataPropertyName = columnName;

                    dgvMain.Columns.Add(c);
                }
            }
            dgvMain.MultiSelect = isMultiSelect;
            bsMain.DataSource = table;
        }

        private void dgvMain_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ReturnSelectedValues();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            ReturnSelectedValues();
        }

        private void ReturnSelectedValues()
        {
            if (dgvMain.SelectedRows.Count != 0)
            {
                selectedValues.Clear();
                foreach (DataGridViewRow row in dgvMain.SelectedRows)
                {
                    Dictionary<string, string> selectedValue = new Dictionary<string, string>();
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        selectedValue.Add(cell.OwningColumn.DataPropertyName, cell.Value.ToString());
                    }
                    selectedValues.Add(selectedValue);
                }
                DialogResult = DialogResult.OK;
            }
        }
    }
}