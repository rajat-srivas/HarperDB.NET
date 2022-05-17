using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static HarperNetClient.Constants;

namespace HarperNetClient
{
	public interface IHarperClientAsync
	{
        Task<IRestResponse> CreateRecordAsync<T>(T itemToCreate);
        Task<IRestResponse> BulkOperationAsync<T>(string dataSource, string operationType = BulkUploadOperation.CSV_URL_LOAD, string actionType = BulkUploadAction.INSERT);
        Task<IRestResponse> GetByIdAsync(string id);
        Task<IRestResponse> ExecuteQueryAsync(string sqlQuery);
        Task<IRestResponse> UpdateRecordAsync<T>(T itemToUpdate);
        Task<IRestResponse> CreateSchemaAsync(string schema);
        Task<IRestResponse> CreateTableAsync(string table, string schema, string hashAttribute = "id");


        Task<IRestResponse> DescribeSchemaAsync(string schema);
        Task<IRestResponse> DescribeTableAsync(string table, string schema);

        Task<IRestResponse> DropSchemaAsync(string schema);
        Task<IRestResponse> DropTableAsync(string table, string schema);

        Task<IRestResponse> CreateAttributeAsync(string schema, string table, string attribute);
        Task<IRestResponse> DropAttributeAsync(string table, string schema, string attribute);
    }
}
