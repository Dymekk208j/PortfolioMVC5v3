using Dapper;
using PortfolioMVC5v3.Models;
using PortfolioMVC5v3.Repositories.Interfaces;
using PortfolioMVC5v3.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioMVC5v3.Repositories.Repositories
{
    public class TechnologyRepository : ITechnologyRepository
    {
        private const string TableName = "Technologies";
        private readonly IDatabaseManager _manager;

        public TechnologyRepository(IDatabaseManager databaseManager)
        {
            _manager = databaseManager;
        }


        public async Task<List<Technology>> GetAllTechnologiesListAsync()
        {
            StringBuilder query = new StringBuilder();
            query.Append("SELECT");
            query.Append(" * ");

            query.Append("FROM ");
            query.Append(TableName);

            try
            {
                using (IDbConnection connection = _manager.GetSqlConnection())
                {
                    var resultEnumerable = await connection.QueryAsync<Technology>(query.ToString());

                    return resultEnumerable.ToList();
                }
            }
            catch (Exception e)
            {
                Logger.Log(e);
            }

            return null;
        }

        public async Task<List<Technology>> GetProjectTechnologiesListAsync(int projectId)
        {
            StringBuilder query = new StringBuilder();
            query.Append("SELECT ");
            query.Append("Technologies.Name, ");
            query.Append("Technologies.KnowledgeLevel, ");
            query.Append("Technologies.ShowInCv, ");
            query.Append("Technologies.TechnologyId, ");
            query.Append("Technologies.ShowInAboutMePage ");

            query.Append("FROM ");
            query.Append("[dbo].[TechnologyProjects] as TP ");

            query.Append("JOIN Technologies ON ");
            query.Append("Technologies.TechnologyId = TP.Technology_TechnologyId ");

            query.Append("WHERE ");
            query.Append("Project_ProjectId = @projectId ");

            try
            {

                using (IDbConnection connection = _manager.GetSqlConnection())
                {
                    var resultEnumerable = await connection.QueryAsync<Technology>(query.ToString(), new { projectId });

                    return resultEnumerable.ToList();
                }
            }
            catch (Exception e)
            {
                Logger.Log(e);
            }

            return null;
        }

        public List<Technology> GetTechnologiesToShowInAboutMePage()
        {
            StringBuilder query = new StringBuilder();
            query.Append("SELECT ");
            query.Append("* ");

            query.Append("FROM ");
            query.Append($" {TableName} ");

            query.Append("WHERE ");
            query.Append("[ShowInAboutMePage] = 1 ");


            query.Append(" ORDER BY ");
            query.Append("KnowledgeLevel DESC ");


            try
            {
                using (IDbConnection connection = _manager.GetSqlConnection())
                {
                    var resultEnumerable = connection.Query<Technology>(query.ToString());

                    return resultEnumerable.ToList();
                }
            }
            catch (Exception e)
            {
                Logger.Log(e);
            }

            return null;
        }

        public async Task<List<Technology>> GetTechnologiesToShowInCv()
        {
            StringBuilder query = new StringBuilder();
            query.Append("SELECT ");
            query.Append("* ");

            query.Append("FROM ");
            query.Append($"{TableName} ");

            query.Append("WHERE ");
            query.Append("[ShowInCv] = 1 ");
            
            query.Append(" ORDER BY ");
            query.Append("KnowledgeLevel DESC ");


            try
            {
                using (IDbConnection connection = _manager.GetSqlConnection())
                {
                    var resultEnumerable = await connection.QueryAsync<Technology>(query.ToString());

                    return resultEnumerable.ToList();
                }
            }
            catch (Exception e)
            {
                Logger.Log(e);
            }

            return null;
        }

        public async Task<Technology> GetTechnology(int technologyId)
        {
            StringBuilder query = new StringBuilder();
            query.Append("SELECT TOP (1) ");
            query.Append("* ");

            query.Append("FROM ");
            query.Append(TableName);

            query.Append(" WHERE ");
            query.Append("TechnologyId = @technologyId ");


            try
            {
                using (IDbConnection connection = _manager.GetSqlConnection())
                {
                    var result = await connection.QueryFirstAsync<Technology>(query.ToString(), new { technologyId });

                    return result;
                }
            }
            catch (Exception e)
            {
                Logger.Log(e);
            }

            return null;
        }

        public async Task<int> InsertTechnology(Technology technology)
        {
            StringBuilder query = new StringBuilder();
            query.Append("INSERT INTO ");
            query.Append(TableName);

            query.Append(" (");
            query.Append("[KnowledgeLevel]");
            query.Append(",[Name]");
            query.Append(",[ShowInCv]");
            query.Append(",[ShowInAboutMePage]");
            query.Append(") ");

            query.Append(" OUTPUT INSERTED.TechnologyId ");

            query.Append(" VALUES ");
            query.Append("(");
            query.Append("@KnowledgeLevel");
            query.Append(", @Name");
            query.Append(", @ShowInCv");
            query.Append(", @ShowInAboutMePage");
            query.Append(")");


            try
            {
                using (IDbConnection connection = _manager.GetSqlConnection())
                {
                    var result = await connection.QueryFirstAsync<int>(query.ToString(),
                        new
                        {
                            technology.KnowledgeLevel,
                            technology.Name,
                            technology.ShowInCv,
                            technology.ShowInAboutMePage
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

        public async Task<bool> UpdateTechnology(Technology technology)
        {
            StringBuilder query = new StringBuilder();
            query.Append("UPDATE ");
            query.Append(TableName);

            query.Append(" SET ");
            query.Append("[KnowledgeLevel] = @KnowledgeLevel ");
            query.Append(",[Name] = @Name ");
            query.Append(",[ShowInCv] = @ShowInCv ");
            query.Append(",[ShowInAboutMePage] = @ShowInAboutMePage ");

            query.Append("WHERE ");
            query.Append("TechnologyId = @TechnologyId ");


            try
            {
                using (IDbConnection connection = _manager.GetSqlConnection())
                {
                    var result = await connection.ExecuteAsync(query.ToString(),
                        new
                        {
                            technology.KnowledgeLevel,
                            technology.Name,
                            technology.ShowInCv,
                            technology.TechnologyId,
                            technology.ShowInAboutMePage

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

        public async Task<bool> RemoveTechnology(int technologyId)
        {
            StringBuilder query = new StringBuilder();
            query.Append("DELETE FROM ");
            query.Append(TableName);

            query.Append(" WHERE ");
            query.Append("TechnologyId = @TechnologyId ");

            try
            {
                using (IDbConnection connection = _manager.GetSqlConnection())
                {
                    var result = await connection.ExecuteAsync(query.ToString(),
                        new
                        {
                            technologyId
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

        public async Task<bool> ClearShowInAboutMe()
        {
            StringBuilder query = new StringBuilder();
            query.Append("UPDATE ");
            query.Append($"{TableName} ");

            query.Append("SET ");
            query.Append("[ShowInAboutMePage] = 0 ");

            try
            {
                using (IDbConnection connection = _manager.GetSqlConnection())
                {
                    var result = await connection.ExecuteAsync(query.ToString());
                    return result > 0;
                }
            }
            catch (Exception e)
            {
                Logger.Log(e);
            }

            return false;
        }

        public async Task<bool> UpdateShowInAboutMe(int technologyId, bool show)
        {
            StringBuilder query = new StringBuilder();
            query.Append("UPDATE ");
            query.Append($"{TableName} ");

            query.Append("SET ");
            query.Append("[ShowInAboutMePage] = @show ");

            query.Append("WHERE ");
            query.Append("[TechnologyId] = @TechnologyId ");

            try
            {
                using (IDbConnection connection = _manager.GetSqlConnection())
                {
                    var result = await connection.ExecuteAsync(query.ToString(), new { show, technologyId });
                    return result > 0;
                }
            }
            catch (Exception e)
            {
                Logger.Log(e);
            }

            return false;
        }

        public async Task<bool> RemoveBindingsBetweenProjectAndTechnologies(int projectId)
        {
            StringBuilder query = new StringBuilder();
            query.Append("DELETE FROM ");
            query.Append("TechnologyProjects ");

            query.Append("WHERE ");
            query.Append("Project_ProjectId = @projectId");
            try
            {
                using (IDbConnection connection = _manager.GetSqlConnection())
                {
                    await connection.ExecuteAsync(query.ToString(), new { projectId });
                    return true;
                }
            }
            catch (Exception e)
            {
                Logger.Log(e);
            }

            return false;
        }

        public async Task<bool> CreateBindingBetweenProjectAndTechnology(int projectId, int technologyId)
        {
            StringBuilder query = new StringBuilder();
            query.Append("INSERT INTO ");
            query.Append("TechnologyProjects ");
            query.Append("( ");
            query.Append("[Technology_TechnologyId], ");
            query.Append("[Project_ProjectId] ");
            query.Append(") ");

            query.Append("VALUES ");
            query.Append("( ");
            query.Append("@technologyId, ");
            query.Append("@projectId ");
            query.Append(") ");
            try
            {
                using (IDbConnection connection = _manager.GetSqlConnection())
                {
                    var result = await connection.ExecuteAsync(query.ToString(), new { technologyId, projectId });
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
