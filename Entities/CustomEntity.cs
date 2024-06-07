using Azure;
using Azure.Data.Tables;
using System;

namespace Wedding.Web.Entities
{

    public class CustomEntity : ITableEntity
    {
        public string? PartitionKey { get; set; }
        public string? RowKey { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
