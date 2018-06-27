using System;
using System.IO;

namespace _03_Minion_Names
{
    public class StartUp
    {
        public static void Main()
        {
            SqlConnection connection = new SqlConnection("Server =.;Database=MinionsDB;Integrated Security = true");
            connection.Open();

            using (connection)
            {
                Console.Write("Enter villain ID: ");
                int villainId = int.Parse(Console.ReadLine());

                GetVillainName(connection, villainId);
                GetMinionsNames(connection, villainId);
            }
        }

        private static void GetMinionsNames(SqlConnection connection, int villainId)
        {
            SqlCommand command = GetSqlCommand(connection, "FindMinionsNames", villainId);
            SqlDataReader reader = command.ExecuteReader();

            using (reader)
            {
                int minionsCount = 0;
                while (reader.Read())
                {
                    string minionName = (string)reader["Name"];
                    int minionAge = (int)reader["Age"];
                    Console.WriteLine($"{++minionsCount}. {minionName} {minionAge}");
                }
            }
        }

        private static void GetVillainName(SqlConnection connection, int villainId)
        {
            SqlCommand command = GetSqlCommand(connection, "FindVillainName", villainId);
            string villainName = (string)command.ExecuteScalar();
            Console.WriteLine(villainName == null
                ? $"No villain with ID {villainId} exists in the database."
                : $"Villain: {villainName}");
        }

        private static SqlCommand GetSqlCommand(SqlConnection connection, string fileName, int villainId)
        {
            string query = File.ReadAllText($@"../../{fileName}.sql");
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@villainId", villainId);

            return command;
        }
    }
}
