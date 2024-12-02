using FanucDC.db;
using FanucDC.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace FanucDC
{
    public partial class AddForm : Form
    {
        public bool isOk = false;

        public Equipment equipment = null;


        static string pattern = @"^((25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$";

        public AddForm()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
           String equipmentCode =  codeText.Text.Trim();
            if (equipmentCode == null || "".Equals(equipmentCode)) {
                MessageBox.Show("设备代码不能为空", "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
           String equipmentName = nameText.Text.Trim();
            if (equipmentName == null || "".Equals(equipmentName))
            {
                MessageBox.Show("设备名称不能为空", "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            String equipmentIp = ipText.Text.Trim();
            if (equipmentIp == null || "".Equals(equipmentIp))
            {
                MessageBox.Show("设备IP不能为空", "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Regex regex = new Regex(pattern);
            if (!regex.IsMatch(equipmentIp))
            {
                MessageBox.Show("设备IP格式异常", "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            String equipmentPort = portText.Text.Trim();
            if (equipmentPort == null || "".Equals(equipmentPort))
            {
                MessageBox.Show("设备端口不能为空", "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string querySql = "select 1 from _equipment_info where equipment_code = '" + equipmentCode + "'";
            var result = SqlServerPool.ExecuteQuery(querySql);
            if (result != null) {
                MessageBox.Show("设备代码已存在", "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string querySql2 = "select 1 from _equipment_info where equipment_name = '" + equipmentName + "'";
            var result2 = SqlServerPool.ExecuteQuery(querySql);
            if (result2 != null)
            {
                MessageBox.Show("设备名称已存在", "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            string querySql3 = "select 1 from _equipment_info where equipment_ip = '" + equipmentIp + "'";
            var result3 = SqlServerPool.ExecuteQuery(querySql);
            if (result3 != null)
            {
                MessageBox.Show("设备IP已存在", "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            string sql = " insert into dbo._equipment_info(equipment_code,equipment_name,equipment_ip,equipment_port) values('{0}','{1}','{2}','{3}')";
            string exec = string.Format(sql, equipmentCode, equipmentName, equipmentIp, equipmentPort);

            //MessageBox.Show(exec, "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
            SqlServerPool.ExecuteNonQuery(exec);
            isOk = true;
            equipment = new Equipment();
            equipment.Code = equipmentCode;
            equipment.Name = equipmentName;
            equipment.Ip = equipmentIp;
            equipment.Port = short.Parse(equipmentPort);

            this.Close();
        }
  
    }
}
