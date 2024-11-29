﻿namespace Web_project.Models
{
    public class User
    {
        public int Id { get; set; }
        public string User1 { get; set; }

        public string Fullname { get; set; }

        public string Password { get; set; }

        public bool IsGender { get; set; }

        public DateTime BirthDay { get; set; }

        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        public DateTime createdAt { get; set; }

        public DateTime updatedAt { get; set; }

        public bool IsAdmin { get; set; }

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

        public User(string user1, string fullname, string password, bool isGender, DateTime birthDay, string email, string phoneNumber, string address, DateTime createdAt, DateTime updatedAt, bool isAdmin)
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
        }
    }
}
