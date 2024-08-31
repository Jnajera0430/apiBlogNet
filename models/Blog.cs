using System.Text.Json.Serialization;
namespace OpenApi.Models;
public class Blog
{
  [JsonIgnore]
  public Guid id
  {
    get; set;
  }

  public string title
  {
    get; set;
  }
  public string auhtor
  {
    get; set;
  }
  public string url
  {
    get; set;
  }

  public int likes
  {
    get; set;
  }

  [JsonIgnore]
  public Guid userId { get; set; }
  public User user { get; set; } = new User();
}