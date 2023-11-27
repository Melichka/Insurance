
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class IncidentStatusRepositorySQL : IRepository<IncidentStatus>
    {
        private InsuranceContext db;
        public IncidentStatusRepositorySQL(InsuranceContext dbcontext)
        {
            this.db = dbcontext;
        }

        public List<IncidentStatus> GetList()
        {
            return db.IncidentStatus.ToList();
        }

        public IncidentStatus GetItem(int id)
        {
            return db.IncidentStatus.Find(id);
        }

        public void Create(IncidentStatus item)
        {
            db.IncidentStatus.Add(item);
        }

        public void Update(IncidentStatus item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            IncidentStatus item = db.IncidentStatus.Find(id);
            if (item != null)
                db.IncidentStatus.Remove(item);
        }
        public bool Save()
        {
            return db.SaveChanges() > 0;
        }
    }
}
