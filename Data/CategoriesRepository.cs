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
                    UPDATE responses SET
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

    public CategoriesRepository(IDbConnection db)
    {
      _db = db;
    }

  }
}