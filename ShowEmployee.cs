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
    public partial class ShowEmployee : Form
    {
        public ShowEmployee()
        {
            InitializeComponent();
            Design.Designer(this);
            Sets.SetGeneral(comboBox1);
            Sets.SetGeneral(comboBox2);
        }
        string type = "";
        Translate tran = new Translate();
        public ShowEmployee(string a)
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

        private void ShowEmployee_Load(object sender, EventArgs e)
        {
            tran.Add("EmployeeID", "ת.ז עובד");
            tran.Add("EmployeeFName", "שם פרטי");
            tran.Add("EmployeeLName", "שם משפחה");
            tran.Add("Name", "עיר");
            tran.Add("EmployeeStreet", "רחוב");
            tran.Add("EmployeeHNumber", "מספר בית");
            tran.Add("EmployeeZipCode", "מיקוד");
            tran.Add("PrePhone.Pre", "קידומת פלאפון");
            tran.Add("EmployeePhone", "פלאפון");
            tran.Add("EmployeeBirthDate", "תאריך לידה");
            tran.Add("EmployeeEmail", "אימייל");
            tran.Add("Roles.Role", "תפקיד");
            tran.Add("Notes", "הערות");
            tran.Add("Active", "פעיל");
            string sql1 = "SELECT EmployeeID AS 'ת.ז עובד', EmployeeFName AS 'שם פרטי', EmployeeLName AS 'שם משפחה', EmployeeCity AS 'עיר', EmployeeStreet AS 'רחוב', EmployeeHNumber AS 'מספר בית', EmployeeZipCode AS 'מיקוד', EmployeePrePhone AS 'קידומת פלאפון', EmployeePhone AS 'פלאפון', EmployeeBirthDate AS 'תאריך לידה', EmployeeEmail AS 'אימייל', Role AS 'תפקיד', Notes AS 'הערות', Active AS 'פעיל' FROM Employees";
            Checks.FillColumns(comboBox1, sql1, "");
        }

        private void comboBox2_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(comboBox1.Text) && comboBox1.SelectedIndex >= 0 && comboBox1.Items[comboBox1.SelectedIndex].ToString() == comboBox1.Text)
            {
                string sql1 = "SELECT EmployeeID AS 'ת.ז עובד', EmployeeFName AS 'שם פרטי', EmployeeLName AS 'שם משפחה', Cities.Name AS 'עיר', EmployeeStreet AS 'רחוב', EmployeeHNumber AS 'מספר בית', EmployeeZipCode AS 'מיקוד', PrePhone.Pre AS 'קידומת פלאפון', EmployeePhone AS 'פלאפון', EmployeeBirthDate AS 'תאריך לידה', EmployeeEmail AS 'אימייל', Roles.Role AS 'תפקיד', Notes AS 'הערות', Active AS 'פעיל' FROM Employees, Roles, PrePhone, Cities WHERE " + tran.Find(comboBox1.Text);
                DateTime a;
                if (tran.Find(comboBox1.Text) == "EmployeeBirthDate" && DateTime.TryParse(comboBox2.Text, out a))
                    sql1 += "='" + Convert.ToDateTime(comboBox2.Text).ToString("MM/dd/yyyy") + "' ";
                else
                    sql1 += " LIKE'" + comboBox2.Text + "%' ";
                sql1 += "AND Cities.ID=Employees.EmployeeCity AND Roles.Number=Employees.Role AND PrePhone.Number=Employees.EmployeePrePhone";
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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string ID = dataGridView1[0, e.RowIndex].Value.ToString();
                if (type.Equals("1"))
                {
                    UpdateEmployee f1 = new UpdateEmployee(ID);
                    FormInPanel.AddForm(Form1.mainp, f1);
                    dataGridView1.Visible = false;
                }
                if (type.Equals("2"))
                {
                    DeleteEmployee f = new DeleteEmployee(ID);
                    FormInPanel.AddForm(Form1.mainp, f);
                    dataGridView1.Visible = false;
                }
            }
            else
                MessageBox.Show("עליך להקליק על אחד מהשורות המלאות");
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sql1 = "SELECT distinct " + tran.Find(comboBox1.Text) + " FROM Employees, Roles, PrePhone, Cities WHERE Cities.ID=Employees.EmployeeCity AND Roles.Number=Employees.Role AND PrePhone.Number=Employees.EmployeePrePhone";
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
