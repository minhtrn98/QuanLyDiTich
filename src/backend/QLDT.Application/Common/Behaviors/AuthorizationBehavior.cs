using QLDT.Application.Common.Request;
using QLDT.Application.Common.Services;

namespace QLDT.Application.Common.Behaviors;

public sealed class AuthorizationBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IAuthorizeableRequest<TResponse>
        where TResponse : IResultBase
{
    private readonly IAuthorizationService _authorizationService;

    public AuthorizationBehavior(IAuthorizationService authorizationService)
    {
        _authorizationService = authorizationService;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        string requestName = typeof(TRequest).Name;
        // using request name to get permission & role required for request

        //List<AuthorizeAttribute> authorizationAttributes = request.GetType()
        //    .GetCustomAttributes<AuthorizeAttribute>()
        //    .ToList();

        //if (authorizationAttributes.Count == 0)
        //{
        //    return await next();
        //}

        IEnumerable<string> requiredPermissions = [];

        IEnumerable<string> requiredRoles = [];

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
