using Newtonsoft.Json;

namespace UptimeSharp.Models
{
  /// <summary>
  /// Base for Responses
  /// </summary>
  [JsonObject]
  internal class ResponseBase
  {

    /// <summary>
    /// Gets or sets the error code.
    /// </summary>
    /// <value>
    /// The error code.
    /// </value>
    [JsonProperty("id")]
    public string ErrorCode { get; set; }

    /// <summary>
    /// Gets or sets the error message.
    /// </summary>
    /// <value>
    /// The error message.
    /// </value>
    [JsonProperty("message")]
    public string ErrorMessage { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this <see cref="ResponseBase"/> is status.
    /// </summary>
    /// <value>
    ///   "ok" or "fail"
    /// </value>
    [JsonProperty("stat")]
    public string RawStatus { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this <see cref="ResponseBase"/> is status.
    /// </summary>
    /// <value>
    ///   <c>true</c> if status is OK; otherwise, <c>false</c>.
    /// </value>
    [JsonIgnore]
    public bool Status
    {
      get { return RawStatus == "ok"; }
    }
  }
}
