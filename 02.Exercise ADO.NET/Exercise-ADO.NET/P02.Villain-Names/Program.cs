using Microsoft.Data.SqlClient;
using System.Text;

namespace P02.Villain_Names
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            await using SqlConnection connection =
                new SqlConnection(Config.ConnnectionString);
            await connection.OpenAsync();

            string result = await GetAllVilliansAndCountOfTheirMinionsAsync(connection);
            Console.WriteLine(result);
        }


        static async Task<string> GetAllVilliansAndCountOfTheirMinionsAsync(SqlConnection sqlConnection)
        {
            StringBuilder sb = new StringBuilder();

            SqlCommand sqlCommand =
                new SqlCommand(SqlQueries.GetAllVilliansAndCountOfTheirMinions, sqlConnection);
            SqlDataReader reader = await sqlCommand.ExecuteReaderAsync();
            while (reader.Read())
            {
                string villainName = (string)reader["Name"];
                int minionsCount = (int)reader["MinionsCount"];


                sb.AppendLine($"{villainName} - {minionsCount}");
            }

            return sb.ToString().TrimEnd();
        }

        static async Task<string> GetVillainWithAllMinionsByIdAsync(SqlConnection sqlConnection, int villainId)
        {
            SqlCommand sqlCommand = 
                new SqlCommand(SqlQueries.GetVillainNameById, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@Id", villainId);
        }
    }
}