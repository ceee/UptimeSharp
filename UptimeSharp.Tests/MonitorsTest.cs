using Xunit;
using System;
using System.Collections.Generic;
using UptimeSharp.Models;

namespace UptimeSharp.Tests
{
  public class MonitorsTest : IDisposable
  {
    UptimeClient client;

    // this API key is associated with the test account uptimesharp@outlook.com
    // please don't abuse it and create your own if you want to test the project!
    string APIKey = "u97240-a24c634b3b84f1af602628e8";


    // setup
    public MonitorsTest()
    {
      //client = new UptimeClient(APIKey);
    }


    // teardown
    public void Dispose()
    {
      //List<Alert> alerts = client.GetAlerts();
      //alerts.ForEach(alert => client.DeleteAlert(alert));
    }


    //bool result = client.Delete(775853599);

    //bool result = client.Create("apitest", "http://pocketsharp.com", Models.Type.HTTP);
    //client.Add("apiKeywordTest", "http://frontendplay.com", "frontendplay", KeywordType.NotExists);

    //client.DeleteAlert(2014599);
    //bool result = client.AddAlert(AlertType.Email, "klika@live.at");

    //bool x = client.Add(
    //  name: "testx",
    //  uri: "http://google.at",
    //  type: Models.Type.Keyword,
    //  keywordType: KeywordType.Exists,
    //  keywordValue: "goog"
    //);
    //List<Monitor> monitors = client.Retrieve(showLog: true);
    //List<Alert> alerts = client.RetrieveAlerts();
    //System.Console.WriteLine(monitor.ID + " " + monitor.Name);
    //alerts.ForEach(item => System.Console.WriteLine(item.ID + " " + item.Type.ToString() + " " + item.Value));

    //monitor.Name = "HALLOOO";
    //bool result = client.Modify(monitor);

    //monitors.ForEach(item => System.Console.WriteLine(item.ID + " " + item.Name + " " + item.Status));
  }
}
