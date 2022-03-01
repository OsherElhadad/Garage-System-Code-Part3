using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
namespace OsherProject
{
    public partial class SalesGraphicQuery : Form
    {
        public bool flag1 = true, flag2 = false;
        public string sql = "", type = "Customers.CustomerID";
        public Series MySeries = new Series();
        public SalesGraphicQuery()
        {
            InitializeComponent();
            Design.Designer(this);
            Sets.SetGeneral(comboBox2);
            comboBox1.Text = "פילוג לפי לקוחות";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                flag1 = false;
                flag2 = true;
            }
            else
            {
                flag2 = false;
                flag1 = true;
            }
            chart1.Series.Clear();
            MySeries.Points.Clear();
            chart1.Series.Add(MySeries);
            if (flag1)
            {
                chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
                MySeries.Font = new Font("Tahoma", 14f);
                chart1.ChartAreas[0].AxisX.LabelStyle.ForeColor = Color.Black;
                chart1.ChartAreas[0].AxisY.LabelStyle.ForeColor = Color.Black;
                chart1.ChartAreas[0].AxisX.LabelStyle.Font = new Font("Tahoma", 14f);
                chart1.ChartAreas[0].AxisY.LabelStyle.Font = new Font("Tahoma", 14f);
                chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.Silver;
                chart1.ChartAreas[0].AxisX.LineColor = Color.Silver;
                chart1.ChartAreas[0].AxisY.LineColor = Color.Silver;
                chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.Silver;
                chart1.ChartAreas[0].AxisX.MajorGrid.LineWidth = 2;
                chart1.ChartAreas[0].AxisY.MajorGrid.LineWidth = 2;
                chart1.ChartAreas[0].AxisX2.MajorGrid.LineWidth = 2;
                chart1.ChartAreas[0].BackColor = Color.White;
            }
            else if (flag2)
            {
                chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
                chart1.Series[0]["PieLabelStyle"] = "Outside";
                chart1.Series[0]["PieLineColor"] = "White";
                chart1.Series[0].Font = new Font("Tahoma", 16f);
                chart1.Series[0].LabelForeColor = Color.White;
                chart1.ChartAreas[0].BackColor = ColorTranslator.FromHtml("#6464FA");
            }

            if (comboBox1.Text == "פילוג לפי לקוחות")
            {
                string sql2 = "SELECT DISTINCT Customers.CustomerFName AS Fname, Customers.CustomerLName AS Lname FROM Cars, Sales, Customers WHERE Sales.CarNumber=Cars.CarNumber AND Cars.CustomerID!='' AND Cars.CustomerID=Customers.CustomerID AND Customers.CustomerID LIKE'" + comboBox2.Text + "%'";
                DataTable dt2 = MyAdoHelperCsharp.ExecuteDataTable("Database1.mdf", sql2);
                for (int i = 0; i < dt2.Rows.Count; i++)
                {
                    sql = "SELECT COUNT(t1.Num) AS Number FROM (SELECT DISTINCT SaleNumber, Customers.CustomerFName AS Fname, Sales.CarNumber AS Num, Customers.CustomerLName AS Lname FROM Cars, Sales, Customers WHERE Sales.CarNumber=Cars.CarNumber AND Cars.CustomerID!='' AND Cars.CustomerID=Customers.CustomerID AND CustomerFName='" + dt2.Rows[i]["FName"].ToString() + "' AND CustomerLName='" + dt2.Rows[i]["LName"].ToString() + "' AND Sales.Active='false' AND Sales.Active2='true') t1";
                    DataTable dt = MyAdoHelperCsharp.ExecuteDataTable("Database1.mdf", sql);
                    if (flag2)
                    {
                        MySeries.Points.AddXY(dt2.Rows[i]["FName"].ToString() + " " + dt2.Rows[i]["LName"].ToString() + " " + dt.Rows[0]["Number"].ToString() + " מכירות ללקוח זה", dt.Rows[0]["Number"].ToString());
                    }
                    else
                    {
                        MySeries.Points.AddXY(dt2.Rows[i]["FName"].ToString() + " " + dt2.Rows[i]["LName"].ToString(), dt.Rows[0]["Number"].ToString());
                    }
                }
                chart1.Visible = true;
            }
            else if (comboBox1.Text == "פילוג לפי פריטים")
            {
                string sql2 = "SELECT DISTINCT Items.ItemName AS Name FROM Items, Sales WHERE Sales.ItemNumber=Items.ItemNumber AND Items.ItemNumber LIKE'" + comboBox2.Text + "%'";
                DataTable dt2 = MyAdoHelperCsharp.ExecuteDataTable("Database1.mdf", sql2);
                for (int i = 0; i < dt2.Rows.Count; i++)
                {
                    sql = "SELECT COUNT(t1.Num) AS C1 FROM (SELECT Sales.ItemNumber AS Num FROM Items, Sales WHERE Sales.ItemNumber=Items.ItemNumber AND ItemName='" + dt2.Rows[i]["Name"].ToString() + "' AND Sales.Active='false' AND Sales.Active2='true') t1";
                    DataTable dt = MyAdoHelperCsharp.ExecuteDataTable("Database1.mdf", sql);
                    if (flag2)
                    {
                        MySeries.Points.AddXY(dt2.Rows[i]["Name"].ToString() + " " + dt.Rows[0]["C1"].ToString() + " מכירות לפריט זה", dt.Rows[0]["C1"].ToString());
                    }
                    else
                    {
                        MySeries.Points.AddXY(dt2.Rows[i]["Name"].ToString(), dt.Rows[0]["C1"].ToString());
                    }
                }
                chart1.Visible = true;
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                flag1 = false;
                flag2 = true;
            }
            else
            {
                flag2 = false;
                flag1 = true;
            }
            chart1.Series.Clear();
            MySeries.Points.Clear();
            chart1.Series.Add(MySeries);
            if (flag1)
            {
                chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
                MySeries.Font = new Font("Tahoma", 14f);
                chart1.ChartAreas[0].AxisX.LabelStyle.ForeColor = Color.Black;
                chart1.ChartAreas[0].AxisY.LabelStyle.ForeColor = Color.Black;
                chart1.ChartAreas[0].AxisX.LabelStyle.Font = new Font("Tahoma", 14f);
                chart1.ChartAreas[0].AxisY.LabelStyle.Font = new Font("Tahoma", 14f);
                chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.Silver;
                chart1.ChartAreas[0].AxisX.LineColor = Color.Silver;
                chart1.ChartAreas[0].AxisY.LineColor = Color.Silver;
                chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.Silver;
                chart1.ChartAreas[0].AxisX.MajorGrid.LineWidth = 2;
                chart1.ChartAreas[0].AxisY.MajorGrid.LineWidth = 2;
                chart1.ChartAreas[0].AxisX2.MajorGrid.LineWidth = 2;
                chart1.ChartAreas[0].BackColor = Color.White;
            }
            else if (flag2)
            {
                chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
                chart1.Series[0]["PieLabelStyle"] = "Outside";
                chart1.Series[0]["PieLineColor"] = "White";
                chart1.Series[0].Font = new Font("Tahoma", 16f);
                chart1.Series[0].LabelForeColor = Color.White;
                chart1.ChartAreas[0].BackColor = ColorTranslator.FromHtml("#6464FA");
            }

            if (comboBox1.Text == "פילוג לפי לקוחות")
            {
                string sql2 = "SELECT DISTINCT Customers.CustomerFName AS Fname, Customers.CustomerLName AS Lname FROM Cars, Sales, Customers WHERE Sales.CarNumber=Cars.CarNumber AND Cars.CustomerID!='' AND Cars.CustomerID=Customers.CustomerID AND Customers.CustomerID LIKE'" + comboBox2.Text + "%'";
                DataTable dt2 = MyAdoHelperCsharp.ExecuteDataTable("Database1.mdf", sql2);
                for (int i = 0; i < dt2.Rows.Count; i++)
                {
                    sql = "SELECT COUNT(t1.Num) AS Number FROM (SELECT DISTINCT SaleNumber, Customers.CustomerFName AS Fname, Sales.CarNumber AS Num, Customers.CustomerLName AS Lname FROM Cars, Sales, Customers WHERE Sales.CarNumber=Cars.CarNumber AND Cars.CustomerID!='' AND Cars.CustomerID=Customers.CustomerID AND CustomerFName='" + dt2.Rows[i]["FName"].ToString() + "' AND CustomerLName='" + dt2.Rows[i]["LName"].ToString() + "' AND Sales.Active='false' AND Sales.Active2='true') t1";
                    DataTable dt = MyAdoHelperCsharp.ExecuteDataTable("Database1.mdf", sql);
                    if (flag2)
                    {
                        MySeries.Points.AddXY(dt2.Rows[i]["FName"].ToString() + " " + dt2.Rows[i]["LName"].ToString() + " " + dt.Rows[0]["Number"].ToString() + " מכירות ללקוח זה", dt.Rows[0]["Number"].ToString());
                    }
                    else
                    {
                        MySeries.Points.AddXY(dt2.Rows[i]["FName"].ToString() + " " + dt2.Rows[i]["LName"].ToString(), dt.Rows[0]["Number"].ToString());
                    }
                }
                chart1.Visible = true;
            }
            else if (comboBox1.Text == "פילוג לפי פריטים")
            {
                string sql2 = "SELECT DISTINCT Items.ItemName AS Name FROM Items, Sales WHERE Sales.ItemNumber=Items.ItemNumber AND Items.ItemNumber LIKE'" + comboBox2.Text + "%'";
                DataTable dt2 = MyAdoHelperCsharp.ExecuteDataTable("Database1.mdf", sql2);
                for (int i = 0; i < dt2.Rows.Count; i++)
                {
                    sql = "SELECT COUNT(t1.Num) AS C1 FROM (SELECT Sales.ItemNumber AS Num FROM Items, Sales WHERE Sales.ItemNumber=Items.ItemNumber AND ItemName='" + dt2.Rows[i]["Name"].ToString() + "' AND Sales.Active='false' AND Sales.Active2='true') t1";
                    DataTable dt = MyAdoHelperCsharp.ExecuteDataTable("Database1.mdf", sql);
                    if (flag2)
                    {
                        MySeries.Points.AddXY(dt2.Rows[i]["Name"].ToString() + " " + dt.Rows[0]["C1"].ToString() + " מכירות לפריט זה", dt.Rows[0]["C1"].ToString());
                    }
                    else
                    {
                        MySeries.Points.AddXY(dt2.Rows[i]["Name"].ToString(), dt.Rows[0]["C1"].ToString());
                    }
                }
                chart1.Visible = true;
            }
        }
        private void comboBox2_TextChanged(object sender, EventArgs e)
        {
            chart1.Series.Clear();
            MySeries.Points.Clear();
            chart1.Series.Add(MySeries);
            if (flag1)
            {
                chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
                MySeries.Font = new Font("Tahoma", 14f);
                chart1.ChartAreas[0].AxisX.LabelStyle.ForeColor = Color.Black;
                chart1.ChartAreas[0].AxisY.LabelStyle.ForeColor = Color.Black;
                chart1.ChartAreas[0].AxisX.LabelStyle.Font = new Font("Tahoma", 14f);
                chart1.ChartAreas[0].AxisY.LabelStyle.Font = new Font("Tahoma", 14f);
                chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.Silver;
                chart1.ChartAreas[0].AxisX.LineColor = Color.Silver;
                chart1.ChartAreas[0].AxisY.LineColor = Color.Silver;
                chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.Silver;
                chart1.ChartAreas[0].AxisX.MajorGrid.LineWidth = 2;
                chart1.ChartAreas[0].AxisY.MajorGrid.LineWidth = 2;
                chart1.ChartAreas[0].AxisX2.MajorGrid.LineWidth = 2;
                chart1.ChartAreas[0].BackColor = Color.White;
            }
            else if (flag2)
            {
                chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
                chart1.Series[0]["PieLabelStyle"] = "Outside";
                chart1.Series[0]["PieLineColor"] = "White";
                chart1.Series[0].Font = new Font("Tahoma", 16f);
                chart1.Series[0].LabelForeColor = Color.White;
                chart1.ChartAreas[0].BackColor = ColorTranslator.FromHtml("#6464FA");
            }

            if (comboBox1.Text == "פילוג לפי לקוחות")
            {
                string sql2 = "SELECT DISTINCT Customers.CustomerFName AS Fname, Customers.CustomerLName AS Lname FROM Cars, Sales, Customers WHERE Sales.CarNumber=Cars.CarNumber AND Cars.CustomerID!='' AND Cars.CustomerID=Customers.CustomerID AND Customers.CustomerID LIKE'" + comboBox2.Text + "%'";
                DataTable dt2 = MyAdoHelperCsharp.ExecuteDataTable("Database1.mdf", sql2);
                for (int i = 0; i < dt2.Rows.Count; i++)
                {
                    sql = "SELECT COUNT(t1.Num) AS Number FROM (SELECT DISTINCT SaleNumber, Customers.CustomerFName AS Fname, Sales.CarNumber AS Num, Customers.CustomerLName AS Lname FROM Cars, Sales, Customers WHERE Sales.CarNumber=Cars.CarNumber AND Cars.CustomerID!='' AND Cars.CustomerID=Customers.CustomerID AND CustomerFName='" + dt2.Rows[i]["FName"].ToString() + "' AND CustomerLName='" + dt2.Rows[i]["LName"].ToString() + "' AND Sales.Active='false' AND Sales.Active2='true') t1";
                    DataTable dt = MyAdoHelperCsharp.ExecuteDataTable("Database1.mdf", sql);
                    if (flag2)
                    {
                        MySeries.Points.AddXY(dt2.Rows[i]["FName"].ToString() + " " + dt2.Rows[i]["LName"].ToString() + " " + dt.Rows[0]["Number"].ToString() + " מכירות ללקוח זה", dt.Rows[0]["Number"].ToString());
                    }
                    else
                    {
                        MySeries.Points.AddXY(dt2.Rows[i]["FName"].ToString() + " " + dt2.Rows[i]["LName"].ToString(), dt.Rows[0]["Number"].ToString());
                    }
                }
                chart1.Visible = true;
            }
            else if (comboBox1.Text == "פילוג לפי פריטים")
            {
                string sql2 = "SELECT DISTINCT Items.ItemName AS Name FROM Items, Sales WHERE Sales.ItemNumber=Items.ItemNumber AND Items.ItemNumber LIKE'" + comboBox2.Text + "%'";
                DataTable dt2 = MyAdoHelperCsharp.ExecuteDataTable("Database1.mdf", sql2);
                for (int i = 0; i < dt2.Rows.Count; i++)
                {
                    sql = "SELECT COUNT(t1.Num) AS C1 FROM (SELECT Sales.ItemNumber AS Num FROM Items, Sales WHERE Sales.ItemNumber=Items.ItemNumber AND ItemName='" + dt2.Rows[i]["Name"].ToString() + "' AND Sales.Active='false' AND Sales.Active2='true') t1";
                    DataTable dt = MyAdoHelperCsharp.ExecuteDataTable("Database1.mdf", sql);
                    if (flag2)
                    {
                        MySeries.Points.AddXY(dt2.Rows[i]["Name"].ToString() + " " + dt.Rows[0]["C1"].ToString() + " מכירות לפריט זה", dt.Rows[0]["C1"].ToString());
                    }
                    else
                    {
                        MySeries.Points.AddXY(dt2.Rows[i]["Name"].ToString(), dt.Rows[0]["C1"].ToString());
                    }
                }
                chart1.Visible = true;
            }
        }

        private void SalesGraphicQuery_Load(object sender, EventArgs e)
        {
            comboBox2.Text = "";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sql1 = "";
            if (comboBox1.Text == "פילוג לפי לקוחות")
            {
                label4.Text = "ת.ז לקוח";
                type = "Customers.CustomerID";
                sql1 = "SELECT distinct " + type + " FROM Cars, Sales, Customers WHERE Sales.CarNumber=Cars.CarNumber AND Cars.CustomerID!='' AND Cars.CustomerID=Customers.CustomerID AND Sales.Active2='true' order by " + type;
            }
            else if(comboBox1.Text == "פילוג לפי פריטים")
            {
                label4.Text = "מספר פריט";
                type = "ItemNumber";
                sql1 = "SELECT distinct " + type + " FROM Sales WHERE Sales.Active2='true' order by " + type;
            }
            comboBox2.DataSource = new List<string>();
            comboBox2.ValueMember = "";
            comboBox2.Text = "";
            DataTable dt1 = MyAdoHelperCsharp.ExecuteDataTable("Database1.mdf", sql1);
            comboBox2.AutoCompleteMode = AutoCompleteMode.Suggest;
            comboBox2.AutoCompleteSource = AutoCompleteSource.ListItems;
            comboBox2.DataSource = dt1;
            if (type.Split('.').Length == 1)
                comboBox2.ValueMember = type;
            else
                comboBox2.ValueMember = type.Split('.')[1];
            comboBox2.Text = "";

            chart1.Series.Clear();
            MySeries.Points.Clear();
            chart1.Series.Add(MySeries);
            if (flag1)
            {
                chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
                MySeries.Font = new Font("Tahoma", 14f);
                chart1.ChartAreas[0].AxisX.LabelStyle.ForeColor = Color.Black;
                chart1.ChartAreas[0].AxisY.LabelStyle.ForeColor = Color.Black;
                chart1.ChartAreas[0].AxisX.LabelStyle.Font = new Font("Tahoma", 14f);
                chart1.ChartAreas[0].AxisY.LabelStyle.Font = new Font("Tahoma", 14f);
                chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.Silver;
                chart1.ChartAreas[0].AxisX.LineColor = Color.Silver;
                chart1.ChartAreas[0].AxisY.LineColor = Color.Silver;
                chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.Silver;
                chart1.ChartAreas[0].AxisX.MajorGrid.LineWidth = 2;
                chart1.ChartAreas[0].AxisY.MajorGrid.LineWidth = 2;
                chart1.ChartAreas[0].AxisX2.MajorGrid.LineWidth = 2;
                chart1.ChartAreas[0].BackColor = Color.White;
            }
            else if (flag2)
            {
                chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
                chart1.Series[0]["PieLabelStyle"] = "Outside";
                chart1.Series[0]["PieLineColor"] = "White";
                chart1.Series[0].Font = new Font("Tahoma", 16f);
                chart1.Series[0].LabelForeColor = Color.White;
                chart1.ChartAreas[0].BackColor = ColorTranslator.FromHtml("#6464FA");
            }

            if (comboBox1.Text == "פילוג לפי לקוחות")
            {
                string sql2 = "SELECT DISTINCT Customers.CustomerFName AS Fname, Customers.CustomerLName AS Lname FROM Cars, Sales, Customers WHERE Sales.CarNumber=Cars.CarNumber AND Cars.CustomerID!='' AND Cars.CustomerID=Customers.CustomerID AND Customers.CustomerID LIKE'" + comboBox2.Text + "%'";
                DataTable dt2 = MyAdoHelperCsharp.ExecuteDataTable("Database1.mdf", sql2);
                for (int i = 0; i < dt2.Rows.Count; i++)
                {
                    sql = "SELECT COUNT(t1.Num) AS Number FROM (SELECT DISTINCT SaleNumber, Customers.CustomerFName AS Fname, Sales.CarNumber AS Num, Customers.CustomerLName AS Lname FROM Cars, Sales, Customers WHERE Sales.CarNumber=Cars.CarNumber AND Cars.CustomerID!='' AND Cars.CustomerID=Customers.CustomerID AND CustomerFName='" + dt2.Rows[i]["FName"].ToString() + "' AND CustomerLName='" + dt2.Rows[i]["LName"].ToString() + "' AND Sales.Active='false' AND Sales.Active2='true') t1";
                    DataTable dt = MyAdoHelperCsharp.ExecuteDataTable("Database1.mdf", sql);
                    if (flag2)
                    {
                        MySeries.Points.AddXY(dt2.Rows[i]["FName"].ToString() + " " + dt2.Rows[i]["LName"].ToString() + " " + dt.Rows[0]["Number"].ToString() + " מכירות ללקוח זה", dt.Rows[0]["Number"].ToString());
                    }
                    else
                    {
                        MySeries.Points.AddXY(dt2.Rows[i]["FName"].ToString() + " " + dt2.Rows[i]["LName"].ToString(), dt.Rows[0]["Number"].ToString());
                    }
                }
                chart1.Visible = true;
            }
            else if (comboBox1.Text == "פילוג לפי פריטים")
            {
                string sql2 = "SELECT DISTINCT Items.ItemName AS Name FROM Items, Sales WHERE Sales.ItemNumber=Items.ItemNumber AND Items.ItemNumber LIKE'" + comboBox2.Text + "%'";
                DataTable dt2 = MyAdoHelperCsharp.ExecuteDataTable("Database1.mdf", sql2);
                for (int i = 0; i < dt2.Rows.Count; i++)
                {
                    sql = "SELECT COUNT(t1.Num) AS C1 FROM (SELECT Sales.ItemNumber AS Num FROM Items, Sales WHERE Sales.ItemNumber=Items.ItemNumber AND ItemName='" + dt2.Rows[i]["Name"].ToString() + "' AND Sales.Active='false' AND Sales.Active2='true') t1";
                    DataTable dt = MyAdoHelperCsharp.ExecuteDataTable("Database1.mdf", sql);
                    if (flag2)
                    {
                        MySeries.Points.AddXY(dt2.Rows[i]["Name"].ToString() + " " + dt.Rows[0]["C1"].ToString() + " מכירות לפריט זה", dt.Rows[0]["C1"].ToString());
                    }
                    else
                    {
                        MySeries.Points.AddXY(dt2.Rows[i]["Name"].ToString(), dt.Rows[0]["C1"].ToString());
                    }
                }
                chart1.Visible = true;
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
    }
}
