using System.Runtime.Serialization;

namespace UptimeSharp.Models
{
  /// <summary>
  /// All parameters which can be passed for monitor modifications
  /// </summary>
  [DataContract]
  internal class MonitorParameters : Parameters
  {
    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    /// <value>
    /// The name.
    /// </value>
    [DataMember(Name = "monitorFriendlyName")]
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the URI.
    /// </summary>
    /// <value>
    /// The URI.
    /// </value>
    [DataMember(Name = "monitorURL")]
    public string Uri { get; set; }

    /// <summary>
    /// Gets or sets the port.
    /// Only for port monitoring.
    /// </summary>
    /// <value>
    /// The port.
    /// </value>
    [DataMember(Name = "monitorPort")]
    public int? Port { get; set; }

    /// <summary>
    /// Gets or sets the HTTP password.
    /// </summary>
    /// <value>
    /// The HTTP password.
    /// </value>
    [DataMember(Name = "monitorHTTPPassword")]
    public string HTTPPassword { get; set; }

    /// <summary>
    /// Gets or sets the HTTP username.
    /// </summary>
    /// <value>
    /// The HTTP username.
    /// </value>
    [DataMember(Name = "monitorHTTPUsername")]
    public string HTTPUsername { get; set; }

    /// <summary>
    /// Gets or sets the type of the keyword.
    /// </summary>
    /// <value>
    /// The type of the keyword.
    /// </value>
    [DataMember(Name = "monitorKeywordType")]
    public KeywordType KeywordType { get; set; }

    /// <summary>
    /// Gets or sets the keyword value.
    /// </summary>
    /// <value>
    /// The keyword value.
    /// </value>
    [DataMember(Name = "monitorKeywordValue")]
    public string KeywordValue { get; set; }

    /// <summary>
    /// Gets or sets the type.
    /// </summary>
    /// <value>
    /// The type.
    /// </value>
    [DataMember(Name = "monitorType")]
    public Type Type { get; set; }

    /// <summary>
    /// Gets or sets the subtype.
    /// </summary>
    /// <value>
    /// The subtype.
    /// </value>
    [DataMember(Name = "monitorSubType")]
    public Subtype Subtype { get; set; }

    /// <summary>
    /// Gets or sets the alerts.
    /// </summary>
    /// <value>
    /// The alert contacts.
    /// </value>
    [DataMember(Name = "monitorAlertContacts")]
    public string[] Alerts { get; set; }
  }
}
