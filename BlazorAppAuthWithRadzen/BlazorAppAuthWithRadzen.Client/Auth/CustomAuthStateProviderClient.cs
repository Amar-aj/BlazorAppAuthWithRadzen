﻿using Microsoft.AspNetCore.Authentication.Cookies;

namespace BlazorAppAuthWithRadzen.Client.Auth;

public class CustomAuthStateProviderClient : BaseAuthenticationStateProvider
{
    public CustomAuthStateProviderClient(IHttpClientFactory httpClientFactory)
        : base(httpClientFactory)
    {
    }

    protected override string GetAuthenticationType()
    {
        //return "jwt"; // Client-side authentication type
        return CookieAuthenticationDefaults.AuthenticationScheme;
    }
}