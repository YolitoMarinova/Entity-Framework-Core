using System;
using System.Data.SqlClient;

namespace _01.InitialSetup
{
    public class StartUp
    {

        //Write a program that connects to your localhost server.

        public static void Main()
        {
            var connection = new SqlConnection(@"Server=LENOVO\SQLEXPRESS;Database=MinionssDB;Integrated Security=true");

            try
            {
                connection.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine("Connecting to database failed.");
                Console.WriteLine(e.Message);                
            }

            using (connection)
            {

            }
        }
    }
}
