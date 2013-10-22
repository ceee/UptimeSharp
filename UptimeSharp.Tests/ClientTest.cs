using Xunit;
using System.Collections.Generic;
using UptimeSharp.Models;

namespace UptimeSharp.Tests
{
  public class ClientTest : TestsBase
  {
    public ClientTest() : base() { }


    [Fact]
    public void Initialize()
    {
      Assert.Null(client.LastRequestData);

      Assert.Equal(APIKey, client.ApiKey);
    }
  }
}
