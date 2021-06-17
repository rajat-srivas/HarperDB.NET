using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace HarperNetClient.models
{
    public class HarperRequest
    {
        public HarperRequest()
        {
        }

        [JsonProperty("operation")]
        public string Operation { get; set; }

        [JsonProperty("schema")]
        public string Schema { get; set; }

        [JsonProperty("table")]
        public string Table { get; set; }

        [JsonProperty("records")]
        public List<Object> Records  { get; set; }

        [JsonProperty("sql")]
        public string SQL { get; set; }

        [JsonProperty("hash_attribute")]
        public string Hash_Attribute { get; set; }

        [JsonProperty("file_path")]
        public string File_Path { get; set; }
    }
}
