
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class IncidentRepositorySQL:IRepository<Incident>
    {
        private InsuranceContext db;
        public IncidentRepositorySQL(InsuranceContext dbcontext)
        {
            this.db = dbcontext;
        }

        public List<Incident> GetList()
        {
            return db.Incident.ToList();
        }

        public Incident GetItem(int id)
        {
            return db.Incident.Find(id);
        }

        public void Create(Incident item)
        {
            db.Incident.Add(item);
        }

        public void Update(Incident item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Incident item = db.Incident.Find(id);
            if (item != null)
                db.Incident.Remove(item);
        }
        public bool Save()
        {
            return db.SaveChanges() > 0;
        }
    }
}
