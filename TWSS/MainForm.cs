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
    public partial class MainForm : Form
    {
        string user;
        private static TS ts;
        
        public MainForm(string user1)
        {
            this.user = user1;
            InitializeComponent();
        }

        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            string time=DateTime.Now.ToLocalTime().ToString();
            this.time2.Text = time;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            timer1.Start();
            //将下面显示欢迎对象的名字设置为登录对象名字
            SQLHelper helper = new SQLHelper();
            string sql1 = "select name from userTable where id=" + "'"+ user + "'";
            Object obj = helper.ExecuteScalar(sql1);
            string tname = obj.ToString();
            this.Tname.Text = tname+"老师";
            //如果是超级管理员则改为超级管理员，否则什么都不说
            string sql2 = "select  * from superManag where id=" + "'" + user + "'";
            int count=helper.SqlServerRecordCount(sql2);
            if (count>0)
            {
                this.superManag.Text = "(超级管理员)";
            }


        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void 退出系统ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("确认退出登录？（记得保存更改哦！）", "退出登录？",  MessageBoxButtons.OKCancel
                , MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            if (dr == DialogResult.OK)
            {
                this.Dispose();
            }
                
            else
                return;
        }

        private void 关于ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("本软件由作者个人设计，能力有限，" +
                "不足之处可以共同维护探讨！\n其源代码已传GitHub，，" +
                "欢迎维护 ！ gitHub地址：" +
                " https://github.com/516396859/TWSS.git");
        }

        private void 计算器ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(@"C:\windows\system32\calc.exe");
        }

        private void 教学统计ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //如果ts为空，或者ts未被关闭了，防止多开实例
            if(ts==null||ts.IsDisposed)
            {
                ts = new TS();
                ts.MdiParent = this;
                ts.Show();
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dr = MessageBox.Show("退出系统？", "退出！", MessageBoxButtons.OKCancel
                 , MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            if (dr == DialogResult.OK)
                System.Environment.Exit(0);
            else
                e.Cancel = true;
        }

        private void toolStripStatusLabel6_Click(object sender, EventArgs e)
        {

        }

        private void time2_Click(object sender, EventArgs e)
        {

        }

        private void 效绩统计ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("可扩展需求！");
        }

        private void 关闭系统ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("退出系统？", "退出！", MessageBoxButtons.OKCancel
                 , MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            if (dr == DialogResult.OK)
                System.Environment.Exit(0);
        }

        private void 竞赛培训ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            keYan sr = new keYan();
            sr.Show();
        }

        private void 个人管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            persionalInfo per = new persionalInfo();
            per.MdiParent = this;
            per.Show();
        }

        private void 工作量查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void 竞赛ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 教学工作量ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            infoQuerAndAlter iqa = new infoQuerAndAlter();
            iqa.MdiParent = this;
            iqa.Show();
        }

        private void 超级管理员ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //判断是否有权限登录到授权管理
            SQLHelper helper = new SQLHelper();
            string sql = "select * from superManag where id='"+Program.mainUser+"'";
            int count=helper.SqlServerRecordCount(sql);
            if (count>0)
            {
                check ck = new check();
                ck.MdiParent = this;
                ck.Show();
            }
            else
            {
                MessageBox.Show("sorry! 您不是管理员！");
            }
            
        }

        private void 帮助ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 科学研究ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CT ct = new CT();
            ct.ShowDialog();
        }
    }
}
