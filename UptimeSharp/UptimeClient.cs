using RestSharp;
using System;
using System.Collections.Generic;

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
    protected string Request(RestRequest request)
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
      // fix content type if wrong determined by RestSharp
      request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };

      IRestResponse<T> response = _restClient.Execute<T>(request);

      LastRequestData = response;
      //ValidateResponse(response);

      return response.Data;
    }


    /// <summary>
    /// Fetches a typed resource
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="resource">Requested resource</param>
    /// <param name="parameters">Additional GET parameters</param>
    /// <returns></returns>
    protected T Get<T>(string resource, List<Parameter> parameters = null) where T : class, new()
    {
      // UptimeRobot uses GET for all of its endpoints
      var request = new RestRequest(resource, Method.GET);

      // enumeration for params
      if (parameters != null)
      {
        parameters.ForEach(
          param => {
            if(param.Value != null)
            {
              request.AddParameter(param);
            }
          }
        );
      }

      // do the request
      return Request<T>(request);
    }


    /// <summary>
    /// Fetches a typed resource
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="resource">Requested resource</param>
    /// <param name="parameter">The parameters.</param>
    /// <returns></returns>
    protected T Get<T>(string resource, params Parameter[] parameters) where T : class, new()
    {
      List<Parameter> parameterList = new List<Parameter>();
      parameterList.AddRange(parameters);

      return Get<T>(resource, parameterList);
    }


    public static Parameter Parameter(string name, object value)
    {
      return new Parameter()
      {
        Name = name,
        Value = value,
        Type = ParameterType.GetOrPost
      };
    }


    ///// <summary>
    ///// Validates the response.
    ///// </summary>
    ///// <param name="response">The response.</param>
    ///// <returns></returns>
    ///// <exception cref="APIException">
    ///// Error retrieving response
    ///// </exception>
    //protected void ValidateResponse(IRestResponse response)
    //{
    //  if (response.StatusCode != HttpStatusCode.OK)
    //  {
    //    // get pocket error headers
    //    Parameter error = response.Headers[1];
    //    Parameter errorCode = response.Headers[2];

    //    string exceptionString = response.Content;

    //    bool isPocketError = error.Name == "X-Error";

    //    // update message to include pocket response data
    //    if (isPocketError)
    //    {
    //      exceptionString = exceptionString + "\nPocketResponse: (" + errorCode.Value + ") " + error.Value;
    //    }

    //    // create exception
    //    APIException exception = new APIException(exceptionString, response.ErrorException);

    //    if (isPocketError)
    //    {
    //      // add custom pocket fields
    //      exception.PocketError = error.Value.ToString();
    //      exception.PocketErrorCode = Convert.ToInt32(errorCode.Value);

    //      // add to generic exception data
    //      exception.Data.Add(error.Name, error.Value);
    //      exception.Data.Add(errorCode.Name, errorCode.Value);
    //    }

    //    throw exception;
    //  }
    //  else if (response.ErrorException != null)
    //  {
    //    throw new APIException("Error retrieving response", response.ErrorException);
    //  }
    //}
  }
}
