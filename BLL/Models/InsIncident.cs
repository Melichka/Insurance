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
    public class IncidentModel
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public bool IsEnded { get; set; }
        public decimal Price { get; set; }

        public int StatusId { get; set; }
        public string Status { get; set; }

        public int InsuranceAgentId { get; set; }

        public int? InsuranceId { get; set; }
    
    public IncidentModel() { }

        public IncidentModel(Incident ii)
        {
            Id= ii.Id;
            Date = ii.Date;
            Price = ii.Price;
            StatusId = ii.StatusId;
            Status = ii.IncidentStatus.Name;
            InsuranceAgentId = ii.InsuranceAgentId;
            InsuranceId = ii.InsuranceId;
            IsEnded = (ii.StatusId == 2) || (ii.StatusId == 3 && (DateTime.Now - Date).Days > 10);
        }
    }
}

