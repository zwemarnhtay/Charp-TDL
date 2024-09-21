using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Text;
using TDL.UI.Models;
using static System.Net.Mime.MediaTypeNames;

namespace TDL.UI.Services;

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
    Console.WriteLine(authState);
    var authUser = authState.User;
    return authUser.FindFirst(ClaimTypes.NameIdentifier)?.Value;
  }

  public async Task<HttpResponseMessage> CreateAsync(TaskCreateModel model)
  {
    var authId = await GetAuthUserId();
    Console.WriteLine(authId);
    model.UserId = authId;

    var json = JsonConvert.SerializeObject(model);
    var content = new StringContent(json, Encoding.UTF8, Application.Json);

    return await _httpClient.PostAsync("api/Task", content);
  }
}
