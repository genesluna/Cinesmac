namespace API.Errors;

public class ApiValidationErrorResponse : ApiResponse
{
  public ApiValidationErrorResponse() : base(400)
  {
  }

  public ICollection<string> Errors { get; set; }
}
