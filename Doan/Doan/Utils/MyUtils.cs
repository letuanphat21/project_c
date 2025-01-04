using Doan.Models;
using System.Security.Cryptography;
using System.Text;

namespace Doan.Utils
{
    public class MyUtils
    {
        public static String keyGenerator(int length = 10)
        {
            var value = "absf@kklmihs!jgasj#123giwj123gnajgalsdj521lkfMLGAJ@!123&^#%adsfad1fjLKJFKLANGNAKFJKSKL";
            var sb = new StringBuilder();
            var rd = new Random();
            for (int i = 0; i < length; i++)
            {
                sb.Append(value[rd.Next(0, value.Length)]);
            }
            return sb.ToString();
        }
        public static string ToMd5Hash(string password, string? randomKey)
        {
            using (var md5 = MD5.Create())
            {
                byte[] data = md5.ComputeHash(Encoding.UTF8.GetBytes(string.Concat(password, randomKey)));
                StringBuilder sBuilder = new StringBuilder();
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }
                return sBuilder.ToString();


            }

        }

        public static List<Product> getListBypageProduct(List<Product> products, int start, int end)
        {
            List<Product> products1 = new List<Product>();
            for (int i = start; i < end; i++)
            {
                products1.Add(products[i]);
            }
            return products1;
        }


        public static List<User> getListBypageUser(List<User> users, int start, int end)
        {
            List<User> users1 = new List<User>();
            for (int i = start; i < end; i++)
            {
                users1.Add(users[i]);
            }
            return users1;
        }


        public static List<Category> getListBypageCate(List<Category> category, int start, int end)
        {
            List<Category> category1 = new List<Category>();
            for (int i = start; i < end; i++)
            {
                category1.Add(category[i]);
            }
            return category1;
        }


        public static List<Discount> getListBypageDiscount(List<Discount> discount, int start, int end)
        {
            List<Discount> discount1 = new List<Discount>();
            for (int i = start; i < end; i++)
            {
                discount1.Add(discount[i]);
            }
            return discount1;
        }


        public static List<Order> getListBypageOrder(List<Order> order, int start, int end)
        {
            List<Order> order1 = new List<Order>();
            for (int i = start; i < end; i++)
            {
                order1.Add(order[i]);
            }
            return order1;
        }
        public static string GenerateOTP(int length)
        {
            if (length <= 0)
            {
                throw new ArgumentException("Độ dài OTP phải lớn hơn 0.");
            }

            const string digits = "0123456789";
            char[] otp = new char[length];

            using (var rng = RandomNumberGenerator.Create())
            {
                byte[] randomBytes = new byte[length];
                rng.GetBytes(randomBytes);

                for (int i = 0; i < length; i++)
                {
                    otp[i] = digits[randomBytes[i] % digits.Length];
                }
            }

            return new string(otp);
        }








    }
}
