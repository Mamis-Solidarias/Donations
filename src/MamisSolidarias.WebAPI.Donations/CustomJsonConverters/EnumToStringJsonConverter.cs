using System.Text.Json;
using System.Text.Json.Serialization;

namespace MamisSolidarias.WebAPI.Donations.CustomJsonConverters;

internal class EnumToStringJsonConverter<T>: JsonConverter<T>
where T: struct
{
    private readonly Type _enumType;
    internal EnumToStringJsonConverter()
    {
        _enumType = typeof(T);

        if (!_enumType.IsEnum)
        {
            throw new ArgumentException("T must be an enumerated type");
        }
            
    }

    public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        
        if (reader.GetString() is { } str&& Enum.TryParse(_enumType,str,true, out var value))
        {
            return (T)value;
        }

        throw new InvalidCastException($"Cannot convert to enum {_enumType.Name}");
    }

    public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}