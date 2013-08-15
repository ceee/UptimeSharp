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
      return Get<Get>("getMonitors").Items;
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
        Monitors = new int[] { monitorId }
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
        Monitors = monitors
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
  }
}