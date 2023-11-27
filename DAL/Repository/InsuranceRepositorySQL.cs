
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class InsuranceRepositorySQL: IRepository<Insurance>
    {
        private InsuranceContext db;
        public InsuranceRepositorySQL(InsuranceContext dbcontext)
        {
            this.db = dbcontext;
        }

        public List<Insurance> GetList()
        {
            return db.Insurance.ToList();
        }

        public Insurance GetItem(int id)
        {
            return db.Insurance.Find(id);
        }

        public void Create(Insurance item)
        {
            db.Insurance.Add(item);
        }

        public void Update(Insurance item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Insurance item = db.Insurance.Find(id);
            if (item != null)
                db.Insurance.Remove(item);
        }
        public bool Save()
        {
            return db.SaveChanges() > 0;
        }
    }
}
