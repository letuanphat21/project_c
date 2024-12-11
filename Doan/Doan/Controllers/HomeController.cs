using Doan.Data;
using Doan.Models;
using Doan.Utils;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace Doan.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ConnectDB _context;  // Khai báo _context
        public HomeController(ILogger<HomeController> logger, ConnectDB context)
        {
            _logger = logger;
            _context = context;  // Khởi tạo _context từ DI container
        }

        public IActionResult Index()
        {
            // Lấy tất cả sản phẩm
            List<Product> list = _context.products.ToList();

            // Lấy tất cả danh mục
            List<Category> listc = _context.categorys.Take(6).ToList();

            // Lấy 4 sản phẩm hàng đầu (giả sử bạn có điều kiện chọn 4 sản phẩm như thế nào)
            List<Product> listFour = _context.products
                  .OrderByDescending(p => p.Id)
                   .Take(4)
                    .ToList();

            // Đưa các dữ liệu vào ViewData
            ViewData["Products"] = list;
            ViewData["Categories"] = listc;
            ViewData["TopFourProducts"] = listFour;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
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
        public IActionResult Login(string username, string password)
        {

            ViewBag.Fail = "";
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
            User user = _context.users.FirstOrDefault(u => u.User1 == username);
            if (user == null)
            {
                ViewBag.Fail = "Sai mật khẩu hoặc tên đăng nhập";
                return View();
            }
            string pass = MyUtils.ToMd5Hash(password, user.RandomKey);

            if (user.Password != pass)
            {
                ViewBag.Fail = "Sai mật khẩu hoặc tên đăng nhập";
                return View();
            }
            // Lưu thông tin người dùng vào session dưới dạng JSON
            HttpContext.Session.SetString("user", JsonConvert.SerializeObject(user));

            if (user.IsAdmin)
            {
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                // Chuyển hướng tới trang Index sau khi đăng nhập thành công
                return RedirectToAction("Index", "Home");

            }





            


        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("user");
            return RedirectToAction("Index");
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

            bool isUsernameExists = _context.users.Any(u => u.User1 == username);

            if (isUsernameExists)
            {
                ViewBag.Fail = "Tên người dùng đã tồn tại.";
                return View();
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
                string keyGenerate = MyUtils.keyGenerator();
                string randomKey = keyGenerate;
                string pass = MyUtils.ToMd5Hash(password, keyGenerate);
                bool gioiTinh = gender == "male";
                User us = new User(username, fullName, pass,gioiTinh,birthDay1,email,phoneNumber,address,DateTime.Now,DateTime.Now,false,randomKey);
                ViewBag.Success = "Đăng ký thành công.";
                _context.users.Add(us);
                _context.SaveChanges();
            }

            return View();


        }
        public IActionResult Changeinfor()
        {
            return  View();
        }

        [HttpPost]
        public IActionResult Changeinfor(string fullname, string gender, string birthday, string address, string email, string phoneNumber)
        {
            ViewBag.Fail = "";
            ViewBag.Success = "";
            var userSession = HttpContext.Session.GetString("user");
            var user = JsonConvert.DeserializeObject<User>(userSession);

            if (user == null)
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
            }
            else if (gender.Equals("female"))
            {
                gd = false;
            }
            user.IsGender = gd;

            
                var existingUser = _context.users.FirstOrDefault(u => u.Id == user.Id);
                if (existingUser != null)
                {
                    // Cập nhật thông tin người dùng trong cơ sở dữ liệu
                    existingUser.Fullname = user.Fullname;
                    existingUser.Email = user.Email;
                    existingUser.PhoneNumber = user.PhoneNumber;
                    existingUser.Address = user.Address;
                    existingUser.BirthDay = user.BirthDay;
                    existingUser.IsGender = user.IsGender;

                    // Lưu thay đổi vào cơ sở dữ liệu
                    _context.SaveChanges();
                }
                else
                {
                    ViewBag.Fail = "Người dùng không tồn tại trong cơ sở dữ liệu.";
                    return View();
                }
          

            ViewBag.Success = "Bạn đã thay đổi thông tin thành công";
            // Cập nhật lại session với thông tin mới
            HttpContext.Session.SetString("user", JsonConvert.SerializeObject(user));
            var updatedUser = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("user"));
            return View(updatedUser);
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
            var user = JsonConvert.DeserializeObject<User>(userSession);

            if (user == null)
            {
                ViewBag.Fail = "Bạn chưa đăng nhập, vui lòng hãy đăng nhập";
                return View();
            }

            string pass = MyUtils.ToMd5Hash(currPass, user.RandomKey);
            if (!pass.Equals(user.Password))
            {
                ViewBag.Fail = "Mật khẩu hiện tại không chính xác";
                return View();
            }

            if (!newPass.Equals(renewPass))
            {
                ViewBag.Fail = "Mật khẩu xác nhận không khớp với mật khẩu mới";
                return View();
            }

            string new_pass = MyUtils.ToMd5Hash(newPass, user.RandomKey);

            var existingUser = _context.users.FirstOrDefault(u => u.Id == user.Id);
            if (existingUser != null)
            {
                // Cập nhật mật khẩu mới cho người dùng
                existingUser.Password = new_pass;

                // Lưu thay đổi vào cơ sở dữ liệu
                _context.SaveChanges();

                // Cập nhật lại thông tin trong session
                user.Password = new_pass;
                HttpContext.Session.SetString("user", JsonConvert.SerializeObject(user));

                ViewBag.Success = "Mật khẩu của bạn đã được thay đổi thành công";
                return View();
            }
            else
            {
                ViewBag.Fail = "Người dùng không tồn tại trong cơ sở dữ liệu.";
                return View();
            }


        }

        public IActionResult SelectCate(string cid)
        {
            int cateId = int.Parse(cid);
            
            List<Product> list = _context.products
                                        .Where(p => p.Cid == cateId) // Lọc theo CategoryId
                                        .ToList();


            List<Category> listc = _context.categorys.Take(6).ToList();


            List<Product> listFour = _context.products
                 .OrderByDescending(p => p.Id)
                  .Take(4)
                   .ToList();


            ViewData["Products"] = list;
            ViewData["Categories"] = listc;
            ViewData["TopFourProducts"] = listFour;

           
            return View("Index");
        }




        public IActionResult SearchByName(string txt)
        {
            List<Product> list = _context.products
                                .Where(p => p.Title.Contains(txt))  
                                .ToList();
            List<Category> listc = _context.categorys.Take(6).ToList();

            List<Product> listFour = _context.products
                 .OrderByDescending(p => p.Id)
                  .Take(4)
                   .ToList();
            ViewData["txtS"] = txt;
            ViewData["Products"] = list;
            ViewData["Categories"] = listc;
            ViewData["TopFourProducts"] = listFour;
            return View("Index");

        }


        public IActionResult ProductDetail(string cid, string id)
        {

            
            if (string.IsNullOrEmpty(cid) || string.IsNullOrEmpty(id) || !int.TryParse(cid, out int cateId) || !int.TryParse(id, out int id1) || cateId <= 0 || id1 <= 0)
            {
                // Chuyển hướng về trang Index
                return RedirectToAction("Index", "Home");
            }

           


            // Lấy danh sách sản phẩm cùng danh mục (CategoryId = cid)
            List<Product> list1 = _context.products
                                          .Where(p => p.Cid == cateId)
                                          .ToList();

            // Lấy thông tin sản phẩm chi tiết theo Id
            Product p = _context.products
                                .FirstOrDefault(p => p.Id == id1);

            // Lấy thông tin hình ảnh liên quan đến sản phẩm
            Image im = _context.images
                               .FirstOrDefault(i => i.Pid == id1);

            // Gửi dữ liệu đến View qua ViewBag
            ViewBag.product = p;
            ViewBag.image = im;
            ViewBag.listp = list1;

            return View("ProductDetail");


        }

        public IActionResult Cart()
        {

            // Lấy giỏ hàng từ session
            var sessionCart = HttpContext.Session.GetString("cart");
            Cart cart = string.IsNullOrEmpty(sessionCart)
                ? new Cart() // Nếu giỏ hàng trống, tạo mới
                : JsonConvert.DeserializeObject<Cart>(sessionCart);

            // Đảm bảo danh sách Items được khởi tạo
            if (cart.Items == null)
            {
                cart.Items = new List<Item>();
            }

            return View(cart); // Truyền giỏ hàng vào View
        }




        public IActionResult AddToCart1(string id, string quantity)
        {
            var sessionCart = HttpContext.Session.GetString("cart");
            Cart cart = string.IsNullOrEmpty(sessionCart)
                ? new Cart()
                : JsonConvert.DeserializeObject<Cart>(sessionCart);


            int quantityValue;
            try
            {
                quantityValue = int.Parse(quantity);
            }
            catch (Exception)
            {
                quantityValue = 1;
            }

            int id1=int.Parse(id);
            var product = _context.products.FirstOrDefault(p => p.Id == id1);
            if (product != null)
            {
                double price = product.Price;


                var item = new Item(product, quantityValue, price);
                cart.addItem(item);
            }

            // Lưu giỏ hàng vào session
            HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(cart));

            // Lưu kích thước giỏ hàng (số lượng sản phẩm)
            HttpContext.Session.SetInt32("size", cart.Items.Count);

            // Chuyển hướng về trang Home hoặc bất kỳ trang nào bạn muốn
            return RedirectToAction("Index", "Home");
        }


        public IActionResult UpdateQuantity(string id, string num)
        {
            var sessionCart = HttpContext.Session.GetString("cart");
            Cart cart = string.IsNullOrEmpty(sessionCart)
                ? new Cart()
                : JsonConvert.DeserializeObject<Cart>(sessionCart);
            int quantity;
            int pid;
            try
            {
                pid = int.Parse(id);
                quantity = int.Parse(num);
                if ((quantity == -1) && (cart.getQuantityById(pid) <= 1))
                {
                    cart.removeItem(pid);
                }
                else
                {
                    int id1 = int.Parse(id);
                    var product = _context.products.FirstOrDefault(p => p.Id == id1);
                    double price = product.Price;
                    Item t = new Item(product, quantity, price);
                    cart.addItem(t);

                }
            }
            catch (Exception) { quantity = -1; }

            // Lưu giỏ hàng vào session
            HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(cart));

            // Lưu kích thước giỏ hàng (số lượng sản phẩm)
            HttpContext.Session.SetInt32("size", cart.Items.Count);


            return View("Cart");
        }


        public IActionResult DeleteItem(string id)
        {
            var sessionCart = HttpContext.Session.GetString("cart");
            Cart cart = string.IsNullOrEmpty(sessionCart)
                ? new Cart()
                : JsonConvert.DeserializeObject<Cart>(sessionCart);

            if (id == null)
            {
                ViewBag.Fail = "Xóa thất bại";
                return View("Cart");
            }
            int pid = int.Parse(id);


            cart.removeItem(pid);
            // Lưu giỏ hàng vào session
            HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(cart));
            // Lưu kích thước giỏ hàng (số lượng sản phẩm)
            HttpContext.Session.SetInt32("size", cart.Items.Count);
            return View("Cart");


        }

        public IActionResult AddToCart2(string id, string quantity,string cid)
        {
            // Kiểm tra và khởi tạo giỏ hàng từ Session
            var sessionCart = HttpContext.Session.GetString("cart");
            Cart cart = string.IsNullOrEmpty(sessionCart)
                ? new Cart()
                : JsonConvert.DeserializeObject<Cart>(sessionCart);

            // Kiểm tra và chuyển đổi các tham số đầu vào
            if (!int.TryParse(quantity, out int quantityValue) || quantityValue <= 0)
            {
                quantityValue = 1; // Giá trị mặc định nếu không hợp lệ
            }

            if (!int.TryParse(id, out int id1) || id1 <= 0)
            {
                return RedirectToAction("Index", "Home"); // Chuyển về trang chính nếu id không hợp lệ
            }

            if (!int.TryParse(cid, out int cateId) || cateId <= 0)
            {
                return RedirectToAction("Index", "Home"); // Chuyển về trang chính nếu cid không hợp lệ
            }

            // Truy vấn sản phẩm từ cơ sở dữ liệu
            var product = _context.products.FirstOrDefault(p => p.Id == id1);
            if (product == null)
            {
                return RedirectToAction("Index", "Home"); // Chuyển về trang chính nếu không tìm thấy sản phẩm
            }

            // Thêm sản phẩm vào giỏ hàng
            double price = product.Price;
            var item = new Item(product, quantityValue, price);
            cart.addItem(item);

            // Lưu giỏ hàng vào Session
            HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(cart));
            HttpContext.Session.SetInt32("size", cart.Items.Count); // Lưu kích thước giỏ hàng

            // Lấy danh sách sản phẩm cùng danh mục
            List<Product> list1 = _context.products
                                          .Where(p => p.Cid == cateId)
                                          .ToList();

            // Lấy thông tin hình ảnh liên quan đến sản phẩm
            Image im = _context.images.FirstOrDefault(i => i.Pid == id1);

            // Gửi dữ liệu đến View
            ViewBag.product = product;
            ViewBag.image = im;
            ViewBag.listp = list1;

            return View("ProductDetail");








        }

















    }
}
