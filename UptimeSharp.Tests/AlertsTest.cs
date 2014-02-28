using System.Linq;
using System.Threading.Tasks;
using UptimeSharp.Models;
using Xunit;

namespace UptimeSharp.Tests
{
  public class AlertsTest : TestsBase
  {
    public AlertsTest() : base() { }


    [Fact]
    public async Task AddInvalidAlertWithTypeSms()
    {
      await ThrowsAsync<UptimeSharpException>(async () =>
      {
        await client.AddAlert(Models.AlertType.SMS, "+436601289172");
      });
    }


    [Fact]
    public async Task AddInvalidAlertWithTypeTwitter()
    {
      await ThrowsAsync<UptimeSharpException>(async () =>
      {
        await client.AddAlert(Models.AlertType.Twitter, "artistandsocial");
      });
    }


    [Fact]
    public async Task AddAndRemoveAlerts()
    {
      string email = "example@ceecore.com";

      Assert.NotNull(await client.AddAlert(AlertType.Email, email));

      Alert origin = await GetOriginAlert(email);

      Assert.Equal(email, origin.Value);
      Assert.Equal(AlertType.Email, origin.Type);

      await client.DeleteAlert(origin);

      origin = await GetOriginAlert(email);

      Assert.Null(origin);
    }


    [Fact]
    public async Task AddAndRetrieveSpecificAlerts()
    {
      Alert alert1;
      Alert alert2;
      Alert alert3;

      Assert.NotNull(alert1 = await client.AddAlert(AlertType.Email, "example1@ceecore.com"));
      Assert.NotNull(alert2 = await client.AddAlert(AlertType.Boxcar, "example2@ceecore.com"));
      Assert.NotNull(alert3 = await client.AddAlert(AlertType.WebHook, "http://ceecore.com?"));

      try
      {
        await client.DeleteAlert(alert1);
        await client.DeleteAlert(alert2);
        await client.DeleteAlert(alert3);
      }
      catch { }
    }


    private async Task<Alert> GetOriginAlert(string value)
    {
      return (await client.GetAlerts()).FirstOrDefault(item => item.Value == value);
    }
  }
}
