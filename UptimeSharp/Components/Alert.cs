﻿using RestSharp;
using System;
using System.Collections.Generic;
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
    /// <returns></returns>
    public List<Alert> RetrieveAlerts(int[] alertIDs = null)
    {
      Parameter alerts = Parameter("alertcontacts", alertIDs != null ? string.Join("-", alertIDs) : null);

      return Get<AlertResponse>("getAlertContacts", alerts).Items;
    }


    /// <summary>
    /// Adds an alert.
    /// mobile/SMS alert contacts are not supported yet, see: http://www.uptimerobot.com/api.asp#methods
    /// </summary>
    /// <param name="type">The type.</param>
    /// <param name="value">The value.</param>
    /// <returns></returns>
    public bool AddAlert(AlertType type, string value)
    {
      if (type == AlertType.SMS)
      {
        throw new APIException("AlertType.SMS is not supported by the UptimeRobot API");
      }

      return Get<DefaultResponse>("newAlertContact", 
        Parameter("alertContactType", (int)type), 
        Parameter("alertContactValue", value)
      ).Status;
    }


    /// <summary>
    /// Adds an alert.
    /// mobile/SMS alert contacts are not supported yet, see: http://www.uptimerobot.com/api.asp#methods
    /// </summary>
    /// <param name="alert">The alert.</param>
    /// <returns></returns>
    public bool AddAlert(Alert alert)
    {
      return AddAlert(alert.Type, alert.Value);
    }


    /// <summary>
    /// Removes an alert.
    /// </summary>
    /// <param name="type">The type.</param>
    /// <param name="value">The value.</param>
    /// <returns></returns>
    public bool DeleteAlert(int alertID)
    {
      return Get<DefaultResponse>("deleteAlertContact", Parameter("alertContactID", alertID)).Status;
    }


    /// <summary>
    /// Removes an alert.
    /// </summary>
    /// <param name="alert">The alert.</param>
    /// <returns></returns>
    public bool DeleteAlert(Alert alert)
    {
      return DeleteAlert((int)alert.ID);
    }
  }
}