using System.ComponentModel.DataAnnotations.Schema;

namespace Doan.Models
{
    public class OrderDetail
    {
        public int Id { get; set; }
        [ForeignKey("Order")]
        public int OrderId { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }

        public double Price { get; set; }


        public int Quantity { get; set; }

        public double SubTotal { get; set; }


        // Navigation property để lấy thông tin về Order
        public Order Order { get; set; }

        // Navigation property để lấy thông tin về Product
        public Product Product { get; set; }

    }
}
