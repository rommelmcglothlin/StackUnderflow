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
      var exists = _repo.GetCategoryByName(categoryData.Name);
      if (exists != null)
      {
        throw new Exception("This category alredy exists.");
      }
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
      category.Name = categoryData.Name;
      if (category == null)
      {
        throw new Exception("You need an ID if you wish to edit the category name.");
      }
      bool success = _repo.UpdateCategory(category);
      if (!success)
      {
        throw new Exception("Unable to change the name of this category at the moment.");
      }
      return category;
    }


    // bool linkedCategory = _repo.SelectCategory(categoryData.Id);
    // if (linkedCategory == true)
    // {
    //   throw new Exception("You can't edit a category that has been added to a question.");
    // }

    public Category DeleteCategory(string id)
    {
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