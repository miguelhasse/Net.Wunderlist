using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Linq;

namespace System.Net.Wunderlist
{
    public sealed class ServiceException : HttpRequestException
    {
        internal ServiceException(int statusCode, JObject error)
            : base((error != null) ? (string)error["message"] : null)
        {
            if (error != null)
            {
                foreach (var keypair in error)
                {
                    switch (keypair.Key)
                    {
                        case "type":
                            this.ErrorType = (string)keypair.Value;
                            break;

                        case "translation_key":
                            this.LocalizationKey = (string)keypair.Value;
                            break;

                        case "message":
                            break;

                        default:
                            var value = keypair.Value.ToString();
                            Data.Add(keypair.Key, value);
                            break;
                    }
                }
            }
            this.StatusCode = statusCode;
        }

        internal ServiceException(int statusCode, string error) : base(error)
        {
            this.StatusCode = statusCode;
        }

        public int StatusCode { get; private set; }

        public string ErrorType { get; private set; }

        public string LocalizationKey { get; private set; }
    }
}
