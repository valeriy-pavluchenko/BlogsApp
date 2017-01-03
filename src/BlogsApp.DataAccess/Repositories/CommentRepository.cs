using BlogsApp.DataAccess.Entities;
using BlogsApp.DataAccess.Exceptions;

using Dapper;

using System.Data;
using System.Linq;

namespace BlogsApp.DataAccess.Repositories
{
    public class CommentRepository : BaseRepository
    {
        public CommentRepository(string connectionString) : base(connectionString)
        {
        }

        public int Insert(Comment entity)
        {
            using (var connection = DbConnection)
            {
                var entityId = connection.Query<int>(
                    sql: "usp_Comment_Insert",
                    commandType: CommandType.StoredProcedure,
                    param: new
                    {
                        entity.UserId,
                        entity.PostId,
                        entity.Message,
                        entity.AddedOnUtc
                    }).Single();

                return entityId;
            }
        }

        public Comment GetById(int commentId)
        {
            using (var connection = DbConnection)
            {
                var entity = connection.Query<Comment>(
                    sql: "usp_Comment_GetById",
                    commandType: CommandType.StoredProcedure,
                    param: new
                    {
                        CommentId = commentId
                    }).SingleOrDefault();

                if (entity == null)
                    throw new DbNotFoundException("Can't find the comment.");

                return entity;
            }
        }

        public Comment[] GetListByUserId(int userId, int limit, int offset)
        {
            using (var connection = DbConnection)
            {
                var entities = connection.Query<Comment>(
                    sql: "usp_Comment_GetListByUserId",
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

        public Comment[] GetListByPostId(int postId, int limit, int offset)
        {
            using (var connection = DbConnection)
            {
                var entities = connection.Query<Comment>(
                    sql: "usp_Comment_GetListByPostId",
                    commandType: CommandType.StoredProcedure,
                    param: new
                    {
                        PostId = postId,
                        Limit = limit,
                        Offset = offset
                    }).ToArray();

                return entities;
            }
        }

        public void Update(Comment entity)
        {
            using (var connection = DbConnection)
            {
                connection.Execute(
                    sql: "usp_Comment_Update",
                    commandType: CommandType.StoredProcedure,
                    param: new
                    {
                        entity.CommentId,
                        entity.Message
                    });
            }
        }

        public void Delete(int commentId)
        {
            using (var connection = DbConnection)
            {
                connection.Execute(
                    sql: "usp_Comment_Delete",
                    commandType: CommandType.StoredProcedure,
                    param: new
                    {
                        CommentId = commentId
                    });
            }
        }
    }
}
