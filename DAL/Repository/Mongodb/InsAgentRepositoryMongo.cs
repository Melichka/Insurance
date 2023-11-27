using DAL.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class InsAgentRepositoryMongo:IRepository<InsAgent>
    {
        private MongoContext db;

        public InsAgentRepositoryMongo(MongoContext dbcontext)
        {
            this.db = dbcontext;
        }

        public List<InsAgent> GetList()
        {
            var builder = new FilterDefinitionBuilder<InsAgent>();
            var filter = builder.Empty; // фильтр для выборки всех документов
            return new List<InsAgent>(db.InsAgentCollection.Find(filter).ToListAsync().Result);
        }

        public InsAgent GetItem(int id)
        {
            return db.InsAgentCollection.Find(i => i.IdInsAgent == id).FirstOrDefault();
        }

        public void Create(InsAgent order)
        {
            InsAgent last = db.InsAgentCollection.Find(new FilterDefinitionBuilder<InsAgent>().Empty).SortByDescending(i => i.IdInsAgent).Limit(1).FirstOrDefault();
            order.IdInsAgent = last != null ? last.IdInsAgent + 1 : 1;
            db.InsAgentCollection.InsertOneAsync(order).Wait();
        }

        public void Update(InsAgent order)
        {
            db.InsAgentCollection.ReplaceOneAsync(new BsonDocument("_id", order.IdInsAgent), order);

        }

        public void Delete(int id)
        {
            db.InsAgentCollection.DeleteOneAsync(new BsonDocument("_id", new ObjectId(id.ToString())));
        }
    }
}
