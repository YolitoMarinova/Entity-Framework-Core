using System;
using System.Data.SqlClient;
using System.Linq;

namespace _08.IncreaseMinionAge
{
    public class StartUp
    {
        //Read from the console minion IDs separated by space.Increment the age of those minions by 1 and make their
        //names title case. Finally, print the name and the age of all minions in the database.

        private static SqlConnection minionsDbConnection = new SqlConnection("Server = LENOVO\\SQLEXPRESS; Database = MinionsDB; Integrated Security = true");

        public static void Main()
        {
            try
            {
                minionsDbConnection.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine("Cannot connect to database.");
                Console.WriteLine(e.Message);

                return;
            }

            Console.Write("Minion Ids: ");
            int[] minionIds = Console.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            using (minionsDbConnection)
            {
                for (int i = 0; i < minionIds.Length; i++)
                {
                    SqlCommand updateMinionsByiDCmd = new SqlCommand(@$" UPDATE Minions
                                                                       SET Name = UPPER(LEFT(Name, 1)) + SUBSTRING(Name, 2, LEN(Name)), Age += 1
                                                                     WHERE Id = {minionIds[i]}",
                                                                     minionsDbConnection);

                    updateMinionsByiDCmd.ExecuteNonQuery();
                }

                SqlCommand printMinionsCmd = new SqlCommand("SELECT [Name], Age FROM Minions", minionsDbConnection);
                SqlDataReader reader = printMinionsCmd.ExecuteReader();

                using (reader)
                {
                    while (reader.Read())
                    {
                        string minionName = (string)reader["Name"];
                        int minionAge = (int)reader["Age"];

                        Console.WriteLine(minionName + " " + minionAge);
                    }
                }
            }
        }
    }
}
