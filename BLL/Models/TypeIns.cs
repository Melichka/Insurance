using DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class InsuranceTypeModel
    {
        public int Id{ get; set; }

        public string Name { get; set; }

        public InsuranceTypeModel() { }
        public InsuranceTypeModel(InsuranceType t)
        {
            Id = t.Id;
            Name = t.Name;
        }
    }
}
