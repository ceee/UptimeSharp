using System;

namespace UptimeSharp
{
  public interface IUptimeClient
  {
    /// <summary>
    /// Accessor for the UptimeRebot API key
    /// see: http://www.uptimerobot.com/api
    /// </summary>
    /// <value>
    /// The API key.
    /// </value>
    string ApiKey { get; set; }

    /// <summary>
    /// Action which is executed before every request
    /// </summary>
    /// <value>
    /// The pre request callback.
    /// </value>
    Action<string> PreRequest { get; set; }
  }
}
