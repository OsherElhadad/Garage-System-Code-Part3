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
    public partial class ShowSale : Form
    {
        public ShowSale()
        {
            InitializeComponent();
            Design.Designer(this);
            Sets.SetGeneral(comboBox1);
            Sets.SetGeneral(comboBox2);
        }
        string type = "";
        Translate tran = new Translate();
        public ShowSale(string a)
        {
            InitializeComponent();
            Design.Designer(this);
            Sets.SetGeneral(comboBox1);
            Sets.SetGeneral(comboBox2);
            type = a;
        }
        string number = "";
        public ShowSale(string a, string num)
        {
            InitializeComponent();
            Design.Designer(this);
            Sets.SetGeneral(comboBox1);
            Sets.SetGeneral(comboBox2);
            type = a;
            number = num;
            comboBox1.Visible = false;
            comboBox2.Visible = false;
            if (type == "dc")
                label4.Text = "עליך למחוק את המכירות הפתוחות של הלקוח על ידי לחיצה על השורה הנבחרת";
            else
                label4.Text = "עליך למחוק את המכירות הפתוחות של המוסך על ידי לחיצה על השורה הנבחרת";
            string sql1 = "SELECT SaleNumber AS 'מספר מכירה', RowNumber AS 'מספר שורה', ItemNumber AS 'מספר פריט', Amount AS 'כמות', Price AS 'מחיר', Sales.CarNumber AS 'מספר רכב', StartDate AS 'תאריך התחלה', SupposeEndDate AS 'תאריך משוער לסיום', EndDate AS 'תאריך סיום', PaymentMethods.Method AS 'אמצעי תשלום', Description AS 'תיאור',";
            if (type == "dc")
                sql1 += " CustomerID AS 'ת.ז לקוח',";
            else
                sql1 += " GarageNumber AS 'מספר מוסך',";
            sql1 +=" Sales.Active AS 'בתהליך', Active2 AS 'פעיל' FROM Sales, PaymentMethods, Cars WHERE ";
            if (type == "dc")
                sql1 += "CustomerID !='' AND CustomerID='" + number + "'";
            else
                sql1 += "GarageNumber !='' AND GarageNumber='" + number + "'";
            sql1 += " AND PaymentMethods.Number=PaymentMethod AND Sales.CarNumber=Cars.CarNumber";
            if (type != "")
                sql1 += " AND Sales.Active='True' AND Active2='True'";
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
                if (type == "dc")
                {
                    string delete = "UPDATE Customers SET Active='" + false + "' WHERE CustomerID='" + number + "'";
                    MyAdoHelperCsharp.DoQuery("Database1.mdf", delete);
                    MessageBox.Show("ללקוח לא היו מכירות פעילות, הלקוח נמחק בהצלחה");
                    this.Close();
                }
                else
                {
                    string delete = "UPDATE Garages SET Active='" + false + "' WHERE GarageNumber='" + number + "'";
                    MyAdoHelperCsharp.DoQuery("Database1.mdf", delete);
                    MessageBox.Show("למוסך לא היו מכירות פעילות, המוסך נמחק בהצלחה");
                    this.Close();
                }
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

        private void comboBox2_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(comboBox1.Text) && comboBox1.SelectedIndex >= 0 && comboBox1.Items[comboBox1.SelectedIndex].ToString() == comboBox1.Text)
            {
                string sql1 = "SELECT SaleNumber AS 'מספר מכירה', RowNumber AS 'מספר שורה', ItemNumber AS 'מספר פריט', Amount AS 'כמות', Price AS 'מחיר', CarNumber AS 'מספר רכב', StartDate AS 'תאריך התחלה', SupposeEndDate AS 'תאריך משוער לסיום', EndDate AS 'תאריך סיום', PaymentMethods.Method AS 'אמצעי תשלום', Description AS 'תיאור', Active AS 'בתהליך', Active2 AS 'פעיל' FROM Sales, PaymentMethods WHERE " + tran.Find(comboBox1.Text);
                DateTime a;
                if ((tran.Find(comboBox1.Text) == "StartDate" || tran.Find(comboBox1.Text) == "SupposeEndDate" || tran.Find(comboBox1.Text) == "EndDate") && DateTime.TryParse(comboBox2.Text, out a))
                    sql1 += "='" + Convert.ToDateTime(comboBox2.Text).ToString("MM/dd/yyyy") + "' ";
                else
                    sql1 += " LIKE'" + comboBox2.Text + "%' ";
                sql1 += "AND PaymentMethods.Number=PaymentMethod";
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

        private void ShowSale_Load(object sender, EventArgs e)
        {
            tran.Add("SaleNumber", "מספר מכירה");
            tran.Add("RowNumber", "מספר שורה");
            tran.Add("ItemNumber", "מספר פריט");
            tran.Add("Amount", "כמות");
            tran.Add("Price", "מחיר");
            tran.Add("CarNumber", "מספר רכב");
            tran.Add("StartDate", "תאריך התחלה");
            tran.Add("SupposeEndDate", "תאריך משוער לסיום");
            tran.Add("EndDate", "תאריך סיום");
            tran.Add("PaymentMethods.Method", "אמצעי תשלום");
            tran.Add("Description", "תיאור");
            tran.Add("Active", "פעיל");
            string sql1 = "SELECT SaleNumber AS 'מספר מכירה', RowNumber AS 'מספר שורה', ItemNumber AS 'מספר פריט', Amount AS 'כמות', Price AS 'מחיר', CarNumber AS 'מספר רכב', StartDate AS 'תאריך התחלה', SupposeEndDate AS 'תאריך משוער לסיום', EndDate AS 'תאריך סיום', PaymentMethod AS 'אמצעי תשלום', Description AS 'תיאור', Active AS 'פעיל' FROM Sales";
            Checks.FillColumns(comboBox1, sql1, "");
        }
        private void dataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string ID = dataGridView1[0, e.RowIndex].Value.ToString();
                if (type.Equals("1"))
                {
                    AddSale f1 = new AddSale(ID, dataGridView1[1, e.RowIndex].Value.ToString(), "u");
                    FormInPanel.AddForm(Form1.mainp, f1);
                    dataGridView1.Visible = false;
                }
                if (type.Equals("2"))
                {
                    DeleteSale f = new DeleteSale(ID, dataGridView1[1, e.RowIndex].Value.ToString());
                    FormInPanel.AddForm(Form1.mainp, f);
                    dataGridView1.Visible = false;
                }
                if (type.Equals("dc"))
                {
                    DialogResult r = MessageBox.Show("?האם אתה בטוח שאתה רוצה למחוק", "מחיקה", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (r == DialogResult.Yes)
                    {
                        string delete = "UPDATE Sales SET Active2='" + false + "' WHERE SaleNumber='" + dataGridView1[0, e.RowIndex].Value.ToString() + "' AND RowNumber='" + dataGridView1[1, e.RowIndex].Value.ToString() + "'";
                        MyAdoHelperCsharp.DoQuery("Database1.mdf", delete);
                        string sql1 = "SELECT SaleNumber AS 'מספר מכירה', RowNumber AS 'מספר שורה', ItemNumber AS 'מספר פריט', Amount AS 'כמות', Price AS 'מחיר', Sales.CarNumber AS 'מספר רכב', StartDate AS 'תאריך התחלה', SupposeEndDate AS 'תאריך משוער לסיום', EndDate AS 'תאריך סיום', PaymentMethods.Method AS 'אמצעי תשלום', Description AS 'תיאור',";
                        if (type == "dc")
                            sql1 += " CustomerID AS 'ת.ז לקוח',";
                        else
                            sql1 += " GarageNumber AS 'מספר מוסך',";
                        sql1 += " Sales.Active AS 'בתהליך', Active2 AS 'פעיל' FROM Sales, PaymentMethods, Cars WHERE ";
                        if (type == "dc")
                            sql1 += "CustomerID !='' AND CustomerID='" + number + "'";
                        else
                            sql1 += "GarageNumber !='' AND GarageNumber='" + number + "'";
                        sql1 += " AND PaymentMethods.Number=PaymentMethod AND Sales.CarNumber=Cars.CarNumber";
                        if (type != "")
                            sql1 += " AND Sales.Active='True' AND Active2='True'";
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
                            if (type == "dc")
                            {
                                string delete2 = "UPDATE Customers SET Active='" + false + "' WHERE CustomerID='" + number + "'";
                                MyAdoHelperCsharp.DoQuery("Database1.mdf", delete2);
                                MessageBox.Show("הלקוח נמחק בהצלחה");
                                UserLogIn.myforms.Remove(UserLogIn.myforms[UserLogIn.myforms.Count - 1]);
                                this.Close();
                            }
                            else
                            {
                                string delete2 = "UPDATE Garages SET Active='" + false + "' WHERE GarageNumber='" + number + "'";
                                MyAdoHelperCsharp.DoQuery("Database1.mdf", delete2);
                                MessageBox.Show("המוסך נמחק בהצלחה");
                                UserLogIn.myforms.Remove(UserLogIn.myforms[UserLogIn.myforms.Count - 1]);
                                this.Close();
                            }
                        }
                    }
                }
                if (type.Equals("dg"))
                {
                    DialogResult r = MessageBox.Show("?האם אתה בטוח שאתה רוצה למחוק", "מחיקה", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (r == DialogResult.Yes)
                    {
                        string delete = "UPDATE Sales SET Active2='" + false + "' WHERE SaleNumber='" + dataGridView1[0, e.RowIndex].Value.ToString() + "' AND RowNumber='" + dataGridView1[1, e.RowIndex].Value.ToString() + "'";
                        MyAdoHelperCsharp.DoQuery("Database1.mdf", delete);
                        string sql1 = "SELECT SaleNumber AS 'מספר מכירה', RowNumber AS 'מספר שורה', ItemNumber AS 'מספר פריט', Amount AS 'כמות', Price AS 'מחיר', Sales.CarNumber AS 'מספר רכב', StartDate AS 'תאריך התחלה', SupposeEndDate AS 'תאריך משוער לסיום', EndDate AS 'תאריך סיום', PaymentMethods.Method AS 'אמצעי תשלום', Description AS 'תיאור',";
                        if (type == "dc")
                            sql1 += " CustomerID AS 'ת.ז לקוח',";
                        else
                            sql1 += " GarageNumber AS 'מספר מוסך',";
                        sql1 += " Sales.Active AS 'בתהליך', Active2 AS 'פעיל' FROM Sales, PaymentMethods, Cars WHERE ";
                        if (type == "dc")
                            sql1 += "CustomerID !='' AND CustomerID='" + number + "'";
                        else
                            sql1 += "GarageNumber !='' AND GarageNumber='" + number + "'";
                        sql1 += " AND PaymentMethods.Number=PaymentMethod AND Sales.CarNumber=Cars.CarNumber";
                        if (type != "")
                            sql1 += " AND Sales.Active='True' AND Active2='True'";
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
                            if (type == "dc")
                            {
                                string delete2 = "UPDATE Customers SET Active='" + false + "' WHERE CustomerID='" + number + "'";
                                MyAdoHelperCsharp.DoQuery("Database1.mdf", delete2);
                                MessageBox.Show("הלקוח נמחק בהצלחה");
                                UserLogIn.myforms.Remove(UserLogIn.myforms[UserLogIn.myforms.Count - 1]);
                                this.Close();
                            }
                            else
                            {
                                string delete2 = "UPDATE Garages SET Active='" + false + "' WHERE GarageNumber='" + number + "'";
                                MyAdoHelperCsharp.DoQuery("Database1.mdf", delete2);
                                MessageBox.Show("המוסך נמחק בהצלחה");
                                UserLogIn.myforms.Remove(UserLogIn.myforms[UserLogIn.myforms.Count - 1]);
                                this.Close();
                            }
                        }
                    }
                }
            }
            else
                MessageBox.Show("עליך להקליק על אחד מהשורות המלאות");
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sql1 = "SELECT distinct " + tran.Find(comboBox1.Text) + " FROM Sales, PaymentMethods WHERE PaymentMethods.Number=PaymentMethod";
            if (type != "")
                sql1 += " AND Active='True' AND Active2='True'";
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
