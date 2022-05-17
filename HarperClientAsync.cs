using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HarperNetClient.models;
using Newtonsoft.Json;
using RestSharp;
using static HarperNetClient.Constants;

namespace HarperNetClient
{
	public class HarperClientAsync : IHarperClientAsync
	{
		private RestClient _client;
		private HarperDbConfiguration _harperDbConfig;
		private string Schema_Table = "";
		public HarperClientAsync(HarperDbConfiguration config)
		{
			_harperDbConfig = config;
			_client = new RestClient(new Uri(_harperDbConfig.InstanceUrl));
		}

		public HarperClientAsync(HarperDbConfiguration config, string table)
		{
			_harperDbConfig = config;
			Schema_Table = table;
			_client = new RestClient(new Uri(_harperDbConfig.InstanceUrl));
		}

		private RestRequest CreateBaseRequest()
		{
			var request = new RestRequest(Method.POST);
			request.AddHeader("Content-Type", "application/json");
			request.AddHeader("Authorization", $"Basic {_harperDbConfig.AuthToken}");

			return request;
		}

		/// <summary>
		/// Create New HarperDB Schema, accepts a string for the Schema Name
		/// </summary>
		/// <param name="schema"></param>
		/// <returns></returns>
		public async Task<IRestResponse> CreateSchemaAsync(string schema)
		{
			try
			{
				Console.WriteLine($"Creating new Schema: {schema}");
				var requestBody = new HarperRequest()
				{
					Operation = "create_schema",
					Schema = schema,
				};

				var request = CreateBaseRequest();
				request.AddParameter("application/json",
					JsonConvert.SerializeObject(requestBody), ParameterType.RequestBody);

				var response = await _client.ExecuteAsync(request);
				return response;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message.ToString());
				return null;
			}
		}

		/// <summary>
		/// Create new Table, accepts string field for Table name and schema. Hash attribute is optional with default value as id
		/// </summary>
		/// <param name="table"></param>
		/// <param name="schema"></param>
		/// <param name="hashAttribute"></param>
		/// <returns></returns>
		public async Task<IRestResponse> CreateTableAsync(string table, string schema, string hashAttribute = "id")
		{
			try
			{
				Console.WriteLine($"Creating new Table: {schema}.{table}");
				var requestBody = new HarperRequest()
				{
					Operation = "create_table",
					Schema = schema,
					Table = table,
					Hash_Attribute = hashAttribute
				};

				var request = CreateBaseRequest();
				request.AddParameter("application/json",
					JsonConvert.SerializeObject(requestBody), ParameterType.RequestBody);

				var response = await _client.ExecuteAsync(request);
				return response;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message.ToString());
				return null;
			}
		}

		/// <summary>
		/// Create Generic T record in HarperDb
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="itemToCreate"></param>
		/// <returns></returns>
		public async Task<IRestResponse> CreateRecordAsync<T>(T itemToCreate)
		{
			try
			{
				Console.WriteLine($"Creating record");
				List<Object> obj = new List<Object>();
				obj.Add(itemToCreate);
				var requestBody = new HarperRequest()
				{
					Operation = "insert",
					Schema = _harperDbConfig.Schema,
					Table = Schema_Table,
					Records = obj
				};

				var request = CreateBaseRequest();
				request.AddParameter("application/json",
					JsonConvert.SerializeObject(requestBody), ParameterType.RequestBody);

				var response = await _client.ExecuteAsync(request);
				return response;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message.ToString());
				return null;
			}
		}

		/// <summary>
		/// dataSource can be the csv url, csv file path on the HaperDB host or the csv data itself based upon the operation types
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="dataSource"></param>
		/// <param name="operationType"></param>
		/// <param name="actionType"></param>
		/// <returns></returns>
		public async Task<IRestResponse> BulkOperationAsync<T>(string dataSource, string operationType = BulkUploadOperation.CSV_URL_LOAD, string actionType = BulkUploadAction.INSERT)
		{
			try
			{
				Console.WriteLine($"Bulk import starting from: {dataSource}");
				var requestBody = new HarperRequest()
				{
					Operation = operationType,
					Schema = _harperDbConfig.Schema,
					Table = Schema_Table,
					Action = actionType
				};

				if (operationType.Equals(BulkUploadOperation.CSV_DATA_LOAD))
					requestBody.Data = dataSource;
				if (operationType.Equals(BulkUploadOperation.CSV_URL_LOAD))
					requestBody.Csv_URL = dataSource;
				if (operationType.Equals(BulkUploadOperation.CSV_FILE_LOAD))
					requestBody.File_Path = dataSource;

				var request = CreateBaseRequest();
				request.AddParameter("application/json",
					JsonConvert.SerializeObject(requestBody), ParameterType.RequestBody);

				var response = await _client.ExecuteAsync(request);
				return response;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message.ToString());
				return null;
			}
		}


		/// <summary>
		/// Get item by id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public async Task<IRestResponse> GetByIdAsync(string id)
		{
			try
			{
				Console.WriteLine($"Get by id: {id}");
				var requestBody = new HarperRequest()
				{
					Operation = "sql",
					SQL = $"SELECT * FROM {_harperDbConfig.Schema}.{Schema_Table} WHERE id = \"{id}\""
				};

				var request = CreateBaseRequest();
				request.AddParameter("application/json", JsonConvert.SerializeObject(requestBody), ParameterType.RequestBody);
				var response = await _client.ExecuteAsync(request);
				return response;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message.ToString());
				return null;
			}
		}

		/// <summary>
		/// Execute any query, accepts string query as the param
		/// </summary>
		/// <param name="sqlQuery"></param>
		/// <returns></returns>
		public async Task<IRestResponse> ExecuteQueryAsync(string sqlQuery)
		{
			try
			{
				Console.WriteLine($"Exwcuting query: {sqlQuery}");
				var requestBody = new HarperRequest()
				{
					Operation = "sql",
					SQL = sqlQuery
				};

				var request = CreateBaseRequest();
				request.AddParameter("application/json", JsonConvert.SerializeObject(requestBody), ParameterType.RequestBody);
				var response = await _client.ExecuteAsync(request);
				return response;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message.ToString());
				return null;
			}
		}

		/// <summary>
		/// Update generic record T 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="itemToUpdate"></param>
		/// <returns></returns>
		public async Task<IRestResponse> UpdateRecordAsync<T>(T itemToUpdate)
		{
			try
			{
				Console.WriteLine($"Updating record");
				List<Object> obj = new List<Object>();
				obj.Add(itemToUpdate);
				var requestBody = new HarperRequest()
				{
					Operation = "update",
					Schema = _harperDbConfig.Schema,
					Table = Schema_Table,
					Records = obj
				};

				var request = CreateBaseRequest();
				request.AddParameter("application/json",
					JsonConvert.SerializeObject(requestBody), ParameterType.RequestBody);

				var response = await _client.ExecuteAsync(request);
				return response;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message.ToString());
				return null;
			}
		}

		public async Task<IRestResponse> DescribeSchemaAsync(string schema)
		{
			try
			{
				var requestBody = new HarperRequest()
				{
					Operation = DDLOperations.DESCRIBE_SCHEMA,
					Schema = schema
				};

				var request = CreateBaseRequest();
				request.AddParameter("application/json",
					JsonConvert.SerializeObject(requestBody), ParameterType.RequestBody);

				var response = await _client.ExecuteAsync(request);
				return response;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message.ToString());
				return null;
			}
		}

		public async Task<IRestResponse> DescribeTableAsync(string table, string schema)
		{
			try
			{
				var requestBody = new HarperRequest()
				{
					Operation = DDLOperations.DESCRIBE_TABLE,
					Schema = schema,
					Table = table
				};

				var request = CreateBaseRequest();
				request.AddParameter("application/json",
					JsonConvert.SerializeObject(requestBody), ParameterType.RequestBody);

				var response = await _client.ExecuteAsync(request);
				return response;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message.ToString());
				return null;
			}
		}

		public async Task<IRestResponse> DropSchemaAsync(string schema)
		{
			try
			{
				var requestBody = new HarperRequest()
				{
					Operation = DDLOperations.DROP_SCHEMA,
					Schema = schema
				};

				var request = CreateBaseRequest();
				request.AddParameter("application/json",
					JsonConvert.SerializeObject(requestBody), ParameterType.RequestBody);

				var response = await _client.ExecuteAsync(request);
				return response;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message.ToString());
				return null;
			}
		}

		public async Task<IRestResponse> DropTableAsync(string table, string schema)
		{
			try
			{
				var requestBody = new HarperRequest()
				{
					Operation = DDLOperations.DROP_TABLE,
					Schema = schema,
					Table = table
				};

				var request = CreateBaseRequest();
				request.AddParameter("application/json",
					JsonConvert.SerializeObject(requestBody), ParameterType.RequestBody);

				var response = await _client.ExecuteAsync(request);
				return response;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message.ToString());
				return null;
			}
		}

		public async Task<IRestResponse> CreateAttributeAsync(string schema, string table, string attribute)
		{
			try
			{
				var requestBody = new HarperRequest()
				{
					Operation = DDLOperations.CREATE_ATTRIBUTE,
					Schema = schema,
					Table = table,
					Attribute = attribute
				};

				var request = CreateBaseRequest();
				request.AddParameter("application/json",
					JsonConvert.SerializeObject(requestBody), ParameterType.RequestBody);

				var response = await _client.ExecuteAsync(request);
				return response;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message.ToString());
				return null;
			}
		}

		public async Task<IRestResponse> DropAttributeAsync(string table, string schema, string attribute)
		{
			try
			{
				var requestBody = new HarperRequest()
				{
					Operation = DDLOperations.DROP_ATTRIBUTE,
					Schema = schema,
					Table = table,
					Attribute = attribute
				};

				var request = CreateBaseRequest();
				request.AddParameter("application/json",
					JsonConvert.SerializeObject(requestBody), ParameterType.RequestBody);

				var response = await _client.ExecuteAsync(request);
				return response;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message.ToString());
				return null;
			}
		}
	}
}
