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
    public class InsuranceAgentModel
    {
        public int Id { get; set; }

        public string Number { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public InsuranceAgentModel() { }

        public InsuranceAgentModel(InsuranceAgent ia)
        {
            Id = ia.Id;
            Number = ia.Number;
            Login = ia.Login;
            Email = ia.Email;
            PhoneNumber = ia.PhoneNumber;
        }

    }
}
