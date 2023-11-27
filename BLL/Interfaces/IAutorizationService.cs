using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public enum UserType { Unauthorized = 0, Client, InsuranceAgent}
    public interface IAutorizationService
    {
        UserInfo GetCurrentUser();

        bool TryAuthorization(string login, string password);

        void LogOut();

        void CreateClient(string FIO, DateTime DoB, string Passport, string Sertificate, string Phone, string Email, string Login, string Password);
    }
}
