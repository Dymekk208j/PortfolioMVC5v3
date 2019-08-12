
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
    public class EmploymentHistoryRepository : IEmploymentHistoryRepository
    {
        private readonly IDatabaseManager _manager;

        private const string TableName = "EmploymentHistories";

        public EmploymentHistoryRepository(IDatabaseManager databaseManager)
        {
             _manager = databaseManager;
        }

        public async Task<List<EmploymentHistory>> GetAllEmploymentsHistories()
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
                    var resultEnumerable = await connection.QueryAsync<EmploymentHistory>(query.ToString());

                    return resultEnumerable.ToList();
                }
            }
            catch (Exception e)
            {
                Logger.Log(e);
            }

            return null;
        }

        public async Task<EmploymentHistory> GetEmploymentHistory(int employmentHistoryId)
        {
            StringBuilder query = new StringBuilder();
            query.Append("SELECT TOP (1) ");
            query.Append(" *  ");

            query.Append("FROM ");
            query.Append($"{TableName} ");

            query.Append("WHERE ");
            query.Append("[EmploymentHistoryId] = @employmentHistoryId ");

            try
            {
                using (IDbConnection connection = _manager.GetSqlConnection())
                {
                    return await connection.QueryFirstAsync<EmploymentHistory>(query.ToString(),
                        new {employmentHistoryId});
                }
            }
            catch (Exception e)
            {
                Logger.Log(e);
            }

            return null;
        }

        public async Task<int> InsertEmploymentHistory(EmploymentHistory employmentHistory)
        {
            StringBuilder query = new StringBuilder();
            query.Append("INSERT INTO ");
            query.Append(TableName);

            query.Append(" (");
            query.Append("[CompanyName]");
            query.Append(",[Position]");
            query.Append(",[CityOfEmployment]");
            query.Append(",[StartDate]");
            query.Append(",[EndDate]");
            query.Append(",[CurrentPlaceOfEmployment]");
            query.Append(",[ShowInCv]");
            query.Append(") ");

            query.Append(" OUTPUT INSERTED.EmploymentHistoryId ");

            query.Append(" VALUES ");
            query.Append("(");
            query.Append("@CompanyName");
            query.Append(", @Position");
            query.Append(", @CityOfEmployment");
            query.Append(", @StartDate");
            query.Append(", @EndDate");
            query.Append(", @CurrentPlaceOfEmployment");
            query.Append(", @ShowInCv");
            query.Append(")");


            try
            {
                using (IDbConnection connection = _manager.GetSqlConnection())
                {
                    var result = await connection.QueryFirstAsync<int>(query.ToString(),
                        new
                        {
                            employmentHistory.CompanyName,
                            employmentHistory.Position,
                            employmentHistory.CityOfEmployment,
                            employmentHistory.StartDate,
                            employmentHistory.EndDate,
                            employmentHistory.CurrentPlaceOfEmployment,
                            employmentHistory.ShowInCv
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

        public async Task<bool> UpdateEmploymentHistory(EmploymentHistory employmentHistory)
        {
            StringBuilder query = new StringBuilder();
            query.Append("UPDATE ");
            query.Append($"{TableName} ");

            query.Append("SET ");
            query.Append("[CompanyName] = @CompanyName ");
            query.Append(",[Position] = @Position ");
            query.Append(",[CityOfEmployment] = @CityOfEmployment ");
            query.Append(",[StartDate] = @StartDate ");
            query.Append(",[EndDate] = @EndDate ");
            query.Append(",[CurrentPlaceOfEmployment] = @CurrentPlaceOfEmployment ");
            query.Append(",[ShowInCv] = @ShowInCv ");

            query.Append("WHERE ");
            query.Append("EmploymentHistoryId = @EmploymentHistoryId ");


            try
            {
                using (IDbConnection connection = _manager.GetSqlConnection())
                {
                    var result = await connection.ExecuteAsync(query.ToString(),
                        new
                        {
                            employmentHistory.EmploymentHistoryId,
                            employmentHistory.CompanyName,
                            employmentHistory.Position,
                            employmentHistory.CityOfEmployment,
                            employmentHistory.StartDate,
                            employmentHistory.EndDate,
                            employmentHistory.CurrentPlaceOfEmployment,
                            employmentHistory.ShowInCv
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

        public async Task<bool> RemoveEmploymentHistory(int employmentHistoryId)
        {
            StringBuilder query = new StringBuilder();
            query.Append("DELETE FROM ");
            query.Append($"{TableName} ");

            query.Append("WHERE ");
            query.Append("EmploymentHistoryId = @employmentHistoryId ");

            try
            {
                using (IDbConnection connection = _manager.GetSqlConnection())
                {
                    var result = await connection.ExecuteAsync(query.ToString(),
                        new
                        {
                            employmentHistoryId
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
