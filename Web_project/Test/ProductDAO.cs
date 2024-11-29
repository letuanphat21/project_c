using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    internal class ProductDAO
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
                    string sql = "SELECT id, categoryId, title, price, discount, inventoryNumber, description, thumbnail FROM product";
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
                            Product p = new Product(id,title,price,discount,inventoryNumber,description,thumbnail,cid);
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
    }
}
