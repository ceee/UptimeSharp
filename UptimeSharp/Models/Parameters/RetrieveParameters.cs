
namespace UptimeSharp.Models
{
  /// <summary>
  /// All parameters which can be passed for monitor retrieval
  /// </summary>
  internal class RetrieveParameters
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
    public bool? ShowLog { get; set; }

    /// <summary>
    /// Defines if the alert contacts set for the monitor to be returned
    /// </summary>
    /// <value>
    /// The alert contacts bool.
    /// </value>
    public bool? ShowAlerts { get; set; }
  }
}
