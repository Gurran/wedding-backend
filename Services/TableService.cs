using Azure.Data.Tables;
using Azure;
using System;
using System.Collections.Generic;

namespace Wedding.Web.Services
{

    public class TableService
    {
        private readonly TableServiceClient _serviceClient;

        public TableService(string connectionString)
        {
            _serviceClient = new TableServiceClient(connectionString);
        }

        public TableClient GetTableClient(string tableName)
        {
            var tableClient = _serviceClient.GetTableClient(tableName);
            tableClient.CreateIfNotExists();
            return tableClient;
        }

        public void AddEntity<T>(string tableName, T entity) where T : class, ITableEntity
        {
            var tableClient = GetTableClient(tableName);
            tableClient.AddEntity(entity);
        }

        public List<T> QueryEntities<T>(string tableName, string partitionKey) where T : class, ITableEntity, new()
        {
            var tableClient = GetTableClient(tableName);
            Pageable<T> queryResults = tableClient.Query<T>(e => e.PartitionKey == partitionKey);
            var results = new List<T>();
            foreach (T entity in queryResults)
            {
                results.Add(entity);
            }
            return results;
        }

        public List<T> GetAllEntities<T>(string tableName) where T : class, ITableEntity, new()
        {
            var tableClient = GetTableClient(tableName);
            Pageable<T> queryResults = tableClient.Query<T>();
            var results = new List<T>();
            foreach (T entity in queryResults)
            {
                results.Add(entity);
            }
            return results;
        }

        public void DeleteEntity(string tableName, string partitionKey, string rowKey)
        {
            var tableClient = GetTableClient(tableName);
            tableClient.DeleteEntity(partitionKey, rowKey);
        }
    }

}