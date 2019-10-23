using System;
using System.Data.SqlClient;

namespace _04.AddMinion
{
    public class StartUp
    {
        //Write a program that reads information about a minion and its villain and adds it to the database.In case the town
        //of the minion is not in the database, insert it as well.In case the villain is not present in the database, add him too
        //with a default evilness factor of "evil". Finally set the new minion to be a servant of the villain.Print appropriate
        // messages after each operation.

        private static SqlConnection minionDbConnection = new SqlConnection(@"Server = LENOVO\SQLEXPRESS; Database=MinionsDB;Integrated Security = true");

        public static void Main()
        {
            try
            {
                minionDbConnection.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine("Cannot connect to current database!");
                Console.WriteLine(e.Message);
                return;
            }

            Console.WriteLine("Input minion information in format (Name Age TownName)");
            Console.Write("Minion: ");
            string[] minionInput = Console.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries);

            int minionId;
            string minionName = null;
            int minionAge = -1;
            string minionTownName = null;
            int townId;

            try
            {
                minionName = minionInput[0];
                minionAge = int.Parse(minionInput[1]);
                minionTownName = minionInput[2];
            }
            catch (Exception e)
            {
                Console.WriteLine("Invalid minion information!");
                Console.WriteLine(e.Message);
                return;
            }

            Console.WriteLine("Input villain name");
            Console.Write("Villain: ");
            int villainId;
            string villainName = Console.ReadLine();

            using (minionDbConnection)
            {
                SqlCommand townCmd = new SqlCommand($"SELECT Id FROM Towns WHERE Name = '{minionTownName}'", minionDbConnection);

                using (townCmd)
                {
                    if (townCmd.ExecuteScalar() == null)
                    {
                        SqlCommand addTown = new SqlCommand($"INSERT INTO Towns (Name) VALUES ('{minionTownName}')", minionDbConnection);
                        addTown.ExecuteNonQuery();

                        Console.WriteLine($"Town {minionTownName} was added to the database.");
                    }

                    townId = (int)townCmd.ExecuteScalar();
                }

                SqlCommand villainCmd = new SqlCommand($"SELECT Id FROM Villains WHERE Name = '{villainName}'", minionDbConnection);

                using (villainCmd)
                {
                    if (villainCmd.ExecuteScalar() == null)
                    {
                        int constIdForEvil = 4;

                        SqlCommand addVillain = new SqlCommand($"INSERT INTO Villains (Name, EvilnessFactorId) VALUES ('{villainName}', {constIdForEvil})", minionDbConnection);
                        addVillain.ExecuteNonQuery();

                        Console.WriteLine($"Villain {villainName} was added to the database.");
                    }

                    villainId = (int)villainCmd.ExecuteScalar();
                }

                SqlCommand addMinnionCmd = new SqlCommand($"INSERT INTO Minions (Name, Age, TownId) VALUES ('{minionName}', {minionAge}, {townId})", minionDbConnection);
                addMinnionCmd.ExecuteNonQuery();

                SqlCommand getMinionIdCmd = new SqlCommand($"SELECT Id FROM Minions WHERE Name = '{minionName}' ORDER BY Id DESC", minionDbConnection);
                minionId = (int)getMinionIdCmd.ExecuteScalar();

                SqlCommand addMinionToVillainCmd = new SqlCommand($"INSERT INTO MinionsVillains (MinionId, VillainId) VALUES ({minionId}, {villainId})", minionDbConnection);
                addMinionToVillainCmd.ExecuteNonQuery();

                Console.WriteLine($"Successfully added {minionName} to be minion of {villainName}.");
            }
        }
    }
}
