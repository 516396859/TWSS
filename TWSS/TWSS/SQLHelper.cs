using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.IO;
using System.Net.Mail;
 

namespace TWSS
{
    class SQLHelper
    {
        ///私有属性:数据库连接字符串
        ///Data Source=(Local)          服务器地址
        ///Initial Catalog=SimpleMESDB  数据库名称
        ///User ID=sa                   数据库用户名
        ///Password=admin123456         数据库密码
      
        private const string connectionString = "" +
            "Data Source=111.230.45.252;Pooling=False;" +
            "Max Pool Size = 1024;" +
            "Initial Catalog=TWSSDat;" +
            "Persist Security Info=True;" +
            "User ID=sa;" +
            "Password=chenjie151525$";

        public void SqlHelp()
        {
            //无参构造函数
        }
        //连接实例
        public static SqlConnection conn;
        //打开数据库连接
        public static void OpenConn()
        {
            string SqlCon = connectionString;//数据库连接字符串
            conn = new SqlConnection(SqlCon);
            if (conn.State.ToString().ToLower() == "open")
            {
                //已经打开了
            }
            else
            {
                conn.Open();
            }
        }
        //关闭数据库连接
        public static void CloseConn()
        {
            if (conn.State.ToString().ToLower() == "open")
            {
                //连接打开时
                conn.Close();
                conn.Dispose();
            }
        }
        // 读取数据  返回  DataReader 对象
        public static SqlDataReader GetDataReaderValue(string sql)
        {
            OpenConn();
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader dr = cmd.ExecuteReader();
            CloseConn();
            return dr;
        }
        // 返回DataSet
        public DataSet GetDataSetValue(string sql, string tableName)
        {
            OpenConn();
            SqlDataAdapter da;
            DataSet ds = new DataSet();
            da = new SqlDataAdapter(sql, conn);
            da.Fill(ds, tableName);
            CloseConn();
            return ds;
        }
        //  返回DataView
        public DataView GetDataViewValue(string sql)
        {
            OpenConn();
            SqlDataAdapter da;
            DataSet ds = new DataSet();
            da = new SqlDataAdapter(sql, conn);
            da.Fill(ds, "temp");
            CloseConn();
            return ds.Tables[0].DefaultView;
        }

        // 返回DataTable
        public DataTable GetDataTableValue(string sql)
        {
            OpenConn();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(sql, conn);
            da.Fill(dt);
            CloseConn();
            return dt;
        }

        // 执行一个SQL操作:添加、删除、更新操作
        public void ExecuteNonQuery(string sql)
        {
            OpenConn();
            SqlCommand cmd;
            cmd = new SqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            CloseConn();
        }

        // 执行一个SQL操作:添加、删除、更新操作，返回受影响的行
        public int ExecuteNonQueryCount(string sql)
        {
            OpenConn();
            SqlCommand cmd;
            cmd = new SqlCommand(sql, conn);
            int value = cmd.ExecuteNonQuery();
            return value;
        }

        //执行一条返回第一条记录第一列的SqlCommand命令
        public object ExecuteScalar(string sql)
        {
            OpenConn();
            SqlCommand cmd;
            cmd = new SqlCommand(sql, conn);
            object value = cmd.ExecuteScalar();
            return value;
        }

        
        // 返回记录数
        public int SqlServerRecordCount(string sql)
        {
            OpenConn();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = sql;
            cmd.Connection = conn;
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            int RecordCount = 0;
            while (dr.Read())
            {
                RecordCount = RecordCount + 1;
            }
            CloseConn();
            return RecordCount;
        }
        


        ///<summary>
        ///一些常用的函数
        ///</summary>

        //判断是否为数字
        public static bool IsNumber(string a)
        {
            if (string.IsNullOrEmpty(a))
                return false;
            foreach (char c in a)
            {
                if (!char.IsDigit(c))
                    return false;
            }
            return true;
        }
    }
}
