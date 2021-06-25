# HarperDbClient_DotNetCore

Nuget Package Link: https://www.nuget.org/packages/HarperDb_Net_Client/

# HarperDbClient_DotNetCore

<h3> A Dot Net Core console app which allows performing CRUD operations to the Harper DB  🚀 🚀 <h3>

* Add the package or the project to your existing solution
  * This exposes the HarperClient class
  * This has two constructors => One with Table & One without a table name
  * In case you already have the table, use the ctor with the table name, else use the one with basic configuration and create the table using the method exposed
  * Schema and other configuration can be passed to this constructor via the HarperDbConfiguration object in the constructor
  * These are the operations supported as of now
 
     * CreateSchema
     * CreateTable
     * CreateRecord
     * CreateBulkRecord
     * GetById
     * ExecuteQuery 
     * UpdateRecord 
  
 * Following are the methods supporting the above operations
  
       * IRestResponse CreateRecord<T>(T itemToCreate)
       * IRestResponse CreateBulkRecord<T>(string csvFilePath
       * IRestResponse GetById(string id)
       * IRestResponse ExecuteQuery(string sqlQuery)
       * IRestResponse UpdateRecord<T>(T itemToUpdate)
       * IRestResponse CreateSchema(string schema)
       * IRestResponse CreateTable(string table, string schema, string hashAttribute = "id")
  
  * Use the HarperDbResponse.cs to cast the response into JSON in your application
      
      * JsonConvert.DeserializeObject<HarperNetClient.models.Content>(response.Content).Message;
  
  Note: As of 17 June 2021, this is the first version. Some bugs might be expected. Feel free to report or help.
  I Will try and update more operations and configure the app for more scalability
  
  
You can refer to the repo for a sample client app => https://github.com/rajat-srivas/HarperDB_Crud_With_DotNetCoreClient
 
 
