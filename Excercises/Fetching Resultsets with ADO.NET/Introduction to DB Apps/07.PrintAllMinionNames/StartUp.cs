using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace _07.PrintAllMinionNames
{
    public class StartUp
    {
        //Write a program that prints all minion names from the minions table in the following order: first record, last
        //record, first + 1, last - 1, first + 2, last - 2 … first + n, last - n.

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

            using (minionsDbConnection)
            {
                SqlCommand minionNamesCmd = new SqlCommand("SELECT [Name] FROM Minions", minionsDbConnection);

                using (minionNamesCmd)
                {                    
                    SqlDataReader reader = minionNamesCmd.ExecuteReader();
                    List<string> minionNames = new List<string>();

                    using (reader)
                    {
                        while (reader.Read())
                        {
                            string currentName = (string)reader["Name"];

                            minionNames.Add(currentName);
                        }
                    }

                    int lenght = (int)Math.Ceiling(minionNames.Count / 2m);

                    for (int i = 0; i < lenght; i++)
                    {
                        Console.WriteLine(minionNames[0 + i]);

                        Console.WriteLine(minionNames[(minionNames.Count-1) - i]);
                    }
                }
            }
        }
    }
}
