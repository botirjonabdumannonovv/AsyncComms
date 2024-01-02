using N90.Domain.Entities;

namespace N90.Application.Common.Identity.Services;

public interface IAccountAggregatorService
{
    ValueTask<bool> CreateUserAsync(User user, CancellationToken cancellationToken = default);
}