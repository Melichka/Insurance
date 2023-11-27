
using BLL;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KonstLab2
{
    public class NinjectRegistrations : NinjectModule
    {
        public override void Load()
        {
            Bind<IInsuranceServices>().To<InsuranceServices>();
            Bind<IReportsService>().To<ReportsService>();
            Bind<IDbCrud>().To<DbDataOperation>();
            Bind<IAutorizationService>().To<AutorizationService>();
            Bind<IPaymentService>().To<PaymentService>();
        }
    }
}
