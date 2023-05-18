using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

public class NullableDateTimeConverter : IsoDateTimeConverter
{
    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        if (reader.TokenType == JsonToken.Null)
        {
            return null;
        }

        return base.ReadJson(reader, objectType, existingValue, serializer);
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        if (value == null)
        {
            writer.WriteNull();
            return;
        }

        base.WriteJson(writer, value, serializer);
    }
}
