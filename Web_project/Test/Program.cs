using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ProductDAO productDAO = new ProductDAO();
            List<Product> products = productDAO.SelectAll();
            foreach (var item in products)
            {
                Console.WriteLine(item);
                
            }
        }
    }
}
