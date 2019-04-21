
using MindMiners.Domain.Interface.Generic;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace MindMiners.Infra.Repository.Generic
{
    public class RepositoryGeneric<T> : InterfaceGeneric<T>, IDisposable where T : class
    {

        private string tableName { get; set; }

        public IMongoClient client;
        public IMongoDatabase database;
        public IMongoCollection<T> collection;
        public RepositoryGeneric(string tableName)
        {
            this.client = new MongoClient("mongodb://mm_user:TesteMM123!@ds019950.mlab.com:19950/mm_fomurilo");
            this.database = client.GetDatabase("mm_fomurilo");
            this.collection = database.GetCollection<T>(tableName);
        }


        public virtual void Add(T document)
        {
            this.collection.InsertOne(document);
        }

        public void Dispose()
        {
        }

        public IList<T> List()
        {
            var items = this.collection.Find(FilterDefinition<T>.Empty)
                    .Project<T>("{_id: 0, StrFile :0}").ToList() ;


            return items;
        }
    }
}
