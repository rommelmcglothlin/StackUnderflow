using System;
using System.Collections.Generic;
using StackUnderflow.Models;

namespace StackUnderflow.Interfaces
{
  public interface IQuestion
  {
    string Id { get; set; }
    string Title { get; set; }
    string Body { get; set; }
    string AuthorId { get; set; }
    string AnswerId { get; set; }
    DateTime QuestionCreated { get; set; }
    DateTime LastModified { get; set; }

    List<Category> Tags { get; set; }
  }
}