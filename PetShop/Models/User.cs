﻿namespace PetShop.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string? Email { get; set; }
        public string PhoneNumber { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string? RefreshToken { get; set; }
       
    }
}
