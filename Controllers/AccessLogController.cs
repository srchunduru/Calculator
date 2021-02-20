using System;
using Calculator.HttpRest.Models;
using Microsoft.Data.Sqlite;

namespace Calculator.Controllers
{
    public class AccessLogController
    {
        const string connectionString = "Data Source=InMemorySample;Mode=Memory;Cache=Shared";
        string message = "Logging User Values";
        public string LogUser(AccessLog item)
        {
            try
            {
                var masterConnection = new SqliteConnection(connectionString);
                masterConnection.Open();
                var createCommand = masterConnection.CreateCommand();
                createCommand.CommandText = @"CREATE TABLE UserData (IP Text,Date Text)";
                createCommand.ExecuteNonQuery();

                using (var firstConnection = new SqliteConnection(connectionString))
                {
                    firstConnection.Open();

                    var updateCommand = firstConnection.CreateCommand();
                    updateCommand.CommandText = @"INSERT INTO UserData(IP,Date) values(@ip,@date)";
                    updateCommand.Parameters.AddWithValue("@ip", item.IpAddress);
                    updateCommand.Parameters.AddWithValue("@date", item.dateOfAccess);
                    updateCommand.ExecuteNonQuery();
                }

                using (var secondConnection = new SqliteConnection(connectionString))
                {
                    secondConnection.Open();
                    var queryCommand = secondConnection.CreateCommand();
                    queryCommand.CommandText = @"SELECT *FROM UserData";
                    var value = (string)queryCommand.ExecuteScalar();
                    Console.WriteLine(value);
                    message = value;
                }
                masterConnection.Close();
                return message;
            }
            catch(Exception ex)
            {
                throw;
            }
        }
    }
}
