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
    public partial class ShowGarage : Form
    {
        public ShowGarage()
        {
            InitializeComponent();
            Design.Designer(this);
            Sets.SetGeneral(comboBox1);
            Sets.SetGeneral(comboBox2);
        }
        string type = "";
        Translate tran = new Translate();
        public ShowGarage(string a)
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
                string sql1 = "SELECT GarageNumber AS 'מספר מוסך', GarageName AS 'שם מוסך', ContactFName AS 'שם פרטי', ContactLName AS 'שם משפחה', Cities.Name AS 'עיר', GarageStreet AS 'רחוב', GarageHNumber AS 'מספר בית', GarageZipCode AS 'מיקוד', PreTelephone.Pre AS 'קידומת טלפון', GarageTelephone AS 'טלפון', PrePhone.Pre AS 'קידומת פלאפון', ContactPhone AS 'פלאפון', GarageEmail AS 'אימייל', Discount AS 'הנחה', Active AS 'פעיל' FROM Garages, Cities, PreTelephone, PrePhone WHERE " + tran.Find(comboBox1.Text) + " LIKE'" + comboBox2.Text + "%' AND Cities.ID=Garages.GarageCity AND PreTelephone.Number=GaragePreTelephone AND PrePhone.Number=ContactPrePhone";
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
                    UpdateGarage f1 = new UpdateGarage(ID);
                    FormInPanel.AddForm(Form1.mainp, f1);
                    dataGridView1.Visible = false;
                }
                if (type.Equals("2"))
                {
                    DeleteGarage f = new DeleteGarage(ID);
                    FormInPanel.AddForm(Form1.mainp, f);
                    dataGridView1.Visible = false;
                }
            }
            else
                MessageBox.Show("עליך להקליק על אחד מהשורות המלאות");
        }

        private void ShowGarage_Load(object sender, EventArgs e)
        {
            tran.Add("GarageNumber", "מספר מוסך");
            tran.Add("GarageName", "שם מוסך");
            tran.Add("ContactFName", "שם פרטי");
            tran.Add("ContactLName", "שם משפחה");
            tran.Add("Name", "עיר");
            tran.Add("GarageStreet", "רחוב");
            tran.Add("GarageHNumber", "מספר בית");
            tran.Add("GarageZipCode", "מיקוד");
            tran.Add("PreTelephone.Pre", "קידומת טלפון");
            tran.Add("GarageTelephone", "טלפון");
            tran.Add("PrePhone.Pre", "קידומת פלאפון");
            tran.Add("ContactPhone", "פלאפון");
            tran.Add("GarageEmail", "אימייל");
            tran.Add("Discount", "הנחה");
            tran.Add("Active", "פעיל");
            string sql1 = "SELECT GarageNumber AS 'מספר מוסך', GarageName AS 'שם מוסך', ContactFName AS 'שם פרטי', ContactLName AS 'שם משפחה', GarageCity AS 'עיר', GarageStreet AS 'רחוב', GarageHNumber AS 'מספר בית', GarageZipCode AS 'מיקוד', GaragePreTelephone AS 'קידומת טלפון', GarageTelephone AS 'טלפון', ContactPrePhone AS 'קידומת פלאפון', ContactPhone AS 'פלאפון', GarageEmail AS 'אימייל', Discount AS 'הנחה', Active AS 'פעיל' FROM Garages";
            Checks.FillColumns(comboBox1, sql1, "");
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sql1 = "SELECT distinct " + tran.Find(comboBox1.Text) + " FROM Garages, Cities, PreTelephone, PrePhone WHERE Cities.ID=Garages.GarageCity AND PreTelephone.Number=GaragePreTelephone AND PrePhone.Number=ContactPrePhone";
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
