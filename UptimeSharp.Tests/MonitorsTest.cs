using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UptimeSharp.Models;
using Xunit;

namespace UptimeSharp.Tests
{
  public class MonitorsTest : TestsBase
  {
    public MonitorsTest() : base() { }


    [Fact]
    public async void AddHTTPMonitor()
    {
      Assert.True(await client.AddMonitor(
        name: "test_1",
        target: "http://test1.com"
      ));
    }


    [Fact]
    public async Task AddKeywordMonitor()
    {
      Assert.True(await client.AddMonitor(
        name: "test_2",
        target: "http://test2.com",
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
        target: "http://test3.com",
        type: Models.Type.Ping
      ));
    }


    [Fact]
    public async Task AddPortMonitor()
    {
      Assert.True(await client.AddMonitor(
        name: "test_4",
        target: "127.0.0.1",
        type: Models.Type.Port,
        subtype: Subtype.Custom,
        port: 50004
      ));
    }


    [Fact]
    public async Task GetMonitors()
    {
      //Assert.True(await client.AddMonitor(
      //  name: "test_5",
      //  target: "255.0.0.1",
      //  type: Models.Type.Port,
      //  subtype: Subtype.HTTP
      //));

      List<Monitor> items = await client.GetMonitors();
      Monitor monitor = items.SingleOrDefault(item => item.Name == "test_5");

      Assert.True(
        monitor != null
        && monitor.Target == "255.0.0.1"
        && monitor.Type == Models.Type.Port
        && monitor.Subtype == Subtype.HTTP);
    }


    [Fact]
    public async Task ModifyAMonitor()
    {
      Assert.True(await client.AddMonitor(
        name: "test_6",
        target: "http://test6.com"
      ));

      List<Monitor> items = await client.GetMonitors();
      Monitor monitor = items.SingleOrDefault(item => item.Name == "test_6");

      Assert.NotNull(monitor);

      monitor.Name = "updated_test_6";

      Assert.True(await client.ModifyMonitor(monitor));

      monitor = await client.GetMonitor(monitor.ID);

      Assert.Equal(monitor.Name, "updated_test_6");
    }
  }
}
