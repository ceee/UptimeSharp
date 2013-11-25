using Newtonsoft.Json;
using PropertyChanged;
using System;
using System.Collections.Generic;

namespace UptimeSharp.Models
{
  /// <summary>
  /// The Monitor Model implementation
  /// </summary>
  [JsonObject]
  [ImplementPropertyChanged]
  public class Monitor
  {
    /// <summary>
    /// Gets or sets the ID.
    /// </summary>
    /// <value>
    /// The ID.
    /// </value>
    [JsonProperty("id")]
    public int ID { get; set; }

    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    /// <value>
    /// The name.
    /// </value>
    [JsonProperty("friendlyname")]
    public string Name { get; set; }

    /// <summary>
    /// Gets the URI.
    /// </summary>
    /// <value>
    /// The URI.
    /// </value>
    [JsonIgnore]
    public Uri Uri { get; set; }

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
    [JsonProperty("alltimeuptimeratio")]
    public float Uptime { get; set; }

    /// <summary>
    /// Gets or sets the uptime custom.
    /// </summary>
    /// <value>
    /// The uptime ratio of the monitor for the given periods
    /// </value>
    [JsonProperty("customuptimeratio")]
    public float? UptimeCustom { get; set; }

    /// <summary>
    /// Gets or sets the HTTP password.
    /// </summary>
    /// <value>
    /// The HTTP password.
    /// </value>
    [JsonProperty("httppassword")]
    public string HTTPPassword { get; set; }

    /// <summary>
    /// Gets or sets the HTTP username.
    /// </summary>
    /// <value>
    /// The HTTP username.
    /// </value>
    [JsonProperty("httpusername")]
    public string HTTPUsername { get; set; }

    /// <summary>
    /// Gets or sets the type of the keyword.
    /// </summary>
    /// <value>
    /// The type of the keyword.
    /// </value>
    [JsonProperty("keywordtype")]
    public KeywordType KeywordType { get; set; }

    /// <summary>
    /// Gets or sets the keyword value.
    /// </summary>
    /// <value>
    /// The keyword value.
    /// </value>
    [JsonProperty("keywordvalue")]
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
    [JsonProperty("subtype")]
    public Subtype Subtype { get; set; }

    /// <summary>
    /// Gets or sets the alerts.
    /// </summary>
    /// <value>
    /// The alert contacts.
    /// </value>
    [JsonProperty("alertcontact")]
    public List<Alert> Alerts { get; set; }

    /// <summary>
    /// Gets or sets the log.
    /// </summary>
    /// <value>
    /// The log with dates and associated alert contacts.
    /// </value>
    [JsonProperty("log")]
    public List<Log> Log { get; set; }
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
