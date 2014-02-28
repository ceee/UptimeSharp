
using System.Collections.Generic;
using System.Runtime.Serialization;
namespace UptimeSharp.Models
{
  /// <summary>
  /// All parameters which can be passed for monitor retrieval
  /// </summary>
  [DataContract]
  internal class RetrieveParameters : Parameters
  {
    /// <summary>
    /// List of monitor ids
    /// </summary>
    /// <value>
    /// The monitors.
    /// </value>
    [DataMember(Name = "monitors")]
    public string[] Monitors { get; set; }

    /// <summary>
    /// Defines the number of days to calculate the uptime ratio
    /// </summary>
    /// <value>
    /// The custom uptime ratio.
    /// </value>
    [DataMember(Name = "customUptimeRatio")]
    public float[] CustomUptimeRatio { get; set; }

    /// <summary>
    /// Defines if the logs of each monitor will be returned
    /// </summary>
    /// <value>
    /// The log bool.
    /// </value>
    [DataMember(Name = "logs")]
    public bool? ShowLog { get; set; }

    /// <summary>
    /// Defines if the alert contacts set for the monitor to be returned
    /// </summary>
    /// <value>
    /// The alert contacts bool.
    /// </value>
    [DataMember(Name = "showMonitorAlertContacts")]
    public bool? ShowAlerts { get; set; }

    /// <summary>
    /// Defines if the resonse times for each monitor will be returned
    /// </summary>
    /// <value>
    /// The alert contacts bool.
    /// </value>
    [DataMember(Name = "responseTimes")]
    public bool? ShowResponseTimes { get; set; }

    /// <summary>
    /// Gets or sets the response time interval in minutes.
    /// </summary>
    /// <value>
    /// The response time interval.
    /// </value>
    [DataMember(Name = "responseTimesAverage")]
    public int? ResponseTimeInterval { get; set; }

    /// <summary>
    /// Converts an object to a list of HTTP Get parameters.
    /// </summary>
    /// <returns></returns>
    public Dictionary<string, string> Convert()
    {
      Dictionary<string, string> parameters = base.Convert();

      if (ShowLog.HasValue)
      {
        parameters.Add("alertContacts", parameters["logs"]);
        parameters.Add("showTimezone", parameters["logs"]);
      }

      return parameters;
    }
  }
}
