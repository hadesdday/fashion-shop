using Microsoft.OData.Edm;
using System;

namespace fashion_shop_group32.Models
{
    public class Users
    {
        public Users()
        {
        }

        public string username { get; set; }
        public string password { get; set; }
        public string role { get; set; }
        public string email { get; set; }
        public int id_khachhang { get; set; }
        public int active { get; set; }
        public string token { get; set; }

        public Users(string username, string token, string password, string role, string email, int id_khachhang, int active)
        {
            this.username = username;
            this.password = password;
            this.role = role;
            this.email = email;
            this.id_khachhang = id_khachhang;
            this.active = active;
            this.token = token;
        }
    }
}