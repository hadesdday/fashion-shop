using Databases;
using fashion_shop_group32.Models;
using MySqlConnector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Security.Cryptography;
using System.Text;
using System.Web.Security;

namespace fashion_shop_group32.Models.Dao
{
    public class UserDao
    {
        private static UserDao instance = null;
        public UserDao()
        {
            //do nothing
        }
        public static UserDao getInstance()
        {
            if (instance == null)
            {
                instance = new UserDao();
            }
            return instance;
        }


        public static Users checkLogin(String username, String password)
        {
            List<Users> usersList = new List<Users>();
            /*try
            {
                Connection conn = DBConnect.getInstance().getConn();
                PreparedStatement statement = conn.prepareStatement("select * from user where name = ?");
                statement.setString(1, username);
                rs = statement.executeQuery();
                while (rs.next())
                {
                    usersList.add(new Users(rs.getString("id"), rs.getString("name"), rs.getString("password"), rs.getString("email"), rs.getInt("phone"), rs.getString("address"), rs.getInt("role")));
                }
                rs.close();
                System.out.println(usersList.get(0));
                if (usersList.size() != 1) return null;
                Users user = usersList.get(0);
                System.out.println("success prepare");
                if (!user.getUserName().equals(username) || !user.getPassword().equals(hashPassword(password))) return null;
                System.out.println("success");
                return user;
            }
            catch (SQLException e)
            {
                e.printStackTrace();
                return null;
            }*/
            string sql = "select * from user where username = @username";
            MySqlCommand command = new MySqlCommand();
            MySqlConnection connect = KetNoi.GetDBConnection();
            connect.Open();
            command.Connection = connect;
            command.CommandText = sql;
            command.Prepare();
            command.Parameters.AddWithValue("@username", username);
            using (DbDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    usersList.Add(new Users(reader.GetString(0), reader.GetString(1), reader.GetString(2),
                        reader.GetString(3), reader.GetString(4), reader.GetString(5),
                        reader.GetInt32(6), reader.GetInt32(7), reader.GetDateTime(8), reader.GetDateTime(9)));
                }
                reader.Close();
                //Console.WriteLine(usersList[0]);
                if (usersList.Count != 1) return null;
                Users user = usersList[0];
                Console.WriteLine("success login");
                if (!user.userName.Equals(username) || verify(user.password, password) == false) return null;
                return user;
            }
        }
        public static string generateToken()
        {
            string chars = "abcdefghijklmnopqrstuvwxyz";
            int size = 10;
            StringBuilder result = new StringBuilder();
            Random random = new Random();
            for (int i = 0; i < size; i++)
            {
                result.Append(chars[random.Next(chars.Length)]);
            }
            return result.ToString();
        }
        public static String generateId()
        {
            /*
             StringBuilder builder = new StringBuilder();
Enumerable
   .Range(65, 26)
    .Select(e => ((char)e).ToString())
    .Concat(Enumerable.Range(97, 26).Select(e => ((char)e).ToString()))
    .Concat(Enumerable.Range(0, 10).Select(e => e.ToString()))
    .OrderBy(e => Guid.NewGuid())
    .Take(11)
    .ToList().ForEach(e => builder.Append(e));
string id = builder.ToString();
            */
            return Guid.NewGuid().ToString();
        }
        public static Boolean register(String username, String password, String email, String ten_kh, String diachi, String sodt)
        {

            string id = generateId();
            string token = generateToken();
            string role = "0";
            //Random rd = new Random();
            //int id_kh = rd.Next(10000000, 100000000);
            int active = 1;
            DateTime createDate = DateTime.Now;
            DateTime updateDate = DateTime.Now;
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
            //int newIdKh = (int)cmd.Parameters["@id_khachhang"].Value;
            int newIdKh = (int)cmd.LastInsertedId;
            //connectCus.Close();
            //ket noi toi database table
            string sql = "insert into user(id_user,username,token,password,role,email,id_khachhang,active,createdat,updatedat) " +
                "values(@id_user,@username,@token,@password,@role,@email,@id_khachhang,@active,@createdat,@updatedat)";
            MySqlCommand command = new MySqlCommand();
            MySqlConnection connect = KetNoi.GetDBConnection();
            connect.Open();
            command.Connection = connect;
            command.CommandText = sql;
            //int newIdKh = (int)cmd.Parameters["@id_khachhang"].Value+1;
            command.Parameters.AddWithValue("@id_user", id);
            command.Parameters.AddWithValue("@username", username);
            command.Parameters.AddWithValue("@token", token);
            command.Parameters.AddWithValue("@password", hashPassword(password));
            command.Parameters.AddWithValue("@role", role);
            command.Parameters.AddWithValue("@email", email);
            command.Parameters.AddWithValue("@id_khachhang", newIdKh);
            command.Parameters.AddWithValue("@active", active);
            command.Parameters.AddWithValue("@createdat", createDate);
            command.Parameters.AddWithValue("@updatedat", updateDate);

            int rows = command.ExecuteNonQuery();
            connectCus.Close();

            return rows == 1 && rowsCustomer == 1;
        }
        public static ArrayList getSellectUser()
        {
            ArrayList listUser = new ArrayList();
            string sql = "select * from User";
            MySqlConnection connect = KetNoi.GetDBConnection();
            connect.Open();
            MySqlCommand command = new MySqlCommand();
            command.Connection = connect;
            command.CommandText = sql;
            using (DbDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    listUser.Add(new Users(reader.GetString(0), reader.GetString(1), reader.GetString(2),
                        reader.GetString(3), reader.GetString(4), reader.GetString(5),
                        reader.GetInt32(6), reader.GetInt32(7), reader.GetDateTime(8), reader.GetDateTime(9)));
                }
                reader.Close();
            }
            return listUser;

        }
        public static String hashPassword(String password)
        {
            /*public static String hashPassword(String password){
        try {
            MessageDigest sha256 = MessageDigest.getInstance("SHA-256");
            byte[] hash = sha256.digest(password.getBytes(StandardCharsets.UTF_8));
            BigInteger number = new BigInteger(1, hash);
            return  number.toString(16);
        } catch (NoSuchAlgorithmException e) {
            return null;
        }
        }*/
            //create salt
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
            //create hash
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000);
            byte[] hash = pbkdf2.GetBytes(20);
            //combine hash and salt
            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);
            //convert to base 64
            string PasswordHash = Convert.ToBase64String(hashBytes);
            return PasswordHash;
        }
        public static Boolean verify(string passwordHash, string password)
        {
            /* Fetch the stored value */
            /* Extract the bytes */
            byte[] hashBytes = Convert.FromBase64String(passwordHash);
            /* Get the salt */
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);
            /* Compute the hash on the password the user entered */
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000);
            byte[] hash = pbkdf2.GetBytes(20);
            /* Compare the results */
            for (int i = 0; i < 20; i++)
            {
                if (hashBytes[i + 16] != hash[i])
                {
                    return false;
                }
            }
            return true;

        }
        public static Customer getSelectedCus(int idCus)
        {
            List<Customer> listCus = new List<Customer>();
            string sql = "select * from khachhang where id_khachhang=@id_khachhang";
            MySqlConnection connect = KetNoi.GetDBConnection();
            connect.Open();
            MySqlCommand command = new MySqlCommand();
            command.Connection = connect;
            command.CommandText = sql;
            command.Parameters.AddWithValue("@id_khachhang", idCus);
            using (DbDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    listCus.Add(new Customer(reader.GetInt32(0), reader.GetString(1), reader.GetString(2),
                        reader.GetString(3), reader.GetString(4),
                        reader.GetDateTime(5), reader.GetDateTime(6)));
                }
                reader.Close();
            }
            if (listCus.Count != 1) return null;
            Customer cus = listCus[0];
            //if (!cus.userName.Equals(username) || verify(user.password, password) == false) return null;
            return cus;
        }
        public static Boolean updateCustomer(int id_kh, string namecus, string address, string phone, string email)
        {
            DateTime updateDate = DateTime.Now;
            MySqlConnection connectCus = KetNoi.GetDBConnection();
            connectCus.Open();
            string sqlCus = "update khachhang set ten_kh=@ten_kh,diachi=@diachi,sodt=@sodt,email=@email,updatedat=@updatedat where id_khachhang = @id_khachhang";
            MySqlCommand cmd = new MySqlCommand(sqlCus, connectCus);
            cmd.Parameters.AddWithValue("@id_khachhang", id_kh);
            cmd.Parameters.AddWithValue("@ten_kh", namecus);
            cmd.Parameters.AddWithValue("@diachi", address);
            cmd.Parameters.AddWithValue("@sodt", phone);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@updatedat", updateDate);
            int rows = cmd.ExecuteNonQuery();
            return rows == 1;
        }
        static void Main(string[] args)
        {
            //register("chanhhiep", "123456", "chanhhuep2907@gmail.com");
        }
    }
}