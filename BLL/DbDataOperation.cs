
using DAL;

using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    public class DbDataOperation : IDbCrud
    {
        IDbRepository db;
        public DbDataOperation(IDbRepository repos)
        {
            db = repos;
        }
        public List<AutoModel> GetAllAuto()
        {
            return db.Auto.GetList().Select(i => new AutoModel(i)).ToList();
        }

        public List<InsuranceTypeModel> GetAllType()
        {
            return db.InsuranceType.GetList().Select(i => new InsuranceTypeModel(i)).ToList();
        }
        public List<ClientModel> GetAllClient()
        {
            return db.Client.GetList().Select(i => new ClientModel(i)).ToList();
        }
        public List<InsuranceModel> GetAllInsurance()
        {
            return db.Insurance.GetList().Select(i => new InsuranceModel(i)).ToList();
        }
        public InsuranceModel GetInsurance(int Id)
        {
            return new InsuranceModel(db.Insurance.GetItem(Id));
        }
        public ClientModel GetClient(int Id)
        {
            return new ClientModel(db.Client.GetItem(Id));
        }

        public void CreateInsurance(InsuranceModel i)
        {
            if (i.Id == -1)
                i.Id = db.Insurance.GetList().OrderByDescending(a => a.Id).FirstOrDefault() == null ? 0 : db.Insurance.GetList().OrderByDescending(a => a.Id).FirstOrDefault().Id + 1;

            db.Insurance.Create(new Insurance() 
            { 
                Id = i.Id,
                StartDate = i.StartDate,
                FinishDate = i.FinishDate,
                Policy = i.Policy,
                Price = i.Price,
                DrivingExperience = i.DrivingExperience,
                OwnerPassport = i.OwnerPassport,
                OwnerSertificate = i.OwnerSertificate,
                AutoId = i.AutoId,
                TypeId = i.TypeId
                
            });
            Save();

        }

        public void CreateIncident(IncidentModel i)
        {

            if (i.Id == -1)
                i.Id = db.Incident.GetList().OrderByDescending(a => a.Id).FirstOrDefault() == null ? 0 : db.Incident.GetList().OrderByDescending(a => a.Id).FirstOrDefault().Id + 1;

            db.Incident.Create(new Incident()
            {
                Id = i.Id,
                Date = i.Date,
                StatusId = i.StatusId,
                Insurance = db.Insurance.GetItem((int)i.InsuranceId),
                InsuranceAgentId = i.InsuranceAgentId,
                InsuranceAgent = db.InsuranceAgent.GetItem(i.InsuranceAgentId),
                InsuranceId = (int)i.InsuranceId,
                Price = i.Price,
                IncidentStatus =  db.IncidentStatus.GetItem(i.StatusId)
            });

            Save();
        }
        public void CreateClient(ClientModel i)
        {
            db.Client.Create(new Client() { Id = i.Id, FIO = i.FIO, BirthDate = i.BirthDate, Passport = i.Passport, Sertificate = i.Sertificate, Login = i.Login, Password = i.Password, Email = i.Email, PhoneNumber = i.PhoneNumber });
            Save();

        }

        public void UpdateInsurance(InsuranceModel p)
        {
            Insurance ph = db.Insurance.GetItem(p.Id);
            ph.StartDate = p.StartDate;
            ph.FinishDate = p.FinishDate;
            ph.Policy = p.Policy;
            ph.Price = p.Price;
            ph.DrivingExperience = p.DrivingExperience;
            ph.OwnerPassport = p.OwnerPassport;
            ph.OwnerSertificate = p.OwnerSertificate;
            ph.AutoId = p.AutoId;
            ph.TypeId = p.TypeId;
            Save();
        }
        public void UpdateClient(ClientModel p)
        {
            Client ph = db.Client.GetItem(p.Id);
            ph.FIO = p.FIO;
            ph.BirthDate = p.BirthDate;
            ph.Passport = p.Passport;
            ph.Sertificate = p.Sertificate;
            ph.Login = p.Login;
            ph.Password = p.Password;
            ph.Email = p.Email;
            ph.PhoneNumber = p.PhoneNumber; 
         
        Save();
        }

        public void DeleteInsurance(int id)
        {
            Insurance p = db.Insurance.GetItem(id);
            if (p != null)
            {
                db.Insurance.Delete(p.Id);
                Save();
            }
        }

        public void DeleteClient(int id)
        {
            Client p = db.Client.GetItem(id);
            if (p != null)
            {
                db.Client.Delete(p.Id);
                Save();
            }
        }

        public bool Save()
        {
            
                if (db.Save() > 0) return true;
                return false;
            
        }

        public void CreateAuto(AutoModel a)
        {
            if (a.Id == -1)
                a.Id = db.Auto.GetList().OrderByDescending(i => i.Id).FirstOrDefault() == null ? 0 : db.Auto.GetList().OrderByDescending(i => i.Id).FirstOrDefault().Id + 1;
            db.Auto.Create(new Auto {
                Id = a.Id,
                Brand = a.Brand,
                Client = db.Client.GetItem(a.ClientId),
                ClientId  = a.ClientId,
                EnginePower = a.EnginePower,
                NumberPTS = a.NumberPTS,
                Insurance = null,
                Model = a.Model,
                NumberAuto = a.NumberAuto,
                Run = a.Run,
            });
            Save();
        }

        public List<IncidentModel> GetAllIncident()
        {
            return db.Incident.GetList().Select(i => new IncidentModel(i)).ToList();
        }

        public List<IncidentStatusModel> GetAllStatus()
        {
            return db.IncidentStatus.GetList().Select(i => new IncidentStatusModel(i)).ToList();
        }

        public void UpdateIncident(IncidentModel i)
        {
            Incident inc = db.Incident.GetItem(i.Id);
            inc.StatusId = i.StatusId;
            inc.IncidentStatus = db.IncidentStatus.GetItem(i.StatusId);
            Save();
        }

        public List<PaymentModel> GetAllPayment()
        {
            return db.Payment.GetList().Select(i => new PaymentModel(i)).ToList();
        }
    }
}

