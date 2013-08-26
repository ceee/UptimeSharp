using Xunit;
using System;
using System.Collections.Generic;
using UptimeSharp.Models;

namespace UptimeSharp.Tests
{
  public class MonitorsTest : IDisposable
  {
    UptimeClient client;

    // this API key is associated with the test account uptimesharp@outlook.com
    // please don't abuse it and create your own if you want to test the project!
    string APIKey = "u97240-a24c634b3b84f1af602628e8";


    // setup
    public MonitorsTest()
    {
      client = new UptimeClient(APIKey);
    }


    // teardown
    public void Dispose()
    {
      List<Monitor> monitors = client.GetMonitors();
      monitors.ForEach(monitor => client.DeleteMonitor(monitor));
    }


    [Fact]
    public void AddHTTPMonitor()
    {
      Assert.True(client.AddMonitor(
        name: "test_1",
        uri: "http://test1.com"
      ));
    }


    [Fact]
    public void AddKeywordMonitor()
    {
      Assert.True(client.AddMonitor(
        name: "test_2",
        uri: "http://test2.com",
        type: Models.Type.Keyword,
        keywordType: KeywordType.Exists,
        keywordValue: "test"
      ));
    }


    [Fact]
    public void AddPingMonitor()
    {
      Assert.True(client.AddMonitor(
        name: "test_3",
        uri: "http://test3.com",
        type: Models.Type.Ping
      ));
    }


    [Fact]
    public void AddPortMonitor()
    {
      Assert.True(client.AddMonitor(
        name: "test_4",
        uri: "127.0.0.1",
        type: Models.Type.Port,
        subtype: Subtype.Custom,
        port: 50004
      ));
    }


    [Fact]
    public void GetMonitors()
    {
      Assert.True(client.AddMonitor(
        name: "test_5",
        uri: "255.0.0.1",
        type: Models.Type.Port,
        subtype: Subtype.HTTP
      ));

      List<Monitor> items = client.GetMonitors();
      Monitor monitor = null;

      items.ForEach(item =>
      {
        if(item.Name == "test_5")
        {
          monitor = item;
        }
      });

      Assert.True(
        monitor != null 
        && monitor.UriString == "255.0.0.1"
        && monitor.Type == Models.Type.Port
        && monitor.Subtype == Subtype.HTTP);
    }


    [Fact]
    public void ModifyAMonitor()
    {
      Assert.True(client.AddMonitor(
        name: "test_6",
        uri: "http://test6.com"
      ));

      List<Monitor> items = client.GetMonitors();
      Monitor monitor = null;

      items.ForEach(item =>
      {
        if (item.Name == "test_6")
        {
          monitor = item;
        }
      });

      Assert.NotNull(monitor);

      monitor.Name = "updated_test_6";

      Assert.True(client.ModifyMonitor(monitor));

      monitor = client.GetMonitor(monitor.ID);

      Assert.Equal(monitor.Name, "updated_test_6");
    }
  }
}
