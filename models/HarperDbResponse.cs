using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace HarperNetClient.models
{
    public class HarperDbResponse
    {
        public HarperDbResponse()
        {
        }

        [JsonProperty("statusCode")]
        public string StatusCode { get; set; }

        [JsonProperty("content")]
        public Content Content { get; set; }

        [JsonProperty("responseStatus")]
        public string ResponseStatus { get; set; }

        [JsonProperty("statusDescription")]
        public string StatusDescription { get; set; }

    }

    public class Content
    {
        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("inserted_hashes")]
        public List<string> Inserted_Hashes { get; set; }

        [JsonProperty("skipped_hashes")]
        public List<string> Skipped_Hashes { get; set; }

        [JsonProperty("deleted_hashes")]
        public List<string> Deleted_Hashes { get; set; }

        [JsonProperty("update_hashes")]
        public List<string> Update_Hashes { get; set; }


    }
}
