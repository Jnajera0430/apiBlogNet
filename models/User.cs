using System.Text.Json.Serialization;

namespace OpenApi.Models;
public class User
{
  public Guid id
  {
    get; set;
  }

  public string username { get; set; }
  public string name { get; set; }

  [JsonIgnore]
  public string password { get; set; }
  public virtual ICollection<Blog> blogs { get; set; } = new List<Blog>();
}