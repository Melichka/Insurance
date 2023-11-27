using DAL.Interfaces;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class TypeInsRepositoryMongo:IRepository<TypeIns>
    {
        private MongoContext db;
        public TypeInsRepositoryMongo(MongoContext dbcontext)
        {
            this.db = dbcontext;
        }

        public List<TypeIns> GetList()
        {
            var builder = new FilterDefinitionBuilder<TypeIns>();
            var filter = builder.Empty; // фильтр для выборки всех документов
            return new List<TypeIns>(db.TypeInsCollection.Find(filter).ToListAsync().Result);
        }

        public TypeIns GetItem(int id)
        {
            return db.TypeInsCollection.Find(i => i.IdTypeIns == id).FirstOrDefault();
        }

        public void Create(TypeIns order)
        {
            TypeIns last = db.TypeInsCollection.Find(new FilterDefinitionBuilder<TypeIns>().Empty).SortByDescending(i => i.IdTypeIns).Limit(1).FirstOrDefault();
            order.IdTypeIns = last != null ? last.IdTypeIns + 1 : 1;
            db.TypeInsCollection.InsertOneAsync(order).Wait();
        }

        public void Update(TypeIns order)
        {
            db.TypeInsCollection.ReplaceOneAsync(new BsonDocument("_id", order.IdTypeIns), order);

        }

        public void Delete(int id)
        {
            db.TypeInsCollection.DeleteOneAsync(new BsonDocument("_id", new ObjectId(id.ToString())));
        }
    }
}
