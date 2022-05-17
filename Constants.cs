using System;
using System.Collections.Generic;
using System.Text;

namespace HarperNetClient
{
	public class Constants
	{
		public struct BulkUploadOperation
		{
			public const string CSV_DATA_LOAD = "csv_data_load";
			public const string CSV_FILE_LOAD = "csv_file_load";
			public const string CSV_URL_LOAD = "csv_url_load";
		}

		public struct BulkUploadAction
		{
			public const string INSERT = "insert";
			public const string UPDATE = "update";
			public const string UPSERT = "upsert";
		}

		public struct DDLOperations
		{
			public const string DESCRIBE_SCHEMA = "describe_schema";
			public const string DESCRIBE_TABLE = "describe_table";
			public const string DROP_SCHEMA = "drop_schema";
			public const string DROP_TABLE = "drop_table";
			public const string CREATE_ATTRIBUTE = "create_attribute";
			public const string DROP_ATTRIBUTE = "drop_attribute";
		}
	}
}
