using BE.Domain;

namespace BE.DAL.Services;

public interface ITokenService
{
    string GenerateToken(User user);

    bool ValidateToken(string token);

    RefreshToken GenerateRefreshToken();
}