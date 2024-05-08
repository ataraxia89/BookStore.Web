using System.Net;
using System.Text.Json;
using System.Text;

namespace BookStore.Web.Data.Tools;

/// <summary>
/// A helper class designed to construct HTTP requests and handle responses.
/// </summary>
public static class JsonRequestHelper
{
    /// <summary>
    /// Calls the <see cref="GetResponse(HttpMethod,string[],Dictionary{string,string},object)">GetResponse</see> method providing relevant parameters.
    /// </summary>
    /// <param name="method">The HTTP method used for the request.</param>
    /// <param name="endpoint">The endpoint to send the HTTP request to.</param>
    /// <returns>An object of type <see cref="HttpResponseMessage"/> if successful.</returns>
    public static async Task<HttpResponseMessage> GetResponse(HttpMethod method, string endpoint)
    {
        return await GetResponse(method, new[] { endpoint }, new Dictionary<string, string>(), null);
    }

    /// <summary>
    /// Calls the <see cref="GetResponse(HttpMethod,string[],Dictionary{string,string},object)">GetResponse</see> method providing relevant parameters.
    /// </summary>
    /// <param name="method">The HTTP method used for the request.</param>
    /// <param name="endpointRoute">The endpoint route to send the HTTP request to.</param>
    /// <returns>An object of type <see cref="HttpResponseMessage"/> if successful.</returns>
    public static async Task<HttpResponseMessage> GetResponse(HttpMethod method, string[] endpointRoute)
    {
        return await GetResponse(method, endpointRoute, new Dictionary<string, string>(), null);
    }

    /// <summary>
    /// Calls the <see cref="GetResponse(HttpMethod,string[],Dictionary{string,string},object)">GetResponse</see> method providing relevant parameters.
    /// </summary>
    /// <param name="method">The HTTP method used for the request.</param>
    /// <param name="endpoint">The endpoint to send the HTTP request to.</param>
    /// <param name="jsonObject">The object to be included in the request body.</param>
    /// <returns>An object of type <see cref="HttpResponseMessage"/> if successful.</returns>
    public static async Task<HttpResponseMessage> GetResponse(HttpMethod method, string endpoint, object jsonObject)
    {
        return await GetResponse(method, new[] { endpoint }, new Dictionary<string, string>(), jsonObject);
    }

    /// <summary>
    /// Calls the <see cref="GetResponse(HttpMethod,string[],Dictionary{string,string},object)">GetResponse</see> method providing relevant parameters.
    /// </summary>
    /// <param name="method">The HTTP method used for the request.</param>
    /// <param name="endpointRoute">The endpoint route to send the HTTP request to.</param>
    /// <param name="jsonObject">The object to be included in the request body.</param>
    /// <returns>An object of type <see cref="HttpResponseMessage"/> if successful.</returns>
    public static async Task<HttpResponseMessage> GetResponse(HttpMethod method, string[] endpointRoute, object jsonObject)
    {
        return await GetResponse(method, endpointRoute, new Dictionary<string, string>(), jsonObject);
    }

    /// <summary>
    /// Calls the <see cref="GetResponse(HttpMethod,string[],Dictionary{string,string},object)">GetResponse</see> method providing relevant parameters.
    /// </summary>
    /// <param name="method">The HTTP method used for the request.</param>
    /// <param name="endpoint">The endpoint to send the HTTP request to.</param>
    /// <param name="parameters">The list of parameters to be sent to the endpoint.</param>
    /// <returns>An object of type <see cref="HttpResponseMessage"/> if successful.</returns>
    public static async Task<HttpResponseMessage> GetResponse(HttpMethod method, string endpoint, Dictionary<string, string> parameters)
    {
        return await GetResponse(method, new[] { endpoint }, parameters, null);
    }

    /// <summary>
    /// Calls the <see cref="GetResponse(HttpMethod,string[],Dictionary{string,string},object)">GetResponse</see> method providing relevant parameters.
    /// </summary>
    /// <param name="method">The HTTP method used for the request.</param>
    /// <param name="endpointRoute">The endpoint route to send the HTTP request to.</param>
    /// <param name="parameters">The list of parameters to be sent to the endpoint.</param>
    /// <returns>An object of type <see cref="HttpResponseMessage"/> if successful.</returns>
    public static async Task<HttpResponseMessage> GetResponse(HttpMethod method, string[] endpointRoute, Dictionary<string, string> parameters)
    {
        return await GetResponse(method, endpointRoute, parameters, null);
    }

    /// <summary>
    /// Calls the <see cref="GetResponse(HttpMethod,string[],Dictionary{string,string},object)">GetResponse</see> method providing relevant parameters.
    /// </summary>
    /// <param name="method">The HTTP method used for the request.</param>
    /// <param name="endpoint">The endpoint to send the HTTP request to.</param>
    /// <param name="parameters">The list of parameters to be sent to the endpoint.</param>
    /// <param name="jsonObject">The object to be included in the request body.</param>
    /// <returns>An object of type <see cref="HttpResponseMessage"/> if successful.</returns>
    public static async Task<HttpResponseMessage> GetResponse(HttpMethod method, string endpoint, Dictionary<string, string> parameters, object jsonObject)
    {
        return await GetResponse(method, new[] { endpoint }, parameters, jsonObject);
    }

    /// <summary>
    /// Constructs and sends an HTTP request to the API.
    /// </summary>
    /// <param name="method">The HTTP method used for the request.</param>
    /// <param name="endpointRoute">The endpoint route to send the HTTP request to.</param>
    /// <param name="parameters">The list of parameters to be sent to the endpoint.</param>
    /// <param name="jsonObject">The object to be included in the request body.</param>
    /// <returns>A <see cref="HttpResponseMessage"/> result.</returns>
    public static async Task<HttpResponseMessage> GetResponse(
        HttpMethod method,
        string[] endpointRoute,
        Dictionary<string, string> parameters,
        object jsonObject)
    {
        if ((method == HttpMethod.Get || method == HttpMethod.Delete) && jsonObject != null)
        {
            throw new WebException("JSON bodies are not allowed for GET or DELETE requests.");
        }

        var apiAddress = AppData.ApiAddress;
        var endpointAddress = apiAddress + string.Join("/", endpointRoute);

        if (parameters.Count > 0)
        {
            var parameterArray = parameters
                .Select(kvp => $"{kvp.Key}={kvp.Value}")
                .ToArray();

            endpointAddress += $"?{string.Join("&", parameterArray)}";
        }

        var requestBody = string.Empty;

        if (jsonObject != null)
        {
            requestBody = JsonSerializer.Serialize(jsonObject);
        }

        var client = new HttpClient();
        var request = new HttpRequestMessage(method, endpointAddress)
        {
            Content = new StringContent(requestBody, Encoding.UTF8, "application/json")
        };

        return await client.SendAsync(request);
    }
}