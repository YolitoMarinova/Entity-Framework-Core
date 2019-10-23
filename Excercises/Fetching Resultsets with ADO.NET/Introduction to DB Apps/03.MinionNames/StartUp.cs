using System;
using System.Data.SqlClient;

namespace _03.MinionNames
{
    public class StartUp
    {
        //Write a program that prints on the console all minion names and age for a given villain id, ordered by name
        //alphabetically.
        //If there is no villain with the given ID, print &quot;No villain with ID &lt;VillainId&gt; exists in the database.&quot;.
        //If the selected villain has no minions, print &quot;(no minions)&quot; on the second row.

        private static SqlConnection minionsDB = new SqlConnection(@"Server=LENOVO\SQLEXPRESS;Database=MinionsDB;Integrated Security=true");

        public static void Main()
        {
            try
            {
                minionsDB.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine("Cannot connect to current database.");
                Console.WriteLine(e.Message);
                return;
            }

            int villainId = 0;

            try
            {
                Console.WriteLine("Input Villain Id:");
                villainId = int.Parse(Console.ReadLine());
            }
            catch (Exception e)
            {
                Console.WriteLine("Id should be a valid number!");
                Console.WriteLine(e.Message);
            }

            using (minionsDB)
            {
                SqlCommand villainNameCmd = null;

                SqlCommand minionsForVillainCmd = null;

                try
                {
                    villainNameCmd = new SqlCommand($"SELECT v.Name AS [VillainName] FROM Villains as v WHERE v.Id = {villainId}", minionsDB);

                    minionsForVillainCmd = new SqlCommand(@$"SELECT ROW_NUMBER() OVER (ORDER BY m.Name) as RowNum," +
                                                                        "m.Name, " +
                                                                        "m.Age" +
                                                               @$" FROM MinionsVillains AS mv
                                                               JOIN Minions As m ON mv.MinionId = m.Id
                                                              WHERE mv.VillainId = {villainId}
                                                           ORDER BY m.Name", minionsDB);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Incorrect query!");
                    Console.WriteLine(e.Message);
                }

                var reader = villainNameCmd.ExecuteReader();

                using (reader)
                {
                    if (reader.Read())
                    {
                        string villainName = (string)reader["VillainName"];

                        Console.WriteLine($"Villain: {villainName}");
                    }
                    else
                    {
                        Console.WriteLine($"No villain with ID {villainId} exists in the database.");
                        return;
                    }
                }


                if (minionsForVillainCmd.ExecuteScalar() == null)
                {
                    Console.WriteLine("(no minions)");
                }

                reader = minionsForVillainCmd.ExecuteReader();

                using (reader)
                {
                    while (reader.Read())
                    {
                        long minionNumber = (long)reader["RowNum"];
                        string minionName = (string)reader["Name"];
                        int minionAge = (int)reader["Age"];

                        Console.WriteLine($"{minionNumber}. {minionName} {minionAge}");
                    }
                }
            }
        }
    }
}
