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
    public partial class UpdateAccount : Form
    {
        public UpdateAccount()
        {
            InitializeComponent();
            comboBox1.Height = 21;
        }
        string ID="";
        string r;
        public UpdateAccount(string id, string row)
        {
            InitializeComponent();
            Sets.SetNumber(textBox1);
            //Sets.SetPayment(textBox2);
            Sets.SetNumber(textBox3);
            Sets.SetNumber(textBox4);
            Sets.SetPrice(textBox5);
            Sets.SetNumber(textBox6);
            Sets.SetCarNumber(textBox7);
            Sets.SetPrice(textBox8);
            Sets.SetDate(dateTimePicker1);
            Sets.SetNote(textBox9);
            comboBox2.Text = row;
            ID = id;
            comboBox1.Text = ID;
            comboBox1.Height = 21;
            r = row;
        }
        bool flag = true;
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (label13.Text == "תקין" && label14.Text == "תקין" && label15.Text == "תקין" && label16.Text == "תקין" && label17.Text == "תקין" && label18.Text == "תקין" && label19.Text == "תקין" && label20.Text == "תקין" && label21.Text == "תקין" && MyAdoHelperCsharp.IsExist("Database1.mdf", "SELECT * FROM Accounts WHERE AccountNumber='" + comboBox1.Text + "' AND RowNumber='" + comboBox2.Text + "';"))
            {
                string update = "UPDATE Accounts SET SaleNumber='" + textBox1.Text + "', PaymentMethod='" + textBox2.Text + "', ItemNumber='" + textBox3.Text + "', Amount='" + textBox4.Text + "', TotalPrice='" + textBox5.Text;
                if (flag)
                    update += "', GarageNumber='" + textBox6.Text;
                else
                    update += "', CustomerID='" + textBox6.Text;
                update += "', CarNumber='" + textBox7.Text + "', Discount='" + textBox8.Text + "', Date=N'" + dateTimePicker1.Value + "', Description='" + textBox9.Text + "', Active='" + checkBox1.Checked + "' WHERE AccountNumber='" + comboBox1.Text + "' AND RowNumber='" + comboBox2.Text + "';";
                MyAdoHelperCsharp.DoQuery("Database1.mdf", update);
                MessageBox.Show("חשבון עודכן בהצלחה");
            }
            else
                MessageBox.Show("עליך לתקן את הטופס");
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            string sql1 = "SELECT * FROM Accounts WHERE AccountNumber='" + ID + "' AND RowNumber='" + comboBox2.Text + "'";
            if (MyAdoHelperCsharp.IsExist("Database1.mdf", sql1))
            {
                groupBox1.Visible = true;
                DataTable dt = MyAdoHelperCsharp.ExecuteDataTable("Database1.mdf", sql1);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    comboBox2.Text = dt.Rows[i]["RowNumber"].ToString();
                    textBox1.Text = dt.Rows[i]["SaleNumber"].ToString();
                    textBox2.Text = dt.Rows[i]["PaymentMethod"].ToString();
                    textBox3.Text = dt.Rows[i]["ItemNumber"].ToString();
                    textBox4.Text = dt.Rows[i]["Amount"].ToString();
                    textBox5.Text = dt.Rows[i]["TotalPrice"].ToString();
                    if (dt.Rows[i]["GarageNumber"].ToString() != "")
                    {
                        Sets.SetNumber(textBox6);
                        flag = true;
                        label8.Text = "מספר מוסך";
                        textBox6.Text = dt.Rows[i]["GarageNumber"].ToString();
                    }
                    else
                    {
                        Sets.SetID(textBox6);
                        flag = false;
                        label8.Text = "ת.ז לקוח";
                        textBox6.Text = dt.Rows[i]["CustomerID"].ToString();
                    }
                    textBox7.Text = dt.Rows[i]["CarNumber"].ToString();
                    textBox9.Text = "f";
                    textBox8.Text = dt.Rows[i]["Discount"].ToString();
                    dateTimePicker1.Text = dt.Rows[i]["Date"].ToString();
                    textBox9.Text = dt.Rows[i]["Description"].ToString();
                    checkBox1.Checked = (bool)dt.Rows[i]["Active"];
                }
            }
            else
                groupBox1.Visible = false;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string sql = "SELECT * FROM Sales WHERE SaleNumber='" + textBox1.Text + "'";
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                textBox1.BackColor = Color.Red;
                label13.ForeColor = Color.Red;
                label13.Text = "מספר מכירה זה לא תקין";
            }
            else
            {
                if (!MyAdoHelperCsharp.IsExist("Database1.mdf", sql))
                {
                    textBox1.BackColor = Color.Red;
                    label13.ForeColor = Color.Red;
                    label13.Text = "מספר מכירה זה לא קיים במאגר";
                }
                else
                {
                    textBox1.BackColor = Color.Green;
                    label13.ForeColor = Color.Green;
                    label13.Text = "תקין";
                }
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox2.Text) || int.Parse(textBox2.Text) == 0)
            {
                textBox2.BackColor = Color.Red;
                label14.ForeColor = Color.Red;
                label14.Text = "אמצעי תשלום זה לא תקין";
            }
            else
            {
                textBox2.BackColor = Color.Green;
                label14.ForeColor = Color.Green;
                label14.Text = "תקין";
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            string sql = "SELECT * FROM Items WHERE ItemNumber='" + textBox3.Text + "'";
            if (string.IsNullOrEmpty(textBox3.Text))
            {
                textBox3.BackColor = Color.Red;
                label15.ForeColor = Color.Red;
                label15.Text = "מספר פריט זה לא תקין";
            }
            else
            {
                if (!MyAdoHelperCsharp.IsExist("Database1.mdf", sql))
                {
                    textBox3.BackColor = Color.Red;
                    label15.ForeColor = Color.Red;
                    label15.Text = "מספר פריט זה לא קיים במאגר";
                }
                else
                {
                    textBox3.BackColor = Color.Green;
                    label15.ForeColor = Color.Green;
                    label15.Text = "תקין";
                }
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox4.Text))
            {
                textBox4.BackColor = Color.Red;
                label16.ForeColor = Color.Red;
                label16.Text = "כמות זו לא תקינה";
            }
            else
            {
                textBox4.BackColor = Color.Green;
                label16.ForeColor = Color.Green;
                label16.Text = "תקין";
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox5.Text))
            {
                textBox5.BackColor = Color.Red;
                label17.ForeColor = Color.Red;
                label17.Text = "מחיר זה לא תקין";
            }
            else
            {
                textBox5.BackColor = Color.Green;
                label17.ForeColor = Color.Green;
                label17.Text = "תקין";
            }
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            if (flag)
            {
                string sql = "SELECT * FROM Garages WHERE GarageNumber='" + textBox6.Text + "'";
                if (string.IsNullOrEmpty(textBox6.Text))
                {
                    textBox6.BackColor = Color.Red;
                    label18.ForeColor = Color.Red;
                    label18.Text = "מספר מוסך זה לא תקין";
                }
                else
                {
                    if (!MyAdoHelperCsharp.IsExist("Database1.mdf", sql))
                    {
                        textBox6.BackColor = Color.Red;
                        label18.ForeColor = Color.Red;
                        label18.Text = "מספר מוסך זה לא קיים במאגר";
                    }
                    else
                    {
                        textBox6.BackColor = Color.Green;
                        label18.ForeColor = Color.Green;
                        label18.Text = "תקין";
                    }
                }
            }
            else
            {
                string sql = "SELECT * FROM Customers WHERE CustomerID='" + textBox6.Text + "'";
                if (!MyAdoHelperCsharp.IsExist("Database1.mdf", sql))
                {
                    textBox6.BackColor = Color.Red;
                    label18.ForeColor = Color.Red;
                    label18.Text = "ת.ז זו לא קיימת במאגר";
                }
                else
                {
                    textBox6.BackColor = Color.Green;
                    label18.ForeColor = Color.Green;
                    label18.Text = "תקין";
                }
            }
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            string sql = "SELECT * FROM Cars WHERE CarNumber='" + textBox7.Text + "'";
            if (string.IsNullOrEmpty(textBox7.Text))
            {
                textBox7.BackColor = Color.Red;
                label19.ForeColor = Color.Red;
                label19.Text = "מספר רכב זה לא תקין";
            }
            else
            {
                if (!MyAdoHelperCsharp.IsExist("Database1.mdf", sql))
                {
                    textBox7.BackColor = Color.Red;
                    label19.ForeColor = Color.Red;
                    label19.Text = "מספר רכב זה לא קיים במאגר";
                }
                else
                {
                    textBox7.BackColor = Color.Green;
                    label19.ForeColor = Color.Green;
                    label19.Text = "תקין";
                }
            }
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox8.Text))
            {
                textBox8.BackColor = Color.Red;
                label20.ForeColor = Color.Red;
                label20.Text = "מחיר זה לא תקין";
            }
            else
            {
                textBox8.BackColor = Color.Green;
                label20.ForeColor = Color.Green;
                label20.Text = "תקין";
            }
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            if (!Checks.NameLength(textBox9.Text, 10) || !Checks.NameSofiot(textBox9.Text))
            {
                if (textBox9.Text.Length == 0)
                {
                    textBox9.BackColor = Color.Green;
                    label21.ForeColor = Color.Green;
                    label21.Text = "תקין";
                }
                else
                {
                    textBox9.BackColor = Color.Red;
                    label21.ForeColor = Color.Red;
                    label21.Text = "תיאור לא תקין";
                }
            }
            else
            {
                textBox9.BackColor = Color.Green;
                label21.ForeColor = Color.Green;
                label21.Text = "תקין";
            }
        }

        private void UpdateAccount_Load(object sender, EventArgs e)
        {
            string sql1 = "SELECT distinct RowNumber FROM Accounts WHERE AccountNumber='" + ID + "' order by RowNumber";
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
            string sql1 = "SELECT * FROM Accounts WHERE AccountNumber='" + ID + "' AND RowNumber='" + comboBox2.Text + "'";
            if (MyAdoHelperCsharp.IsExist("Database1.mdf", sql1))
            {
                groupBox1.Visible = true;
                DataTable dt = MyAdoHelperCsharp.ExecuteDataTable("Database1.mdf", sql1);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    textBox1.Text = dt.Rows[i]["SaleNumber"].ToString();
                    textBox2.Text = dt.Rows[i]["PaymentMethod"].ToString();
                    textBox3.Text = dt.Rows[i]["ItemNumber"].ToString();
                    textBox4.Text = dt.Rows[i]["Amount"].ToString();
                    textBox5.Text = dt.Rows[i]["TotalPrice"].ToString();
                    if (dt.Rows[i]["GarageNumber"].ToString() != "")
                    {
                        Sets.SetNumber(textBox6);
                        flag = true;
                        label8.Text = "מספר מוסך";
                        textBox6.Text = dt.Rows[i]["GarageNumber"].ToString();
                    }
                    else
                    {
                        Sets.SetID(textBox6);
                        flag = false;
                        label8.Text = "ת.ז לקוח";
                        textBox6.Text = dt.Rows[i]["CustomerID"].ToString();
                    }
                    textBox7.Text = dt.Rows[i]["CarNumber"].ToString();
                    textBox9.Text = "f";
                    textBox8.Text = dt.Rows[i]["Discount"].ToString();
                    dateTimePicker1.Text = dt.Rows[i]["Date"].ToString();
                    textBox9.Text = dt.Rows[i]["Description"].ToString();
                    checkBox1.Checked = (bool)dt.Rows[i]["Active"];
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AddAccount f = new AddAccount(comboBox1.Text, comboBox2.Text, "u");
            f.Show();
            this.Close();
        }
    }
}
