using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TWSS
{
    static class Program
    {
        public static string mainUser;
        public static login log;
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            //一个系统只需要一个登录实例
            log = new login();
            
            while (true){
                //显示登录窗体
                log.ShowDialog();
                //判断登录界面给通过了没有，运行主系统
                if (log.DialogResult == DialogResult.OK)
                {
                    MainForm mf = new MainForm(mainUser);
                    Application.Run(mf);
                }
                else if (log.DialogResult == DialogResult.Cancel)
                {
                    log.Dispose();
                    return;
                }
            }
        }
    }
}
