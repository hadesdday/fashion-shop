﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace fashion_shop_group32.Models
{
    public class Customer
    {
        public int IdCus { get; set; }
        public string NameCus { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string email { get; set; }
        public DateTime createDate { get; set; }
        public DateTime updateDate { get; set; }

        public Customer(int idCus, string nameCus, string address, string phone, string email, DateTime createDate, DateTime updateDate)
        {
            IdCus = idCus;
            NameCus = nameCus;
            Address = address;
            Phone = phone;
            this.email = email;
            this.createDate = createDate;
            this.updateDate = updateDate;
        }
    }

}