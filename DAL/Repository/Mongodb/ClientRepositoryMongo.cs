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
    public class ClientRepositoryMongo:IRepository<Client>
    {
        private MongoContext db;
        public ClientRepositoryMongo(MongoContext dbcontext)
        {
            this.db = dbcontext;
        }

        public List<Client> GetList()
        {
            var builder = new FilterDefinitionBuilder<Client>();
            var filter = builder.Empty; // фильтр для выборки всех документов
            return new List<Client>(db.ClientCollection.Find(filter).ToListAsync().Result);
        }

        public Client GetItem(int id)
        {
            return db.ClientCollection.Find(i => i.IdClient == id).FirstOrDefault();
        }

        public void Create(Client order)
        {
            Client last = db.ClientCollection.Find(new FilterDefinitionBuilder<Client>().Empty).SortByDescending(i => i.IdClient).Limit(1).FirstOrDefault();
            order.IdClient = last != null ? last.IdClient + 1 : 1;
            db.ClientCollection.InsertOneAsync(order).Wait();
        }

        public void Update(Client order)
        {
            db.ClientCollection.ReplaceOneAsync(new BsonDocument("_id", order.IdClient), order);

        }

        public void Delete(int id)
        {
            db.ClientCollection.DeleteOneAsync(new BsonDocument("_id", new ObjectId(id.ToString())));
        }

    }
}
