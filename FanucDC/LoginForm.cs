using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void loginBtn_Click(object sender, EventArgs e)
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

            if ("jcjm".Equals(username) && "jm1234".Equals(password))
            {
                MainForm mainForm = new MainForm();
                mainForm.ShowDialog();
                this.Close();
                //this.Close();
            } else
            {
                MessageBox.Show("用户名密码错误", "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
