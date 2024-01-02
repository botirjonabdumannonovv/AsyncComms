using Microsoft.Extensions.Options;
using N90.Application.Common.EventBus.Brokers;
using N90.Infrastructure.Common.Settings;
using RabbitMQ.Client;

namespace N90.Infrastructure.Common.EventBus.Brokers;

public class RabbitMqConnectionProvider : IRabbitMqConnectionProvider
{
    private readonly IConnectionFactory _connectionFactory;
    private IConnection? _connection;

    public RabbitMqConnectionProvider(IOptions<RabbitMqConnectionSettings> rabbitMqConnectionSettings)
    {
        _connectionFactory = new ConnectionFactory
        {
            HostName = rabbitMqConnectionSettings.Value.HostName,
            Port = rabbitMqConnectionSettings.Value.Port
        };
    }

    public async ValueTask<IChannel> CreateChannelAsync(CancellationToken cancellationToken = default)
    {
        _connection ??= await _connectionFactory.CreateConnectionAsync(cancellationToken);

        return await _connection.CreateChannelAsync();
    }
}   
