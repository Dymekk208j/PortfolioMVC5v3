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
    public class EducationRepository : IEducationRepository
    {
        private readonly IDatabaseManager _manager;

        private const string TableName = "Educations";

        public EducationRepository(IDatabaseManager databaseManager)
        {
             _manager = databaseManager;
        }

        public async Task<List<Education>> GetAllEducations()
        {
            StringBuilder query = new StringBuilder();
            query.Append("SELECT ");
            query.Append(" *  ");

            query.Append("FROM ");
            query.Append($"{TableName} ");

            query.Append("ORDER BY [StartDate] DESC");

            try
            {

                using (IDbConnection connection = _manager.GetSqlConnection())
                {
                    var resultEnumerable = await connection.QueryAsync<Education>(query.ToString());

                    return resultEnumerable.ToList();
                }
            }
            catch (Exception e)
            {
                Logger.Log(e);
            }

            return null;
        }

        public async Task<Education> GetEducation(int educationId)
        {
            StringBuilder query = new StringBuilder();
            query.Append("SELECT TOP (1) ");
            query.Append(" *  ");

            query.Append("FROM ");
            query.Append($"{TableName} ");

            query.Append("WHERE ");
            query.Append("[EducationId] = @educationId ");

            try
            {
                using (IDbConnection connection = _manager.GetSqlConnection())
                {
                    return await connection.QueryFirstAsync<Education>(query.ToString(), new {educationId});
                }
            }
            catch (Exception e)
            {
                Logger.Log(e);
            }

            return null;
        }

        public async Task<int> InsertEducation(Education education)
        {
            StringBuilder query = new StringBuilder();
            query.Append("INSERT INTO ");
            query.Append(TableName);

            query.Append(" (");
            query.Append("[SchoolName]");
            query.Append(",[Department]");
            query.Append(",[Specialization]");
            query.Append(",[StartDate]");
            query.Append(",[EndDate]");
            query.Append(",[CurrentPlaceOfEducation]");
            query.Append(",[ShowInCv]");
            query.Append(") ");

            query.Append(" OUTPUT INSERTED.EducationId ");

            query.Append(" VALUES ");
            query.Append("(");
            query.Append("@SchoolName");
            query.Append(", @Department");
            query.Append(", @Specialization");
            query.Append(", @StartDate");
            query.Append(", @EndDate");
            query.Append(", @CurrentPlaceOfEducation");
            query.Append(", @ShowInCv");
            query.Append(")");


            try
            {
                using (IDbConnection connection = _manager.GetSqlConnection())
                {
                    var result = await connection.QueryFirstAsync<int>(query.ToString(),
                        new
                        {
                            education.SchoolName,
                            education.Department,
                            education.Specialization,
                            education.StartDate,
                            education.EndDate,
                            education.CurrentPlaceOfEducation,
                            education.ShowInCv
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

        public async Task<bool> UpdateEducation(Education education)
        {
            StringBuilder query = new StringBuilder();
            query.Append("UPDATE ");
            query.Append($"{TableName} ");

            query.Append("SET ");
            query.Append("[SchoolName] = @SchoolName ");
            query.Append(",[Department] = @Department ");
            query.Append(",[Specialization] = @Specialization ");
            query.Append(",[StartDate] = @StartDate ");
            query.Append(",[EndDate] = @EndDate ");
            query.Append(",[CurrentPlaceOfEducation] = @CurrentPlaceOfEducation ");
            query.Append(",[ShowInCv] = @ShowInCv ");

            query.Append("WHERE ");
            query.Append("EducationId = @EducationId ");


            try
            {
                using (IDbConnection connection = _manager.GetSqlConnection())
                {
                    var result = await connection.ExecuteAsync(query.ToString(),
                        new
                        {
                            education.EducationId,
                            education.SchoolName,
                            education.Department,
                            education.Specialization,
                            education.StartDate,
                            education.EndDate,
                            education.CurrentPlaceOfEducation,
                            education.ShowInCv
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

        public async Task<bool> RemoveEducation(int educationId)
        {
            StringBuilder query = new StringBuilder();
            query.Append("DELETE FROM ");
            query.Append($"{TableName} ");

            query.Append("WHERE ");
            query.Append("EducationId = @educationId ");

            try
            {
                using (IDbConnection connection = _manager.GetSqlConnection())
                {
                    var result = await connection.ExecuteAsync(query.ToString(),
                        new
                        {
                            educationId
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
