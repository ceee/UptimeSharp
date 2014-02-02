![UptimeSharp](https://raw.github.com/ceee/UptimeSharp/master/Assets/github-header.png)

**UptimeSharp** is a .NET portable class library that integrates the [UptimeRobot API](http://www.uptimerobot.com/api.asp).

The wrapper consists of 2 parts:

- Get and modify monitors
- Get and modify alert contacts

## Install UptimeSharp using [NuGet](https://www.nuget.org/packages/UptimeSharp/)

```
Install-Package UptimeSharp
```

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
List<Monitor> monitors = await _client.GetMonitors()

monitors.ForEach(
	item => Debug.WriteLine(item.Name + " | " + item.Type)
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
List<Monitor> items = await _client.GetMonitors();
```

Get monitors by ID - or a single monitor:

```csharp
List<Monitor> items = await _client.GetMonitors(new string[]{ 12891, 98711 });
// or
Monitor item = await _client.GetMonitor("12891");
```

Provide additional params for more data:

```csharp
List<Monitor> items = await _client.GetMonitors(
	monitorIDs: new string[]{ 12891, 98711 },
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
Task<Monitor> AddMonitor(
	string name, 
	string target, 
	Type type = Type.HTTP, 
	Subtype subtype = Subtype.Unknown,
    int? port = null, 
	string keywordValue = null, 
	KeywordType keywordType = KeywordType.Unknown,
    string[] alerts = null, 
	string HTTPUsername = null,
	string HTTPPassword = null
)
```

Example - Watch a SMTP Server:

```csharp
Monitor monitor = await _client.AddMonitor(
	name: "cee",
	target: "127.0.0.1",
	type: Type.Port,
	subtype: Subtype.SMTP
);
```

`name`: A friendly name for the new monitor
<br>
`target`: The URI or IP to watch
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
bool isSuccess = await _client.DeleteMonitor("12891");
```

Delete a monitor by a Monitor instance:

```csharp
// Monitor myMonitor = ...
bool isSuccess = await _client.DeleteMonitor(myMonitor);
```


## Modify

In order to modify an existing monitor, just alter the properties of the Monitor instance and call the `ModifyMonitor` method:

```csharp
// Monitor myMonitor = ...
myMonitor.Name = "my new name :-)";
bool isSuccess = await _client.ModifyMonitor(myMonitor);
```

**Important:** It is not possible to alter the `Type` of a monitor after its creation! In case you want to do this, you have to delete the monitor and create a new one with the changed type.

### Modify Alerts

Retrieve all alerts:

```csharp
List<Alert> items = await _client.GetAlerts();
```

Retrieve alerts by IDs:

```csharp
List<Alert> items = await _client.GetAlerts(new string[]{ "12897", "98711" });
```

Retrieve a specific alert:

```csharp
Alert item = await _client.GetAlert("12897");
```

Adds an alert _(Due to UptimeRobot API limitations SMS and Twitter alert contact types are not supported yet)_:

```csharp
Alert alert = await _client.AddAlert(AlertType.Email, "uptimesharp@outlook.com");
```

Adds an alert from instance:

```csharp
// Alert myAlert = ...
Alert alert = await _client.AddAlert(myAlert);
```

Deletes an alert:

```csharp
bool isSuccess = await _client.DeleteAlert("12897");
```

Deletes an alert from instance:

```csharp
// Alert myAlert = ...
bool isSuccess = await _client.DeleteAlert(myAlert);
```

## Monitor Types

- [HTTP](#http-monitoring)
- [Keyword](#keyword)
- [Ping](#ping)
- [Port](#port)

### HTTP Monitoring

Simple HTTP monitor which requests the webpage every 5 minutes and checks for HTTP Status 200 OK.

### Keyword Monitoring

The keyword monitor is sniffing the page content if a specified keyword exists/not exists.

The keyword is submitted via the **keywordValue** parameter. The **keywordType** parameter specifies if the value should exist or not exist.

### Ping Monitoring

This type lets you monitor a server by pinging it.

### Port Monitoring

If you want to monitor a port, you need to specify a **subType** which is a common port a custom one:

- HTTP :80
- HTTPS :443
- FTP :21
- SMTP :25
- POP3 :110
- IMAP :143
- Custom Port (use the "port" parameter, if this option is selected)

---


## Supported platforms

UptimeSharp is a **Portable Class Library**, therefore it's compatible with multiple platforms:

- **.NET** >= 4.5 (including WPF)
- **Silverlight** >= 4
- **Windows Phone** >= 7.5
- **Windows Store**

## Dependencies

- [Microsoft.Bcl.Async](https://www.nuget.org/packages/Microsoft.Bcl.Async/)
- [Microsoft.Net.Http](https://www.nuget.org/packages/Microsoft.Net.Http/)
- [Newtonsoft.Json](https://www.nuget.org/packages/Newtonsoft.Json/)
- [PropertyChanged.Fody](https://github.com/Fody/PropertyChanged)

## Contributors

| [![ceee](http://gravatar.com/avatar/9c61b1f4307425f12f05d3adb930ba66?s=70)](https://github.com/ceee "Tobias Klika") |
|---|
| [ceee](https://github.com/ceee) |
## License

[MIT License](https://github.com/ceee/UptimeSharp/blob/master/LICENSE-MIT)
