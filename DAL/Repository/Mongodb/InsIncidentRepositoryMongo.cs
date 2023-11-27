using DAL.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class InsIncidentRepositoryMongo:IRepository<InsIncident>
    {
        private MongoContext db;
        public InsIncidentRepositoryMongo(MongoContext dbcontext)
        {
            this.db = dbcontext;
        }

        public List<InsIncident> GetList()
        {
            var builder = new FilterDefinitionBuilder<InsIncident>();
            var filter = builder.Empty; // фильтр для выборки всех документов
            return new List<InsIncident>(db.InsIncidentCollection.Find(filter).ToListAsync().Result);
        }

        public InsIncident GetItem(int id)
        {
            return db.InsIncidentCollection.Find(i => i.IdInsIncident == id).FirstOrDefault();
        }

        public void Create(InsIncident order)
        {
            InsIncident last = db.InsIncidentCollection.Find(new FilterDefinitionBuilder<InsIncident>().Empty).SortByDescending(i => i.IdInsIncident).Limit(1).FirstOrDefault();
            order.IdInsIncident = last != null ? last.IdInsIncident + 1 : 1;
            db.InsIncidentCollection.InsertOneAsync(order).Wait();
        }

        public void Update(InsIncident order)
        {
            db.InsIncidentCollection.ReplaceOneAsync(new BsonDocument("_id", order.IdInsIncident), order);

        }

        public void Delete(int id)
        {
            db.InsIncidentCollection.DeleteOneAsync(new BsonDocument("_id", new ObjectId(id.ToString())));
        }
    }
}
