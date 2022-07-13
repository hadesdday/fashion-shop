using Databases;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace fashion_shop_group32.Models.Dao
{
    public class CheckOutDao
    {

        private static CheckOutDao instance = null;
        public CheckOutDao()
        {
            //do nothing
        }
        public static CheckOutDao getInstance()
        {
            if (instance == null)
            {
                instance = new CheckOutDao();
            }
            return instance;
        }
        public static String generateId()
        {
            return Guid.NewGuid().ToString();
        }
        public static Boolean SaveBillNotUSer(string magg,string pttt,double trigia,string ten_kh,string diachi,string sodt,string email,List<CartDao> listBuy)
        {
            //innitial
            int trangthai = 1;
            DateTime createDate = DateTime.Now;
            DateTime updateDate = DateTime.Now;
            int rowsBillDetail = 0; 
            
            //save khach hang
            //ket noi toi database table khach hang

            MySqlConnection connectCus = KetNoi.GetDBConnection();
            connectCus.Open();
            string sqlCus = "insert into khachhang(id_khachhang,ten_kh,diachi,sodt,email,createdat,updatedat) " +
               "values(@id_khachhang,@ten_kh,@diachi,@sodt,@email,@createdat,@updatedat)";
            MySqlCommand cmd = new MySqlCommand(sqlCus, connectCus);
            int id_kh = (int)cmd.LastInsertedId;
            cmd.Parameters.AddWithValue("@id_khachhang", id_kh);
            cmd.Parameters.AddWithValue("@ten_kh", ten_kh);
            cmd.Parameters.AddWithValue("@diachi", diachi);
            cmd.Parameters.AddWithValue("@sodt", sodt);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@createdat", createDate);
            cmd.Parameters.AddWithValue("@updatedat", updateDate);
            int rowsCustomer = cmd.ExecuteNonQuery();
            //save bill
            int newIdkh = (int)cmd.LastInsertedId;
            String sqlSaveBill = "insert into hoadon(id_hoadon,id_khachhang,id_magg,mapttt,trigia,trangthai,createdat,updatedat) " +
               "values(@id_hoadon,@id_khachhang,@id_magg,@mapttt,@trigia,@trangthai,@createdat,@updatedat)";
            MySqlConnection connectBill = KetNoi.GetDBConnection();
            connectBill.Open();
            MySqlCommand commandBill = new MySqlCommand(sqlSaveBill, connectBill);
            int id_bill = (int)commandBill.LastInsertedId;
            commandBill.Parameters.AddWithValue("@id_hoadon", id_bill);
            commandBill.Parameters.AddWithValue("@id_khachhang", newIdkh);
            commandBill.Parameters.AddWithValue("@id_magg", magg);
            commandBill.Parameters.AddWithValue("@mapttt", pttt);
            commandBill.Parameters.AddWithValue("@trigia", trigia);
            commandBill.Parameters.AddWithValue("@trangthai", trangthai);
            commandBill.Parameters.AddWithValue("@createdat", createDate);
            commandBill.Parameters.AddWithValue("@updatedat", updateDate);
            int rowsBill = commandBill.ExecuteNonQuery();

            //save BillDetail
            int newId_bill= (int)commandBill.LastInsertedId;
            String sqlSaveBillDetail = "insert into chitiethoadon(id_hoadon,id_sanpham,soluong,createdat,updatedat) " +
                "values(@id_hoadon,@id_sanpham,@soluong,@createdat,@updatedat)";
            MySqlConnection connectBillDetail = KetNoi.GetDBConnection();
            connectBillDetail.Open();
            foreach (CartDao item in listBuy)
            {
                MySqlCommand commandBillDetail = new MySqlCommand(sqlSaveBillDetail, connectBillDetail);
                //int id_billdetail = (int)commandBillDetail.LastInsertedId;
                commandBillDetail.Parameters.AddWithValue("@id_hoadon", newId_bill);
                commandBillDetail.Parameters.AddWithValue("@id_sanpham", item.Pro.id_sanpham);
                commandBillDetail.Parameters.AddWithValue("@soluong", item.Pro.quantitySold);
                commandBillDetail.Parameters.AddWithValue("@createdat", createDate);
                commandBillDetail.Parameters.AddWithValue("@updatedat", updateDate);
                 rowsBillDetail = commandBillDetail.ExecuteNonQuery();
            }
            return rowsBill == 1 && rowsBillDetail == 1 && rowsCustomer == 1;
        }
        public static Boolean SaveBillUser(string magg, string pttt, double trigia,string id_kh, List<CartDao> listBuy)
        {
            //innitial
            int trangthai = 1;
            DateTime createDate = DateTime.Now;
            DateTime updateDate = DateTime.Now;

            int rowsBillDetail = 0;


            
            //save bill

            String sqlSaveBill = "insert into hoadon(id_hoadon,id_khachhang,id_magg,mapttt,trigia,trangthai,createdat,updatedat) " +
               "values(@id_hoadon,@id_khachhang,@id_magg,@mapttt,@trigia,@trangthai,@createdat,@updatedat)";
            MySqlConnection connectBill = KetNoi.GetDBConnection();
            connectBill.Open();
            MySqlCommand commandBill = new MySqlCommand(sqlSaveBill, connectBill);
            int id_bill = (int)commandBill.LastInsertedId;
            commandBill.Parameters.AddWithValue("@id_hoadon", id_bill);
            commandBill.Parameters.AddWithValue("@id_khachhang", id_kh);
            commandBill.Parameters.AddWithValue("@id_magg", magg);
            commandBill.Parameters.AddWithValue("@mapttt", pttt);
            commandBill.Parameters.AddWithValue("@trigia", trigia);
            commandBill.Parameters.AddWithValue("@trangthai", trangthai);
            commandBill.Parameters.AddWithValue("@createdat", createDate);
            commandBill.Parameters.AddWithValue("@updatedat", updateDate);
            int rowsBill = commandBill.ExecuteNonQuery();

            //save BillDetail
            int newId_bill = (int)commandBill.LastInsertedId;
            String sqlSaveBillDetail = "insert into chitiethoadon(id_hoadon,id_sanpham,soluong,createdat,updatedat) " +
                "values(@id_hoadon,@id_sanpham,@soluong,@createdat,@updatedat)";
            MySqlConnection connectBillDetail = KetNoi.GetDBConnection();
            connectBillDetail.Open();
            foreach (CartDao item in listBuy)
            {
                MySqlCommand commandBillDetail = new MySqlCommand(sqlSaveBillDetail, connectBillDetail);
                //int id_billdetail = (int)commandBillDetail.LastInsertedId;
                commandBillDetail.Parameters.AddWithValue("@id_hoadon", newId_bill);
                commandBillDetail.Parameters.AddWithValue("@id_sanpham", item.Pro.id_sanpham);
                commandBillDetail.Parameters.AddWithValue("@soluong", item.Pro.quantitySold);
                commandBillDetail.Parameters.AddWithValue("@createdat", createDate);
                commandBillDetail.Parameters.AddWithValue("@updatedat", updateDate);
                rowsBillDetail = commandBillDetail.ExecuteNonQuery();
            }
            return rowsBill == 1 && rowsBillDetail == 1 ;
        }
        public static string getCoupon(string couponId)
        {
            List<string> listCouponId = new List<string>();
            string sql = "select id_magg from magiamgia where id_magg=@id_magg";
            MySqlCommand command = new MySqlCommand();
            MySqlConnection connect = KetNoi.GetDBConnection();
            connect.Open();
            command.Connection = connect;
            command.CommandText = sql;
            command.Prepare();
            command.Parameters.AddWithValue("@id_magg", couponId);
            using (DbDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    listCouponId.Add(reader.GetString(0));
                }
                reader.Close();
                //Console.WriteLine(usersList[0]);
                if (listCouponId.Count != 1) return null;
                string coupon = listCouponId[0];
                
                //if (!user.userName.Equals(username) || verify(user.password, password) == false) return null;
                return coupon;
            }
        }
        public static List<PaymentMethod> GetPaymentMethods()
        {
            List<PaymentMethod> listPaymentMethods = new List<PaymentMethod>();
            string sql = "select mapttt,tenpttt from thanhtoan";
            MySqlCommand command = new MySqlCommand();
            MySqlConnection connect = KetNoi.GetDBConnection();
            connect.Open();
            command.Connection = connect;
            command.CommandText = sql;
            using (DbDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    listPaymentMethods.Add(new PaymentMethod(reader.GetString(0), reader.GetString(1)));
                }
                reader.Close();
            }
            return listPaymentMethods;
        }
    }
}