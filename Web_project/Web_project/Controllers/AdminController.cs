using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Configuration;
using Web_project.Dao;
using Web_project.Models;

namespace Web_project.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            ProductDAO pd = new ProductDAO();
            List<Product> list = pd.SelectAll();
            CategoryDAO cd = new CategoryDAO();
            List<Category> listc = cd.SelectAll();
            UserDAO ud = new UserDAO();
            List<User> listu = ud.SelectAll();
            int soLuongUser= listu.Count;
            int soLuongProduct = list.Count;
            int soLuongCategory = listc.Count;
            ViewBag.TotalProducts = soLuongProduct;
            ViewBag.TotalCategories = soLuongCategory;
            ViewBag.TotalUsers= soLuongUser;
            return View();
        }

        public IActionResult ManagerUser(string xpage)
        {

            var userSession = HttpContext.Session.GetString("user");
            var user = JsonConvert.DeserializeObject<User>(userSession);
            UserDAO pd = new UserDAO();
            List<User> listu = pd.SelectAll();

            int page, numperpage = 5;
            int size =listu.Count;
            int num = (size % numperpage == 0 ? (size / numperpage) : ((size / numperpage) + 1));// số trang

            if(xpage == null)
            {
                page = 1;
            }else
            {
                page = int.Parse(xpage);
            }


            int start, end;
            start = (page - 1) * numperpage;
            end=Math.Min(page*numperpage, size);

            List<User> list = pd.getListBypage(listu,start,end);

            ViewBag.Users= list;
            ViewBag.Page = page;
            ViewBag.Num=num;
            return View();
        }

        public IActionResult ManagerProduct(string xpage)
        {

            var userSession = HttpContext.Session.GetString("user");
            var user = JsonConvert.DeserializeObject<User>(userSession);
            ProductDAO pd = new ProductDAO();
            List<Product> listu =pd.SelectAll() ;

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

            List<Product> list = pd.getListBypage(listu, start, end);

            ViewBag.Products = list;
            ViewBag.Page = page;
            ViewBag.Num = num;
            return View();
        }
    }
}
