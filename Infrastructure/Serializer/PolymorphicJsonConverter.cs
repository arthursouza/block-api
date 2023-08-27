using Domain.Entities;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Infrastructure.Serializer;

public class PolymorphicJsonConverter<T> : JsonConverter<T>
{
    public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using (JsonDocument doc = JsonDocument.ParseValue(ref reader))
        {
            var root = doc.RootElement;
            var targetType = typeToConvert;
            var typeId = Guid.Parse(root.GetProperty("id").GetString());

            if (typeId == BlockType.Hero.Id)
            {
                targetType = typeof(WebsiteHeroBlock);
            }
            else if (typeId == BlockType.Header.Id)
            {
                targetType = typeof(WebsiteHeaderBlock);
            }
            else if (typeId == BlockType.Services.Id)
            {
                targetType = typeof(WebsiteServicesBlock);
            }

            return (T)JsonSerializer.Deserialize(root.GetRawText(), targetType, options);
        }
    }

    public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
    {
        var targetType = value.GetType();
        JsonSerializer.Serialize(writer, value, targetType, options);
    }
}
