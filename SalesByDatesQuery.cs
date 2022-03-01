using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OsherProject
{
    public partial class SalesByDatesQuery : Form
    {
        public SalesByDatesQuery()
        {
            InitializeComponent();
            Design.Designer(this);
            Sets.SetGeneral(comboBox1);
            dateTimePicker1.MaxDate = DateTime.Today;
            dateTimePicker2.MaxDate = DateTime.Today;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult r = MessageBox.Show("?האם אתה בטוח שאתה רוצה לצאת מהפעולה", "יציאה", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
            if (r == DialogResult.Yes)
            {
                UserLogIn.myforms[UserLogIn.myforms.Count - 1].Close();
                UserLogIn.myforms.Remove(UserLogIn.myforms[UserLogIn.myforms.Count - 1]);
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            dateTimePicker2.MinDate = dateTimePicker1.Value;
            string sql1 = "SELECT SaleNumber AS 'מספר מכירה', RowNumber AS 'מספר שורה', ItemNumber AS 'מספר פריט', Amount AS 'כמות', Price AS 'מחיר', CarNumber AS 'מספר רכב', StartDate AS 'תאריך התחלה', SupposeEndDate AS 'תאריך משוער לסיום', EndDate AS 'תאריך סיום', PaymentMethods.Method AS 'אמצעי תשלום', Description AS 'תיאור', Active AS 'פעיל' FROM Sales, PaymentMethods WHERE StartDate >='" + dateTimePicker1.Value.ToString("MM/dd/yyyy") + "' AND StartDate <='" + dateTimePicker2.Value.ToString("MM/dd/yyyy") + "' AND PaymentMethods.Number=PaymentMethod AND CarNumber LIKE'" + comboBox1.Text + "%' AND Sales.Active2='true'";
            if (MyAdoHelperCsharp.IsExist("Database1.mdf", sql1))
            {
                dataGridView1.Visible = true;
                dataGridView1.DataSource = MyAdoHelperCsharp.ExecuteDataTable("Database1.mdf", sql1);
            }
            else
                dataGridView1.Visible = false;
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            string sql1 = "SELECT SaleNumber AS 'מספר מכירה', RowNumber AS 'מספר שורה', ItemNumber AS 'מספר פריט', Amount AS 'כמות', Price AS 'מחיר', CarNumber AS 'מספר רכב', StartDate AS 'תאריך התחלה', SupposeEndDate AS 'תאריך משוער לסיום', EndDate AS 'תאריך סיום', PaymentMethods.Method AS 'אמצעי תשלום', Description AS 'תיאור', Active AS 'פעיל' FROM Sales, PaymentMethods WHERE StartDate >='" + dateTimePicker1.Value.ToString("MM/dd/yyyy") + "' AND StartDate <='" + dateTimePicker2.Value.ToString("MM/dd/yyyy") + "' AND PaymentMethods.Number=PaymentMethod AND CarNumber LIKE'" + comboBox1.Text + "%' AND Sales.Active2='true'";
            if (MyAdoHelperCsharp.IsExist("Database1.mdf", sql1))
            {
                dataGridView1.Visible = true;
                dataGridView1.DataSource = MyAdoHelperCsharp.ExecuteDataTable("Database1.mdf", sql1);
            }
            else
                dataGridView1.Visible = false;
        }

        private void SalesByDatesQuery_Load(object sender, EventArgs e)
        {
            string sql1 = "SELECT distinct CarNumber FROM Sales WHERE Sales.Active2='true' order by CarNumber";
            DataTable dt1 = MyAdoHelperCsharp.ExecuteDataTable("Database1.mdf", sql1);
            comboBox1.AutoCompleteMode = AutoCompleteMode.Suggest;
            comboBox1.AutoCompleteSource = AutoCompleteSource.ListItems;
            comboBox1.DataSource = dt1;
            comboBox1.ValueMember = "CarNumber";
            comboBox1.Text = "";
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            string sql1 = "SELECT SaleNumber AS 'מספר מכירה', RowNumber AS 'מספר שורה', ItemNumber AS 'מספר פריט', Amount AS 'כמות', Price AS 'מחיר', CarNumber AS 'מספר רכב', StartDate AS 'תאריך התחלה', SupposeEndDate AS 'תאריך משוער לסיום', EndDate AS 'תאריך סיום', PaymentMethods.Method AS 'אמצעי תשלום', Description AS 'תיאור', Active AS 'פעיל' FROM Sales, PaymentMethods WHERE StartDate >='" + dateTimePicker1.Value.ToString("MM/dd/yyyy") + "' AND StartDate <='" + dateTimePicker2.Value.ToString("MM/dd/yyyy") + "' AND PaymentMethods.Number=PaymentMethod AND CarNumber LIKE'" + comboBox1.Text + "%' AND Sales.Active2='true'";
            if (MyAdoHelperCsharp.IsExist("Database1.mdf", sql1))
            {
                dataGridView1.Visible = true;
                dataGridView1.DataSource = MyAdoHelperCsharp.ExecuteDataTable("Database1.mdf", sql1);
            }
            else
                dataGridView1.Visible = false;
        }
    }
}
