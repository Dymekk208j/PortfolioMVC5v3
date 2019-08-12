using System;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using PortfolioMVC5v3.Models;
using PortfolioMVC5v3.Repositories.Interfaces;
using PortfolioMVC5v3.Utilities;

namespace PortfolioMVC5v3.Repositories.Repositories
{
    public class AboutMeRepository : IAboutMeRepository
    {
        private const string TableName = "AboutMeManagement";
        private readonly IDatabaseManager _manager;

        public AboutMeRepository(IDatabaseManager databaseManager)
        {
            _manager = databaseManager;
        }

        public async Task<AboutMe> GetAboutMeAsync()
        {
            StringBuilder query = new StringBuilder();
            query.Append("SELECT TOP(1) ");
            query.Append("AboutMeId, ");
            query.Append("Title, ");
            query.Append("Text, ");
            query.Append("ImageLink ");

            query.Append("FROM ");
            query.Append(TableName);

            query.Append(" WHERE ");
            query.Append("AboutMeId = 1");

            try
            {
                using (IDbConnection connection = _manager.GetSqlConnection())
                {
                    return await connection.QueryFirstOrDefaultAsync<AboutMe>(query.ToString());
                }

            }
            catch (Exception e)
            {
                Logger.Log(e);
            }

            return null;
        }

        public async Task<bool> UpdateAboutMeAsync(AboutMe aboutMe)
        {
            StringBuilder query = new StringBuilder();
            query.Append("UPDATE ");
            query.Append($"{TableName} ");

            query.Append("SET ");
            query.Append("[ImageLink] = @ImageLink ");
            query.Append(",[Text] = @Text ");
            query.Append(",[Title] = @Title ");

            try
            {
                using (IDbConnection connection = _manager.GetSqlConnection())
                {
                    var result = await connection.ExecuteAsync(query.ToString(),
                        new
                        {
                            aboutMe.ImageLink,
                            aboutMe.Text,
                            aboutMe.Title
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
