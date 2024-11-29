namespace Web_project.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Thumbnail {  get; set; }



        public Category() { }
        public Category(int id, string name, string thumbnail)
        {
            Id = id;
            Name = name;
            Thumbnail = thumbnail;
        }
    }
}
