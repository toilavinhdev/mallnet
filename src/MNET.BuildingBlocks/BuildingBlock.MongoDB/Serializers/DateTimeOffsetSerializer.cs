using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace BuildingBlock.MongoDB.Serializers;

public class DateTimeOffsetSerializer: SerializerBase<DateTimeOffset>
{
    public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, DateTimeOffset value)
    {
        context.Writer.WriteDateTime(value.ToUniversalTime().Ticks / 10000);
    }

    public override DateTimeOffset Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        return new DateTimeOffset(context.Reader.ReadDateTime(), TimeSpan.Zero);
    }
}