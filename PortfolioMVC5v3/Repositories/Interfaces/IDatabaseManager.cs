using System.Data;

namespace PortfolioMVC5v3.Repositories.Interfaces
{
    public interface IDatabaseManager
    {
        IDbConnection GetSqlConnection();
    }
}
