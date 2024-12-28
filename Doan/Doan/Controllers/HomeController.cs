using Doan.Data;
using Doan.Models;
using Doan.Utils;
using Microsoft.AspNetCore.Mvc;
using NETCore.MailKit.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
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
        public IActionResult Login(string? returnUrl)
        {
            if (!string.IsNullOrEmpty(returnUrl))
            {
                HttpContext.Session.SetString("ReturnUrl", returnUrl);
            }
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password, string? returnUrl)
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

            if (user.Password != pass ||!user.IsConfirmEmail)
            {
                ViewBag.Fail = "Sai mật khẩu hoặc tên đăng nhập hoặc chưa xác nhận email";
                return View();
            }
            // Lưu thông tin người dùng vào session dưới dạng JSON
            HttpContext.Session.SetString("user", JsonConvert.SerializeObject(user));
            var sessionReturnUrl = HttpContext.Session.GetString("ReturnUrl");
            HttpContext.Session.Remove("ReturnUrl");

            // Nếu có ReturnUrl thì điều hướng lại trang đó
            if (!string.IsNullOrEmpty(sessionReturnUrl) && Url.IsLocalUrl(sessionReturnUrl))
            {
                return Redirect(sessionReturnUrl);
            }

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

            // Check for email uniqueness
            if (string.IsNullOrEmpty(ViewBag.Fail))
            {
                bool isEmailExists = _context.users.Any(u => u.Email == email);
                if (isEmailExists)
                {
                    ViewBag.Fail = "Email đã được sử dụng.";
                    return View();
                }
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
                User us = new User(username, fullName, pass,gioiTinh,birthDay1,email,phoneNumber,address,DateTime.Now,DateTime.Now,false,randomKey,false);
               
                _context.users.Add(us);
                _context.SaveChanges();
                // Generate OTP
                string otp = MyUtils.GenerateOTP(6);

                // Save OTP to Session
                HttpContext.Session.SetString("OTP", otp);
                HttpContext.Session.SetString("Email", email);
                HttpContext.Session.SetString("OTPExpiryTime", DateTime.Now.AddMinutes(5).ToString());

                // Send OTP to user's email
                Email.SendEmailAsync(email,"Mã xác nhận tài khoản của bạn","<h1>Mã xác nhận của bạn là :"+otp+"<h1>");

                // Redirect to ConfirmOTP page
                return RedirectToAction("ConfirmOTP", "Home");

            }

            return View();
        }

        public IActionResult ConfirmOTP()
        {
            return View();
           
        }


        [HttpPost]
        public IActionResult ConfirmOTP(string otp)
        {
            // Lấy thông tin email từ Session
            string email = HttpContext.Session.GetString("Email");

            // Kiểm tra nếu email không tồn tại trong Session
            if (string.IsNullOrEmpty(email))
            {
                TempData["Fail"] = "Mã OTP đã hết hạn hoặc không hợp lệ.";
                return RedirectToAction("ConfirmOTP");
            }

            // Lấy mã OTP lưu trong Session
            string storedOtp = HttpContext.Session.GetString("OTP");
            string expiryTimeStr = HttpContext.Session.GetString("OTPExpiryTime");
            DateTime otpExpiryTime = DateTime.Parse(expiryTimeStr);

            // Kiểm tra thời gian hết hạn của OTP
            if (DateTime.Now > otpExpiryTime)
            {
                TempData["Fail"] = "Mã OTP đã hết hạn. Vui lòng gửi lại mã.";
                return RedirectToAction("ConfirmOTP");
            }

            // So sánh mã OTP
            if (otp == storedOtp)
            {
                // Nếu mã OTP hợp lệ, lấy thông tin người dùng từ cơ sở dữ liệu
                var user = _context.users.FirstOrDefault(u => u.Email == email);

                if (user != null)
                {
                    user.IsConfirmEmail = true;  // Cập nhật trạng thái xác thực người dùng
                    _context.SaveChanges();

                    TempData["Success"] = "Xác thực thành công!";

                    // Hủy session các thông tin OTP
                    HttpContext.Session.Remove("Email");
                    HttpContext.Session.Remove("OTP");
                    HttpContext.Session.Remove("OTPExpiryTime");

                    return RedirectToAction("SuccessConfirmEmail", "Home");
                }
            }
            else
            {
                TempData["Fail"] = "Mã OTP không hợp lệ. Vui lòng thử lại.";
            }

            return RedirectToAction("ConfirmOTP");
        }



        public IActionResult SuccessConfirmEmail()
        {
            return View();
        }


        [HttpGet]
        public IActionResult ResendOTP()
        {
            // Lấy thông tin email từ Session
            string email = HttpContext.Session.GetString("Email");

            // Kiểm tra nếu email không tồn tại trong Session
            if (string.IsNullOrEmpty(email))
            {
                TempData["Fail"] = "Email không hợp lệ hoặc hết hạn.";
                return RedirectToAction("ConfirmOTP");
            }

            // Tạo mã OTP mới
            string newOtp = MyUtils.GenerateOTP(6);

            // Cập nhật mã OTP mới vào Session
            HttpContext.Session.SetString("OTP", newOtp);
            string otpExpiryTime = DateTime.Now.AddMinutes(5).ToString();
            HttpContext.Session.SetString("OTPExpiryTime", otpExpiryTime);

            // Gửi mã OTP mới qua email
            Email.SendEmailAsync(email, "Mã xác nhận tài khoản của bạn", "<h1>Mã xác nhận của bạn là :" + newOtp + "<h1>");

            return RedirectToAction("ConfirmOTP");
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
                return View(user);
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

            // Kiểm tra email không được trùng
            var emailExists = _context.users.Any(u => u.Email == email && u.Id != user.Id);
            if (emailExists)
            {
                ViewBag.Fail = "Email đã tồn tại. Vui lòng chọn email khác.";
                return View(user);
            }

            user.Email = email;

            var existingUser = _context.users.FirstOrDefault(u => u.Id == user.Id);
            if (existingUser != null)
            {
                // Cập nhật thông tin người dùng trong cơ sở dữ liệu
                existingUser.Fullname = user.Fullname;
                existingUser.Email = user.Email;
                existingUser.PhoneNumber = user.PhoneNumber;
                existingUser.Address = user.Address;
                existingUser.BirthDay = user.BirthDay;
                existingUser.updatedAt = DateTime.Now;
                existingUser.IsGender = user.IsGender;

                // Lưu thay đổi vào cơ sở dữ liệu
                _context.SaveChanges();
            }
            else
            {
                ViewBag.Fail = "Người dùng không tồn tại trong cơ sở dữ liệu.";
                return View(user);
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
                existingUser.updatedAt=DateTime.Now;

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
                cart.Items = new Dictionary<int, Item>();
            }

            return View(cart); // Truyền giỏ hàng vào View
        }




        public IActionResult AddToCart1(string id, string quantity)
        {
            // Lấy giỏ hàng từ session
            var sessionCart = HttpContext.Session.GetString("cart");
            Cart cart = string.IsNullOrEmpty(sessionCart)
                ? new Cart()
                : JsonConvert.DeserializeObject<Cart>(sessionCart);

            // Parse quantity, mặc định là 1 nếu không hợp lệ
            int quantityValue = 1;
            if (!int.TryParse(quantity, out quantityValue) || quantityValue <= 0)
            {
                quantityValue = 1;
            }

            // Parse product ID
            if (!int.TryParse(id, out int productId))
            {
                // Xử lý khi ID sản phẩm không hợp lệ
                return BadRequest("Invalid product ID");
            }

            // Tìm sản phẩm trong cơ sở dữ liệu
            var product = _context.products.FirstOrDefault(p => p.Id == productId);
            if (product != null)
            {
                // Tạo Item mới
                var item = new Item(product, quantityValue,product.Price);

                // Thêm Item vào giỏ hàng
                cart.AddItem(item);
            }
            else
            {
                // Xử lý khi không tìm thấy sản phẩm
                return NotFound("Product not found");
            }

            // Lưu giỏ hàng vào session dưới dạng JSON
            HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(cart));

            // Lưu kích thước giỏ hàng (số lượng sản phẩm khác nhau)
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
                if ((quantity == -1) && (cart.GetQuantityById(pid) <= 1))
                {
                    cart.RemoveItem(pid);
                }
                else
                {
                    int id1 = int.Parse(id);
                    var product = _context.products.FirstOrDefault(p => p.Id == id1);
                    double price = product.Price;
                    Item t = new Item(product, quantity, price);
                    cart.AddItem(t);

                }
            }
            catch (Exception) { quantity = -1; }

            // Lưu giỏ hàng vào session
            HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(cart));

            // Lưu kích thước giỏ hàng (số lượng sản phẩm)
            HttpContext.Session.SetInt32("size", cart.Items.Count);


            return View("Cart");
        }


        [HttpPost]
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


            cart.RemoveItem(pid);
            // Lưu giỏ hàng vào session
            HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(cart));
            // Lưu kích thước giỏ hàng (số lượng sản phẩm)
            HttpContext.Session.SetInt32("size", cart.Items.Count);
            return RedirectToAction("Cart", "Home");


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
            cart.AddItem(item);

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
        public IActionResult FormCheckOut()
        {
            var sessionUser = HttpContext.Session.GetString("user");
            User user = string.IsNullOrEmpty(sessionUser)
                ? new User() 
                : JsonConvert.DeserializeObject<User>(sessionUser);

            return View();
        }

        [HttpPost]
        public IActionResult FormCheckOut(string fullname, string email, string phonenumber, string address, string note)
        {
            var cartSession = HttpContext.Session.GetString("cart");
            Cart cart = JsonConvert.DeserializeObject<Cart>(cartSession);


            if (cart.IsCartEmpty())
            {
                return View("Index");
            }



            var sessionUser = HttpContext.Session.GetString("user");
            User user = string.IsNullOrEmpty(sessionUser)
                ? new User()
                : JsonConvert.DeserializeObject<User>(sessionUser);


            Order order = new Order(user.Id, fullname,email,phonenumber,address,note, DateTime.MinValue, "pending",0,null);
            HttpContext.Session.SetString("order", JsonConvert.SerializeObject(order));

            return View("PaymentMethod");

        }

        public IActionResult PaymentMethod()
        {
            return View();
        }

        [HttpGet]
        public IActionResult PaymentMethod1(string method)
        {
            string method_last = "";
            if (method.Equals("1"))
            {
                method_last = "VNPayQR";
            }
            else if (method.Equals("2"))
            {
                method_last = "Thẻ nội địa";
            }
            else if (method.Equals("3"))
            {
                method_last = "Thẻ quốc tế";
            }
            else if (method.Equals("4"))
            {
                method_last = "Ví VNPay";
            }
            else if (method.Equals("5"))
            {
                method_last = "Tiền mặt";
            }

            var sessionCart = HttpContext.Session.GetString("cart");
            var cartSession = HttpContext.Session.GetString("cart");
            Cart cart = JsonConvert.DeserializeObject<Cart>(cartSession);

            // Check if the cart is empty
            if (cart.IsCartEmpty())
            {
                return View("Index");
            }

            var orderSession = HttpContext.Session.GetString("order");
            Order order = JsonConvert.DeserializeObject<Order>(orderSession);

            // List of discounts to show on the page
            List<Discount> validDiscounts = _context.discounts
                                        .Where(d => d.End_Date >= DateTime.Now)  // Lọc các discount còn hạn
                                        .ToList();

            ViewBag.discountList = validDiscounts;

            // Set the payment method to the order
            order.PaymentMethod = method_last;

            // Update the session with the updated order
            HttpContext.Session.SetString("order", JsonConvert.SerializeObject(order));

            
            return View("Check");
        }



        public IActionResult Check()
        {
            var orderSession = HttpContext.Session.GetString("order");
            Order order = JsonConvert.DeserializeObject<Order>(orderSession);

            var cartSession = HttpContext.Session.GetString("cart");
            Cart cart = JsonConvert.DeserializeObject<Cart>(cartSession);
            return View();
        }



        public IActionResult ApplyDiscount(string discountCode)
        {
            // Lấy giỏ hàng từ session
            var cartSession = HttpContext.Session.GetString("cart");
            Cart cart = JsonConvert.DeserializeObject<Cart>(cartSession);

            var orderSession = HttpContext.Session.GetString("order");
            Order order = JsonConvert.DeserializeObject<Order>(orderSession);


            // Tính tổng tiền và áp dụng giảm giá
            double totalMoney = 0;
            Discount discount = null;

            if (!string.IsNullOrEmpty(discountCode))
            {
                int discount1 =int.Parse(discountCode);
                discount = _context.discounts.FirstOrDefault(d => d.Id == discount1);
                if (discount != null)
                {
                    totalMoney = cart.GetTotalMoney() * (1 - (discount.Discount_Value / 100.0));
                }
            }

            if (discount == null)
            {
                totalMoney = cart.GetTotalMoney();
            }

            // Lấy danh sách mã giảm giá hợp lệ
            List<Discount> validDiscounts = _context.discounts
                                       .Where(d => d.End_Date >= DateTime.Now)  // Lọc các discount còn hạn
                                       .ToList();

            // Truyền dữ liệu sang View
            ViewBag.discountList = validDiscounts;
            int didId = int.Parse(discountCode);
            ViewBag.DisId = didId;
            ViewBag.TotalMoneyUseDis = totalMoney;

            return View("Check");
        }


        public IActionResult ConfirmCheckOut(string disID)
        {
            var cartSession = HttpContext.Session.GetString("cart");
            Cart cart = JsonConvert.DeserializeObject<Cart>(cartSession);

            var orderSession = HttpContext.Session.GetString("order");
            Order order = JsonConvert.DeserializeObject<Order>(orderSession);

            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    // Tính tổng tiền và áp dụng giảm giá
                    double totalMoney = 0;
                    Discount discount = null;

                    if (!string.IsNullOrEmpty(disID))
                    {
                        int discountId = int.Parse(disID);
                        discount = _context.discounts.FirstOrDefault(d => d.Id == discountId);
                       

                        if (discount != null)
                        {
                            totalMoney = cart.GetTotalMoney() * (1 - (discount.Discount_Value / 100.0));
                            _context.discounts.Remove(discount);
                            _context.SaveChanges();
                        }
                    }
                    if (discount == null)
                    {
                        totalMoney = cart.GetTotalMoney();
                    }
                    order.TotalMoney = totalMoney;
                    order.OrderDate = DateTime.Now;

                    _context.orders.Add(order);
                    _context.SaveChanges();  // Thêm order vào database

                    // Thêm thông tin order detail
                    foreach (var item in cart.Items.Values)
                    {
                        if (item.product != null)
                        {
                            var orderDetail = new OrderDetail
                            {
                                OrderId = order.Id,
                                ProductId = item.product.Id,
                                Price = item.Price,
                                Quantity = item.Quantity,
                                SubTotal = item.Price * item.Quantity
                            };

                            _context.orderDetails.Add(orderDetail);
                        }
                    }

                    _context.SaveChanges();  // Thêm các order detail

                    // Commit transaction
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    // Rollback transaction if any error occurs
                    transaction.Rollback();
                    // Log error or return an appropriate response
                }
            }

            HttpContext.Session.Remove("cart");
            HttpContext.Session.Remove("order");
            HttpContext.Session.Remove("size");
            return RedirectToAction("Thank");
        }

        public IActionResult Thank()
        {
            return View(); 
        }
        


        public IActionResult ViewOrder(string xpage)
        {
            var sessionUser = HttpContext.Session.GetString("user");
            User user = string.IsNullOrEmpty(sessionUser)
                ? new User()
                : JsonConvert.DeserializeObject<User>(sessionUser);

            if (user != null && user.Id > 0)
            {
               
                List<Order> orders = _context.orders.Where(o => o.UserId == user.Id).ToList();
                int page, numperpage = 5;
                int size = orders.Count;
                int num = (size % numperpage == 0 ? (size / numperpage) : ((size / numperpage) + 1));// số trang


                if (xpage == null)
                {
                    page = 1;
                }
                else
                {
                    page = int.Parse(xpage);
                }

                int start, end;
                start = (page - 1) * numperpage;
                end = Math.Min(page * numperpage, size);

                List<Order> list1 =MyUtils.getListBypageOrder(orders, start, end);

                ViewBag.Listo = list1;
                ViewBag.num = num;
                ViewBag.page = page;

                return View();


            }else
            {
                return View("Login");
            }



        }


        public IActionResult OrderDetail(string oid)
        {
            if (string.IsNullOrEmpty(oid))
            {
                return BadRequest("Order ID is required.");
            }
            int id = int.Parse(oid);
            var order = _context.orders
            .Where(o => o.Id == id)
            .FirstOrDefault();




            var orderDetails = _context.orderDetails
             .Where(od => od.OrderId == id)
             .Select(od => new
           {
         od.Id,
          od.Quantity,
           od.Price,
              ProductTitle = od.Product.Title 
             })
               .ToList();


            ViewBag.order = order;
            ViewBag.orderDetails = orderDetails;
            return View();
        }


        public IActionResult DeleteOrder(string oid)
        {
            int orderId = int.Parse(oid);
            ViewBag.Fail = "";
            ViewBag.Success = "";

            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    // Xóa orderDetails trước
                    var orderDetails = _context.orderDetails
                        .Where(od => od.OrderId == orderId)
                        .ToList();

                    _context.orderDetails.RemoveRange(orderDetails);

                    var order = _context.orders
                        .FirstOrDefault(o => o.Id == orderId);

                    if (order == null)
                    {
                        return NotFound("Order not found.");
                    }

                    _context.orders.Remove(order);

                    // Lưu các thay đổi
                    _context.SaveChanges();

                    transaction.Commit();
                    ViewBag.Success = "Xóa đơn hàng thành công";
                }
                catch (Exception ex)
                {
                    // Hủy transaction nếu có lỗi
                    transaction.Rollback();
                    ViewBag.Fail = "Xóa đơn hàng thất bại.";
                }
            }

            return RedirectToAction("ViewOrder");
        }





        public IActionResult Forgotpass()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Forgotpass(string email, string newPass, string renewPass)
        {
            ViewBag.Success = "";
            ViewBag.Fail = "";

            // 1. Kiểm tra đầu vào
            if (string.IsNullOrEmpty(email))
            {
                ViewBag.Fail = "Email không được để trống.";
                return View();
            }

            if (string.IsNullOrEmpty(newPass) || string.IsNullOrEmpty(renewPass))
            {
                ViewBag.Fail = "Mật khẩu không được để trống.";
                return View();
            }

            if (!newPass.Equals(renewPass))
            {
                ViewBag.Fail = "Mật khẩu không trùng khớp với mật khẩu nhập lại.";
                return View();
            }

            // 2. Tìm người dùng trong cơ sở dữ liệu
            User user = _context.users.FirstOrDefault(u => u.Email == email);
            if (user == null)
            {
                ViewBag.Fail = "Email không tồn tại.";
                return View();
            }

            string otp = MyUtils.GenerateOTP(6);
            HttpContext.Session.SetString("OTP", otp);
            HttpContext.Session.SetString("pass", newPass);
            HttpContext.Session.SetString("Email", email);
            HttpContext.Session.SetString("OTPExpiryTime", DateTime.Now.AddMinutes(5).ToString());

            // Send OTP to user's email
            Email.SendEmailAsync(email, "Mã OTP ", "<h1>Mã xác nhận của bạn là :" + otp + "<h1>");

            return RedirectToAction("ConfirmOTPForgotpass", "Home");
        }


        public IActionResult ConfirmOTPForgotpass()
        {
            return View();

        }


        [HttpPost]
        public IActionResult ConfirmOTPForgotpass(string otp)
        {
            // Lấy thông tin email từ Session
            string email = HttpContext.Session.GetString("Email");

            // Kiểm tra nếu email không tồn tại trong Session
            if (string.IsNullOrEmpty(email))
            {
                TempData["Fail"] = "Mã OTP đã hết hạn hoặc không hợp lệ.";
                return RedirectToAction("ConfirmOTPForgotpass");
            }

            // Lấy mã OTP lưu trong Session
            string storedOtp = HttpContext.Session.GetString("OTP");
            string expiryTimeStr = HttpContext.Session.GetString("OTPExpiryTime");
            DateTime otpExpiryTime = DateTime.Parse(expiryTimeStr);
            string newPass = HttpContext.Session.GetString("pass");
            // Kiểm tra thời gian hết hạn của OTP
            if (DateTime.Now > otpExpiryTime)
            {
                TempData["Fail"] = "Mã OTP đã hết hạn. Vui lòng gửi lại mã.";
                return RedirectToAction("ConfirmOTPFotgotpass");
            }

            


            // So sánh mã OTP
            if (otp == storedOtp)
            {
                // Nếu mã OTP hợp lệ, lấy thông tin người dùng từ cơ sở dữ liệu
                var user = _context.users.FirstOrDefault(u => u.Email == email);
                string keyGenerate = MyUtils.keyGenerator();
                string randomKey = keyGenerate;
                string pass = MyUtils.ToMd5Hash(newPass, keyGenerate);
                if (user != null)
                {
                   user.Password = pass;
                    user.RandomKey = randomKey;
                    try
                    {
                        _context.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        // Log lỗi nếu có
                        Console.WriteLine($"Lỗi khi lưu thay đổi: {ex.Message}");
                        TempData["Fail"] = "Có lỗi xảy ra khi lưu thay đổi. Vui lòng thử lại.";
                    }

                    TempData["Success"] = "Xác thực thành công!";

                    // Hủy session các thông tin OTP
                    HttpContext.Session.Remove("Email");
                    HttpContext.Session.Remove("OTP");
                    HttpContext.Session.Remove("OTPExpiryTime");
                    HttpContext.Session.Remove("pass");

                    return RedirectToAction("SuccessChangPass", "Home");
                }
            }
            else
            {
                TempData["Fail"] = "Mã OTP không hợp lệ. Vui lòng thử lại.";
            }
            return RedirectToAction("ConfirmOTPForgotpass");
        }

        public IActionResult SuccessChangPass()
        {
            return View();
        }


        [HttpGet]
        public IActionResult ResendOTPForgotpass()
        {
            // Lấy thông tin email từ Session
            string email = HttpContext.Session.GetString("Email");

            // Kiểm tra nếu email không tồn tại trong Session
            if (string.IsNullOrEmpty(email))
            {
                TempData["Fail"] = "Email không hợp lệ hoặc hết hạn.";
                return RedirectToAction("ConfirmOTPForgotpass");
            }

            // Tạo mã OTP mới
            string newOtp = MyUtils.GenerateOTP(6);

            // Cập nhật mã OTP mới vào Session
            HttpContext.Session.SetString("OTP", newOtp);
            string otpExpiryTime = DateTime.Now.AddMinutes(5).ToString();
            HttpContext.Session.SetString("OTPExpiryTime", otpExpiryTime);

            // Gửi mã OTP mới qua email
            Email.SendEmailAsync(email, "Mã xác nhận tài khoản của bạn", "<h1>Mã xác nhận của bạn là :" + newOtp + "<h1>");

            return RedirectToAction("ConfirmOTPForgotpass");
        }











    }
}
