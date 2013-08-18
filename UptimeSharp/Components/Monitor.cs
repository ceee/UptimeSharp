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
    /// Retrieves all monitors from UptimeRobot
    /// </summary>
    /// <returns></returns>
    public List<Monitor> Get()
    {
      GetParameters parameters = new GetParameters()
      {
        ShowAlerts = true
      };

      return Get<Get>("getMonitors", parameters.Convert()).Items;
    }


    /// <summary>
    /// Retrieves a monitor from UptimeRobot
    /// </summary>
    /// <param name="monitorId">a specific monitor ID</param>
    /// <returns></returns>
    public Monitor Get(int monitorId)
    {
      GetParameters parameters = new GetParameters()
      {
        Monitors = new int[] { monitorId },
        ShowAlerts = true
      };

      return Get<Get>("getMonitors", parameters.Convert()).Items[0];
    }


    /// <summary>
    /// Retrieves specified monitors from UptimeRobot
    /// </summary>
    /// <param name="monitors">monitor list</param>
    /// <returns></returns>
    public List<Monitor> Get(int[] monitors)
    {
      GetParameters parameters = new GetParameters()
      {
        Monitors = monitors,
        ShowAlerts = true
      };

      return Get<Get>("getMonitors", parameters.Convert()).Items;
    }


    /// <summary>
    /// Retrieves all monitors from UptimeRobot
    /// </summary>
    /// <param name="parameters">parameters, which are mapped to the officials from http://www.uptimerobot.com/api.asp#methods </param>
    /// <returns></returns>
    public List<Monitor> Get(GetParameters parameters)
    {
      return Get<Get>("getMonitors", parameters.Convert()).Items;
    }


    /// <summary>
    /// Deletes a monitor
    /// </summary>
    /// <param name="ID">The unique identifier for the monitor.</param>
    /// <returns>success state</returns>
    public bool Delete(int ID)
    {
      List<Parameter> parameters = new List<Parameter>()
      {
        new Parameter() { Name = "monitorID", Value = ID, Type = ParameterType.GetOrPost }
      };

      return Get<Default>("deleteMonitor", parameters).Status;
    }


    /// <summary>
    /// Deletes a monitor
    /// </summary>
    /// <param name="monitor">The monitor.</param>
    /// <returns>success state</returns>
    public bool Delete(Monitor monitor)
    {
      return Delete(monitor.ID);
    }
  }
}