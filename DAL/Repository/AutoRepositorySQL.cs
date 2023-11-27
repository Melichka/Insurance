
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class AutoRepositorySQL : IRepository<Auto>
    {
        private InsuranceContext db;
        public AutoRepositorySQL(InsuranceContext dbcontext)
        {
            this.db = dbcontext;
        }

        public List<Auto> GetList()
        {
            return db.Auto.ToList();
        }

        public Auto GetItem(int id)
        {
            return db.Auto.Find(id);
        }

        public void Create(Auto item)
        {
            db.Auto.Add(item);
        }

        public void Update(Auto item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Auto item = db.Auto.Find(id);
            if (item != null)
                db.Auto.Remove(item);
        }
        public bool Save()
        {
            return db.SaveChanges() > 0;
        }
    }
}
