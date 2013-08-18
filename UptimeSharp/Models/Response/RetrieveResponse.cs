using System.Collections.Generic;
using System.Runtime.Serialization;

namespace UptimeSharp.Models
{
  /// <summary>
  /// Monitor Response
  /// </summary>
  [DataContract]
  internal class RetrieveResponse : ResponseBase
  {
    /// <summary>
    /// Gets or sets the item dictionary.
    /// The list is 2 layers deep, so this one is necessary :-/
    /// </summary>
    /// <value>
    /// The item dictionary.
    /// </value>
    [DataMember(Name = "monitors")]
    public Dictionary<string, List<Monitor>> ItemDictionary { get; set; }

    /// <summary>
    /// Gets the items.
    /// </summary>
    /// <value>
    /// The items.
    /// </value>
    [IgnoreDataMember]
    public List<Monitor> Items
    {
      get
      {
        return ItemDictionary["monitor"]; 
      }
    }
  }
}
