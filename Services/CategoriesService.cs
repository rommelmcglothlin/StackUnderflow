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
      var category = GetCategoryById(categoryData.Id);
      if (category == null)
      {
        throw new Exception("You need an ID if you wish to edit the category name.");
      }
      category.Name = categoryData.Name;
      bool success = _repo.UpdateCategory(category);
      if (!success)
      {
        throw new Exception("Unable to change the name of this category at the moment.");
      }
      return category;
    }



    public CategoriesService(CategoriesRepository repo)
    {
      _repo = repo;
    }

  }
}