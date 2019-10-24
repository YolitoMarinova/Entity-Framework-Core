using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace _05.ChangeTownNamesCasing
{
    public class StartUp
    {
        //Write a program that changes all town names to uppercase for a given country.

        private static SqlConnection minionsDbConnection = new SqlConnection("Server = LENOVO\\SQLEXPRESS; Database = MinionsDb; Integrated Security = true");

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

            Console.Write("Country name: ");
            string countryName = Console.ReadLine();

            using (minionsDbConnection)
            {
                SqlCommand changeTownNamesCmd = new SqlCommand(@$"UPDATE Towns
                                                                    SET Name = UPPER(Name)
                                                                  WHERE CountryCode = (SELECT c.Id FROM Countries AS c WHERE c.Name = '{countryName}')",
                                                                  minionsDbConnection);

                using (changeTownNamesCmd)
                {
                    int rowsAffected = changeTownNamesCmd.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        Console.WriteLine("No town names were affected.");
                        return;
                    }

                    Console.WriteLine($"{rowsAffected} town names were affected.");
                }

                SqlCommand townNamesAffectedCmd = new SqlCommand(@$"SELECT t.Name 
                                                                     FROM Towns as t
                                                                     JOIN Countries AS c ON c.Id = t.CountryCode
                                                                    WHERE c.Name = '{countryName}'",
                                                                    minionsDbConnection);

                var reader = townNamesAffectedCmd.ExecuteReader();
                List<string> townNames = new List<string>();

                using (reader)
                {
                    while (reader.Read())
                    {
                        string currentTownName = (string)reader["Name"];

                        townNames.Add(currentTownName);
                    }
                }

                Console.WriteLine("[" + String.Join(", ", townNames) + "]");
            }
        }
    }
}
