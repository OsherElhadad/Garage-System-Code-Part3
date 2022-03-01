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
    public partial class UpdateOrder : Form
    {
        public UpdateOrder()
        {
            InitializeComponent();
            comboBox1.Height = 21;
        }
        string ID="";
        string r;
        int amountorder;
        decimal a = 0;
        public UpdateOrder(string id, string row)
        {
            InitializeComponent();
            Sets.SetNumber(textBox3);
            Sets.SetNumber(textBox4);
            Sets.SetNumber(textBox5);
            Sets.SetDate(dateTimePicker1);
            comboBox2.Text = row;
            ID = id;
            comboBox1.Text = ID;
            comboBox1.Height = 21;
            r = row;
            string sql = "SELECT Price FROM Orders WHERE OrderNumber='" + comboBox1.Text + "'";
            DataTable dt = MyAdoHelperCsharp.ExecuteDataTable("Database1.mdf", sql);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                a += (decimal)dt.Rows[i]["Price"];
            }
            string sql1 = "SELECT Price FROM Orders WHERE OrderNumber='" + comboBox1.Text + "' AND RowNumber='" + row + "'";
            DataTable dt1 = MyAdoHelperCsharp.ExecuteDataTable("Database1.mdf", sql1);
            a -= (decimal)dt1.Rows[0]["Price"];
            textBox7.Text = a.ToString();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (label11.Text == "תקין" && label13.Text == "תקין" && MyAdoHelperCsharp.IsExist("Database1.mdf", "SELECT * FROM Orders WHERE OrderNumber='" + comboBox1.Text + "' AND RowNumber='" + comboBox2.Text + "';"))
            {
                string update = "UPDATE Orders SET StartDate=N'" + dateTimePicker1.Value + "', SupposeEndDate=N'" + dateTimePicker2.Value;
                if (dateTimePicker3.Visible)
                    update += "', EndDate=N'" + dateTimePicker3.Value;
                update += "', ItemNumber='" + textBox3.Text + "', Amount='" + textBox4.Text + "', SupplierNumber='" + textBox5.Text + "', Price='" + textBox2.Text + "', Active='" + checkBox1.Checked + "' WHERE OrderNumber='" + comboBox1.Text + "' AND RowNumber='" + comboBox2.Text + "';";
                MyAdoHelperCsharp.DoQuery("Database1.mdf", update);
                string sql = "SELECT Amount FROM Items WHERE ItemNumber='" + textBox3.Text + "'";
                DataTable dt = MyAdoHelperCsharp.ExecuteDataTable("Database1.mdf", sql);
                int amount = int.Parse(dt.Rows[0]["Amount"].ToString()) + int.Parse(textBox4.Text) - amountorder;
                string update1 = "UPDATE Items SET Amount='" + amount + "' WHERE ItemNumber='" + textBox3.Text + "'";
                MyAdoHelperCsharp.DoQuery("Database1.mdf", update1);
                MessageBox.Show("הזמנה עודכנה בהצלחה");
            }
            else
                MessageBox.Show("עליך לתקן את הטופס");
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            string sql1 = "SELECT * FROM Orders WHERE OrderNumber='" + ID + "' AND RowNumber='" + comboBox2.Text + "'";
            if (MyAdoHelperCsharp.IsExist("Database1.mdf", sql1))
            {
                groupBox1.Visible = true;
                DataTable dt = MyAdoHelperCsharp.ExecuteDataTable("Database1.mdf", sql1);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    comboBox2.Text = dt.Rows[i]["RowNumber"].ToString();
                    dateTimePicker1.Text = dt.Rows[i]["StartDate"].ToString();
                    dateTimePicker2.MinDate = dateTimePicker1.Value;
                    dateTimePicker3.MinDate = dateTimePicker1.Value;
                    dateTimePicker2.Text = dt.Rows[i]["SupposeEndDate"].ToString();
                    if (!string.IsNullOrEmpty(dt.Rows[i]["EndDate"].ToString()))
                        dateTimePicker3.Text = dt.Rows[i]["EndDate"].ToString();
                    else
                    {
                        label5.Visible = false;
                        dateTimePicker3.Visible = false;
                    }
                    textBox3.Text = dt.Rows[i]["ItemNumber"].ToString();
                    textBox4.Text = dt.Rows[i]["Amount"].ToString();
                    amountorder = int.Parse(textBox4.Text);
                    textBox5.Text = dt.Rows[i]["SupplierNumber"].ToString();
                    textBox2.Text = dt.Rows[i]["Price"].ToString();
                    checkBox1.Checked = (bool)dt.Rows[i]["Active"];
                }
            }
            else
                groupBox1.Visible = false;
        }
        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            string sql = "SELECT * FROM Items WHERE ItemNumber='" + textBox3.Text + "'";
            if (string.IsNullOrEmpty(textBox3.Text))
            {
                textBox3.BackColor = Color.Red;
                label11.ForeColor = Color.Red;
                label11.Text = "מספר פריט זה לא תקין";
            }
            else
            {
                if (!MyAdoHelperCsharp.IsExist("Database1.mdf", sql))
                {
                    textBox3.BackColor = Color.Red;
                    label11.ForeColor = Color.Red;
                    label11.Text = "מספר פריט זה לא קיים במאגר";
                }
                else
                {
                    textBox3.BackColor = Color.Green;
                    label11.ForeColor = Color.Green;
                    label11.Text = "תקין";
                }
            }
            string sql1 = "SELECT * FROM Unions WHERE SupplierNumber='" + textBox5.Text + "' AND ItemNumber='" + textBox3.Text + "'";
            if (string.IsNullOrEmpty(textBox5.Text))
            {
                textBox5.BackColor = Color.Red;
                label13.ForeColor = Color.Red;
                label13.Text = "מספר ספק זה לא תקין";
            }
            else
            {
                if (!MyAdoHelperCsharp.IsExist("Database1.mdf", sql1))
                {
                    textBox5.BackColor = Color.Red;
                    label13.ForeColor = Color.Red;
                    label13.Text = "מספר ספק זה לא קיים במאגר";
                }
                else
                {
                    textBox5.BackColor = Color.Green;
                    label13.ForeColor = Color.Green;
                    label13.Text = "תקין";
                }
            }
            if (label11.Text == "תקין" && label13.Text == "תקין")
                textBox4.Text = "1";
            else
                textBox4.Text = "0";
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            string sql = "SELECT PriceFromSupplier FROM Unions WHERE SupplierNumber='" + textBox5.Text + "' AND ItemNumber='" + textBox3.Text + "'";
            if (int.Parse(textBox4.Text) > 0 && MyAdoHelperCsharp.IsExist("Database1.mdf", sql))
            {
                DataTable dt = MyAdoHelperCsharp.ExecuteDataTable("Database1.mdf", sql);
                decimal x = decimal.Parse(dt.Rows[0]["PriceFromSupplier"].ToString());
                decimal y = decimal.Parse(textBox4.Text) * x;
                textBox2.Text = y.ToString();
                string sql4 = "SELECT * FROM Orders WHERE OrderNumber='" + comboBox1.Text + "' AND RowNumber='" + r + "'";
                DataTable dt3 = MyAdoHelperCsharp.ExecuteDataTable("Database1.mdf", sql4);
                textBox7.Text = (a + y).ToString();
            }
            else
            {
                textBox2.Text = "0.00";
                textBox7.Text = a.ToString();
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            string sql = "SELECT * FROM Unions WHERE SupplierNumber='" + textBox5.Text + "' AND ItemNumber='" + textBox3.Text + "'";
            if (string.IsNullOrEmpty(textBox5.Text))
            {
                textBox5.BackColor = Color.Red;
                label13.ForeColor = Color.Red;
                label13.Text = "מספר ספק זה לא תקין";
            }
            else
            {
                if (!MyAdoHelperCsharp.IsExist("Database1.mdf", sql))
                {
                    textBox5.BackColor = Color.Red;
                    label13.ForeColor = Color.Red;
                    label13.Text = "מספר ספק זה לא קיים במאגר";
                }
                else
                {
                    textBox5.BackColor = Color.Green;
                    label13.ForeColor = Color.Green;
                    label13.Text = "תקין";
                }
            }
            if (label11.Text == "תקין" && label13.Text == "תקין")
                textBox4.Text = "1";
            else
                textBox4.Text = "0";
        }

        private void UpdateOrder_Load(object sender, EventArgs e)
        {
            string sql1 = "SELECT distinct RowNumber FROM Orders WHERE OrderNumber='" + ID + "' order by RowNumber";
            DataTable dt1 = MyAdoHelperCsharp.ExecuteDataTable("Database1.mdf", sql1);
            comboBox2.AutoCompleteMode = AutoCompleteMode.Suggest;
            comboBox2.AutoCompleteSource = AutoCompleteSource.ListItems;
            comboBox2.DataSource = dt1;
            comboBox2.ValueMember = "RowNumber";
            comboBox2.Text = "";
            comboBox2.Text = r;
        }

        private void comboBox2_TextChanged(object sender, EventArgs e)
        {
            string sql1 = "SELECT * FROM Orders WHERE OrderNumber='" + ID + "' AND RowNumber='" + comboBox2.Text + "'";
            if (MyAdoHelperCsharp.IsExist("Database1.mdf", sql1))
            {
                groupBox1.Visible = true;
                DataTable dt = MyAdoHelperCsharp.ExecuteDataTable("Database1.mdf", sql1);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dateTimePicker1.Text = dt.Rows[i]["StartDate"].ToString();
                    dateTimePicker2.MinDate = dateTimePicker1.Value;
                    dateTimePicker3.MinDate = dateTimePicker1.Value;
                    dateTimePicker2.Text = dt.Rows[i]["SupposeEndDate"].ToString();
                    if (!string.IsNullOrEmpty(dt.Rows[i]["EndDate"].ToString()))
                        dateTimePicker3.Text = dt.Rows[i]["EndDate"].ToString();
                    else
                    {
                        label5.Visible = false;
                        dateTimePicker3.Visible = false;
                    }
                    textBox3.Text = dt.Rows[i]["ItemNumber"].ToString();
                    textBox4.Text = dt.Rows[i]["Amount"].ToString();
                    amountorder = int.Parse(textBox4.Text);
                    textBox5.Text = dt.Rows[i]["SupplierNumber"].ToString();
                    textBox2.Text = dt.Rows[i]["Price"].ToString();
                    checkBox1.Checked = (bool)dt.Rows[i]["Active"];
                    a = 0;
                    string sql = "SELECT Price FROM Orders WHERE OrderNumber='" + ID + "'";
                    DataTable dt1 = MyAdoHelperCsharp.ExecuteDataTable("Database1.mdf", sql);
                    for (int j = 0; j < dt1.Rows.Count; j++)
                    {
                        a += (decimal)dt1.Rows[j]["Price"];
                    }
                    textBox7.Text = a.ToString();
                    a -= (decimal)dt.Rows[i]["Price"];
                }
            }
            else
                groupBox1.Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AddOrder f = new AddOrder(comboBox1.Text, comboBox2.Text, "u");
            f.Show();
            this.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (label11.Text == "תקין" && label13.Text == "תקין" && !string.IsNullOrEmpty(textBox4.Text))
                textBox4.Text = (int.Parse(textBox4.Text) + 1).ToString();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (textBox4.Text != "0" && textBox4.Text != "1" && !string.IsNullOrEmpty(textBox4.Text))
                textBox4.Text = (int.Parse(textBox4.Text) - 1).ToString();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            dateTimePicker2.MinDate = dateTimePicker1.Value;
            dateTimePicker3.MinDate = dateTimePicker1.Value;
        }
    }
}
