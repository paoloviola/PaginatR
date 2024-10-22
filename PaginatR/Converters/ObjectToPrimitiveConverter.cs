using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PaginatR.Converters
{
    internal class ObjectToPrimitiveConverter : JsonConverter<object>
    {
        public override object? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            switch (reader.TokenType)
            {
                case JsonTokenType.String:
                    return reader.GetString();
                case JsonTokenType.Number:
                    if (reader.TryGetInt32(out int intValue))
                    {
                        return intValue;
                    }
                    if (reader.TryGetDouble(out double doubleValue))
                    {
                        return doubleValue;
                    }
                    break;
                case JsonTokenType.True:
                case JsonTokenType.False:
                    return reader.GetBoolean();
                case JsonTokenType.Null:
                    return null;
                default:
                    return JsonSerializer.Deserialize<object>(ref reader, options);
            }

            throw new JsonException("Unsupported JSON token type.");
        }

        public override void Write(Utf8JsonWriter writer, object value, JsonSerializerOptions options)
        {
            switch (value)
            {
                case string stringValue:
                    writer.WriteStringValue(stringValue);
                    break;
                case int intValue:
                    writer.WriteNumberValue(intValue);
                    break;
                case double doubleValue:
                    writer.WriteNumberValue(doubleValue);
                    break;
                case bool boolValue:
                    writer.WriteBooleanValue(boolValue);
                    break;
                default:
                    JsonSerializer.Serialize(writer, value, options);
                    break;
            }
        }
    }
}
