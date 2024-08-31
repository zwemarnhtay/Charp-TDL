using TDL.Domain.Enums;

namespace TDL.Application.DTOs;

public class ResponseDto<T>
{
  public ResponseStatusCode StatusCode { get; set; }
  public bool IsSuccess { get; set; }
  public string Message { get; set; }
  public T Data { get; set; }

  public static ResponseDto<T> Success(ResponseStatusCode statusCode, string msg) =>
    new ResponseDto<T> { IsSuccess = true, StatusCode = statusCode, Message = msg };

  public static ResponseDto<T> Success(ResponseStatusCode statusCode, string msg, T data) =>
    new ResponseDto<T> { IsSuccess = true, StatusCode = statusCode, Message = msg, Data = data };

  public static ResponseDto<T> Fail(ResponseStatusCode statusCode, string msg) =>
    new ResponseDto<T> { IsSuccess = false, StatusCode = statusCode, Message = msg };
}
