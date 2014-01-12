﻿using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UptimeSharp.Models;

namespace UptimeSharp
{
  /// <summary>
  /// UptimeClient
  /// </summary>
  public partial class UptimeClient
  {
    /// <summary>
    /// Retrieves alerts from UptimeRobot
    /// </summary>
    /// <param name="alertIDs">Retrieve specified alert contacts by supplying IDs for them.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    public async Task<List<Alert>> GetAlerts(string[] alertIDs = null, CancellationToken cancellationToken = default(CancellationToken))
    {
      AlertResponse response = await Request<AlertResponse>("getAlertContacts", cancellationToken, new Dictionary<string, string>()
      {
        { "alertcontacts", alertIDs != null ? string.Join("-", alertIDs) : null }
      });

      return response.Items;
    }


    /// <summary>
    /// Retrieves an alert from UptimeRobot
    /// </summary>
    /// <param name="alertID">The alertID.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    public async Task<Alert> GetAlert(string alertID, CancellationToken cancellationToken = default(CancellationToken))
    {
      List<Alert> alerts = await GetAlerts(new string[] { alertID }, cancellationToken);

      return alerts != null && alerts.Count > 0 ? alerts[0] : null;
    }


    /// <summary>
    /// Adds an alert.
    /// mobile/SMS alert contacts are not supported yet, see: http://www.uptimerobot.com/api.asp#methods
    /// </summary>
    /// <param name="type">The type.</param>
    /// <param name="value">The value.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    /// <exception cref="UptimeSharpException">AlertType.SMS and AlertType.Twitter are not supported by the UptimeRobot API</exception>
    public async Task<bool> AddAlert(AlertType type, string value, CancellationToken cancellationToken = default(CancellationToken))
    {
      if (type == AlertType.SMS || type == AlertType.Twitter)
      {
        throw new UptimeSharpException("AlertType.SMS and AlertType.Twitter are not supported by the UptimeRobot API");
      }

      DefaultResponse response = await Request<DefaultResponse>("newAlertContact", cancellationToken, new Dictionary<string, string>()
      {
        { "alertContactType", ((int)type).ToString() },
        { "alertContactValue", value }
      });

      return response.Status;
    }


    /// <summary>
    /// Adds an alert.
    /// mobile/SMS alert contacts are not supported yet, see: http://www.uptimerobot.com/api.asp#methods
    /// </summary>
    /// <param name="alert">The alert.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    public async Task<bool> AddAlert(Alert alert, CancellationToken cancellationToken = default(CancellationToken))
    {
      return await AddAlert(alert.Type, alert.Value, cancellationToken);
    }


    /// <summary>
    /// Removes an alert.
    /// </summary>
    /// <param name="alertID">The alert ID.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    /// <exception cref="UptimeSharpException">Can't delete main alert</exception>
    public async Task<bool> DeleteAlert(string alertID, CancellationToken cancellationToken = default(CancellationToken))
    {
      if (alertID.StartsWith("0"))
      {
        throw new UptimeSharpException("Can't delete main alert");
      }

      DefaultResponse response = await Request<DefaultResponse>("deleteAlertContact", cancellationToken, new Dictionary<string, string>()
      {
        { "alertContactID", alertID }
      });

      return response.Status;
    }


    /// <summary>
    /// Removes an alert.
    /// </summary>
    /// <param name="alert">The alert.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    public async Task<bool> DeleteAlert(Alert alert, CancellationToken cancellationToken = default(CancellationToken))
    {
      return await DeleteAlert(alert.ID, cancellationToken);
    }
  }
}