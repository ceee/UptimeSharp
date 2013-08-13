﻿using System;
using System.Runtime.Serialization;

namespace UptimeSharp
{
  /// <summary>
  /// custom UptimeRobot API Exceptions
  /// </summary>
  public class APIException : Exception
  {
    /// <summary>
    /// Gets or sets the UptimeRobot error code.
    /// </summary>
    /// <value>
    /// The pocket error code.
    /// </value>
    public int? UptimeErrorCode { get; set; }

    /// <summary>
    /// Gets or sets the UptimeRobot error.
    /// </summary>
    /// <value>
    /// The pocket error.
    /// </value>
    public string UptimeError { get; set; }


    /// <summary>
    /// Initializes a new instance of the <see cref="APIException"/> class.
    /// </summary>
    public APIException()
      : base() { }


    /// <summary>
    /// Initializes a new instance of the <see cref="APIException"/> class.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public APIException(string message)
      : base(message) { }


    /// <summary>
    /// Initializes a new instance of the <see cref="APIException"/> class.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
    public APIException(string message, Exception innerException)
      : base(message, innerException) { }


    /// <summary>
    /// Initializes a new instance of the <see cref="APIException"/> class.
    /// </summary>
    /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
    /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
    protected APIException(SerializationInfo info, StreamingContext context)
      : base(info, context) { }
  }
}
