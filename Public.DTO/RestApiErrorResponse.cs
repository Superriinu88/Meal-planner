using System.Net;

namespace Public.DTO;

public class RestApiErrorResponse
{
    public HttpStatusCode Status { get; set; }
    
    public string Error { get; set; } = default!;

}
