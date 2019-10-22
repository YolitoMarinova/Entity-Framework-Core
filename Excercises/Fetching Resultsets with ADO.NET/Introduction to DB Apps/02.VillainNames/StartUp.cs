using System;
using System.Data.SqlClient;

namespace _02.VillainNames
{
    public class StartUp
    {
        public static void Main()
        {
            var connection = new SqlConnection(@"Server=LENOVO\SQLEXPRESS;Database=MinionsDB;Integrated Security=true");

            connection.Open();

            using (connection)
            {
                var command = new SqlCommand("SELECT v.[Name], COUNT(mv.MinionId) AS Count " +
                    "FROM MinionsVillains AS mv " +
                    "JOIN Villains AS v ON v.Id = mv.VillainId " +
                    "GROUP BY v.[Name] HAVING COUNT(mv.MinionId) > 3 " +
                    "ORDER BY Count DESC", connection);
                
                var reader = command.ExecuteReader();

                using (reader)
                {
                    while (reader.Read())
                    {
                        string name = (string)reader["Name"];
                        int countOfMinions = (int)reader["Count"];

                        Console.WriteLine(name + " - " + countOfMinions);
                    }
                }
            }
        }
    }
}
