using System;
using System.Collections.Generic;
using System.Linq;
using StackUnderflow.Data;
using StackUnderflow.Models;

namespace StackUnderflow.Services
{
  public class QuestionsService
  {
    private readonly QuestionsRepository _repo;
    private readonly ResponsesRepository _rr;

    public List<Question> GetAll()
    {
      return _repo.GetAllQuestions().ToList();
    }

    public Question Answered(Question questionData) //NOTE may not need this
    {
      if (questionData.QuestionAnswered == true)
      {
        throw new Exception("You can't edit an answered question");
      }
      return questionData;

    }

    public Question Create(Question questionData)
    {
      questionData.Id = Guid.NewGuid().ToString();
      _repo.Create(questionData);
      return questionData;
    }

    public Question GetQuestionById(string id)
    {
      var question = _repo.GetQuestionsById(id);
      if (question == null)
      {
        throw new Exception("Not a valid ID. Try again.");
      }
      return question;

    }

    public Question EditQuestion(Question questionData)
    {
      var question = GetQuestionById(questionData.Id);
      if (question == null)
      {
        throw new Exception("You need an ID if you wish to edit a question");
      }
      var check = Answered(question);
      question.Title = questionData.Title;
      question.Body = questionData.Body;
      question.LastModified = DateTime.Now;
      bool success = _repo.UpdateQuestion(question);
      if (!success)
      {
        throw new Exception("Question couldn't be edited. Please try again.");
      }
      return question;
    }

    public Question DeleteQuestion(string id)
    {
      var question = GetQuestionById(id);
      var deleted = _repo.DeleteQuestion(id);
      if (!deleted)
      {
        throw new Exception($"Unable to remove Question ID: {id}");
      }
      return question;
    }

    public List<Response> GetResponses(string id)
    {
      Question question = _repo.GetQuestionsById(id);
      if (question == null)
      {
        throw new Exception("Not a valid ID");
      }
      return _rr.GetResponsesForQuestion(question.Id).ToList();
    }

    public QuestionsService(QuestionsRepository repo, ResponsesRepository rr)
    {
      _repo = repo;
      _rr = rr;
    }

  }
}