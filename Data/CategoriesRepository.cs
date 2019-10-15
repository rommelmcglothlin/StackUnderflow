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

    public Category GetCategoryById(string id)
    {
      return _db.QueryFirstOrDefault<Category>(
          "SELECT * FROM categories WHERE id = @id",
          new { id });
    }

    internal bool UpdateCategory(Category categoryData)
    {
      var update = _db.Execute(@"
                    UPDATE categories SET
                    name = @Name
                    WHERE id = @Id;"
                  , categoryData);
      return update == 1;
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


    public bool SelectCategory(string id)
    {
      var sql = @"
          SELECT 
          c.id  
          FROM categories c
          JOIN questionscategory qc ON q.id = qc.questionid
          JOIN questions q ON qc.categoryid = c.id
          WHERE c.id = @id";
      var success = _db.Execute(sql, new { id });
      return success == 1;

    }


    public CategoriesRepository(IDbConnection db)
    {
      _db = db;
    }

  }
}
