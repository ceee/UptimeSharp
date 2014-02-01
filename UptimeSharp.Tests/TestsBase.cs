using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UptimeSharp.Models;
using Xunit;

namespace UptimeSharp.Tests
{
  public class TestsBase : IDisposable
  {
    protected UptimeClient client;

    protected List<int> alertsToDelete = new List<int>();
    protected List<int> monitorsToDelete = new List<int>();

    // this API key is associated with the test account uptimesharp@outlook.com
    // please don't abuse it and create your own if you want to test the project!
    protected string APIKey = "u97240-a24c634b3b84f1af602628e8";


    // setup
    public TestsBase()
    {
      client = new UptimeClient(APIKey);
    }


    // teardown
    public async void Dispose()
    {
      List<Monitor> monitors = await client.GetMonitors();
      alertsToDelete.ForEach(async id =>
      {
        await client.DeleteAlert(id.ToString());
      });
      monitors.ForEach(async id =>
      {
        await client.DeleteMonitor(id);
      });
    }


    // async throws
    public static async Task ThrowsAsync<TException>(Func<Task> func)
    {
      var expected = typeof(TException);
      System.Type actual = null;
      try
      {
        await func();
      }
      catch (Exception e)
      {
        actual = e.GetType();
      }
      Assert.Equal(expected, actual);
    }
  }
}
