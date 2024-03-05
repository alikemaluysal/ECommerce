using WebMVC.Client.HttpClientHelpers.Responses;

namespace WebMVC.Client.HttpClientHelpers.Exceptions;

public class BaseApiException : Exception
{
    public ApiErrorResponse ErrorResponse { get; private set; }

    public BaseApiException(ApiErrorResponse errorResponse)
        : base(errorResponse?.Detail)
    {
        ErrorResponse = errorResponse;
    }
}
