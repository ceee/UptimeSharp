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
    /// <param name="parameters">parameters, which are mapped to the officials from http://www.uptimerobot.com/api.asp#methods </param>
    /// <returns></returns>
    public List<Monitor> Retrieve(RetrieveParameters parameters)
    {
      return Get<RetrieveResponse>("getMonitors", parameters.Convert()).Items;
    }


    /// <summary>
    /// Retrieves all monitors from UptimeRobot
    /// </summary>
    /// <returns></returns>
    public List<Monitor> Retrieve()
    {
      return Retrieve(new RetrieveParameters()
      {
        ShowAlerts = true
      });
    }


    /// <summary>
    /// Retrieves a monitor from UptimeRobot
    /// </summary>
    /// <param name="monitorId">a specific monitor ID</param>
    /// <returns></returns>
    public Monitor Retrieve(int monitorId)
    {
      return Retrieve(new RetrieveParameters()
      {
        Monitors = new int[] { monitorId },
        ShowAlerts = true
      })[0];
    }


    /// <summary>
    /// Retrieves specified monitors from UptimeRobot
    /// </summary>
    /// <param name="monitors">monitor list</param>
    /// <returns></returns>
    public List<Monitor> Retrieve(int[] monitors)
    {
      return Retrieve(new RetrieveParameters()
      {
        Monitors = monitors,
        ShowAlerts = true
      });
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

      return Get<DefaultResponse>("deleteMonitor", parameters).Status;
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
    /// <param name="parameters">The monitor parameters.</param>
    /// <returns>
    /// success state
    /// </returns>
    public bool Add(MonitorParameters parameters)
    {
      return Get<DefaultResponse>("newMonitor", parameters.Convert()).Status;
    }


    /// <summary>
    /// Creates a HTTP/IP monitor.
    /// </summary>
    /// <param name="name">The name of the new monitor.</param>
    /// <param name="uri">The URI or IP to watch.</param>
    /// <param name="alerts">A ID list of existing alerts to notify.</param>
    /// <returns>
    /// success state
    /// </returns>
    public bool Add(string name, string uri, int[] alerts = null)
    {
      return Add(new MonitorParameters()
      {
        Name = name,
        Uri = uri,
        Type = Type.HTTP,
        Alerts = alerts
      });
    }


    /// <summary>
    /// Creates a Port monitor.
    /// </summary>
    /// <param name="name">The name of the new monitor.</param>
    /// <param name="uri">The URI or IP to watch.</param>
    /// <param name="subtype">The subtype of the port.</param>
    /// <param name="port">The port (only for Subtype.Custom).</param>
    /// <param name="alerts">A ID list of existing alerts to notify.</param>
    /// <returns>
    /// success state
    /// </returns>
    public bool Add(string name, string uri, Subtype subtype, int? port = null, int[] alerts = null)
    {
      return Add(new MonitorParameters()
      {
        Name = name,
        Uri = uri,
        Type = Type.Port,
        Subtype = subtype,
        Port = port,
        Alerts = alerts
      });
    }


    /// <summary>
    /// Creates a Keyword monitor.
    /// </summary>
    /// <param name="name">The name of the new monitor.</param>
    /// <param name="uri">The URI or IP to watch.</param>
    /// <param name="keywordType">Type of the keyword.</param>
    /// <param name="keywordValue">The keyword value.</param>
    /// <param name="alerts">A ID list of existing alerts to notify.</param>
    /// <returns>
    /// success state
    /// </returns>
    public bool Add(string name, string uri, string keyword, KeywordType keywordType = KeywordType.Exists, int[] alerts = null)
    {
      return Add(new MonitorParameters()
      {
        Name = name,
        Uri = uri,
        Type = Type.Keyword,
        KeywordValue = keyword,
        KeywordType = keywordType,
        Alerts = alerts
      });
    }


    /// <summary>
    /// Edits a monitor.
    /// </summary>
    /// <param name="monitorID">The monitor unique identifier.</param>
    /// <param name="parameters">The monitor parameters.</param>
    /// <returns>
    /// success state
    /// </returns>
    public bool Modify(int monitorID, MonitorParameters parameters)
    {
      List<Parameter> paramList = parameters.Convert();
      paramList.Add(Parameter("monitorID", monitorID));

      return Get<DefaultResponse>("editorMonitor", paramList).Status;
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