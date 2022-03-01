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
    public partial class UpdateCar : Form
    {
        public UpdateCar()
        {
            InitializeComponent();
            Design.Designer(this);
            Sets.SetCarModel(dateTimePicker1);
            comboBox1.Height = 30;
            Sets.SetGeneral(textBox2);
        }
        string ID = "";
        string t;
        public UpdateCar(string id)
        {
            InitializeComponent();
            Design.Designer(this);
            Sets.SetCarModel(dateTimePicker1);
            string sql2 = "SELECT distinct Name FROM CarTypes order by Name";
            DataTable dt2 = MyAdoHelperCsharp.ExecuteDataTable("Database1.mdf", sql2);
            textBox2.AutoCompleteMode = AutoCompleteMode.Suggest;
            textBox2.AutoCompleteSource = AutoCompleteSource.ListItems;
            textBox2.DataSource = dt2;
            textBox2.ValueMember = "Name";
            textBox2.Text = "";
            ID = id;
            comboBox1.Text = ID;
            comboBox1.Height = 30;
            Sets.SetID(textBox1);
            Sets.SetGeneral(textBox2);
        }

        public UpdateCar(string id, string type)
        {
            InitializeComponent();
            Design.Designer(this);
            Sets.SetCarModel(dateTimePicker1);
            Sets.SetGeneral(textBox2);
            string sql1;
            if (type == "c")
                sql1 = "SELECT distinct CustomerID FROM Customers order by CustomerID";
            else
                sql1 = "SELECT distinct GarageNumber FROM Garages order by GarageNumber";
            DataTable dt1 = MyAdoHelperCsharp.ExecuteDataTable("Database1.mdf", sql1);
            textBox1.AutoCompleteMode = AutoCompleteMode.Suggest;
            textBox1.AutoCompleteSource = AutoCompleteSource.ListItems;
            textBox1.DataSource = dt1;
            if (type == "c")
                textBox1.ValueMember = "CustomerID";
            else
                textBox1.ValueMember = "GarageNumber";
            textBox1.Text = "";

            string sql2 = "SELECT distinct Name FROM CarTypes order by Name";
            DataTable dt2 = MyAdoHelperCsharp.ExecuteDataTable("Database1.mdf", sql2);
            textBox2.AutoCompleteMode = AutoCompleteMode.Suggest;
            textBox2.AutoCompleteSource = AutoCompleteSource.ListItems;
            textBox2.DataSource = dt2;
            textBox2.ValueMember = "Name";
            textBox2.Text = "";
            ID = id;
            t = type;
            comboBox1.Text = ID;
            comboBox1.Height = 30;
            if (t == "c")
            {
                Sets.SetID(textBox1);
                label3.Text = "ת.ז לקוח";
            }
            else
            {
                Sets.SetNumber(textBox1);
                label3.Text = "מספר מוסך";
            }
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

        private void button2_Click(object sender, EventArgs e)
        {
            if (label6.Text == "תקין" && label7.Text == "תקין")
            {
                string sql = "SELECT * FROM CarTypes WHERE Name='" + textBox2.Text + "'";
                DataTable dt = MyAdoHelperCsharp.ExecuteDataTable("Database1.mdf", sql);
                string update = "UPDATE Cars SET";
                if (t == "c")
                    update += " CustomerID='" + textBox1.Text + "',";
                update += " CarType='" + dt.Rows[0]["ID"].ToString() + "', Year='" + dateTimePicker1.Value.ToString("MM/dd/yyyy") + "', GearType='";
                if (radioButton1.Checked)
                    update += "ידני";
                else
                    update += "אוטומט";
                update += "', Active='" + checkBox1.Checked + "'";
                if (t == "g")
                    update += ", GarageNumber='" + textBox1.Text + "'";
                update += " WHERE CarNumber='" + comboBox1.Text + "';";
                MyAdoHelperCsharp.DoQuery("Database1.mdf", update);
                MessageBox.Show("מכונית עודכנה בהצלחה");
            }
            else
                MessageBox.Show("עליך לתקן את הטופס");
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            string sql1 = "SELECT * FROM Cars WHERE CarNumber='" + ID + "'";
            if (MyAdoHelperCsharp.IsExist("Database1.mdf", sql1))
            {
                groupBox1.Visible = true;
                DataTable dt = MyAdoHelperCsharp.ExecuteDataTable("Database1.mdf", sql1);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string sql4 = "SELECT * FROM CarTypes WHERE ID='" + dt.Rows[i]["CarType"].ToString() + "'";
                    DataTable dt4 = MyAdoHelperCsharp.ExecuteDataTable("Database1.mdf", sql4);

                    if (t == "c")
                        textBox1.Text = dt.Rows[i]["CustomerID"].ToString();
                    else
                        textBox1.Text = dt.Rows[i]["GarageNumber"].ToString();
                    textBox2.Text = dt4.Rows[0]["Name"].ToString();
                    dateTimePicker1.Text = dt.Rows[i]["Year"].ToString();
                    if (dt.Rows[i]["GearType"].ToString() == "ידני")
                        radioButton1.Checked = true;
                    else
                        radioButton2.Checked = true;
                    checkBox1.Checked = (bool)dt.Rows[i]["Active"];
                    if (!checkBox1.Checked)
                        checkBox1.Visible = true;
                }
            }
            else
                groupBox1.Visible = false;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (t == "c")
            {
                string sql = "SELECT * FROM Customers WHERE CustomerID='" + textBox1.Text + "'";
                if (!MyAdoHelperCsharp.IsExist("Database1.mdf", sql))
                {
                    textBox1.BackColor = Color.Red;
                    label6.ForeColor = Color.Red;
                    label6.Text = "ת.ז זו לא קיימת במאגר";
                }
                else
                {
                    textBox1.BackColor = Color.Green;
                    label6.ForeColor = Color.Green;
                    label6.Text = "תקין";
                }
            }
            else
            {
                string sql = "SELECT * FROM Garages WHERE GarageNumber='" + textBox1.Text + "'";
                if (string.IsNullOrEmpty(textBox1.Text))
                {
                    textBox1.BackColor = Color.Red;
                    label6.ForeColor = Color.Red;
                    label6.Text = "מספר מוסך זה לא תקין";
                }
                else
                {
                    if (!MyAdoHelperCsharp.IsExist("Database1.mdf", sql))
                    {
                        textBox1.BackColor = Color.Red;
                        label6.ForeColor = Color.Red;
                        label6.Text = "מספר מוסך זה לא קיים במאגר";
                    }
                    else
                    {
                        textBox1.BackColor = Color.Green;
                        label6.ForeColor = Color.Green;
                        label6.Text = "תקין";
                    }
                }
            }
        }

        private void textBox2_TextChanged_1(object sender, EventArgs e)
        {
            string sql = "SELECT * FROM CarTypes WHERE Name='" + textBox2.Text + "'";
            if (!MyAdoHelperCsharp.IsExist("Database1.mdf", sql))
            {
                textBox2.BackColor = Color.Red;
                label7.ForeColor = Color.Red;
                label7.Text = "סוג זה לא תקין";
            }
            else
            {
                textBox2.BackColor = Color.Green;
                label7.ForeColor = Color.Green;
                label7.Text = "תקין";
            }
        }
    }
}
