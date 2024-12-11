using System.ComponentModel.DataAnnotations;

namespace Doan.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public string Thumbnail { get; set; }
        // Navigation property
        public ICollection<Product> Products { get; set; }


        public Category() { }
        public Category(int id, string name, string thumbnail)
        {
            Id = id;
            Name = name;
            Thumbnail = thumbnail;
        }


    }
}
