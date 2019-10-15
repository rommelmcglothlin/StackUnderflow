using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Threading.Tasks;
using StackUnderflow.Models;
using StackUnderflow.Services;
using Microsoft.AspNetCore.Mvc;
using StackUnderflow.Data;

namespace StackUnderflow.Controllers
{
  [Authorize]
  [Route("api/[controller]")]
  [ApiController]
  public class QuestionsController : ControllerBase
  {
    private readonly QuestionsService _qs;
    private readonly ResponsesRepository _rs;

    [HttpGet]
    public ActionResult<IEnumerable<Question>> Get()
    {
      return _qs.GetAll().ToList();
    }

    // GET api/values/5
    [HttpGet("{id}")]
    public ActionResult<Question> Get(string id)
    {
      try
      {
        Question question = _qs.GetQuestionById(id);
        return Ok(question);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }


    [HttpPut("{id}")]
    public ActionResult Put(string id, [FromBody] Question questionData)
    {
      try
      {
        questionData.Id = id;
        var question = _qs.EditQuestion(questionData);
        questionData.AuthorId = HttpContext.User.FindFirst("Id").Value;
        return Ok(question);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }


    [HttpPost]
    public ActionResult<Question> Post([FromBody] Question questionData)
    {
      try
      {
        questionData.AuthorId = HttpContext.User.FindFirst("Id").Value;
        Question myQuestion = _qs.Create(questionData);
        return Created("api/question/" + myQuestion.Id, myQuestion);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("{id}/responses")]
    public ActionResult<IEnumerable<Response>> GetResponses(string id)
    {
      try
      {
        var allResponses = _rs.GetResponsesForQuestion(id);
        return Ok(allResponses);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }

    }


    [HttpDelete("{id}")]
    public ActionResult<Question> Delete(string id)
    {
      try
      {
        var question = _qs.DeleteQuestion(id);
        question.AuthorId = HttpContext.User.FindFirst("Id").Value;
        return Ok(question);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    public QuestionsController(QuestionsService qs, ResponsesRepository rs)
    {
      _qs = qs;
      _rs = rs;
    }
  }
}
