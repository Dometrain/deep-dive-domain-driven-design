using UserManagement.Api.Models;

namespace UserManagement.Application.Authentication.Common;

public record AuthenticationResult(
    User User,
    string Token);