
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
    [DataMember(Name = "custom_uptime_ratios")]
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
    [DataMember(Name = "alert_contacts")]
    public int? ShowAlerts { get; set; }

    /// <summary>
    /// Defines if the resonse times for each monitor will be returned
    /// </summary>
    /// <value>
    /// The alert contacts bool.
    /// </value>
    [DataMember(Name = "response_times")]
    public int? ShowResponseTimes { get; set; }

    /// <summary>
    /// Gets or sets the response time interval in minutes.
    /// </summary>
    /// <value>
    /// The response time interval.
    /// </value>
    [DataMember(Name = "response_times_average")]
    public int? ResponseTimeAverage { get; set; }

    /// <summary>
    /// optional (the number of logs to be returned (descending order). If empty, all logs are returned.
    /// </summary>
    [DataMember(Name = "logs_limit")]
    public int? LogsLimit { get; set; }

    /// <summary>
    /// optional (the number of response time logs to be returned (descending order). 
    /// If empty, last 24 hours of logs are returned (if response_times_start_date 
    /// and response_times_end_date are not used).
    /// </summary>
    [DataMember(Name = "response_times_limit")]
    public int? ResponseTimesLimit { get; set; }

    /// <summary>
    /// Converts an object to a list of HTTP Get parameters.
    /// </summary>
    /// <returns></returns>
    public Dictionary<string, string> Convert()
    {
      Dictionary<string, string> parameters = base.Convert();

      if (ShowLog.HasValue)
      {
        if (parameters.ContainsKey("alert_contacts"))
        {
          parameters["alert_contacts"] = parameters["logs"];
        }
        else
        {
          parameters.Add("alert_contacts", parameters["logs"]);
        }

        if (parameters.ContainsKey("timezone"))
        {
          parameters["timezone"] = parameters["logs"];
        }
        else
        {
          parameters.Add("timezone", parameters["logs"]);
        }
      }

      return parameters;
    }
  }
}
