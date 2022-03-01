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
    public partial class UpdateSale : Form
    {
        public UpdateSale()
        {
            InitializeComponent();
            comboBox1.Height = 21;
        }
        string ID="";
        string r;
        int amountorder;
        decimal a = 0;
        public UpdateSale(string id, string row)
        {
            InitializeComponent();
            Sets.SetNumber(textBox1);
            Sets.SetNumber(textBox3);
            Sets.SetPrice(textBox4);
            Sets.SetCarNumber(textBox5);
            Sets.SetPayment(comboBox3);
            Sets.SetNote(textBox2);
            Sets.SetDate(dateTimePicker1);

            string sql2 = "SELECT distinct Method FROM PaymentMethods order by Method";
            DataTable dt2 = MyAdoHelperCsharp.ExecuteDataTable("Database1.mdf", sql2);
            comboBox3.AutoCompleteMode = AutoCompleteMode.Suggest;
            comboBox3.AutoCompleteSource = AutoCompleteSource.ListItems;
            comboBox3.DataSource = dt2;
            comboBox3.ValueMember = "Method";
            comboBox3.Text = "";

            comboBox2.Text = row;
            ID = id;
            comboBox1.Text = ID;
            comboBox1.Height = 21;
            r = row;
            string sql = "SELECT Price FROM Sales WHERE SaleNumber='" + comboBox1.Text + "'";
            DataTable dt = MyAdoHelperCsharp.ExecuteDataTable("Database1.mdf", sql);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                a += (decimal)dt.Rows[i]["Price"];
            }
            string sql1 = "SELECT Price FROM Sales WHERE SaleNumber='" + comboBox1.Text + "' AND RowNumber='" + row + "'";
            DataTable dt1 = MyAdoHelperCsharp.ExecuteDataTable("Database1.mdf", sql1);
            a -= (decimal)dt1.Rows[0]["Price"];
            textBox6.Text = a.ToString();
        }
        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            string sql1 = "SELECT * FROM Sales WHERE SaleNumber='" + ID + "' AND RowNumber='" + comboBox2.Text + "'";
            if (MyAdoHelperCsharp.IsExist("Database1.mdf", sql1))
            {
                groupBox1.Visible = true;
                DataTable dt = MyAdoHelperCsharp.ExecuteDataTable("Database1.mdf", sql1);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    comboBox2.Text = dt.Rows[i]["RowNumber"].ToString();
                    textBox1.Text = dt.Rows[i]["ItemNumber"].ToString();
                    textBox3.Text = dt.Rows[i]["Amount"].ToString();
                    amountorder = int.Parse(textBox3.Text);
                    textBox4.Text = dt.Rows[i]["Price"].ToString();
                    textBox5.Text = dt.Rows[i]["CarNumber"].ToString();
                    comboBox3.Text = dt.Rows[i]["PaymentMethod"].ToString();
                    textBox2.Text = "f";
                    textBox2.Text = dt.Rows[i]["Description"].ToString();
                    dateTimePicker1.Text = dt.Rows[i]["StartDate"].ToString();
                    dateTimePicker2.MinDate = dateTimePicker1.Value;
                    dateTimePicker3.MinDate = dateTimePicker1.Value;
                    dateTimePicker2.Text = dt.Rows[i]["SupposeEndDate"].ToString();
                    if (!string.IsNullOrEmpty(dt.Rows[i]["EndDate"].ToString()))
                        dateTimePicker3.Text = dt.Rows[i]["EndDate"].ToString();
                    else
                    {
                        label19.Visible = false;
                        dateTimePicker3.Visible = false;
                    }
                    checkBox1.Checked = (bool)dt.Rows[i]["Active"];
                }
            }
            else
                groupBox1.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (label10.Text == "תקין" && label13.Text == "תקין" && label14.Text == "תקין" && label15.Text == "תקין" && label17.Text == "תקין" && MyAdoHelperCsharp.IsExist("Database1.mdf", "SELECT * FROM Sales WHERE SaleNumber='" + comboBox1.Text + "' AND RowNumber='" + comboBox2.Text + "';"))
            {
                string sql1 = "SELECT * FROM PaymentMethods WHERE Method='" + comboBox3.Text + "'";
                DataTable dt1 = MyAdoHelperCsharp.ExecuteDataTable("Database1.mdf", sql1);
                string update = "UPDATE Sales SET ItemNumber='" + textBox1.Text + "', Amount='" + textBox3.Text + "', Price='" + textBox4.Text + "', CarNumber='" + textBox5.Text;
                update += "', StartDate=N'" + dateTimePicker1.Value + "', SupposeEndDate=N'" + dateTimePicker2.Value;
                if (dateTimePicker3.Visible)
                    update += "', EndDate=N'" + dateTimePicker3.Value;
                update += "', PaymentMethod='" + dt1.Rows[0]["Number"].ToString() + "', Description='" + textBox2.Text + "', Active='" + checkBox1.Checked + "' WHERE SaleNumber='" + comboBox1.Text + "' AND RowNumber='" + comboBox2.Text + "';";
                string sql = "SELECT Amount FROM Items WHERE ItemNumber='" + textBox1.Text + "'";
                DataTable dt = MyAdoHelperCsharp.ExecuteDataTable("Database1.mdf", sql);
                int amount = int.Parse(dt.Rows[0]["Amount"].ToString()) - int.Parse(textBox3.Text) + amountorder;
                if (amount >= 0)
                {
                    MyAdoHelperCsharp.DoQuery("Database1.mdf", update);
                    string update1 = "UPDATE Items SET Amount='" + amount + "' WHERE ItemNumber='" + textBox1.Text + "'";
                    MyAdoHelperCsharp.DoQuery("Database1.mdf", update1);
                    MessageBox.Show("מכירה עודכנה בהצלחה");
                }
                else
                {
                    MessageBox.Show("כמות זו של פריט זה לא קיימת במאגר");
                    DialogResult r = MessageBox.Show("?האם אתה רוצה להזמין פריט זה", "הזמנה", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (r == DialogResult.Yes)
                    {
                        AddOrder f = new AddOrder(textBox1.Text);
                        f.Show();
                    }
                }
            }
            else
                MessageBox.Show("עליך לתקן את הטופס");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string sql = "SELECT * FROM Items WHERE ItemNumber='" + textBox1.Text + "'";
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                textBox1.BackColor = Color.Red;
                label10.ForeColor = Color.Red;
                label10.Text = "מספר פריט זה לא תקין";
            }
            else
            {
                if (!MyAdoHelperCsharp.IsExist("Database1.mdf", sql))
                {
                    textBox1.BackColor = Color.Red;
                    label10.ForeColor = Color.Red;
                    label10.Text = "מספר פריט זה לא קיים במאגר";
                }
                else
                {
                    textBox1.BackColor = Color.Green;
                    label10.ForeColor = Color.Green;
                    label10.Text = "תקין";
                }
            }
            if (label10.Text == "תקין")
                textBox3.Text = "1";
            else
                textBox3.Text = "0";
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            decimal z = 1;
            string sql = "SELECT PriceList FROM Items WHERE ItemNumber='" + textBox1.Text + "'";
            if (int.Parse(textBox3.Text) > 0 && MyAdoHelperCsharp.IsExist("Database1.mdf", sql))
            {
                string sql1 = "SELECT * FROM Cars WHERE CarNumber='" + textBox5.Text + "' AND GarageNumber!=''";
                if (MyAdoHelperCsharp.IsExist("Database1.mdf", sql1))
                {
                    DataTable dt1 = MyAdoHelperCsharp.ExecuteDataTable("Database1.mdf", sql1);
                    string sql2 = "SELECT * FROM Garages WHERE GarageNumber='" + dt1.Rows[0]["GarageNumber"].ToString() + "'";
                    if (MyAdoHelperCsharp.IsExist("Database1.mdf", sql2))
                    {
                        DataTable dt2 = MyAdoHelperCsharp.ExecuteDataTable("Database1.mdf", sql2);
                        z = 1 - (decimal.Parse(dt2.Rows[0]["Discount"].ToString()) / 100);
                    }
                }
                DataTable dt = MyAdoHelperCsharp.ExecuteDataTable("Database1.mdf", sql);
                decimal x = decimal.Parse(dt.Rows[0]["PriceList"].ToString());
                decimal y = decimal.Parse(textBox3.Text) * x * z;
                textBox4.Text = y.ToString();
                string sql4 = "SELECT * FROM Sales WHERE SaleNumber='" + comboBox1.Text + "' AND RowNumber='" + r + "'";
                DataTable dt3 = MyAdoHelperCsharp.ExecuteDataTable("Database1.mdf", sql4);
                textBox6.Text = (a + y).ToString();
            }
            else
            {
                textBox4.Text = "0.00";
                textBox6.Text = a.ToString();
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox4.Text))
            {
                textBox4.BackColor = Color.Red;
                label13.ForeColor = Color.Red;
                label13.Text = "מחיר זה לא תקין";
            }
            else
            {
                textBox4.BackColor = Color.Green;
                label13.ForeColor = Color.Green;
                label13.Text = "תקין";
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            string sql = "SELECT * FROM Cars WHERE CarNumber='" + textBox5.Text + "'";
            if (string.IsNullOrEmpty(textBox5.Text))
            {
                textBox5.BackColor = Color.Red;
                label14.ForeColor = Color.Red;
                label14.Text = "מספר רכב זה לא תקין";
            }
            else
            {
                if (!MyAdoHelperCsharp.IsExist("Database1.mdf", sql))
                {
                    textBox5.BackColor = Color.Red;
                    label14.ForeColor = Color.Red;
                    label14.Text = "מספר רכב זה לא קיים במאגר";
                }
                else
                {
                    textBox5.BackColor = Color.Green;
                    label14.ForeColor = Color.Green;
                    label14.Text = "תקין";
                }
            }
            decimal z = 1;
            string sql3 = "SELECT PriceList FROM Items WHERE ItemNumber='" + textBox1.Text + "'";
            if (int.Parse(textBox3.Text) > 0 && MyAdoHelperCsharp.IsExist("Database1.mdf", sql3))
            {
                string sql1 = "SELECT * FROM Cars WHERE CarNumber='" + textBox5.Text + "' AND GarageNumber!=''";
                if (MyAdoHelperCsharp.IsExist("Database1.mdf", sql1))
                {
                    DataTable dt1 = MyAdoHelperCsharp.ExecuteDataTable("Database1.mdf", sql1);
                    string sql2 = "SELECT * FROM Garages WHERE GarageNumber='" + dt1.Rows[0]["GarageNumber"].ToString() + "'";
                    if (MyAdoHelperCsharp.IsExist("Database1.mdf", sql2))
                    {
                        DataTable dt2 = MyAdoHelperCsharp.ExecuteDataTable("Database1.mdf", sql2);
                        z = 1 - (decimal.Parse(dt2.Rows[0]["Discount"].ToString()) / 100);
                    }
                }
                DataTable dt = MyAdoHelperCsharp.ExecuteDataTable("Database1.mdf", sql3);
                decimal x = decimal.Parse(dt.Rows[0]["PriceList"].ToString());
                decimal y = decimal.Parse(textBox3.Text) * x * z;
                textBox4.Text = y.ToString();
                string sql4 = "SELECT * FROM Sales WHERE SaleNumber='" + comboBox1.Text + "' AND RowNumber='" + r + "'";
                DataTable dt3 = MyAdoHelperCsharp.ExecuteDataTable("Database1.mdf", sql4);
                textBox6.Text = (a + y).ToString();
            }
            else
            {
                textBox4.Text = "0.00";
                textBox6.Text = a.ToString();
            }
        }
        private void UpdateSale_Load(object sender, EventArgs e)
        {
            string sql1 = "SELECT distinct RowNumber FROM Sales WHERE SaleNumber='" + ID + "' order by RowNumber";
            DataTable dt1 = MyAdoHelperCsharp.ExecuteDataTable("Database1.mdf", sql1);
            comboBox2.AutoCompleteSource = AutoCompleteSource.ListItems;
            comboBox2.DataSource = dt1;
            comboBox2.ValueMember = "RowNumber";
            comboBox2.Text = "";
            comboBox2.Text = r;
        }

        private void comboBox2_TextChanged(object sender, EventArgs e)
        {
            string sql1 = "SELECT * FROM Sales WHERE SaleNumber='" + ID + "' AND RowNumber='" + comboBox2.Text + "'";
            if (MyAdoHelperCsharp.IsExist("Database1.mdf", sql1))
            {
                groupBox1.Visible = true;
                DataTable dt = MyAdoHelperCsharp.ExecuteDataTable("Database1.mdf", sql1);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    textBox1.Text = dt.Rows[i]["ItemNumber"].ToString();
                    textBox3.Text = dt.Rows[i]["Amount"].ToString();
                    amountorder = int.Parse(textBox3.Text);
                    textBox4.Text = dt.Rows[i]["Price"].ToString();
                    textBox5.Text = dt.Rows[i]["CarNumber"].ToString();
                    comboBox3.Text = dt.Rows[i]["PaymentMethod"].ToString();
                    textBox2.Text = "f";
                    textBox2.Text = dt.Rows[i]["Description"].ToString();
                    dateTimePicker1.Text = dt.Rows[i]["StartDate"].ToString();
                    dateTimePicker2.MinDate = dateTimePicker1.Value;
                    dateTimePicker3.MinDate = dateTimePicker1.Value;
                    dateTimePicker2.Text = dt.Rows[i]["SupposeEndDate"].ToString();
                    if (!string.IsNullOrEmpty(dt.Rows[i]["EndDate"].ToString()))
                        dateTimePicker3.Text = dt.Rows[i]["EndDate"].ToString();
                    else
                    {
                        label19.Visible = false;
                        dateTimePicker3.Visible = false;
                    }
                    checkBox1.Checked = (bool)dt.Rows[i]["Active"];
                    a = 0;
                    string sql = "SELECT Price FROM Sales WHERE SaleNumber='" + ID + "'";
                    DataTable dt1 = MyAdoHelperCsharp.ExecuteDataTable("Database1.mdf", sql);
                    for (int j = 0; j < dt1.Rows.Count; j++)
                    {
                        a += (decimal)dt1.Rows[j]["Price"];
                    }
                    textBox6.Text = a.ToString();
                    a -= (decimal)dt.Rows[i]["Price"];
                }
            }
            else
                groupBox1.Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AddSale f = new AddSale(comboBox1.Text, comboBox2.Text, "u");
            f.Show();
            this.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (label10.Text == "תקין" && !string.IsNullOrEmpty(textBox3.Text))
            {
                string sql = "SELECT Amount FROM Items WHERE ItemNumber='" + textBox1.Text + "'";
                DataTable dt = MyAdoHelperCsharp.ExecuteDataTable("Database1.mdf", sql);
                int amount = int.Parse(dt.Rows[0]["Amount"].ToString()) - int.Parse(textBox3.Text) + amountorder;
                if (amount >= 0)
                    textBox3.Text = (int.Parse(textBox3.Text) + 1).ToString();
                else
                {
                    MessageBox.Show("כמות זו של פריט זה לא קיימת במאגר");
                    DialogResult r = MessageBox.Show("?האם אתה רוצה להזמין פריט זה", "הזמנה", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (r == DialogResult.Yes)
                    {
                        AddOrder f = new AddOrder(textBox1.Text);
                        f.Show();
                    }
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (textBox3.Text != "0" && textBox3.Text != "1" && !string.IsNullOrEmpty(textBox3.Text))
                textBox3.Text = (int.Parse(textBox3.Text) - 1).ToString();
        }

        private void comboBox3_TextChanged(object sender, EventArgs e)
        {
            string sql = "SELECT * FROM PaymentMethods WHERE Method='" + comboBox3.Text + "'";
            if (!MyAdoHelperCsharp.IsExist("Database1.mdf", sql))
            {
                comboBox3.BackColor = Color.Red;
                label15.ForeColor = Color.Red;
                label15.Text = "אמצצעי תשלום זה לא תקין";
            }
            else
            {
                comboBox3.BackColor = Color.Green;
                label15.ForeColor = Color.Green;
                label15.Text = "תקין";
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (!Checks.NameLength(textBox2.Text, 10) || !Checks.NameSofiot(textBox2.Text))
            {
                if (textBox2.Text.Length == 0)
                {
                    textBox2.BackColor = Color.Green;
                    label17.ForeColor = Color.Green;
                    label17.Text = "תקין";
                }
                else
                {
                    textBox2.BackColor = Color.Red;
                    label17.ForeColor = Color.Red;
                    label17.Text = "תיאור לא תקין";
                }
            }
            else
            {
                textBox2.BackColor = Color.Green;
                label17.ForeColor = Color.Green;
                label17.Text = "תקין";
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            dateTimePicker2.MinDate = dateTimePicker1.Value;
            dateTimePicker3.MinDate = dateTimePicker1.Value;
        }
    }
}
