
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class PaymentRepositorySQL:IRepository<Payment>
    {
        private InsuranceContext db;
        public PaymentRepositorySQL(InsuranceContext dbcontext)
        {
            this.db = dbcontext;
        }

        public List<Payment> GetList()
        {
            return db.Payment.ToList();
        }

        public Payment GetItem(int id)
        {
            return db.Payment.Find(id);
        }

        public void Create(Payment item)
        {
            db.Payment.Add(item);
        }

        public void Update(Payment item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Payment item = db.Payment.Find(id);
            if (item != null)
                db.Payment.Remove(item);
        }
        public bool Save()
        {
            return db.SaveChanges() > 0;
        }
    }
}
