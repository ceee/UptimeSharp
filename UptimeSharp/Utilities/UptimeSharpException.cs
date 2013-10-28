using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace UptimeSharp
{
  /// <summary>
  /// custom UptimeRobot API Exceptions
  /// </summary>
  public class UptimeSharpException : Exception
  {
    /// <summary>
    /// Gets or sets the UptimeRobot error code.
    /// </summary>
    /// <value>
    /// The pocket error code.
    /// </value>
    public int? ErrorCode { get; set; }

    /// <summary>
    /// Gets or sets the UptimeRobot error.
    /// </summary>
    /// <value>
    /// The pocket error.
    /// </value>
    public string Error { get; set; }


    /// <summary>
    /// Initializes a new instance of the <see cref="UptimeSharpException"/> class.
    /// </summary>
    public UptimeSharpException()
      : base() { }


    /// <summary>
    /// Initializes a new instance of the <see cref="UptimeSharpException"/> class.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public UptimeSharpException(string message)
      : base(message) { }


    /// <summary>
    /// Initializes a new instance of the <see cref="UptimeSharpException" /> class.
    /// </summary>
    /// <param name="message">The message.</param>
    /// <param name="args">The arguments.</param>
    public UptimeSharpException(string message, params object[] args)
      : base(string.Format(message, args)) { }


    /// <summary>
    /// Initializes a new instance of the <see cref="UptimeSharpException"/> class.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
    public UptimeSharpException(string message, Exception innerException)
      : base(message, innerException) { }


    /// <summary>
    /// Initializes a new instance of the <see cref="UptimeSharpException"/> class.
    /// </summary>
    /// <param name="message">The message.</param>
    /// <param name="innerException">The inner exception.</param>
    /// <param name="args">The arguments.</param>
    public UptimeSharpException(string message, Exception innerException, params object[] args)
      : base(string.Format(message, args), innerException) { }
  }
}
