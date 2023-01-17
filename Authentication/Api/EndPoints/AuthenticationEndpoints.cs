using Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.EndPoints
{
  public static class AuthenticationEndPoints
  {
    public static void MapAuthenticationEndPoints(this WebApplication app)
    {
      var authentication = app.MapGroup("/authentication");

      authentication.MapPost("/logon", EmailLogOn);
      authentication.MapGet("/logout", LogOut);

    }

    static IResult EmailLogOn([FromBody] EmailLogin emailLoginDto, IUserService userService)
    {
      var user = userService.Get(emailLoginDto.Email);

      if (user == null)
      {
        return TypedResults.BadRequest("User Not Found");
      }

      if(!emailLoginDto.Password.ToLower().Equals(user.Password.ToLower())) 
      { 
        return TypedResults.BadRequest("Invalid Password");
      }
       
      //Sign In
      

      return TypedResults.Ok("Authenticated");
    }

    static IResult LogOut()
    {
      
      return TypedResults.Ok();
    }


    public class EmailLogin
    {
      public string Email { get; set; }
      public string Password { get; set; }
    }
  }
}
