using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using PortfolioMVC5v3.Repositories.Interfaces;

namespace PortfolioMVC5v3.Repositories.Repositories
{
    public class DatabaseManager : IDatabaseManager
    {
        private readonly string _connectionString;

        public DatabaseManager()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["AppConnectionString"].ConnectionString;
        }

        public IDbConnection GetSqlConnection()
        {
            IDbConnection connection = new SqlConnection(_connectionString);
            connection.Open();
            return connection;
        }
    }
}