using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using MySql.Data.MySqlClient;
using Web_project.Data;
using Web_project.Models;

namespace Web_project.Dao
{
    public class UserDAO
    {
        public bool CheckUser(string tenDangNhap)
        {
            bool ketQua = false;

            try
            {
                // Bước 1: Tạo kết nối đến CSDL
                using (MySqlConnection conn = DBUtils.GetDBConnection())
                {
                    // Mở kết nối
                    conn.Open();

                    // Bước 2: Tạo đối tượng command
                    string sql = "SELECT * FROM [user] WHERE [user] = @tenDangNhap";
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        // Thêm tham số vào câu lệnh SQL
                        cmd.Parameters.AddWithValue("@tenDangNhap", tenDangNhap);



                        // Bước 3: Thực thi câu lệnh SQL
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            // Bước 4: Xử lý kết quả
                            if (reader.HasRows)
                            {
                                ketQua = true;
                            }
                        }
                    }
                }
            }
            catch (MySqlException e)
            {
                // Bắt lỗi và in ra console (hoặc log nếu cần)
                Console.WriteLine(e.Message);
            }

            return ketQua;
        }

        public void Insert(Models.User us)
        {
            int ketQua = 0; // Số hàng bị ảnh hưởng
            try
            {
                // Kết nối đến cơ sở dữ liệu
                using (MySqlConnection conn = DBUtils.GetDBConnection())
                {
                    conn.Open();

                    // Chuỗi SQL sử dụng tham số (không nên sử dụng giá trị trực tiếp để tránh SQL Injection)
                    string sql = "INSERT INTO `user` (fullname, `user`, `password`, gender, birthday, email, phoneNumber, address, createdAt, updatedAt, isAdmin) " +
                                 "VALUES (@fullname, @user, @password, @gender, @birthday, @email, @phoneNumber, @address, @createdAt, @updatedAt, 0)";

                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        // Gán giá trị tham số
                        cmd.Parameters.AddWithValue("@fullname", us.Fullname);
                        cmd.Parameters.AddWithValue("@user", us.User1);
                        cmd.Parameters.AddWithValue("@password", us.Password);
                        cmd.Parameters.AddWithValue("@gender", us.IsGender);
                        cmd.Parameters.AddWithValue("@birthday", us.BirthDay);
                        cmd.Parameters.AddWithValue("@email", us.Email);
                        cmd.Parameters.AddWithValue("@phoneNumber", us.PhoneNumber);
                        cmd.Parameters.AddWithValue("@address", us.Address);
                        cmd.Parameters.AddWithValue("@createdAt", DateTime.Now);
                        cmd.Parameters.AddWithValue("@updatedAt", DateTime.Now);

                        // Thực thi câu lệnh
                        ketQua = cmd.ExecuteNonQuery();
                        Console.WriteLine("Số dòng bị ảnh hưởng: " + ketQua);
                    }
                }
            }
            catch (MySqlException e)
            {
                // Xử lý lỗi
                Console.WriteLine("Lỗi: " + e.Message);
            }
        }


        public User login(string username, string password)
        {
            User KetQua = null;

            try
            {
                using (MySqlConnection conn = DBUtils.GetDBConnection())
                {
                    conn.Open();

                    String sql = "SELECT * FROM `user` WHERE `user` = @username AND `password` = @password";

                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@password", password);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int id = reader.GetInt32("id");
                                string user = reader.GetString("user");
                                string fullName = reader.GetString("fullname");
                                string pass = reader.GetString("password");
                                bool gender = reader.GetBoolean("gender");
                                DateTime birthDay = reader.GetDateTime("birthday");
                                string email = reader.GetString("email");
                                string phoneNumber = reader.GetString("phoneNumber");
                                string address = reader.GetString("address");
                                DateTime createdAt = reader.GetDateTime("createdAt");
                                DateTime updatedAt = reader.GetDateTime("updatedAt");
                                bool isAmin = reader.GetBoolean("isAdmin");

                                User us = new User(id, user, fullName, pass, gender, birthDay, email, phoneNumber, address, createdAt, updatedAt, isAmin);
                                KetQua = us;
                            }
                        }
                    }



                }
            }
            catch (MySqlException e)
            {
                Console.WriteLine("Đã xảy ra lỗi khi kết nối cơ sở dữ liệu: " + e.Message);

            }
            return KetQua;

        }


        public bool ChangePass(User u)
        {
            bool ketQua = false;

            try
            {
                // Bước 1: Tạo kết nối đến CSDL
                using (MySqlConnection conn = DBUtils.GetDBConnection())
                {
                    // Mở kết nối
                    conn.Open();

                    // Bước 2: Tạo đối tượng command
                    string sql = "UPDATE user SET password = @matKhauMoi WHERE id = @id";
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        // Thêm tham số vào câu lệnh SQL
                        cmd.Parameters.AddWithValue("@matKhauMoi", u.Password);
                        cmd.Parameters.AddWithValue("@id", u.Id);



                        // Bước 3: Thực thi câu lệnh SQL
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            ketQua = true; // Đổi mật khẩu thành công
                        }
                    }
                }
            }
            catch (MySqlException e)
            {
                // Bắt lỗi và in ra console (hoặc log nếu cần)
                Console.WriteLine(e.Message);
            }

            return ketQua;
        }


        public bool UpdateInfor(User u)
        {
            bool ketQua = false;

            try
            {
                // Bước 1: Tạo kết nối đến CSDL
                using (MySqlConnection conn = DBUtils.GetDBConnection())
                {
                    // Mở kết nối
                    conn.Open();

                    // Bước 2: Tạo đối tượng command
                    string sql = "UPDATE user\r\nSET fullname = @fullname,\r\n    gender = @gender,\r\n    birthday = @birthday,\r\n    email = @email,\r\n    phoneNumber = @phoneNumber,\r\n    address = @address,\r\n    updatedAt = @updatedAt\r\nWHERE id = @id;";
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        // Thêm tham số vào câu lệnh SQL
                        cmd.Parameters.AddWithValue("@fullname", u.Fullname);
                        cmd.Parameters.AddWithValue("@gender", u.IsGender);
                        cmd.Parameters.AddWithValue("@birthday", u.BirthDay);
                        cmd.Parameters.AddWithValue("@email", u.Email);
                        cmd.Parameters.AddWithValue("@phoneNumber", u.PhoneNumber);
                        cmd.Parameters.AddWithValue("@address", u.Address);
                        cmd.Parameters.AddWithValue("@updatedAt", DateTime.Now);
                        cmd.Parameters.AddWithValue("@id", u.Id);

                        // Bước 3: Thực thi câu lệnh SQL
                        int solan = cmd.ExecuteNonQuery();

                        if (solan > 0)
                        {
                            ketQua = true;
                        }

                    }
                }
            }
            catch (MySqlException e)
            {
                // Bắt lỗi và in ra console (hoặc log nếu cần)
                Console.WriteLine(e.Message);
            }

            return ketQua;
        }


        public List<User> SelectAll()
        {

            List<User> users = new List<User>();

            try
            {

                using (MySqlConnection conn = DBUtils.GetDBConnection())
                {
                    conn.Open();


                    string sql = "SELECT * FROM `user`";


                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {

                                int id = reader.GetInt32("id");
                                string username = reader.GetString("user");
                                string fullName = reader.GetString("fullname");
                                string password = reader.GetString("password");
                                bool gender = reader.GetBoolean("gender");
                                DateTime birthDay = reader.GetDateTime("birthday");
                                string email = reader.GetString("email");
                                string phoneNumber = reader.GetString("phoneNumber");
                                string address = reader.GetString("address");
                                DateTime createdAt = reader.GetDateTime("createdAt");
                                DateTime updatedAt = reader.GetDateTime("updatedAt");
                                bool isAdmin = reader.GetBoolean("isAdmin");


                                User us = new User(id, username, fullName, password, gender, birthDay, email, phoneNumber, address, createdAt, updatedAt, isAdmin);


                                users.Add(us);
                            }
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {

                Console.WriteLine("Đã xảy ra lỗi khi kết nối cơ sở dữ liệu: " + ex.Message);
            }
            catch (Exception ex)
            {

                Console.WriteLine("Đã xảy ra lỗi không xác định: " + ex.Message);
            }


            return users;
        }



        public List<User> getListBypage(List<User> users, int start, int end)
        {
            List<User> users1 = new List<User>();
            for (int i = start; i < end; i++)
            {
                users1.Add(users[i]);
            }
            return users1;
        }
    }
}
