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
    public partial class CT : Form
    {
        public CT()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void toolStripLabel4_Click(object sender, EventArgs e)
        {
            MessageBox.Show("1、其他教师尽量填全，可空，不够填写可以在备注补充\n"
                +"2、");
        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {
            string proj = textBox1.Text.Trim();
            string workLoad = textBox2.Text.Trim();
            string date = dtp9.Text.Trim();
            string t1 = "";
            string t2 = "";
            string t3 = "";
            string t4 = "";
            t1 = textBox5.Text.Trim();
            t2 = textBox6.Text.Trim();
            t3 = textBox7.Text.Trim();
            t4 = textBox8.Text.Trim();
            string remark = textBox3.Text.Trim();
            string name = textBox4.Text.Trim();
            string tableId= tableId = "C" + DateTime.Now.ToString("yyyyMMddHHmmss", System.Globalization.DateTimeFormatInfo.InvariantInfo);
            //判断proj是否为空
            if (string.IsNullOrEmpty(proj) == true)
            {
                MessageBox.Show("项目不允许为空！");
                return;
            }
            //工作量不允许为空
            if (string.IsNullOrEmpty(workLoad) == true)
            {
                MessageBox.Show("工作量不允许为空！");
                return;
            }
            //日期不允许为空
            if (string.IsNullOrEmpty(date) == true)
            {
                MessageBox.Show("有效日期不允许为空！");
                return;
            }
            //将数据插入数据库
            string sql = "insert into competition values (" +
                "'" +tableId+ "'" +","+
                "'" +Program.mainUser+ "'" + "," +
                "'" +name+ "'" + "," +
                "'" +proj+ "'" + "," +
                "'" +workLoad+ "'" + "," +
                "'" +date+ "'" + "," +
                "'" +t1+ "'" + "," +
                "'" +t2+ "'" + "," +
                "'" +t3+ "'" + "," +
                "'" +t4+ "'" +","+
                "'"+remark+"'"+
                ") ";
            MessageBox.Show(sql);
            SQLHelper helper = new SQLHelper();
            helper.SqlServerRecordCount(sql);
            MessageBox.Show("已成功保存数据！");
            toolStripLabel2.PerformClick();
            
        }

        private void CT_Load(object sender, EventArgs e)
        {
            string name = "";
            string sql = "select name from userTable where id='" 
                + Program.mainUser + "'";
            SQLHelper helper = new SQLHelper();
            name = helper.ExecuteScalar(sql).ToString();
            textBox4.Text = name;
        }

        private void toolStripLabel2_Click(object sender, EventArgs e)
        {
            
            textBox1.Text = null;
            textBox2.Text = null;
            textBox5.Text = null;
            textBox6.Text = null;
            textBox7.Text = null;
            textBox8.Text = null;
            textBox3.Text = null;

        }

        private void toolStripLabel3_Click(object sender, EventArgs e)
        {

        }
    }
}
