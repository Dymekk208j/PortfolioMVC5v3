using Dapper;
using Microsoft.AspNet.Identity.EntityFramework;
using PortfolioMVC5v3.Models.ViewModels;
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
    public class AccountRepository : IAccountRepository
    {
        private readonly IDatabaseManager _manager;


        private const string TableName = "AspNetUsers";

        public AccountRepository(IDatabaseManager databaseManager)
        {
            _manager = databaseManager;
        }


        public async Task<List<AppUserViewModel>> GetAllUsers()
        {
            StringBuilder query = new StringBuilder();

            query.Append("SELECT ");
            query.Append("[Id], ");
            query.Append("[FirstName], ");
            query.Append("[Email], ");
            query.Append("[LastName], ");
            query.Append("[Blocked], ");
            query.Append("[UserName] ");

            query.Append("FROM ");
            query.Append($"{TableName} ");

            try
            {

                using (IDbConnection connection = _manager.GetSqlConnection())
                {
                    var resultEnumerable = await connection.QueryAsync<AppUserViewModel>(query.ToString());

                    return resultEnumerable.ToList();
                }
            }
            catch (Exception e)
            {
                Logger.Log(e);
            }

            return null;
        }

        public async Task<List<IdentityRole>> GetAllRoles()
        {
            StringBuilder query = new StringBuilder();

            query.Append("SELECT * ");

            query.Append("FROM ");
            query.Append("AspNetRoles ");

            try
            {
                using (IDbConnection connection = _manager.GetSqlConnection())
                {
                    var resultEnumerable = await connection.QueryAsync<IdentityRole>(query.ToString());

                    return resultEnumerable.ToList();
                }
            }
            catch (Exception e)
            {
                Logger.Log(e);
            }

            return null;
        }

        public async Task<IdentityRole> GetRole(string id)
        {
            StringBuilder query = new StringBuilder();

            query.Append("SELECT TOP(1) * ");

            query.Append("FROM ");
            query.Append("AspNetRoles ");

            query.Append("WHERE ");
            query.Append("Id = @id ");

            try
            {
                using (IDbConnection connection = _manager.GetSqlConnection())
                {
                    var resultEnumerable = await connection.QueryFirstAsync<IdentityRole>(query.ToString(), new { id });

                    return resultEnumerable;
                }
            }
            catch (Exception e)
            {
                Logger.Log(e);
            }

            return null;
        }

        public async Task<bool> UpdateRole(IdentityRole role)
        {
            StringBuilder query = new StringBuilder();

            query.Append("UPDATE ");
            query.Append("AspNetRoles ");

            query.Append("SET ");
            query.Append("Name = @Name ");

            query.Append("WHERE ");
            query.Append("Id = @id ");

            try
            {
                using (IDbConnection connection = _manager.GetSqlConnection())
                {
                    var resultEnumerable = await connection.ExecuteAsync(query.ToString(), new { role.Id, role.Name });

                    return resultEnumerable > 0;
                }
            }
            catch (Exception e)
            {
                Logger.Log(e);
            }

            return false;
        }

        public async Task<bool> AddRole(IdentityRole role)
        {
            StringBuilder query = new StringBuilder();

            query.Append("INSERT INTO ");
            query.Append("AspNetRoles ");
            query.Append("( ");
            query.Append("[Id], ");
            query.Append("[Name] ");
            query.Append(") ");

            query.Append("VALUES( ");
            query.Append("@Id, ");
            query.Append("@Name ");
            query.Append(") ");

            try
            {
                using (IDbConnection connection = _manager.GetSqlConnection())
                {
                    var resultEnumerable = await connection.ExecuteAsync(query.ToString(), new { role.Id, role.Name });

                    return resultEnumerable > 0;
                }
            }
            catch (Exception e)
            {
                Logger.Log(e);
            }

            return false;
        }

        public async Task<bool> RemoveRole(string id)
        {
            StringBuilder query = new StringBuilder();

            query.Append("DELETE FROM ");
            query.Append("AspNetRoles ");

            query.Append("WHERE ");
            query.Append("Id = @id ");

            try
            {
                using (IDbConnection connection = _manager.GetSqlConnection())
                {
                    var resultEnumerable = await connection.ExecuteAsync(query.ToString(), new { id });

                    return resultEnumerable > 0;
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
