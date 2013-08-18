using System.Runtime.Serialization;

namespace UptimeSharp.Models
{
  /// <summary>
  /// The Alert Model
  /// </summary>
  [DataContract]
  public class Alert
  {
    /// <summary>
    /// Gets or sets the ID.
    /// </summary>
    /// <value>
    /// The ID.
    /// </value>
    [DataMember(Name = "id")]
    public int? ID { get; set; }

    /// <summary>
    /// Gets or sets the alert type.
    /// </summary>
    /// <value>
    /// The name.
    /// </value>
    [DataMember(Name = "type")]
    public Type Type { get; set; }

    /// <summary>
    /// Gets or sets the alert value.
    /// </summary>
    /// <value>
    /// The value - Phone Number / E-Mail / Account
    /// </value>
    [DataMember(Name = "value")]
    public string Value { get; set; }
  }



  /// <summary>
  /// The type of the alert contact notified.
  /// </summary>
  public enum Type
  {
    /// <summary>
    /// SMS
    /// </summary>
    SMS = 1,
    /// <summary>
    /// E-Mail
    /// </summary>
    EMail = 2,
    /// <summary>
    /// Twitter DM
    /// </summary>
    Twitter = 3,
    /// <summary>
    /// Boxcar
    /// </summary>
    Boxcar = 4
  }
}
