namespace UserManagement.Infrastructure.IntegrationEvents.Settings;

public class MessageBrokerSettings
{
    public const string Section = "MessageBroker";

    public string HostName { get; init; } = null!;
    public int Port { get; init; }
    public string UserName { get; init; } = null!;
    public string Password { get; init; } = null!;
    public string QueueName { get; init; } = null!;
    public string ExchangeName { get; init; } = null!;
}