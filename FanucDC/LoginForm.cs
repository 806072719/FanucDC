using FanucDC.db;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace FanucDC
{
    public partial class LoginForm : Form
    {
        // 私有化构造辅助，避免重复创建
        private static readonly MD5 _md5Provider = MD5.Create();

        public LoginForm()
        {
            InitializeComponent();
            // 初始化数据库连接池（启用，提升首次查询速度）
            SqlServerPool.initConnectionPool();
            // 固定窗体大小，禁止最大化
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            // 窗体居中显示
            this.StartPosition = FormStartPosition.CenterScreen;
            // 密码框默认密文显示
            passwordText.PasswordChar = '*';
        }

        /// <summary>
        /// 登录按钮点击事件（核心逻辑）
        /// </summary>
        private void button2_Click(object sender, EventArgs e)
        {
            // 1. 获取并清洗输入数据
            string username = usernameText.Text.Trim();
            string password = passwordText.Text.Trim();

            // 2. 非空校验（简化代码，友好提示+焦点定位）
            if (string.IsNullOrEmpty(username))
            {
                ShowErrorTip("用户名不能为空！");
                usernameText.Focus();
                return;
            }
            if (string.IsNullOrEmpty(password))
            {
                ShowErrorTip("密码不能为空！");
                passwordText.Focus();
                return;
            }

            try
            {
                // 3. 参数化查询用户信息（彻底防SQL注入，适配改造后的SqlServerPool）
                string querySql = "SELECT password FROM _user_info WHERE username = @Username";
                var paramDict = new Dictionary<string, object>
                {
                    { "@Username", username }
                };
                var result = SqlServerPool.ExecuteQuery(querySql, paramDict);

                // 4. 用户名不存在校验
                if (result == null || result.Length == 0)
                {
                    ShowErrorTip("用户名不存在，请检查输入！");
                    usernameText.SelectAll();// 选中输入框内容，方便用户修改
                    return;
                }

                // 5. 密码加密（修复原ASCII编码Bug，改用UTF8，适配绝大多数场景）
                string encryptPwd = MD5Encrypt(password);
                // 6. 数据库密码获取（处理DBNull，避免空引用）
                string dbPwd = result[0] == DBNull.Value ? string.Empty : result[0].ToString().Trim();

                // 7. 密码校验
                if (encryptPwd.Equals(dbPwd, StringComparison.OrdinalIgnoreCase))
                {
                    // 登录成功 - 隐藏登录窗体，打开主窗体
                    LoginSuccessHandler();
                }
                else
                {
                    ShowErrorTip("密码错误，请重新输入！");
                    passwordText.SelectAll();
                    passwordText.Focus();
                }
            }
            catch (Exception ex)
            {
                // 全局异常捕获，避免程序崩溃，便于问题排查
                ShowErrorTip($"登录失败：{ex.Message}");
            }
        }

        #region 私有工具方法（简化重复代码，提升可读性）
        /// <summary>
        /// 统一显示错误提示框
        /// </summary>
        private void ShowErrorTip(string tipText)
        {
            MessageBox.Show(tipText, "登录异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// 登录成功处理逻辑
        /// </summary>
        private void LoginSuccessHandler()
        {
            // 打开主窗体，居中显示
            using (IndexForm mainForm = new IndexForm())
            {
                mainForm.StartPosition = FormStartPosition.CenterScreen;
                // 隐藏登录窗体
                this.Hide();
                // 显示主窗体，关闭后主窗体释放资源
                mainForm.ShowDialog();
            }
            // 主窗体关闭后，彻底关闭登录窗体
            this.Close();
        }

        /// <summary>
        /// MD5加密（修复原ASCII编码Bug，改用UTF8，静态复用MD5对象）
        /// </summary>
        /// <param name="str">需要加密的字符串</param>
        /// <returns>32位小写MD5加密字符串</returns>
        private string MD5Encrypt(string str)
        {
            if (string.IsNullOrEmpty(str)) return string.Empty;

            // 改用UTF8编码，修复原ASCII编码对中文/特殊字符加密不一致的问题
            byte[] inputBytes = Encoding.UTF8.GetBytes(str);
            // 复用MD5对象，提升性能
            byte[] hashBytes = _md5Provider.ComputeHash(inputBytes);

            // 字节数组转32位小写MD5字符串
            StringBuilder sb = new StringBuilder();
            foreach (byte b in hashBytes)
            {
                sb.Append(b.ToString("x2")); // 小写x2，直接生成小写，无需后续ToLower()
            }
            return sb.ToString();
        }
        #endregion

        #region 无用事件清理（移除空方法，精简代码）
        private void usernameLabel_Click(object sender, EventArgs e) { }
        private void timer1_Tick(object sender, EventArgs e) { }
        #endregion

        // 修复：移除重复的 Dispose 方法，改用 FormClosing 事件释放 MD5 资源
        private void LoginForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // 释放 MD5 资源，避免非托管资源泄漏
            _md5Provider?.Dispose();
        }
    }
}