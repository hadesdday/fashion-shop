using Databases;
using fashion_shop_group32.Context;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace fashion_shop_group32.Controllers.admin
{
    public class GetStatisticController : Controller
    {
        public JsonResult GetStats()
        {
            double sumTurnover = 0;
            int sumCustomer = 0;
            int sumOrder = 0;

            IDictionary<string, double> turnoverByMonths = new Dictionary<string, double>();
            for (int i = 1; i < 13; i++)
            {
                turnoverByMonths.Add(i.ToString(), 0);
            }

            MySqlConnection conn = KetNoi.GetDBConnection();
            conn.Open();
            MySqlCommand newCmd = conn.CreateCommand();
            string querystring = "select month(createdat) thang ,sum(trigia) tong\n" +
                        " from hoadon\n" +
                        " where trangthai = 5 and year(createdat) = year(NOW())\n" +
                        "group by month(createdat)";
            newCmd.CommandText = querystring;
            using (MySqlDataReader reader = newCmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string key = reader[0].ToString();
                        double value = Double.Parse(reader[1].ToString());
                        turnoverByMonths[key] = value;
                    }
                }
                else Console.WriteLine("not enough data.");
            }
            conn.Close();

            using (var ctx = new AdminDbContext())
            {
                sumTurnover = ctx.hoadon.Sum(o => o.trigia);
                sumCustomer = ctx.khachhang.Count();
                sumOrder = ctx.hoadon.Count();
            }

            var jsonArray = Json(new { sumTurnover, sumCustomer, sumOrder, turnoverByMonths }, JsonRequestBehavior.AllowGet);
            return jsonArray;
        }
    }
}