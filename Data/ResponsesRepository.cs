using System.Collections.Generic;
using System.Data;
using Dapper;
using StackUnderflow.Models;

namespace StackUnderflow.Data
{
  public class ResponsesRepository
  {
    private readonly IDbConnection _db;

    public Response Create(Response responseData)
    {
      var sql = @"INSERT INTO responses
                    (id, body, questionid, authorid)
                    VALUES
                    (@Id, @Body, @QuestionId, @AuthorId);";
      var x = _db.Execute(sql, responseData);
      return responseData;
    }

    public Response GetResponseById(string id)
    {
      return _db.QueryFirstOrDefault<Response>(
          "SELECT * FROM responses WHERE id = @id",
          new { id });
    }

    internal bool UpdateResponse(Response responseData)
    {
      var update = _db.Execute(@"
                UPDATE responses SET
                body = @Body, 
                updated = @Updated
                WHERE id = @Id;"
                , responseData);
      return update == 1;
    }

    public IEnumerable<Response> GetResponsesForQuestion(string id)
    {
      return _db.Query<Response>(@"
                SELECT * FROM responses
                where questionid = @id;",
                new { id });
    }

    public ResponsesRepository(IDbConnection db)
    {
      _db = db;

    }
  }
}