using System;
using RestSharp;

namespace HarperNetClient
{
    public interface IHarperClient
    {
        IRestResponse CreateRecord<T>(T itemToCreate);
        IRestResponse CreateBulkRecord<T>(string csvFilePath);
        IRestResponse GetById(string id);
        IRestResponse ExecuteQuery(string sqlQuery);
        IRestResponse UpdateRecord<T>(T itemToUpdate);
        IRestResponse CreateSchema(string schema);
        IRestResponse CreateTable(string table, string schema, string hashAttribute = "id");

    }
}
