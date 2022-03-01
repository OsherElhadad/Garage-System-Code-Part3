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
    public partial class ShowAccount : Form
    {
        public ShowAccount()
        {
            InitializeComponent();
        }
        string type = "";
        public ShowAccount(string a)
        {
            InitializeComponent();
            type = a;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBox2_TextChanged_1(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(comboBox1.Text) && comboBox1.SelectedIndex >= 0 && comboBox1.Items[comboBox1.SelectedIndex].ToString() == comboBox1.Text)
            {
                string sql1 = "SELECT * FROM Accounts WHERE " + comboBox1.Text + " LIKE'" + comboBox2.Text + "%'";
                if (type == "2")
                    sql1 += " AND Active='True'";
                if (MyAdoHelperCsharp.IsExist("Database1.mdf", sql1))
                {
                    dataGridView1.Visible = true;
                    dataGridView1.DataSource = MyAdoHelperCsharp.ExecuteDataTable("Database1.mdf", sql1);
                }
                else
                    dataGridView1.Visible = false;
            }
            else
                dataGridView1.Visible = false;
        }

        private void ShowAccount_Load(object sender, EventArgs e)
        {
            Checks.FillColumns(comboBox1, "Accounts");
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string ID = dataGridView1[0, e.RowIndex].Value.ToString();
                MessageBox.Show(ID);
                if (type.Equals("1"))
                {
                    UpdateAccount f1 = new UpdateAccount(ID, dataGridView1[1, e.RowIndex].Value.ToString());
                    f1.Show();
                }
                if (type.Equals("2"))
                {
                    DeleteAccount f = new DeleteAccount(ID, dataGridView1[1, e.RowIndex].Value.ToString());
                    f.Show();
                }
            }
            else
                MessageBox.Show("עליך להקליק על אחד מהשורות המלאות");
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sql1 = "SELECT distinct " + comboBox1.Text + " FROM Accounts";
            if (type == "2")
                sql1 += " WHERE Active='True'";
            sql1 += " order by " + comboBox1.Text;
            comboBox2.DataSource = new List<string>();
            comboBox2.ValueMember = "";
            comboBox2.Text = "";
            DataTable dt1 = MyAdoHelperCsharp.ExecuteDataTable("Database1.mdf", sql1);
            comboBox2.AutoCompleteMode = AutoCompleteMode.Suggest;
            comboBox2.AutoCompleteSource = AutoCompleteSource.ListItems;
            comboBox2.DataSource = dt1;
            comboBox2.ValueMember = comboBox1.Text;
            comboBox2.Text = "";
        }
    }
}
