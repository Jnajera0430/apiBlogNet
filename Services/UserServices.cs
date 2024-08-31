using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Identity;
using OpenApi.Models;

namespace OpenApi.Services;
public class UserServices : IUserServices
{
  private readonly AppContext _appContext;
  private readonly ILogger<UserServices> _logger;
  private readonly PasswordHasher<User> _passwordHasher;
  public UserServices(ILogger<UserServices> logger, AppContext appContext)
  {
    _appContext = appContext;
    _logger = logger;
    _passwordHasher = new PasswordHasher<User>();
  }
  public IEnumerable<User> Get()
  {
    return _appContext.users;
  }

  public async Task Save(CreateUserDto user)
  {


    var userToCreate = new User
    {
      id = Guid.NewGuid(),
      name = user.name,
      username = user.username,
      password = user.password,
    };
    string passwordHashed = _passwordHasher.HashPassword(userToCreate, user.password);
    userToCreate.password = passwordHashed;
    _logger.LogDebug("este es el username: {username} y esta es la pass: {pass}", userToCreate.username, userToCreate.password);
    _appContext.Add(userToCreate);
    await _appContext.SaveChangesAsync();
  }

  public async Task Update(string id, User user)
  {
    var userToEdit = await _appContext.users.FindAsync(id);
    if (userToEdit != null)
    {
      userToEdit.username = user.username;
      userToEdit.name = user.name;
      userToEdit.password = user.password;

      await _appContext.SaveChangesAsync();
    }
  }

  public async Task Delete(string id)
  {
    var userToDelete = await _appContext.users.FindAsync(id);
    if (userToDelete != null)
    {
      _appContext.Remove(userToDelete);

      await _appContext.SaveChangesAsync();
    }
  }

}

public interface IUserServices
{
  public IEnumerable<User> Get();
  public Task Save(CreateUserDto user);
  public Task Update(string id, User user);
  public Task Delete(string id);

}