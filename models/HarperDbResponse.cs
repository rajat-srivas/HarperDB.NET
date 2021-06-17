using System;
using System.Collections.Generic;

namespace HarperNetClient.models
{
    public class HarperDbResponse
    {
        public HarperDbResponse()
        {
        }

        public string StatusCode { get; set; }

        public Content Content { get; set; }

        public string ResponseStatus { get; set; }

        public string StatusDescription { get; set; }

    }

    public class Content
    {
        public string Message { get; set; }

        public List<string> Inserted_Hashes { get; set; }

        public string SkippedHashes { get; set; }

    }
}
