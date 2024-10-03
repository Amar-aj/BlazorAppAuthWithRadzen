using BlazorAppAuthWithRadzen.Client.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BlazorAppAuthWithRadzen.Controllers;

[Route("api/[controller]/[action]")]
public class AccountController : Controller
{
    [HttpPost]
    public async Task<IActionResult> Login(LoginRequest tokenRequest)
    {
        var user = TempMaster.GetUsers().Where(x => x.user_name == tokenRequest.UserName).FirstOrDefault();
        if (user != null && user.password == tokenRequest.Password)
        {
            var claims = new List<Claim> { new Claim(ClaimTypes.Name, user.user_name) };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity));
            var cookieOptions = new CookieOptions { Expires = DateTimeOffset.UtcNow.AddDays(1), Secure = true };
            Response.Cookies.Append("APITest", "Test value", cookieOptions);

            //server-side redirect (Redirect method) inherently causes a full page load.
            return Redirect("/");
        }

        return Unauthorized();
    }
    [HttpGet]
    public IActionResult GetUser()
    {
        var user = new User
        (
            user_name: User.Identity?.Name
        //IsAuthenticated = User.Identity?.IsAuthenticated ?? false,
        //Roles = User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value)
        );

        return Ok(user);
    }

    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return Redirect("/");
    }
}
public static class TempMaster
{
    public static List<User> GetUsers()
    {
        return new List<User> { new User(user_name: "aa", password: "as"), new User(user_name: "bb", password: "bs"), };
    }
}
