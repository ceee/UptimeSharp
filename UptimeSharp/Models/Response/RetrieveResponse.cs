using Newtonsoft.Json;
using System.Collections.Generic;

namespace UptimeSharp.Models
{
  /// <summary>
  /// Monitor Response
  /// </summary>
  [JsonObject]
  internal class RetrieveResponse : Response
  {
    /// <summary>
    /// Gets or sets the item dictionary.
    /// The list is 2 layers deep, so this one is necessary :-/
    /// </summary>
    /// <value>
    /// The item dictionary.
    /// </value>
    [JsonProperty("monitors")]
    public List<Monitor> Monitors { get; set; }

    [JsonProperty("timezone")]
    public string Timezone { get; set; }
  }
}
