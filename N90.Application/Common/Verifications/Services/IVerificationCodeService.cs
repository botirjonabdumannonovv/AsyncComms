using N90.Domain.Enums;

namespace N90.Application.Common.Verifications.Services;

public interface IVerificationCodeService
{
    ValueTask<VerificationType?> GetVerificationTypeAsync(string code, CancellationToken cancellationToken = default);
}