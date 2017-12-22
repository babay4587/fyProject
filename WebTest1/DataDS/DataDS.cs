using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Net;
using System.Web.Configuration;

namespace WebTest1.DataDS
{
    public class DataDS
    {
        private string sqlAddress = string.Empty;
        private string sqlUsr = string.Empty;
        private string sqlPwd = string.Empty;

        Properties.Settings config = Properties.Settings.Default;

        //Provider=SQLOLEDB.1;Persist Security Info=False;User ID=sa;Initial Catalog=SITMesdb;Data Source=192.168.0.185
        public SqlConnection sqlConn()
        {
            sqlAddress = config.SQLAddress;
            sqlUsr = config.SQLID;
            sqlPwd = config.SQLPwd;

            string sqlCon = string.Format("Data Source='{2}';Initial Catalog=SITMesdb;user ID='{0}';password='{1}';Enlist=true;Pooling=true;Max Pool Size=300;Min Pool Size=0;Connection Lifetime=300;packet size=1000", sqlUsr, sqlPwd, sqlAddress);

            SqlConnection conn = new SqlConnection(sqlCon);

            try
            {

                conn.Open();
                return conn;
            }
            catch
            {
                return null;
            }
            
        }

        public SqlCommand creatCmd(string sql, SqlConnection conn)
        {
            SqlCommand cmd;
            cmd = new SqlCommand(sql, conn);
            return cmd;
        }

        public DataTable getSQL(string sql)
        {
            SqlConnection sqlconnect = sqlConn();

            SqlCommand cmd = creatCmd(sql, sqlconnect);

            SqlDataAdapter adt = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();

            try
            {
                adt.Fill(dt);
                if (dt != null && dt.Rows.Count > 0)
                {
                    return dt;
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
            finally
            {
                sqlconnect.Close();
                cmd.Dispose();
            }
        }
    }
}