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
    public class AutoRepositoryMongo : IRepository<Auto>
    {
        private MongoContext db;
        public AutoRepositoryMongo(MongoContext dbcontext)
        {
            this.db = dbcontext;
        }


        public List<Auto> GetList()
        {
            var builder = new FilterDefinitionBuilder<Auto>();
            var filter = builder.Empty; // фильтр для выборки всех документов
            return new List<Auto>(db.AutoCollection.Find(filter).ToListAsync().Result);
        }

        public Auto GetItem(int id)
        {
            return db.AutoCollection.Find(i => i.IdAuto == id).FirstOrDefault();
        }

        public void Create(Auto order)
        {
            Auto last = db.AutoCollection.Find(new FilterDefinitionBuilder<Auto>().Empty).SortByDescending(i => i.IdAuto).Limit(1).FirstOrDefault();
            order.IdAuto = last != null ? last.IdAuto + 1 : 1;
            db.AutoCollection.InsertOneAsync(order).Wait();
        }

        public void Update(Auto order)
        {
            db.AutoCollection.ReplaceOneAsync(new BsonDocument("_id", order.IdAuto), order);

        }

        public void Delete(int id)
        {
            db.AutoCollection.DeleteOneAsync(new BsonDocument("_id", new ObjectId(id.ToString())));
        }

    }
}
