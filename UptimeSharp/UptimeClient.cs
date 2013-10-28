using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace UptimeSharp
{
  /// <summary>
  /// UptimeClient
  /// </summary>
  public class UptimeClient : IUptimeClient
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
    public string ApiKey { get; set; }


    /// <summary>
    /// Initializes a new instance of the <see cref="UptimeClient"/> class.
    /// </summary>
    /// <param name="apiKey">The API key</param>
    public UptimeClient(string apiKey)
    {
      // assign public properties
      ApiKey = apiKey;

      // initialize REST client
      _restClient = new HttpClient(new HttpClientHandler()
      {
        AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip
      });

      // set base uri
      _restClient.BaseAddress = baseUri;

      // defines the response format
      _restClient.DefaultRequestHeaders.Add("Accept", "application/json");
    }
  }
}
