using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class InsuranceServices:IInsuranceServices
    {
        private IDbRepository db;

        public InsuranceServices(IDbRepository repos)
        {
            db = repos;
        }

        public bool MakeInsurance(InsuranceModel orderDto)
        {

            DateTime now = DateTime.Now;
           
            Insurance insurance = new Insurance
            {
                StartDate = DateTime.Now,
                FinishDate = now.AddYears(1),
                Policy =  orderDto.Policy,
                Price = orderDto.Price,
                DrivingExperience = orderDto.DrivingExperience,
                OwnerPassport = orderDto.OwnerPassport,
                OwnerSertificate = orderDto.OwnerSertificate,
                TypeId  = orderDto.TypeId,
                AutoId = orderDto.AutoId,

            };


            db.Insurance.Create(insurance);
            //db.Insurance.Add(insurance);
            if (db.Save() > 0)
                return true;
            return false;

        }

    }
}
