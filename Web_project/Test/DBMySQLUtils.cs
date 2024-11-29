using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    internal class DBMySQLUtils
    {

        public static MySqlConnection

          GetDBConnection(String host, int port, String database, string username, string password)
        {
            //Connection string
            String connString = "Server=" + host + ";Database=" + database
                + ";port=" + port + ";User Id=" + username + ";password=" + password + ";CharSet=utf8;";
            Console.WriteLine(connString);

            MySqlConnection conn = new MySqlConnection(connString);

            return conn;
        }
    }
}
