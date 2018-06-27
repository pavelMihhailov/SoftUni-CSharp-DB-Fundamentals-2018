﻿using System;
using System.IO;

namespace _02_Villain_Names
{
    public class StartUp
    {
        public static void Main()
        {
            SqlConnection connection = new SqlConnection("Server =.;Database=MinionsDB;Integrated Security = true");
            connection.Open();

            using (connection)
            {
                string query = File.ReadAllText(@"../../FindVillainsCounts.sql");
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                using (reader)
                {
                    while (reader.Read())
                    {
                        string villainName = (string)reader["Name"];
                        int numberOfMinions = (int)reader["NumberOfMinions"];
                        Console.WriteLine($"{villainName} {numberOfMinions}");
                    }
                }
            }
        }
    }
}
