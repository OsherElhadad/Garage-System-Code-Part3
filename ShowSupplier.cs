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
    public partial class ShowSupplier : Form
    {
        public ShowSupplier()
        {
            InitializeComponent();
            Design.Designer(this);
            Sets.SetGeneral(comboBox1);
            Sets.SetGeneral(comboBox2);
        }
        string type = "";
        Translate tran = new Translate();
        public ShowSupplier(string a)
        {
            InitializeComponent();
            Design.Designer(this);
            type = a;
            Sets.SetGeneral(comboBox1);
            Sets.SetGeneral(comboBox2);
        }

        DataGridView dataGridView2;
        public ShowSupplier(string a, DataGridView d)
        {
            InitializeComponent();
            Design.Designer(this);
            Sets.SetGeneral(comboBox1);
            Sets.SetGeneral(comboBox2);
            dataGridView2 = d;
            type = a;
            Sets.SetNumber(textBox1);
            Sets.SetPrice(textBox2);
        }
        ComboBox sup1up;
        TextBox sup2up;
        TextBox sup3up;
        ComboBox sup4up;
        string item;
        string sup;
        public ShowSupplier(string a, ComboBox c1, TextBox t2, TextBox t3, ComboBox c4)
        {
            InitializeComponent();
            Design.Designer(this);
            Sets.SetGeneral(comboBox1);
            Sets.SetGeneral(comboBox2);
            sup1up = c1;
            sup2up = t2;
            sup3up = t3;
            sup4up = c4;
            item = c4.Text;
            sup = c1.Text;
            type = a;
            Sets.SetNumber(textBox1);
            Sets.SetPrice(textBox2);
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
                string sql1 = "SELECT SupplierNumber AS 'מספר ספק', SupplierName AS 'שם ספק', ContactFName AS 'שם פרטי', ContactLName AS 'שם משפחה', Cities.Name AS 'עיר', SupplierStreet AS 'רחוב', SupplierHNumber AS 'מספר בית', SupplierZipCode AS 'מיקוד', PreTelephone.Pre AS 'קידומת טלפון', SupplierTelephone AS 'טלפון', PrePhone.Pre AS 'קידומת פלאפון', ContactPhone AS 'פלאפון', SupplierEmail AS 'אימייל', Active AS 'פעיל' FROM Suppliers, Cities, PreTelephone, PrePhone WHERE " + tran.Find(comboBox1.Text) + " LIKE'" + comboBox2.Text + "%' AND Cities.ID=Suppliers.SupplierCity AND PreTelephone.Number=SupplierPreTelephone AND PrePhone.Number=ContactPrePhone";
                if (type == "2" || type=="3")
                    sql1 += " AND Active='True'";
                if (MyAdoHelperCsharp.IsExist("Database1.mdf", sql1))
                {
                    dataGridView1.Visible = true;
                    dataGridView1.DataSource = MyAdoHelperCsharp.ExecuteDataTable("Database1.mdf", sql1);
                    if (type != "")
                        label8.Visible = true;
                }
                else
                {
                    dataGridView1.Visible = false;
                    label8.Visible = false;
                }
            }
            else
            {
                dataGridView1.Visible = false;
                label8.Visible = false;
            }
        }

        private void ShowSupplier_Load(object sender, EventArgs e)
        {
            tran.Add("SupplierNumber", "מספר ספק");
            tran.Add("SupplierName", "שם ספק");
            tran.Add("ContactFName", "שם פרטי");
            tran.Add("ContactLName", "שם משפחה");
            tran.Add("Name", "עיר");
            tran.Add("SupplierStreet", "רחוב");
            tran.Add("SupplierHNumber", "מספר בית");
            tran.Add("SupplierZipCode", "מיקוד");
            tran.Add("PreTelephone.Pre", "קידומת טלפון");
            tran.Add("SupplierTelephone", "טלפון");
            tran.Add("PrePhone.Pre", "קידומת פלאפון");
            tran.Add("ContactPhone", "פלאפון");
            tran.Add("SupplierEmail", "אימייל");
            tran.Add("Active", "פעיל");
            string sql1 = "SELECT SupplierNumber AS 'מספר ספק', SupplierName AS 'שם ספק', ContactFName AS 'שם פרטי', ContactLName AS 'שם משפחה', SupplierCity AS 'עיר', SupplierStreet AS 'רחוב', SupplierHNumber AS 'מספר בית', SupplierZipCode AS 'מיקוד', SupplierPreTelephone AS 'קידומת טלפון', SupplierTelephone AS 'טלפון', ContactPrePhone AS 'קידומת פלאפון', ContactPhone AS 'פלאפון', SupplierEmail AS 'אימייל', Active AS 'פעיל' FROM Suppliers";
            Checks.FillColumns(comboBox1, sql1, "");
        }
        int a;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string ID = dataGridView1[0, e.RowIndex].Value.ToString();
                if (type.Equals("1"))
                {
                    UpdateSupplier f1 = new UpdateSupplier(ID);
                    FormInPanel.AddForm(Form1.mainp, f1);
                    dataGridView1.Visible = false;
                }
                if (type.Equals("2"))
                {
                    DeleteSupplier f = new DeleteSupplier(ID);
                    FormInPanel.AddForm(Form1.mainp, f);
                    dataGridView1.Visible = false;
                }
                if (type.Equals("3"))
                {
                    bool f = false;
                    for (int i = 0; i < dataGridView2.Rows.Count && !f; i++)
                        if (dataGridView2[0, i].Value.ToString() == ID)
                            f = true;
                    if (!f && !string.IsNullOrEmpty(dataGridView1[0, e.RowIndex].Value.ToString()))
                    {
                        a = e.RowIndex;
                        label4.Visible = true;
                        textBox1.Visible = true;
                        label6.Visible = true;
                        textBox2.Visible = true;
                        button2.Visible = true;
                        textBox1.Text = "";
                        textBox2.Text = "";
                        label5.Text = "";
                        label7.Text = "";
                    }
                    else
                    {
                        label4.Visible = false;
                        textBox1.Visible = false;
                        label6.Visible = false;
                        textBox2.Visible = false;
                        button2.Visible = false;
                        textBox1.Text = "";
                        textBox2.Text = "";
                        label5.Text = "";
                        label7.Text = "";
                    }
                }
                if (type.Equals("4"))
                {
                    if (!sup.Equals(dataGridView1[0, e.RowIndex].Value.ToString()) && !string.IsNullOrEmpty(dataGridView1[0, e.RowIndex].Value.ToString()))
                    {
                        a = e.RowIndex;
                        label4.Visible = true;
                        textBox1.Visible = true;
                        label6.Visible = true;
                        textBox2.Visible = true;
                        button2.Visible = true;
                        textBox1.Text = "";
                        textBox2.Text = "";
                        label5.Text = "";
                        label7.Text = "";
                    }
                    else
                    {
                        label4.Visible = false;
                        textBox1.Visible = false;
                        label6.Visible = false;
                        textBox2.Visible = false;
                        button2.Visible = false;
                        textBox1.Text = "";
                        textBox2.Text = "";
                        label5.Text = "";
                        label7.Text = "";
                    }
                }
            }
            else
                MessageBox.Show("עליך להקליק על אחד מהשורות המלאות");
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sql1 = "SELECT distinct " + tran.Find(comboBox1.Text) + " FROM Suppliers, Cities, PreTelephone, PrePhone WHERE Cities.ID=Suppliers.SupplierCity AND PreTelephone.Number=SupplierPreTelephone AND PrePhone.Number=ContactPrePhone";
            if (type == "2" || type == "3")
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                textBox1.BackColor = Color.Red;
                label5.ForeColor = Color.Red;
                label5.Text = "מספר קטלוגי זה לא תקין";
            }
            else
            {
                string sql = "SELECT * FROM Unions WHERE SupplierNumber='" + dataGridView1[0, a].Value.ToString() + "' AND ItemNumberAtSupplier='" + textBox1.Text + "'";
                if (MyAdoHelperCsharp.IsExist("Database1.mdf", sql))
                {
                    textBox1.BackColor = Color.Red;
                    label5.ForeColor = Color.Red;
                    label5.Text = "מספר קטלוגי של ספק זה קיים";
                }
                else
                {
                    textBox1.BackColor = Color.Green;
                    label5.ForeColor = Color.Green;
                    label5.Text = "תקין";
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (label5.Text == "תקין" && label7.Text == "תקין")
            {
                if (type == "3")
                {
                    dataGridView2.Rows.Insert(dataGridView2.Rows.Count, dataGridView1[0, a].Value.ToString(), dataGridView1[1, a].Value.ToString(), textBox1.Text, textBox2.Text, "מחק");
                    UserLogIn.myforms.Remove(UserLogIn.myforms[UserLogIn.myforms.Count - 1]);
                    this.Close();
                }
                else
                {
                    string insert = "INSERT INTO Unions VALUES ('" + dataGridView1[0, a].Value.ToString() + "','" + item + "','" + textBox1.Text + "','" + textBox2.Text + "','" + true + "');";
                    MyAdoHelperCsharp.DoQuery("Database1.mdf", insert);
                    MessageBox.Show("ספק נוסף");
                    UserLogIn.myforms.Remove(UserLogIn.myforms[UserLogIn.myforms.Count - 1]);
                    UpdateItem f = new UpdateItem(item, dataGridView1[0, a].Value.ToString());
                    f.Show();
                    this.Close();
                }
            }
            else
                MessageBox.Show("הטופס לא תקין");
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox2.Text))
            {
                textBox2.BackColor = Color.Red;
                label7.ForeColor = Color.Red;
                label7.Text = "מחיר זה לא תקין";
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
