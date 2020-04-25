using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SmartControl.Api.Server.MyJsonConverter
{
    /// <summary>
    /// Date Converter for date format
    /// </summary>
    public class MyDateConverter : JsonConverter<DateTime>
    {
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var s = reader.GetString();
            CultureInfo provider = CultureInfo.InvariantCulture;
            try
            {
                return DateTime.ParseExact(s, "yyyy-MM-ddTH:mm", provider);
            }
            catch
            {
                return new DateTime(DateTime.MinValue.Ticks);
            }
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString("yyyy-MM-ddTH:mm"));
        }
    }
}
