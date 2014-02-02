using System;
using System.Threading.Tasks;
using Xunit;

namespace UptimeSharp.Tests
{
  public class TestsBase : IDisposable
  {
    protected UptimeClient client;

    // this API key is associated with the test account uptimesharp@outlook.com
    // please don't abuse it and create your own if you want to test the project!
    protected string APIKey = "u97240-a24c634b3b84f1af602628e8";


    // setup
    public TestsBase()
    {
      client = new UptimeClient(APIKey);
    }


    // teardown
    public void Dispose() { }


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
