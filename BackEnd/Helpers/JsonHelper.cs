using System.Text.Json;
using System.Text.Json.Serialization;

namespace Backend.Helpers;

public static class JsonHelper
{
    public static JsonSerializerOptions GetJsonSerializerOptions()
    {
        return new JsonSerializerOptions(new JsonSerializerOptions
        {
            WriteIndented = true,
            Converters =
            {
                new JsonStringEnumConverter()
            }
        });
    }
}