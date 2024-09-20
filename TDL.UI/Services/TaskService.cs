using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using TDL.UI.Models;
using static System.Net.Mime.MediaTypeNames;

namespace TDL.UI.Services
{
  public class TaskService
  {
    private readonly AuthenticationStateProvider _authStateProvider;
    private readonly HttpClient _httpClient;

    public TaskService(AuthenticationStateProvider authStateProvider, HttpClient httpClient)
    {
      _authStateProvider = authStateProvider;
      _httpClient = httpClient;
    }

    private async Task<String> GetAuthUserId()
    {
      var authState = await _authStateProvider.GetAuthenticationStateAsync();
      var authUser = authState.User;
      return authUser.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    }

    public async Task<HttpResponseMessage> CreateAsync(TaskCreateModel model)
    {
      var authId = await GetAuthUserId();

      model.UserId = authId;

      var json = JsonConvert.SerializeObject(model);
      var content = new StringContent(json, Encoding.UTF8, Application.Json);

      var token = await ((CustomAuthStateProvider)_authStateProvider).GetTokenAsync();

      _httpClient.DefaultRequestHeaders.Authorization =
        new AuthenticationHeaderValue("Bearer", token);

      return await _httpClient.PostAsync("api/Task", content);
    }
  }
}
