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
    public partial class infoQuerAndAlter : Form
    {
        public infoQuerAndAlter()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
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
                    this.cm.Items.Add(i + "-" + xueqi);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string prj = null;
            string className = null;
            string term = null;
            string startData = null;
            string endData = null;

            prj = tb1.Text.Trim();
            className = tb2.Text.Trim();
            term = cm.Text.Trim();
            startData = dtp1.Text.Trim();
            endData = dtp2.Text.Trim();
            
            string sqlCommend=null;
            //根据所填查询项 自动生成查询语句
            if (!string.IsNullOrEmpty(prj))
            {
                sqlCommend += " and "+"proj="+"'"+prj+"'";
            }
            if (!string.IsNullOrEmpty(className))
            {
                sqlCommend += " and " + "className=" + "'" + className + "'";
            }
            if (!string.IsNullOrEmpty(term))
            {
                sqlCommend+= " and " + "term=" + "'" + term + "'";
            }

            sqlCommend = "select * from TeachWork where teacherId=" +
                "'" + Program.mainUser + "'" + " and " +
                "submitTime>=" + "'" + startData + "'" + " and submitTime<=" +
                "'" + endData + "'"+sqlCommend;
            //MessageBox.Show(sqlCommend);
            SQLHelper helper = new SQLHelper();
            DataSet ds = helper.GetDataSetValue(sqlCommend,"TeacherWork");
            dataGridView1.DataSource = ds.Tables["TeacherWork"];

            //计算总的工作量
            DataTable dt=ds.Tables["TeacherWork"];
            int rowCount = dt.Rows.Count;
            double allWorkLoad=0;
            for(int i=0;i<rowCount;i++)
            {
                string work = dt.Rows[i][6].ToString();
                string cofe = dt.Rows[i][9].ToString();
                allWorkLoad += double.Parse(work) * double.Parse(cofe);
            }

            this.label5.Text = "满足查询的总工作量为：" + allWorkLoad;

            //将一些不允许更改的选项冻结
            dataGridView1.Columns[0].ReadOnly = true;
            dataGridView1.Columns[3].ReadOnly = true;
            dataGridView1.Columns[11].ReadOnly = true;
            dataGridView1.Columns[13].ReadOnly = true;
            dataGridView1.Columns[14].ReadOnly = true;
            dataGridView1.Columns[15].ReadOnly = true;
            dataGridView1.Columns[16].ReadOnly = true;
        }

        private void dataGridView1_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            //右键执行这些任务
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

        private void 删除行ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //获取当前所选数据行的索引
            int row = dataGridView1.CurrentCell.RowIndex;
            //获取tableId 进行索引删除
            string tableId = dataGridView1.Rows[row].Cells[0].Value.ToString();

            DialogResult dr = MessageBox.Show("确认要删除该行记录？", "确认删除？", MessageBoxButtons.OKCancel
                , MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            if (dr == DialogResult.Cancel)
                return;
            
            string sql = "delete TeachWork where tableId='"+tableId+"'";
            SQLHelper helper = new SQLHelper();
            int count=helper.ExecuteNonQueryCount(sql);
            MessageBox.Show("已删除"+count+"一行数据！");
            //刷新表显示
            button1.PerformClick();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            string strcolumn = dataGridView1.Columns[e.ColumnIndex].HeaderText;//获取列标题
            string strrow = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();//获取焦点触发行的第一个值
            string value = dataGridView1.CurrentCell.Value.ToString();//获取当前点击的活动单元格的值

            String sql = "update TeachWork set " + strcolumn + " ='" + value + "'" +
                " where tableId="+"'"+strrow+"'";
            //MessageBox.Show(sql);
            SQLHelper helper = new SQLHelper();
            int count =helper.ExecuteNonQueryCount(sql);
            if(count>0)
            {
                MessageBox.Show("修改成功！");
            }

        }

        private void infoQuerAndAlter_Load(object sender, EventArgs e)
        {
           
        }
    }
}
