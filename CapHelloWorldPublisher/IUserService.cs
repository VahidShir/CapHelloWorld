using CapHelloWorldPublisher.Dtos;

namespace CapHelloWorldPublisher;

public interface IUserService
{
    Task CreateAsync(UserDto user);
}