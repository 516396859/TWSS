using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TWSS
{
    public partial class persionalInfo : Form
    {
        SQLHelper helper;
        public persionalInfo()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
        string COLLAGE = null;
        private void persionalInfo_Load(object sender, EventArgs e)
        {
            //给comboBox1 添加项
            this.comboBox1.Items.Add("生物与制药学院");
            this.comboBox1.Items.Add("计算机科学与工程学院");
            this.comboBox1.Items.Add("教育科学学院");
            this.comboBox1.Items.Add("历史文化旅游学院");
            this.comboBox1.Items.Add("美术与设计学院");
            this.comboBox1.Items.Add("数学与统计学院");
            this.comboBox1.Items.Add("商学院");
            this.comboBox1.Items.Add("体育与健康学院");
            this.comboBox1.Items.Add("外国语学院");
            this.comboBox1.Items.Add("物理与电信工程学院");
            this.comboBox1.Items.Add("音乐舞蹈学院");
            this.comboBox1.Items.Add("政法学院");
            //从数据库读取 个人信息
            string sql = "select * from userTable where id="+ "'" + Program.mainUser + "'";
            helper = new SQLHelper();
            DataSet ds=helper.GetDataSetValue(sql,"userTable");
            DataTable dt = null;
            dt = ds.Tables["userTable"];
            string user;
            string title;
            string collage;
            string name;
           
            user = dt.Rows[0][0].ToString();
            name = dt.Rows[0][2].ToString();
            title = dt.Rows[0][3].ToString();
            collage = dt.Rows[0][4].ToString();
            COLLAGE = collage;
            textBox1.Text = user;
            textBox2.Text = name;
            textBox3.Text = collage;
            textBox4.Text = title;
 

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string user;
            string title;
            string collage;
            string name;
            
            textBox2.Enabled = true;
            textBox3.Visible = false;
            comboBox1.Visible = true;
            comboBox1.Text = COLLAGE;
            textBox4.Enabled = true;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            string title;
            string collage;
            string name;
            
            name = textBox2.Text.Trim();
            collage = comboBox1.Text.Trim();
            title = textBox4.Text.Trim();


            if(string.IsNullOrEmpty(name))
            {
                MessageBox.Show("姓名不允许为空！");
                return;
            }

            if (string.IsNullOrEmpty(collage))
            {
                MessageBox.Show("学院不允许为空！");
                return;
            }

            if (string.IsNullOrEmpty(title))
            {
                MessageBox.Show("职称不允许为空！");
                return;
            }



            //将更改后的数据更新到数据库
            string updata = "update userTable \n set title=" + "'" + title + "'" +","+"name="+ "'" + name + "'" +","+ "collage=" + "'" + collage + "'" + " where id=" + "'" + Program.mainUser + "'";
            helper.ExecuteNonQuery(updata);

            textBox2.Enabled = false;
            textBox4.Enabled = false;
            textBox3.Text = collage;
            textBox3.Visible = true;
            comboBox1.Visible = false;

            MessageBox.Show("信息修改成功！");
            
        }
    }
}
