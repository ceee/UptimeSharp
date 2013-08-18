using RestSharp;
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
    /// <param name="IDs">Retrieve specified alert contacts by supplying IDs for them.</param>
    /// <returns></returns>
    public List<Alert> RetrieveAlerts(int[] alertIDs = null)
    {
      return Get<AlertResponse>("getAlertContacts").Items;
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
      List<Parameter> parameters = new List<Parameter>()
      {
        Utilities.CreateParam("alertContactType", (int)type),
        Utilities.CreateParam("alertContactValue", value)
      };

      return Get<DefaultResponse>("newAlertContact", parameters).Status;
    }


    /// <summary>
    /// Removes an alert.
    /// </summary>
    /// <param name="type">The type.</param>
    /// <param name="value">The value.</param>
    /// <returns></returns>
    public bool DeleteAlert(int alertID)
    {
      List<Parameter> parameters = new List<Parameter>()
      {
        Utilities.CreateParam("alertContactID", alertID)
      };

      return Get<DefaultResponse>("deleteAlertContact", parameters).Status;
    }
  }
}