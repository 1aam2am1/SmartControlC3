using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Text.Json;
using System.Text.Json.Serialization;
using Api.Extensions;

namespace SmartControl
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        App()
        {
            //WARN: Remove and use JSON default in net 5.0
            ((JsonSerializerOptions)typeof(JsonSerializerOptions)
                .GetField("s_defaultOptions", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic).GetValue(null))
                .Converters.Add(new JsonNonStringKeyDictionaryConverterFactory());
            /// check windows resolution. Change window size accordingly
        }
    }
}
