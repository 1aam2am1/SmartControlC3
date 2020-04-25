using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SmartControl.Api.Data
{
    [JsonConverter(typeof(MyVersionConverter))]
    public class MyVersion
    {
        public int Major { set; get; }
        public int Minor { set; get; }
        public int Build { set; get; }
        public int Revision { set; get; }

        public static implicit operator MyVersion(Version v)
        {
            return new MyVersion
            {
                Major = v.Major,
                Minor = v.Minor,
                Build = v.Build,
                Revision = v.Revision
            };
        }
    }


    public class MyVersionConverter : JsonConverter<MyVersion>
    {
        public override MyVersion Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var s = reader.GetString();
            var tab = s.Split(".", 4);

            return new MyVersion
            {
                Major = Int32.Parse(tab[0]),
                Minor = Int32.Parse(tab[1]),
                Build = Int32.Parse(tab[2]),
                Revision = Int32.Parse(tab[3])
            };
        }

        public override void Write(Utf8JsonWriter writer, MyVersion value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(string.Format("{0}.{1}.{2}.{3}", value.Major, value.Minor, value.Build, value.Revision));
        }
    }
}
