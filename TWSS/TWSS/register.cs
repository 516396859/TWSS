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
    public partial class register : Form
    {
        public register()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void register_Load(object sender, EventArgs e)
        {
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
           

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
          
        }

        private void label6_Click(object sender, EventArgs e)
        {
            MessageBox.Show("《用户协议》：\n    本软件承若绝不会泄露用户信息，" +
                "本软件的使用权仅限玉林师范学院各二级学院使用!" +
                "因使用本软件造成的损失需用户自行承担相应责任，您可决定是否注册。注册则表明您同意该用户协议！"+
                "若贵机构不符合《用户协议》请终止注册，本软件解释权最终归计算机学院所有!");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //检查人员编号
            if(string.IsNullOrEmpty(idTb.Text.Trim())==true)
            {
                MessageBox.Show("人员编号不允许为空！");
                return;
            }
            //检查登录密码是否合法
            if(passwordTb.Text.Trim().Length<6|| passwordTb.Text.Trim().Length>12|| string.IsNullOrEmpty(passwordTb.Text.Trim()) == true)
            {
                MessageBox.Show("密码字符个数必须大于6小于12个！");
                return;
            }
            if(!passwordTb.Text.Trim().Equals(passwordTb2.Text.Trim()))
            {
                MessageBox.Show("两次输入的密码不一致！");
                return;
            }
            //检查二级密码是否为空
            if (string.IsNullOrEmpty(textBox1.Text.Trim()) == true)
            {
                MessageBox.Show("二级密码不允许为空！");
                return;
            }

            //检查姓名是否为空
            if (string.IsNullOrEmpty(nameTb.Text.Trim())==true)
            {
                MessageBox.Show("名字不允许为空！");
                return;
            }
            //检查职称是否为空
            if (string.IsNullOrEmpty(titleTb.Text.Trim()) == true)
            {
                MessageBox.Show("职称不允许为空！");
                return;
            }
            //检查学院是否为空
            if (string.IsNullOrEmpty(comboBox1.Text.Trim()) == true)
            {
                MessageBox.Show("学院不允许为空！");
                return;
            }
            
            //检查是否同意用户协议
            if (checkBox1.Checked == false)
            {
                MessageBox.Show("必须同意《用户协议》才能使用本软件！");
                return;
            }

            //========================运行到这来表示上面条件全部满足（将数据写入数据库userDat）=========================
            string id=idTb.Text.Trim();
            string password=passwordTb.Text.Trim();
            string name=nameTb.Text.Trim();
            string title=titleTb.Text.Trim();
            string collage=comboBox1.Text.Trim();
            string pass2 = textBox1.Text.Trim();
            //先查有没有相同id 不能重复注册
            SQLHelper helper = new SQLHelper();
            string sql = "select id from userTable where id="+"'"+id+"'";
            int count = 0;
            count = helper.SqlServerRecordCount(sql);
            if(count==1)
            {
                MessageBox.Show("已经存在该用户，请直接用密码登录！");
                return;
            }
            else
            {
                string insertSql = "insert into userTable values("+"'"+id+ "'" + "," + "'" + password + "'" +
                    "," + "'" + name + "'" + "," + "'" + title + "'" +
                    "," + "'" + collage + "'" +","+ "'" + pass2+"'" + ")";

                int flag = 0;
                flag=helper.ExecuteNonQueryCount(insertSql);
                if(flag==1)
                {
                    MessageBox.Show("已成功注册！请直接登录！");
                    this.Close();
                    this.Dispose();
                }
            }
            

        }
    }
}
