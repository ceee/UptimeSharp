using RestSharp;
using System;
using System.Collections.Generic;

namespace UptimeSharp.Models
{
  /// <summary>
  /// All parameters which can be passed for monitor modifications
  /// </summary>
  public class MonitorParameters
  {
    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    /// <value>
    /// The name.
    /// </value>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the URI.
    /// </summary>
    /// <value>
    /// The URI.
    /// </value>
    public string Uri { get; set; }

    /// <summary>
    /// Gets or sets the port.
    /// Only for port monitoring.
    /// </summary>
    /// <value>
    /// The port.
    /// </value>
    public int? Port { get; set; }

    /// <summary>
    /// Gets or sets the HTTP password.
    /// </summary>
    /// <value>
    /// The HTTP password.
    /// </value>
    public string HTTPPassword { get; set; }

    /// <summary>
    /// Gets or sets the HTTP username.
    /// </summary>
    /// <value>
    /// The HTTP username.
    /// </value>
    public string HTTPUsername { get; set; }

    /// <summary>
    /// Gets or sets the type of the keyword.
    /// </summary>
    /// <value>
    /// The type of the keyword.
    /// </value>
    public KeywordType KeywordType { get; set; }

    /// <summary>
    /// Gets or sets the keyword value.
    /// </summary>
    /// <value>
    /// The keyword value.
    /// </value>
    public string KeywordValue { get; set; }

    /// <summary>
    /// Gets or sets the type.
    /// </summary>
    /// <value>
    /// The type.
    /// </value>
    public Type Type { get; set; }

    /// <summary>
    /// Gets or sets the subtype.
    /// </summary>
    /// <value>
    /// The subtype.
    /// </value>
    public Subtype Subtype { get; set; }

    /// <summary>
    /// Gets or sets the alerts.
    /// </summary>
    /// <value>
    /// The alert contacts.
    /// </value>
    public int[] Alerts { get; set; }


    /// <summary>
    /// Converts this instance to a parameter list.
    /// </summary>
    /// <returns>Parameter list</returns>
    public List<Parameter> Convert()
    {
      List<Parameter> parameters = new List<Parameter>();

      if(!string.IsNullOrEmpty(Name))
      {
        parameters.Add(Utilities.CreateParam("monitorFriendlyName", Name)); 
      }

      if(!string.IsNullOrEmpty(Uri))
      {
        parameters.Add(Utilities.CreateParam("monitorURL", Uri)); 
      }

      if(Type != null && (int)Type != 0)
      {
        parameters.Add(Utilities.CreateParam("monitorType", (int)Type)); 
      }

      // special params for port listener
      if (Type == Type.Port && Subtype != Subtype.Unknown && Port.HasValue)
      {
        parameters.Add(Utilities.CreateParam("monitorSubType", (int)Subtype));

        if (Subtype == Subtype.Custom)
        {
          parameters.Add(Utilities.CreateParam("monitorPort", Port));
        }
      }

      // keyword listener
      if (Type == Type.Keyword && !string.IsNullOrEmpty(KeywordValue))
      {
        parameters.Add(Utilities.CreateParam("monitorKeywordType", (int)KeywordType));
        parameters.Add(Utilities.CreateParam("monitorKeywordValue", KeywordValue));
      }

      // HTTP basic auth credentials
      if (!string.IsNullOrEmpty(HTTPUsername))
      {
        parameters.Add(Utilities.CreateParam("monitorHTTPUsername", HTTPUsername));
      }
      if (!string.IsNullOrEmpty(HTTPPassword))
      {
        parameters.Add(Utilities.CreateParam("monitorHTTPPassword", HTTPPassword));
      }

      // alert notifications
      if (Alerts != null && Alerts.Length > 0)
      {
        parameters.Add(Utilities.CreateParam("monitorAlertContacts", string.Join("-", Alerts)));
      }

      return parameters;
    }
  }
}
