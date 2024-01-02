using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using N90.Domain.Entities;
using N90.Persistence.Caching.Brokers;
using N90.Persistence.DataContexts;
using N90.Persistence.Repositories.Interfaces;

namespace N90.Persistence.Repositories;

public class EmailHistoryRepository(NotificationDbContext dbContext, ICacheBroker cacheBroker)
    : EntityRepositoryBase<EmailHistory, NotificationDbContext>(dbContext, cacheBroker), IEmailHistoryRepository
{
    public new IQueryable<EmailHistory> Get(Expression<Func<EmailHistory, bool>>? predicate = default, bool asNoTracking = false)
    {
        return base.Get(predicate, asNoTracking);
    }

    public new async ValueTask<EmailHistory> CreateAsync(
        EmailHistory emailHistory,
        bool saveChanges = true,
        CancellationToken cancellationToken = default
    )
    {
        if (emailHistory.EmailTemplate is not null)
            DbContext.Entry(emailHistory.EmailTemplate).State = EntityState.Unchanged;

        var createdHistory = await base.CreateAsync(emailHistory, saveChanges, cancellationToken);

        if (emailHistory.EmailTemplate is not null)
            DbContext.Entry(emailHistory.EmailTemplate).State = EntityState.Detached;

        return createdHistory;
    }
}