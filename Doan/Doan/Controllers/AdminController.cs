using Doan.Data;
using Doan.Models;
using Doan.Utils;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Doan.Controllers
{
    public class AdminController : Controller
    {

        private readonly ILogger<AdminController> _logger;
        private readonly ConnectDB _context;  // Khai báo _context
        public AdminController(ILogger<AdminController> logger, ConnectDB context)
        {
            _logger = logger;
            _context = context;  // Khởi tạo _context từ DI container
        }

        public IActionResult Index()
        {
            // Lấy danh sách tất cả sản phẩm từ Entity Framework
            List<Product> list = _context.products.ToList();

            // Lấy danh sách tất cả danh mục từ Entity Framework
            List<Order> listc = _context.orders.ToList();

            // Lấy danh sách tất cả người dùng từ Entity Framework
            List<User> listu = _context.users.ToList();

            // Tính toán tổng số lượng sản phẩm, danh mục, và người dùng
            int soLuongUser = listu.Count;
            int soLuongProduct = list.Count;
            int soLuongOrder = listc.Count;

            // Đưa các giá trị vào ViewBag để hiển thị trên giao diện
            ViewBag.TotalProducts = soLuongProduct;
            ViewBag.TotalOrder = soLuongOrder;
            ViewBag.TotalUsers = soLuongUser;

            return View();
        }



        public IActionResult ManagerUser(string xpage)
        {

            var userSession = HttpContext.Session.GetString("user");
            if (string.IsNullOrEmpty(userSession))
            {
                // Chuyển hướng đến trang Login nếu Session rỗng hoặc null
                return RedirectToAction("Login", "Home");
            }
            var user = JsonConvert.DeserializeObject<User>(userSession);
            List<User> listu = _context.users.ToList();

            int page, numperpage = 5;
            int size = listu.Count;
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

            List<User> list = MyUtils.getListBypageUser(listu, start, end);

            ViewBag.Users = list;
            ViewBag.Page = page;
            ViewBag.Num = num;
            return View();
        }

        public IActionResult ManagerProduct(string xpage)
        {

            var userSession = HttpContext.Session.GetString("user");
            if (string.IsNullOrEmpty(userSession))
            {
                // Chuyển hướng đến trang Login nếu Session rỗng hoặc null
                return RedirectToAction("Login", "Home");
            }
            var user = JsonConvert.DeserializeObject<User>(userSession);
            List<Product> listu = _context.products.ToList();

            int page, numperpage = 5;
            int size = listu.Count;
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

            List<Product> list = MyUtils.getListBypageProduct(listu, start, end);

            ViewBag.Products = list;
            ViewBag.Page = page;
            ViewBag.Num = num;
            return View();
        }


        public IActionResult ManagerCategory(string xpage)
        {

            var userSession = HttpContext.Session.GetString("user");
            if (string.IsNullOrEmpty(userSession))
            {
                // Chuyển hướng đến trang Login nếu Session rỗng hoặc null
                return RedirectToAction("Login", "Home");
            }
            var user = JsonConvert.DeserializeObject<User>(userSession);
            List<Category> listu = _context.categorys.ToList();

            int page, numperpage = 5;
            int size = listu.Count;
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

            List<Category> list = MyUtils.getListBypageCate(listu, start, end);

            ViewBag.Categorys = list;
            ViewBag.Page = page;
            ViewBag.Num = num;
            return View();
        }


        public IActionResult ManagerDiscount(string xpage)
        {

            var userSession = HttpContext.Session.GetString("user");
            if (string.IsNullOrEmpty(userSession))
            {
                // Chuyển hướng đến trang Login nếu Session rỗng hoặc null
                return RedirectToAction("Login", "Home");
            }
            var user = JsonConvert.DeserializeObject<User>(userSession);
            List<Discount> listu = _context.discounts.ToList();

            int page, numperpage = 5;
            int size = listu.Count;
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

            List<Discount> list = MyUtils.getListBypageDiscount(listu, start, end);

            ViewBag.Discount = list;
            ViewBag.Page = page;
            ViewBag.Num = num;
            return View();
        }





    }
}
