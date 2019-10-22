using System;
using System.Data.SqlClient;

namespace _01.InitialSetup
{
    public class StartUp
    {
        public static void Main()
        {
            var connection = new SqlConnection(@"Server=LENOVO\SQLEXPRESS;Database=MinionsDB;Integrated Security=true");

            connection.Open();

            using (connection)
            {

            }
        }
    }
}
