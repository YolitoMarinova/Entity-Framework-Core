using System;
using System.Data.SqlClient;

namespace _03.MinionNames
{
    public class StartUp
    {
        public static void Main()
        {
            var minionsDB = new SqlConnection(@"Server=LENOVO\SQLEXPRESS;Database=MinionsDB;Integrated Security=true");

            minionsDB.Open();

            using (minionsDB)
            {
                var command = new SqlCommand("SELECT *" +
                    "FROM MinionsVillains AS mv" +
                    "JOIN Villains AS v ON v.Id = mv.VillianId" +
                    "JOIN Minions AS m ON m.Id = mv.MinionId", minionsDB);
            }
        }
    }
}
