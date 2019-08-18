
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

            query.Append("ORDER BY PositionInCv, [StartDate] DESC");

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
                    var employmentHistoryId = await connection.QueryFirstAsync<int>(query.ToString(),
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

                    string updateQuery = $"UPDATE {TableName} SET [PositionInCv] = {employmentHistoryId} WHERE [EmploymentHistoryId] = {employmentHistoryId}";
                    await connection.ExecuteAsync(updateQuery);

                    return employmentHistoryId;
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

        public async Task<List<EmploymentHistory>> GetEmploymentHistoriesToShowInCvAsync()
        {
            StringBuilder query = new StringBuilder();
            query.Append("SELECT ");
            query.Append(" *  ");

            query.Append("FROM ");
            query.Append($"{TableName} ");

            query.Append("WHERE ");
            query.Append("[ShowInCv] = 1 ");

            query.Append("ORDER BY PositionInCv, [StartDate] DESC");

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

            return new List<EmploymentHistory>();
        }

        public async Task<bool> ReorderEmploymentHistoriesPositionsInCv(int oldPositionEmploymentHistoryId, int newPositionEmploymentHistoryId)
        {
            StringBuilder query = new StringBuilder();

            query.Append($"DECLARE @oldPositionId int = {oldPositionEmploymentHistoryId} ");
            query.Append($"DECLARE @newPositionId int = {newPositionEmploymentHistoryId} ");
            query.Append("DECLARE @oldPosition int ");
            query.Append("DECLARE @newPosition int ");
            query.Append($"SELECT @oldPosition = [PositionInCv] FROM {TableName} WHERE EmploymentHistoryId = @oldPositionId ");
            query.Append($"SELECT @newPosition = [PositionInCv] FROM {TableName} WHERE EmploymentHistoryId = @newPositionId ");
            query.Append("IF @oldPosition < @newPosition ");
            query.Append("BEGIN ");
            query.Append($"UPDATE {TableName} SET [PositionInCv] = [PositionInCv] - 1 WHERE [PositionInCv] <= @newPosition ");
            query.Append("END ELSE ");
            query.Append("BEGIN ");
            query.Append($"UPDATE {TableName} SET [PositionInCv] = [PositionInCv] + 1 WHERE [PositionInCv] >= @newPosition ");
            query.Append("END ");
            query.Append($"UPDATE {TableName} SET [PositionInCV] = @newPosition WHERE EmploymentHistoryId = @oldPositionId ");

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

        public async Task<bool> SetEmploymentHistoryShowInCvState(bool state, int employmentHistoryId)
        {
            StringBuilder query = new StringBuilder();
            query.Append("UPDATE ");
            query.Append($"{TableName} ");

            query.Append("SET ");
            query.Append("[ShowInCv] = @state ");

            query.Append("WHERE ");
            query.Append("[EmploymentHistoryId] = @employmentHistoryId ");

            try
            {
                using (IDbConnection connection = _manager.GetSqlConnection())
                {
                    var result = await connection.ExecuteAsync(query.ToString(), new { state, employmentHistoryId });
                    return result > 0;
                }
            }
            catch (Exception e)
            {
                Logger.Log(e);
            }

            return false;
        }

        public async Task<List<EmploymentHistory>> GetEmploymentHistoriesNotShowInCvAsync()
        {
            StringBuilder query = new StringBuilder();
            query.Append("SELECT ");
            query.Append(" *  ");

            query.Append("FROM ");
            query.Append($"{TableName} ");

            query.Append("WHERE ");
            query.Append("[ShowInCv] = 0 ");

            query.Append("ORDER BY PositionInCv, [StartDate] DESC");

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

            return new List<EmploymentHistory>();
        }
    }
}
