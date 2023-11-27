using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class IncidentStatusModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IncidentStatusModel() { }
        public IncidentStatusModel(IncidentStatus t)
        {
            Id = t.Id;
            Name = t.Name;
        }
    }
}
