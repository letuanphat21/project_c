using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Doan.Models
{
    public class Image
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Product")]
        public int Pid { get; set; }
        public string? Image1 { get; set; }
        public string? Image2 { get; set; }
        public Product Product { get; set; }
        public Image()
        {
        }

        public Image(int id, int pid, string image1, string image2)
        {
            Id = id;
            Pid = pid;
            Image1 = image1;
            Image2 = image2;
        }

    }
}
