using PortfolioMVC5v3.Repositories.Interfaces;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PortfolioMVC5v3.Models;
using PortfolioMVC5v3.Utilities;

namespace PortfolioMVC5v3.Repositories.Repositories
{
    public class AchievementRepository : IAchievementRepository
    {
        private readonly IDatabaseManager _manager;

        private const string TableName = "Achievements";

        public AchievementRepository(IDatabaseManager databaseManager)
        {
             _manager = databaseManager;
        }

        public async Task<List<Achievement>> GetAllAchievements()
        {
            StringBuilder query = new StringBuilder();
            query.Append("SELECT ");
            query.Append(" *  ");

            query.Append("FROM ");
            query.Append($"{TableName} ");

            query.Append("ORDER BY [Date] DESC");

            try
            {

                using (IDbConnection connection = _manager.GetSqlConnection())
                {
                    var resultEnumerable = await connection.QueryAsync<Achievement>(query.ToString());

                    return resultEnumerable.ToList();
                }
            }
            catch (Exception e)
            {
                Logger.Log(e);
            }

            return null;
        }

        public async Task<Achievement> GetAchievement(int achievementId)
        {
            StringBuilder query = new StringBuilder();
            query.Append("SELECT TOP (1) ");
            query.Append(" *  ");

            query.Append("FROM ");
            query.Append($"{TableName} ");

            query.Append("WHERE ");
            query.Append("[AchievementId] = @achievementId ");

            try
            {
                using (IDbConnection connection = _manager.GetSqlConnection())
                {
                    return await connection.QueryFirstAsync<Achievement>(query.ToString(), new {achievementId});
                }
            }
            catch (Exception e)
            {
                Logger.Log(e);
            }

            return null;
        }

        public async Task<int> InsertAchievement(Achievement achievement)
        {
            StringBuilder query = new StringBuilder();
            query.Append("INSERT INTO ");
            query.Append(TableName);

            query.Append(" (");
            query.Append("[Title]");
            query.Append(",[Description]");
            query.Append(",[Date]");
            query.Append(",[ShowInCv]");
            query.Append(") ");

            query.Append(" OUTPUT INSERTED.AchievementId ");

            query.Append(" VALUES ");
            query.Append("(");
            query.Append("@Title");
            query.Append(", @Description");
            query.Append(", @Date");
            query.Append(", @ShowInCv");
            query.Append(")");


            try
            {
                using (IDbConnection connection = _manager.GetSqlConnection())
                {
                    var result = await connection.QueryFirstAsync<int>(query.ToString(),
                        new
                        {
                            achievement.Title,
                            achievement.Description,
                            achievement.Date,
                            achievement.ShowInCv
                        });

                    return result;
                }
            }
            catch (Exception e)
            {
                Logger.Log(e);
            }

            return -1;
        }

        public async Task<bool> UpdateAchievement(Achievement achievement)
        {
            StringBuilder query = new StringBuilder();
            query.Append("UPDATE ");
            query.Append($"{TableName} ");

            query.Append("SET ");
            query.Append("[Title] = @Title ");
            query.Append(",[Description] = @Description ");
            query.Append(",[Date] = @Date ");
            query.Append(",[ShowInCv] = @ShowInCv ");

            query.Append("WHERE ");
            query.Append("AchievementId = @AchievementId ");


            try
            {
                using (IDbConnection connection = _manager.GetSqlConnection())
                {
                    var result = await connection.ExecuteAsync(query.ToString(),
                        new
                        {
                            achievement.Title,
                            achievement.Description,
                            achievement.Date,
                            achievement.ShowInCv,
                            achievement.AchievementId
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

        public async Task<bool> RemoveAchievement(int achievementId)
        {
            StringBuilder query = new StringBuilder();
            query.Append("DELETE FROM ");
            query.Append($"{TableName} ");

            query.Append("WHERE ");
            query.Append("AchievementId = @achievementId ");

            try
            {
                using (IDbConnection connection = _manager.GetSqlConnection())
                {
                    var result = await connection.ExecuteAsync(query.ToString(),
                        new
                        {
                            achievementId
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

        public async Task<List<Achievement>> GetAchievementsToShowInCvAsync()
        {
            StringBuilder query = new StringBuilder();
            query.Append("SELECT ");
            query.Append(" *  ");

            query.Append("FROM ");
            query.Append($"{TableName} ");

            query.Append("WHERE ");
            query.Append("[ShowInCv] = 1 ");

            query.Append("ORDER BY [Date] DESC");

            try
            {

                using (IDbConnection connection = _manager.GetSqlConnection())
                {
                    var resultEnumerable = await connection.QueryAsync<Achievement>(query.ToString());

                    return resultEnumerable.ToList();
                }
            }
            catch (Exception e)
            {
                Logger.Log(e);
            }

            return null;
        }
    }
}
