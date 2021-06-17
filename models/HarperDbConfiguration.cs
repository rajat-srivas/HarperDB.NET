using System;
namespace HarperNetClient.models
{
    public class HarperDbConfiguration
    {
        public HarperDbConfiguration()
        {

        }

        public string InstanceUrl { get; set; }

        public string AuthToken { get; set; }

        public string Schema { get; set; }

    }
}
