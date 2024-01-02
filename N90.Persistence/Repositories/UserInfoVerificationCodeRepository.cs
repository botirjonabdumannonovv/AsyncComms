using System.Linq.Expressions;
using N90.Domain.Entities;
using N90.Persistence.Caching.Brokers;
using N90.Persistence.DataContexts;
using N90.Persistence.Repositories.Interfaces;

namespace N90.Persistence.Repositories;

public class UserInfoVerificationCodeRepository(IdentityDbContext identityDbContext, ICacheBroker cacheBroker)
    : EntityRepositoryBase<UserInfoVerificationCode, IdentityDbContext>(identityDbContext, cacheBroker), IUserInfoVerificationCodeRepository
{
    public new IQueryable<UserInfoVerificationCode> Get(
        Expression<Func<UserInfoVerificationCode, bool>>? predicate = default,
        bool asNoTracking = false
    )
    {
        return base.Get(predicate, asNoTracking);
    }

    public new ValueTask<UserInfoVerificationCode?> GetByIdAsync(
        Guid codeId,
        bool asNoTracking = false,
        CancellationToken cancellationToken = default
    )
    {
        return base.GetByIdAsync(codeId, asNoTracking, cancellationToken);
    }

    public new async ValueTask<UserInfoVerificationCode> CreateAsync(
        UserInfoVerificationCode verificationCode,
        bool saveChanges = true,
        CancellationToken cancellationToken = default
    )
    {
       
        return await base.CreateAsync(verificationCode, saveChanges, cancellationToken);
    }

    public async ValueTask DeactivateAsync(Guid codeId, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        
        if (saveChanges)
            await DbContext.SaveChangesAsync(cancellationToken);
    }
}