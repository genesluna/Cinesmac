namespace Application.Core;

public enum ErrorType
{
  NotFound,
  NoContent,
  SaveChangesError,
  Unauthorized
}

public class Result<T>
{
  public bool IsSuccess { get; set; }
  public T Value { get; set; }
  public ErrorType ErrorType { get; set; }
  public string ErrorMessage { get; set; }
  public static Result<T> Success(T value) => new Result<T> { IsSuccess = true, Value = value };
  public static Result<T> Failure(ErrorType errorType, string errorMessage = null) =>
    new Result<T> { IsSuccess = false, ErrorMessage = errorMessage, ErrorType = errorType };
}
