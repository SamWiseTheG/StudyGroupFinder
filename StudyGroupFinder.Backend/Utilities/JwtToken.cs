using System;
using System.Linq;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using Newtonsoft.Json;

namespace StudyGroupFinder.API.Utilities
{
    public sealed class JwtToken
    {
        [JsonProperty("token")]
        public string Token { get; set; }
        [JsonProperty("validTo")]
        public DateTime? ValidTo { get; set; }
        [JsonProperty("info")]
        public Dictionary<string, string> Info = new Dictionary<string, string>();

        internal JwtToken(JwtSecurityToken token)
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token);
            ValidTo = token.ValidTo;
            Info = token.Claims
                        .Where(x => 
                        {
                            switch (x.Type)
                            {
                                case "sub":
                                case "jti":
                                case "exp":
                                case "iss":
                                case "aud":
                                    return false;
                                default:
                                    return true;
                            };
                        })
                        .ToDictionary(x => x.Type, x => x.Value);
        }
    }
}
