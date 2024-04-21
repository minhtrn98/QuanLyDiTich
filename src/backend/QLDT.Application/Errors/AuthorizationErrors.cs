namespace QLDT.Application.Errors;

public static class AuthorizationErrors
{
    public static Error MissingRole => Error.Unauthorized("MissingRole", "User is missing required roles for taking this action.");
    public static Error MissingPermission => Error.Unauthorized("MissingPermission", "User is missing required permissions for taking this action.");
}
