using MySqlConnector;
using System;

namespace Databases
{
    class Program
    {
        static void Main(string[] args)
        {
            string strConnection = "server=localhost;uid=root;pwd=;database=test;";

            MySqlConnection con = new MySqlConnection(strConnection);

            string queryString = "SELECT * FROM student where score > @score1";

            MySqlCommand newCmd = con.CreateCommand();

            //MySqlParameter score1 = new MySqlParameter("@score1", 9);

            MySqlParameter score1 = new MySqlParameter("@score1", MySqlDbType.Int32);
            score1.Value = 4;
            newCmd.CommandText = queryString;
            newCmd.Parameters.Add(score1);
            //newCmd.Parameters.AddWithValue("@score1", 9);

            string insertQuery = @"INSERT INTO student (id,name,age,score) VALUES (@id,@name,@age,@score)";
            MySqlCommand insertCmd = con.CreateCommand();
            MySqlParameter idIns = new MySqlParameter("@id", MySqlDbType.Int32);
            MySqlParameter nameIns = new MySqlParameter("@name", MySqlDbType.VarChar);
            MySqlParameter ageIns = new MySqlParameter("@age", MySqlDbType.Int32);
            MySqlParameter scoreIns = new MySqlParameter("@score", MySqlDbType.Int32);
            idIns.Value = 7;
            nameIns.Value = "hieu nguyen";
            ageIns.Value = 29;
            scoreIns.Value = 8;

            insertCmd.CommandText = insertQuery;
            insertCmd.Parameters.AddWithValue("@id", 7);
            insertCmd.Parameters.AddWithValue("@name", "hieu nguyen");
            insertCmd.Parameters.AddWithValue("@age", 29);
            insertCmd.Parameters.AddWithValue("@score", 8);


            //insertCmd.Parameters.AddWithValue("@id", idIns);
            //insertCmd.Parameters.AddWithValue("@name", nameIns);
            //insertCmd.Parameters.AddWithValue("@age", ageIns);
            //insertCmd.Parameters.AddWithValue("@score", scoreIns);

            string deleteQuery = @"DELETE FROM student where id = @id";
            MySqlCommand delCmd = con.CreateCommand();
            delCmd.CommandText = deleteQuery;
            delCmd.Parameters.AddWithValue("@id", 7);

            string updateQuery = @"UPDATE student SET name = @name where id = @id";
            MySqlCommand updateCmd = con.CreateCommand();
            updateCmd.CommandText = updateQuery;
            updateCmd.Parameters.AddWithValue("@id", "6");
            updateCmd.Parameters.AddWithValue("@name", "tran hieu ne");


            //MySqlCommand cmd = con.CreateCommand();
            //cmd.CommandText = "select * from student";

            try
            {
                con.Open();
                //MySqlDataReader rd = cmd.ExecuteReader();
                //Console.WriteLine("original table");
                //while (rd.Read())
                //{
                //    Console.Write(rd["id"] + "\t");
                //    Console.Write(rd["name"] + "\t");
                //    Console.Write(rd["age"] + "\t");
                //    Console.Write(rd["score"] + "\n");
                //}

                //Console.WriteLine("table with condition");
                //MySqlDataReader newRd = newCmd.ExecuteReader();
                //while (newRd.Read())
                //{
                //    Console.Write(newRd["id"] + "\t");
                //    Console.Write(newRd["name"] + "\t");
                //    Console.Write(newRd["age"] + "\t");
                //    Console.Write(newRd["score"] + "\n");
                //}

                //var rows = insertCmd.ExecuteNonQuery();
                //Console.WriteLine("rows affected : {rows}",rows);

                //var rowsDel = delCmd.ExecuteNonQuery();
                //Console.WriteLine("rows affected : {0}", rowsDel);

                var rowsUpdated = updateCmd.ExecuteNonQuery();
                Console.WriteLine("rows affected : {0}", rowsUpdated);

                con.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("loi " + ex.Message);
            }

            Console.ReadLine();
        }
    }
}
