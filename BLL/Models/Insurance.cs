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
    public class InsuranceModel
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
        public string Policy { get; set; }
        public  string FIO { get; set; }
        public decimal? Price { get; set; }
        public int? DrivingExperience { get; set; }
        public string OwnerPassport { get; set; }
        public string AutoFullName { get; set; }
        public string OwnerSertificate { get; set; }
        public int AutoId { get; set; }
        public int TypeId { get; set; }
        public string Type { get; set; }
        public decimal LeftPrice { get; set; }
        public InsuranceModel() { }
        
        public InsuranceModel (Insurance i)
        {
            Id = i.Id;
            StartDate = i.StartDate;
            FinishDate = i.FinishDate;
            Policy = i.Policy;
            Price = i.Price;
            LeftPrice = (decimal)(Price - i.Payment.Sum(s => s.Price));
            FIO = i.Auto.Client.FIO;
            DrivingExperience = i.DrivingExperience;
            OwnerPassport = i.OwnerPassport;               
            OwnerSertificate = i.OwnerSertificate;
            AutoId = i.AutoId;
            TypeId = i.TypeId;
            Type = i.InsuranceType.Name;
            AutoFullName = i.Auto.Brand + " " + i.Auto.Model;
        }
    }
}
