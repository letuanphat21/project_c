using MySql.Data.MySqlClient;
using System.Data;
using Web_project.Data;
using Web_project.Models;

namespace Web_project.Dao
{
    public class ProductDAO
    {

        public List<Product> SelectAll()
        {
            List<Product> ketQua = new List<Product>();

            // Đảm bảo console hỗ trợ UTF-8 nếu cần
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // Lấy kết nối từ DBUtils
            using (MySqlConnection conn = DBUtils.GetDBConnection())
            {
                try
                {
                    conn.Open();
                    string sql = "SELECT id, categoryId, title,brand ,price, discount, warranty, inventoryNumber, description, thumbnail, createdAt, updatedAt, numOfPur FROM product";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);

                    // Đọc dữ liệu từ cơ sở dữ liệu
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32("id");
                            int cid = reader.GetInt32("categoryid");
                            string title = reader.GetString("title");
                            string brand = reader.GetString("brand");
                            double price = reader.GetDouble("price");
                            int discount = reader.GetInt32("discount");
                            int warranty = reader.GetInt32("warranty");
                            int inventoryNumber = reader.GetInt32("inventoryNumber");
                            string description = reader.GetString("description");
                            string thumbnail = reader.GetString("thumbnail");
                            DateTime createAt = reader.GetDateTime("createdAt");
                            DateTime updateAt = reader.GetDateTime("updatedAt");
                            int numOfPur = reader.GetInt32("numOfPur");

                            // Tạo đối tượng Product và thêm vào danh sách
                            Product p = new Product(id, title,brand,price, discount, warranty, inventoryNumber, description, thumbnail,createAt,updateAt ,cid, numOfPur);

                            ketQua.Add(p);
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    // Log lỗi hoặc xử lý lỗi tùy ý
                    Console.WriteLine($"Lỗi kết nối cơ sở dữ liệu: {ex.Message}");
                }
                catch (Exception ex)
                {
                    // Log lỗi hoặc xử lý lỗi chung
                    Console.WriteLine($"Lỗi không xác định: {ex.Message}");
                }
            }

            return ketQua;
        }

        public List<Product> SelectFourPro()
        {
            List<Product> ketQua = new List<Product>();

            // Đảm bảo console hỗ trợ UTF-8 nếu cần
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // Lấy kết nối từ DBUtils
            using (MySqlConnection conn = DBUtils.GetDBConnection())
            {
                try
                {
                    conn.Open();
                    string sql = "SELECT id, categoryId, title,price, discount, inventoryNumber, description, thumbnail FROM product ORDER BY id DESC LIMIT 4";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);

                    // Đọc dữ liệu từ cơ sở dữ liệu
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32("id");
                            int cid = reader.GetInt32("categoryid");
                            string title = reader.GetString("title");
                            double price = reader.GetDouble("price");
                            int discount = reader.GetInt32("discount");
                            int inventoryNumber = reader.GetInt32("inventoryNumber");
                            string description = reader.GetString("description");
                            string thumbnail = reader.GetString("thumbnail");

                            // Tạo đối tượng Product và thêm vào danh sách
                            Product p = new Product(id, title, price, discount, inventoryNumber, description, thumbnail, cid);
                            ketQua.Add(p);
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    // Log lỗi hoặc xử lý lỗi tùy ý
                    Console.WriteLine($"Lỗi kết nối cơ sở dữ liệu: {ex.Message}");
                }
                catch (Exception ex)
                {
                    // Log lỗi hoặc xử lý lỗi chung
                    Console.WriteLine($"Lỗi không xác định: {ex.Message}");
                }
            }

            return ketQua;
        }


        public List<Product> SelectProductByCid(string cid)
        {
            List<Product> ketQua = new List<Product>();
            
            Console.OutputEncoding = System.Text.Encoding.UTF8;

           
            using (MySqlConnection conn = DBUtils.GetDBConnection())
            {
                try
                {
                    conn.Open();

                    string sql = "SELECT id, categoryId, title, price, discount, inventoryNumber, description, thumbnail " +
                                 "FROM product " +
                                 "WHERE categoryId = @categoryId ";
                                

                    MySqlCommand cmd = new MySqlCommand(sql, conn);

                   
                    cmd.Parameters.AddWithValue("@categoryId", cid);

                  
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32("id");
                            int categoryId = reader.GetInt32("categoryId");
                            string title = reader.GetString("title");
                            double price = reader.GetDouble("price");
                            int discount = reader.GetInt32("discount");
                            int inventoryNumber = reader.GetInt32("inventoryNumber");
                            string description = reader.GetString("description");
                            string thumbnail = reader.GetString("thumbnail");

                            
                            Product p = new Product(id, title, price, discount, inventoryNumber, description, thumbnail, categoryId);
                            ketQua.Add(p);
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    
                    Console.WriteLine($"Lỗi kết nối cơ sở dữ liệu: {ex.Message}");
                }
                catch (Exception ex)
                {
                   
                    Console.WriteLine($"Lỗi không xác định: {ex.Message}");
                }
            }

            return ketQua;
        }


        public List<Product> SearchByName(string txt)
        {
            List<Product> ketQua = new List<Product>();

            Console.OutputEncoding = System.Text.Encoding.UTF8;


            using (MySqlConnection conn = DBUtils.GetDBConnection())
            {
                try
                {
                    conn.Open();

                    string sql = "SELECT id, categoryId, title, price, discount, inventoryNumber, description, thumbnail " +
                         "FROM product " +
                         "WHERE title LIKE @title";


                    MySqlCommand cmd = new MySqlCommand(sql, conn);


                    cmd.Parameters.AddWithValue("@title", "%" + txt + "%");


                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32("id");
                            int categoryId = reader.GetInt32("categoryId");
                            string title = reader.GetString("title");
                            double price = reader.GetDouble("price");
                            int discount = reader.GetInt32("discount");
                            int inventoryNumber = reader.GetInt32("inventoryNumber");
                            string description = reader.GetString("description");
                            string thumbnail = reader.GetString("thumbnail");


                            Product p = new Product(id, title, price, discount, inventoryNumber, description, thumbnail, categoryId);
                            ketQua.Add(p);
                        }
                    }
                }
                catch (MySqlException ex)
                {

                    Console.WriteLine($"Lỗi kết nối cơ sở dữ liệu: {ex.Message}");
                }
                catch (Exception ex)
                {

                    Console.WriteLine($"Lỗi không xác định: {ex.Message}");
                }
            }

            return ketQua;
        }

        public List<Product> getListBypage(List<Product> products, int start, int end)
        {
            List<Product> products1 = new List<Product>();
            for (int i = start; i < end; i++)
            {
                products1.Add(products[i]);
            }
            return products1;
        }







    }
}
