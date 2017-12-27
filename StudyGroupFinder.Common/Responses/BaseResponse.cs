using System;
using Newtonsoft.Json;

namespace StudyGroupFinder.Common.Responses
{
    public class BaseResponse 
    {
        [JsonProperty("success")]
        public bool Success { get; set; } = false; 
        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
