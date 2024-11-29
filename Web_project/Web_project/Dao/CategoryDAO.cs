using MySql.Data.MySqlClient;
using Web_project.Data;
using Web_project.Models;

namespace Web_project.Dao
{
    public class CategoryDAO
    {
        public List<Category> SelectAll()
        {
            List<Category> ketQua = new List<Category>();

            // Đảm bảo console hỗ trợ UTF-8 nếu cần
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // Lấy kết nối từ DBUtils
            using (MySqlConnection conn = DBUtils.GetDBConnection())
            {
                try
                {
                    conn.Open();
                    string sql = "SELECT id, name, thumb FROM category";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);

                    // Đọc dữ liệu từ cơ sở dữ liệu
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id=reader.GetInt32("id");
                            string name=reader.GetString("name");
                            string thumb = reader.GetString("thumb");

                            // Tạo đối tượng Product và thêm vào danh sách
                          Category c = new Category(id,name,thumb);
                            ketQua.Add(c);
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
