using Doan.Data;
using Doan.Models;
using Doan.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Security.Cryptography;

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

            // Lấy danh sách tất cả Category từ Entity Framework
            List<Category> listCategory = _context.categorys.ToList();

            // Tính toán tổng số lượng sản phẩm, danh mục, và người dùng
            int soLuongUser = listu.Count;
            int soLuongProduct = list.Count;
            int soLuongOrder = listc.Count;
            int soLuongCategory = listCategory.Count;

            // Đưa các giá trị vào ViewBag để hiển thị trên giao diện
            ViewBag.TotalProducts = soLuongProduct;
            ViewBag.TotalOrder = soLuongOrder;
            ViewBag.TotalUsers = soLuongUser;
            ViewBag.TotalCategorys = soLuongCategory;
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

        public IActionResult ManagerOrder(string xpage)
        {

            var userSession = HttpContext.Session.GetString("user");
            if (string.IsNullOrEmpty(userSession))
            {
                // Chuyển hướng đến trang Login nếu Session rỗng hoặc null
                return RedirectToAction("Login", "Home");
            }
            var user = JsonConvert.DeserializeObject<User>(userSession);
            List<Order> listu = _context.orders.ToList();

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

            List<Order> list = MyUtils.getListBypageOrder(listu, start, end);

            ViewBag.Order = list;
            ViewBag.Page = page;
            ViewBag.Num = num;
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> CancelOrder([FromBody] int orderId) // Nhận trực tiếp ID
        {
            if (orderId <= 0)
            {
                return BadRequest(new { message = "Mã hóa đơn không hợp lệ" });
            }

            // Tìm đơn hàng theo ID
            var order = _context.orders.FirstOrDefault(o => o.Id == orderId);
            if (order == null)
            {
                return NotFound(new { message = "Không tìm thấy đơn hàng" });
            }

            // Xóa đơn hàng
            _context.orders.Remove(order);
            await _context.SaveChangesAsync(); // Thao tác lưu thay đổi không đồng bộ

            // Gửi email không đồng bộ
            _ = Task.Run(async () =>
            {
                await Email.SendEmailAsync(order.Email, "Thông báo về đơn hàng", getNoiDungDeleteOrder());
            });

            return Ok(new { message = "Đơn hàng đã được xóa thành công" });
        }

        public string getNoiDungDeleteOrder()
        {
            return "<h1>Xin lỗi đơn hàng của bạn đã bị hủy</h1>";
        }


        [HttpPost]
        public async Task<IActionResult> ConfirmOrder([FromBody] int orderId)
        {
            if (orderId <= 0)
            {
                return BadRequest(new { message = "Mã hóa đơn không hợp lệ" });
            }

            // Tìm đơn hàng theo ID
            var order = _context.orders.FirstOrDefault(o => o.Id == orderId);
            if (order == null)
            {
                return NotFound(new { message = "Không tìm thấy đơn hàng" });
            }

            // Xử lý xác nhận đơn hàng
            order.Status = "Confirmed";
            _context.SaveChanges();

            // Gửi email không đồng bộ
            _ = Task.Run(async () =>
            {
                await Email.SendEmailAsync(order.Email, "Thông báo về đơn hàng", getNoiDung());
            });

            return Ok(new { message = "Đơn hàng đã được xác nhận thành công" });
        }

        public string getNoiDung()
        {
            string noidung = "<h1>Đơn hàng của bạn đã được xác nhận</h1>";
            return noidung;
        }




        public IActionResult OrderDetail(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("Order ID is required.");
            }
            int id1 = int.Parse(id);
            var order = _context.orders
            .Where(o => o.Id == id1)
            .FirstOrDefault();




            var orderDetails = _context.orderDetails
             .Where(od => od.OrderId == id1)
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














        public IActionResult AddProduct()
        {
            ViewBag.Categories = _context.categorys.ToList();
            return View();
        }
      
        // them sản phẩm
        [HttpPost]
        public IActionResult AddProduct(int id, string title, string brand, double price, int discount, int warranty, int inventoryNumber, string description, IFormFile thumbnail, DateTime createAt, DateTime updateAt, int cid, int numOfPur)
        {
            try
            {
                // Kiểm tra nếu sản phẩm đã tồn tại
                var existingProduct = _context.products.FirstOrDefault(p => p.Id == id);
                if (existingProduct != null)
                {
                    return Json(new { success = false, message = "Mã sản phẩm đã tồn tại!" });
                }

                // Xử lý tệp hình ảnh
                string thumbnailPath = null;
                if (thumbnail != null)
                {
                    // Thư mục lưu trữ hình ảnh
                    string uploadsFolder = Path.Combine("wwwroot/image");

                    // Đảm bảo thư mục tồn tại
                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }
                    string filePath = Path.Combine(uploadsFolder, thumbnail.FileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        thumbnail.CopyTo(fileStream);
                    }

                    // Lưu đường dẫn hình ảnh
                    thumbnailPath = $"/image/{thumbnail.FileName}";
                }

                // Tạo đối tượng sản phẩm mới
                var newProduct = new Product
                {
                    Id = id,
                    Title = title,
                    Brand = brand,
                    Price = price,
                    Discount = discount,
                    Warranty = warranty,
                    InventoryNumber = inventoryNumber,
                    Description = description,
                    Thumbnail = thumbnailPath,
                    CreateAt = createAt,
                    UpdateAt = updateAt,
                    Cid = cid,
                    NumOfPur = numOfPur,
                };
                _context.products.Add(newProduct);
                _context.SaveChanges();
                return Redirect("/Admin/ManagerProduct");
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        // Sửa sản phẩm(GET)
        [HttpGet]
        public IActionResult EditProduct(int id)
        {
            var product = _context.products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                TempData["Error"] = "Sản phẩm không tồn tại.";
                return RedirectToAction("ManagerProduct");
            }
            var category = _context.categorys.Select(c => new { c.Id, c.Name }).ToList();
            ViewBag.Categories = category;


            return View(product);
        }

        [HttpPost]
        public IActionResult EditProduct(int id, string title, string brand, double price, int discount, int warranty, int inventoryNumber, string description, IFormFile thumbnail, DateTime createAt, DateTime updateAt, int cid, int numOfPur)
        {
            try
            {
                // Kiểm tra nếu các tham số bắt buộc hợp lệ
                if (string.IsNullOrEmpty(title))
                {
                    return Json(new { success = false, message = "Tên sản phẩm không được để trống!" });
                }

                // Tìm sản phẩm theo ID
                var product = _context.products.FirstOrDefault(p => p.Id == id);
                if (product == null)
                {
                    return Json(new { success = false, message = "Sản phẩm không tồn tại!" });
                }

                // Cập nhật các thông tin của sản phẩm
                product.Title = title;
                product.Brand = brand;
                product.Price = price;
                product.Discount = discount;
                product.Warranty = warranty;
                product.InventoryNumber = inventoryNumber;
                product.Description = description;
                product.UpdateAt = updateAt;
                product.Cid = cid;
                product.NumOfPur = numOfPur;

                // Cập nhật hình ảnh nếu có
                if (thumbnail != null)
                {
                    string uploadsFolder = Path.Combine("wwwroot", "image");
                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    string filePath = Path.Combine(uploadsFolder, thumbnail.FileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        thumbnail.CopyTo(fileStream);
                    }

                    product.Thumbnail = $"/image/{thumbnail.FileName}";
                }
                _context.SaveChanges();
                return Redirect("/Admin/ManagerProduct");
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        [HttpPost]
        public IActionResult DeleteProduct(int Id)
        {
            try
            {
                var product = _context.products.FirstOrDefault(p => p.Id == Id);
                if (product == null)
                {
                    return Json(new { success = false, message = "Sản phẩm không tồn tại!" });
                }

                _context.products.Remove(product);  
                _context.SaveChanges(); 
                return Json(new { success = true, redirectUrl = Url.Action("ManagerProduct", "Admin") });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Có lỗi xảy ra! " + ex.Message });
            }
        }

        //Thêm danh mục
        [HttpPost]
        public IActionResult AddCategory(int id, string name, string thumbnail)
        {
            try
            {
                // Kiểm tra nếu danh mục đã tồn tại
                var existingCategory = _context.categorys.FirstOrDefault(c => c.Id == id);
                if (existingCategory != null)
                {
                    return Json(new { success = false, message = "Mã danh mục đã tồn tại!" });
                }

                // Tạo đối tượng danh mục mới và thêm vào cơ sở dữ liệu
                var newCategory = new Category
                {
                    Id = id,
                    Name = name,
                    Thumbnail = thumbnail // Giả sử bạn có trường Image trong bảng Category
                };

                _context.categorys.Add(newCategory);
                _context.SaveChanges();

                // Trả về phản hồi thành công
                return Json(new { success = true, redirectUrl = Url.Action("ManagerCategory", "Admin") });
            }
            catch (Exception ex)
            {
                // Ghi log lỗi và trả về thông báo lỗi chi tiết
                return Json(new { success = false, message = "Có lỗi xảy ra! " + ex.Message });
            }
        }


        // Sửa danh mục (GET)
        [HttpGet]
        public IActionResult EditCategory(int id)
        {
            var userSession = HttpContext.Session.GetString("user");
            if (string.IsNullOrEmpty(userSession))
            {
                return RedirectToAction("Login", "Home");
            }

            var category = _context.categorys.FirstOrDefault(c => c.Id == id);
            if (category == null)
            {
                TempData["Error"] = "Danh mục không tồn tại.";
                return RedirectToAction("ManagerCategory");
            }

            return View(category);
        }

        // Cập nhật danh mục (POST)
        [HttpPost]
        public IActionResult EditCategory(int Id, string Name)
        {
            try
            {
                // Kiểm tra nếu Id và Name hợp lệ
                if (string.IsNullOrEmpty(Name))
                {
                    return Json(new { success = false, message = "Tên danh mục không được để trống!" });
                }

                var category = _context.categorys.FirstOrDefault(c => c.Id == Id);
                if (category == null)
                {
                    return Json(new { success = false, message = "Danh mục không tồn tại!" });
                }

                category.Name = Name;
                _context.SaveChanges();

                // Sau khi cập nhật thành công, chuyển hướng người dùng về trang ManagerCategory
                return Json(new { success = true, redirectUrl = Url.Action("ManagerCategory", "Admin") });
            }
            catch (Exception ex)
            {
                // Ghi log lỗi và trả về thông báo lỗi chi tiết
                return Json(new { success = false, message = "Có lỗi xảy ra! " + ex.Message });
            }
        }
        //xóa danh mục
        [HttpPost]
        public IActionResult DeleteCategory(int Id)
        {
            try
            {
                // Kiểm tra nếu Id và Name hợp lệ
             
                var category = _context.categorys.FirstOrDefault(c => c.Id == Id);
                if (category == null)
                {
                    return Json(new { success = false, message = "Danh mục không tồn tại!" });
                }

                _context.categorys.Remove(category);  // Thực hiện xóa danh mục khỏi cơ sở dữ liệu
                _context.SaveChanges();  // Lưu thay đổi vào cơ sở dữ liệu


                // Sau khi cập nhật thành công, chuyển hướng người dùng về trang ManagerCategory
                return Json(new { success = true, redirectUrl = Url.Action("ManagerCategory", "Admin") });
            }
            catch (Exception ex)
            {
                // Ghi log lỗi và trả về thông báo lỗi chi tiết
                return Json(new { success = false, message = "Có lỗi xảy ra! " + ex.Message });
            }
        }



    }
}
