using Doan.Data;
using Doan.Models;
using Doan.Utils;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using Newtonsoft.Json;
using System.Security.Cryptography;

namespace Doan.Controllers
{
    public class AdminController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILogger<AdminController> _logger;
        private readonly ConnectDB _context;  // Khai báo _context
        
        public AdminController(ILogger<AdminController> logger, ConnectDB context, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _context = context;  // Khởi tạo _context từ DI container
            _webHostEnvironment = webHostEnvironment;
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
            var successMessage = TempData["Success"] as string;  // Truy cập thông báo từ TempData
            ViewBag.Success = successMessage;  // Gán lại cho ViewBag nếu cần
            var fail = TempData["Fail"] as string;
            ViewBag.Fail = fail;
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
        public IActionResult ManagerDiscount(string xpage, string search, string filter)
        {
            var userSession = HttpContext.Session.GetString("user");
            if (string.IsNullOrEmpty(userSession))
            {
                return RedirectToAction("Login", "Home");
            }

            var discounts = _context.discounts.AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                discounts = discounts.Where(d => d.Name.Contains(search));
            }

            if (!string.IsNullOrEmpty(filter))
            {
                var now = DateTime.Now;
                if (filter == "active")
                {
                    discounts = discounts.Where(d => d.Start_Date <= now && d.End_Date >= now);
                }
                else if (filter == "expired")
                {
                    discounts = discounts.Where(d => d.End_Date < now);
                }
            }

            var discountList = discounts.ToList();

            int page, numperpage = 5;
            int size = discountList.Count;
            int num = (size % numperpage == 0 ? (size / numperpage) : ((size / numperpage) + 1)); // số trang

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

            List<Discount> list = discountList.Skip(start).Take(end - start).ToList();

            ViewBag.Discounts = list;
            ViewBag.Page = page;
            ViewBag.Num = num;
            ViewBag.Search = search;
            ViewBag.Filter = filter;
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

        public List<Order> GetOrders()
        {
            return _context.orders.ToList();
        }
        public ActionResult SearchOrders(string search, string filter)
        {
            var orders=GetOrders();

            if (!string.IsNullOrEmpty(search))
            {
                orders = orders.Where(u => u.FullName.Contains(search) || u.Address.Contains(search)).ToList();
            }

            if (!string.IsNullOrEmpty(filter))
            {
                if (filter == "pending")
                {
                    orders = orders.Where(u => u.Status.Equals("pending", StringComparison.OrdinalIgnoreCase)).ToList();
                }
                else if (filter == "confirmed")
                {
                    orders = orders.Where(u => u.Status.Equals("confirmed", StringComparison.OrdinalIgnoreCase)).ToList();
                }
            }

            return PartialView("_OrderTable", orders);
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
            var order = await _context.orders.FirstOrDefaultAsync(o => o.Id == orderId);
            if (order == null)
            {
                return NotFound(new { message = "Không tìm thấy đơn hàng" });
            }

            // Xử lý xác nhận đơn hàng
            order.Status = "Confirmed";
            await _context.SaveChangesAsync();

            var orderDetails = await _context.orderDetails
                               .Where(od => od.OrderId == orderId)
                               .ToListAsync();

            foreach (OrderDetail od in orderDetails)
            {
                var product = await _context.products.FirstOrDefaultAsync(p => p.Id == od.ProductId);
                if (product == null)
                {
                    // Xử lý khi sản phẩm không tìm thấy, có thể bỏ qua hoặc trả về lỗi
                    continue;
                }
                product.InventoryNumber -= od.Quantity;
                product.NumOfPur += od.Quantity; // Cân nhắc tăng bằng số lượng đã mua
            }
            await _context.SaveChangesAsync();

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

        [HttpPost]
        public IActionResult DeleteCategory(int id)
        {
            // Tìm danh mục theo id
            var category = _context.categorys.FirstOrDefault(c => c.Id == id);

            if (category != null)
            {
                // Xóa danh mục
                _context.categorys.Remove(category);
                _context.SaveChanges();

                // Xóa hình ảnh thumbnail của danh mục trong thư mục
                var uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "image/categorys");
                var filePath = Path.Combine(uploadPath, category.Thumbnail);

                if (!string.IsNullOrEmpty(category.Thumbnail) && System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
            }

            // Chuyển hướng về trang quản lý danh mục
            return RedirectToAction("ManagerCategory");
        }

        public IActionResult AddCategory(IFormFile Thumbnail, string Name)
        {
            if (Thumbnail != null && Thumbnail.Length > 0)
            {
                // Đường dẫn thư mục lưu file
                var uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "image/categorys");

                // Đảm bảo thư mục tồn tại
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }

                // Tạo tên file không trùng
                var fileName = Path.GetFileNameWithoutExtension(Thumbnail.FileName);
                var fileExtension = Path.GetExtension(Thumbnail.FileName);
                var newFileName = fileName + fileExtension;
                var filePath = Path.Combine(uploadPath, newFileName);

                // Kiểm tra nếu tên file đã tồn tại và đổi tên ngay lập tức
                int counter = 1;
                while (System.IO.File.Exists(filePath))
                {
                    newFileName = $"{fileName}_{counter}{fileExtension}";
                    filePath = Path.Combine(uploadPath, newFileName);
                    counter++;
                }

                // Lưu file vào thư mục
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    Thumbnail.CopyTo(stream);
                }

                // Thêm danh mục mới vào database
                var newCategory = new Category
                {
                    Name = Name,
                    Thumbnail = newFileName // Lưu tên file mới vào cơ sở dữ liệu
                };

                _context.categorys.Add(newCategory);
                _context.SaveChanges();
            }

            // Chuyển hướng về trang quản lý danh mục
            return RedirectToAction("ManagerCategory");
        }

        public IActionResult EditCategory(string id)
        {
            int idc= int.Parse(id);
            var category = _context.categorys.Find(idc);  // Lấy danh mục theo id
            if (category == null)
            {
                return NotFound();
            }

            ViewBag.Category = category;
            return View();
        }
        [HttpPost]
        public IActionResult EditCategory(int id, string name, IFormFile Thumbnail)
        {
            var category = _context.categorys.Find(id);  // Lấy danh mục theo id
            if (category == null)
            {
                return NotFound();
            }

            // Lưu tên danh mục
            category.Name = name;

            if (Thumbnail != null && Thumbnail.Length > 0)
            {
                // Đường dẫn thư mục lưu file
                var uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "image/categorys");

                // Đảm bảo thư mục tồn tại
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }

                // Xóa hình ảnh cũ
                if (!string.IsNullOrEmpty(category.Thumbnail))
                {
                    var oldFilePath = Path.Combine(uploadPath, category.Thumbnail);
                    if (System.IO.File.Exists(oldFilePath))
                    {
                        System.IO.File.Delete(oldFilePath);
                    }
                }

                // Tạo tên file không trùng
                var fileName = Path.GetFileNameWithoutExtension(Thumbnail.FileName);
                var fileExtension = Path.GetExtension(Thumbnail.FileName);
                var newFileName = fileName + fileExtension;
                var filePath = Path.Combine(uploadPath, newFileName);

                int counter = 1;
                while (System.IO.File.Exists(filePath))
                {
                    newFileName = $"{fileName}_{counter}{fileExtension}";
                    filePath = Path.Combine(uploadPath, newFileName);
                    counter++;
                }

                // Lưu file vào thư mục
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    Thumbnail.CopyTo(stream);
                }

                // Cập nhật tên mới vào danh mục
                category.Thumbnail = newFileName;
            }

            _context.SaveChanges();
            ViewBag.Category = category;
            return RedirectToAction("ManagerCategory");
        }


        [HttpPost]
        public IActionResult DeleteProduct(int id)
        {
            // Xóa hình ảnh liên quan đến sản phẩm
            var image = _context.images.FirstOrDefault(i => i.Pid == id);

            if (image != null)
            {
                var uploadPath1 = Path.Combine(_webHostEnvironment.WebRootPath, "image/details");
                var imagePath1 = Path.Combine(uploadPath1, image.Image2);

                if (System.IO.File.Exists(imagePath1))
                {
                    System.IO.File.Delete(imagePath1);
                }

                _context.images.Remove(image);
                _context.SaveChanges();
            }
            // Xóa sản phẩm
            var product = _context.products.Find(id);
            if (product != null)
            {
                _context.products.Remove(product);
                _context.SaveChanges();

                var uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "image/thumbnail");
                var imagePath = Path.Combine(uploadPath, product.Thumbnail);

                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }

            return RedirectToAction("ManagerProduct");
        }



        public IActionResult AddProduct()
        {
            List<Category> listCategory = _context.categorys.ToList();
            ViewBag.Category = listCategory;
            return View();
        }
        [HttpPost]
        public IActionResult AddProduct(IFormFile Thumbnail,IFormFile? image2, string title, string brand, double price, int discount, int warranty, int inventoryNumber, string description, string Category)
        {
            ViewBag.Fail = "";
            ViewBag.Success = "";
            if (Thumbnail != null && Thumbnail.Length > 0)
            {
                // Đường dẫn thư mục lưu file
                var uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "image/thumbnail");

                // Đảm bảo thư mục tồn tại
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }

                // Tạo tên file không trùng
                var fileName = Path.GetFileNameWithoutExtension(Thumbnail.FileName);
                var fileExtension = Path.GetExtension(Thumbnail.FileName);
                var newFileName = fileName + fileExtension;
                var filePath = Path.Combine(uploadPath, newFileName);

                // Kiểm tra nếu tên file đã tồn tại và đổi tên ngay lập tức
                int counter = 1;
                while (System.IO.File.Exists(filePath))
                {
                    newFileName = $"{fileName}_{counter}{fileExtension}";
                    filePath = Path.Combine(uploadPath, newFileName);
                    counter++;
                }

                // Lưu file vào thư mục
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    Thumbnail.CopyTo(stream);
                }

                string image2Path = null;
                if (image2 != null && image2.Length > 0)
                {
                    // Đường dẫn thư mục lưu file details
                    var detailsPath = Path.Combine(_webHostEnvironment.WebRootPath, "image/details");

                    // Đảm bảo thư mục tồn tại
                    if (!Directory.Exists(detailsPath))
                    {
                        Directory.CreateDirectory(detailsPath);
                    }

                    // Tạo tên file không trùng
                    var image2FileName = Path.GetFileNameWithoutExtension(image2.FileName);
                    var image2FileExtension = Path.GetExtension(image2.FileName);
                    var newImage2FileName = image2FileName + image2FileExtension;
                    var image2FilePath = Path.Combine(detailsPath, newImage2FileName);

                    // Kiểm tra nếu tên file đã tồn tại và đổi tên ngay lập tức
                    int image2Counter = 1;
                    while (System.IO.File.Exists(image2FilePath))
                    {
                        newImage2FileName = $"{image2FileName}_{image2Counter}{image2FileExtension}";
                        image2FilePath = Path.Combine(detailsPath, newImage2FileName);
                        image2Counter++;
                    }

                    // Lưu file vào thư mục
                    using (var image2Stream = new FileStream(image2FilePath, FileMode.Create))
                    {
                        image2.CopyTo(image2Stream);
                    }

                    image2Path = newImage2FileName; // Lưu tên file vào biến
                }
                // Tạo sản phẩm mới với đường dẫn hình ảnh
                int cid = int.Parse(Category);
                var product = new Product
                {
                    Title = title,
                    Brand = brand,
                    Price = price,
                    Discount = discount,
                    Warranty = warranty,
                    Description = description,
                    InventoryNumber = inventoryNumber,
                    Thumbnail = newFileName, // Lưu tên file mới vào cơ sở dữ liệu
                    CreateAt = DateTime.Now,
                    UpdateAt = DateTime.Now,
                    Cid = cid,
                    NumOfPur = 0

                };


                // Gọi dịch vụ hoặc phương thức để thêm sản phẩm vào cơ sở dữ liệu
                _context.products.Add(product);
                _context.SaveChanges();


                var newImage = new Image
                {
                    Pid =product.Id,
                    Image1=newFileName,
                    Image2=image2Path
                };
                _context.images.Add(newImage);
                _context.SaveChanges();

                // Chuyển hướng hoặc hiển thị thông báo thành công
                ViewBag.Success = "Thêm sản phẩm thành công";
                TempData["Success"] = ViewBag.Success;
                return RedirectToAction("ManagerProduct");
            }
            else
            {
                ViewBag.Fail = "thêm sản phẩm thất bại";
                TempData["Fail"]= ViewBag.Fail;
                return RedirectToAction("ManagerProduct");
            }
        }


        public IActionResult EditProduct(string id)
        {
            int pid = int.Parse(id);
            var image = _context.images.FirstOrDefault(i => i.Pid == pid);
            var product = _context.products.FirstOrDefault(p => p.Id == pid);
            var categories = _context.categorys.ToList(); // Lấy danh sách thể loại

            ViewBag.Product = product;
            ViewBag.Image = image;
            ViewBag.Category = categories; // Thêm danh sách thể loại vào ViewBag

            return View();
        }
        [HttpPost]
        public IActionResult EditProduct(int pid, string title, string brand, double price, int discount, int warranty, int inventoryNumber, string description, int category, IFormFile thumbnail, IFormFile image2)
        {
            var product = _context.products.Find(pid);  // Lấy sản phẩm theo id
            var image = _context.images.FirstOrDefault(i => i.Pid == pid);

            if (product == null)
            {
                return NotFound();
            }

            // Lưu thông tin sản phẩm
            product.Title = title;
            product.Brand = brand;
            product.Price = price;
            product.Discount = discount;
            product.Warranty = warranty;
            product.InventoryNumber = inventoryNumber;
            product.Description = description;
            product.Cid = category;  // Lưu chọn thể loại vào Cid

            // Xử lý hình ảnh thumbnail
            if (thumbnail != null && thumbnail.Length > 0)
            {
                var uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "image/thumbnail");

                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }

                // Xóa hình ảnh cũ
                if (!string.IsNullOrEmpty(product.Thumbnail))
                {
                    var oldFilePath = Path.Combine(uploadPath, product.Thumbnail);
                    if (System.IO.File.Exists(oldFilePath))
                    {
                        System.IO.File.Delete(oldFilePath);
                    }
                }

                var fileName = Path.GetFileNameWithoutExtension(thumbnail.FileName);
                var fileExtension = Path.GetExtension(thumbnail.FileName);
                var newFileName = fileName + fileExtension;
                var filePath = Path.Combine(uploadPath, newFileName);

                int counter = 1;
                while (System.IO.File.Exists(filePath))
                {
                    newFileName = $"{fileName}_{counter}{fileExtension}";
                    filePath = Path.Combine(uploadPath, newFileName);
                    counter++;
                }

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    thumbnail.CopyTo(stream);
                }

                product.Thumbnail = newFileName;
            }

            // Xử lý hình ảnh khác
            if (image2 != null && image2.Length > 0)
            {
                var uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "image/details");

                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }

                if (!string.IsNullOrEmpty(image.Image2))
                {
                    var oldFilePath = Path.Combine(uploadPath, image.Image2);
                    if (System.IO.File.Exists(oldFilePath))
                    {
                        System.IO.File.Delete(oldFilePath);
                    }
                }

                var fileName = Path.GetFileNameWithoutExtension(image2.FileName);
                var fileExtension = Path.GetExtension(image2.FileName);
                var newFileName = fileName + fileExtension;
                var filePath = Path.Combine(uploadPath, newFileName);

                int counter = 1;
                while (System.IO.File.Exists(filePath))
                {
                    newFileName = $"{fileName}_{counter}{fileExtension}";
                    filePath = Path.Combine(uploadPath, newFileName);
                    counter++;
                }

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    image2.CopyTo(stream);
                }
                image.Image1 = newFileName;
                image.Image2 = newFileName;
            }

            _context.SaveChanges();  // Lưu thay đổi vào CSDL
            return RedirectToAction("ManagerProduct", "Admin");
        }







        //Tân thêm
        [HttpPost]
        public IActionResult AddUser(string username, string fullname, string email, string phoneNumber, string role, string address, string password, bool isGender, DateTime birthDay)
        {
            try
            {
                // Kiểm tra trùng lặp tên người dùng
                var existingUserByUsername = _context.users.FirstOrDefault(u => u.User1 == username);
                if (existingUserByUsername != null)
                {
                    return Json(new { success = false, message = "Tên người dùng đã tồn tại!" });
                }

                // Kiểm tra trùng lặp email
                var existingUserByEmail = _context.users.FirstOrDefault(u => u.Email == email);
                if (existingUserByEmail != null)
                {
                    return Json(new { success = false, message = "Email đã tồn tại!" });
                }

                // Sinh key ngẫu nhiên và mã hóa mật khẩu
                string keyGenerate = MyUtils.keyGenerator();
                string randomKey = keyGenerate;
                string pass = MyUtils.ToMd5Hash(password, keyGenerate);

                // Tạo người dùng mới
                var newUser = new User
                {
                    User1 = username,
                    Fullname = fullname,
                    Email = email,
                    PhoneNumber = phoneNumber,
                    IsAdmin = role == "Admin",
                    Address = address,
                    Password = pass,
                    IsGender = isGender,
                    BirthDay = birthDay,
                    RandomKey = randomKey,
                    createdAt = DateTime.Now,
                    updatedAt = DateTime.Now,
                    IsConfirmEmail = true
                };

                // Lưu vào cơ sở dữ liệu
                _context.users.Add(newUser);
                _context.SaveChanges();

                return Json(new { success = true, redirectUrl = Url.Action("ManagerUser", "Admin") });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while saving the entity changes.");
                return Json(new { success = false, message = "Có lỗi xảy ra! " + ex.InnerException?.Message });
            }
        }
        //Tấn Thêm

        [HttpPost]
        public IActionResult DeleteUser(int id)
        {
            try
            {
                var user = _context.users.FirstOrDefault(u => u.Id == id);
                if (user == null)
                {
                    return Json(new { success = false, message = "Người dùng không tồn tại!" });
                }

                _context.users.Remove(user);
                _context.SaveChanges();

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi xóa tài khoản.");
                return Json(new { success = false, message = "Có lỗi xảy ra!" });
            }
        }
        // Tấn Them 
        [HttpPost]
        public IActionResult EditUser(int userId, string username,  string fullname, string email, string phoneNumber, bool IsAdmin, string address, bool isGender, DateTime birthDay,bool IsConfirm)
        {
            try
            {
                // Tìm user trong database
                var user = _context.users.FirstOrDefault(u => u.Id == userId);
                if (user == null)
                {
                    return Json(new { success = false, message = "Người dùng không tồn tại!" });
                }



                // Cập nhật thông tin người dùng
                user.User1 = username;
                user.Fullname = fullname;
                user.Email = email;
                user.PhoneNumber = phoneNumber;
                user.IsAdmin = IsAdmin;
                user.Address = address;
                user.IsGender = isGender;
                user.BirthDay = birthDay;
                user.updatedAt = DateTime.Now;
                user.IsConfirmEmail = IsConfirm;

                _context.SaveChanges();

                return Json(new { success = true, redirectUrl = Url.Action("ManagerUser", "Admin") });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the user.");
                return Json(new { success = false, message = "Có lỗi xảy ra! " + ex.InnerException?.Message });
            }
        }

        //Tấn Thêm 
        [HttpGet]
        public IActionResult GetUserById(int id)
        {
            var user = _context.users
                .Where(u => u.Id == id)
                .Select(u => new
                {
                    id = u.Id,
                    username = u.User1,
                    fullname = u.Fullname,
                    password=u.Password,
                    email = u.Email,
                    phoneNumber = u.PhoneNumber,
                    role = u.IsAdmin,
                    address = u.Address,
                    isGender = u.IsGender,
                    birthDay = u.BirthDay,
                    emailConfirm=u.IsConfirmEmail
                })
                .FirstOrDefault();

            if (user == null)
            {
                return Json(new { success = false, message = "Người dùng không tồn tại!" });
            }

            return Json(new { success = true, data = user });
        }


        // tấn thêm 
        [HttpGet]
        public IActionResult CreateDiscount()
        {
            return View();
        }


        // tấn thêm 
        [HttpPost]
        public IActionResult CreateDiscount(Discount discount)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Tự động tăng mã giảm giá
                    var lastDiscount = _context.discounts.OrderByDescending(d => d.Id).FirstOrDefault();
                    discount.Id = (lastDiscount != null) ? lastDiscount.Id + 1 : 1;

                    // Chỉ lấy ngày tháng năm cho ngày bắt đầu và ngày kết thúc
                    discount.Start_Date = discount.Start_Date.Date;
                    discount.End_Date = discount.End_Date.Date;

                    _context.discounts.Add(discount);
                    _context.SaveChanges();
                    return RedirectToAction("ManagerDiscount");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while saving the entity changes.");
                    return Json(new { success = false, message = "Có lỗi xảy ra! " + ex.InnerException?.Message });
                }
            }
            else
            {
                return View(discount);
            }
        }
        // Tấn Thêm 

        [HttpGet]
        public IActionResult EditDiscount(int id)
        {
            var discount = _context.discounts.Find(id);
            return View(discount);
        }
        // Tấn thêm

        [HttpPost]
        public IActionResult EditDiscount(Discount discount)
        {
            var existingDiscount = _context.discounts.Find(discount.Id);
            if (existingDiscount != null)
            {
                existingDiscount.Name = discount.Name;
                existingDiscount.Discount_Value = discount.Discount_Value;
                existingDiscount.Start_Date = discount.Start_Date.Date;
                existingDiscount.End_Date = discount.End_Date.Date;
                _context.SaveChanges();
            }
            return RedirectToAction("ManagerDiscount");
        }
        // tấn Thêm 
        [HttpPost]
        public IActionResult DeleteDiscount(int id)
        {
            try
            {
                var discount = _context.discounts.Find(id);
                if (discount == null)
                {
                    return Json(new { success = false, message = "Thẻ giảm giá không tồn tại!" });
                }

                _context.discounts.Remove(discount);
                _context.SaveChanges();

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Có lỗi xảy ra! " + ex.Message });
            }
        }
        // Tấn Thêm 

        public List<User> GetUsers()
        {
            return _context.users.ToList();
        }
        public ActionResult SearchUsers(string search, string filter)
        {
            var users = GetUsers();

            if (!string.IsNullOrEmpty(search))
            {
                users = users.Where(u => u.User1.Contains(search) || u.Fullname.Contains(search)).ToList();
            }

            if (!string.IsNullOrEmpty(filter))
            {
                if (filter == "admin")
                {
                    users = users.Where(u => u.IsAdmin).ToList();
                }
                else if (filter == "user")
                {
                    users = users.Where(u => !u.IsAdmin).ToList();
                }
            }

            return PartialView("_UserTable", users);
        }
        // Tấn Thêm
        public IActionResult SearchDiscounts(string search, string filter)
        {
            var discounts = _context.discounts.AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                discounts = discounts.Where(d => d.Name.Contains(search));
            }

            if (!string.IsNullOrEmpty(filter))
            {
                var now = DateTime.Now;
                if (filter == "active")
                {
                    discounts = discounts.Where(d => d.Start_Date <= now && d.End_Date >= now);
                }
                else if (filter == "expired")
                {
                    discounts = discounts.Where(d => d.End_Date < now);
                }
            }

            var discountList = discounts.ToList();

            return PartialView("_DiscountTablePartial", discountList);
        }










    }
}
