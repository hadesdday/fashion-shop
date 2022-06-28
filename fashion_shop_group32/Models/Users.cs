using System;

namespace fashion_shop_group32.Models
{
    public class Users
    {
        public string idUser { get; set; }
        public string userName { get; set; }
        public string tokken { get; set; }
        public string password { get; set; }
        public string role { get; set; }
        public string email { get; set; }
        public int idKhachHang { get; set; }
        public int active { get; set; }
        public DateTime createDate { get; set; }
        public DateTime updateDate { get; set; }

        public Users(string idUser, string userName, string tokken, string password, string role, string email, int idKhachHang, int active, Date createDate, Date updateDate)
        {
            this.idUser = idUser;
            this.userName = userName;
            this.tokken = tokken;
            this.password = password;
            this.role = role;
            this.email = email;
            this.idKhachHang = idKhachHang;
            this.active = active;
            this.createDate = createDate;
            this.updateDate = updateDate;
        }

    }
}