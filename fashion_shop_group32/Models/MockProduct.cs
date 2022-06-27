using Databases;
using MySqlConnector;
using System;
using System.Collections.Generic;

namespace fashion_shop_group32.Models
{
    public class MockProduct : IProduct
    {
        private List<Product> _productList;
        public MockProduct()
        {
            _productList = new List<Product>()
            //{
            //    new Product("sp001", "san pham 1", "noi-com", 1000, "kk01", "phillips", 10, "1"),
            //    new Product("sp001", "san pham 1", "noi-com", 1000, "kk01", "phillips", 10, "1"),
            //    new Product("sp001", "san pham 1", "noi-com", 1000, "kk01", "phillips", 10, "1")
            //}
            ;

        }
        public Boolean isContain(string name)
        {
            for (int i = 0; i < _productList.Count; i++)
            {
                if (_productList[i].ten_sp == name) return true;
            }
            return false;
        }
        public Boolean isContainString(List<string> list, string s)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] == s) return true;
            }
            return false;
        }
        public IEnumerable<Product> GetProductsByCategory(string cat)
        {
            MySqlConnection conn = KetNoi.GetDBConnection();
            conn.Open();
            MySqlCommand newCmd = conn.CreateCommand();
            string queryString = "SELECT a.id_sanpham,a.ten_sp,a.ma_loaisp,a.ma_mau,a.ma_size,a.gia,a.loai,a.id_km,a.thuonghieu,a.soluongton,a.mota,a.active from sanpham a, loaisanpham b where a.ma_loaisp = b.ma_loaisp and a.ma_loaisp =@maloaisp";
            MySqlParameter maloaisp = new MySqlParameter("@maloaisp", MySqlDbType.String);
            maloaisp.Value = cat;
            newCmd.CommandText = queryString;
            newCmd.Parameters.Add(maloaisp);
            using (MySqlDataReader reader = newCmd.ExecuteReader())
            {
                // Kiểm tra có kết quả trả về
                if (reader.HasRows)
                { // Đọc từng dòng kết quả cho đến hết
                    while (reader.Read())
                    {
                        _productList.Add(new Product(reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString(), reader.GetDouble(5), reader[6].ToString(), reader[7].ToString(), reader[8].ToString(), reader.GetInt32(9), reader[10].ToString(), reader[11].ToString()));
                    }
                }
                else Console.WriteLine("No rows found.");
            }
            conn.Close();

            return _productList;
        }
        public IEnumerable<Product> GetProductsByCategoryAndLoai(string cat, string loai)
        {
            MySqlConnection conn = KetNoi.GetDBConnection();
            conn.Open();
            MySqlCommand newCmd = conn.CreateCommand();
            string queryString = "SELECT a.id_sanpham,a.ten_sp,a.ma_loaisp,a.ma_mau,a.ma_size,a.gia,a.loai,a.id_km,a.thuonghieu,a.soluongton,a.mota,a.active from sanpham a, loaisanpham b where a.ma_loaisp = b.ma_loaisp and a.ma_loaisp =@maloaisp and a.loai=@loai";
            MySqlParameter maloaisp = new MySqlParameter("@maloaisp", MySqlDbType.String);
            maloaisp.Value = cat;
            MySqlParameter loaisp = new MySqlParameter("@loai", MySqlDbType.String);
            loaisp.Value = loai;
            newCmd.CommandText = queryString;
            newCmd.Parameters.Add(maloaisp);
            newCmd.Parameters.Add(loaisp);
            using (MySqlDataReader reader = newCmd.ExecuteReader())
            {
                // Kiểm tra có kết quả trả về
                if (reader.HasRows)
                { // Đọc từng dòng kết quả cho đến hết
                    while (reader.Read())
                    {

                        _productList.Add(new Product(reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString(), reader.GetDouble(5), reader[6].ToString(), reader[7].ToString(), reader[8].ToString(), reader.GetInt32(9), reader[10].ToString(), reader[11].ToString()));
                    }
                }
                else Console.WriteLine("No rows found.");
            }
            conn.Close();

            return _productList;
        }
        public IEnumerable<Product> GetProductsByCategoryAndLoaiAndFilter(string cat, string loai, string mau, string size, string gia)
        {
            string filterNew = "";
            string filterQuery = "";

            //if (filter != null && filter != ""&&filter.Length-1>=0)
            //{
            //    System.Diagnostics.Debug.WriteLine("asdsada");
            //    filterNew = filter.Substring(0, filter.Length - 1).Replace("+", "");
            //    filterQuery = "and (c.ma_mausp=\'" + filterNew + "\')";
            //    //filterQuery = " and (c.ma_mausp=\'" + filterNew.Replace(" ", "").Replace("-", "\' or c.ma_mausp=\'") + "\')";
            //}
            if (mau != null && mau != "")
            {
                filterQuery += ("and c.ma_mausp='" + mau + "' ");
            }
            if (size != null && size != "")
            {
                filterQuery += ("and d.ma_sizesp='" + size + "' ");

            }
            if (gia != null && gia != "")
            {
                string s = "";
                switch (gia)
                {
                    case "<200":
                        s = " a.gia<200000 ";
                        break;
                    case "200-800":
                        s = " (a.gia>=200000 and a.gia<800000) ";
                        break;
                    case "800-2000":
                        s = " (a.gia>=800000 and a.gia<2020000) ";
                        break;
                    case ">2000":
                        s = " a.gia>=2000000 ";
                        break;
                }
                filterQuery = "and" + s;
            }

            //if (filterQuery != null && filterQuery != "")
            //{
            //    filterQuery = " and"+filterQuery;
            //}


            System.Diagnostics.Debug.WriteLine(filterQuery);
            MySqlConnection conn = KetNoi.GetDBConnection();
            conn.Open();
            MySqlCommand newCmd = conn.CreateCommand();
            string queryString = "SELECT a.id_sanpham,a.ten_sp,a.ma_loaisp,a.ma_mau,a.ma_size,a.gia,a.loai,a.id_km,a.thuonghieu,a.soluongton,a.mota,a.active from sanpham a, loaisanpham b,mausanpham c,sizesanpham d where a.ma_loaisp = b.ma_loaisp and a.ma_mau=c.ma_mausp and a.ma_size=d.ma_sizesp and a.ma_loaisp =@maloaisp and a.loai=@loai " + filterQuery;
            MySqlParameter maloaisp = new MySqlParameter("@maloaisp", MySqlDbType.String);
            maloaisp.Value = cat;
            MySqlParameter loaisp = new MySqlParameter("@loai", MySqlDbType.String);
            loaisp.Value = loai;
            newCmd.CommandText = queryString;
            newCmd.Parameters.Add(maloaisp);
            newCmd.Parameters.Add(loaisp);
            using (MySqlDataReader reader = newCmd.ExecuteReader())
            {
                // Kiểm tra có kết quả trả về
                if (reader.HasRows)
                { // Đọc từng dòng kết quả cho đến hết
                    while (reader.Read())
                    {
                        Product p = new Product(reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString(), reader.GetDouble(5), reader[6].ToString(), reader[7].ToString(), reader[8].ToString(), reader.GetInt32(9), reader[10].ToString(), reader[11].ToString());
                        if (mau != null && mau != "")
                        {
                            p.ma_mau = mau;
                        }
                        if (size != null && size != "")
                        {
                            p.ma_size = size;
                        }

                        if (!isContain(reader[1].ToString()))
                            _productList.Add(p);
                    }
                }
                else Console.WriteLine("No rows found.");
            }
            conn.Close();

            return _productList;
        }
        public Product GetProductsByID(string id)
        {
            MySqlConnection conn = KetNoi.GetDBConnection();
            conn.Open();
            MySqlCommand newCmd = conn.CreateCommand();
            string queryString = "select * from sanpham  where id_sanpham=@idsp";
            MySqlParameter idsp = new MySqlParameter("@idsp", MySqlDbType.String);
            idsp.Value = id;
            newCmd.CommandText = queryString;
            newCmd.Parameters.Add(idsp);
            using (MySqlDataReader reader = newCmd.ExecuteReader())
            {
                // Kiểm tra có kết quả trả về
                if (reader.HasRows)
                { // Đọc từng dòng kết quả cho đến hết
                    while (reader.Read())
                    {
                        _productList.Add(new Product(reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString(), reader.GetDouble(5), reader[6].ToString(), reader[7].ToString(), reader[8].ToString(), reader.GetInt32(9), reader[10].ToString(), reader[11].ToString()));
                    }
                }
                else Console.WriteLine("No rows found.");
            }

            List<string> imgs = new List<string>();
            newCmd = conn.CreateCommand();
            queryString = "select link_anh from hinhanh  where id_sanpham=@idsp";
            newCmd.CommandText = queryString;
            newCmd.Parameters.Add(idsp);
            using (MySqlDataReader reader = newCmd.ExecuteReader())
            {
                // Kiểm tra có kết quả trả về
                if (reader.HasRows)
                { // Đọc từng dòng kết quả cho đến hết
                    while (reader.Read())
                    {
                        imgs.Add(reader[0].ToString());
                    }
                }
                else Console.WriteLine("No rows found.");
            }
            //Dòng code t fix là từ chỗ này(Hiệp>3)
            if (_productList.Count != 1) return null;
            //nó bị lỗi ArgumentOutOfRangeException
            //productlist[0] đc nó xác định là empty nên ko thể thay thế imgs trong đó đc
            //nên t đã thêm 1 dòng dùng để clone nó ra
            Product pro = _productList[0];
            pro.imgs = imgs;
            conn.Close();
            
            return pro;
        }

        public Product GetProductsByName(string name)
        {
            MySqlConnection conn = KetNoi.GetDBConnection();
            conn.Open();
            MySqlCommand newCmd = conn.CreateCommand();
            string queryString = "select * from sanpham  where ten_sp=@namesp";
            MySqlParameter namesp = new MySqlParameter("@namesp", MySqlDbType.String);
            namesp.Value = name;
            newCmd.CommandText = queryString;
            newCmd.Parameters.Add(namesp);
            using (MySqlDataReader reader = newCmd.ExecuteReader())
            {
                // Kiểm tra có kết quả trả về
                if (reader.HasRows)
                { // Đọc từng dòng kết quả cho đến hết
                    while (reader.Read())
                    {
                        _productList.Add(new Product(reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString(), reader.GetDouble(5), reader[6].ToString(), reader[7].ToString(), reader[8].ToString(), reader.GetInt32(9), reader[10].ToString(), reader[11].ToString()));
                    }
                }
                else Console.WriteLine("No rows found.");
            }

            List<string> imgs = new List<string>();
            newCmd = conn.CreateCommand();
            queryString = "select link_anh from hinhanh a,sanpham b  where a.id_sanpham=b.id_sanpham and b.ten_sp=@namesp";
            newCmd.CommandText = queryString;
            newCmd.Parameters.Add(namesp);
            using (MySqlDataReader reader = newCmd.ExecuteReader())
            {
                // Kiểm tra có kết quả trả về
                if (reader.HasRows)
                { // Đọc từng dòng kết quả cho đến hết
                    while (reader.Read())
                    {
                        imgs.Add(reader[0].ToString());
                    }
                }
                else Console.WriteLine("No rows found.");
            }
            _productList[0].imgs = imgs;
            conn.Close();

            return _productList[0];
        }

        public Product GetProduct(string id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Product> GetAllProducts()
        {
            MySqlConnection conn = KetNoi.GetDBConnection();
            conn.Open();
            string queryString = "select a.id_sanpham,a.ten_sp,a.ma_loaisp,a.ma_mau,a.ma_size,a.gia,a.loai,a.id_km,a.thuonghieu,a.soluongton,a.mota,a.active,b.rate FROM sanpham a LEFT JOIN khuyenmai b on a.id_km=b.id_km";
            MySqlCommand command = new MySqlCommand(queryString, conn);

            using (MySqlDataReader reader = command.ExecuteReader())
            {
                // Kiểm tra có kết quả trả về
                if (reader.HasRows)
                { // Đọc từng dòng kết quả cho đến hết
                    while (reader.Read())
                    {
                        Product p = new Product(reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString(), reader.GetDouble(5), reader[6].ToString(), reader[7].ToString(), reader[8].ToString(), reader.GetInt32(9), reader[10].ToString(), reader[11].ToString());
                        if (!reader.IsDBNull(12))
                        {
                            p.rateDiscount = reader.GetDouble(12);
                        }
                        _productList.Add(p);
                    }
                }
                else Console.WriteLine("No rows found.");
            }
            conn.Close();

            return _productList;
        }

        public IEnumerable<Product> GetLatestProducts()
        {
            MySqlConnection conn = KetNoi.GetDBConnection();
            conn.Open();
            string queryString = "select a.id_sanpham,a.ten_sp,a.ma_loaisp,a.ma_mau,a.ma_size,a.gia,a.loai,a.id_km,a.thuonghieu,a.soluongton,a.mota,a.active FROM sanpham a LEFT JOIN khuyenmai b on a.id_km=b.id_km ORDER BY a.createdat desc limit 6";
            MySqlCommand command = new MySqlCommand(queryString, conn);

            using (MySqlDataReader reader = command.ExecuteReader())
            {
                // Kiểm tra có kết quả trả về
                if (reader.HasRows)
                { // Đọc từng dòng kết quả cho đến hết
                    while (reader.Read())
                    {
                        _productList.Add(new Product(reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString(), reader.GetDouble(5), reader[6].ToString(), reader[7].ToString(), reader[8].ToString(), reader.GetInt32(9), reader[10].ToString(), reader[11].ToString()));
                    }
                }
                else Console.WriteLine("No rows found.");
            }
            conn.Close();

            return _productList;
        }
        public IEnumerable<Product> GetRandomProducts()
        {
            MySqlConnection conn = KetNoi.GetDBConnection();
            conn.Open();
            string queryString = "select a.id_sanpham,a.ten_sp,a.ma_loaisp,a.ma_mau,a.ma_size,a.gia,a.loai,a.id_km,a.thuonghieu,a.soluongton,a.mota,a.active FROM sanpham a LEFT JOIN khuyenmai b on a.id_km=b.id_km ORDER BY Rand() limit 6";
            MySqlCommand command = new MySqlCommand(queryString, conn);

            using (MySqlDataReader reader = command.ExecuteReader())
            {
                // Kiểm tra có kết quả trả về
                if (reader.HasRows)
                { // Đọc từng dòng kết quả cho đến hết
                    while (reader.Read())
                    {
                        _productList.Add(new Product(reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString(), reader.GetDouble(5), reader[6].ToString(), reader[7].ToString(), reader[8].ToString(), reader.GetInt32(9), reader[10].ToString(), reader[11].ToString()));
                    }
                }
                else Console.WriteLine("No rows found.");
            }
            conn.Close();

            return _productList;
        }
        public IEnumerable<Product> GetMostSoldProducts()
        {
            MySqlConnection conn = KetNoi.GetDBConnection();
            conn.Open();
            string queryString = "select a.id_sanpham,a.ten_sp,a.ma_loaisp,a.ma_mau,a.ma_size,a.gia,a.loai,a.id_km,a.thuonghieu,a.soluongton,a.mota,a.active from sanpham a LEFT JOIN khuyenmai b on a.id_km=b.id_km INNER JOIN (SELECT id_sanpham from chitiethoadon GROUP BY id_sanpham ORDER BY sum(soluong) DESC limit 6) as c on a.id_sanpham=c.id_sanpham limit 6";
            MySqlCommand command = new MySqlCommand(queryString, conn);

            using (MySqlDataReader reader = command.ExecuteReader())
            {
                // Kiểm tra có kết quả trả về
                if (reader.HasRows)
                { // Đọc từng dòng kết quả cho đến hết
                    while (reader.Read())
                    {
                        _productList.Add(new Product(reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString(), reader.GetDouble(5), reader[6].ToString(), reader[7].ToString(), reader[8].ToString(), reader.GetInt32(9), reader[10].ToString(), reader[11].ToString()));
                    }
                }
                else Console.WriteLine("No rows found.");
            }
            conn.Close();

            return _productList;
        }
        public IEnumerable<string> GetColorsByNameProduct(string name)
        {
            List<string> list = new List<string>();
            MySqlConnection conn = KetNoi.GetDBConnection();
            conn.Open();
            MySqlCommand newCmd = conn.CreateCommand();
            string queryString = "select ma_mau from sanpham  where ten_sp=@namesp";
            MySqlParameter namesp = new MySqlParameter("@namesp", MySqlDbType.String);
            namesp.Value = name;
            newCmd.CommandText = queryString;
            newCmd.Parameters.Add(namesp);
            using (MySqlDataReader reader = newCmd.ExecuteReader())
            {
                // Kiểm tra có kết quả trả về
                if (reader.HasRows)
                { // Đọc từng dòng kết quả cho đến hết
                    while (reader.Read())
                    {
                        if (!isContainString(list, reader[0].ToString()))
                            list.Add(reader[0].ToString());
                    }
                }
                else Console.WriteLine("No rows found.");
            }


            conn.Close();

            return list;
        }

        public IEnumerable<string> GetSizesByNameProduct(string name)
        {
            List<string> list = new List<string>();
            MySqlConnection conn = KetNoi.GetDBConnection();
            conn.Open();
            MySqlCommand newCmd = conn.CreateCommand();
            string queryString = "select ma_size from sanpham  where ten_sp=@namesp";
            MySqlParameter namesp = new MySqlParameter("@namesp", MySqlDbType.String);
            namesp.Value = name;
            newCmd.CommandText = queryString;
            newCmd.Parameters.Add(namesp);
            using (MySqlDataReader reader = newCmd.ExecuteReader())
            {
                // Kiểm tra có kết quả trả về
                if (reader.HasRows)
                { // Đọc từng dòng kết quả cho đến hết
                    while (reader.Read())
                    {
                        if (!isContainString(list, reader[0].ToString()))
                            list.Add(reader[0].ToString());
                    }
                }
                else Console.WriteLine("No rows found.");
            }


            conn.Close();

            return list;
        }
    }
}