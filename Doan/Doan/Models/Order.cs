using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Doan.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        public string? Note { get; set; }


        public DateTime OrderDate { get; set; }

        public string Status { get; set; }

        public double TotalMoney { get; set; }

        public string PaymentMethod { get; set; }

        // Navigation property for the related User
        public User User { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; }

        public Order()
        {
        }

        public Order(int id, int userId, string fullName, string email, string phoneNumber, string address, string note, DateTime orderDate, string status, double totalMoney, string paymentMethod)
        {
            Id = id;
            UserId = userId;
            FullName = fullName;
            Email = email;
            PhoneNumber = phoneNumber;
            Address = address;
            Note = note;
            OrderDate = orderDate;
            Status = status;
            TotalMoney = totalMoney;
            PaymentMethod = paymentMethod;
        }

        public Order(int userId, string fullName, string email, string phoneNumber, string address, string note, DateTime orderDate, string status, double totalMoney, string paymentMethod)
        {
            UserId = userId;
            FullName = fullName;
            Email = email;
            PhoneNumber = phoneNumber;
            Address = address;
            Note = note;
            OrderDate = orderDate;
            Status = status;
            TotalMoney = totalMoney;
            PaymentMethod = paymentMethod;
        }
    }
}
