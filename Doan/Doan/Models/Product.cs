using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Doan.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }

        public string Brand { get; set; }
        public double Price { get; set; }
        public int Discount { get; set; }
        public int Warranty { get; set; }
        public int InventoryNumber { get; set; }
        public string Description { get; set; }
        public string Thumbnail { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }

        [ForeignKey("Category")]
        public int Cid { get; set; }
        public Category Category { get; set; }
        public int NumOfPur { get; set; }
        // Navigation property for images
        public ICollection<Image> Images { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; }
        public Product()
        {
        }

        public Product(int id, string title, double price, int discount, int inventoryNumber, string description, string thumbnail, int cid)
        {
            Id = id;
            Title = title;
            Price = price;
            Discount = discount;
            InventoryNumber = inventoryNumber;
            Description = description;
            Thumbnail = thumbnail;
            Cid = cid;
        }

        public Product(int id, string title, string brand, double price, int discount, int warranty, int inventoryNumber, string description, string thumbnail, DateTime createAt, DateTime updateAt, int cid, int numOfPur)
        {
            Id = id;
            Title = title;
            Brand = brand;
            Price = price;
            Discount = discount;
            Warranty = warranty;
            InventoryNumber = inventoryNumber;
            Description = description;
            Thumbnail = thumbnail;
            CreateAt = createAt;
            UpdateAt = updateAt;
            Cid = cid;
            NumOfPur = numOfPur;
        }



    }
}
