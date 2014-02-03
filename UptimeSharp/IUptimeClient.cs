using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UptimeSharp.Models;

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


    #region Monitors
    /// <summary>
    /// Retrieves specified monitors from UptimeRobot
    /// </summary>
    /// <param name="monitorIDs">The monitor IDs.</param>
    /// <param name="customUptimeRatio">The custom uptime ratio.</param>
    /// <param name="showLog">if set to <c>true</c> [show log].</param>
    /// <param name="showAlerts">if set to <c>true</c> [show alerts].</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    /// Monitor List
    /// </returns>
    /// <exception cref="UptimeSharpException"></exception>
    Task<List<Models.Monitor>> GetMonitors(
      string[] monitorIDs = null,
      float[] customUptimeRatio = null,
      bool showLog = false,
      bool showAlerts = true,
      CancellationToken cancellationToken = default(CancellationToken));

    /// <summary>
    /// Retrieves a monitor from UptimeRobot
    /// </summary>
    /// <param name="monitorId">a specific monitor ID</param>
    /// <param name="customUptimeRatio">The custom uptime ratio.</param>
    /// <param name="showLog">if set to <c>true</c> [show log].</param>
    /// <param name="showAlerts">if set to <c>true</c> [show alerts].</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    /// The Monitor
    /// </returns>
    /// <exception cref="UptimeSharpException"></exception>
    Task<Models.Monitor> GetMonitor(
      string monitorId,
      float[] customUptimeRatio = null,
      bool showLog = false,
      bool showAlerts = true,
      CancellationToken cancellationToken = default(CancellationToken));

    /// <summary>
    /// Deletes a monitor
    /// </summary>
    /// <param name="monitorId">a specific monitor ID</param>
    /// <returns>
    /// Success state
    /// </returns>
    /// <exception cref="UptimeSharpException"></exception>
    Task<bool> DeleteMonitor(string monitorId, CancellationToken cancellationToken = default(CancellationToken));

    /// <summary>
    /// Deletes a monitor
    /// </summary>
    /// <param name="monitor">The monitor.</param>
    /// <returns>
    /// Success state
    /// </returns>
    /// <exception cref="UptimeSharpException"></exception>
    Task<bool> DeleteMonitor(Models.Monitor monitor, CancellationToken cancellationToken = default(CancellationToken));

    /// <summary>
    /// Creates a monitor.
    /// </summary>
    /// <param name="name">The name of the new monitor.</param>
    /// <param name="target">The URI or IP to watch.</param>
    /// <param name="type">The type of the monitor.</param>
    /// <param name="subtype">The subtype of the monitor (if port).</param>
    /// <param name="port">The port (only for Subtype.Custom).</param>
    /// <param name="keywordValue">The keyword value.</param>
    /// <param name="keywordType">Type of the keyword.</param>
    /// <param name="alerts">An ID list of existing alerts to notify.</param>
    /// <param name="HTTPUsername">The HTTP username.</param>
    /// <param name="HTTPPassword">The HTTP password.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    /// New Monitor (without details)
    /// </returns>
    /// <exception cref="UptimeSharpException"></exception>
    Task<Models.Monitor> AddMonitor(
      string name,
      string target,
      Models.Type type = Models.Type.HTTP,
      Subtype subtype = Subtype.Unknown,
      int? port = null,
      string keywordValue = null,
      KeywordType keywordType = KeywordType.Unknown,
      string[] alerts = null,
      string HTTPUsername = null,
      string HTTPPassword = null,
      CancellationToken cancellationToken = default(CancellationToken));

    /// <summary>
    /// Edits a monitor.
    /// </summary>
    /// <param name="monitor">The monitor.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    /// Success state
    /// </returns>
    /// <exception cref="UptimeSharpException"></exception>
    Task<bool> ModifyMonitor(Models.Monitor monitor, CancellationToken cancellationToken = default(CancellationToken));
    #endregion


    #region Alerts
    /// <summary>
    /// Retrieves alerts from UptimeRobot
    /// </summary>
    /// <param name="alertIDs">Retrieve specified alert contacts by supplying IDs for them.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    /// <exception cref="UptimeSharpException"></exception>
    Task<List<Alert>> GetAlerts(string[] alertIDs = null, CancellationToken cancellationToken = default(CancellationToken));

    /// <summary>
    /// Retrieves an alert from UptimeRobot
    /// </summary>
    /// <param name="alertID">The alertID.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    /// <exception cref="UptimeSharpException"></exception>
    Task<Alert> GetAlert(string alertID, CancellationToken cancellationToken = default(CancellationToken));

    /// <summary>
    /// Adds an alert.
    /// mobile/SMS alert contacts are not supported yet, see: http://www.uptimerobot.com/api.asp#methods
    /// </summary>
    /// <param name="type">The type.</param>
    /// <param name="value">The value.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    /// <exception cref="UptimeSharpException">AlertType.SMS and AlertType.Twitter are not supported by the UptimeRobot API</exception>
    Task<Alert> AddAlert(AlertType type, string value, CancellationToken cancellationToken = default(CancellationToken));

    /// <summary>
    /// Adds an alert.
    /// mobile/SMS alert contacts are not supported yet, see: http://www.uptimerobot.com/api.asp#methods
    /// </summary>
    /// <param name="alert">The alert.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    /// <exception cref="UptimeSharpException"></exception>
    Task<Alert> AddAlert(Alert alert, CancellationToken cancellationToken = default(CancellationToken));

    /// <summary>
    /// Removes an alert.
    /// </summary>
    /// <param name="alertID">The alert ID.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    /// <exception cref="UptimeSharpException"></exception>
    Task<bool> DeleteAlert(string alertID, CancellationToken cancellationToken = default(CancellationToken));

    /// <summary>
    /// Removes an alert.
    /// </summary>
    /// <param name="alert">The alert.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    /// <exception cref="UptimeSharpException"></exception>
    Task<bool> DeleteAlert(Alert alert, CancellationToken cancellationToken = default(CancellationToken));
    #endregion


    #region Account
    /// <summary>
    /// Determines whether a e-mail is available.
    /// </summary>
    /// <param name="email">The email.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentNullException">Please provide an e-mail address</exception>
    /// <exception cref="System.ArgumentException">Please provide a valid e-mail address</exception>
    Task<bool> IsEmailAvailable(string email, CancellationToken cancellationToken = default(CancellationToken));

    /// <summary>
    /// Registers the account.
    /// </summary>
    /// <param name="fullName">The full name (min. 3 chars).</param>
    /// <param name="email">The email address.</param>
    /// <param name="password">The password (min. 6 chars).</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentNullException">Please provide all parameters (username, email and password)</exception>
    /// <exception cref="System.ArgumentException">
    /// Please provide a valid e-mail address
    /// or
    /// Name must be at least 3 characters
    /// or
    /// Username must be at least 6 characters
    /// </exception>
    Task<bool> RegisterAccount(string fullName, string email, string password, CancellationToken cancellationToken = default(CancellationToken));
    #endregion
  }
}
