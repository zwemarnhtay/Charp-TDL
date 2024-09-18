using Newtonsoft.Json;
using System.Text;
using TDL.UI.Models;
using static System.Net.Mime.MediaTypeNames;

namespace TDL.UI.Services;

public class AuthService
{
  HttpClient _httpClient;

  public AuthService(HttpClient httpClient)
  {
    _httpClient = httpClient;
  }

  public async Task<HttpResponseMessage> RegisterAsync(RegisterModel model)
  {
    var jsonBlog = JsonConvert.SerializeObject(model);
    var content = new StringContent(jsonBlog, Encoding.UTF8, Application.Json);
    return await _httpClient.PostAsync("api/Auth/Register", content);
  }

  public async Task<HttpResponseMessage> LoginAsync(LoginModel model)
  {
    var jsonBlog = JsonConvert.SerializeObject(model);
    var content = new StringContent(jsonBlog, Encoding.UTF8, Application.Json);
    return await _httpClient.PostAsync("api/Auth/Login", content);
  }
}
