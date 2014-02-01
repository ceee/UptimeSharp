using System;
using System.Collections.Generic;
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
    public string Target { get; set; }

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

    /// <summary>
    /// Converts an object to a list of HTTP Get parameters.
    /// </summary>
    /// <returns></returns>
    public Dictionary<string, string> Convert()
    {
      Dictionary<string, string> parameters = new Dictionary<string, string>();

      parameters.Add("monitorFriendlyName", Name);
      parameters.Add("monitorURL", Target);

      if ((int)Type != 0)
      {
        parameters.Add("monitorType", ((int)Type).ToString());
      }

      // special params for port listener
      if (Type == Type.Port && Subtype != Subtype.Unknown)
      {
        parameters.Add("monitorSubType", ((int)Subtype).ToString());

        if (Subtype == Subtype.Custom && Port.HasValue)
        {
          parameters.Add("monitorPort", ((int)Port).ToString());
        }
      }

      // keyword listener
      if (Type == Type.Keyword && !String.IsNullOrEmpty(KeywordValue))
      {
        parameters.Add("monitorKeywordType", ((int)KeywordType).ToString());
        parameters.Add("monitorKeywordValue", KeywordValue);
      }

      // HTTP basic auth credentials
      if (!String.IsNullOrEmpty(HTTPUsername))
      {
        parameters.Add("monitorHTTPUsername", HTTPUsername);
        parameters.Add("monitorHTTPPassword", HTTPPassword);
      }

      // alert notifications
      if (Alerts != null && Alerts.Length > 0)
      {
        parameters.Add("monitorAlertContacts", String.Join("-", Alerts));
      }

      return parameters;
    }
  }
}
