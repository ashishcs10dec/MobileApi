using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace DoorStep.Api
{
    public class HelperMethods
    {
        public static string TryRetrieveToken(HttpRequestMessage request, out string token)
        {
            token = null;
            IEnumerable<string> authzHeaders;
            if (!request.Headers.TryGetValues("Authorization", out authzHeaders) || authzHeaders.Count() > 1)
            {
                return token;
            }
            var bearerToken = authzHeaders.ElementAt(0);
            token = bearerToken.StartsWith("Bearer ") ? bearerToken.Substring(7) : bearerToken;
            return token;
        }
    }
}