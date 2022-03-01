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
    public partial class UpdateEmployee : Form
    {
        public UpdateEmployee()
        {
            InitializeComponent();
            Design.Designer(this);
            comboBox1.Height = 30;
        }
        string ID = "";
        public UpdateEmployee(string id)
        {
            InitializeComponent();
            Design.Designer(this);
            string sql1 = "SELECT distinct Name FROM Cities order by Name";
            DataTable dt1 = MyAdoHelperCsharp.ExecuteDataTable("Database1.mdf", sql1);
            textBox4.AutoCompleteMode = AutoCompleteMode.Suggest;
            textBox4.AutoCompleteSource = AutoCompleteSource.ListItems;
            textBox4.DataSource = dt1;
            textBox4.ValueMember = "Name";
            textBox4.Text = null;

            string sql2 = "SELECT distinct Role FROM Roles order by Role";
            DataTable dt2 = MyAdoHelperCsharp.ExecuteDataTable("Database1.mdf", sql2);
            textBox1.AutoCompleteMode = AutoCompleteMode.Suggest;
            textBox1.AutoCompleteSource = AutoCompleteSource.ListItems;
            textBox1.DataSource = dt2;
            textBox1.ValueMember = "Role";
            textBox1.Text = null;

            string sql3 = "SELECT distinct Pre FROM PrePhone";
            DataTable dt3 = MyAdoHelperCsharp.ExecuteDataTable("Database1.mdf", sql3);
            comboBox2.AutoCompleteSource = AutoCompleteSource.ListItems;
            comboBox2.DataSource = dt3;
            comboBox2.ValueMember = "Pre";
            comboBox2.Text = null;
            ID = id;
            comboBox1.Text = ID;
            comboBox1.Height = 30;
            Sets.SetName(textBox2);
            Sets.SetName(textBox3);
            Sets.SetName(textBox4);
            Sets.SetName(textBox5);
            Sets.SetHouseNumber(textBox6);
            Sets.SetZipCode(textBox7);
            Sets.SetPrePhone(comboBox2);
            Sets.SetPhone(textBox9);
            Sets.SetBirthDate(dateTimePicker1);
            Sets.SetEmail(textBox10);
            Sets.SetRole(textBox1);
            Sets.SetNote(textBox8);
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

        private void button3_Click(object sender, EventArgs e)
        {
            if (label14.Text == "תקין" && label15.Text == "תקין" && label16.Text == "תקין" && label17.Text == "תקין" && label18.Text == "תקין" && label19.Text == "תקין" && label20.Text == "תקין" && label21.Text == "תקין" && label22.Text == "תקין" && label23.Text == "תקין" && label23.Text == "תקין")
            {
                string sql = "SELECT * FROM Cities WHERE Name='" + textBox4.Text + "'";
                DataTable dt = MyAdoHelperCsharp.ExecuteDataTable("Database1.mdf", sql);
                string sql1 = "SELECT * FROM Roles WHERE Role='" + textBox1.Text + "'";
                DataTable dt1 = MyAdoHelperCsharp.ExecuteDataTable("Database1.mdf", sql1);
                string sql2 = "SELECT * FROM PrePhone WHERE Pre='" + comboBox2.Text + "'";
                DataTable dt2 = MyAdoHelperCsharp.ExecuteDataTable("Database1.mdf", sql2);
                string update = "UPDATE Employees SET EmployeeFName='" + textBox2.Text + "', EmployeeLName='" + textBox3.Text + "', EmployeeCity='" + dt.Rows[0]["ID"].ToString() + "', EmployeeStreet='" + textBox5.Text + "', EmployeeHNumber='" + textBox6.Text + "', EmployeeZipCode='" + textBox7.Text + "', EmployeePrePhone='" + dt2.Rows[0]["Number"].ToString() + "', EmployeePhone='" + textBox9.Text + "', EmployeeBirthDate=N'" + dateTimePicker1.Value.ToString("MM/dd/yyyy") + "', EmployeeEmail='" + textBox10.Text + "', Role='" + dt1.Rows[0]["Number"].ToString() + "', Notes='" + textBox8.Text + "', Active='" + checkBox1.Checked + "' WHERE EmployeeID='" + ID + "';";
                MyAdoHelperCsharp.DoQuery("Database1.mdf", update);
                MessageBox.Show("עובד עודכן בהצלחה");
            }
            else
                MessageBox.Show("עליך לתקן את הטופס");
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            string sql1 = "SELECT * FROM Employees WHERE EmployeeID='" + ID + "'";
            if (MyAdoHelperCsharp.IsExist("Database1.mdf", sql1))
            {
                groupBox1.Visible = true;
                DataTable dt = MyAdoHelperCsharp.ExecuteDataTable("Database1.mdf", sql1);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string sql4 = "SELECT * FROM Cities WHERE ID='" + dt.Rows[i]["EmployeeCity"].ToString() + "'";
                    DataTable dt4 = MyAdoHelperCsharp.ExecuteDataTable("Database1.mdf", sql4);
                    string sql5 = "SELECT * FROM Roles WHERE Number='" + dt.Rows[i]["Role"].ToString() + "'";
                    DataTable dt5 = MyAdoHelperCsharp.ExecuteDataTable("Database1.mdf", sql5);
                    string sql6 = "SELECT * FROM PrePhone WHERE Number='" + dt.Rows[i]["EmployeePrePhone"].ToString() + "'";
                    DataTable dt6 = MyAdoHelperCsharp.ExecuteDataTable("Database1.mdf", sql6);

                    textBox2.Text = dt.Rows[i]["EmployeeFName"].ToString();
                    textBox3.Text = dt.Rows[i]["EmployeeLName"].ToString();
                    textBox4.Text = dt4.Rows[0]["Name"].ToString();
                    textBox5.Text = dt.Rows[i]["EmployeeStreet"].ToString();
                    textBox6.Text = dt.Rows[i]["EmployeeHNumber"].ToString();
                    textBox7.Text = dt.Rows[i]["EmployeeZipCode"].ToString();
                    comboBox2.Text = dt6.Rows[0]["Pre"].ToString();
                    textBox9.Text = dt.Rows[i]["EmployeePhone"].ToString();
                    dateTimePicker1.Text = dt.Rows[i]["EmployeeBirthDate"].ToString();
                    textBox10.Text = dt.Rows[i]["EmployeeEmail"].ToString();
                    textBox1.Text = dt5.Rows[0]["Role"].ToString();
                    textBox8.Text = "f";
                    textBox8.Text = dt.Rows[i]["Notes"].ToString();
                    checkBox1.Checked = (bool)dt.Rows[i]["Active"];
                    if (!checkBox1.Checked)
                        checkBox1.Visible = true;
                }
            }
            else
                groupBox1.Visible = false;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (!Checks.NameLength(textBox2.Text, 4) || !Checks.NameSofiot(textBox2.Text))
            {
                textBox2.BackColor = Color.Red;
                label14.ForeColor = Color.Red;
                label14.Text = "שם פרטי זה לא תקין";
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
            if (!Checks.NameLength(textBox3.Text, 4) || !Checks.NameSofiot(textBox3.Text))
            {
                textBox3.BackColor = Color.Red;
                label15.ForeColor = Color.Red;
                label15.Text = "שם משפחה זה לא תקין";
            }
            else
            {
                textBox3.BackColor = Color.Green;
                label15.ForeColor = Color.Green;
                label15.Text = "תקין";
            }
        }

        private void textBox4_TextChanged_1(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox6.Text) && MyAdoHelperCsharp.IsExist("Database1.mdf", "SELECT * FROM Employees WHERE EmployeeCity='" + Checks.GetCityNum(textBox4.Text) + "' AND EmployeeStreet='" + textBox5.Text + "' AND EmployeeHNumber !='" + textBox6.Text + "'"))
            {
                DataTable dt = MyAdoHelperCsharp.ExecuteDataTable("Database1.mdf", "SELECT EmployeeHnumber, EmployeeZipCode FROM Employees WHERE EmployeeCity='" + Checks.GetCityNum(textBox4.Text) + "' AND EmployeeStreet='" + textBox5.Text + "' AND EmployeeHNumber !='" + textBox6.Text + "'");
                string adress = dt.Rows[0]["EmployeeHnumber"].ToString();
                string adress2 = textBox6.Text;
                int zipcode = int.Parse(dt.Rows[0]["EmployeeZipCode"].ToString());
                if (adress[adress.Length - 1] >= 'א' && adress[adress.Length - 1] <= 'ת')
                {
                    adress = adress.Remove(adress.Length - 1);
                }
                if (!string.IsNullOrEmpty(adress2) && adress2[adress2.Length - 1] >= 'א' && adress2[adress2.Length - 1] <= 'ת')
                {
                    adress2 = adress2.Remove(adress2.Length - 1);
                }
                if (string.IsNullOrEmpty(adress2))
                    zipcode = zipcode + -int.Parse(adress);
                else
                    zipcode = zipcode + int.Parse(adress2) - int.Parse(adress);
                textBox7.Text = zipcode.ToString();
                textBox7.ReadOnly = true;
            }
            else
            {
                if (MyAdoHelperCsharp.IsExist("Database1.mdf", "SELECT * FROM Employees WHERE EmployeeCity='" + Checks.GetCityNum(textBox4.Text) + "' AND EmployeeStreet='" + textBox5.Text + "' AND EmployeeHNumber ='" + textBox6.Text + "'"))
                {
                    DataTable dt = MyAdoHelperCsharp.ExecuteDataTable("Database1.mdf", "SELECT EmployeeHnumber, EmployeeZipCode FROM Employees WHERE EmployeeCity='" + Checks.GetCityNum(textBox4.Text) + "' AND EmployeeStreet='" + textBox5.Text + "' AND EmployeeHNumber ='" + textBox6.Text + "'");
                    textBox7.Text = dt.Rows[0]["EmployeeZipCode"].ToString();
                    textBox7.ReadOnly = false;
                }
                else
                {
                    textBox7.ReadOnly = false;
                    textBox7.Text = "";
                }
            }
            string sql = "SELECT * FROM Cities WHERE Name='" + textBox4.Text + "'";
            if (!MyAdoHelperCsharp.IsExist("Database1.mdf", sql))
            {
                textBox4.BackColor = Color.Red;
                label16.ForeColor = Color.Red;
                label16.Text = "עיר זו לא תקינה";
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
            if (!string.IsNullOrEmpty(textBox6.Text) && MyAdoHelperCsharp.IsExist("Database1.mdf", "SELECT * FROM Employees WHERE EmployeeCity='" + Checks.GetCityNum(textBox4.Text) + "' AND EmployeeStreet='" + textBox5.Text + "' AND EmployeeHNumber !='" + textBox6.Text + "'"))
            {
                DataTable dt = MyAdoHelperCsharp.ExecuteDataTable("Database1.mdf", "SELECT EmployeeHnumber, EmployeeZipCode FROM Employees WHERE EmployeeCity='" + Checks.GetCityNum(textBox4.Text) + "' AND EmployeeStreet='" + textBox5.Text + "' AND EmployeeHNumber !='" + textBox6.Text + "'");
                string adress = dt.Rows[0]["EmployeeHnumber"].ToString();
                string adress2 = textBox6.Text;
                int zipcode = int.Parse(dt.Rows[0]["EmployeeZipCode"].ToString());
                if (adress[adress.Length - 1] >= 'א' && adress[adress.Length - 1] <= 'ת')
                {
                    adress = adress.Remove(adress.Length - 1);
                }
                if (!string.IsNullOrEmpty(adress2) && adress2[adress2.Length - 1] >= 'א' && adress2[adress2.Length - 1] <= 'ת')
                {
                    adress2 = adress2.Remove(adress2.Length - 1);
                }
                if (string.IsNullOrEmpty(adress2))
                    zipcode = zipcode + -int.Parse(adress);
                else
                    zipcode = zipcode + int.Parse(adress2) - int.Parse(adress);
                textBox7.Text = zipcode.ToString();
                textBox7.ReadOnly = true;
            }
            else
            {
                if (MyAdoHelperCsharp.IsExist("Database1.mdf", "SELECT * FROM Employees WHERE EmployeeCity='" + Checks.GetCityNum(textBox4.Text) + "' AND EmployeeStreet='" + textBox5.Text + "' AND EmployeeHNumber ='" + textBox6.Text + "'"))
                {
                    DataTable dt = MyAdoHelperCsharp.ExecuteDataTable("Database1.mdf", "SELECT EmployeeHnumber, EmployeeZipCode FROM Employees WHERE EmployeeCity='" + Checks.GetCityNum(textBox4.Text) + "' AND EmployeeStreet='" + textBox5.Text + "' AND EmployeeHNumber ='" + textBox6.Text + "'");
                    textBox7.Text = dt.Rows[0]["EmployeeZipCode"].ToString();
                    textBox7.ReadOnly = false;
                }
                else
                {
                    textBox7.ReadOnly = false;
                    textBox7.Text = "";
                }
            }
            if (!Checks.NameLength(textBox5.Text, 4) || !Checks.NameSofiot(textBox5.Text))
            {
                textBox5.BackColor = Color.Red;
                label17.ForeColor = Color.Red;
                label17.Text = "רחוב זה לא תקין";
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
            if (!string.IsNullOrEmpty(textBox6.Text) && MyAdoHelperCsharp.IsExist("Database1.mdf", "SELECT * FROM Employees WHERE EmployeeCity='" + Checks.GetCityNum(textBox4.Text) + "' AND EmployeeStreet='" + textBox5.Text + "' AND EmployeeHNumber !='" + textBox6.Text + "'"))
            {
                DataTable dt = MyAdoHelperCsharp.ExecuteDataTable("Database1.mdf", "SELECT EmployeeHnumber, EmployeeZipCode FROM Employees WHERE EmployeeCity='" + Checks.GetCityNum(textBox4.Text) + "' AND EmployeeStreet='" + textBox5.Text + "' AND EmployeeHNumber !='" + textBox6.Text + "'");
                string adress = dt.Rows[0]["EmployeeHnumber"].ToString();
                string adress2 = textBox6.Text;
                int zipcode = int.Parse(dt.Rows[0]["EmployeeZipCode"].ToString());
                if (adress[adress.Length - 1] >= 'א' && adress[adress.Length - 1] <= 'ת')
                {
                    adress = adress.Remove(adress.Length - 1);
                }
                if (!string.IsNullOrEmpty(adress2) && adress2[adress2.Length - 1] >= 'א' && adress2[adress2.Length - 1] <= 'ת')
                {
                    adress2 = adress2.Remove(adress2.Length - 1);
                }
                if (string.IsNullOrEmpty(adress2))
                    zipcode = zipcode + -int.Parse(adress);
                else
                    zipcode = zipcode + int.Parse(adress2) - int.Parse(adress);
                textBox7.Text = zipcode.ToString();
                textBox7.ReadOnly = true;
            }
            else
            {
                if (MyAdoHelperCsharp.IsExist("Database1.mdf", "SELECT * FROM Employees WHERE EmployeeCity='" + Checks.GetCityNum(textBox4.Text) + "' AND EmployeeStreet='" + textBox5.Text + "' AND EmployeeHNumber ='" + textBox6.Text + "'"))
                {
                    DataTable dt = MyAdoHelperCsharp.ExecuteDataTable("Database1.mdf", "SELECT EmployeeHnumber, EmployeeZipCode FROM Employees WHERE EmployeeCity='" + Checks.GetCityNum(textBox4.Text) + "' AND EmployeeStreet='" + textBox5.Text + "' AND EmployeeHNumber ='" + textBox6.Text + "'");
                    textBox7.Text = dt.Rows[0]["EmployeeZipCode"].ToString();
                    textBox7.ReadOnly = false;
                }
                else
                {
                    textBox7.ReadOnly = false;
                    textBox7.Text = "";
                }
            }
            if (textBox6.Text.Length == 0)
            {
                textBox6.BackColor = Color.Red;
                label18.ForeColor = Color.Red;
                label18.Text = "מספר בית זה לא תקין";
            }
            else
            {
                textBox6.BackColor = Color.Green;
                label18.ForeColor = Color.Green;
                label18.Text = "תקין";
            }
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            if (textBox7.Text.Length != 7)
            {
                textBox7.BackColor = Color.Red;
                label19.ForeColor = Color.Red;
                label19.Text = "מיקוד זה לא תקין";
            }
            else
            {
                textBox7.BackColor = Color.Green;
                label19.ForeColor = Color.Green;
                label19.Text = "תקין";
            }
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            if (textBox9.Text.Length != 7)
            {
                textBox9.BackColor = Color.Red;
                label20.ForeColor = Color.Red;
                label20.Text = "פלאפון זה לא תקין";
            }
            else
            {
                textBox9.BackColor = Color.Green;
                label20.ForeColor = Color.Green;
                label20.Text = "תקין";
            }
        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {
            if (!EmailChecks.CheckEmail(textBox10.Text))
            {
                textBox10.BackColor = Color.Red;
                label21.ForeColor = Color.Red;
                label21.Text = "אימייל זה לא תקין";
            }
            else
            {
                if (!EmailChecks.SendEmail(textBox10.Text, "", ""))
                {
                    textBox10.BackColor = Color.Red;
                    label21.ForeColor = Color.Red;
                    label21.Text = "אימייל זה לא תקין";
                }
                else
                {
                    textBox10.BackColor = Color.Green;
                    label21.ForeColor = Color.Green;
                    label21.Text = "תקין";
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string sql = "SELECT * FROM Roles WHERE Role='" + textBox1.Text + "'";
            if (!MyAdoHelperCsharp.IsExist("Database1.mdf", sql))
            {
                textBox1.BackColor = Color.Red;
                label22.ForeColor = Color.Red;
                label22.Text = "תפקיד זה לא תקין";
            }
            else
            {
                textBox1.BackColor = Color.Green;
                label22.ForeColor = Color.Green;
                label22.Text = "תקין";
            }
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            if (!Checks.NameLength(textBox8.Text, 10) || !Checks.NameSofiot(textBox8.Text))
            {
                if (textBox8.Text.Length == 0)
                {
                    textBox8.BackColor = Color.Green;
                    label23.ForeColor = Color.Green;
                    label23.Text = "תקין";
                }
                else
                {
                    textBox8.BackColor = Color.Red;
                    label23.ForeColor = Color.Red;
                    label23.Text = "הערות לא תקינות";
                }
            }
            else
            {
                textBox8.BackColor = Color.Green;
                label23.ForeColor = Color.Green;
                label23.Text = "תקין";
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            if (dateTimePicker1.Value <= DateTime.Today.AddYears(-100))
            {
                DialogResult r = MessageBox.Show("הלקוח בן יותר ממאה שנים. האם אתה בטוח שאתה רוצה להוסיפו?", "האם אתה בטוח?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (r == DialogResult.No)
                    dateTimePicker1.Value = dateTimePicker1.MaxDate;
            }
        }

        private void UpdateEmployee_Load(object sender, EventArgs e)
        {

        }
    }
}
