using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;

namespace LoginModul
{
    public class MsSqlLoginLogDataMapper : MsSqlConnect, ILoginLogDataMapper
    {
        public MsSqlLoginLogDataMapper(string connectionString) : base(connectionString)
        { }
        public void Create(LoginLog data)
        {
           // data.LoginLogID = ReadMaxID() + 1;
            ConnectDB();
            sqlCommandString = "insert into LoginLog(Timestamp,Location,Severity,Message) values ('" + data.LogTime.ToString("yyyy-MM-dd HH':'mm':'ss'.'fffffff") + "','" + data.Location + "','" + data.Severity + "', '" + data.Message + "')";
            cmd = new SqlCommand(sqlCommandString, dbconn);
            int i = cmd.ExecuteNonQuery();
            CloseDB();
        }
        //public long ReadMaxID()
        //{
        //    ConnectDB();
        //    sqlCommandString = "select top 1 LoginLogID from LoginLog order by LoginLogID desc";
        //    cmd = new SqlCommand(sqlCommandString, dbconn);
        //    reader = cmd.ExecuteReader();

        //    long LoginLogID = 1;
        //    if (reader.Read())
        //    {
        //        LoginLogID = (long)reader["LoginLogID"];
        //    }
        //    CloseDB();
        //    return LoginLogID;
        //}
    }
}
