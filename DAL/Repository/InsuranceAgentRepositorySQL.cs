
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class InsuranceAgentRepositorySQL : IRepository<InsuranceAgent>
    {
        private InsuranceContext db;

        public InsuranceAgentRepositorySQL(InsuranceContext dbcontext)
        {
            this.db = dbcontext;
        }

        public List<InsuranceAgent> GetList()
        {
            return db.InsuranceAgent.ToList();
        }

        public InsuranceAgent GetItem(int id)
        {
            return db.InsuranceAgent.Find(id);
        }

        public void Create(InsuranceAgent InsAgent)
        {
            db.InsuranceAgent.Add(InsAgent);
        }

        public void Update(InsuranceAgent InsAgent)
        {
            db.Entry(InsAgent).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            InsuranceAgent InsAgent = db.InsuranceAgent.Find(id);
            if (InsAgent != null)
                db.InsuranceAgent.Remove(InsAgent);
        }
    }
}
