using Api.Services;

namespace Api.EndPoints
{
  public static class UsersEndpoints
  {
    public static void MapUsersEndpoints(this WebApplication app)
    {
      var authentication = app.MapGroup("/user");

      authentication.MapPost("/current", CurrentUser);
    }

    static IResult CurrentUser( IUserService userService)
    {
      long? maybeUserId = null;

      if (maybeUserId.HasValue)
      {
        var user = userService.Get(maybeUserId.Value);

        if (user == null) return TypedResults.NotFound();

        return TypedResults.Ok(user);
      }

      return TypedResults.Unauthorized();
    }
  }
}
