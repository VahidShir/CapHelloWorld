using DotNetCore.CAP;
using System.Text.Json;

namespace CapHelloWorldConsumer.Events;

public class UserAddedEventSubscriber : ICapSubscribe
{
    [CapSubscribe("UserAdded")]
    public void Consumer(JsonElement customerData)
    {
        Console.WriteLine(customerData);
    }
}