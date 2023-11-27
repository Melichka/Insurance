using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class DbReposMongo:IDbRepository
    {
        private MongoContext db;
        private AutoRepositoryMongo AutoRepository;
        private ClientRepositoryMongo ClientRepository;
        private InsAgentRepositoryMongo InsAgentRepository;
        private InsIncidentRepositoryMongo InsIncidentRepository;
        private InsuranceRepositoryMongo InsuranceRepository;
        private PaymentRepositoryMongo PaymentRepository;
        private TypeInsRepositoryMongo TypeInsRepository;
        private ReportsRepositoryMongo reportsRepository;

        public DbReposMongo(string cs)
        {
            db = new MongoContext(cs);
            AutoRepository = new AutoRepositoryMongo(db);
            ClientRepository = new ClientRepositoryMongo(db);
            InsAgentRepository = new InsAgentRepositoryMongo(db);
            InsIncidentRepository = new InsIncidentRepositoryMongo(db);
            InsuranceRepository = new InsuranceRepositoryMongo(db);
            PaymentRepository = new PaymentRepositoryMongo(db);
            TypeInsRepository = new TypeInsRepositoryMongo(db);
            reportsRepository = new ReportsRepositoryMongo(db);
            //Seed();
        }

        public IRepository<Auto> Auto
        {
            get
            {
                return AutoRepository;
            }
        }

        public IRepository<Client> Client
        {
            get
            {
                return ClientRepository;
            }
        }

        public IRepository<InsAgent> InsAgent
        {
            get
            {
                return InsAgentRepository;
            }
        }
        public IRepository<InsIncident> InsIncident
        {
            get
            {
                return InsIncidentRepository;
            }
        }
        public IRepository<Insurance> Insurance
        {
            get
            {
                return InsuranceRepository;
            }
        }
        public IRepository<Payment> Payment
        {
            get
            {
                return PaymentRepository;
            }
        }
        public IRepository<TypeIns> TypeIns
        {
            get
            { 
                return TypeInsRepository;
            }
        }

        public IReportsRepository Reports
        {
            get { return reportsRepository; }
        }

        public int Save()
        {
            return 1;
        }


        //private void Seed()
        //{
        //    List<Auto> m = new List<Auto>(){
        //        new Auto() { IdAuto =0,Brand="Kia",Model=1.ToString(),EnginePower=100,NumberAuto=13123.ToString(),Run=13,NumberPTS=1231.ToString(),IdClient=0,IdInsurance=0 },
        //       new Auto (){IdAuto =1,Brand="Buba",Model=2.ToString(),EnginePower=110,NumberAuto=243242.ToString(),Run=3,NumberPTS=312.ToString(),IdClient=1,IdInsurance=1}
        //    };
        //    db.AutoCollection.InsertMany(m);
        //}
    }
}
