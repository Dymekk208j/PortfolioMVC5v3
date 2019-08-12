using System;
using PortfolioMVC5v3.Repositories.Interfaces;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using PortfolioMVC5v3.Models;
using PortfolioMVC5v3.Utilities;

namespace PortfolioMVC5v3.Repositories.Repositories
{
    public class MainPageRepository : IMainPageRepository
    {
        private readonly IDatabaseManager _manager;

        private const string TableName = "MainPageManagement";

        public MainPageRepository(IDatabaseManager databaseManager)
        {
            _manager = databaseManager;
        }

        public async Task<MainPage> GetMainPageAsync()
        {
            StringBuilder query = new StringBuilder();
            query.Append("SELECT TOP(1) ");
            query.Append("*  ");

            query.Append("FROM ");
            query.Append($"{TableName} ");

            try
            {
                using (IDbConnection connection = _manager.GetSqlConnection())
                {
                    var resultEnumerable = await connection.QueryFirstAsync<MainPage>(query.ToString());
                    return resultEnumerable;

                }


            }
            catch (Exception e)
            {
                Logger.Log(e);
            }

            return null;
        }

        public async Task<bool> UpdateMainPage(MainPage mainPage)
        {
            StringBuilder query = new StringBuilder();
            query.Append("UPDATE ");
            query.Append($"{TableName} ");

            query.Append("SET ");
            query.Append("[Description] = @Description ");
            query.Append(",[ImageLink] = @ImageLink ");
            query.Append(",[Title] = @Title ");

            query.Append("WHERE ");
            query.Append("Id = @Id ");


            try
            {
                using (IDbConnection connection = _manager.GetSqlConnection())
                {
                    var result = await connection.ExecuteAsync(query.ToString(),
                        new
                        {
                            mainPage.Description,
                            mainPage.Id,
                            mainPage.ImageLink,
                            mainPage.Title

                        });

                    return result > 0;
                }
            }
            catch (Exception e)
            {
                Logger.Log(e);
            }

            return false;
        }
    }
}
