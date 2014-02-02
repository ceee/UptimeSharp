using Newtonsoft.Json;

namespace UptimeSharp.Models
{
  /// <summary>
  /// Alert Response
  /// </summary>
  [JsonObject]
  internal class AddAlertResponse : Response
  {
    /// <summary>
    /// Gets or sets the item dictionary.
    /// The list is 2 layers deep, so this one is necessary :-/
    /// </summary>
    /// <value>
    /// The item dictionary.
    /// </value>
    [JsonProperty("alertcontact")]
    public Alert Alert { get; set; }
  }
}
