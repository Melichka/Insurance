
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class InsuranceTypeRepositorySQL : IRepository<InsuranceType>
    {
        private InsuranceContext db;
        public InsuranceTypeRepositorySQL(InsuranceContext dbcontext)
        {
            this.db = dbcontext;
        }

        public List<InsuranceType> GetList()
        {
            return db.InsuranceType.ToList();
        }

        public InsuranceType GetItem(int id)
        {
            return db.InsuranceType.Find(id);
        }

        public void Create(InsuranceType item)
        {
            db.InsuranceType.Add(item);
        }

        public void Update(InsuranceType item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            InsuranceType item = db.InsuranceType.Find(id);
            if (item != null)
                db.InsuranceType.Remove(item);
        }
        public bool Save()
        {
            return db.SaveChanges() > 0;
        }
    }
}
