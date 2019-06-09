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
    public partial class TS : Form
    {
        public TS()
        {
            InitializeComponent();
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void TS_Load(object sender, EventArgs e)
        {
            //初始化 下拉列表的学期
            string xueqi;
            for (int i = 2017; i < 2050; i++)
            {
                for (int j = 1; j <= 2; j++)
                {
                    if (j == 1)
                        xueqi = "春";
                    else
                        xueqi = "秋";
                    this.cm8.Items.Add(i + "-" + xueqi);
                }
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("退出前记得保存哦!", "退出？", MessageBoxButtons.OKCancel
               , MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            if (dr == DialogResult.OK)
                this.Dispose();
            
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }
        private void toolStripLabel3_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("退出前记得保存哦？", "退出？", MessageBoxButtons.OKCancel
               , MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            if (dr == DialogResult.OK)
                this.Dispose();
            else
                return;
        }

        private void TS_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dr = MessageBox.Show("退出前记得保存哦？", "退出？", MessageBoxButtons.OKCancel
               , MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            if (dr == DialogResult.OK)
                this.Dispose();
            else
                e.Cancel = true;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            
        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {
            string proj = tb1.Text.Trim();
            string banJi=tb2.Text.Trim();
            string className=tb3.Text.Trim(); 
            string teacherId=tb4.Text.Trim();
            string classHour=tb5.Text.Trim();
            string workLoad_=tb6.Text.Trim();
            string number=tb7.Text.Trim();
            string term=cm8.Text.Trim();
            string validTime=dtp9.Text.Trim();
            string coef=tb12.Text.Trim(); 
            string remark=tb13.Text.Trim();
            string director=tb14.Text.Trim();
            string submitTime= DateTime.Now.ToString("yyyy-MM-dd", System.Globalization.DateTimeFormatInfo.InvariantInfo);
            string tableId;
            int pass1 = 0;
            int pass2 = 0;
            int pass3 = 0;

            //自动生成tableId
            tableId="T"+DateTime.Now.ToString("yyyyMMddHHmmss", System.Globalization.DateTimeFormatInfo.InvariantInfo);

            //判断项目字段是否为空
            if (string.IsNullOrEmpty(proj) == true)
            {
                MessageBox.Show("项目不允许为空！");
                return;
            }
            
           
            //判断教师ID是否为空
            if (string.IsNullOrEmpty(teacherId) == true)
            {
                MessageBox.Show("教师编号不允许为空！");
                return;

            }
            //判断教师编号与账户是否一致
            if (!Program.mainUser.Equals(teacherId))
            {
                MessageBox.Show("教师编号必须与登录账号一致！");
                return;
            }



            //判断工作量是否合法
            if (string.IsNullOrEmpty(workLoad_) == true)
            {
                MessageBox.Show("工作量不允许为空！");
                return;
            }

            try
            {
                double work = System.Convert.ToDouble(workLoad_);
            }
            catch
            {
                MessageBox.Show("工作量必须为数字！");
                return;
            }

            //判断指导人数是否合法
            try
            {
                int work = System.Convert.ToInt32(number);
            }
            catch
            {
                MessageBox.Show("指导人数必须为整数！");
                return;
            }

            //判断开课学期是否为空
            if (string.IsNullOrEmpty(term) == true)
            {
                MessageBox.Show("开课学期不允许为空！");
                return;
            }
            //判断系数是否为空
            if (string.IsNullOrEmpty(coef) == true)
            {
                MessageBox.Show("系数不允许为空！");
                return;
                
            }
            //判断系数是否合法
            try
            {
                double coe = System.Convert.ToDouble(coef);
            }
            catch
            {
                MessageBox.Show("系数必须为数字！");
                return;
            }


            //将表格数据提交到数据库
            string sql = "insert into TeachWork values(" +
                "'"+tableId+ "'" +","+
                "'" + proj + "'" + "," +
                "'" + banJi + "'" + "," +
                "'" + teacherId+ "'" + "," +
                "'" + className + "'" + "," +
                      classHour + "," +
                      workLoad_ + "," +
                      number +  "," +
                "'" + term + "'" + "," +
                      coef +  "," +
                "'" + director + "'" + "," +
                "'" + validTime + "'" + "," +
                "'" + remark + "'" + "," +
                "'" + submitTime + "'" + "," +
                      pass1 + "," +
                      pass2 + "," +
                      pass3 +
                ")";

            MessageBox.Show(sql);
            SQLHelper helper = new SQLHelper();
            helper.ExecuteNonQueryCount(sql);


            MessageBox.Show("YES");
        }

        private void toolStripLabel4_Click(object sender, EventArgs e)
        {
            MessageBox.Show("填写该统计表请参照" +
                "《玉林师范学院教学工作量计算表》：\n" +
                "1、系数没有增加就填原系数，增加了就填写增加后的系数,详细数目请查阅表格！\n" +
                "2、项目请参照表格填写，系数必须与项目保持一致！\n" +
                "3、教研室主任该项，请填写审核统计表的教研室主任的名字\n" +
                "4、班级人数、计划课时等只需填写数字不需要写单位\n" +
                "5、工作量和系数必须为数字类型，不能带有字符\n" +
                "6、非必填项如若没有可以不填！");
        }

        private void toolStripLabel2_Click(object sender, EventArgs e)
        {
            tb1.Text = null;
            tb2.Text = null;
            tb3.Text = null;
            tb4.Text = null;
            tb5.Text = null;
            tb6.Text = null;
            tb7.Text = null;
            cm8.Text = null;
            tb12.Text = null;
            tb13.Text = null;
            tb14.Text = null;

        }
        //快捷键的设置
        private void TS_KeyDown(object sender, KeyEventArgs e)
        {
             if(e.Modifiers==Keys.Control)
            {
                switch(e.KeyCode)
                {
                    case Keys.S:
                        toolStripLabel1_Click(this, EventArgs.Empty);
                        break;
                    case Keys.D:
                        toolStripLabel2_Click(this, EventArgs.Empty);
                        break;
                    case Keys.E:
                        toolStripLabel3_Click(this, EventArgs.Empty);
                        break;
                    case Keys.A:
                        toolStripLabel4_Click(this, EventArgs.Empty);
                        break;
                }
            }
        }

        private void dtp9_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
