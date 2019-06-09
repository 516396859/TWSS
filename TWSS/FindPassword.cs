using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TWSS
{
    public partial class FindPassword : Form
    {
        public FindPassword()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {


            //检查人员编号
            if (string.IsNullOrEmpty(textBox4.Text.Trim()) == true)
            {
                MessageBox.Show("人员编号不允许为空！");
                return;
            }
            //检查二级口令是否为空
            if (string.IsNullOrEmpty(textBox1.Text.Trim()) == true)
            {
                MessageBox.Show("二级口令不允许为空！");
                return;
            }
            //检查登录密码是否合法
            if (textBox2.Text.Trim().Length < 6 || textBox2.Text.Trim().Length > 12 || string.IsNullOrEmpty(textBox2.Text.Trim()) == true)
            {
                MessageBox.Show("密码字符个数必须大于6小于12个！");
                return;
            }
            if (!textBox2.Text.Trim().Equals(textBox3.Text.Trim()))
            {
                MessageBox.Show("两次输入的密码不一致！");
                return;
            }
            string password = textBox2.Text.Trim();
            string id = textBox4.Text.Trim();
            string en = textBox1.Text.Trim();
            SQLHelper helper = new SQLHelper();
            string sql = "select * from userTable where encrypted=" + "'" + en + "'"+" and id="+"'"+id+"'";

            SQLHelper help = new SQLHelper();
            int count = 0;
            count = help.SqlServerRecordCount(sql);
            if (count > 0)
            {
                string updata = "update userTable \n set password=" + "'" + password+ "'" + " where id=" + "'" + id + "'";
                helper.ExecuteNonQuery(updata);
                MessageBox.Show("密码已经修改！请妥善保管！");
                this.Close();
                this.Dispose();

            }
            else
            {
                MessageBox.Show("输入的二级口令不正确或者人员编号错误！");
            }


        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
