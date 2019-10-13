using System;
using StackUnderflow.Interfaces;

namespace StackUnderflow.Models
{
  public class Response : IResponse
  {
    public string Id { get; set; }
    public string Body { get; set; }
    public string QuestionId { get; set; }
    public string AuthorId { get; set; }
    public bool Answered { get; set; }
    public DateTime Responded { get; set; }
    public DateTime Updated { get; set; }
  }
}