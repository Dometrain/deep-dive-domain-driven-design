using ErrorOr;

using MediatR;

using UserManagement.Application.Authentication.Common;

namespace UserManagement.Api.UseCases.Authentication.Register;

public record LoginQuery(
    string Email,
    string Password) : IRequest<ErrorOr<AuthenticationResult>>;