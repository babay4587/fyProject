using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using WebTest1.DataDS;


namespace WebTest1.SqlFunc
{
    public class SQLFunc
    {

        DataDS.DataDS dsl = new DataDS.DataDS();

        public DataTable getDisMsg(string msgID)
        {
            string sql = string.Empty;

            sql = string.Format("select msg_pk,msg_body,MSG_type,MSG_schema,MSG_ts" +
                " from [DISMsgDB].[dbo].[DIS_MESSAGES] where msg_pk='{0}'", msgID);

            DataTable dt = dsl.getSQL(sql);

            return dt;
        }

        
    }
}