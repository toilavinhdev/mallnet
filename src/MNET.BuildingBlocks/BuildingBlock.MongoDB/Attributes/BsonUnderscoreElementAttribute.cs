using System.Runtime.CompilerServices;
using BuildingBlock.DataExtensions;
using MongoDB.Bson.Serialization.Attributes;

namespace BuildingBlock.MongoDB.Attributes;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
public class BsonUnderscoreElementAttribute([CallerMemberName] string elementName = null!)
    : BsonElementAttribute(elementName.ToUnderscoreCase());