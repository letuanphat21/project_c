using System.ComponentModel.DataAnnotations;

namespace Doan.Models
{
    public class User
    {

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên người dùng là bắt buộc")]
        [StringLength(50, ErrorMessage = "Tên người dùng không được dài quá 50 ký tự")]
        public string User1 { get; set; }

        [Required(ErrorMessage = "Tên đầy đủ là bắt buộc")]
        [StringLength(100, ErrorMessage = "Tên đầy đủ không được dài quá 100 ký tự")]
        public string Fullname { get; set; }

        [Required(ErrorMessage = "Cần có mật khẩu")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Mật khẩu phải dài ít nhất 1 ký tự")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Giới tính là bắt buộc")]
        public bool IsGender { get; set; }

        [Required(ErrorMessage = "Ngày sinh là bắt buộc")]
        public DateTime BirthDay { get; set; }

        [Required(ErrorMessage = "Email là bắt buộc")]
        [EmailAddress(ErrorMessage = "Địa chỉ email không hợp lệ")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Số điện thoại là bắt buộc")]
        [Phone(ErrorMessage = "Số điện thoại không hợp lệ")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Địa chỉ là bắt buộc")]
        public string Address { get; set; } = string.Empty;

        public DateTime createdAt { get; set; } = DateTime.Now;
        public DateTime updatedAt { get; set; } = DateTime.Now;
        public bool IsAdmin { get; set; }
        public string RandomKey { get; set; }

        public bool IsConfirmEmail { get; set; }

        // Navigation property for related orders
        public ICollection<Order> Orders { get; set; }


        public User()
        {
        }

        public User(int id, string user1, string fullname, string password, bool isGender, DateTime birthDay, string email, string phoneNumber, string address, DateTime createdAt, DateTime updatedAt, bool isAdmin)
        {
            Id = id;
            User1 = user1;
            Fullname = fullname;
            Password = password;
            IsGender = isGender;
            BirthDay = birthDay;
            Email = email;
            PhoneNumber = phoneNumber;
            Address = address;
            this.createdAt = createdAt;
            this.updatedAt = updatedAt;
            IsAdmin = isAdmin;
        }

        public User(string user1, string fullname, string password, bool isGender, DateTime birthDay, string email, string phoneNumber, string address, DateTime createdAt, DateTime updatedAt, bool isAdmin, string randomKey,bool isConfirmEmail)
        {
            User1 = user1;
            Fullname = fullname;
            Password = password;
            IsGender = isGender;
            BirthDay = birthDay;
            Email = email;
            PhoneNumber = phoneNumber;
            Address = address;
            this.createdAt = createdAt;
            this.updatedAt = updatedAt;
            IsAdmin = isAdmin;
            RandomKey = randomKey;
            IsConfirmEmail = isConfirmEmail;
        }

       
    }
}
