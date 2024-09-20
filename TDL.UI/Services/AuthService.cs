using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json;
using TDL.UI.Models;
using static System.Net.Mime.MediaTypeNames;

namespace TDL.UI.Services;

public class AuthService
{
  private readonly HttpClient _httpClient;
  private readonly ILocalStorageService _localStorage;
  private readonly AuthenticationStateProvider _authStateProvider;

  public AuthService(
    HttpClient httpClient,
    ILocalStorageService localStorage,
    AuthenticationStateProvider authStateProvider
    )
  {
    _httpClient = httpClient;
    _localStorage = localStorage;
    _authStateProvider = authStateProvider;
  }

  public async Task<HttpResponseMessage> RegisterAsync(RegisterModel model)
  {
    var json = JsonConvert.SerializeObject(model);
    var content = new StringContent(json, Encoding.UTF8, Application.Json);
    return await _httpClient.PostAsync("api/Auth/Register", content);
  }

  public async Task<bool> LoginAsync(LoginModel model)
  {
    var json = JsonConvert.SerializeObject(model);
    var content = new StringContent(json, Encoding.UTF8, Application.Json);
    var response = await _httpClient.PostAsync("api/Auth/Login", content);

    if (!response.IsSuccessStatusCode)
    {
      return false;
    }

    string jsonResponse = await response.Content.ReadAsStringAsync();

    var jsonDocument = JsonDocument.Parse(jsonResponse);
    var root = jsonDocument.RootElement;

    // Extract JWT token and other fields dynamically
    var token = root.GetProperty("jwt").GetString();

    await _localStorage.SetItemAsync("Token", token);
    await _authStateProvider.GetAuthenticationStateAsync();

    return true;
  }
}
