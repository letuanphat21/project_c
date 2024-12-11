namespace Doan.Models
{
    public class Discount
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public double  Discount_Value { get; set; }

        public DateTime Start_Date { get; set; }

        public DateTime End_Date { get; set; }

        public Discount(int id, string name, double discount_Value, DateTime start_Date, DateTime end_Date)
        {
            Id = id;
            Name = name;
            Discount_Value = discount_Value;
            Start_Date = start_Date;
            End_Date = end_Date;
        }
    }
}
