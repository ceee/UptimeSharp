using Xunit;
using System.Collections.Generic;
using UptimeSharp.Models;
using System;
using System.Threading.Tasks;

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

      Assert.True(await client.AddAlert(AlertType.Email, email));

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
      Assert.True(await client.AddAlert(AlertType.Email, "example1@ceecore.com"));
      Assert.True(await client.AddAlert(AlertType.Boxcar, "example2@ceecore.com"));

      List<Alert> alerts = await client.GetAlerts();

      Assert.InRange(alerts.ToArray().Length, 2, 100);

      List<Alert> specificAlerts = await client.GetAlerts(new string[] { alerts[0].ID, alerts[1].ID });

      Assert.Equal(2, specificAlerts.ToArray().Length);
    }


    private async Task<Alert> GetOriginAlert(string value)
    {
      List<Alert> alerts = await client.GetAlerts();
      Alert origin = null;

      alerts.ForEach(alert =>
      {
        if (alert.Value == value)
        {
          origin = alert;
        }
      });

      return origin;
    }
  }
}
