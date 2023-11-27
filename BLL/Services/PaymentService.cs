using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class PaymentService : IPaymentService
    {
        IDbRepository context;

        public PaymentService(IDbRepository context)
        {
            this.context = context;
        }

        public decimal CalculateFinalPrice(decimal assumedPrice, Insurance insurance)
        {
            decimal payedSum = (decimal)insurance.Payment.Sum(i => i.Price);
            decimal leftSum = (decimal)insurance.Price - payedSum;
            if (leftSum > assumedPrice)
                return assumedPrice;
            else
                return leftSum;
        }

        public void RecivePayment(IncidentModel model)
        {
            Incident inc = context.Incident.GetItem(model.Id);
            inc.StatusId = 2;
            inc.IncidentStatus = context.IncidentStatus.GetItem(2);
            context.Incident.Update(inc);
            context.Save();
        }

        public decimal SendPayment(PaymentModel model)
        {
            if (model.Id == -1)
                model.Id = context.Payment.GetList().OrderByDescending(a => a.Id).FirstOrDefault() == null ? 0 : context.Payment.GetList().OrderByDescending(a => a.Id).FirstOrDefault().Id + 1;

            decimal finalPrice = CalculateFinalPrice(model.Price, context.Insurance.GetItem(model.InsuranceId));

            Payment p = new Payment()
            {
                Id = model.Id,
                Date = model.Date,
                InsuranceId = model.InsuranceId,
                Price = finalPrice,
                Insurance = context.Insurance.GetItem(model.InsuranceId)
            };

            context.Payment.Create(p);
            context.Insurance.GetItem(model.InsuranceId).Payment.Add(p);
            context.Save();

            return finalPrice;
        }
    }
}
