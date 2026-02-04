using System.Data.SqlClient;
using System.Data;
using System.Reflection;
using System.Collections.Generic;
using System;
// 新增：引入配置文件读取命名空间
using System.Configuration;

namespace FanucDC.db
{
    internal class SqlServerPool
    {
        #region 从配置文件读取数据库连接字符串（核心改造：移除硬编码）
        // 静态只读连接串，程序启动时读取一次，保证性能
        private static readonly string connectionString;

        // 静态构造函数：程序首次使用此类时执行，读取配置文件
        static SqlServerPool()
        {
            try
            {
                // 读取App.config中name="FanucDCConn"的连接串
                ConnectionStringSettings connSetting = ConfigurationManager.ConnectionStrings["FanucDCConn"];
                if (connSetting == null || string.IsNullOrEmpty(connSetting.ConnectionString))
                {
                    throw new ConfigurationErrorsException("配置文件App.config中未找到名称为FanucDCConn的数据库连接配置，或连接字符串为空！");
                }
                // 赋值给全局连接串
                connectionString = connSetting.ConnectionString;
            }
            catch (Exception ex)
            {
                throw new Exception("数据库连接配置读取失败，请检查App.config的connectionStrings节点！", ex);
            }
        }
        #endregion

        #region 初始化连接池（保留原逻辑，无修改）
        public static void initConnectionPool()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
            }
        }
        #endregion

        #region 无参数基础方法（兼容原有代码，内部调用参数化重载，无修改）
        public static int ExecuteNonQuery(string cmdText)
        {
            return ExecuteNonQuery(cmdText, null);
        }

        public static DataTable ExecuteDataTable(string cmdText)
        {
            return ExecuteDataTable(cmdText, null);
        }

        public static object[] ExecuteQuery(string cmdText)
        {
            return ExecuteQuery(cmdText, null);
        }

        public static List<object[]> ExecuteQueryList(string cmdText)
        {
            return ExecuteQueryList(cmdText, null);
        }

        public static List<T> QueryList<T>(string cmdText)
        {
            return QueryList<T>(cmdText, null);
        }
        #endregion

        #region 核心参数化方法（适配IndexForm，防SQL注入，无修改）
        public static int ExecuteNonQuery(string cmdText, Dictionary<string, object> parameters)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(cmdText, conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandTimeout = 30;
                        AddSqlParameters(cmd, parameters);

                        return cmd.ExecuteNonQuery();
                    }
                }
                catch (SqlException ex)
                {
                    throw new Exception($"SQL非查询执行失败：{cmdText}，异常信息：{ex.Message}", ex);
                }
            }
        }

        public static DataTable ExecuteDataTable(string cmdText, Dictionary<string, object> parameters)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(cmdText, conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandTimeout = 30;
                        AddSqlParameters(cmd, parameters);

                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                        {
                            sda.Fill(dt);
                        }
                    }
                }
                catch (SqlException ex)
                {
                    throw new Exception($"SQL查询DataTable失败：{cmdText}，异常信息：{ex.Message}", ex);
                }
            }
            return dt;
        }

        public static object[] ExecuteQuery(string cmdText, Dictionary<string, object> parameters)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(cmdText, conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandTimeout = 30;
                        AddSqlParameters(cmd, parameters);

                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if (sdr.Read())
                            {
                                object[] rowData = new object[sdr.FieldCount];
                                sdr.GetSqlValues(rowData);
                                return rowData;
                            }
                        }
                    }
                }
                catch (SqlException ex)
                {
                    throw new Exception($"SQL查询单行数据失败：{cmdText}，异常信息：{ex.Message}", ex);
                }
            }
            return null;
        }

        public static List<object[]> ExecuteQueryList(string cmdText, Dictionary<string, object> parameters)
        {
            List<object[]> result = new List<object[]>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(cmdText, conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandTimeout = 30;
                        AddSqlParameters(cmd, parameters);

                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            while (sdr.Read())
                            {
                                object[] rowData = new object[sdr.FieldCount];
                                sdr.GetSqlValues(rowData);
                                result.Add(rowData);
                            }
                        }
                    }
                }
                catch (SqlException ex)
                {
                    throw new Exception($"SQL查询多行数据失败：{cmdText}，异常信息：{ex.Message}", ex);
                }
            }
            return result;
        }

        /// <summary>
        /// 实体查询（彻底移除扩展方法，用原生代码判断列名是否存在）
        /// </summary>
        public static List<T> QueryList<T>(string cmdText, Dictionary<string, object> parameters)
        {
            var result = new List<T>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(cmdText, conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandTimeout = 30;
                        AddSqlParameters(cmd, parameters);

                        using (var reader = cmd.ExecuteReader())
                        {
                            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
                            while (reader.Read())
                            {
                                var item = Activator.CreateInstance<T>();
                                foreach (var property in properties)
                                {
                                    try
                                    {
                                        // 核心修复：原生代码判断列名是否存在
                                        bool isColumnExist = false;
                                        for (int i = 0; i < reader.FieldCount; i++)
                                        {
                                            if (reader.GetName(i).Equals(property.Name, StringComparison.OrdinalIgnoreCase))
                                            {
                                                isColumnExist = true;
                                                break;
                                            }
                                        }
                                        if (!isColumnExist) continue; // 列名不存在直接跳过

                                        var ordinal = reader.GetOrdinal(property.Name);
                                        if (reader.GetValue(ordinal) != DBNull.Value)
                                        {
                                            // 处理可空类型转换
                                            var targetType = GetUnderlyingType(property.PropertyType);
                                            var value = Convert.ChangeType(reader.GetValue(ordinal), targetType);
                                            property.SetValue(item, value, null);
                                        }
                                    }
                                    catch (Exception)
                                    {
                                        continue; // 单个属性赋值失败不影响整体
                                    }
                                }
                                result.Add(item);
                            }
                        }
                    }
                }
                catch (SqlException ex)
                {
                    throw new Exception($"SQL实体查询失败：{cmdText}，异常信息：{ex.Message}", ex);
                }
            }
            return result;
        }
        #endregion

        #region 私有工具方法（无修改）
        /// <summary>
        /// 给SqlCommand添加参数，自动处理null值
        /// </summary>
        private static void AddSqlParameters(SqlCommand cmd, Dictionary<string, object> parameters)
        {
            if (parameters == null || parameters.Count == 0) return;

            foreach (var param in parameters)
            {
                object paramValue = param.Value ?? DBNull.Value;
                cmd.Parameters.AddWithValue(param.Key, paramValue);
            }
        }

        /// <summary>
        /// 处理可空类型（int? → int，DateTime? → DateTime）
        /// </summary>
        private static Type GetUnderlyingType(Type type)
        {
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                return Nullable.GetUnderlyingType(type);
            }
            return type;
        }
        #endregion
    }
}