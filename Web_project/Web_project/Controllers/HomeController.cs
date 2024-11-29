using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySqlX.XDevAPI;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Globalization;
using Web_project.Dao;
using Web_project.Models;

namespace Web_project.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            ProductDAO pd = new ProductDAO();
            List<Product> list = pd.SelectAll();
            CategoryDAO cd = new CategoryDAO();
            List<Category> listc = cd.SelectAll();
            List<Product> listFour = pd.SelectFourPro();
            ViewData["Products"] = list;
            ViewData["Categories"] = listc;
            ViewData["TopFourProducts"] = listFour;
            return View();
        }
        public IActionResult About()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Login(string username,string password)
        {
            if (string.IsNullOrEmpty(username))
            {
                ViewBag.Fail = "Tên đăng nhập không được để trống.";
                return View();
            }

            else if (string.IsNullOrEmpty(password))
            {
                ViewBag.Fail = "Mật khẩu không được để trống.";
                return View();
            }

            UserDAO ud = new UserDAO();
            User us =ud.login(username, password);
            if(us != null)
            {
                HttpContext.Session.SetString("user", JsonConvert.SerializeObject(us));

                if (us.IsAdmin)
                {
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    return View("Index");
                }


            }
            else
            {
                ViewBag.Fail = "Sai tên đăng nhập hoặc mật khẩu.";
                return View("Login");
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("user");
            return RedirectToAction("Index");
        }

        public IActionResult Changepass()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Changepass(string currPass, string newPass, string renewPass)
        {
            ViewBag.Fail = "";
            ViewBag.Success = "";
             var userSession = HttpContext.Session.GetString("user");
            if(userSession == null)
            {
                ViewBag.Fail = "Bạn chưa đăng nhập, vui lòng hãy đăng nhập";
                return View();
            }
            // khúc này là biến từ chuỗi dạng json thành dạng user lại 
            var user = JsonConvert.DeserializeObject<User>(userSession);
            if(!user.Password.Equals(currPass))
            {
                ViewBag.Fail = "Mật khẩu hiện tại không chính xác";
                return View();
            }

            if(!newPass.Equals(renewPass))
            {
                ViewBag.Fail = "Mật khẩu xác nhận không khớp với mật khẩu mới";
                return View();
            }

            user.Password = newPass;

            UserDAO userDAO = new UserDAO();
            bool changePass = userDAO.ChangePass(user);

            if (changePass)
            {
                ViewBag.Success = "Bạn đã thay đổi mật khẩu thành công";
                //Cập nhập lại session với 
                HttpContext.Session.SetString("user", JsonConvert.SerializeObject(user));
            }
            else
            {
                ViewBag.Fail = "Quá trình thay đổi mật khẩu không thành công!";
            }
            return View();

        }

        public IActionResult Signup()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Signup(string username, string password, string repassword, string fullName, string gender, string birthday, string email, string phoneNumber, string address)
        {
            // Initialize error/success messages
            ViewBag.Fail = "";
            ViewBag.Success = "";

            // Validate null or empty inputs
            if (string.IsNullOrEmpty(username))
            {
                ViewBag.Fail = "Tên đăng nhập không được để trống.";
            }
            else if (string.IsNullOrEmpty(password))
            {
                ViewBag.Fail = "Mật khẩu không được để trống.";
            }
            else if (string.IsNullOrEmpty(repassword))
            {
                ViewBag.Fail = "Vui lòng nhập lại mật khẩu.";
            }
            else if (string.IsNullOrEmpty(fullName))
            {
                ViewBag.Fail = "Họ và tên không được để trống.";
            }
            else if (string.IsNullOrEmpty(gender))
            {
                ViewBag.Fail = "Giới tính không được để trống.";
            }
            else if (string.IsNullOrEmpty(birthday))
            {
                ViewBag.Fail = "Ngày sinh không được để trống.";
            }
            else if (string.IsNullOrEmpty(email))
            {
                ViewBag.Fail = "Email không được để trống.";
            }
            else if (string.IsNullOrEmpty(phoneNumber))
            {
                ViewBag.Fail = "Số điện thoại không được để trống.";
            }
            else if (string.IsNullOrEmpty(address))
            {
                ViewBag.Fail = "Địa chỉ không được để trống.";
            }

            // Parse and validate birthDay
            DateTime birthDay1;
            try
            {
                birthDay1 = DateTime.Parse(birthday);
            }
            catch (FormatException)
            {
                ViewBag.Fail = "Ngày sinh không hợp lệ. Vui lòng nhập theo định dạng hợp lệ (VD: yyyy-MM-dd).";
                birthDay1 = DateTime.MinValue; // Không sử dụng trong trường hợp thất bại
            }

            // Validate password match
            if (ViewBag.Fail == "" && password != repassword)
            {
                ViewBag.Fail = "Mật khẩu không giống mật khẩu nhập lại.";
            }

            // Check if username already exists
            UserDAO userDAO = new UserDAO();
            if (userDAO.CheckUser(username))
            {
                ViewBag.Fail = "Tên đăng nhập đã tồn tại.";
            }

            // Handle validation errors
            if (ViewBag.Fail.Length > 0)
            {
                ViewData["Username"] = username;
                ViewData["FullName"] = fullName;
                ViewData["Gender"] = gender;
                ViewData["Birthday"] = birthday;
                ViewData["Email"] = email;
                ViewData["PhoneNumber"] = phoneNumber;
                ViewData["Address"] = address;
            }
            else
            {
                // Success case
                bool gioiTinh = gender == "male";
                ViewBag.Success = "Đăng ký thành công.";
                User us = new User(username, fullName, password, gioiTinh, birthDay1, email, phoneNumber, address, DateTime.Now, DateTime.Now, false);
                userDAO.Insert(us);
            }

            return View();
        }


        public IActionResult Changeinfor()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Changeinfor(string fullname,string gender,string birthday,string address, string email,string phoneNumber)
        {
            ViewBag.Fail = "";
            ViewBag.Success = "";
            var userSession = HttpContext.Session.GetString("user");
            var user = JsonConvert.DeserializeObject<User>(userSession);

            if(user == null)
            {
                ViewBag.Fail = "Bạn chưa đăng nhập nha";
                return View("Login");
            }

            user.Fullname = fullname;
            user.Email = email;
            user.PhoneNumber = phoneNumber;
            user.Address = address;
            DateTime birthDay1;

            try
            {
                birthDay1 = DateTime.ParseExact(birthday, "yyyy-MM-ddTHH:mm", CultureInfo.InvariantCulture);
            }
            catch (FormatException)
            {
                ViewBag.Fail = "Ngày sinh không hợp lệ. Vui lòng nhập theo định dạng hợp lệ (VD: yyyy-MM-dd).";
                birthDay1 = DateTime.MinValue; // Không sử dụng trong trường hợp thất bại
            }
            user.BirthDay = birthDay1;
            bool gd = true;
            if (gender.Equals("male"))
            {
                gd = true;
            }else if (gender.Equals("female"))
            {
                gd = false;
            }
            user.IsGender = gd;

            UserDAO userDAO = new UserDAO();
            bool ketQua= userDAO.UpdateInfor(user);
            if (ketQua)
            {
                ViewBag.Success = "Bạn đã thay đổi thông tin thành công";
                //Cập nhập lại session với 
                HttpContext.Session.SetString("user", JsonConvert.SerializeObject(user));
                var updatedUser = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("user"));
                return View(updatedUser);
            }
            else
            {
                ViewBag.Fail = "Bạn đã thay đôi thông tin không thành công";
                return View();
            }
           



        }



        public IActionResult SelectCate(string cid)
        {
            ProductDAO pd = new ProductDAO();
            List<Product> list = pd.SelectProductByCid(cid);
            CategoryDAO cd = new CategoryDAO();
            List<Category> listc = cd.SelectAll();
            List<Product> listFour = pd.SelectFourPro();
            ViewData["Products"] = list;
            ViewData["Categories"] = listc;
            ViewData["TopFourProducts"] = listFour;
            return View("Index");



        }

        public IActionResult SearchByName(string txt)
        {
            ProductDAO pd = new ProductDAO();
            List<Product> list = pd.SearchByName(txt);
            CategoryDAO cd = new CategoryDAO();
            List<Category> listc = cd.SelectAll();
            List<Product> listFour = pd.SelectFourPro();
            ViewData["txtS"] = txt;
            ViewData["Products"] = list;
            ViewData["Categories"] = listc;
            ViewData["TopFourProducts"] = listFour;
            return View("Index");

        }





    } 

}
