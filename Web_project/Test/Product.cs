using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    internal class Product
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public double Price { get; set; }
        public int Discount { get; set; }
        public int Warranty { get; set; }
        public int InventoryNumber { get; set; }
        public string Description { get; set; }
        public string Thumbnail { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public int Cid { get; set; }

        public Product(int id, string title, double price, int discount,  int inventoryNumber, string description, string thumbnail,  int cid)
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

        public override string ToString()
        {
            return $"ID: {Id}, " +
                   $"Title: {Title}, " +
                   $"Price: {Price:C2}, " +
                   $"Discount: {Discount}%, " +
                   $"Inventory: {InventoryNumber}, " +
                   $"Description: {Description}, " +
                   $"Thumbnail: {Thumbnail}, " +
                   $"Category ID: {Cid},";
                 
        }



    }
}
