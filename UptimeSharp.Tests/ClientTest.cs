using NUnit.Framework;
using System.Collections.Generic;
using UptimeSharp.Models;

namespace UptimeSharp.Tests
{
  [TestFixture]
  public class ClientTest
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


    [Test]
    public void Initialize()
    {
      Assert.IsNull(client.LastRequestData, "LastRequestData should be null on init");

      Assert.AreEqual(APIKey, client.ApiKey, "API Key should be correctly assigned");
    }
  }
}
