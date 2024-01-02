using N90.Domain.Entities;

namespace N90.Application.Common.Identity.Services;

public interface IPasswordGeneratorService
{
    string GeneratePassword();

    string GetValidatedPassword(string password, User user);
}