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
    public partial class SupplierByCityQuery : Form
    {
        public SupplierByCityQuery()
        {
            InitializeComponent();
            Design.Designer(this);
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

        private void SupplierByCityQuery_Load(object sender, EventArgs e)
        {
            string sql2 = "SELECT distinct Cities.Name FROM Suppliers, Cities, PreTelephone, PrePhone WHERE Cities.ID=Suppliers.SupplierCity AND PreTelephone.Number=SupplierPreTelephone AND PrePhone.Number=ContactPrePhone AND Active='True' order by Cities.Name";
            DataTable dt2 = MyAdoHelperCsharp.ExecuteDataTable("Database1.mdf", sql2);
            comboBox2.AutoCompleteMode = AutoCompleteMode.Suggest;
            comboBox2.AutoCompleteSource = AutoCompleteSource.ListItems;
            comboBox2.DataSource = dt2;
            comboBox2.ValueMember = "Name";
            comboBox2.Text = "";

            string sql1 = "SELECT distinct SupplierNumber FROM Suppliers, Cities, PreTelephone, PrePhone WHERE Cities.ID=Suppliers.SupplierCity AND PreTelephone.Number=SupplierPreTelephone AND PrePhone.Number=ContactPrePhone AND Active='True' order by SupplierNumber";
            DataTable dt1 = MyAdoHelperCsharp.ExecuteDataTable("Database1.mdf", sql1);
            comboBox1.AutoCompleteMode = AutoCompleteMode.Suggest;
            comboBox1.AutoCompleteSource = AutoCompleteSource.ListItems;
            comboBox1.DataSource = dt1;
            comboBox1.ValueMember = "SupplierNumber";
            comboBox1.Text = "";
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(comboBox1.Text) || !string.IsNullOrEmpty(comboBox2.Text))
            {
                string sql1 = "SELECT SupplierNumber AS 'מספר ספק', SupplierName AS 'שם ספק', ContactFName AS 'שם פרטי', ContactLName AS 'שם משפחה', Cities.Name AS 'עיר', SupplierStreet AS 'רחוב', SupplierHNumber AS 'מספר בית', SupplierZipCode AS 'מיקוד', PreTelephone.Pre AS 'קידומת טלפון', SupplierTelephone AS 'טלפון', PrePhone.Pre AS 'קידומת פלאפון', ContactPhone AS 'פלאפון', SupplierEmail AS 'אימייל', Active AS 'פעיל' FROM Suppliers, Cities, PreTelephone, PrePhone WHERE SupplierNumber LIKE'" + comboBox1.Text + "%' AND Cities.Name LIKE'" + comboBox2.Text + "%' AND Cities.ID=Suppliers.SupplierCity AND PreTelephone.Number=SupplierPreTelephone AND PrePhone.Number=ContactPrePhone AND Active='True' order by SupplierNumber";
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

        private void comboBox2_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(comboBox1.Text) || !string.IsNullOrEmpty(comboBox2.Text))
            {
                string sql1 = "SELECT SupplierNumber AS 'מספר ספק', SupplierName AS 'שם ספק', ContactFName AS 'שם פרטי', ContactLName AS 'שם משפחה', Cities.Name AS 'עיר', SupplierStreet AS 'רחוב', SupplierHNumber AS 'מספר בית', SupplierZipCode AS 'מיקוד', PreTelephone.Pre AS 'קידומת טלפון', SupplierTelephone AS 'טלפון', PrePhone.Pre AS 'קידומת פלאפון', ContactPhone AS 'פלאפון', SupplierEmail AS 'אימייל', Active AS 'פעיל' FROM Suppliers, Cities, PreTelephone, PrePhone WHERE SupplierNumber LIKE'" + comboBox1.Text + "%' AND Cities.Name LIKE'" + comboBox2.Text + "%' AND Cities.ID=Suppliers.SupplierCity AND PreTelephone.Number=SupplierPreTelephone AND PrePhone.Number=ContactPrePhone AND Active='True' order by SupplierNumber";
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
    }
}
