using Dapper;
using PortfolioMVC5v3.Models;
using PortfolioMVC5v3.Repositories.Interfaces;
using PortfolioMVC5v3.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace PortfolioMVC5v3.Repositories.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly IDatabaseManager _manager;
        private const string TableName = "Projects";
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

            query.Append("ORDER BY [PositionInCv] ");

            try
            {

                using (IDbConnection connection = _manager.GetSqlConnection())
                {
                    var resultEnumerable = await connection.QueryAsync<Project>(query.ToString(),
                    new
                    {
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
                    return await connection.QueryFirstAsync<Project>(query.ToString(), new { projectId });
                }
            }
            catch (Exception e)
            {
                Logger.Log(e);
            }

            return null;
        }

        public async Task<bool> UpdateProject(Project project)
        {
            StringBuilder query = new StringBuilder();
            query.Append("UPDATE ");
            query.Append($"{TableName} ");

            query.Append("SET ");
            query.Append("[AuthorId] = @AuthorId, ");
            query.Append("[IconId] = @IconId, ");
            query.Append("[Title] = @Title, ");
            query.Append("[ShortDescription] = @ShortDescription, ");
            query.Append("[FullDescription] = @FullDescription, ");
            query.Append("[Commercial] = @Commercial, ");
            query.Append("[ShowInCv] = @ShowInCv, ");
            query.Append("[DateTimeCreated] = @DateTimeCreated, ");
            query.Append("[TempProject] = @TempProject, ");
            query.Append("[GitHubLink] = @GitHubLink ");

            query.Append("WHERE ");
            query.Append("[ProjectId] = @ProjectId ");

            try
            {
                using (IDbConnection connection = _manager.GetSqlConnection())
                {
                    var result = await connection.ExecuteAsync(query.ToString(), project);
                    return result > 0;
                }
            }
            catch (Exception e)
            {
                Logger.Log(e);
            }

            return false;
        }

        public async Task<int> CreateProject(Project project)
        {
            StringBuilder query = new StringBuilder();
            query.Append("INSERT INTO ");
            query.Append($"{TableName} ");
            query.Append("(");
            query.Append("[AuthorId], ");
            query.Append("[IconId], ");
            query.Append("[Title], ");
            query.Append("[ShortDescription], ");
            query.Append("[FullDescription], ");
            query.Append("[Commercial], ");
            query.Append("[ShowInCv], ");
            query.Append("[DateTimeCreated], ");
            query.Append("[TempProject], ");
            query.Append("[GitHubLink]");
            query.Append(")");

            query.Append(" OUTPUT INSERTED.ProjectId ");

            query.Append("VALUES");
            query.Append("(");
            query.Append("@AuthorId, ");
            query.Append("@IconId, ");
            query.Append("@Title, ");
            query.Append("@ShortDescription, ");
            query.Append("@FullDescription, ");
            query.Append("@Commercial, ");
            query.Append("@ShowInCv, ");
            query.Append("@DateTimeCreated, ");
            query.Append("@TempProject, ");
            query.Append("@GitHubLink ");
            query.Append(")");


            try
            {
                int projectId;
                using (IDbConnection connection = _manager.GetSqlConnection())
                {
                    projectId = await connection.QueryFirstAsync<int>(query.ToString(), project);

                    string updateQuery = $"UPDATE {TableName} SET [PositionInCv] = {projectId} WHERE [ProjectId] = {projectId}";
                    await connection.ExecuteAsync(updateQuery);
                }

                return projectId;
            }
            catch (Exception e)
            {
                Logger.Log(e);
            }

            return 0;
        }

        public async Task<bool> RemoveProject(int projectId)
        {
            StringBuilder query = new StringBuilder();
            query.Append("DELETE FROM ");
            query.Append($"{TableName} ");

            query.Append("WHERE ");
            query.Append("[ProjectId] = @ProjectId ");

            try
            {
                using (IDbConnection connection = _manager.GetSqlConnection())
                {
                    var result = await connection.ExecuteAsync(query.ToString(), new { projectId });
                    return result > 0;
                }
            }
            catch (Exception e)
            {
                Logger.Log(e);
            }

            return false;
        }

        public async Task<bool> RemoveScreenShoots(int projectId)
        {
            try
            {
                List<Image> screenShoots = await GetProjectScreenShoots(projectId);
                if (screenShoots.Count > 0)
                {
                    screenShoots.ForEach(s => RemoveScreenShootFile($"{s.Guid}{s.FileName}"));
                }

                StringBuilder query = new StringBuilder();
                query.Append("DELETE FROM ");
                query.Append("[Images] ");

                query.Append("WHERE ");
                query.Append("[ProjectId] = @ProjectId ");

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
            }
            catch (Exception e)
            {
                Logger.Log(e);
            }

            return false;
        }

        public async Task<List<Image>> GetProjectScreenShoots(int projectId)
        {
            StringBuilder query = new StringBuilder();
            query.Append("SELECT ");
            query.Append("* ");

            query.Append("FROM ");
            query.Append("[dbo].[Images] ");

            query.Append("WHERE ");
            query.Append("[ProjectId] = @projectId ");

            try
            {
                using (IDbConnection connection = _manager.GetSqlConnection())
                {
                    var result = await connection.QueryAsync<Image>(query.ToString(), new { projectId });
                    return result.ToList();
                }
            }
            catch (Exception e)
            {
                Logger.Log(e);
            }

            return new List<Image>();
        }

        public async Task<bool> AddScreenShoot(Image screenShoot)
        {
            StringBuilder query = new StringBuilder();
            query.Append("INSERT INTO ");
            query.Append("[Images] ");
            query.Append("(");
            query.Append("[FileName], ");
            query.Append("[ProjectId], ");
            query.Append("[Guid]");
            query.Append(")");

            query.Append(" OUTPUT INSERTED.ImageId ");

            query.Append("VALUES");
            query.Append("(");
            query.Append("@FileName, ");
            query.Append("@ProjectId, ");
            query.Append("@Guid ");
            query.Append(")");

            try
            {
                using (IDbConnection connection = _manager.GetSqlConnection())
                {
                    var result = await connection.QueryFirstAsync<int>(query.ToString(), screenShoot);
                    return result > 0;
                }
            }
            catch (Exception e)
            {
                Logger.Log(e);
            }

            return false;
        }

        public async Task<bool> ReorderProjectsPositionsInCv(int oldPositionProjectId, int newPositionProjectId)
        {
            StringBuilder query = new StringBuilder();

            query.Append($"DECLARE @oldPositionId int = {oldPositionProjectId} ");
            query.Append($"DECLARE @newPositionId int = {newPositionProjectId} ");
            query.Append("DECLARE @oldPosition int ");
            query.Append("DECLARE @newPosition int ");
            query.Append($"SELECT @oldPosition = [PositionInCv] FROM {TableName} WHERE ProjectId = @oldPositionId ");
            query.Append($"SELECT @newPosition = [PositionInCv] FROM {TableName} WHERE ProjectId = @newPositionId ");
            query.Append("IF @oldPosition < @newPosition ");
            query.Append("BEGIN ");
            query.Append($"UPDATE {TableName} SET [PositionInCv] = [PositionInCv] - 1 WHERE [PositionInCv] <= @newPosition ");
            query.Append("END ELSE ");
            query.Append("BEGIN ");
            query.Append($"UPDATE {TableName} SET [PositionInCv] = [PositionInCv] + 1 WHERE [PositionInCv] >= @newPosition ");
            query.Append("END ");
            query.Append($"UPDATE {TableName} SET [PositionInCV] = @newPosition WHERE ProjectId = @oldPositionId ");

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

        public async Task<bool> SetProjectShowInCvState(bool state, int projectId)
        {
            StringBuilder query = new StringBuilder();
            query.Append("UPDATE ");
            query.Append($"{TableName} ");

            query.Append("SET ");
            query.Append("[ShowInCv] = @state ");

            query.Append("WHERE ");
            query.Append("[ProjectId] = @ProjectId ");

            try
            {
                using (IDbConnection connection = _manager.GetSqlConnection())
                {
                    var result = await connection.ExecuteAsync(query.ToString(), new { state, projectId });
                    return result > 0;
                }
            }
            catch (Exception e)
            {
                Logger.Log(e);
            }

            return false;
        }

        private static void RemoveScreenShootFile(string filename)
        {
            try
            {
                string screenShootsPath = HttpContext.Current.Server.MapPath("~/Assets/ScreenShoots/");
                if (File.Exists($"{screenShootsPath}{filename}"))
                {
                    File.Delete($"{screenShootsPath}{filename}");
                }
            }
            catch (Exception e)
            {
                Logger.Log(e);
            }
        }
    }
}
