using Newtonsoft.Json;
using System;

namespace UptimeSharp.Models
{
  /// <summary>
  /// The Response Time POCO
  /// </summary>
  [JsonObject]
  public class ResponseTime
  {
    /// <summary>
    /// Gets or sets the response time in milliseconds.
    /// </summary>
    /// <value>
    /// The type.
    /// </value>
    [JsonProperty("value")]
    public int Time { get; set; }

    /// <summary>
    /// Gets or sets the date time, when the response time was saved.
    /// </summary>
    /// <value>
    /// The date.
    /// </value>
    [JsonProperty("datetime")]
    public DateTime Date { get; set; }
  }
}
