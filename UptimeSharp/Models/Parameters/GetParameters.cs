using RestSharp;
using System;
using System.Collections.Generic;

namespace UptimeSharp.Models
{
  /// <summary>
  /// All parameters which can be passed for monitor retrieval
  /// </summary>
  public class GetParameters
  {
    /// <summary>
    /// List of monitor ids
    /// </summary>
    /// <value>
    /// The monitors.
    /// </value>
    public int[] Monitors { get; set; }

    /// <summary>
    /// Defines the number of days to calculate the uptime ratio
    /// </summary>
    /// <value>
    /// The custom uptime ratio.
    /// </value>
    public float[] CustomUptimeRatio { get; set; }

    /// <summary>
    /// Defines if the logs of each monitor will be returned
    /// </summary>
    /// <value>
    /// The log bool.
    /// </value>
    public bool? ShowLogs { get; set; }

    /// <summary>
    /// Defines if the notified alert contacts of each notification will be returned
    /// </summary>
    /// <value>
    /// The alert contacts bool.
    /// </value>
    public bool? ShowAlertContacts { get; set; }

    /// <summary>
    /// Defines if the alert contacts set for the monitor to be returned
    /// </summary>
    /// <value>
    /// The alert contacts bool.
    /// </value>
    public bool? ShowMonitorAlertContacts { get; set; }

    /// <summary>
    /// Defines if the user's timezone should be returned
    /// </summary>
    /// <value>
    /// The alert contacts bool.
    /// </value>
    public bool? ShowTimezone { get; set; }


    /// <summary>
    /// Converts this instance to a parameter list.
    /// </summary>
    /// <returns>Parameter list</returns>
    public List<Parameter> Convert()
    {
      List<Parameter> parameters = new List<Parameter>();

      if (Monitors != null)
      {
        parameters.Add(Utilities.CreateParam("monitors", String.Join("-", Monitors)));
      }
      if (CustomUptimeRatio != null)
      {
        parameters.Add(Utilities.CreateParam("customUptimeRatio", String.Join("-", CustomUptimeRatio)));
      }
      if (ShowLogs.HasValue)
      {
        parameters.Add(Utilities.CreateParam("logs", ShowLogs));
      }
      if (ShowAlertContacts.HasValue)
      {
        parameters.Add(Utilities.CreateParam("alertContacts", ShowAlertContacts));
      }
      if (ShowMonitorAlertContacts.HasValue)
      {
        parameters.Add(Utilities.CreateParam("showMonitorAlertContacts", ShowMonitorAlertContacts));
      }
      if (ShowTimezone.HasValue)
      {
        parameters.Add(Utilities.CreateParam("showTimezone", ShowTimezone));
      }

      return parameters;
    }
  }
}
