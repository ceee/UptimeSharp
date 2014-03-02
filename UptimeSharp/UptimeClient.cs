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
    /// REST client used for the account communication
    /// </summary>
    protected readonly HttpClient _accountClient;

    /// <summary>
    /// Caches HTTP headers from last response
    /// </summary>
    private HttpResponseHeaders lastHeaders;

    /// <summary>
    /// Caches JSON data from last response
    /// </summary>
    public string lastResponseData;

    /// <summary>
    /// The base URL for the UptimeRobot API
    /// </summary>
    protected static Uri baseUri = new Uri("http://api.uptimerobot.com/");

    /// <summary>
    /// The account base URL for the UptimeRobot API
    /// Does not use the official API endpoint
    /// </summary>
    protected static Uri accountBaseUri = new Uri("http://uptimerobot.com/inc/dml/userDML.php");

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
      _accountClient = new HttpClient(handler ?? new HttpClientHandler()
      {
        AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip
      });

      if (timeout.HasValue)
      {
        _restClient.Timeout = TimeSpan.FromSeconds(timeout.Value);
        _accountClient.Timeout = TimeSpan.FromSeconds(timeout.Value);
      }

      _restClient.BaseAddress = baseUri;
      _accountClient.BaseAddress = accountBaseUri;

      // defines the response format
      _restClient.DefaultRequestHeaders.Add("Accept", "application/json");
      _accountClient.DefaultRequestHeaders.Add("Accept", "application/json");
    }


    /// <summary>
    /// Fetches a typed resource
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="method">Requested method (appended to base path)</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <param name="parameters">Additional POST parameters</param>
    /// <param name="isAccountRequest">if set to <c>true</c> [is account request].</param>
    /// <returns></returns>
    /// <exception cref="UptimeSharpException">
    /// </exception>
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

      IEnumerable<string> paramEnumerable = parameters.Where(item => !String.IsNullOrEmpty(item.Value)).Select(item => Uri.EscapeDataString(item.Key) + "=" + Uri.EscapeDataString(item.Value));

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
      string responseString = await response.Content.ReadAsStringAsync();
      T parsedResponse;

      // fix buggy response by uptimerobot óÒ
      if (!String.IsNullOrEmpty(responseString))
      {
        responseString = responseString.Replace("}{", "},{");
      }

      // cache response
      lastResponseData = responseString;

      // deserialize object
      parsedResponse = JsonConvert.DeserializeObject<T>(
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
            new UriConverter(),
            new EnumConverter()
          }
        }
      );

      // validate response
      ValidateResponse(parsedResponse as IResponse);

      return parsedResponse;
    }


    /// <summary>
    /// Fetches a typed resource
    /// </summary>
    /// <param name="action">The action.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <param name="parameters">Additional POST parameters</param>
    /// <param name="isPost">if set to <c>true</c> [is post].</param>
    /// <returns></returns>
    /// <exception cref="UptimeSharpException">
    /// </exception>
    internal async Task<AccountResponse> AccountRequest(string action, CancellationToken cancellationToken, Dictionary<string, string> parameters = null, bool isPost = false)
    {
      HttpRequestMessage request;
      HttpResponseMessage response = null;

      if (parameters == null)
      {
        parameters = new Dictionary<string, string>();
      }

      if (!isPost)
      {
        IEnumerable<string> paramEnumerable = parameters.Where(item => !String.IsNullOrEmpty(item.Value)).Select(item => Uri.EscapeDataString(item.Key) + "=" + Uri.EscapeDataString(item.Value));
        request = new HttpRequestMessage(HttpMethod.Get, "?action=" + Uri.EscapeDataString(action) + "&" + String.Join("&", paramEnumerable));
      }
      else
      {
        request = new HttpRequestMessage(HttpMethod.Post, "?action=" + Uri.EscapeDataString(action))
        {
          Content = new FormUrlEncodedContent(parameters.Where(item => !String.IsNullOrEmpty(item.Value)))
        };
      }

      // call pre request action
      if (PreRequest != null)
      {
        PreRequest(action);
      }

      // make async request
      try
      {
        response = await _accountClient.SendAsync(request, cancellationToken);
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
      string responseString = await response.Content.ReadAsStringAsync();

      // cache response
      lastResponseData = responseString;

      // deserialize object
      return new AccountResponse()
      {
        RawStatus = responseString == "true" ? "ok" : "fail"
      };
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
      if (!String.IsNullOrEmpty(response.ErrorCode) && successFailCodes.Contains(response.ErrorCode))
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
