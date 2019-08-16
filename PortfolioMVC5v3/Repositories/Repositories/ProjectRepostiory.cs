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
    public class ProjectRepository : IProjectRepository
    {
        private readonly IDatabaseManager _manager;

        public ProjectRepository(IDatabaseManager databaseManager)
        {
             _manager = databaseManager;
        }


        public async Task<List<Project>> GetProjectsList(bool? showInCvProjects, bool? tempProjects)
        {
            StringBuilder query = new StringBuilder();
            query.Append("SELECT ");
            query.Append(" *  ");

            query.Append("FROM ");
            query.Append("[dbo].[Projects] ");
            
            if (showInCvProjects.HasValue)
            {
                query.Append("WHERE ");
                query.Append("showInCv =  @showInCvProjects ");
            }

            if (tempProjects.HasValue)
            {
                query.Append(!query.ToString().Contains("WHERE") ? "WHERE " : "AND ");
                query.Append("tempProject = @tempProjects ");
            }

            try
            {

                using (IDbConnection connection = _manager.GetSqlConnection())
                {
                    var resultEnumerable = await connection.QueryAsync<Project>(query.ToString(),
                    new{
                        showInCvProjects,
                        tempProjects
                    });

                    return resultEnumerable.ToList();
                }

            }
            catch (Exception e)
            {
                Logger.Log(e);
            }

            return new List<Project>();
        }

        public async Task<Project> GetProject(int projectId)
        {
            StringBuilder query = new StringBuilder();
            query.Append("SELECT ");
            query.Append(" *  ");

            query.Append("FROM ");
            query.Append("[dbo].[Projects] ");

            query.Append("WHERE ");
            query.Append("[ProjectId] = @projectId ");

            try
            {
                using (IDbConnection connection = _manager.GetSqlConnection())
                {
                    return await connection.QueryFirstAsync<Project>(query.ToString(), new {projectId});
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
