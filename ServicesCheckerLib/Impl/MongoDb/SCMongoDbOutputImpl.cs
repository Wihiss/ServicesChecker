﻿using MongoDB.Driver;
using ServicesCheckerLib.Def;
using ServicesCheckerLib.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ServicesCheckerLib.Impl.MongoDb
{
    internal class SCMongoDbOutputImpl : ISCOutput
    {
        private readonly MongoClient _mongoClient;
        private readonly IMongoCollection<OutputElement> _outputRecordCollection;

        public SCMongoDbOutputImpl(string host, int port, string dbName, string collectionName, string user, string password)
        {
            if (string.IsNullOrEmpty(host))
                throw new ArgumentException("host cannot be blank");
            if (port <= 0)
                throw new ArgumentException("port cannot be less or equal to 0");
            if (string.IsNullOrEmpty(dbName))
                throw new ArgumentException("dbName cannot be blank");
            if (string.IsNullOrEmpty(collectionName))
                throw new ArgumentException("collectionName cannot be blank");

            if (string.IsNullOrEmpty(user))
                _mongoClient = new MongoClient($"mongodb://{host}:{port}");
            else
                _mongoClient = new MongoClient($"mongodb://{user}:{password}@{host}:{port}");

            var db = _mongoClient.GetDatabase(dbName);
            if (db == null)
                throw new InvalidOperationException("Database " + dbName + " not found");

            _outputRecordCollection = db.GetCollection<OutputElement>(collectionName);
        }

        public async Task Write(DateTime timestamp, ServiceDef serviceDef, CheckResult r)
        {
            OutputElement outputElement = new OutputElement()
            {
                Timestamp = timestamp,
                ServiceName = serviceDef.Name,
                Status = r.Status,
                LastError = r.LastError == null? null : r.LastError.Message
            };

            await _outputRecordCollection.InsertOneAsync(outputElement);
        }
    }
}
