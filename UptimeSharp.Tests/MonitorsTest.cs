using Xunit;
using System;
using System.Collections.Generic;
using UptimeSharp.Models;
using System.Threading.Tasks;

namespace UptimeSharp.Tests
{
  public class MonitorsTest : TestsBase
  {
    public MonitorsTest() : base() { }


    public async Task Dispose()
    {
      List<Monitor> monitors = await client.GetMonitors();
      monitors.ForEach(item => monitorsToDelete.Add(item.ID));
    }


    [Fact]
    public async Task AddHTTPMonitor()
    {
      Assert.True(await client.AddMonitor(
        name: "test_1",
        uri: "http://test1.com"
      ));
    }


    [Fact]
    public async Task AddKeywordMonitor()
    {
      Assert.True(await client.AddMonitor(
        name: "test_2",
        uri: "http://test2.com",
        type: Models.Type.Keyword,
        keywordType: KeywordType.Exists,
        keywordValue: "test"
      ));
    }


    [Fact]
    public async Task AddPingMonitor()
    {
      Assert.True(await client.AddMonitor(
        name: "test_3",
        uri: "http://test3.com",
        type: Models.Type.Ping
      ));
    }


    [Fact]
    public async Task AddPortMonitor()
    {
      Assert.True(await client.AddMonitor(
        name: "test_4",
        uri: "127.0.0.1",
        type: Models.Type.Port,
        subtype: Subtype.Custom,
        port: 50004
      ));
    }


    [Fact]
    public async Task GetMonitors()
    {
      Assert.True(await client.AddMonitor(
        name: "test_5",
        uri: "255.0.0.1",
        type: Models.Type.Port,
        subtype: Subtype.HTTP
      ));

      List<Monitor> items = await client.GetMonitors();
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
    public async Task ModifyAMonitor()
    {
      Assert.True(await client.AddMonitor(
        name: "test_6",
        uri: "http://test6.com"
      ));

      List<Monitor> items = await client.GetMonitors();
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

      Assert.True(await client.ModifyMonitor(monitor));

      monitor = await client.GetMonitor(monitor.ID);

      Assert.Equal(monitor.Name, "updated_test_6");
    }
  }
}
