using CapHelloWorldPublisher.Data;
using CapHelloWorldPublisher.Dtos;
using DotNetCore.CAP;
using System.Text.Json;

namespace CapHelloWorldPublisher;

public class UserService : IUserService
{
    private readonly UserDbContext _dbContext;
    private readonly ICapPublisher _capPublisher;

    public UserService(UserDbContext dbContext, ICapPublisher capPublisher)
    {
        _dbContext = dbContext;
        _capPublisher = capPublisher;
    }
    public async Task CreateAsync(UserDto user)
    {
        await _dbContext.Users.AddAsync(new User { FirstName = user.FirstName, LastName = user.LastName});
        await _dbContext.SaveChangesAsync();
        var content = JsonSerializer.Serialize(user);
        await _capPublisher.PublishAsync<string>("UserAdded", content);
    }
}