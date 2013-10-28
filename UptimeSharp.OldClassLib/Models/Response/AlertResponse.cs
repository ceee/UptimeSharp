using System.Collections.Generic;
using System.Runtime.Serialization;

namespace UptimeSharp.Models
{
  /// <summary>
  /// Alert Response
  /// </summary>
  [DataContract]
  internal class AlertResponse : ResponseBase
  {
    /// <summary>
    /// Gets or sets the item dictionary.
    /// The list is 2 layers deep, so this one is necessary :-/
    /// </summary>
    /// <value>
    /// The item dictionary.
    /// </value>
    [DataMember(Name = "alertcontacts")]
    public Dictionary<string, List<Alert>> ItemDictionary { get; set; }

    /// <summary>
    /// Gets the items.
    /// </summary>
    /// <value>
    /// The items.
    /// </value>
    [IgnoreDataMember]
    public List<Alert> Items
    {
      get
      {
        return ItemDictionary["alertcontact"];
      }
    }
  }
}
