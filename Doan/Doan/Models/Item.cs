namespace Doan.Models
{
    public class Item
    {
        public Product product { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; } // giá chỗ này có thể khác nếu muốn thêm gì

        public Item(Product product, int quantity, double price)
        {
            this.product = product;
            Quantity = quantity;
            Price = price;
        }


    }
}
