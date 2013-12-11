﻿using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace UptimeSharp
{

  public class BoolConverter : JsonConverter
  {
    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
      writer.WriteValue(((bool)value) ? 1 : 0);
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
      return reader.Value.ToString() == "1";
    }

    public override bool CanConvert(Type objectType)
    {
      return objectType == typeof(bool);
    }
  }



  public class UnixDateTimeConverter : DateTimeConverterBase
  {
    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
      DateTime epoc = new DateTime(1970, 1, 1);
      var delta = (DateTime)value - epoc;

      writer.WriteValue((long)delta.TotalSeconds);
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
      if (reader.Value.ToString() == "0")
      {
        return null;
      }

      return new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(Convert.ToDouble(reader.Value)).ToLocalTime();
    }
  }



  public class UriConverter : JsonConverter
  {
    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
      if (reader.TokenType == JsonToken.String && Uri.IsWellFormedUriString(reader.Value.ToString(), UriKind.Absolute))
      {
        return new Uri(reader.Value.ToString());
      }

      return null;
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
      if (value == null)
      {
        writer.WriteNull();
      }
      else if (value is Uri)
      {
        writer.WriteValue(((Uri)value).OriginalString);
      }
    }

    public override bool CanConvert(Type objectType)
    {
      return objectType.Equals(typeof(Uri));
    }
  }
}