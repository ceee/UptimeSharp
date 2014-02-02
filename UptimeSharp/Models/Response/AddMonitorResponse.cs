using Newtonsoft.Json;

namespace UptimeSharp.Models
{
  /// <summary>
  /// Add Monitor Response
  /// </summary>
  [JsonObject]
  internal class AddMonitorResponse : Response
  {
    /// <summary>
    /// Gets or sets the monitor.
    /// </summary>
    /// <value>
    /// The monitor.
    /// </value>
    [JsonProperty("monitor")]
    public Monitor Monitor { get; set; }
  }
}
