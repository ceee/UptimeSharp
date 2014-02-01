using Newtonsoft.Json;
using System.Collections.Generic;

namespace UptimeSharp.Models
{
  /// <summary>
  /// Alert Response
  /// </summary>
  [JsonObject]
  internal class AlertResponse : Response
  {
    /// <summary>
    /// Gets or sets the item dictionary.
    /// The list is 2 layers deep, so this one is necessary :-/
    /// </summary>
    /// <value>
    /// The item dictionary.
    /// </value>
    [JsonProperty("alertcontacts")]
    public Dictionary<string, List<Alert>> ItemDictionary { get; set; }

    /// <summary>
    /// Gets the items.
    /// </summary>
    /// <value>
    /// The items.
    /// </value>
    [JsonIgnore]
    public List<Alert> Items
    {
      get
      {
        return ItemDictionary["alertcontact"];
      }
    }
  }
}
