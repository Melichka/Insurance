
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DbReposSQL : IDbRepository
    {
        private InsuranceContext db;
        private AutoRepositorySQL AutoRepository;
        private ClientRepositorySQL ClientRepository;
        private InsuranceAgentRepositorySQL InsuranceAgentRepository;
        private IncidentRepositorySQL IncidentRepository;
        private InsuranceRepositorySQL InsuranceRepository;
        private PaymentRepositorySQL PaymentRepository;
        private InsuranceTypeRepositorySQL InsuranceTypeRepository;
        private ReportsRepositorySQL reportsRepository;
        private IncidentStatusRepositorySQL IncidentStatusRepository;

        public DbReposSQL()
        {
            db = new InsuranceContext();
        }

        public IRepository<Auto> Auto
        {
            get
            {
                if (AutoRepository == null)
                    AutoRepository = new AutoRepositorySQL(db);
                return AutoRepository;
            }
        }

        public IRepository<Client> Client
        {
            get
            {
                if (ClientRepository == null)
                    ClientRepository = new ClientRepositorySQL(db);
                return ClientRepository;
            }
        }

        public IRepository<InsuranceAgent> InsuranceAgent
        {
            get
            {
                if (InsuranceAgentRepository == null)
                    InsuranceAgentRepository = new InsuranceAgentRepositorySQL(db);
                return InsuranceAgentRepository;
            }
        }
        public IRepository<Incident> Incident
        {
            get
            {
                if (IncidentRepository == null)
                    IncidentRepository = new IncidentRepositorySQL(db);
                return IncidentRepository;
            }
        }
        public IRepository<Insurance> Insurance
        {
            get
            {
                if (InsuranceRepository == null)
                    InsuranceRepository = new InsuranceRepositorySQL(db);
                return InsuranceRepository;
            }
        }
        public IRepository<Payment> Payment
        {
            get
            {
                if (PaymentRepository == null)
                    PaymentRepository = new PaymentRepositorySQL(db);
                return PaymentRepository;
            }
        }
        public IRepository<InsuranceType> InsuranceType
        {
            get
            {
                if (InsuranceTypeRepository == null)
                    InsuranceTypeRepository = new InsuranceTypeRepositorySQL(db);
                return InsuranceTypeRepository;
            }
        }

        public IRepository<IncidentStatus> IncidentStatus
        {
            get
            {
                if (IncidentStatusRepository == null)
                    IncidentStatusRepository = new IncidentStatusRepositorySQL(db);
                return IncidentStatusRepository;
            }
        }

        public IReportsRepository Reports
        {
            get
            {
                if (reportsRepository == null)
                    reportsRepository = null; // new ReportsRepositorySQL(db);
                return reportsRepository;
            }
        }

        public int Save()
        {
            return db.SaveChanges();
        }
    }
}
