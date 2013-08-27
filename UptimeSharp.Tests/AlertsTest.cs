using Xunit;
using System.Collections.Generic;
using UptimeSharp.Models;
using System;

namespace UptimeSharp.Tests
{
  public class AlertsTest : IDisposable
  {
    UptimeClient client;

    // this API key is associated with the test account uptimesharp@outlook.com
    // please don't abuse it and create your own if you want to test the project!
    string APIKey = "u97240-a24c634b3b84f1af602628e8";


    // setup
    public AlertsTest()
    {
      client = new UptimeClient(APIKey);
    }


    // teardown
    public void Dispose()
    {
      client.GetAlerts().ForEach(alert => 
      {
        try
        {
          client.DeleteAlert(alert);
        }
        catch(UptimeSharpException e){}
      });
    }


    [Fact]
    public void AddInvalidAlertWithTypeSms()
    {
      Assert.Throws<UptimeSharpException>(() =>
      {
        client.AddAlert(Models.AlertType.SMS, "+436601289172");
      });
    }


    [Fact]
    public void AddInvalidAlertWithTypeTwitter()
    {
      Assert.Throws<UptimeSharpException>(() =>
      {
        client.AddAlert(Models.AlertType.Twitter, "artistandsocial");
      });
    }


    [Fact]
    public void AddAndRemoveAlerts()
    {
      string email = "example@ceecore.com";

      Assert.True(client.AddAlert(AlertType.Email, email));

      Alert origin = GetOriginAlert(email);

      Assert.Equal(email, origin.Value);
      Assert.Equal(AlertType.Email, origin.Type);

      client.DeleteAlert(origin);

      origin = GetOriginAlert(email);

      Assert.Null(origin);
    }


    [Fact]
    public void AddAndRetrieveSpecificAlerts()
    {
      Assert.True(client.AddAlert(AlertType.Email, "example1@ceecore.com"));
      Assert.True(client.AddAlert(AlertType.Boxcar, "example2@ceecore.com"));

      List<Alert> alerts = client.GetAlerts();

      Assert.InRange(alerts.ToArray().Length, 2, 100);

      List<Alert> specificAlerts = client.GetAlerts(new string[] { alerts[0].ID, alerts[1].ID });

      Assert.Equal(2, specificAlerts.ToArray().Length);
    }


    private Alert GetOriginAlert(string value)
    {
      List<Alert> alerts = client.GetAlerts();
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
