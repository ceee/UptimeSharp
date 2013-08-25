using NUnit.Framework;
using System.Collections.Generic;
using UptimeSharp.Models;

namespace UptimeSharp.Tests
{
  [TestFixture]
  public class AlertsTest
  {
    UptimeClient client;

    // this API key is associated with the test account uptimesharp@outlook.com
    // please don't abuse it and create your own if you want to test the project!
    string APIKey = "u97240-a24c634b3b84f1af602628e8";


    [SetUp]
    public void Setup()
    {
      client = new UptimeClient(APIKey);
    }


    [TearDown]
    public void Teardown()
    {
      List<Alert> alerts = client.GetAlerts();
      alerts.ForEach(alert => client.DeleteAlert(alert));
    }


    [Test]
    [ExpectedException(typeof(APIException))]
    public void AddInvalidAlertWithTypeSms()
    {
      client.AddAlert(Models.AlertType.SMS, "+436601289172");
    }


    [Test]
    [ExpectedException(typeof(APIException))]
    public void AddInvalidAlertWithTypeTwitter()
    {
      client.AddAlert(Models.AlertType.Twitter, "artistandsocial");
    }


    [Test]
    public void AddAndRemoveAlerts()
    {
      string email = "example@ceecore.com";

      Assert.IsTrue(client.AddAlert(AlertType.Email, email), "Response should be true for adding a new alert");

      Alert origin = GetOriginAlert(email);

      Assert.AreEqual(email, origin.Value, "Retrieved alert should have the value (example@ceecore.com) which was submitted");
      Assert.AreEqual(AlertType.Email, origin.Type, "Retrieved alert should have the type (email) which was submitted");

      client.DeleteAlert(origin);

      origin = GetOriginAlert(email);

      Assert.IsNull(origin, "Alert should have been deleted");
    }


    [Test]
    public void AddAndRetrieveSpecificAlerts()
    {
      Assert.IsTrue(
        client.AddAlert(AlertType.Email, "example1@ceecore.com"),
        "Response should be true for adding a new alert (email 1)"
      );
      Assert.IsTrue(
        client.AddAlert(AlertType.Boxcar, "example@ceecore.com"),
        "Response should be true for adding a new alert (bocar 4)"
      );

      List<Alert> alerts = client.GetAlerts();

      Assert.GreaterOrEqual(alerts.ToArray().Length, 2, "Alerts length should be at least 2", alerts);

      List<Alert> specificAlerts = client.GetAlerts(new int[] { alerts[0].ID, alerts[1].ID });

      Assert.AreEqual(2, specificAlerts.ToArray().Length, "Specific alerts length should be 2", specificAlerts);
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
