using MySql.Data.MySqlClient;

namespace Web_project.Data
{
    public class DBMySQLUtils
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
