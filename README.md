# UptimeSharp

**UptimeSharp** is a C#.NET class library that integrates the [UptimeRobot API](http://www.uptimerobot.com/api.asp).

The wrapper consists of 2 parts:

- Get and modify monitors
- Get and modify alert contacts

[uptimesharp.frontendplay.com](http://uptimesharp.frontendplay.com/)

## Install using NuGet

```
Install-Package UptimeSharp
```

[nuget.org/packages/UptimeSharp](https://www.nuget.org/packages/UptimeSharp/)

## Usage Example

Get your [API Key UptimeRobot](http://uptimerobot.com/mySettings.asp) (left section under "API Information")

Include the UptimeSharp namespace and it's associated models:

```csharp
using UptimeSharp;
using UptimeSharp.Models;
```

Initialize UptimeClient with:

```csharp
UptimeClient _client = new UptimeClient("[YOUR_API_KEY]");
```

Do a simple request - e.g. get all your monitors:

```csharp
_client.GetMonitors().ForEach(
	item => Console.WriteLine(item.Name + " | " + item.Type)
);
```

Which will output:

    frontendplay | HTTP
    google | Keyword
    localhost | Ping
	...


## Constructor

```csharp
UptimeClient(string apiKey)
```

Get your [API Key UptimeRobot](http://uptimerobot.com/mySettings.asp) (left section under "API Information")


## Retrieve

Get list of all monitors:

```csharp
List<Monitor> items = _client.GetMonitors();
```

Get monitors by ID - or a single monitor:

```csharp
List<Monitor> items = _client.GetMonitors(new int[]{ 12891, 98711 });
// or
Monitor item = _client.GetMonitor(12891);
```

Provide additional params for more data:

```csharp
List<Monitor> items = _client.GetMonitors(
	monitorIDs: new int[]{ 12891, 98711 },
	customUptimeRatio: new float[] { 7, 30, 45 },
	showLog: true,
	showAlerts: true
);
```

`monitorIDs`: You can remove this parameter if you want to retrieve all monitors _(default: null)_
<br>
`customUptimeRatio`: the number of days to calculate the uptime ratio(s) for _(default: null)_
<br>
`showLog`: include log, if true _(default: false)_
<br>
`showAlerts`: include alerts, if true _(default: true)_
<br>


## Add

Adds/creates a new monitor.

```csharp
bool AddMonitor(
	string name, 
	string uri, 
	Type type = Type.HTTP, 
	Subtype subtype = Subtype.Unknown,
    int? port = null, 
	string keywordValue = null, 
	KeywordType keywordType = KeywordType.Unknown,
    int[] alerts = null, 
	string HTTPUsername = null,
	string HTTPPassword = null
)
```

Example - Watch an SMTP Server:

```csharp
bool isSuccess = _client.AddMonitor(
	name: "cee",
	uri: "127.0.0.1",
	type: Type.Port,
	subtype: Subtype.SMTP
);
```

`name`: A friendly name for the new monitor
<br>
`uri`: The URI or IP to watch
<br>
`type`: The type of the monitor (see [# Monitor Types](#monitor-types))
<br>
`subtype`: The subtype of the monitor (only for Type.Port) (see [# Monitor Types](#monitor-types))
<br>
`port`: The port (only for Subtype.Custom)
<br>
`keywordValue`: The keyword value (for Type.Keyword)
<br>
`keywordType`: Type of the keyword (for Type.Keyword)
<br>
`alerts`: An ID list of existing alerts to notify
<br>
`HTTPUsername`: The HTTP username
<br>
`HTTPPassword`: The HTTP password

As you can see, a lot of these parameters are only available if you've specified the correct `Type`.
<br>
If you've selected `Type.Port` for example, UptimeSharp will ignore the `keywordValue` and `keywordType` parameters, even if you submitted valid ones.


## Delete

Delete a monitor by ID:

```csharp
bool isSuccess = _client.DeleteMonitor(12891);
```

Delete a monitor by a Monitor instance:

```csharp
// Monitor myMonitor = ...
bool isSuccess = _client.DeleteMonitor(myMonitor);
```


## Modify

In order to modify an existing monitor, just alter the properties of the Monitor instance and call the `ModifyMonitor` method:

```csharp
// Monitor myMonitor = ...
myMonitor.Name = "my new name :-)";
bool isSuccess = _client.ModifyMonitor(myMonitor);
```

**Important:** It is not possible to alter the `Type` of a monitor after its creation! In case you want to do this, you have to delete the monitor and create a new one with the changed type.

### Modify Alerts

Retrieve all alerts:

```csharp
List<Alert> items = _client.GetAlerts();
```

Retrieve alerts by IDs:

```csharp
List<Alert> items = _client.GetAlerts(new string[]{ "12897", "98711" });
```

Retrieve a specific alert:

```csharp
Alert item = _client.GetAlert("12897");
```

Adds an alert _(Due to UptimeRobot API limitations SMS and Twitter alert contact types are not supported yet)_:

```csharp
bool isSuccess = _client.AddAlert(AlertType.Email, "uptimesharp@outlook.com");
```

Adds an alert from instance:

```csharp
// Alert myAlert = ...
bool isSuccess = _client.AddAlert(myAlert);
```

Deletes an alert:

```csharp
bool isSuccess = _client.DeleteAlert("12897");
```

Deletes an alert from instance:

```csharp
// Alert myAlert = ...
bool isSuccess = _client.DeleteAlert(myAlert);
```

---

## Release History

- 2013-08-28 v0.2.0 Request Validation
- 2013-08-26 v0.1.1 Adding a Port monitor works now
- 2013-08-25 v0.1.0 Monitor and Alert APIs

## Dependencies

- [RestSharp](http://restsharp.org/)
- [ServiceStack.Text](https://github.com/ServiceStack/ServiceStack.Text)

## Contributors
| [![twitter/artistandsocial](http://gravatar.com/avatar/9c61b1f4307425f12f05d3adb930ba66?s=70)](http://twitter.com/artistandsocial "Follow @artistandsocial on Twitter") |
|---|
| [Tobias Klika @ceee](https://github.com/ceee) |

## License

[MIT License](https://github.com/ceee/UptimeSharp/blob/master/LICENSE-MIT)
