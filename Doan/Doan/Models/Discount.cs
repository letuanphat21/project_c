using System.ComponentModel.DataAnnotations;

namespace Doan.Models
{
    public class Discount
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên là bắt buộc")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Giá trị chiết khấu là bắt buộc")]
        [Range(0, 100, ErrorMessage = "Giá trị giảm giá phải nằm trong khoảng từ 0 đến 100")]
        public double Discount_Value { get; set; }

        [Required(ErrorMessage = "Ngày bắt đầu là bắt buộc")]
        public DateTime Start_Date { get; set; }

        [Required(ErrorMessage = "Ngày kết thúc là bắt buộc")]
        [DateGreaterThan("Start_Date", ErrorMessage = "Ngày kết thúc phải lớn hơn ngày bắt đầu")]
        public DateTime End_Date { get; set; }

        public Discount(int id, string name, double discount_Value, DateTime start_Date, DateTime end_Date)
        {
            Id = id;
            Name = name;
            Discount_Value = discount_Value;
            Start_Date = start_Date;
            End_Date = end_Date;
        }

        public Discount() { }
    }

    public class DateGreaterThanAttribute : ValidationAttribute
    {
        private readonly string _comparisonProperty;

        public DateGreaterThanAttribute(string comparisonProperty)
        {
            _comparisonProperty = comparisonProperty;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var currentValue = (DateTime)value;

            var property = validationContext.ObjectType.GetProperty(_comparisonProperty);

            if (property == null)
                throw new ArgumentException("Property with this name not found");

            var comparisonValue = (DateTime)property.GetValue(validationContext.ObjectInstance);

            if (currentValue <= comparisonValue)
                return new ValidationResult(ErrorMessage);

            return ValidationResult.Success;
        }
    }
}
