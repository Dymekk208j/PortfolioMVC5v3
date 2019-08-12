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
    public class ImageRepository : IImageRepository
    {
        private readonly IDatabaseManager _manager;

        private const string TableName = "Images";

        public ImageRepository(IDatabaseManager databaseManager)
        {
             _manager = databaseManager;
        }

        public async Task<List<Image>> GetProjectImages(int projectId)
        {
            StringBuilder query = new StringBuilder();
            query.Append("SELECT ");
            query.Append("* ");

            query.Append("FROM ");
            query.Append($"{TableName} ");

            query.Append("WHERE ");
            query.Append("Project_ProjectId = @projectId ");

            try
            {
                using (IDbConnection connection = _manager.GetSqlConnection())
                {
                    var resultEnumerable = await connection.QueryAsync<Image>(query.ToString(), new {projectId});

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
