using N90.Domain.Entities;
using N90.Domain.Enums;

namespace N90.Application.Common.Verifications.Services;

public interface IUserInfoVerificationCodeService : IVerificationCodeService
{
    IList<string> Generate();

    ValueTask<(UserInfoVerificationCode Code, bool IsValid)> GetByCodeAsync(string code, CancellationToken cancellationToken = default);

    ValueTask<UserInfoVerificationCode> CreateAsync(VerificationCodeType codeType, Guid userId, CancellationToken cancellationToken = default);

    ValueTask DeactivateAsync(Guid codeId, bool saveChanges = true, CancellationToken cancellationToken = default);
}