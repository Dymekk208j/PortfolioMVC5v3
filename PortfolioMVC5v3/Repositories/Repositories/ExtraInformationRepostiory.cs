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
    public class ExtraInformationRepository : IExtraInformationRepository
    {
        private readonly IDatabaseManager _manager;

        private const string TableName = "ExtraInformation";

        public ExtraInformationRepository(IDatabaseManager databaseManager)
        {
             _manager = databaseManager;
        }

        public async Task<List<ExtraInformation>> GetAllExtraInformation()
        {
            StringBuilder query = new StringBuilder();
            query.Append("SELECT ");
            query.Append(" *  ");

            query.Append("FROM ");
            query.Append($"{TableName} ");

            query.Append("ORDER BY ");
            query.Append("PositionInCv ");


            try
            {
                using (IDbConnection connection = _manager.GetSqlConnection())
                {
                    var resultEnumerable = await connection.QueryAsync<ExtraInformation>(query.ToString());

                    return resultEnumerable.ToList();
                }
            }
            catch (Exception e)
            {
                Logger.Log(e);
            }

            return new List<ExtraInformation>();
        }

        public async Task<ExtraInformation> GetExtraInformation(int extraInformationId)
        {
            StringBuilder query = new StringBuilder();
            query.Append("SELECT TOP (1) ");
            query.Append(" *  ");

            query.Append("FROM ");
            query.Append($"{TableName} ");

            query.Append("WHERE ");
            query.Append("[ExtraInformationId] = @extraInformationId ");

            try
            {
                using (IDbConnection connection = _manager.GetSqlConnection())
                {
                    return await connection.QueryFirstAsync<ExtraInformation>(query.ToString(),
                        new {extraInformationId});
                }
            }
            catch (Exception e)
            {
                Logger.Log(e);
            }

            return null;
        }

        public async Task<int> InsertExtraInformation(ExtraInformation extraInformation)
        {
            StringBuilder query = new StringBuilder();
            query.Append("INSERT INTO ");
            query.Append(TableName);

            query.Append(" (");
            query.Append("[Title]");
            query.Append(",[Type]");
            query.Append(",[ShowInCv]");
            query.Append(") ");

            query.Append(" OUTPUT INSERTED.ExtraInformationId ");

            query.Append(" VALUES ");
            query.Append("(");
            query.Append("@Title");
            query.Append(", @Type");
            query.Append(", @ShowInCv");
            query.Append(")");


            try
            {
                using (IDbConnection connection = _manager.GetSqlConnection())
                {
                    var extraInformationId = await connection.QueryFirstAsync<int>(query.ToString(),
                        new
                        {
                            extraInformation.Title,
                            extraInformation.Type,
                            extraInformation.ShowInCv
                        });

                    string updateQuery = $"UPDATE {TableName} SET [PositionInCv] = {extraInformationId} WHERE [ExtraInformationId] = {extraInformationId}";
                    await connection.ExecuteAsync(updateQuery);

                    return extraInformationId;
                }
            }
            catch (Exception e)
            {
                Logger.Log(e);
            }

            return -1;
        }

        public async Task<bool> UpdateExtraInformation(ExtraInformation extraInformation)
        {
            StringBuilder query = new StringBuilder();
            query.Append("UPDATE ");
            query.Append($"{TableName} ");

            query.Append("SET ");
            query.Append("[Title] = @Title ");
            query.Append(",[Type] = @Type ");
            query.Append(",[ShowInCv] = @ShowInCv ");

            query.Append("WHERE ");
            query.Append("ExtraInformationId = @ExtraInformationId ");

            try
            {
                using (IDbConnection connection = _manager.GetSqlConnection())
                {
                    var result = await connection.ExecuteAsync(query.ToString(),
                        new
                        {
                            extraInformation.ExtraInformationId,
                            extraInformation.Title,
                            extraInformation.Type,
                            extraInformation.ShowInCv
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

        public async Task<bool> RemoveExtraInformation(int extraInformationId)
        {
            StringBuilder query = new StringBuilder();
            query.Append("DELETE FROM ");
            query.Append($"{TableName} ");

            query.Append("WHERE ");
            query.Append("ExtraInformationId = @extraInformationId ");

            try
            {
                using (IDbConnection connection = _manager.GetSqlConnection())
                {
                    var result = await connection.ExecuteAsync(query.ToString(),
                        new
                        {
                            extraInformationId
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

        public async Task<List<ExtraInformation>> GetExtraInformationToShowInCvAsync()
        {
            StringBuilder query = new StringBuilder();
            query.Append("SELECT ");
            query.Append(" *  ");

            query.Append("FROM ");
            query.Append($"{TableName} ");

            query.Append("WHERE ");
            query.Append("[ShowInCv] = 1 ");

            query.Append("ORDER BY ");
            query.Append("PositionInCV ");

            try
            {
                using (IDbConnection connection = _manager.GetSqlConnection())
                {
                    var resultEnumerable = await connection.QueryAsync<ExtraInformation>(query.ToString());

                    return resultEnumerable.ToList();
                }
            }
            catch (Exception e)
            {
                Logger.Log(e);
            }

            return new List<ExtraInformation>();
        }

        public async Task<List<ExtraInformation>> GetExtraInformationNotShownInCvAsync()
        {
            StringBuilder query = new StringBuilder();
            query.Append("SELECT ");
            query.Append(" *  ");

            query.Append("FROM ");
            query.Append($"{TableName} ");

            query.Append("WHERE ");
            query.Append("[ShowInCv] = 0 ");

            query.Append("ORDER BY ");
            query.Append("PositionInCV ");

            try
            {
                using (IDbConnection connection = _manager.GetSqlConnection())
                {
                    var resultEnumerable = await connection.QueryAsync<ExtraInformation>(query.ToString());

                    return resultEnumerable.ToList();
                }
            }
            catch (Exception e)
            {
                Logger.Log(e);
            }

            return new List<ExtraInformation>();
        }

        public async Task<bool> ReorderExtraInformationPositionsInCv(int oldPositionProjectId, int newPositionProjectId)
        {
            StringBuilder query = new StringBuilder();

            query.Append($"DECLARE @oldPositionId int = {oldPositionProjectId} ");
            query.Append($"DECLARE @newPositionId int = {newPositionProjectId} ");
            query.Append("DECLARE @oldPosition int ");
            query.Append("DECLARE @newPosition int ");
            query.Append($"SELECT @oldPosition = [PositionInCv] FROM {TableName} WHERE ExtraInformationId = @oldPositionId ");
            query.Append($"SELECT @newPosition = [PositionInCv] FROM {TableName} WHERE ExtraInformationId = @newPositionId ");
            query.Append("IF @oldPosition < @newPosition ");
            query.Append("BEGIN ");
            query.Append($"UPDATE {TableName} SET [PositionInCv] = [PositionInCv] - 1 WHERE [PositionInCv] <= @newPosition ");
            query.Append("END ELSE ");
            query.Append("BEGIN ");
            query.Append($"UPDATE {TableName} SET [PositionInCv] = [PositionInCv] + 1 WHERE [PositionInCv] >= @newPosition ");
            query.Append("END ");
            query.Append($"UPDATE {TableName} SET [PositionInCV] = @newPosition WHERE ExtraInformationId = @oldPositionId ");

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

        public async Task<bool> SetExtraInformationShowInCvState(bool state, int extraInformationId)
        {
            StringBuilder query = new StringBuilder();
            query.Append("UPDATE ");
            query.Append($"{TableName} ");

            query.Append("SET ");
            query.Append("[ShowInCv] = @state ");

            query.Append("WHERE ");
            query.Append("[ExtraInformationId] = @extraInformationId ");

            try
            {
                using (IDbConnection connection = _manager.GetSqlConnection())
                {
                    var result = await connection.ExecuteAsync(query.ToString(), new { state, extraInformationId });
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
