using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;

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
      ValidateResponse<T>(response);

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


    /// <summary>
    /// Creates a RestSharp.Parameter instance.
    /// </summary>
    /// <param name="name">The name.</param>
    /// <param name="value">The value.</param>
    /// <returns></returns>
    internal static Parameter Parameter(string name, object value)
    {
      return new Parameter()
      {
        Name = name,
        Value = value,
        Type = ParameterType.GetOrPost
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
    protected void ValidateResponse<T>(IRestResponse<T> response) where T : new()
    {
      Type responseType = response.Data.GetType();

      // get status property from ResponseBase POCO
      PropertyInfo statusProp = responseType.GetProperty("Status");
      object status = statusProp.GetValue(response.Data, null);

      // Error from UptimeSharp
      if (status.Equals(false))
      {
        // get error properties from response
        PropertyInfo errorProp = responseType.GetProperty("ErrorMessage");
        object error = errorProp.GetValue(response.Data, null);

        PropertyInfo errorCodeProp = responseType.GetProperty("ErrorCode");
        object errorCode = errorCodeProp.GetValue(response.Data, null);

        // create exception
        UptimeSharpException exception = new UptimeSharpException(
          "UptimeRobot Exception: {0} (Code: {1})\n{2}",
          response.ErrorException,
          error,
          errorCode,
          "http://uptimerobot.com/api.asp#errorMessages"
        );

        // add custom pocket fields
        exception.Error = error.ToString();
        exception.ErrorCode = Convert.ToInt32(errorCode);

        // add to generic exception data
        exception.Data.Add("Error", error);
        exception.Data.Add("ErrorCode", errorCode);

        throw exception;
      }
      // HTTP Request Error
      else if (response.StatusCode != HttpStatusCode.OK)
      {
        throw new UptimeSharpException(
          "Request Exception: {0} (Code: {1})", 
          response.ErrorException, 
          response.ErrorMessage, 
          response.StatusCode
        );
      }
      // Exception by RestSharp
      else if (response.ErrorException != null)
      {
        throw new UptimeSharpException("Error retrieving response", response.ErrorException);
      }
    }
  }
}
