using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;

namespace LoginModul
{
    public abstract class MsSqlConnect
    {
        protected string _connectionString = ""; //"Data Source=10.165.150.52;Initial Catalog=mkuRentalDM13A;User ID=dme131a;Password=dme131a";

        protected SqlConnection dbconn;
        protected SqlCommand cmd;
        protected SqlDataReader reader;
        protected SqlParameter parameter;
        protected string sqlCommandString;

        public MsSqlConnect(string connectionString)
        {
            _connectionString = connectionString;
        }
        public void ConnectDB()
        {
            dbconn = new SqlConnection(_connectionString);
            dbconn.Open();
        }
        public void CloseDB()
        {
            dbconn.Close();
        }
    }
}
