using System;

namespace OOAD.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string StudentId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Faculty { get; set; }
        public string Telephon { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }


    }
}