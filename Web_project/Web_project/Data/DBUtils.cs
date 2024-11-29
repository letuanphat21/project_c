using MySql.Data.MySqlClient;

namespace Web_project.Data
{
    public class DBUtils
    {
        public static MySqlConnection GetDBConnection()
        {
            string host = "127.0.0.1";
            int port = 3306;
            string database = "webnhomc";
            string username = "root";
            string password = "";

            return DBMySQLUtils.GetDBConnection(host, port, database, username, password);

        }
    }
}
