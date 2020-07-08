using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreDemo.Utils
{
    public class AuthJwtEncoder
    {
        private static readonly string key = "jwt";
        private static readonly string secret = "!QAZ@WSX1qaz2wsx";
        private static readonly IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
        private static readonly IJsonSerializer serializer = new JsonNetSerializer();
        private static readonly IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
        private static readonly IDateTimeProvider provider = new UtcDateTimeProvider();
        private static readonly IJwtValidator validator = new JwtValidator(serializer, provider);
        private static readonly IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);
        private static readonly IJwtDecoder decoder = new JwtDecoder(serializer, validator, urlEncoder, algorithm);
        private static readonly JsonSerializerSettings serializerSettings = new JsonSerializerSettings()
        {
            DateFormatString = "yyyy-MM-ddTHH:mm:ss.FFFFFFFK",
            Converters = { new StringEnumConverter() }
        };
      
        public static string Encode<T>(T jwtObj)
        {
            Dictionary<string, string> payload = new Dictionary<string, string>()
            {
                { key, JsonConvert.SerializeObject(jwtObj, serializerSettings)}
            };
            return encoder.Encode(payload, secret);
        }

        public static T Decode<T>(string jwt)
        {
            var jDictionary = JsonConvert.DeserializeObject<JObject>(decoder.Decode(jwt));
            var json = jDictionary.Property(key).Value.ToObject<string>();
            return JsonConvert.DeserializeObject<T>(json, serializerSettings);
        }
    }
}
