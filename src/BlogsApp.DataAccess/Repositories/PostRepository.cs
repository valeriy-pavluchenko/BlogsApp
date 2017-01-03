using BlogsApp.DataAccess.Entities;
using BlogsApp.DataAccess.Exceptions;

using Dapper;

using System.Data;
using System.Linq;

namespace BlogsApp.DataAccess.Repositories
{
    public class PostRepository : BaseRepository
    {
        public PostRepository(string connectionString) : base(connectionString)
        {
        }

        public int Insert(Post entity)
        {
            using (var connection = DbConnection)
            {
                var entityId = connection.Query<int>(
                    sql: "usp_Post_Insert",
                    commandType: CommandType.StoredProcedure,
                    param: new
                    {
                        entity.UserId,
                        entity.Title,
                        entity.Content,
                        entity.AddedOnUtc
                    }).Single();

                return entityId;
            }
        }

        public Post GetById(int postId)
        {
            using (var connection = DbConnection)
            {
                var entity = connection.Query<Post>(
                    sql: "usp_Post_GetById",
                    commandType: CommandType.StoredProcedure,
                    param: new
                    {
                        PostId = postId
                    }).SingleOrDefault();

                if (entity == null)
                    throw new DbNotFoundException("Can't find the post.");

                return entity;
            }
        }

        public Post[] GetList(int limit, int offset)
        {
            using (var connection = DbConnection)
            {
                var entities = connection.Query<Post>(
                    sql: "usp_Post_GetList",
                    commandType: CommandType.StoredProcedure,
                    param: new
                    {
                        Limit = limit,
                        Offset = offset
                    }).ToArray();

                return entities;
            }
        }

        public Post[] GetListByUserId(int userId, int limit, int offset)
        {
            using (var connection = DbConnection)
            {
                var entities = connection.Query<Post>(
                    sql: "usp_Post_GetListByUserId",
                    commandType: CommandType.StoredProcedure,
                    param: new
                    {
                        UserId = userId,
                        Limit = limit,
                        Offset = offset
                    }).ToArray();

                return entities;
            }
        }

        public void Update(Post entity)
        {
            using (var connection = DbConnection)
            {
                connection.Execute(
                    sql: "usp_Post_Update",
                    commandType: CommandType.StoredProcedure,
                    param: new
                    {
                        entity.Title,
                        entity.Content
                    });
            }
        }

        public void Delete(int postId)
        {
            using (var connection = DbConnection)
            {
                connection.Execute(
                    sql: "usp_Post_Delete",
                    commandType: CommandType.StoredProcedure,
                    param: new
                    {
                        PostId = postId
                    });
            }
        }
    }
}
