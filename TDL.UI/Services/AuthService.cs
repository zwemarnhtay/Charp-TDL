using Blazored.LocalStorage;
using Newtonsoft.Json;
using System.Text;
using TDL.UI.Models;
using static System.Net.Mime.MediaTypeNames;

namespace TDL.UI.Services;

public class AuthService
{
  private readonly HttpClient _httpClient;
  private readonly ILocalStorageService _localStorage;

  public AuthService(HttpClient httpClient, ILocalStorageService localStorage)
  {
    _httpClient = httpClient;
    _localStorage = localStorage;
  }

  public async Task<HttpResponseMessage> RegisterAsync(RegisterModel model)
  {
    var jsonBlog = JsonConvert.SerializeObject(model);
    var content = new StringContent(jsonBlog, Encoding.UTF8, Application.Json);
    return await _httpClient.PostAsync("api/Auth/Register", content);
  }

  public async Task<bool> LoginAsync(LoginModel model)
  {
    var jsonBlog = JsonConvert.SerializeObject(model);
    var content = new StringContent(jsonBlog, Encoding.UTF8, Application.Json);
    var response = await _httpClient.PostAsync("api/Auth/Login", content);

    if (!response.IsSuccessStatusCode)
    {
      return false;
    }

    string token = await response.Content.ReadAsStringAsync();
    await _localStorage.SetItemAsync("token", token);
    return true;
  }
}
