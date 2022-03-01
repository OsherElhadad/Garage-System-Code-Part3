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
    public partial class ShowCustomer : Form
    {
        public ShowCustomer()
        {
            InitializeComponent();
            Design.Designer(this);
            Sets.SetGeneral(comboBox1);
            Sets.SetGeneral(comboBox2);
        }
        string type = "";
        Translate tran = new Translate();
        public ShowCustomer(string a)
        {
            InitializeComponent();
            Design.Designer(this);
            type = a;
            Sets.SetGeneral(comboBox1);
            Sets.SetGeneral(comboBox2);
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

        private void comboBox2_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(comboBox1.Text) && comboBox1.SelectedIndex >= 0 && comboBox1.Items[comboBox1.SelectedIndex].ToString() == comboBox1.Text)
            {
                string sql1 = "SELECT CustomerID AS 'ת.ז לקוח', CustomerFName AS 'שם פרטי', CustomerLName AS 'שם משפחה', Cities.Name AS 'עיר', CustomerStreet AS 'רחוב', CustomerHNumber AS 'מספר בית', CustomerZipCode AS 'מיקוד', PreTelephone.Pre AS 'קידומת טלפון', CustomerTelephone AS 'טלפון', PrePhone.Pre AS 'קידומת פלאפון', CustomerPhone AS 'פלאפון', CustomerBirthDate AS 'תאריך לידה', CustomerEmail AS 'אימייל', Active AS 'פעיל' FROM Customers, Cities, PreTelephone, PrePhone WHERE " + tran.Find(comboBox1.Text);
                DateTime a;
                if (tran.Find(comboBox1.Text) == "CustomerBirthDate" && DateTime.TryParse(comboBox2.Text, out a))
                    sql1 += "='" + Convert.ToDateTime(comboBox2.Text).ToString("MM/dd/yyyy") + "' ";
                else
                    sql1 += " LIKE'" + comboBox2.Text + "%' ";
                sql1 += "AND Cities.ID=Customers.CustomerCity AND PreTelephone.Number=CustomerPreTelephone AND PrePhone.Number=CustomerPrePhone";
                if (type == "2")
                    sql1 += " AND Active='True'";
                if (MyAdoHelperCsharp.IsExist("Database1.mdf", sql1))
                {
                    dataGridView1.Visible = true;
                    dataGridView1.DataSource = MyAdoHelperCsharp.ExecuteDataTable("Database1.mdf", sql1);
                    if (type != "")
                        label4.Visible = true;
                }
                else
                {
                    dataGridView1.Visible = false;
                    label4.Visible = false;
                }
            }
            else
            {
                dataGridView1.Visible = false;
                label4.Visible = false;
            }
        }

        private void ShowCustomer_Load(object sender, EventArgs e)
        {
            tran.Add("CustomerID", "ת.ז לקוח");
            tran.Add("CustomerFName", "שם פרטי");
            tran.Add("CustomerLName", "שם משפחה");
            tran.Add("Name", "עיר");
            tran.Add("CustomerStreet", "רחוב");
            tran.Add("CustomerHNumber", "מספר בית");
            tran.Add("CustomerZipCode", "מיקוד");
            tran.Add("PreTelephone.Pre", "קידומת טלפון");
            tran.Add("CustomerTelephone", "טלפון");
            tran.Add("PrePhone.Pre", "קידומת פלאפון");
            tran.Add("CustomerPhone", "פלאפון");
            tran.Add("CustomerBirthDate", "תאריך לידה");
            tran.Add("CustomerEmail", "אימייל");
            tran.Add("Active", "פעיל");
            string sql1 = "SELECT CustomerID AS 'ת.ז לקוח', CustomerFName AS 'שם פרטי', CustomerLName AS 'שם משפחה', CustomerCity AS 'עיר', CustomerStreet AS 'רחוב', CustomerHNumber AS 'מספר בית', CustomerZipCode AS 'מיקוד', CustomerPreTelephone AS 'קידומת טלפון', CustomerTelephone AS 'טלפון', CustomerPrePhone AS 'קידומת פלאפון', CustomerPhone AS 'פלאפון', CustomerBirthDate AS 'תאריך לידה', CustomerEmail AS 'אימייל', Active AS 'פעיל' FROM Customers";
            Checks.FillColumns(comboBox1, sql1, "");
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string ID = dataGridView1[0, e.RowIndex].Value.ToString();
                if (type.Equals("1"))
                {
                    UpdateCustomer f1 = new UpdateCustomer(ID);
                    FormInPanel.AddForm(Form1.mainp, f1);
                    dataGridView1.Visible = false;
                }
                if (type.Equals("2"))
                {
                    DeleteCustomer f = new DeleteCustomer(ID);
                    FormInPanel.AddForm(Form1.mainp, f);
                    dataGridView1.Visible = false;
                }
            }
            else
                MessageBox.Show("עליך להקליק על אחד מהשורות המלאות");
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sql1 = "SELECT distinct " + tran.Find(comboBox1.Text) + " FROM Customers, Cities, PreTelephone, PrePhone WHERE Cities.ID=Customers.CustomerCity AND PreTelephone.Number=Customers.CustomerPreTelephone AND PrePhone.Number=Customers.CustomerPrePhone";
            if (type == "2")
                sql1 += " AND Active='True'";
            sql1 += " order by " + tran.Find(comboBox1.Text);
            comboBox2.DataSource = new List<string>();
            comboBox2.ValueMember = "";
            comboBox2.Text = "";
            DataTable dt1 = MyAdoHelperCsharp.ExecuteDataTable("Database1.mdf", sql1);
            comboBox2.AutoCompleteMode = AutoCompleteMode.Suggest;
            comboBox2.AutoCompleteSource = AutoCompleteSource.ListItems;
            comboBox2.DataSource = dt1;
            if (tran.Find(comboBox1.Text).Split('.').Length == 1)
                comboBox2.ValueMember = tran.Find(comboBox1.Text);
            else
                comboBox2.ValueMember = tran.Find(comboBox1.Text).Split('.')[1];
            comboBox2.Text = "";
        }
    }
}
