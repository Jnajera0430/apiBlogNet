
public class CreateBlogDto
{
  public string title { get; set; }
  public string auhtor { get; set; }
  public string url { get; set; }
  public int likes { get; set; } = 0;
  public Guid userId { get; set; }
}