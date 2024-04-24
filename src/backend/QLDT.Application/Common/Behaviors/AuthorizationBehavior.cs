using QLDT.Application.Common.Request;
using QLDT.Application.Common.Services;
using QLDT.Domain.UnitOfWork;

namespace QLDT.Application.Common.Behaviors;

public sealed class AuthorizationBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IAuthorizeableRequest<TResponse>
        where TResponse : IResultBase
{
    private readonly IAuthorizationService _authorizationService;
    private readonly IUnitOfWork _unitOfWork;

    public AuthorizationBehavior(
        IAuthorizationService authorizationService, IUnitOfWork unitOfWork)
    {
        _authorizationService = authorizationService;
        _unitOfWork = unitOfWork;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        string requestName = typeof(TRequest).FullName
            ?.Replace("+", "") ?? "";
        IEnumerable<string> requiredPermissions = await _unitOfWork.Functions
            .GetPermissionNames(requestName, cancellationToken);

        IEnumerable<string> requiredRoles = [];//TODO:

        Result<Success> authorizeError = _authorizationService.AuthorizeCurrentUser(
            requiredRoles,
            requiredPermissions);

        if (authorizeError.IsError)
        {
            return (dynamic)authorizeError.Errors;
        }
        return await next();
    }
}
