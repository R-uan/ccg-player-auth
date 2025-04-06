using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MongoDB.Bson;

namespace PlayerAuthServer.Utilities
{
    public class ObjectIdToStringConverter : ValueConverter<ObjectId, string>
    {
        public ObjectIdToStringConverter()
            : base(
                v => v.ToString(),            // ObjectId → string
                v => ObjectId.Parse(v))       // string → ObjectId
        { }
    }
}