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
    public class PaymentRepositoryMongo:IRepository<Payment>
    {
        private MongoContext db;
        public PaymentRepositoryMongo(MongoContext dbcontext)
        {
            this.db = dbcontext;
        }

        public List<Payment> GetList()
        {
            var builder = new FilterDefinitionBuilder<Payment>();
            var filter = builder.Empty; // фильтр для выборки всех документов
            return new List<Payment>(db.PaymentCollection.Find(filter).ToListAsync().Result);
        }

        public Payment GetItem(int id)
        {
            return db.PaymentCollection.Find(i => i.IdPayment == id).FirstOrDefault();
        }

        public void Create(Payment order)
        {
            Payment last = db.PaymentCollection.Find(new FilterDefinitionBuilder<Payment>().Empty).SortByDescending(i => i.IdPayment).Limit(1).FirstOrDefault();
            order.IdPayment = last != null ? last.IdPayment + 1 : 1;
            db.PaymentCollection.InsertOneAsync(order).Wait();
        }

        public void Update(Payment order)
        {
            db.PaymentCollection.ReplaceOneAsync(new BsonDocument("_id", order.IdPayment), order);

        }

        public void Delete(int id)
        {
            db.PaymentCollection.DeleteOneAsync(new BsonDocument("_id", new ObjectId(id.ToString())));
        }
    }
}
