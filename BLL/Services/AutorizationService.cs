using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public struct UserInfo
    {
        public UserType type;
        public int id;
        public string Name;
    }
    public class AutorizationService : IAutorizationService
    {
        IDbRepository context;
        UserInfo curUser;

        public AutorizationService(IDbRepository context)
        {
            this.context = context;
            curUser = new UserInfo { type = UserType.Unauthorized, id = -1, Name = "" };
        }

        public UserInfo GetCurrentUser()
        {
            return curUser;
        }

        public bool TryAuthorization(string login, string password)
        {
            Client c = context.Client.GetList().Where(i => i.Login == login).FirstOrDefault();
            if (c != null)
            {
                if (c.Password == password)
                {
                    curUser.type = UserType.Client;
                    curUser.id = c.Id;
                    curUser.Name = c.FIO;
                    return true;
                }

            }

            InsuranceAgent s = context.InsuranceAgent.GetList().Where(i => i.Login == login).FirstOrDefault();

            if (s != null)
            {
                if (s.Password == password)
                {
                    curUser.type = UserType.InsuranceAgent;
                    curUser.id = s.Id;
                    curUser.Name = s.Number;
                    return true;
                }

                return false;
            }

            return false;
        }

        public void LogOut()
        {
            curUser.type = UserType.Unauthorized;
            curUser.id = -1;
        }

        public void CreateClient(string FIO, DateTime DoB, string Passport, string Sertificate, string Phone, string Email, string Login, string Password)
        {
            Client c = new Client
            {
                Id = context.Client.GetList().OrderByDescending(i => i.Id).FirstOrDefault() == null ? 0 : context.Client.GetList().OrderByDescending(i => i.Id).FirstOrDefault().Id + 1,
                Auto = null,
                BirthDate = DoB,
                Email = Email,
                FIO = FIO,
                Login = Login,
                Passport = Passport,
                Password = Password,
                PhoneNumber = Phone,
                Sertificate =Sertificate
            };

            context.Client.Create(c);
            context.Save();
        }
    }
}
