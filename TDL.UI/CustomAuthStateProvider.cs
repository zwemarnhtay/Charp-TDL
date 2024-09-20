using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Text.Json;

namespace TDL.UI;

public class CustomAuthStateProvider : AuthenticationStateProvider
{
  private readonly ILocalStorageService _localStorage;
  private readonly HttpClient _httpClient;

  public CustomAuthStateProvider(ILocalStorageService localStorage, HttpClient httpClient)
  {
    _localStorage = localStorage;
    _httpClient = httpClient;
  }

  public override async Task<AuthenticationState> GetAuthenticationStateAsync()
  {
    var token = await _localStorage.GetItemAsStringAsync("Token");

    if (string.IsNullOrEmpty(token))
    {
      return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
    }

    var identity = new ClaimsIdentity();
    _httpClient.DefaultRequestHeaders.Authorization = null;

    /*if (!string.IsNullOrEmpty(token))
    {*/
    identity = new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt");
    //_httpClient.DefaultRequestHeaders.Authorization =
    //    new AuthenticationHeaderValue("Bearer", token.Replace("\"", ""));
    /* }*/

    var user = new ClaimsPrincipal(identity);
    var state = new AuthenticationState(user);

    NotifyAuthenticationStateChanged(Task.FromResult(state));

    return state;
  }

  public async Task<String> GetTokenAsync()
  {
    var token = await _localStorage.GetItemAsStringAsync("Token");
    return token.Replace("\"", "");
  }

  private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
  {
    var payload = jwt.Split('.')[1];
    var jsonBytes = ParseBase64WithoutPadding(payload);
    var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
    return keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()));
  }

  private byte[] ParseBase64WithoutPadding(string base64)
  {
    switch (base64.Length % 4)
    {
      case 2: base64 += "=="; break;
      case 3: base64 += "="; break;
    }
    return Convert.FromBase64String(base64);
  }
}
