using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
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

  public async Task<HttpResponseMessage> CreateAsync(TaskCreateModel model)
  {
    var authId = await GetAuthUserId();
    Console.WriteLine(authId);
    model.UserId = authId;

    Console.WriteLine("deadline : " + model.Deadline);

    var json = JsonConvert.SerializeObject(model);
    Console.WriteLine(json);

    var content = new StringContent(json, Encoding.UTF8, Application.Json);

    return await _httpClient.PostAsync("api/Task", content);
  }

  public async Task<List<TaskModel>?> GetTaskListById()
  {
    string userId = await GetAuthUserId();
    Console.WriteLine(userId);
    var response = await _httpClient.GetAsync($"api/Task/{userId}/list");

    if (!response.IsSuccessStatusCode)
    {
      return null;
    }

    var jsonStr = await response.Content.ReadAsStringAsync();

    Console.WriteLine(jsonStr);

    var document = JsonDocument.Parse(jsonStr);
    var root = document.RootElement;

    var data = root.GetProperty("data").GetRawText();

    Console.WriteLine("data : " + data);

    var list = JsonConvert.DeserializeObject<List<TaskModel>>(data);
    return list;
  }

  public async Task<string?> CompleteTaskAsync(string id)
  {
    var json = JsonConvert.SerializeObject(id);
    var content = new StringContent(json, Encoding.UTF8, Application.Json);

    var response = await _httpClient.PutAsync($"api/Task/{id}/complete", content);

    if (!response.IsSuccessStatusCode)
    {
      return null;
    }

    var jsonStr = await response.Content.ReadAsStringAsync();

    var document = JsonDocument.Parse(jsonStr);
    var root = document.RootElement;

    var msg = root.GetProperty("message").GetRawText();

    return msg;
  }

  private async Task<String> GetAuthUserId()
  {
    var authState = await _authStateProvider.GetAuthenticationStateAsync();

    var authUser = authState.User;
    return authUser.FindFirst(ClaimTypes.NameIdentifier)?.Value;
  }
}
