using Newtonsoft.Json;

namespace N90.Application.Common.Serializers;

public interface IJsonSerializationSettingsProvider
{
    JsonSerializerSettings Get(bool clone = false);
}