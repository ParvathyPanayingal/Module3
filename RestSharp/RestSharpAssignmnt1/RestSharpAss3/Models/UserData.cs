﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpAss3
{
    public class UserData
    {
        [JsonProperty("userId")]
        public string? UserId { get; set; }

        [JsonProperty("id")]
        public int? Id { get; set; }

        [JsonProperty("title")]
        public string? Title { get; set; }

        [JsonProperty("body")]
        public string? Body { get; set; }
    }

    
}
