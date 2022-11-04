namespace API.Errors;
public class ApiResponse
{
  public ApiResponse(int statusCode, string message = null)
  {
    StatusCode = statusCode;
    Message = message ?? GetDefaultMessageForStatuscode(statusCode);
  }

  public int StatusCode { get; set; }
  public string Message { get; set; }

  private string GetDefaultMessageForStatuscode(int statusCode)
  {
    return statusCode switch
    {
      400 => "You have made a bad request",
      401 => "You're not authorized",
      404 => "Resource not found",
      500 => "Internal server error",
      _ => null
    };
  }

}
