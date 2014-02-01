using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using UptimeSharp.Models;

namespace UptimeSharp
{
  /// <summary>
  /// UptimeClient
  /// </summary>
  public partial class UptimeClient : IUptimeClient
  {
    /// <summary>
    /// REST client used for the API communication
    /// </summary>
    protected readonly HttpClient _restClient;

    /// <summary>
    /// Caches HTTP headers from last response
    /// </summary>
    private HttpResponseHeaders lastHeaders;

    /// <summary>
    /// Caches JSON data from last response
    /// </summary>
    public string lastResponseData;

    /// <summary>
    /// The base URL for the Pocket API
    /// </summary>
    protected static Uri baseUri = new Uri("http://api.uptimerobot.com/");

    /// <summary>
    /// Accessor for the UptimeRebot API key
    /// see: http://www.uptimerobot.com/api
    /// </summary>
    /// <value>
    /// The API key.
    /// </value>
    public string ApiKey { get; set; }

    /// <summary>
    /// Action which is executed before every request
    /// </summary>
    /// <value>
    /// The pre request callback.
    /// </value>
    public Action<string> PreRequest { get; set; }


    /// <summary>
    /// The fail codes that don't raise exceptions
    /// </summary>
    private static readonly string[] successFailCodes = new string[] { "212", "221" };


    /// <summary>
    /// Initializes a new instance of the <see cref="UptimeClient"/> class.
    /// </summary>
    /// <param name="apiKey">The API key</param>
    /// <param name="handler">The HttpMessage handler.</param>
    /// <param name="timeout">Request timeout (in seconds).</param>
    public UptimeClient(string apiKey, HttpMessageHandler handler = null, int? timeout = null)
    {
      // assign public properties
      ApiKey = apiKey;

      // initialize REST client
      _restClient = new HttpClient(handler ?? new HttpClientHandler()
      {
        AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip
      });

      if (timeout.HasValue)
      {
        _restClient.Timeout = TimeSpan.FromSeconds(timeout.Value);
      }

      // set base target
      _restClient.BaseAddress = baseUri;

      // defines the response format
      _restClient.DefaultRequestHeaders.Add("Accept", "application/json");
    }


    /// <summary>
    /// Fetches a typed resource
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="method">Requested method (appended to base path)</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <param name="parameters">Additional POST parameters</param>
    /// <returns></returns>
    protected async Task<T> Request<T>(string method, CancellationToken cancellationToken, Dictionary<string, string> parameters = null) where T : class, new()
    {
      HttpRequestMessage request;
      HttpResponseMessage response = null;

      if (parameters == null)
      {
        parameters = new Dictionary<string, string>();
      }

      // add api key to each request
      parameters.Add("apiKey", ApiKey);

      // require UptimeRobot to respond with JSON formatting
      parameters.Add("format", "json");

      // UptimeRobot returns by default JSON-P when json-formatting is set
      // with this param it can return raw JSON
      parameters.Add("noJsonCallback", "1");

      IEnumerable<string> paramEnumerable = parameters.Select(item => Uri.EscapeDataString(item.Key) + "=" + Uri.EscapeDataString(item.Value));

      // content of the request
      request = new HttpRequestMessage(HttpMethod.Get, method + "?" + String.Join("&", paramEnumerable));

      // call pre request action
      if (PreRequest != null)
      {
        PreRequest(method);
      }

      // make async request
      try
      {
        response = await _restClient.SendAsync(request, cancellationToken);
      }
      catch (HttpRequestException exc)
      {
        throw new UptimeSharpException(exc.Message, exc);
      }

      // HTTP error
      if (!response.IsSuccessStatusCode)
      {
        throw new UptimeSharpException("Request Exception: {0} (Code: {1})", response.ReasonPhrase, response.StatusCode.ToString());
      }

      // cache headers
      lastHeaders = response.Headers;

      // read response
      var responseString = await response.Content.ReadAsStringAsync();

      // cache response
      lastResponseData = responseString;

      // deserialize object
      T parsedResponse = JsonConvert.DeserializeObject<T>(
        responseString,
        new JsonSerializerSettings
        {
          Error = (object sender, ErrorEventArgs args) =>
          {
            throw new UptimeSharpException(String.Format("Parse Exception: {0}", args.ErrorContext.Error.Message));
          },
          Converters =
          {
            new BoolConverter(),
            new UnixDateTimeConverter(),
            new UriConverter()
          }
        }
      );

      // validate response
      ValidateResponse(parsedResponse as IResponse);

      return parsedResponse;
    }


    /// <summary>
    /// Validates the response.
    /// </summary>
    /// <param name="response">The response.</param>
    /// <returns></returns>
    /// <exception cref="UptimeSharpException">
    /// Error retrieving response
    /// </exception>
    internal void ValidateResponse(IResponse response)
    {
      // it's a success ;-)
      if (response.Success)
      {
        return;
      }

      // don't raise exceptions for minor error codes
      if (successFailCodes.Contains(response.ErrorCode))
      {
        return;
      }

      // create exception
      UptimeSharpException exception = new UptimeSharpException(
        "UptimeRobot Exception: {0} (Code: {1})\n{2}",
        null,
        response.ErrorMessage,
        response.ErrorCode,
        "http://uptimerobot.com/api.asp#errorMessages"
      );

      // add custom fields
      exception.Error = response.ErrorMessage;
      exception.ErrorCode = response.ErrorCode;

      // add to generic exception data
      exception.Data.Add("Error", response.ErrorMessage);
      exception.Data.Add("ErrorCode", response.ErrorCode);

      throw exception;
    }
  }
}
