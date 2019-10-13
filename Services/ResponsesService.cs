using System;
using StackUnderflow.Data;
using StackUnderflow.Models;

namespace StackUnderflow.Services
{
  public class ResponsesService
  {
    private readonly ResponsesRepository _repo;

    public Response Create(Response responseData)
    {
      responseData.Id = Guid.NewGuid().ToString();
      _repo.Create(responseData);
      return responseData;
    }

    public Response GetResponseById(string id)
    {
      var response = _repo.GetResponseById(id);
      if (response == null)
      {
        throw new Exception("Not a valid ID. Try again.");
      }
      return response;
    }

    public Response EditResponse(Response responseData)
    {
      var response = GetResponseById(responseData.Id);
      response.Body = responseData.Body;
      response.Updated = DateTime.Now;
      bool success = _repo.UpdateResponse(response);
      if (!success)
      {
        throw new Exception("Unable to edit this response");
      }
      return response;
    }

    public Response GetResponseForQuestion(Response responseData)
    {

      return null;
    }


    public ResponsesService(ResponsesRepository repo)
    {
      _repo = repo;
    }
  }

}