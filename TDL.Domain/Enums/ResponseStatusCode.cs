namespace TDL.Domain.Enums;

public enum ResponseStatusCode
{
  OK = 200,
  Created = 201,
  BadRequest = 400,
  UnAuthorized = 401,
  NotFound = 404,
  Conflict = 409, //already existed
  RequestCanceled = 499,
  InternalServerError = 500
}
