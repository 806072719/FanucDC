using FanucDC.db;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FanucDC
{
    public partial class LoginForm : Form
    {


        public LoginForm()
        {
            InitializeComponent();
            initPool();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        private void initPool()
        {
            //SqlServerPool.initConnectionPool();
        }



        //private void loginBtn_Click(object sender, EventArgs e)
        //{
        //    string username = usernameText.Text;
        //    if (username == null || "".Equals(username))
        //    {
        //        MessageBox.Show("用户名不能为空", "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        return;
        //    }
        //    string password = passwordText.Text;
        //    if (password == null || "".Equals(password))
        //    {
        //        MessageBox.Show("密码不能为空", "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        return;
        //    }

        //    if ("jcjm".Equals(username) && "jm1234".Equals(password))
        //    {
        //        MainForm ui = new MainForm();
        //        this.Visible = false;
        //        ui.ShowDialog();//此处不可用Show()
        //        this.Dispose();
        //        this.Close();
        //        //this.Close();
        //    }
        //    else
        //    {
        //        MessageBox.Show("用户名密码错误", "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}



        private void usernameLabel_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string username = usernameText.Text;
            if (username == null || "".Equals(username))
            {
                MessageBox.Show("用户名不能为空", "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string password = passwordText.Text;
            if (password == null || "".Equals(password))
            {
                MessageBox.Show("密码不能为空", "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }



            string querySql = "select username,password from _user_info where username = '" + username + "'";

            var result = SqlServerPool.ExecuteQuery(querySql);

            if (result == null || result.Length == 0)
            {
                MessageBox.Show("用户名不存在", "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                // pAsswo0rd
                string orp = MD5(password.Trim());
                var uname = result.GetValue(0);
                var pwd = result.GetValue(1);
                if (orp.Equals(pwd.ToString()))
                {
                    IndexForm ui = new IndexForm();
                    ui.StartPosition = FormStartPosition.CenterParent;
                    this.Visible = false;
                    ui.ShowDialog();
                    this.Dispose();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("密码错误" + orp + " " + pwd, "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }



        public static string MD5(string str)
        {
            // 1 创建MD5对象 c#提供了Cryptography类库中md5类生产md5对象
            // Security 安全
            MD5 md5 = System.Security.Cryptography.MD5.Create();

            // 2 使用md5对象对i1进行加密处理  编码之后的字节数组。
            byte[] bs = System.Text.Encoding.ASCII.GetBytes(str);

            // 3 把字节数组通过md5进行加密ComputeHash
            // Compute 计算
            // Hash 哈希函数 
            // y = x +1  x=0
            byte[] hashS = md5.ComputeHash(bs);

            //4 把数组转成字符串
            StringBuilder s = new StringBuilder();//可变字符串
            for (int i = 0; i < hashS.Length; i++)
            {   //16进制0-15 ，0-9还是数字，
                //ToString("X2")
                //X代表是16进制，大写的X是代表大写16进制的
                //小写的x代表的小写的16进制
                //2  不足俩位的前面补0
                //例如0x0A，如果没有2. 输出结果是0xA
                s.Append(hashS[i].ToString("X2"));  //追加字符串 ，等同于+拼接字符串
            }
            return s.ToString().ToLower();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }
    }
}
