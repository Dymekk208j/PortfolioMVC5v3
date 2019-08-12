using System;
using System.Collections.Generic;
using PortfolioMVC5v3.Repositories.Interfaces;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using PortfolioMVC5v3.Models;
using PortfolioMVC5v3.Utilities;

namespace PortfolioMVC5v3.Repositories.Repositories
{
    public class IconRepository : IIconRepository
    {
        private readonly IDatabaseManager _manager;

        private const string TableName = "Icons";

        public IconRepository(IDatabaseManager databaseManager)
        {
             _manager = databaseManager;
        }

        public async Task<List<Icon>> GetAllIcons()
        {
            StringBuilder query = new StringBuilder();
            query.Append("SELECT ");
            query.Append("* ");

            query.Append("FROM ");
            query.Append($"{TableName} ");

            try
            {
                using (IDbConnection connection = _manager.GetSqlConnection())
                {
                    var result = await connection.QueryAsync<Icon>(query.ToString());

                    return result.ToList();
                }
            }
            catch (Exception e)
            {
                Logger.Log(e);
            }

            return null;
        }

        public async Task<Icon> GetIconAsync(int iconId)
        {
            StringBuilder query = new StringBuilder();
            query.Append("SELECT TOP (1) ");
            query.Append("* ");

            query.Append("FROM ");
            query.Append($"{TableName} ");

            query.Append("WHERE ");
            query.Append("IconId = @iconId ");


            try
            {
                using (IDbConnection connection = _manager.GetSqlConnection())
                {
                    var result = await connection.QueryFirstAsync<Icon>(query.ToString(), new {iconId});

                    return result;
                }
            }
            catch (Exception e)
            {
                Logger.Log(e);
            }

            return null;
        }

        public async Task<bool> RemoveIconAsync(int iconId)
        {
            StringBuilder query = new StringBuilder();
            query.Append("DELETE FROM ");
            query.Append($"{TableName} ");
            
            query.Append("WHERE ");
            query.Append("IconId = @iconId ");

            try
            {
                using (IDbConnection connection = _manager.GetSqlConnection())
                {
                    var result = await connection.ExecuteAsync(query.ToString(),
                        new
                        {
                            iconId
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

        public Task<bool> AddIconAsync(Icon icon)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> UpdateIconAsync(Icon icon)
        {
            throw new System.NotImplementedException();
        }
    }
}
