using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
namespace OsherProject
{
    public partial class UpdateItem : Form
    {
        public UpdateItem()
        {
            InitializeComponent();
            comboBox1.Height = 21;
        }
        string ID="";
        string supnum;
        public UpdateItem(string id, string sup)
        {
            InitializeComponent();
            ID = id;

            string sql1 = "SELECT distinct Name FROM CarTypes order by Name";
            DataTable dt1 = MyAdoHelperCsharp.ExecuteDataTable("Database1.mdf", sql1);
            textBox7.AutoCompleteMode = AutoCompleteMode.Suggest;
            textBox7.AutoCompleteSource = AutoCompleteSource.ListItems;
            textBox7.DataSource = dt1;
            textBox7.ValueMember = "Name";
            textBox7.Text = "";

            supnum = sup;
            comboBox1.Text = ID;
            comboBox1.Height = 21;
            Sets.SetName(textBox1);
            Sets.SetNumber(textBox8);
            Sets.SetNumber(textBox3);
            Sets.SetPrice(textBox4);
            Sets.SetNote(textBox6);
            Sets.SetPrice(textBox2);
        }
        bool f;
        string picLoc = "";
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (label10.Text == "תקין" && label12.Text == "תקין" && label19.Text == "תקין" && ((label13.Text == "תקין" && comboBox2.Items.Count > 0) || (string.IsNullOrEmpty(label13.Text) && comboBox2.Items.Count==0)) && label15.Text == "תקין" && label16.Text == "תקין" && ((label18.Text == "תקין" && comboBox2.Items.Count > 0) || (string.IsNullOrEmpty(label18.Text) && comboBox2.Items.Count == 0)))
            {
                string sql1 = "SELECT * FROM CarTypes WHERE Name='" + textBox7.Text + "'";
                DataTable dt1 = MyAdoHelperCsharp.ExecuteDataTable("Database1.mdf", sql1);
                string update = "UPDATE Items SET ItemName='" + textBox1.Text + "', Amount='" + textBox3.Text + "', Description='" + textBox6.Text + "', CarType='" + dt1.Rows[0]["ID"].ToString() + "', PriceList='" + textBox2.Text;
                if (string.IsNullOrEmpty(pictureBox1.ImageLocation))
                {
                    update += "', Picture= NULL , Active='" + checkBox1.Checked + "' WHERE ItemNumber='" + comboBox1.Text + "';";
                    MyAdoHelperCsharp.DoQuery("Database1.mdf", update);
                }
                else
                {
                    byte[] binaryImg;
                    FileStream fs = new FileStream(picLoc, FileMode.Open, FileAccess.Read);
                    BinaryReader br = new BinaryReader(fs);
                    binaryImg = br.ReadBytes((int)fs.Length);
                    update += "', Picture= @img , Active='" + checkBox1.Checked + "' WHERE ItemNumber='" + comboBox1.Text + "';";
                    MyAdoHelperCsharp.DoQueryWithImage("Database1.mdf", update, binaryImg);
                }
                string update2 = "UPDATE Unions SET ItemNumberAtSupplier='" + textBox8.Text + "', PriceFromSupplier='" + textBox4.Text + "' WHERE ItemNumber='" + comboBox1.Text + "' AND SupplierNumber='" + comboBox2.Text + "';";
                MyAdoHelperCsharp.DoQuery("Database1.mdf", update2);
                if (f != checkBox1.Checked)
                {
                    string update3 = "UPDATE Unions SET Active='" + checkBox1.Checked + "' WHERE ItemNumber='" + comboBox1.Text + "';";
                    MyAdoHelperCsharp.DoQuery("Database1.mdf", update3);
                }
                MessageBox.Show("פריט עודכן בהצלחה");
            }
            else
                MessageBox.Show("עליך לתקן את הטופס");
        }
        string itemnumsup;
        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            string sql1 = "SELECT * FROM Items WHERE ItemNumber='" + ID + "'";
            if (MyAdoHelperCsharp.IsExist("Database1.mdf", sql1))
            {
                groupBox1.Visible = true;
                DataTable dt = MyAdoHelperCsharp.ExecuteDataTable("Database1.mdf", sql1);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    textBox1.Text = dt.Rows[i]["ItemName"].ToString();
                    textBox3.Text = dt.Rows[i]["Amount"].ToString();
                    textBox6.Text = "f";
                    textBox6.Text = dt.Rows[i]["Description"].ToString();
                    textBox7.Text = dt.Rows[i]["CarType"].ToString();
                    textBox2.Text = dt.Rows[i]["PriceList"].ToString();
                    if (!string.IsNullOrEmpty(dt.Rows[i]["Picture"].ToString()))
                    {
                        byte[] tempimg = (byte[])dt.Rows[i]["Picture"];
                        MemoryStream tempstream = new MemoryStream(tempimg);
                        pictureBox1.Image = Image.FromStream(tempstream);
                        pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                    }
                    checkBox1.Checked = (bool)dt.Rows[i]["Active"];
                    f = checkBox1.Checked;
                }
                string sql2 = "SELECT * FROM Unions WHERE ItemNumber='" + ID + "' AND SupplierNumber='" + supnum + "'";
                if (MyAdoHelperCsharp.IsExist("Database1.mdf", sql2))
                {
                    DataTable dt1 = MyAdoHelperCsharp.ExecuteDataTable("Database1.mdf", sql2);
                    string sql3 = "SELECT distinct SupplierNumber FROM Unions WHERE ItemNumber='" + ID + "'";
                    DataTable dt2 = MyAdoHelperCsharp.ExecuteDataTable("Database1.mdf", sql3);
                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {
                        comboBox2.DataSource = dt2;
                        comboBox2.ValueMember = "SupplierNumber";
                        comboBox2.Text = dt1.Rows[i]["SupplierNumber"].ToString();
                        itemnumsup = dt1.Rows[i]["ItemNumberAtSupplier"].ToString();
                        textBox8.Text = dt1.Rows[i]["ItemNumberAtSupplier"].ToString();
                        textBox4.Text = dt1.Rows[i]["PriceFromSupplier"].ToString();
                    }
                }
            }
            else
                groupBox1.Visible = false;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (!Checks.NameLength(textBox1.Text, 4) || !Checks.NameSofiot(textBox1.Text))
            {
                textBox1.BackColor = Color.Red;
                label10.ForeColor = Color.Red;
                label10.Text = "שם פריט זה לא תקין";
            }
            else
            {
                textBox1.BackColor = Color.Green;
                label10.ForeColor = Color.Green;
                label10.Text = "תקין";
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox3.Text))
            {
                textBox3.BackColor = Color.Red;
                label12.ForeColor = Color.Red;
                label12.Text = "כמות זו לא תקינה";
            }
            else
            {
                textBox3.BackColor = Color.Green;
                label12.ForeColor = Color.Green;
                label12.Text = "תקין";
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox4.Text) && comboBox2.Items.Count > 0)
            {
                textBox4.BackColor = Color.Red;
                label13.ForeColor = Color.Red;
                label13.Text = "מחיר זה לא תקין";
            }
            else
            {
                if (!string.IsNullOrEmpty(textBox4.Text) && comboBox2.Items.Count == 0)
                {
                    textBox4.BackColor = Color.Red;
                    label13.ForeColor = Color.Red;
                    label13.Text = "מחיר זה לא תקין";
                }
                else
                {
                    textBox4.BackColor = Color.Green;
                    label13.ForeColor = Color.Green;
                    label13.Text = "תקין";
                }
            }
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            if (!Checks.NameLength(textBox6.Text, 10) || !Checks.NameSofiot(textBox6.Text))
            {
                if (textBox6.Text.Length == 0)
                {
                    textBox6.BackColor = Color.Green;
                    label15.ForeColor = Color.Green;
                    label15.Text = "תקין";
                }
                else
                {
                    textBox6.BackColor = Color.Red;
                    label15.ForeColor = Color.Red;
                    label15.Text = "תיאור לא תקין";
                }
            }
            else
            {
                textBox6.BackColor = Color.Green;
                label15.ForeColor = Color.Green;
                label15.Text = "תקין";
            }
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            string sql = "SELECT * FROM CarTypes WHERE Name='" + textBox7.Text + "'";
            if (!MyAdoHelperCsharp.IsExist("Database1.mdf", sql))
            {
                textBox7.BackColor = Color.Red;
                label16.ForeColor = Color.Red;
                label16.Text = "סוג זה לא תקין";
            }
            else
            {
                textBox7.BackColor = Color.Green;
                label16.ForeColor = Color.Green;
                label16.Text = "תקין";
            }
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            string sql = "SELECT * FROM Unions WHERE ItemNumberAtSupplier='" + textBox8.Text + "' AND SupplierNumber='" + comboBox2.Text + "'";
            if (string.IsNullOrEmpty(textBox8.Text) && comboBox2.Items.Count > 0)
            {
                textBox8.BackColor = Color.Red;
                label18.ForeColor = Color.Red;
                label18.Text = "מספר קטלוגי זה לא תקין";
            }
            else
            {
                if (!string.IsNullOrEmpty(textBox8.Text) && comboBox2.Items.Count == 0)
                {
                    textBox8.BackColor = Color.Red;
                    label18.ForeColor = Color.Red;
                    label18.Text = "מספר קטלוגי זה לא תקין";
                }
                else
                {
                    if (MyAdoHelperCsharp.IsExist("Database1.mdf", sql) && textBox8.Text != itemnumsup)
                    {
                        textBox8.BackColor = Color.Red;
                        label18.ForeColor = Color.Red;
                        label18.Text = "מספר קטלוגי זה קיים במאגר";
                    }
                    else
                    {
                        textBox8.BackColor = Color.Green;
                        label18.ForeColor = Color.Green;
                        label18.Text = "תקין";
                    }
                }
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sql1 = "SELECT * FROM Items WHERE ItemNumber='" + ID + "'";
            if (MyAdoHelperCsharp.IsExist("Database1.mdf", sql1))
            {
                groupBox1.Visible = true;
                DataTable dt = MyAdoHelperCsharp.ExecuteDataTable("Database1.mdf", sql1);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    textBox1.Text = dt.Rows[i]["ItemName"].ToString();
                    textBox3.Text = dt.Rows[i]["Amount"].ToString();
                    textBox6.Text = "f";
                    textBox6.Text = dt.Rows[i]["Description"].ToString();
                    textBox7.Text = dt.Rows[i]["CarType"].ToString();
                    textBox2.Text = dt.Rows[i]["PriceList"].ToString();
                    if (!string.IsNullOrEmpty(dt.Rows[i]["Picture"].ToString()))
                    {
                        byte[] tempimg = (byte[])dt.Rows[i]["Picture"];
                        MemoryStream tempstream = new MemoryStream(tempimg);
                        pictureBox1.Image = Image.FromStream(tempstream);
                        pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                    }
                    checkBox1.Checked = (bool)dt.Rows[i]["Active"];
                    f = checkBox1.Checked;
                }
                string sql2 = "SELECT * FROM Unions WHERE ItemNumber='" + ID + "' AND SupplierNumber='" + comboBox2.Text + "'";
                if (MyAdoHelperCsharp.IsExist("Database1.mdf", sql2))
                {
                    DataTable dt1 = MyAdoHelperCsharp.ExecuteDataTable("Database1.mdf", sql2);
                    string sql3 = "SELECT distinct SupplierNumber FROM Unions WHERE ItemNumber='" + ID + "'";
                    DataTable dt2 = MyAdoHelperCsharp.ExecuteDataTable("Database1.mdf", sql3);
                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {
                        itemnumsup = dt1.Rows[i]["ItemNumberAtSupplier"].ToString();
                        textBox8.Text = dt1.Rows[i]["ItemNumberAtSupplier"].ToString();
                        textBox4.Text = dt1.Rows[i]["PriceFromSupplier"].ToString();
                    }
                }
            }
            else
                groupBox1.Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ShowSupplier f = new ShowSupplier("4", comboBox2, textBox8, textBox4, comboBox1);
            f.Show();
            this.Close();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox2.Text))
            {
                textBox2.BackColor = Color.Red;
                label19.ForeColor = Color.Red;
                label19.Text = "מחיר זה לא תקין";
            }
            else
            {
                textBox2.BackColor = Color.Green;
                label19.ForeColor = Color.Green;
                label19.Text = "תקין";
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "JPG Files (*.jpg)|*.jpg|PNG Files (*.png)|*.png|All Files (*.*)|*.*";
            dlg.Title = "Select Product Image";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                picLoc = dlg.FileName;
                pictureBox1.ImageLocation = picLoc;
                pictureBox1.Visible = true;
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            }
        }
        
        private void button5_Click(object sender, EventArgs e)
        {
            pictureBox1.SizeMode = PictureBoxSizeMode.Normal;
            pictureBox1.ImageLocation = "";
        }
    }
}
