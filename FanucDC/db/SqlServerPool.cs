using System.Data.SqlClient;
using System.Data;
using System.Collections;
using System.Reflection;
using System.Xml;

namespace FanucDC.db
{
    internal class SqlServerPool
    {
        static string connectionString = "Server=127.0.0.1;DataBase=trace;Uid=sa;Pwd=1234.abcD;pooling=true;Max pool size=20;Min pool size=10;";
        //static string connectionString = "Server=192.168.110.125;DataBase=trace;Uid=sa;Pwd=1234.abcD;pooling=true;Max pool size=20;Min pool size=10;";
        //static string connectionString = "Server=127.0.0.1;DataBase=trace;Uid=sa;Pwd=Pasxs5w0rd;pooling=true;Max pool size=20;Min pool size=10;";

        public static void initConnectionPool()
        {
            SqlConnection connection = new SqlConnection(connectionString);
        }


        public static int ExecuteNonQuery(string cmdText)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            {
                conn.Open();
                return ExecuteNonQuery(conn, cmdText);
            }
        }

        private static int ExecuteNonQuery(SqlConnection conn, string cmdText)
        {
            int res;
            using (SqlCommand cmd = new SqlCommand(cmdText, conn))
            {
                cmd.CommandType = CommandType.Text;
                res = cmd.ExecuteNonQuery();
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
            return res;
        }


        /// <summary>
        /// 执行查询SQL语句
        /// </summary>
        /// <param name="cmdText">SQL语句</param>
        /// <returns></returns>
        public static DataTable ExecuteDataTable(string cmdText)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                return ExecuteDataTable(conn, cmdText);
            }
        }

        /// <summary>
        /// 执行查询SQL语句
        /// </summary>
        /// <param name="conn">SqlConnection</param>
        /// <param name="cmdText">SQL语句</param>
        /// <returns></returns>
        private static DataTable ExecuteDataTable(SqlConnection conn, string cmdText)
        {
            DataTable dt = new DataTable();
            using (SqlDataAdapter sda = new SqlDataAdapter(cmdText, conn))
            {
                sda.Fill(dt);
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
            return dt;
        }

        /// <summary>
        /// 执行查询SQL语句
        /// </summary>
        /// <param name="cmdText">SQL语句</param>
        /// <returns></returns>
        public static object[] ExecuteQuery(string cmdText)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                return ExecuteQuery(conn, cmdText);
            }
        }




        /// <summary>
        /// 执行查询SQL语句
        /// </summary>
        /// <param name="conn">SqlConnection</param>
        /// <param name="cmdText">SQL语句</param>
        /// <returns></returns>
        private static object[] ExecuteQuery(SqlConnection conn, string cmdText)
        {

            using (SqlCommand cmd = new SqlCommand(cmdText, conn))
            {
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    //dt.Load(sdr);
                    //sdr.Close();
                    //sdr.Dispose();
                    //if (conn.State == ConnectionState.Open)
                    //{
                    //    conn.Close();
                    //    conn.Dispose();
                    //}
                    if (sdr.Read())
                    {
                        // 返回整行数据
                        object[] rowData = new object[sdr.FieldCount];
                        sdr.GetSqlValues(rowData);
                        return rowData;
                    }
                }
            }
            return null;
        }



        public static List<object[]> ExecuteQueryList(string cmdText)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                return ExecuteQueryList(conn, cmdText);
            }
        }


        private static List<object[]> ExecuteQueryList(SqlConnection conn, string cmdText)
        {
            List<object[]> result = new List<object[]>();

            using (SqlCommand cmd = new SqlCommand(cmdText, conn))
            {
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    //dt.Load(sdr);
                    //sdr.Close();
                    //sdr.Dispose();
                    //if (conn.State == ConnectionState.Open)
                    //{
                    //    conn.Close();
                    //    conn.Dispose();
                    //}
                    while (sdr.Read())
                    {
                        // 返回整行数据
                        object[] rowData = new object[sdr.FieldCount];
                        sdr.GetSqlValues(rowData);
                        result.Add(rowData);
                    }
                }
            }
            return result;
        }

        public static List<T> QueryList<T>(string cmdText)
        {
            var result = new List<T>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(cmdText, conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var item = Activator.CreateInstance<T>();
                            foreach (var property in typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance ))
                            {
                                var ordinal = reader.GetOrdinal(property.Name);
                                if (reader.GetValue(ordinal) != DBNull.Value)
                                {
                                    property.SetValue(item, Convert.ChangeType(reader.GetValue(ordinal), property.PropertyType), null);
                                }
                            }
                            result.Add(item);
                        }
                    }
                }
                return result;
            }
        }



    }
}
