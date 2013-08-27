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
    /// Retrieves specified monitors from UptimeRobot
    /// </summary>
    /// <param name="monitors">monitor list</param>
    /// <param name="customUptimeRatio">The custom uptime ratio.</param>
    /// <param name="showLog">if set to <c>true</c> [show log].</param>
    /// <param name="showAlerts">if set to <c>true</c> [show alerts].</param>
    /// <returns>
    /// Monitor List
    /// </returns>
    public List<Monitor> GetMonitors(int[] monitorIDs = null, float[] customUptimeRatio = null, bool showLog = false, bool showAlerts = true)
    {
      RetrieveParameters parameters = new RetrieveParameters()
      {
        Monitors = monitorIDs,
        CustomUptimeRatio = customUptimeRatio,
        ShowAlerts = showAlerts,
        ShowLog = showLog
      };

      return Get<RetrieveResponse>("getMonitors", parameters.Convert()).Items;
    }


    /// <summary>
    /// Retrieves a monitor from UptimeRobot
    /// </summary>
    /// <param name="monitorId">a specific monitor ID</param>
    /// <param name="customUptimeRatio">The custom uptime ratio.</param>
    /// <param name="showLog">if set to <c>true</c> [show log].</param>
    /// <param name="showAlerts">if set to <c>true</c> [show alerts].</param>
    /// <returns>
    /// The Monitor
    /// </returns>
    public Monitor GetMonitor(int monitorId, float[] customUptimeRatio = null, bool showLog = false, bool showAlerts = true)
    {
      List<Monitor> monitors = GetMonitors(new int[] { monitorId }, customUptimeRatio, showLog, showAlerts);

      return monitors.ToArray().Length > 0 ? monitors[0] : null;
    }


    /// <summary>
    /// Deletes a monitor
    /// </summary>
    /// <param name="monitorId">a specific monitor ID</param>
    /// <returns>
    /// Success state
    /// </returns>
    public bool DeleteMonitor(int monitorId)
    {
      return Get<DefaultResponse>("deleteMonitor", Parameter("monitorID", monitorId)).Status;
    }


    /// <summary>
    /// Deletes a monitor
    /// </summary>
    /// <param name="monitor">The monitor.</param>
    /// <returns>
    /// Success state
    /// </returns>
    public bool DeleteMonitor(Monitor monitor)
    {
      return DeleteMonitor(monitor.ID);
    }


    /// <summary>
    /// Creates a monitor.
    /// </summary>
    /// <param name="name">The name of the new monitor.</param>
    /// <param name="uri">The URI or IP to watch.</param>
    /// <param name="type">The type of the monitor.</param>
    /// <param name="subtype">The subtype of the monitor (if port).</param>
    /// <param name="port">The port (only for Subtype.Custom).</param>
    /// <param name="keywordValue">The keyword value.</param>
    /// <param name="keywordType">Type of the keyword.</param>
    /// <param name="alerts">An ID list of existing alerts to notify.</param>
    /// <param name="HTTPUsername">The HTTP username.</param>
    /// <param name="HTTPPassword">The HTTP password.</param>
    /// <returns>
    /// Success state
    /// </returns>
    public bool AddMonitor(string name, string uri, Type type = Type.HTTP, Subtype subtype = Subtype.Unknown,
                           int? port = null, string keywordValue = null, KeywordType keywordType = KeywordType.Unknown,
                           string[] alerts = null, string HTTPUsername = null, string HTTPPassword = null)
    {

      MonitorParameters parameters = new MonitorParameters()
      {
        Name = name,
        Uri = uri,
        Type = type,
        Subtype = subtype,
        Port = port,
        KeywordType = keywordType,
        KeywordValue = keywordValue,
        Alerts = alerts,
        HTTPPassword = HTTPPassword,
        HTTPUsername = HTTPUsername
      };

      return Get<DefaultResponse>("newMonitor", parameters.Convert()).Status;
    }


    /// <summary>
    /// Edits a monitor.
    /// </summary>
    /// <param name="monitor">The monitor.</param>
    /// <returns>
    /// Success state
    /// </returns>
    public bool ModifyMonitor(Monitor monitor)
    {
      List<string> alerts = null;

      if (monitor.Alerts != null)
      {
        monitor.Alerts.ForEach(item => alerts.Add(item.ID));
      }

      MonitorParameters parameters = new MonitorParameters()
      {
        Name = monitor.Name,
        Uri = monitor.UriString != null ? monitor.UriString : null,
        Port = monitor.Port,
        HTTPPassword = monitor.HTTPPassword,
        HTTPUsername = monitor.HTTPUsername,
        KeywordType = monitor.KeywordType,
        KeywordValue = monitor.KeywordValue,
        Subtype = monitor.Subtype,
        Alerts = alerts != null ? alerts.ToArray() : null
      };

      List<Parameter> paramList = parameters.Convert();

      // fix bad behaviour in API if no subtype is submitted
      if(parameters.Subtype == Subtype.Unknown)
      {
        paramList.Add(Parameter("monitorSubType", 0));
      }

      paramList.Add(Parameter("monitorID", monitor.ID));

      return Get<DefaultResponse>("editMonitor", paramList).Status;
    }
  }
}