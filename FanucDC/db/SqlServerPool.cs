using System.Data.SqlClient;
using System.Data;

namespace FanucDC.db
{
    internal class SqlServerPool
    {

        static string connectionString = "Server=;DataBase=SMDB;Uid=sa;Pwd=123456;pooling=true;Max pool size=20;Min pool size=10;";

        public static SqlConnection sqlConnection = null;


        public static void initConnectionPool()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            sqlConnection = connection;
        }


        public static int ExecuteNonQuery(string cmdText)
        {
            SqlConnection conn = sqlConnection;
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

    }
}
