using System;
using System.Data.SqlClient;

namespace _06.RemoveVillain
{
    public class StartUp
    {
        //Write a program that receives the ID of a villain, deletes him from the database and releases his minions from
        //serving to him.
        //Make sure all operations go as planned,
        //otherwise do not make any changes in the database.

        private static SqlConnection minionsDbConnection = new SqlConnection("Server = LENOVO\\SQLEXPRESS; Database = MinionsDB; Integrated Security = true");
        private static SqlTransaction sqlTransaction;

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

            int villainId = int.Parse(Console.ReadLine());

            using (minionsDbConnection)
            {
                sqlTransaction = minionsDbConnection.BeginTransaction();

                SqlCommand command = minionsDbConnection.CreateCommand();
                command.Transaction = sqlTransaction;

                try
                {
                    command.CommandText = $"SELECT Name FROM Villains WHERE Id = {villainId}";

                    if (command.ExecuteScalar() == null)
                    {
                        Console.WriteLine("No such villain was found.");
                        return;
                    }

                    string villainName = (string)command.ExecuteScalar();

                    command.CommandText = $"DELETE FROM MinionsVillains WHERE VillainId = {villainId}";
                    int countOfMinnionsAffected = command.ExecuteNonQuery();

                    command.CommandText = $"DELETE FROM Villains WHERE Id = {villainId}";
                    command.ExecuteNonQuery();

                    sqlTransaction.Commit();

                    Console.WriteLine($"{villainName} was deleted.");
                    Console.WriteLine($"{countOfMinnionsAffected} minions were released.");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);

                    try
                    {
                        sqlTransaction.Rollback();
                    }
                    catch (Exception exRollback)
                    {
                        Console.WriteLine(exRollback.Message);
                    }
                }
            }           
        }
    }
}
