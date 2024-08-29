using TDL.Domain.Enums;

namespace TDL.Application.DTOs;

public class ResponseDto<T>
{
  public ResponseStatusCode StatusCode { get; set; }
  public bool IsSuccess { get; set; }
  public string Message { get; set; }
  public T Data { get; set; }
}
