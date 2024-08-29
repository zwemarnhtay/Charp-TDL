namespace TDL.Application.DTOs;

public record TaskDto(string Id, string Title, string Description,
                  DateOnly Deadline, bool IsCompleted, string UserId);
