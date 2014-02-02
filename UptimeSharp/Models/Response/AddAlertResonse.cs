using Newtonsoft.Json;

namespace UptimeSharp.Models
{
  /// <summary>
  /// Add Alert Response
  /// </summary>
  [JsonObject]
  internal class AddAlertResponse : Response
  {
    /// <summary>
    /// Gets or sets the alert.
    /// </summary>
    /// <value>
    /// The alert.
    /// </value>
    [JsonProperty("alertcontact")]
    public Alert Alert { get; set; }
  }
}
