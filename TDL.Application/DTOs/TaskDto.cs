namespace TDL.Application.DTOs;

public record TaskDto(string Id, string Title, string Description,
                  DateTime Deadline, bool IsCompleted, string UserId);
