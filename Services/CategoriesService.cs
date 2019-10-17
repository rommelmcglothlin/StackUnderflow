using System;
using StackUnderflow.Data;
using StackUnderflow.Models;

namespace StackUnderflow.Services
{
  public class CategoriesService
  {
    private readonly CategoriesRepository _repo;

    public Category AddCategory(Category categoryData)
    {
      // var exists = _repo.GetCategoryByName(categoryData.Name); NOTE ADDED As per business logic, however it does not allow to test further if it has any duplicates.
      // if (exists != null)
      // {
      //   throw new Exception("This category alredy exists.");
      // }
      categoryData.Id = Guid.NewGuid().ToString();
      _repo.Create(categoryData);
      return categoryData;
    }

    public Category GetCategoryById(string id)
    {
      var category = _repo.GetCategoryById(id);
      if (category == null)
      {
        throw new Exception("Not a valid ID. Try again.");
      }
      return category;
    }

    public Category EditCategory(Category categoryData)
    {
      var categoryQuestions = _repo.GetCategoryQuestion(categoryData.Id);
      if (categoryQuestions != null)
      {
        throw new Exception("You can't edit categories that have been added to questions");
      }
      var category = _repo.GetCategoryById(categoryData.Id);
      category.Name = categoryData.Name;
      var update = _repo.UpdateCategory(category);
      if (!update)
      {
        throw new Exception("Unable to change the name of this category at the moment.");
      }
      return category;
    }

    public Category DeleteCategory(string id)
    {
      var categoryQuestion = _repo.GetCategoryQuestion(id);
      if (categoryQuestion != null)
      {
        throw new Exception("Unable to delete categories that have been added to queestions.");
      }
      var category = GetCategoryById(id);
      var deleted = _repo.DeleteCategory(id);
      if (!deleted)
      {
        throw new Exception($"Unable to delete category ID: {id}");
      }
      return category;
    }

    public CategoriesService(CategoriesRepository repo)
    {
      _repo = repo;
    }

  }
}