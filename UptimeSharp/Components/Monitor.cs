﻿using RestSharp;
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
    /// <returns></returns>
    public List<Monitor> Retrieve(int[] monitors = null, float[] customUptimeRatio = null, bool showLog = false, bool showAlerts = true)
    {
      RetrieveParameters parameters = new RetrieveParameters()
      {
        Monitors = monitors,
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
    /// <returns></returns>
    public Monitor Retrieve(int monitorId)
    {
      return Retrieve(new int[] { monitorId })[0];
    }


    /// <summary>
    /// Deletes a monitor
    /// </summary>
    /// <param name="monitorId">The unique identifier for the monitor.</param>
    /// <returns>success state</returns>
    public bool Delete(int monitorId)
    {
      return Get<DefaultResponse>("deleteMonitor", Parameter("monitorID", monitorId)).Status;
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


    /// <summary>
    /// Creates a monitor.
    /// </summary>
    /// <param name="name">The name of the new monitor.</param>
    /// <param name="uri">The URI or IP to watch.</param>
    /// <param name="type">The type of the monitor.</param>
    /// <param name="subtype">The subtype of the port.</param>
    /// <param name="port">The port (only for Subtype.Custom).</param>
    /// <param name="keywordValue">The keyword value.</param>
    /// <param name="keywordType">Type of the keyword.</param>
    /// <param name="alerts">A ID list of existing alerts to notify.</param>
    /// <returns>
    /// success state
    /// </returns>
    public bool Add(string name, string uri, Type type = Type.HTTP, Subtype subtype = Subtype.Unknown,
                    int? port = null, string keywordValue = null, KeywordType keywordType = KeywordType.Unknown,
                    int[] alerts = null, string HTTPPassword = null, string HTTPUsername = null)
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
    /// success state
    /// </returns>
    public bool Modify(Monitor monitor)
    {
      List<int> alerts = null;

      if (monitor.Alerts != null)
      {
        monitor.Alerts.ForEach(item => alerts.Add((int)item.ID));
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