using System;
using System.Data.SqlClient;

namespace _02.VillainNames
{
    public class StartUp
    {
        //Write a program that prints on the console all villains’ names and their number of minions of those who have more
        //than 3 minions ordered descending by number of minions.

        private static SqlConnection connection = new SqlConnection(@"Server=LENOVO\SQLEXPRESS;Database=MinionsDB;Integrated Security=true");

        public static void Main()
        {
            try
            {
                connection.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine("Connecting to database failed.");
                Console.WriteLine(e.Message);
                return;
            }

            using (connection)
            {
                try
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
                catch (Exception e)
                {
                    Console.WriteLine("SqlCommands was incorrect.");
                    Console.WriteLine(e.Message);
                }               
                
            }
        }
    }
}
