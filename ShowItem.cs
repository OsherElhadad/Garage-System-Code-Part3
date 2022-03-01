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
    public partial class ShowItem : Form
    {
        public ShowItem()
        {
            InitializeComponent();
            Design.Designer(this);
            Sets.SetGeneral(comboBox1);
            Sets.SetGeneral(comboBox2);
        }
        string type = "";
        public ShowItem(string a)
        {
            InitializeComponent();
            Design.Designer(this);
            type = a;
            Sets.SetGeneral(comboBox1);
            Sets.SetGeneral(comboBox2);
        }
        Translate tran = new Translate();
        TextBox supnumber;
        public ShowItem(string a, ComboBox itemnum, TextBox supnum)
        {
            InitializeComponent();
            tran.Add("ItemNumber", "מספר פריט");
            tran.Add("ItemName", "שם פריט");
            tran.Add("Amount", "כמות");
            tran.Add("MaxAmount", "כמות מקסימום");
            tran.Add("MinAmount", "כמות מינימום");
            tran.Add("Description", "תיאור");
            tran.Add("CarTypes.Name", "סוג רכב");
            tran.Add("PriceList", "מחירון");
            tran.Add("SupplierNumber", "מספר ספק");
            tran.Add("ItemNumberAtSupplier", "מספר פריט אצל ספק");
            tran.Add("PriceFromSupplier", "מחיר מהספק");
            tran.Add("Unions.Active", "פעיל");
            Design.Designer(this);
            Sets.SetGeneral(comboBox1);
            Sets.SetGeneral(comboBox2);
            type = a;
            supnumber = supnum;
            comboBox1.Text = "מספר פריט";
            comboBox2.Text = itemnum.Text;
            label2.Visible = false;
            label3.Visible = false;
            string sql1 = "SELECT SupplierNumber AS 'מספר ספק', Items.ItemNumber AS 'מספר פריט', ItemNumberAtSupplier AS 'מספר פריט אצל ספק', PriceList AS 'מחירון', PriceFromSupplier AS 'מחיר מהספק', ItemName AS 'שם פריט', Amount AS 'כמות', MaxAmount AS 'כמות מקסימום', MinAmount AS 'כמות מינימום', Description AS 'תיאור', CarTypes.Name AS 'סוג רכב' FROM Items, Unions, CarTypes WHERE Items.ItemNumber = Unions.ItemNumber AND CarTypes.ID=CarType AND Items.";
            sql1 += tran.Find(comboBox1.Text) + "='" + comboBox2.Text + "' AND Unions.Active='True' AND Items.Active='True' AND SupplierNumber!=''";
            if (MyAdoHelperCsharp.IsExist("Database1.mdf", sql1))
            {
                dataGridView1.Visible = true;
                dataGridView1.DataSource = MyAdoHelperCsharp.ExecuteDataTable("Database1.mdf", sql1);
            }
            else
            {
                dataGridView1.Visible = false;
                label4.Visible = true;
            }
            comboBox1.Visible = false;
            comboBox2.Visible = false;
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

        private void comboBox2_TextChanged_1(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(comboBox1.Text) && comboBox1.SelectedIndex >= 0 && comboBox1.Items[comboBox1.SelectedIndex].ToString() == comboBox1.Text)
            {
                string sql1 = "SELECT SupplierNumber AS 'מספר ספק', Items.ItemNumber AS 'מספר פריט', ItemNumberAtSupplier AS 'מספר פריט אצל ספק', PriceList AS 'מחירון', PriceFromSupplier AS 'מחיר מהספק', ItemName AS 'שם פריט', Amount AS 'כמות', MaxAmount AS 'כמות מקסימום', MinAmount AS 'כמות מינימום', Description AS 'תיאור', CarTypes.Name AS 'סוג רכב' FROM Items, Unions, CarTypes WHERE Items.ItemNumber = Unions.ItemNumber AND CarTypes.ID=CarType AND ";
                if (tran.Find(comboBox1.Text) == "ItemNumber")
                    sql1 += "Items.";
                sql1 += tran.Find(comboBox1.Text) + " LIKE'" + comboBox2.Text + "%'";
                if (type == "2")
                    sql1 += " AND Unions.Active='True' AND Items.Active='True'";
                if (MyAdoHelperCsharp.IsExist("Database1.mdf", sql1))
                {
                    dataGridView1.Visible = true;
                    dataGridView1.DataSource = MyAdoHelperCsharp.ExecuteDataTable("Database1.mdf", sql1);
                    if (type != "")
                        label5.Visible = true;
                }
                else
                {
                    dataGridView1.Visible = false;
                    label5.Visible = false;
                }
            }
            else
            {
                dataGridView1.Visible = false;
                label5.Visible = false;
            }
        }

        private void ShowItem_Load(object sender, EventArgs e)
        {
            tran.Add("ItemNumber", "מספר פריט");
            tran.Add("ItemName", "שם פריט");
            tran.Add("Amount", "כמות");
            tran.Add("MaxAmount", "כמות מקסימום");
            tran.Add("MinAmount", "כמות מינימום");
            tran.Add("Description", "תיאור");
            tran.Add("CarTypes.Name", "סוג רכב");
            tran.Add("PriceList", "מחירון");
            tran.Add("SupplierNumber", "מספר ספק");
            tran.Add("ItemNumberAtSupplier", "מספר פריט אצל ספק");
            tran.Add("PriceFromSupplier", "מחיר מהספק");
            tran.Add("Unions.Active", "פעיל");
            string sql1 = "SELECT SupplierNumber AS 'מספר ספק', Items.ItemNumber AS 'מספר פריט', ItemNumberAtSupplier AS 'מספר פריט אצל ספק', PriceList AS 'מחירון', PriceFromSupplier AS 'מחיר מהספק', ItemName AS 'שם פריט', Amount AS 'כמות', MaxAmount AS 'כמות מקסימום', MinAmount AS 'כמות מינימום', Description AS 'תיאור', CarTypes.Name AS 'סוג רכב' FROM Items, Unions, CarTypes WHERE Items.ItemNumber = Unions.ItemNumber AND CarTypes.ID=CarType";
            DataTable dt = MyAdoHelperCsharp.ExecuteDataTable("Database1.mdf", sql1);
            comboBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBox1.AutoCompleteSource = AutoCompleteSource.ListItems;
            int x = dt.Columns.Count;
            for (int i = 0; i < x; i++)
                comboBox1.Items.Add(dt.Columns[i].ToString());
            comboBox1.Text = "";
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string ID = dataGridView1[1, e.RowIndex].Value.ToString();
                if (type.Equals("1"))
                {
                    AddItem f1 = new AddItem("u", ID);
                    FormInPanel.AddForm(Form1.mainp, f1);
                    dataGridView1.Visible = false;
                }
                if (type.Equals("2"))
                {
                    DeleteItem f = new DeleteItem(ID);
                    FormInPanel.AddForm(Form1.mainp, f);
                    dataGridView1.Visible = false;
                }
                if (type.Equals("3"))
                {
                    supnumber.Text = dataGridView1[0, e.RowIndex].Value.ToString();
                    UserLogIn.myforms.Remove(UserLogIn.myforms[UserLogIn.myforms.Count - 1]);
                    this.Close();
                }
            }
            else
                MessageBox.Show("עליך להקליק על אחד מהשורות המלאות");
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sql1 = "SELECT distinct ";
            if (tran.Find(comboBox1.Text) == "ItemNumber")
                sql1 += "Items.";
            sql1 += tran.Find(comboBox1.Text) + " FROM Items, Unions, CarTypes WHERE Items.ItemNumber = Unions.ItemNumber AND CarTypes.ID=CarType";
            if (type == "2")
                sql1 += " AND Unions.Active='True' AND Items.Active='True'";
            sql1 += " order by ";
            if (tran.Find(comboBox1.Text) == "ItemNumber")
                sql1 += "Items.";
            sql1 += tran.Find(comboBox1.Text);
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
