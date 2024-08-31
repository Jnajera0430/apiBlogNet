using Microsoft.EntityFrameworkCore;
using OpenApi.Models;

namespace OpenApi.Services;
public interface IBlogService
{
  public IEnumerable<Blog> Get();
  public Task Save(CreateBlogDto blog);

  public Task Update(string id, Blog blog);

  public Task Delete(Guid id);
}

public class BlogServices : IBlogService
{
  private readonly AppContext _appContext;
  private readonly ILogger<BlogServices> _logger;

  public BlogServices(AppContext appContext, ILogger<BlogServices> logger)
  {
    _appContext = appContext;
    _logger = logger;
  }
  public IEnumerable<Blog> Get()
  {
    return [.. _appContext.blogs.Include(b => b.user)];
  }

  public async Task Save(CreateBlogDto blog)
  {
    var user = _appContext.users.Find(blog.userId);

    if (user != null)
    {
      var blogToCreate = new Blog
      {
        id = Guid.NewGuid(),
        title = blog.title,
        auhtor = blog.auhtor,
        url = blog.url,
        likes = blog.likes,
        user = user
      };

      _appContext.blogs.Add(blogToCreate);
      await _appContext.SaveChangesAsync();
    }
    else
    {
      _logger.LogWarning("Usuario no encontrado para userId: " + blog.userId);
    }

  }

  public async Task Update(string id, Blog blog)
  {
    var blogtoEdit = await _appContext.blogs.FindAsync(id);
    if (blogtoEdit != null)
    {
      blogtoEdit.title = blog.title;
      blogtoEdit.auhtor = blog.auhtor;
      blogtoEdit.url = blog.url;
      blogtoEdit.likes = blog.likes;

      await _appContext.SaveChangesAsync();
    }

  }

  public async Task Delete(Guid id)
  {
    var blogtoDelete = await _appContext.blogs.FindAsync(id);
    if (blogtoDelete != null)
    {
      _appContext.Remove(blogtoDelete);
      await _appContext.SaveChangesAsync();
    }

  }
}