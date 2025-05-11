using BE.DAL.Reporsitory;
using BE.Domain;
using Microsoft.AspNetCore.Identity;
using StackExchange.Redis;

namespace BE.DAL.Services;

public class AuthenticateService
{
    private readonly IPostgresRepository<RefreshToken> _refreshTokenRepository;
    private readonly ITokenService _tokenService;
    private readonly UserManager<User> _userManager;
    private readonly StackExchange.Redis.IDatabase _redisDatabase;

    public AuthenticateService(
        IPostgresRepository<RefreshToken> refreshTokenRepository,
        ITokenService tokenService,
        UserManager<User> userManager,
        IConnectionMultiplexer redisConnection)
    {
        _refreshTokenRepository = refreshTokenRepository;
        _tokenService = tokenService;
        _userManager = userManager;
        _redisDatabase = redisConnection.GetDatabase();
    }

    /// <summary>
    /// Authenticates a user by verifying their username and password.
    /// Generates an access token and a refresh token, storing the refresh token using Redis stored
    /// </summary>
    /// <param name="username">The username of the user attempting to authenticate.</param>
    /// <param name="password">The password of the user attempting to authenticate.</param>
    /// <returns>A JWT access token if authentication is successful.</returns>
    /// <exception cref="Exception">Thrown if the credentials are invalid.</>
    public async Task<string> Authenticate(string username, string password)
    {
        var user = await _userManager.FindByNameAsync(username);
        if (user == null || !await _userManager.CheckPasswordAsync(user, password))
            throw new Exception("Invalid credentials");

        var token = _tokenService.GenerateToken(user);

        // Store token in Redis with an expiration time
        var refreshToken = _tokenService.GenerateRefreshToken();
        refreshToken.UserId = user.Id;
        refreshToken.ExpiryDate = DateTime.UtcNow.AddDays(7);

        await _redisDatabase.StringSetAsync(refreshToken.Token, user.Id.ToString(), TimeSpan.FromDays(7));

        // Save refresh token in the database
        await _refreshTokenRepository.AddAsync(refreshToken);

        return token;
    }

    /// <summary>
    /// Refreshes an expired or expiring access token using a valid refresh token.
    /// Generates a new access token and refresh token, updating the stored refresh token
    /// in both Redis and the database.
    /// </summary>
    /// <param name="token">The refresh token used to request a new access token.</param>
    /// <returns>A new JWT access token if the refresh token is valid.</returns>
    /// <exception cref="Exception">Thrown if the refresh token is invalid or expired.</exception>
    public async Task<string> RefreshToken(string token)
    {
        var userId = await _redisDatabase.StringGetAsync(token);
        if (string.IsNullOrEmpty(userId))
            throw new Exception("Invalid or expired refresh token");

        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
            throw new Exception("User not found");

        var newToken = _tokenService.GenerateToken(user);

        // Generate and store a new refresh token
        var newRefreshToken = _tokenService.GenerateRefreshToken();
        newRefreshToken.UserId = user.Id;
        newRefreshToken.ExpiryDate = DateTime.UtcNow.AddDays(7);

        await _redisDatabase.StringSetAsync(newRefreshToken.Token, user.Id.ToString(), TimeSpan.FromDays(7));
        await _refreshTokenRepository.AddAsync(newRefreshToken);

        return newToken;
    }
}
