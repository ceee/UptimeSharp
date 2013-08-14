using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UptimeSharp
{
  /// <summary>
  /// UptimeClient
  /// </summary>
  public partial class UptimeClient
  {
    /// <summary>
    /// REST client used for the API communication
    /// </summary>
    protected readonly RestClient _restClient;

    /// <summary>
    /// The base URL for the UptimeRobot API
    /// </summary>
    protected static Uri baseUri = new Uri("http://api.uptimerobot.com/");

    /// <summary>
    /// callback URL for API calls
    /// </summary>
    protected string CallbackUri { get; set; }

    /// <summary>
    /// Accessor for the UptimeRebot API key
    /// see: http://http://www.uptimerobot.com/api.asp
    /// </summary>
    public string ApiKey { get; set; }

    /// <summary>
    /// Returns all associated data from the last request
    /// </summary>
    public IRestResponse LastRequestData { get; private set; }


    /// <summary>
    /// Initializes a new instance of the <see cref="UptimeClient"/> class.
    /// </summary>
    /// <param name="apiKey">The API key</param>
    public UptimeClient(string apiKey)
    {
      // assign public properties
      ApiKey = apiKey;

      // initialize REST client
      _restClient = new RestClient(baseUri.ToString());

      // add default parameters to each request
      _restClient.AddDefaultParameter("apiKey", ApiKey);

      // defines the response format (according to the UptimeRobot docs)
      _restClient.AddDefaultParameter("format", "json");

      // UptimeRobot returns by default JSON-P when json-formatting is set
      // with this param it can return raw JSON
      _restClient.AddDefaultParameter("noJsonCallback", "1");

      // custom JSON deserializer (ServiceStack.Text)
      _restClient.AddHandler("application/json", new JsonDeserializer());

      // add custom deserialization lambdas
      JsonDeserializer.AddCustomDeserialization();
    }


    /// <summary>
    /// Makes a HTTP REST request to the API
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns></returns>
    public string Request(RestRequest request)
    {
      IRestResponse response = _restClient.Execute(request);

      LastRequestData = response;
      //ValidateResponse(response);

      return response.Content;
    }

    /// <summary>
    /// Makes a typed HTTP REST request to the API
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="request">The request.</param>
    /// <returns></returns>
    protected T Request<T>(RestRequest request) where T : new()
    {
      IRestResponse<T> response = _restClient.Execute<T>(request);

      LastRequestData = response;
      //ValidateResponse(response);

      return response.Data;
    }
  }
}
