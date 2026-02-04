using FanucDC.db;
using FanucDC.Models;
using System.Text.RegularExpressions;

namespace FanucDC
{
    public partial class AddForm : Form
    {
        // 标识是否保存成功
        public bool isOk = false;
        // 保存新增的设备信息
        public Equipment equipment = null;

        // IP正则表达式（提取为只读字段，避免重复创建）
        private static readonly Regex _ipRegex = new Regex(@"^((25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$");

        public AddForm()
        {
            InitializeComponent();
            // 优化：设置端口默认值，提升用户体验
            portText.Text = "8080";
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            isOk = false;
            this.Close();
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            // 1. 获取并校验输入框内容（统一trim，避免空格问题）
            string equipmentCode = codeText.Text.Trim();
            string equipmentName = nameText.Text.Trim();
            string equipmentIp = ipText.Text.Trim();
            string equipmentPortStr = portText.Text.Trim();

            // 2. 非空校验（简化代码，减少重复判断）
            if (string.IsNullOrEmpty(equipmentCode))
            {
                ShowErrorTip("设备代码不能为空！");
                codeText.Focus(); // 定位到错误输入框，提升体验
                return;
            }
            if (string.IsNullOrEmpty(equipmentName))
            {
                ShowErrorTip("设备名称不能为空！");
                nameText.Focus();
                return;
            }
            if (string.IsNullOrEmpty(equipmentIp))
            {
                ShowErrorTip("设备IP不能为空！");
                ipText.Focus();
                return;
            }
            if (string.IsNullOrEmpty(equipmentPortStr))
            {
                ShowErrorTip("设备端口不能为空！");
                portText.Focus();
                return;
            }

            // 3. IP格式校验
            if (!_ipRegex.IsMatch(equipmentIp))
            {
                ShowErrorTip("设备IP格式错误，请输入合法的IPv4地址！");
                ipText.Focus();
                return;
            }

            // 4. 端口号格式校验+转换（关键：处理非数字/超出short范围的情况）
            if (!short.TryParse(equipmentPortStr, out short equipmentPort) || equipmentPort <= 0 || equipmentPort > 65535)
            {
                ShowErrorTip("设备端口格式错误，请输入1-65535之间的数字！");
                portText.Focus();
                return;
            }

            try
            {
                // 5. 校验设备代码/名称/IP是否重复（修复笔误+参数化查询，防注入）
                if (IsEquipmentExist("equipment_code", equipmentCode))
                {
                    ShowErrorTip("设备代码已存在，请勿重复添加！");
                    codeText.Focus();
                    return;
                }
                if (IsEquipmentExist("equipment_name", equipmentName))
                {
                    ShowErrorTip("设备名称已存在，请勿重复添加！");
                    nameText.Focus();
                    return;
                }
                if (IsEquipmentExist("equipment_ip", equipmentIp))
                {
                    ShowErrorTip("设备IP已存在，请勿重复添加！");
                    ipText.Focus();
                    return;
                }

                // 6. 新增设备（参数化插入，彻底杜绝SQL注入，适配改造后的SqlServerPool）
                string insertSql = "INSERT INTO dbo._equipment_info (equipment_code,equipment_name,equipment_ip,equipment_port) " +
                                   "VALUES (@EquipmentCode, @EquipmentName, @EquipmentIp, @EquipmentPort)";
                var paramDict = new Dictionary<string, object>()
                {
                    { "@EquipmentCode", equipmentCode },
                    { "@EquipmentName", equipmentName },
                    { "@EquipmentIp", equipmentIp },
                    { "@EquipmentPort", equipmentPort }
                };
                // 执行插入
                SqlServerPool.ExecuteNonQuery(insertSql, paramDict);

                // 7. 设置返回结果
                isOk = true;
                equipment = new Equipment()
                {
                    Code = equipmentCode,
                    Name = equipmentName,
                    Ip = equipmentIp,
                    Port = equipmentPort
                };

                // 提示成功并关闭窗体
                MessageBox.Show("设备新增成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                // 全局异常捕获，避免程序崩溃，提示具体错误
                ShowErrorTip($"设备新增失败：{ex.Message}");
            }
        }

        #region 私有工具方法（简化重复代码，提升可读性）
        /// <summary>
        /// 统一显示错误提示框
        /// </summary>
        /// <param name="tipText">错误信息</param>
        private void ShowErrorTip(string tipText)
        {
            MessageBox.Show(tipText, "操作异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// 校验设备指定字段是否已存在（参数化查询，通用方法）
        /// </summary>
        /// <param name="fieldName">数据库字段名</param>
        /// <param name="fieldValue">字段值</param>
        /// <returns>是否存在</returns>
        private bool IsEquipmentExist(string fieldName, string fieldValue)
        {
            // 参数化查询，避免SQL注入，同时适配字段通用判断
            string querySql = $"SELECT 1 FROM _equipment_info WHERE {fieldName} = @FieldValue";
            var paramDict = new Dictionary<string, object>()
            {
                { "@FieldValue", fieldValue }
            };
            // 执行查询，结果不为null则表示已存在
            var result = SqlServerPool.ExecuteQuery(querySql, paramDict);
            return result != null;
        }
        #endregion

        // 优化：窗体关闭时重置状态
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            if (!isOk)
            {
                equipment = null;
            }
        }
    }
}