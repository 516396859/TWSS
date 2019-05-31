using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TWSS
{
    public partial class login : Form
    {
        string user;
        string password;
        public login()
        {
            InitializeComponent();
        }
         
        private void login_Load(object sender, EventArgs e)
        {

            //如果存在 记住密码文件
            
            if (File.Exists("user.bin"))
            {
                checkBox1.Checked = true;
               
                string contend1 = "";
                string contend2 = "";
                try
                {
                    //从user、password 文件取出数据
                    StreamReader sreader1 = new StreamReader("user.bin", Encoding.Default);
                    StreamReader sreader2 = new StreamReader("password.bin", Encoding.Default);
                    contend1 += sreader1.ReadLine() + Environment.NewLine;
                    contend2 += sreader2.ReadLine() + Environment.NewLine;

                    sreader1.Close();
                    sreader2.Close();
                }
                catch { }
                //让两个输入框显示账号密码
                this.textBox1.Text = contend1.Trim();
                this.textBox2.Text = contend2.Trim();

                //如果存在 自动登录文件
                if (File.Exists("autologin.bin"))
                {
                    checkBox2.Checked = true;
                    button1.PerformClick();
                }
            }

            
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string user="";
            string password="";
            //当选中记住密码时，将密码保存到本地
            user = this.textBox1.Text.Trim();
            password = this.textBox2.Text.Trim();

            if (checkBox1.Checked == true)
            {
               
                FileStream fs1 = new FileStream("user.bin", FileMode.Create);
                StreamWriter sw1 = new StreamWriter(fs1);
                sw1.Write(user);
                sw1.Flush();
                fs1.Close();

                FileStream fs2 = new FileStream("password.bin", FileMode.Create);
                StreamWriter sw2 = new StreamWriter(fs2);
                sw2.Write(password);
                sw2.Flush();
                fs2.Close();
            }
            //当不需要记住密码时，删除文件
            if (checkBox1.Checked == false)
            {
                if(File.Exists("password.bin")&&File.Exists("user.bin"))
                {
                    File.Delete("password.bin");
                    File.Delete("user.bin");
                }
                
            }
            //===================检查是否自动登录=======================
            if (checkBox2.Checked == true)
            {
                
                FileStream fs = new FileStream("autologin.bin", FileMode.Create);
                StreamWriter sw = new StreamWriter(fs);
                sw.Write("");
                sw.Flush();
                fs.Close();
            }
            if (checkBox2.Checked == false)
            {
                if(File.Exists("autologin.bin"))
                File.Delete("autologin.bin");
            }

            //MessageBox.Show("点击了按钮！");
            //=========================进行登录（账号、密码）判断=====================

            string sql = "select id from userTable where id="+"'"+user+"'"+" and password="+"'"+password+"'";
            SQLHelper help=new SQLHelper();
            int count = 0;
            count = help.SqlServerRecordCount(sql);
            if(count>0)
            {
                Program.mainUser = user;
                this.DialogResult = DialogResult.OK;
                this.Hide();
            }
            else
            {
                MessageBox.Show("Sorry! 账号不存在或者密码错误！");
            }


        }

       
        private void label3_Click(object sender, EventArgs e)
        {
            FindPassword ff = new FindPassword();
            ff.ShowDialog();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
           if (checkBox2.Checked == true)
               checkBox1.Checked = true;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {
            //打开注册界面进行注册
            register re = new register();
            re.ShowDialog();
        }

     
    }
}
