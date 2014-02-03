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

      await ThrowsAsync<ArgumentException>(async () =>
      {
        await client.IsEmailAvailable("opiu@opiuast");
      });
    }


    [Fact]
    public async Task IsAccountRegistrationWorking()
    {
      Assert.True(await client.RegisterAccount("my name", Guid.NewGuid().ToString() + "@live.at", "123456"));
      Assert.False(await client.RegisterAccount("my name", "cee@live.at", "123456"));

      await ThrowsAsync<ArgumentNullException>(async () =>
      {
        await client.RegisterAccount("", "cee@live.at", "poiuadfafo");
      });

      await ThrowsAsync<ArgumentNullException>(async () =>
      {
        await client.RegisterAccount("my name", "", "poiuadfafo");
      });

      await ThrowsAsync<ArgumentNullException>(async () =>
      {
        await client.RegisterAccount("my name", "cee@live.at", "");
      });

      await ThrowsAsync<ArgumentException>(async () =>
      {
        await client.RegisterAccount("my name", "opiu@opiuast", "password");
      });

      await ThrowsAsync<ArgumentException>(async () =>
      {
        await client.RegisterAccount("my", "cee@live.at", "password");
      });

      await ThrowsAsync<ArgumentException>(async () =>
      {
        await client.RegisterAccount("my name", "cee@live.at", "pass");
      });
    }
  }
}
