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
    public class InsuranceRepositoryMongo: IRepository<Insurance>
    {
        private MongoContext db;
        public InsuranceRepositoryMongo(MongoContext dbcontext)
        {
            this.db = dbcontext;
        }

        public List<Insurance> GetList()
        {
            var builder = new FilterDefinitionBuilder<Insurance>();
            var filter = builder.Empty; // фильтр для выборки всех документов
            return new List<Insurance>(db.InsuranceCollection.Find(filter).ToListAsync().Result);
        }

        public Insurance GetItem(int id)
        {
            return db.InsuranceCollection.Find(i => i.IdInsurance == id).FirstOrDefault();
        }

        public void Create(Insurance order)
        {
            Insurance last = db.InsuranceCollection.Find(new FilterDefinitionBuilder<Insurance>().Empty).SortByDescending(i => i.IdInsurance).Limit(1).FirstOrDefault();
            order.IdInsurance = last != null ? last.IdInsurance + 1 : 1;
            db.InsuranceCollection.InsertOneAsync(order).Wait();
        }

        public void Update(Insurance order)
        {
            db.InsuranceCollection.ReplaceOneAsync(i => i.IdInsurance == order.IdInsurance, order);

        }

        public void Delete(int id)
        {
            db.InsuranceCollection.DeleteOneAsync(i=>i.IdInsurance==id);
        }
    }
}
