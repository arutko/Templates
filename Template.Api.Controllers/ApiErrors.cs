using Template.Common;

namespace Template.Api.Controllers;

public static class ApiErrors
{
    public static readonly Error WrongUserCredentials =
        new Error("Login", "User with given auth data address not found");
    public static readonly Error NoActiveAccount =
        new Error("Login", "Account with given UserId doesn't exist");
}