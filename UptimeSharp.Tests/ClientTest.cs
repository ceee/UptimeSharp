using Xunit;

namespace UptimeSharp.Tests
{
  public class ClientTest : TestsBase
  {
    public ClientTest() : base() { }


    [Fact]
    public void Initialize()
    {
      Assert.Null(client.lastResponseData);

      Assert.Equal(APIKey, client.ApiKey);
    }
  }
}
