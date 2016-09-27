using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;

namespace LoginModul
{
    public class MsSqlLoginDataMapper : MsSqlConnect, ILoginDataMapper
    {
        public MsSqlLoginDataMapper(string connectionString) : base(connectionString)
        { }
        public void Create(Login login)
        {
            ConnectDB();
            sqlCommandString = "insert into Login(ConfirmedEMail,UnconfirmedEMail,HashedPassword,UserID,BadLogins,VerificationCode) values ('" + login.ConfirmedEMail + "','" + login.UnconfirmedEMail + "',"+ login.HashedPassword + "," + login.UserID + ", " + login.BadLogins.ToString() + ", '" + login.VerificationCode + "')";
            cmd = new SqlCommand(sqlCommandString, dbconn);
            int i = cmd.ExecuteNonQuery();
            CloseDB();
        }

        public void Delete(Login login)
        {
            ConnectDB();
            sqlCommandString = "delete from login where UserID = '" + login.UserID.ToString() + "'";
            cmd = new SqlCommand(sqlCommandString, dbconn);
            int i = cmd.ExecuteNonQuery();
            CloseDB();
        }

        public bool DoesVerificationCodeExist(string verificationCode)
        {
            ConnectDB();
            sqlCommandString = "select count(*) as c from Login where VerificationCode = '" + verificationCode + "'";
            cmd = new SqlCommand(sqlCommandString, dbconn);
            reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                int c = (int)reader["c"];
                if (c == 0)
                    return false;
                else
                    return true;
            }
            CloseDB();
            return false;
        }

        public Login Read(long UserID)
        {
            ConnectDB();
            sqlCommandString = "select UnconfirmedEMail,ConfirmedEMail,HashedPassword,BadLogins,VerificationCode,UserID from Login where UserID = '" + UserID.ToString() + "'";
            cmd = new SqlCommand(sqlCommandString, dbconn);
            reader = cmd.ExecuteReader();

            Login newL = null;
            if (reader.Read())
            {
                newL = new Login();
                newL.UnconfirmedEMail = (string)reader["UnconfirmedEMail"];
                newL.ConfirmedEMail = (string)reader["ConfirmedEMail"];
                newL.HashedPassword = (long)reader["HashedPassword"];
                newL.BadLogins = (int)reader["BadLogins"];
                newL.VerificationCode = ((string)reader["VerificationCode"]).Trim();
                newL.UserID = (long)reader["UserID"];
            }
            CloseDB();
            return newL;
        }
        public Login Read(string EMail)
        {
            ConnectDB();
            sqlCommandString = "select UnconfirmedEMail,ConfirmedEMail,HashedPassword,BadLogins,VerificationCode,UserID from Login where UnconfirmedEMail = '" + EMail + "' or ConfirmedEMail = '" + EMail + "' ";
            cmd = new SqlCommand(sqlCommandString, dbconn);
            reader = cmd.ExecuteReader();

            Login newL = null;
            if (reader.Read())
            {
                newL = new Login();
                newL.UnconfirmedEMail = (string)reader["UnconfirmedEMail"];
                newL.ConfirmedEMail = (string)reader["ConfirmedEMail"];
                newL.HashedPassword = (long)reader["HashedPassword"];
                newL.BadLogins = (Int16)reader["BadLogins"];
                newL.VerificationCode = ((string)reader["VerificationCode"]).Trim();
                newL.UserID = (int)reader["UserID"];
            }
            CloseDB();
            return newL;
        }
        public long ReadMaxID()
        {
            ConnectDB();
            sqlCommandString = "select top 1 userID from Login order by userID desc";
            cmd = new SqlCommand(sqlCommandString, dbconn);
            reader = cmd.ExecuteReader();

            long UserID = -1;
            if (reader.Read())
            {
                UserID = (long)reader["UserID"];
            }
            CloseDB();
            return UserID;
        }

        public void Update(Login login)
        {
            ConnectDB();
            sqlCommandString = "update Login set UnconfirmedEMail = '" + login.UnconfirmedEMail + "', ConfirmedEMail = '" + login.ConfirmedEMail + "',HashedPassword = " + login.HashedPassword + ",BadLogins = " + login.BadLogins + ", verificationCode = '" + login.VerificationCode + "' where UserID = " + login.UserID + "";
            cmd = new SqlCommand(sqlCommandString, dbconn);
            int i = cmd.ExecuteNonQuery();
            CloseDB();
        }
    }
}
