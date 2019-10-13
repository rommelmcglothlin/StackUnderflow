using System.Collections.Generic;
using System.Data;
using Dapper;
using StackUnderflow.Models;

namespace StackUnderflow.Data
{
  public class QuestionsRepository
  {
    private readonly IDbConnection _db;

    public Question Create(Question questionData)
    {
      var sql = @"INSERT INTO questions
                (id, title, body, questioncreated)
                VALUES
                (@Id, @Title, @Body, @QuestionCreated);";
      var x = _db.Execute(sql, questionData);
      return questionData;
    }

    public IEnumerable<Question> GetAllQuestions()
    {
      return _db.Query<Question>("SELECT * FROM questions");
    }

    public Question GetQuestionsById(string id)
    {
      var question = _db.QueryFirstOrDefault<Question>(
          "SELECT * FROM question WHERE id = @id",
          new { id });
      return question;

    }

    internal bool UpdateQuestion(Question question)
    {
      var update = _db.Execute(@"
                UPDATE questions SET
                title = @Title,
                body = @body,
                answered = @QuestionAnswered,
                lastmodified = @LastModified
                WHERE id = @Id
                ", question);
      return update == 1;
    }

    internal bool DeleteQuestion(string id)
    {
      var success = _db.Execute(@"
    DELETE FROM questions WHERE id = @id
      ", new { id });
      return success == 1;
    }




    public QuestionsRepository(IDbConnection db)
    {
      _db = db;
    }

  }
}