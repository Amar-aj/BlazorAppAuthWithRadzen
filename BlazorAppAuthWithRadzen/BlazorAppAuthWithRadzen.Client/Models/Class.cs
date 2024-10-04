namespace BlazorAppAuthWithRadzen.Client.Models;

public class LoginRequest
{
    public string UserName { get; set; }
    public string Password { get; set; }
}
public class User(string user_name, string? password = null)
{
    public string user_name { get; set; }
    public string? password { get; set; }
}

public class UserInfo
{
    public required string user_id { get; set; }
    public required string user_name { get; set; }
    public required string email { get; set; }

}