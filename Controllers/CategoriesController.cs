using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StackUnderflow.Models;
using StackUnderflow.Services;

namespace StackUnderflow.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class CategoriesController : ControllerBase
  {
    private readonly CategoriesService _cs;

    [HttpPost]
    public ActionResult<Category> Post([FromBody]  Category categoryData)
    {
      try
      {
        Category newCategory = _cs.AddCategory(categoryData);
        return Created("api/category" + newCategory.Id, newCategory);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpPut("{id}")]
    public ActionResult Put(string id, [FromBody] Category categoryData)
    {
      try
      {
        categoryData.Id = id;
        var category = _cs.EditCategory(categoryData);
        return Ok(category);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpDelete("{id}")] //NOTE  not sure if this is needed 
    public ActionResult<Category> Delete(string id)
    {
      try
      {
        var category = _cs.DeleteCategory(id);
        return Ok(category);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    public CategoriesController(CategoriesService cs)
    {
      _cs = cs;
    }
  }
}