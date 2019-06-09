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
    public partial class check : Form
    {
        public check()
        {
            InitializeComponent();
        }

        private void check_Load(object sender, EventArgs e)
        {
            this.radioButton1.Checked = true;
        }

        //这是用户的权限登记，对应数据库的grade ， pass1,pass2.pass3
        string grade = null;
        private void button1_Click(object sender, EventArgs e)
        {
            string name = null;
            string id = null;
            string isCheck = "0";
            
            string myName = null;
            
            
            //获取用户输入字段
            name = tb2.Text.Trim();
            id = tb1.Text.Trim();
            

            SQLHelper helper = new SQLHelper();
            string sql_grade = "select grade from superManag where id='" + Program.mainUser + "'";
            grade = helper.ExecuteScalar(sql_grade).ToString();
            string sql_myName= "select name from userTable where id='" + Program.mainUser + "'";
            myName = helper.ExecuteScalar(sql_myName).ToString();
            //判断是否输入合法
            if (string.IsNullOrEmpty(name) && string.IsNullOrEmpty(id))
            {
                MessageBox.Show("教师姓名和编号必须要填一个！");
                return;
            }
            //单选框赋值
            if (radioButton1.Checked == true)
                isCheck = "0";
            else
                isCheck = grade;
            //生成sql查询语句

            if (!string.IsNullOrEmpty(name))
            {
                //寻找该名字的teacherId
                string sql = "select id from userTable where name='" + name + "'";
                id=helper.ExecuteScalar(sql).ToString();
                //MessageBox.Show(id);
                
            }
            if (string.IsNullOrEmpty(name))
            {
                id = tb1.Text.Trim();
            }

            string sqlCommend = "select * from TeachWork where pass" + grade + "='" +
                isCheck + "'" + " and director='" + myName + "' and teacherId='"+id+"'";
            //MessageBox.Show(sqlCommend);
            DataSet ds = helper.GetDataSetValue(sqlCommend, "superManag");
            dataGridView1.DataSource = ds.Tables["superManag"];
            dataGridView1.ReadOnly = true;


        }

        private void dataGridView1_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            //右键显示这些
            if (e.Button == MouseButtons.Right)
            {
                if (e.RowIndex >= 0)
                {
                    this.dataGridView1.Rows[e.RowIndex].Selected = true;//是否选中当前行
                    int index = e.RowIndex;
                    this.dataGridView1.CurrentCell = this.dataGridView1.Rows[e.RowIndex].Cells[0];

                    //每次选中行都刷新到datagridview中的活动单元格
                    this.contextMenuStrip1.Show(this.dataGridView1, e.Location);
                    //指定控件（DataGridView），指定位置（鼠标指定位置）
                    this.contextMenuStrip1.Show(Cursor.Position);//锁定右键列表出现的位置

                }
            }
        }

        private void 审核通过ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //获取高亮所选的行
            int row = dataGridView1.CurrentCell.RowIndex;
            //获取tableId 进行索引删除
            string tableId = dataGridView1.Rows[row].Cells[0].Value.ToString();
            int find = int.Parse(grade);
            string gra= dataGridView1.Rows[row].Cells[13+find].Value.ToString();

            if (!gra.Equals("0"))
            {
                MessageBox.Show("已经通过审核！不需要重复审核！");
                return;
            }

            string updataSql = "update TeachWork set pass" + grade+"='"+grade+"'"+
                "  where tableId='"+tableId+"'";
            //MessageBox.Show(updataSql);
            SQLHelper helper = new SQLHelper();
            helper.ExecuteNonQueryCount(updataSql);
            MessageBox.Show("  通过审核！");
            button1.PerformClick();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
