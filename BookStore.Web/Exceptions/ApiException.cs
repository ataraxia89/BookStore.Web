using System.Net;

namespace BookStore.Web.Exceptions;

public class ApiException : Exception
{
    public ApiException(HttpStatusCode statusCode, string message)
        : base($"{statusCode} - {message}")
    {
    }

    public ApiException(HttpStatusCode statusCode)
        : base($"{statusCode} received from API")
    {
    }
}