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
    public partial class keYan : Form
    {
        public keYan()
        {
            InitializeComponent();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void toolStripLabel3_Click(object sender, EventArgs e)
        {

        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {
            string name = textBox4.Text.Trim();
            string proj = textBox1.Text.Trim();
            string workLoad = textBox2.Text.Trim();
            string date = dtp9.Text.Trim();
            string remark = textBox3.Text.Trim();
            string tableId = tableId = "C" + DateTime.Now.ToString("yyyyMMddHHmmss", System.Globalization.DateTimeFormatInfo.InvariantInfo);

            string sql = "insert into scientific values(" +
                "'" +tableId+ "'" +","+
                "'" +Program.mainUser+ "'" + "," +
                "'" +name+ "'" + "," +
                "'" +proj+ "'" + "," +
                "'" +workLoad+ "'" + "," +
                "'" +date+ "'" + "," +
                "'" +remark+ "'" +
                ")";
            SQLHelper helper = new SQLHelper();
            MessageBox.Show(sql);
            helper.SqlServerRecordCount(sql);
            MessageBox.Show("已成功保存数据！");
            toolStripLabel2.PerformClick();


        }

        private void keYan_Load(object sender, EventArgs e)
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
            textBox3.Text = null;
        }
    }
}
