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
    public partial class UpdateSupplier : Form
    {
        public UpdateSupplier()
        {
            InitializeComponent();
            Design.Designer(this);
            comboBox1.Height = 30;
        }
        string ID = "";
        public UpdateSupplier(string id)
        {
            InitializeComponent();
            Design.Designer(this);
            string sql1 = "SELECT distinct Name FROM Cities order by Name";
            DataTable dt1 = MyAdoHelperCsharp.ExecuteDataTable("Database1.mdf", sql1);
            textBox3.AutoCompleteMode = AutoCompleteMode.Suggest;
            textBox3.AutoCompleteSource = AutoCompleteSource.ListItems;
            textBox3.DataSource = dt1;
            textBox3.ValueMember = "Name";
            textBox3.Text = null;

            string sql2 = "SELECT distinct Pre FROM PreTelephone";
            DataTable dt2 = MyAdoHelperCsharp.ExecuteDataTable("Database1.mdf", sql2);
            comboBox2.AutoCompleteSource = AutoCompleteSource.ListItems;
            comboBox2.DataSource = dt2;
            comboBox2.ValueMember = "Pre";
            comboBox2.Text = null;

            string sql3 = "SELECT distinct Pre FROM PrePhone";
            DataTable dt3 = MyAdoHelperCsharp.ExecuteDataTable("Database1.mdf", sql3);
            comboBox3.AutoCompleteSource = AutoCompleteSource.ListItems;
            comboBox3.DataSource = dt3;
            comboBox3.ValueMember = "Pre";
            comboBox3.Text = null;
            ID = id;
            comboBox1.Text = ID;
            comboBox1.Height = 30;
            Sets.SetName(textBox10);
            Sets.SetName(textBox1);
            Sets.SetName(textBox2);
            Sets.SetName(textBox3);
            Sets.SetName(textBox4);
            Sets.SetHouseNumber(textBox5);
            Sets.SetZipCode(textBox6);
            Sets.SetPrePhone(comboBox2);
            Sets.SetPhone(textBox7);
            Sets.SetPrePhone(comboBox3);
            Sets.SetPhone(textBox8);
            Sets.SetEmail(textBox9);
        }
        bool f;
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
            if (label13.Text == "תקין" && label14.Text == "תקין" && label15.Text == "תקין" && label16.Text == "תקין" && label17.Text == "תקין" && label18.Text == "תקין" && label19.Text == "תקין" && label20.Text == "תקין" && label21.Text == "תקין" && label22.Text == "תקין")
            {
                string sql1 = "SELECT * FROM Cities WHERE Name='" + textBox3.Text + "'";
                DataTable dt1 = MyAdoHelperCsharp.ExecuteDataTable("Database1.mdf", sql1);
                string sql2 = "SELECT * FROM PreTelephone WHERE Pre='" + comboBox2.Text + "'";
                DataTable dt2 = MyAdoHelperCsharp.ExecuteDataTable("Database1.mdf", sql2);
                string sql3 = "SELECT * FROM PrePhone WHERE Pre='" + comboBox3.Text + "'";
                DataTable dt3 = MyAdoHelperCsharp.ExecuteDataTable("Database1.mdf", sql3);
                string update = "UPDATE Suppliers SET SupplierName='" + textBox10.Text + "', ContactFName='" + textBox1.Text + "', ContactLName='" + textBox2.Text + "', SupplierCity='" + dt1.Rows[0]["ID"].ToString() + "', SupplierStreet='" + textBox4.Text + "', SupplierHNumber='" + textBox5.Text + "', SupplierZipCode='" + textBox6.Text + "', SupplierPreTelephone='" + dt2.Rows[0]["Number"].ToString() + "', SupplierTelephone='" + textBox7.Text + "', ContactPrePhone='" + dt3.Rows[0]["Number"].ToString() + "', ContactPhone='" + textBox8.Text + "', SupplierEmail='" + textBox9.Text + "', Active='" + checkBox1.Checked + "' WHERE SupplierNumber='" + ID + "';";
                MyAdoHelperCsharp.DoQuery("Database1.mdf", update);
                if (f != checkBox1.Checked)
                {
                    string update2 = "UPDATE Unions SET Active='" + checkBox1.Checked + "' WHERE SupplierNumber='" + comboBox1.Text + "';";
                    MyAdoHelperCsharp.DoQuery("Database1.mdf", update2);
                }
                MessageBox.Show("ספק עודכן בהצלחה");
            }
            else
                MessageBox.Show("עליך לתקן את הטופס");
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            string sql1 = "SELECT * FROM Suppliers WHERE SupplierNumber='" + ID + "'";
            if (MyAdoHelperCsharp.IsExist("Database1.mdf", sql1))
            {
                groupBox1.Visible = true;
                DataTable dt = MyAdoHelperCsharp.ExecuteDataTable("Database1.mdf", sql1);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string sql4 = "SELECT * FROM Cities WHERE ID='" + dt.Rows[i]["SupplierCity"].ToString() + "'";
                    DataTable dt4 = MyAdoHelperCsharp.ExecuteDataTable("Database1.mdf", sql4);
                    string sql5 = "SELECT * FROM PreTelephone WHERE Number='" + dt.Rows[i]["SupplierPreTelephone"].ToString() + "'";
                    DataTable dt5 = MyAdoHelperCsharp.ExecuteDataTable("Database1.mdf", sql5);
                    string sql6 = "SELECT * FROM PrePhone WHERE Number='" + dt.Rows[i]["ContactPrePhone"].ToString() + "'";
                    DataTable dt6 = MyAdoHelperCsharp.ExecuteDataTable("Database1.mdf", sql6);

                    textBox10.Text = dt.Rows[i]["SupplierName"].ToString();
                    textBox1.Text = dt.Rows[i]["ContactFName"].ToString();
                    textBox2.Text = dt.Rows[i]["ContactLName"].ToString();
                    textBox3.Text = dt4.Rows[0]["Name"].ToString();
                    textBox4.Text = dt.Rows[i]["SupplierStreet"].ToString();
                    textBox5.Text = dt.Rows[i]["SupplierHNumber"].ToString();
                    textBox6.Text = dt.Rows[i]["SupplierZipCode"].ToString();
                    comboBox2.Text = dt5.Rows[0]["Pre"].ToString();
                    textBox7.Text = dt.Rows[i]["SupplierTelephone"].ToString();
                    comboBox3.Text = dt6.Rows[0]["Pre"].ToString();
                    textBox8.Text = dt.Rows[i]["ContactPhone"].ToString();
                    textBox9.Text = dt.Rows[i]["SupplierEmail"].ToString();
                    checkBox1.Checked = (bool)dt.Rows[i]["Active"];
                    f = checkBox1.Checked;
                    if (!checkBox1.Checked)
                        checkBox1.Visible = true;
                }
            }
            else
                groupBox1.Visible = false;
        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {
            if (!Checks.NameLength(textBox10.Text, 4) || !Checks.NameSofiot(textBox10.Text))
            {
                textBox10.BackColor = Color.Red;
                label13.ForeColor = Color.Red;
                label13.Text = "שם ספק זה לא תקין";
            }
            else
            {
                textBox10.BackColor = Color.Green;
                label13.ForeColor = Color.Green;
                label13.Text = "תקין";
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (!Checks.NameLength(textBox1.Text, 4) || !Checks.NameSofiot(textBox1.Text))
            {
                textBox1.BackColor = Color.Red;
                label14.ForeColor = Color.Red;
                label14.Text = "שם פרטי זה לא תקין";
            }
            else
            {
                textBox1.BackColor = Color.Green;
                label14.ForeColor = Color.Green;
                label14.Text = "תקין";
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (!Checks.NameLength(textBox2.Text, 4) || !Checks.NameSofiot(textBox2.Text))
            {
                textBox2.BackColor = Color.Red;
                label15.ForeColor = Color.Red;
                label15.Text = "שם משפחה זה לא תקין";
            }
            else
            {
                textBox2.BackColor = Color.Green;
                label15.ForeColor = Color.Green;
                label15.Text = "תקין";
            }
        }

        private void textBox3_TextChanged_1(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox5.Text) && MyAdoHelperCsharp.IsExist("Database1.mdf", "SELECT * FROM Suppliers WHERE SupplierCity='" + Checks.GetCityNum(textBox3.Text) + "' AND SupplierStreet='" + textBox4.Text + "' AND SupplierHNumber !='" + textBox5.Text + "'"))
            {
                DataTable dt = MyAdoHelperCsharp.ExecuteDataTable("Database1.mdf", "SELECT SupplierHnumber, SupplierZipCode FROM Suppliers WHERE SupplierCity='" + Checks.GetCityNum(textBox3.Text) + "' AND SupplierStreet='" + textBox4.Text + "' AND SupplierHNumber !='" + textBox5.Text + "'");
                string adress = dt.Rows[0]["SupplierHnumber"].ToString();
                string adress2 = textBox5.Text;
                int zipcode = int.Parse(dt.Rows[0]["SupplierZipCode"].ToString());
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
                textBox6.Text = zipcode.ToString();
                textBox6.ReadOnly = true;
            }
            else
            {
                if (MyAdoHelperCsharp.IsExist("Database1.mdf", "SELECT * FROM Suppliers WHERE SupplierCity='" + Checks.GetCityNum(textBox3.Text) + "' AND SupplierStreet='" + textBox4.Text + "' AND SupplierHNumber ='" + textBox5.Text + "'"))
                {
                    DataTable dt = MyAdoHelperCsharp.ExecuteDataTable("Database1.mdf", "SELECT SupplierHnumber, SupplierZipCode FROM Suppliers WHERE SupplierCity='" + Checks.GetCityNum(textBox3.Text) + "' AND SupplierStreet='" + textBox4.Text + "' AND SupplierHNumber ='" + textBox5.Text + "'");
                    textBox6.Text = dt.Rows[0]["SupplierZipCode"].ToString();
                    textBox6.ReadOnly = false;
                }
                else
                {
                    textBox6.ReadOnly = false;
                    textBox6.Text = "";
                }
            }
            string sql = "SELECT * FROM Cities WHERE Name='" + textBox3.Text + "'";
            if (!MyAdoHelperCsharp.IsExist("Database1.mdf", sql))
            {
                textBox3.BackColor = Color.Red;
                label16.ForeColor = Color.Red;
                label16.Text = "עיר זו לא תקינה";
            }
            else
            {
                textBox3.BackColor = Color.Green;
                label16.ForeColor = Color.Green;
                label16.Text = "תקין";
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox5.Text) && MyAdoHelperCsharp.IsExist("Database1.mdf", "SELECT * FROM Suppliers WHERE SupplierCity='" + Checks.GetCityNum(textBox3.Text) + "' AND SupplierStreet='" + textBox4.Text + "' AND SupplierHNumber !='" + textBox5.Text + "'"))
            {
                DataTable dt = MyAdoHelperCsharp.ExecuteDataTable("Database1.mdf", "SELECT SupplierHnumber, SupplierZipCode FROM Suppliers WHERE SupplierCity='" + Checks.GetCityNum(textBox3.Text) + "' AND SupplierStreet='" + textBox4.Text + "' AND SupplierHNumber !='" + textBox5.Text + "'");
                string adress = dt.Rows[0]["SupplierHnumber"].ToString();
                string adress2 = textBox5.Text;
                int zipcode = int.Parse(dt.Rows[0]["SupplierZipCode"].ToString());
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
                textBox6.Text = zipcode.ToString();
                textBox6.ReadOnly = true;
            }
            else
            {
                if (MyAdoHelperCsharp.IsExist("Database1.mdf", "SELECT * FROM Suppliers WHERE SupplierCity='" + Checks.GetCityNum(textBox3.Text) + "' AND SupplierStreet='" + textBox4.Text + "' AND SupplierHNumber ='" + textBox5.Text + "'"))
                {
                    DataTable dt = MyAdoHelperCsharp.ExecuteDataTable("Database1.mdf", "SELECT SupplierHnumber, SupplierZipCode FROM Suppliers WHERE SupplierCity='" + Checks.GetCityNum(textBox3.Text) + "' AND SupplierStreet='" + textBox4.Text + "' AND SupplierHNumber ='" + textBox5.Text + "'");
                    textBox6.Text = dt.Rows[0]["SupplierZipCode"].ToString();
                    textBox6.ReadOnly = false;
                }
                else
                {
                    textBox6.ReadOnly = false;
                    textBox6.Text = "";
                }
            }
            if (!Checks.NameLength(textBox4.Text, 4) || !Checks.NameSofiot(textBox4.Text))
            {
                textBox4.BackColor = Color.Red;
                label17.ForeColor = Color.Red;
                label17.Text = "רחוב זה לא תקין";
            }
            else
            {
                textBox4.BackColor = Color.Green;
                label17.ForeColor = Color.Green;
                label17.Text = "תקין";
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox5.Text) && MyAdoHelperCsharp.IsExist("Database1.mdf", "SELECT * FROM Suppliers WHERE SupplierCity='" + Checks.GetCityNum(textBox3.Text) + "' AND SupplierStreet='" + textBox4.Text + "' AND SupplierHNumber !='" + textBox5.Text + "'"))
            {
                DataTable dt = MyAdoHelperCsharp.ExecuteDataTable("Database1.mdf", "SELECT SupplierHnumber, SupplierZipCode FROM Suppliers WHERE SupplierCity='" + Checks.GetCityNum(textBox3.Text) + "' AND SupplierStreet='" + textBox4.Text + "' AND SupplierHNumber !='" + textBox5.Text + "'");
                string adress = dt.Rows[0]["SupplierHnumber"].ToString();
                string adress2 = textBox5.Text;
                int zipcode = int.Parse(dt.Rows[0]["SupplierZipCode"].ToString());
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
                textBox6.Text = zipcode.ToString();
                textBox6.ReadOnly = true;
            }
            else
            {
                if (MyAdoHelperCsharp.IsExist("Database1.mdf", "SELECT * FROM Suppliers WHERE SupplierCity='" + Checks.GetCityNum(textBox3.Text) + "' AND SupplierStreet='" + textBox4.Text + "' AND SupplierHNumber ='" + textBox5.Text + "'"))
                {
                    DataTable dt = MyAdoHelperCsharp.ExecuteDataTable("Database1.mdf", "SELECT SupplierHnumber, SupplierZipCode FROM Suppliers WHERE SupplierCity='" + Checks.GetCityNum(textBox3.Text) + "' AND SupplierStreet='" + textBox4.Text + "' AND SupplierHNumber ='" + textBox5.Text + "'");
                    textBox6.Text = dt.Rows[0]["SupplierZipCode"].ToString();
                    textBox6.ReadOnly = false;
                }
                else
                {
                    textBox6.ReadOnly = false;
                    textBox6.Text = "";
                }
            }
            if (textBox5.Text.Length == 0)
            {
                textBox5.BackColor = Color.Red;
                label18.ForeColor = Color.Red;
                label18.Text = "מספר בית זה לא תקין";
            }
            else
            {
                textBox5.BackColor = Color.Green;
                label18.ForeColor = Color.Green;
                label18.Text = "תקין";
            }
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            if (textBox6.Text.Length != 7)
            {
                textBox6.BackColor = Color.Red;
                label19.ForeColor = Color.Red;
                label19.Text = "מיקוד זה לא תקין";
            }
            else
            {
                textBox6.BackColor = Color.Green;
                label19.ForeColor = Color.Green;
                label19.Text = "תקין";
            }
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            if (textBox7.Text.Length != 7)
            {
                textBox7.BackColor = Color.Red;
                label20.ForeColor = Color.Red;
                label20.Text = "טלפון זה לא תקין";
            }
            else
            {
                textBox7.BackColor = Color.Green;
                label20.ForeColor = Color.Green;
                label20.Text = "תקין";
            }
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            if (textBox8.Text.Length != 7)
            {
                textBox8.BackColor = Color.Red;
                label21.ForeColor = Color.Red;
                label21.Text = "פלאפון זה לא תקין";
            }
            else
            {
                textBox8.BackColor = Color.Green;
                label21.ForeColor = Color.Green;
                label21.Text = "תקין";
            }
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            if (!EmailChecks.CheckEmail(textBox9.Text))
            {
                textBox9.BackColor = Color.Red;
                label22.ForeColor = Color.Red;
                label22.Text = "אימייל זה לא תקין";
            }
            else
            {
                if (!EmailChecks.SendEmail(textBox9.Text, "", ""))
                {
                    textBox9.BackColor = Color.Red;
                    label22.ForeColor = Color.Red;
                    label22.Text = "אימייל זה לא תקין";
                }
                else
                {
                    textBox9.BackColor = Color.Green;
                    label22.ForeColor = Color.Green;
                    label22.Text = "תקין";
                }
            }
        }
    }
}
