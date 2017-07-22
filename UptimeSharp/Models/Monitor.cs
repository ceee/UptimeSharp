using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;

namespace UptimeSharp.Models
{
  /// <summary>
  /// The Monitor Model implementation
  /// </summary>
  [JsonObject]
  [DebuggerDisplay("Name = {Name}, Target = {Target}")]
  public class Monitor
  {
    /// <summary>
    /// Gets or sets the ID.
    /// </summary>
    /// <value>
    /// The ID.
    /// </value>
    [JsonProperty("id")]
    public string ID { get; set; }

    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    /// <value>
    /// The name.
    /// </value>
    [JsonProperty("friendly_name")]
    public string Name { get; set; }

    /// <summary>
    /// Gets the target URI/IP.
    /// </summary>
    /// <value>
    /// The target URI/IP.
    /// </value>
    [JsonProperty("url")]
    public string Target { get; set; }

    /// <summary>
    /// Gets or sets the port.
    /// Only for port monitoring.
    /// </summary>
    /// <value>
    /// The port.
    /// </value>
    [JsonProperty("port")]
    public int? Port { get; set; }

    /// <summary>
    /// Gets or sets the uptime.
    /// </summary>
    /// <value>
    /// Uptime ratio of the monitor calculated since the monitor is created.
    /// </value>
    [JsonProperty("all_time_uptime_ratio")]
    public float Uptime { get; set; }

    /*
    /// <summary>
    /// Gets or sets the uptime custom.
    /// </summary>
    /// <value>
    /// The uptime ratio of the monitor for the given periods
    /// </value>
    [JsonProperty("customuptimeratio")]
    public float? UptimeCustom { get; set; }
    */

    /// <summary>
    /// Gets or sets the HTTP password.
    /// </summary>
    /// <value>
    /// The HTTP password.
    /// </value>
    [JsonProperty("http_password")]
    public string HTTPPassword { get; set; }

    /// <summary>
    /// Gets or sets the HTTP username.
    /// </summary>
    /// <value>
    /// The HTTP username.
    /// </value>
    [JsonProperty("http_username")]
    public string HTTPUsername { get; set; }

    /// <summary>
    /// Gets or sets the type of the keyword.
    /// </summary>
    /// <value>
    /// The type of the keyword.
    /// </value>
    [JsonProperty("keyword_type")]
    public KeywordType KeywordType { get; set; }

    /// <summary>
    /// Gets or sets the keyword value.
    /// </summary>
    /// <value>
    /// The keyword value.
    /// </value>
    [JsonProperty("keyword_value")]
    public string KeywordValue { get; set; }

    /// <summary>
    /// Gets or sets the status.
    /// </summary>
    /// <value>
    /// The status.
    /// </value>
    [JsonProperty("status")]
    public Status Status { get; set; }

    /// <summary>
    /// Gets or sets the type.
    /// </summary>
    /// <value>
    /// The type.
    /// </value>
    [JsonProperty("type")]
    public Type Type { get; set; }

    /// <summary>
    /// Gets or sets the subtype.
    /// </summary>
    /// <value>
    /// The subtype.
    /// </value>
    [JsonProperty("sub_type")]
    public Subtype Subtype { get; set; }

    /// <summary>
    /// Gets or sets the alerts.
    /// </summary>
    /// <value>
    /// The alert contacts.
    /// </value>
    [JsonProperty("alert_contacts")]
    public List<Alert> Alerts { get; set; }

    /// <summary>
    /// Gets or sets the log.
    /// </summary>
    /// <value>
    /// The log with dates and associated alert contacts.
    /// </value>
    [JsonProperty("logs")]
    public List<Log> Log { get; set; }

    /// <summary>
    /// Gets or sets the response times.
    /// </summary>
    /// <value>
    /// The response times.
    /// </value>
    [JsonProperty("response_times")]
    public List<ResponseTime> ResponseTimes { get; set; }
  }



  /// <summary>
  /// The status of the monitor
  /// </summary>
  public enum Status
  {
    /// <summary>
    /// Paused
    /// </summary>
    Pause = 0,
    /// <summary>
    /// Not checked yet
    /// </summary>
    NotChecked = 1,
    /// <summary>
    /// Up
    /// </summary>
    Up = 2,
    /// <summary>
    /// Seems down
    /// </summary>
    SeemsDown = 8,
    /// <summary>
    /// Down
    /// </summary>
    Down = 9
  }

  /// <summary>
  /// The type of the monitor
  /// </summary>
  public enum Type
  {
    /// <summary>
    /// HTTP
    /// </summary>
    HTTP = 1,
    /// <summary>
    /// Keyword
    /// </summary>
    Keyword = 2,
    /// <summary>
    /// Ping
    /// </summary>
    Ping = 3,
    /// <summary>
    /// Port
    /// </summary>
    Port = 4
  }

  /// <summary>
  /// Shows which pre-defined port/service is monitored or if a custom port is monitored.
  /// </summary>
  public enum Subtype
  {
    /// <summary>
    /// Unknown
    /// </summary>
    Unknown,
    /// <summary>
    /// HTTP
    /// </summary>
    HTTP = 1,
    /// <summary>
    /// HTTPS
    /// </summary>
    HTTPS = 2,
    /// <summary>
    /// FTP
    /// </summary>
    FTP = 3,
    /// <summary>
    /// SMTP
    /// </summary>
    SMTP = 4,
    /// <summary>
    /// POP3
    /// </summary>
    POP3 = 5,
    /// <summary>
    /// IMAP
    /// </summary>
    IMAP = 6,
    /// <summary>
    /// Custom Port
    /// </summary>
    Custom = 99
  }

  /// <summary>
  /// Shows if the monitor will be flagged as down when the keyword exists or not exists
  /// </summary>
  public enum KeywordType
  {
    /// <summary>
    /// Unknown
    /// </summary>
    Unknown,
    /// <summary>
    /// Exists
    /// </summary>
    Exists = 1,
    /// <summary>
    /// Exists not
    /// </summary>
    NotExists = 2
  }
}
