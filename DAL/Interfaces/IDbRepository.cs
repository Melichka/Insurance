using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IDbRepository
    {
        IRepository<Auto> Auto { get; }
        IRepository<Client> Client { get; }
        IRepository<InsuranceAgent> InsuranceAgent { get; }
        IRepository<Incident> Incident { get; }
        IRepository<Insurance> Insurance { get; }
        IRepository<Payment> Payment { get; }
        IRepository<InsuranceType> InsuranceType { get; }
        IRepository<IncidentStatus> IncidentStatus { get; }
        IReportsRepository Reports { get; }
        int Save();
    }
}
