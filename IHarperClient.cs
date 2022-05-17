using System;
using RestSharp;
using static HarperNetClient.Constants;

namespace HarperNetClient
{
    public interface IHarperClient
    {
        IRestResponse CreateRecord<T>(T itemToCreate);
        IRestResponse BulkOperation<T>(string dataSource, string operationType = BulkUploadOperation.CSV_URL_LOAD, string actionType = BulkUploadAction.INSERT);
        IRestResponse GetById(string id);
        IRestResponse ExecuteQuery(string sqlQuery);
        IRestResponse UpdateRecord<T>(T itemToUpdate);
        IRestResponse CreateSchema(string schema);
        IRestResponse CreateTable(string table, string schema, string hashAttribute = "id");

        IRestResponse DescribeSchema(string schema);
        IRestResponse DescribeTable(string table, string schema);

        IRestResponse DropSchema(string schema);
        IRestResponse DropTable(string table, string schema);

        IRestResponse CreateAttribute(string schema, string table, string attribute);
        IRestResponse DropAttribute(string table, string schema, string attribute);

    }
}
