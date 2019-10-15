using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StackUnderflow.Models;
using StackUnderflow.Services;

namespace StackUnderflow.Controllers
{
  [Authorize]
  [Route("api/[controller]")]
  [ApiController]
  public class ResponsesController : ControllerBase
  {
    private readonly ResponsesService _rs;


    [HttpPut("{id}")]
    public ActionResult Put(string id, [FromBody] Response responseData)
    {
      try
      {
        responseData.AuthorId = HttpContext.User.FindFirst("Id").Value;
        responseData.Id = id;
        var response = _rs.EditResponse(responseData);
        return Ok(response);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }


    [HttpPost]
    public ActionResult<Response> Post([FromBody] Response responseData)
    {
      try
      {
        responseData.AuthorId = HttpContext.User.FindFirst("Id").Value;
        Response myResponse = _rs.Create(responseData);
        return Created("api/response/" + myResponse.Id, myResponse);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }


    public ResponsesController(ResponsesService rs)
    {
      _rs = rs;
    }

  }
}