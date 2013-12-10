using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

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

      // set base uri
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
      HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, method);
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

      // content of the request
      request.Content = new FormUrlEncodedContent(parameters);

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

      // validate HTTP response
      ValidateResponse(response);

      // cache headers
      lastHeaders = response.Headers;

      // read response
      var responseString = await response.Content.ReadAsStringAsync();

      responseString = responseString.Replace("[]", "{}");

      // deserialize object
      T parsedResponse = JsonConvert.DeserializeObject<T>(
        responseString,
        new JsonSerializerSettings
        {
          Error = (object sender, ErrorEventArgs args) =>
          {
            throw new UptimeSharpException(String.Format("Parse error: {0}", args.ErrorContext.Error.Message));
          },
          Converters =
          {
            new BoolConverter(),
            new UnixDateTimeConverter(),
            new UriConverter()
          }
        }
      );

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
    protected void ValidateResponse(HttpResponseMessage response)
    {
      // no error found
      if (response.StatusCode == HttpStatusCode.OK)
      {
        return;
      }

      //string exceptionString = response.ReasonPhrase;
      //bool isPocketError = response.Headers.Contains("X-Error");

      //// fetch custom pocket headers
      //string error = TryGetHeaderValue(response.Headers, "X-Error");
      //int errorCode = Convert.ToInt32(TryGetHeaderValue(response.Headers, "X-Error-Code"));

      //// create exception strings
      //if (isPocketError)
      //{
      //  exceptionString = String.Format("Pocket error: {0} ({1}) ", error, errorCode);
      //}
      //else
      //{
      //  exceptionString = String.Format("Request error: {0} ({1})", response.ReasonPhrase, (int)response.StatusCode);
      //}

      //// create exception
      //PocketException exception = new PocketException(exceptionString);

      //if (isPocketError)
      //{
      //  // add custom pocket fields
      //  exception.PocketError = error;
      //  exception.PocketErrorCode = errorCode;

      //  // add to generic exception data
      //  exception.Data.Add("X-Error", error);
      //  exception.Data.Add("X-Error-Code", errorCode);
      //}

      //throw exception;
    }
  }
}
