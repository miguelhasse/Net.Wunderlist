using Newtonsoft.Json;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;

namespace System.Net.Wunderlist.Internal
{
    internal sealed class ResourceCreationConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
#if PORTABLE
            return typeof(Resource).GetTypeInfo().IsAssignableFrom(objectType.GetTypeInfo()) || objectType == typeof(ResourcePart) || objectType == typeof(ResourceRevision);
#else
            return typeof(Resource).IsAssignableFrom(objectType) || objectType == typeof(ResourcePart) || objectType == typeof(ResourceRevision);
#endif
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.StartObject)
            {
                var jtoken = Newtonsoft.Json.Linq.JToken.ReadFrom(reader);

                switch (objectType.Name)
                {
                    case "Comment": return new Comment(jtoken);
                    case "File": return new File(jtoken);
                    case "Folder": return new Folder(jtoken);
                    case "List": return new List(jtoken);
                    case "Membership": return new Membership(jtoken);
                    case "Note": return new Note(jtoken);
                    case "Reminder": return new Reminder(jtoken);
                    case "MainTask": return new MainTask(jtoken);
                    case "SubTask": return new SubTask(jtoken);
                    case "User": return new User(jtoken);
                    case "Webhook": return new Webhook(jtoken);
                    case "Positions": return new Positions(jtoken);
                    case "ResourcePart": return new ResourcePart(jtoken);
                    case "ResourceRevision": return new ResourceRevision(jtoken);
                }
                throw new JsonSerializationException(String.Format(CultureInfo.InvariantCulture,
                    "Unexpected object type when converting resource: {0}", objectType));
            }
            throw new JsonSerializationException(String.Format(CultureInfo.InvariantCulture, 
                "Unexpected token when converting resource: {0}", reader.TokenType));
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
