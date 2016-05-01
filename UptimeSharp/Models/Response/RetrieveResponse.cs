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
    public Dictionary<string, List<Monitor>> ItemDictionary { get; set; }

    [JsonProperty("timezone")]
    public string Timezone { get; set; }

    /// <summary>
    /// Gets the items.
    /// </summary>
    /// <value>
    /// The items.
    /// </value>
    [JsonIgnore]
    public List<Monitor> Items
    {
      get
      {
        return ItemDictionary != null ? ItemDictionary["monitor"] : null;
      }
    }
  }
}
