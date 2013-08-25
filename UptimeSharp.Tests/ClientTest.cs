using Xunit;
using System.Collections.Generic;
using UptimeSharp.Models;

namespace UptimeSharp.Tests
{
  public class ClientTest
  {
    UptimeClient client;

    // this API key is associated with the test account uptimesharp@outlook.com
    // please don't abuse it and create your own if you want to test the project!
    string APIKey = "u97240-a24c634b3b84f1af602628e8";


    // setup
    public ClientTest()
    {
      client = new UptimeClient(APIKey);
    }


    [Fact]
    public void Initialize()
    {
      Assert.Null(client.LastRequestData);

      Assert.Equal(APIKey, client.ApiKey);
    }
  }
}
