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
    public class ClientModel
    {
        public int Id { get; set; }

        public string FIO { get; set; }

        public DateTime BirthDate { get; set; }

        public string Passport { get; set; }

        public string Sertificate { get; set; }

        public int IdInsAgent { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public ClientModel() { }

        public ClientModel(Client c)
        {
            Id = c.Id;
            FIO = c.FIO;
            BirthDate = c.BirthDate;
            Passport = c.Passport;  
            Sertificate = c.Sertificate;
            Login = c.Login;
            Password = c.Password;
            Email = c.Email;
            PhoneNumber = c.PhoneNumber;
        }
    }
}

