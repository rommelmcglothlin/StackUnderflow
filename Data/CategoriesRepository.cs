using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using StackUnderflow.Models;

namespace StackUnderflow.Data
{
  public class CategoriesRepository
  {
    private readonly IDbConnection _db;

    public Category Create(Category categoryData)
    {
      var sql = @"INSERT INTO categories
                    (id, name)
                    VALUES
                    (@Id, @Name);";
      var x = _db.Execute(sql, categoryData);
      return categoryData;
    }

    public Category GetCategoryByName(string name)
    {
      return _db.QueryFirstOrDefault<Category>(
        "SELECT * FROM categories WHERE name = @name",
        new { name });
    }

    public Category GetCategoryById(string id)
    {
      return _db.QueryFirstOrDefault<Category>(
          "SELECT * FROM categories WHERE id = @id",
          new { id });
    }

    public LinkorUnlinkCategory GetCategoryQuestion(string id)
    {
      var sql = @"SELECT * FROM categoryquestions WHERE categoryid = @id;";

      return _db.QueryFirstOrDefault<LinkorUnlinkCategory>(sql, new { id });
    }


    public bool UpdateCategory(Category categoryData)
    {
      var sql = @"UPDATE categories SET
                name = @Name
                WHERE id = @Id;";
      var success = _db.Execute(sql, categoryData);
      return success == 1;
    }

    public IEnumerable<Category> CategoryForQuestion(string id)
    {
      return _db.Query<Category>(@"
                SELECT * FROM categories
                WHERE questionid = @id;",
                new { id });
    }

    internal bool LinkCategory(string categoryId, string questionId)
    {
      var id = Guid.NewGuid().ToString();
      var sql = @"INSERT INTO categoryquestions
                (id, categoryid, questionid)
                VALUES
                (@id, @categoryId, @questionId);";
      var success = _db.Execute(sql, new { id, categoryId, questionId });
      return success == 1;
    }

    internal bool UnlinkCategory(string categoryId, string questionId)
    {
      var sql = @"DELETE FROM categoryquestions
                  WHERE categoryid = @categoryId AND questionid = @questionId";
      var success = _db.Execute(sql, new { categoryId, questionId });
      return success == 1;
    }

    internal bool DeleteCategory(string id)
    {
      var success = _db.Execute(@"
      DELETE FROM categories WHERE id = @id",
      new { id });
      return success == 1;
    }
    internal bool CategoryQuestionDel(string id)
    {
      var success = _db.Execute(@"
      DELETE FROM categoryquestions WHERE categoryid = @Id",
      new { id });
      return success == 1;
    }

    public CategoriesRepository(IDbConnection db)
    {
      _db = db;
    }

  }
}
