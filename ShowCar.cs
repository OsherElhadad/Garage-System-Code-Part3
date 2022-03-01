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
    public partial class ShowCar : Form
    {
        public ShowCar()
        {
            InitializeComponent();
            Design.Designer(this);
            Sets.SetGeneral(comboBox1);
            Sets.SetGeneral(comboBox2);
        }
        Translate tran = new Translate();
        string type = "";
        string t = "";
        string number = "";
        public ShowCar(string a, string type2)
        {
            InitializeComponent();
            Design.Designer(this);
            Sets.SetGeneral(comboBox1);
            Sets.SetGeneral(comboBox2);
            type = a;
            t = type2;
        }
        public ShowCar(string a, string num, string type2)
        {
            InitializeComponent();
            Design.Designer(this);
            Sets.SetGeneral(comboBox1);
            Sets.SetGeneral(comboBox2);
            type = a;
            t = type2;
            number = num;
            comboBox1.Visible = false;
            comboBox2.Visible = false;
            if (type == "dc")
                label4.Text = "עליך למחוק את הרכבים הפעילים של הלקוח על ידי לחיצה על השורה הנבחרת";
            else
                label4.Text = "עליך למחוק את הרכבים הפעילים של המוסך על ידי לחיצה על השורה הנבחרת";
            string sql1 = "SELECT CarNumber AS 'מספר רכב', CarTypes.Name AS 'סוג רכב', Year AS 'שנה', GearType AS 'גיר', ";
            if (t == "c")
                sql1 += "CustomerID AS 'ת.ז לקוח', ";
            else
                sql1 += "GarageNumber AS 'מספר מוסך', ";
            sql1 += "Active AS 'פעיל'";
            sql1 += " FROM Cars, CarTypes WHERE CarTypes.ID=CarType";
            if (type != "")
                sql1 += " AND Active='True'";
            if (t == "c")
                sql1 += " AND CustomerID !='' AND CustomerID='" + number + "'";
            else
                sql1 += " AND GarageNumber !='' AND GarageNumber='" + number + "'";
            if (MyAdoHelperCsharp.IsExist("Database1.mdf", sql1))
            {
                label4.Visible = true;
                dataGridView1.Visible = true;
                dataGridView1.DataSource = MyAdoHelperCsharp.ExecuteDataTable("Database1.mdf", sql1);
                FormInPanel.AddForm(Form1.mainp, this);
            }
            else
            {
                dataGridView1.Visible = false;
                label4.Visible = false;
                ShowSale f;
                if (t == "c")
                {
                    MessageBox.Show("ללקוח אין רכבים");
                    f = new ShowSale("dc", number);
                }
                else
                {
                    MessageBox.Show("למוסך אין רכבים");
                    f = new ShowSale("dg", number);
                }
                this.Close();
            }
        }
        public ShowCar(string type2)
        {
            InitializeComponent();
            Design.Designer(this);
            Sets.SetGeneral(comboBox1);
            Sets.SetGeneral(comboBox2);
            t = type2;
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
                string sql1 = "SELECT CarNumber AS 'מספר רכב', CarTypes.Name AS 'סוג רכב', Year AS 'שנה', GearType AS 'גיר', ";
                if (t == "c")
                    sql1 += "CustomerID AS 'ת.ז לקוח', ";
                else
                    sql1 += "GarageNumber AS 'מספר מוסך', ";
                sql1 += "Active AS 'פעיל'";
                sql1 += " FROM Cars, CarTypes WHERE " + tran.Find(comboBox1.Text);
                DateTime a;
                if (tran.Find(comboBox1.Text) == "Year" && DateTime.TryParse(comboBox2.Text,out a))
                    sql1 += "='"+Convert.ToDateTime(comboBox2.Text).ToString("MM/dd/yyyy") + "' AND CarTypes.ID=CarType";
                else
                    sql1 += " LIKE'"+comboBox2.Text + "%' AND CarTypes.ID=CarType";
                if (type != "")
                    sql1 += " AND Active='True'";
                if (t == "c")
                    sql1 += " AND CustomerID !=''";
                else
                    sql1 += " AND GarageNumber !=''";
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

        private void ShowCar_Load(object sender, EventArgs e)
        {
            tran.Add("CarNumber", "מספר רכב");
            tran.Add("CarTypes.Name", "סוג רכב");
            tran.Add("Year", "שנה");
            tran.Add("GearType", "גיר");
            tran.Add("CustomerID", "ת.ז לקוח");
            tran.Add("GarageNumber", "מספר מוסך");
            tran.Add("Active", "פעיל");
            string sql = "SELECT CarNumber AS 'מספר רכב', CarType AS 'סוג רכב', Year AS 'שנה', GearType AS 'גיר', ";
            if (t == "c")
                sql += "CustomerID AS 'ת.ז לקוח', ";
            else
                sql += "GarageNumber AS 'מספר מוסך', ";
            sql += "Active AS 'פעיל' FROM Cars";
            DataTable dt = MyAdoHelperCsharp.ExecuteDataTable("Database1.mdf", sql);
            comboBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBox1.AutoCompleteSource = AutoCompleteSource.ListItems;
            int x = dt.Columns.Count - 1;
            for (int i = 0; i < x; i++)
            {
                comboBox1.Items.Add(dt.Columns[i].ToString());
            }
            comboBox1.Text = "";
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string ID = dataGridView1[0, e.RowIndex].Value.ToString();
                if (type.Equals("1"))
                {
                    UpdateCar f1 = new UpdateCar(ID, t);
                    FormInPanel.AddForm(Form1.mainp, f1);
                    dataGridView1.Visible = false;
                }
                if (type.Equals("2"))
                {
                    DeleteCar f = new DeleteCar(ID, t);
                    FormInPanel.AddForm(Form1.mainp, f);
                    dataGridView1.Visible = false;
                }
                if (type.Equals("d"))
                {
                    DialogResult r = MessageBox.Show("?האם אתה בטוח שאתה רוצה למחוק", "מחיקה", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (r == DialogResult.Yes)
                    {
                        string delete = "UPDATE Cars SET Active='" + false + "' WHERE CarNumber='" + ID +"'";
                        MyAdoHelperCsharp.DoQuery("Database1.mdf", delete);
                        string sql1 = "SELECT CarNumber AS 'מספר רכב', CarTypes.Name AS 'סוג רכב', Year AS 'שנה', GearType AS 'גיר', ";
                        if (t == "c")
                            sql1 += "CustomerID AS 'ת.ז לקוח', ";
                        else
                            sql1 += "GarageNumber AS 'מספר מוסך', ";
                        sql1 += "Active AS 'פעיל'";
                        sql1 += " FROM Cars, CarTypes WHERE CarTypes.ID=CarType";
                        if (type != "")
                            sql1 += " AND Active='True'";
                        if (t == "c")
                            sql1 += " AND CustomerID !='' AND CustomerID='" + number + "'";
                        else
                            sql1 += " AND GarageNumber !='' AND GarageNumber='" + number + "'";
                        if (MyAdoHelperCsharp.IsExist("Database1.mdf", sql1))
                        {
                            label4.Visible = true;
                            dataGridView1.Visible = true;
                            dataGridView1.DataSource = MyAdoHelperCsharp.ExecuteDataTable("Database1.mdf", sql1);
                        }
                        else
                        {
                            dataGridView1.Visible = false;
                            label4.Visible = false;
                            ShowSale f;
                            if (t == "c")
                            {
                                MessageBox.Show("הרכבים של הלקוח נמחקו");
                                UserLogIn.myforms.Remove(UserLogIn.myforms[UserLogIn.myforms.Count - 1]);
                                f = new ShowSale("dc", number);
                            }
                            else
                            {
                                MessageBox.Show("הרכבים של המוסך נמחקו");
                                UserLogIn.myforms.Remove(UserLogIn.myforms[UserLogIn.myforms.Count - 1]);
                                f = new ShowSale("dg", number);
                            }
                            this.Close();
                        }
                    }
                }
            }
            else
                MessageBox.Show("עליך להקליק על אחד מהשורות המלאות");
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sql1 = "SELECT distinct " + tran.Find(comboBox1.Text) + " FROM Cars, CarTypes WHERE CarTypes.ID=CarType";
            if (type != "")
            {
                sql1 += " AND Active='True'";
                if (t == "c")
                    sql1 += " AND CustomerID !=''";
                else
                    sql1 += " AND GarageNumber !=''";
            }
            else
            {
                if (t == "c")
                    sql1 += " AND CustomerID !=''";
                else
                    sql1 += " AND GarageNumber !=''";
            }
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
