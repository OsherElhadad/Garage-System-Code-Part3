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
    public partial class ShowOrder : Form
    {
        public ShowOrder()
        {
            InitializeComponent();
            Design.Designer(this);
            Sets.SetGeneral(comboBox1);
            Sets.SetGeneral(comboBox2);
        }
        string type = "";
        Translate tran = new Translate();
        public ShowOrder(string a)
        {
            InitializeComponent();
            Design.Designer(this);
            Sets.SetGeneral(comboBox1);
            Sets.SetGeneral(comboBox2);
            type = a;
        }
        string supnum = "";
        public ShowOrder(string a, string num)
        {
            InitializeComponent();
            Design.Designer(this);
            Sets.SetGeneral(comboBox1);
            Sets.SetGeneral(comboBox2);
            type = a;
            supnum = num;
            comboBox1.Visible = false;
            comboBox2.Visible = false;
            label4.Text = "עליך למחוק את ההזמנות הפתוחות של הספק על ידי לחיצה על השורה הנבחרת";
            string sql1 = "SELECT OrderNumber AS 'מספר הזמנה', RowNumber AS 'מספר שורה', StartDate AS 'תאריך התחלה', SupposeEndDate AS 'תאריך משוער לסיום', EndDate AS 'תאריך סיום', ItemNumber AS 'מספר פריט', Amount AS 'כמות', SupplierNumber AS 'מספר ספק', Price AS 'מחיר', Active AS 'בתהליך', Active2 AS 'פעיל' FROM Orders WHERE SupplierNumber='" + supnum + "' AND Active='True' AND Active2='True'";
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
                string delete1 = "UPDATE Suppliers SET Active='" + false + "' WHERE SupplierNumber='" + supnum + "'";
                MyAdoHelperCsharp.DoQuery("Database1.mdf", delete1);
                string delete2 = "UPDATE Unions SET Active='" + false + "' WHERE SupplierNumber='" + supnum + "';";
                MyAdoHelperCsharp.DoQuery("Database1.mdf", delete2);
                MessageBox.Show("לספק לא היו הזמנות פעילות, הספק נמחק בהצלחה");
                this.Close();
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

        private void comboBox2_TextChanged_1(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(comboBox1.Text) && comboBox1.SelectedIndex >= 0 && comboBox1.Items[comboBox1.SelectedIndex].ToString() == comboBox1.Text)
            {
                string sql1 = "SELECT OrderNumber AS 'מספר הזמנה', RowNumber AS 'מספר שורה', StartDate AS 'תאריך התחלה', SupposeEndDate AS 'תאריך משוער לסיום', EndDate AS 'תאריך סיום', ItemNumber AS 'מספר פריט', Amount AS 'כמות', SupplierNumber AS 'מספר ספק', Price AS 'מחיר', Active AS 'בתהליך', Active2 AS 'פעיל' FROM Orders WHERE " + tran.Find(comboBox1.Text);
                DateTime a;
                if ((tran.Find(comboBox1.Text) == "StartDate" || tran.Find(comboBox1.Text) == "SupposeEndDate" || tran.Find(comboBox1.Text) == "EndDate") && DateTime.TryParse(comboBox2.Text, out a))
                    sql1 += "='" + Convert.ToDateTime(comboBox2.Text).ToString("MM/dd/yyyy") + "'";
                else
                    sql1 += " LIKE'" + comboBox2.Text + "%'";
                if (type != "")
                    sql1 += " AND Active='True' AND Active2='True'";
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

        private void ShowOrder_Load(object sender, EventArgs e)
        {
            tran.Add("OrderNumber", "מספר הזמנה");
            tran.Add("RowNumber", "מספר שורה");
            tran.Add("StartDate", "תאריך התחלה");
            tran.Add("SupposeEndDate", "תאריך משוער לסיום");
            tran.Add("EndDate", "תאריך סיום");
            tran.Add("ItemNumber", "מספר פריט");
            tran.Add("Amount", "כמות");
            tran.Add("SupplierNumber", "מספר ספק");
            tran.Add("Price", "מחיר");
            tran.Add("Active", "פעיל");
            string sql1 = "SELECT OrderNumber AS 'מספר הזמנה', RowNumber AS 'מספר שורה', StartDate AS 'תאריך התחלה', SupposeEndDate AS 'תאריך משוער לסיום', EndDate AS 'תאריך סיום', ItemNumber AS 'מספר פריט', Amount AS 'כמות', SupplierNumber AS 'מספר ספק', Price AS 'מחיר', Active AS 'פעיל' FROM Orders";
            Checks.FillColumns(comboBox1, sql1, "");
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string ID = dataGridView1[0, e.RowIndex].Value.ToString();
                if (type.Equals("1"))
                {
                    AddOrder f1 = new AddOrder(ID, dataGridView1[1, e.RowIndex].Value.ToString(), "u");
                    //UpdateOrder f1 = new UpdateOrder(ID, dataGridView1[1, e.RowIndex].Value.ToString());
                    FormInPanel.AddForm(Form1.mainp, f1);
                    dataGridView1.Visible = false;
                }
                if (type.Equals("2"))
                {
                    DeleteOrder f = new DeleteOrder(ID, dataGridView1[1, e.RowIndex].Value.ToString());
                    FormInPanel.AddForm(Form1.mainp, f);
                    dataGridView1.Visible = false;
                }
                if (type.Equals("d"))
                {
                    DialogResult r = MessageBox.Show("?האם אתה בטוח שאתה רוצה למחוק", "מחיקה", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (r == DialogResult.Yes)
                    {
                        string delete = "UPDATE Orders SET Active2='" + false + "' WHERE OrderNumber='" + dataGridView1[0, e.RowIndex].Value.ToString() + "' AND RowNumber='" + dataGridView1[1, e.RowIndex].Value.ToString() + "'";
                        MyAdoHelperCsharp.DoQuery("Database1.mdf", delete);
                        string sql1 = "SELECT OrderNumber AS 'מספר הזמנה', RowNumber AS 'מספר שורה', StartDate AS 'תאריך התחלה', SupposeEndDate AS 'תאריך משוער לסיום', EndDate AS 'תאריך סיום', ItemNumber AS 'מספר פריט', Amount AS 'כמות', SupplierNumber AS 'מספר ספק', Price AS 'מחיר', Active AS 'פעיל' FROM Orders WHERE SupplierNumber='" + supnum + "' AND Active='True' AND Active2='True'";
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
                            string delete1 = "UPDATE Suppliers SET Active='" + false + "' WHERE SupplierNumber='" + supnum + "'";
                            MyAdoHelperCsharp.DoQuery("Database1.mdf", delete1);
                            string delete2 = "UPDATE Unions SET Active='" + false + "' WHERE SupplierNumber='" + supnum + "';";
                            MyAdoHelperCsharp.DoQuery("Database1.mdf", delete2);
                            MessageBox.Show("הספק נמחק בהצלחה");
                            UserLogIn.myforms.Remove(UserLogIn.myforms[UserLogIn.myforms.Count - 1]);
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
            string sql1 = "SELECT distinct " + tran.Find(comboBox1.Text) + " FROM Orders";
            if (type != "")
                sql1 += " WHERE Active='True' AND Active2='True'";
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
