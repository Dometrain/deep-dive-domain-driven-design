using ErrorOr;

using MediatR;

using UserManagement.Application.Authentication.Common;

namespace UserManagement.Application.Authentication.Login;

public record RegisterCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password) : IRequest<ErrorOr<AuthenticationResult>>;