using System;
using System.Collections.Generic;
using HarperNetClient.models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;

namespace HarperNetClient
{
    public class HarperClient : IHarperClient
    {

        private RestClient _client;
        private HarperDbConfiguration _harperDbConfig;
        private string Schema_Table = "";
        private ILogger<HarperClient> _logger;
        public HarperClient(HarperDbConfiguration config)
        {
            _harperDbConfig = config;
            _client = new RestClient(new Uri(_harperDbConfig.InstanceUrl));
        }

        public HarperClient(HarperDbConfiguration config, string table)
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


        public IRestResponse CreateSchema(string schema)
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

                var response = _client.Execute(request);
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return null;
            }
        }

        public IRestResponse CreateTable(string table, string schema, string hashAttribute = "id")
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

                var response = _client.Execute(request);
                Console.WriteLine($"{response.Content}");
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return null;
            }
        }

        public IRestResponse CreateRecord<T>(T itemToCreate)
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

                var response = _client.Execute(request);
                Console.WriteLine($"{response.Content}");
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return null;
            }
        }

        public IRestResponse CreateBulkRecord<T>(string csvFilePath)
        {
            try
            {
                Console.WriteLine($"Bulk import starting from: {csvFilePath}");
              var requestBody = new HarperRequest()
                {
                    Operation = "csv_file_load",
                    Schema = _harperDbConfig.Schema,
                    Table = Schema_Table,
                    File_Path = csvFilePath
                };

                var request = CreateBaseRequest();
                request.AddParameter("application/json",
                    JsonConvert.SerializeObject(requestBody), ParameterType.RequestBody);

                var response = _client.Execute(request);
                Console.WriteLine($"{response.Content}");
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return null;
            }
        }



        public IRestResponse GetById(string id)
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
                var response = _client.Execute(request);
                Console.WriteLine($"{response.Content}");
                Console.WriteLine(response.Content);

                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return null;
            }
        }

        public IRestResponse ExecuteQuery(string sqlQuery)
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
                var response = _client.Execute(request);
                Console.WriteLine($"{response.Content}");
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return null;
            }
        }

        public IRestResponse UpdateRecord<T>(T itemToUpdate)
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

                var response = _client.Execute(request);
                Console.WriteLine($"{response.Content}");
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
