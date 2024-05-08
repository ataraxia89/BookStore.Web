using System.Net;
using System.Text.Json;

namespace BookStore.Web.Data.Tools;

/// <summary>
/// A helper class to return the result of an API call including a returned object.
/// </summary>
/// <typeparam name="T">The object type to be returned.</typeparam>
public class ApiResult<T> : ApiResult
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ApiResult{T}"/> class.
    /// </summary>
    /// <param name="returnObject">The response object to be returned.</param>
    /// <param name="statusCode">The returned HTTP status code.</param>
    /// <param name="isSuccess">Whether the API response was a successful status code.</param>
    public ApiResult(T returnObject, HttpStatusCode statusCode, bool isSuccess)
        : base(statusCode, isSuccess)
    {
        Object = returnObject;
    }

    /// <summary>
    /// Gets the object returned in the API response.
    /// </summary>
    public T Object { get; }

    public static async Task<ApiResult<T>> GenerateApiResultAsync(HttpResponseMessage response)
    {
        string content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            return new ApiResult<T>(default, response.StatusCode, response.IsSuccessStatusCode);
        }

        T returnObject = typeof(T) == typeof(string)
            ? (T)Convert.ChangeType(content, typeof(T))
            : JsonSerializer.Deserialize<T>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return new ApiResult<T>(returnObject, response.StatusCode, response.IsSuccessStatusCode);
    }
}

/// <summary>
/// A helper class to return the result of an API call.
/// </summary>
public class ApiResult
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ApiResult"/> class.
    /// </summary>
    /// <param name="statusCode">The returned HTTP status code.</param>
    /// <param name="isSuccess">Whether the API response was a successful status code.</param>
    public ApiResult(HttpStatusCode statusCode, bool isSuccess)
    {
        StatusCode = statusCode;
        IsSuccess = isSuccess;
    }

    /// <summary>
    /// Gets a value indicating the returned HTTP status code.
    /// </summary>
    public HttpStatusCode StatusCode { get; }

    /// <summary>
    /// Gets a value indicating whether the API response was a successful status code.
    /// </summary>
    public bool IsSuccess { get; }

    public static ApiResult GenerateApiResult(HttpResponseMessage response)
    {
        return new ApiResult(response.StatusCode, response.IsSuccessStatusCode);
    }
}