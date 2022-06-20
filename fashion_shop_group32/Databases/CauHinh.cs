using MySqlConnector;
using System;

namespace Databases
{
    class CauHinh
    {
        public static MySqlConnection
       GetDBConnection(string host, int port, string database, string username, string password)
        {

            /* Chuỗi kết nối trong thư viện MySql.Data.dll

            String connString = "Server=" + host + ";Database=" + database

              + ";port=" + port + ";User Id=" + username + ";password=" + password;*/

            String connString = "Server=" + host + ";Database=" + database + ";User=" + username

                + ";Port=" + port + ";Password=" + password + ";SSL Mode = None";

            MySqlConnection conn = new MySqlConnection(connString);

            return conn;

        }
    }
}
