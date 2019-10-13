using StackUnderflow.Interfaces;

namespace StackUnderflow.Models
{
  public class Category : ICategory
  {
    public string Id { get; set; }
    public string Name { get; set; }
  }
}