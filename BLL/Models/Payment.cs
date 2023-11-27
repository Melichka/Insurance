using DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class PaymentModel
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public decimal Price { get; set; }

        public int InsuranceId { get; set; }

        public PaymentModel() { }
        public PaymentModel(Payment p)
        {
            Id = p.Id;
            Date = p.Date;
            Price = (decimal)p.Price;
            InsuranceId = p.InsuranceId;
        }

    }
}

