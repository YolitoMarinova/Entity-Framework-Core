using System;
using System.Data.SqlClient;

namespace _09.IncreaseAgeStoredProcedure
{
    public class StartUp
    {
        //Create stored procedure usp_GetOlder(directly in the database using Management Studio or any other similar
        //tool) that receives MinionId and increases that minion’s age by 1. Write a program that uses that stored procedure
        //to increase the age of a minion whose id will be given as input from the console.After that print the name and the
        //age of that minion.

        private static SqlConnection minionsDbConnection = new SqlConnection("Server = LENOVO\\SQLEXPRESS; Database = MinionsDB; Integrated Security = true");

        public static void Main()
        {
            try
            {
                minionsDbConnection.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine("Cannot connect to current database.");
                Console.WriteLine(e.Message);

                return;
            }

            Console.Write("Minion Id: ");
            int minionId = int.Parse(Console.ReadLine());

            using (minionsDbConnection)
            {
                SqlCommand useProcCmd = new SqlCommand($"EXEC dbo.usp_GetOlder {minionId}", minionsDbConnection);
                useProcCmd.ExecuteNonQuery();

                SqlCommand minionNameAndAgeCmd = new SqlCommand($"SELECT [Name], Age FROM Minions WHERE Id = {minionId}", minionsDbConnection);
                SqlDataReader reader = minionNameAndAgeCmd.ExecuteReader();

                using (reader)
                {
                    while (reader.Read())
                    {
                        string minionName = (string)reader["Name"];
                        int minionAge = (int)reader["Age"];

                        Console.WriteLine($"{minionName} - {minionAge} years old");
                    }
                }
            }
        }
    }
}
