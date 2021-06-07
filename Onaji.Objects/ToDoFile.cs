using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Onaji.Objects
{
    public class ToDoFile
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "location")]
        public string Location { get; set; }

        [JsonProperty(PropertyName = "size")]
        public long Size { get; set; }

        [JsonProperty(PropertyName = "lastModified")]
        public DateTime LastModified { get; set; }

        [JsonProperty(PropertyName = "sha256")]
        public string checksum { get; set; }
    }
}
