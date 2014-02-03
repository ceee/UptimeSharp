using System;
using System.Threading.Tasks;
using Xunit;

namespace UptimeSharp.Tests
{
  public class AccountTest : TestsBase
  {
    public AccountTest() : base() { }


    [Fact]
    public async Task IsAvailabilityCheckWorking()
    {
      Assert.False(await client.IsEmailAvailable("cee@live.at"));
      Assert.True(await client.IsEmailAvailable(Guid.NewGuid().ToString() + "@live.at"));

      await ThrowsAsync<ArgumentNullException>(async () =>
      {
        await client.IsEmailAvailable("");
      });

      await ThrowsAsync<ArgumentNullException>(async () =>
      {
        await client.IsEmailAvailable("");
      });

      await ThrowsAsync<ArgumentException>(async () =>
      {
        await client.IsEmailAvailable("opiu@opiuast");
      });
    }
  }
}
