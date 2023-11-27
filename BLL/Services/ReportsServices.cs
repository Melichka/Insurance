using DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ReportsService : IReportsService
    {
        private IDbRepository db;
        public ReportsService(IDbRepository repos)
        {
            db = repos;
        }

        //выполнить ХП
        public List<Report2> ExecuteSP(int pup)
        {
            //return db.Reports.ExecuteSP(pup).Select(i => new Report2 { FIO = i.FIO, OwnerPassport = i.OwnerPassport, Brand = i.Brand, NumberAuto = i.NumberAuto, Price = i.Price }).ToList();
            return null;
        }

        public List<Report1> Report_1(int IdBrand)
        {
            //var request = db.Reports.Report_1(IdBrand)
            //    .Select(i => new Report1() { FIO = i.FIO, Brand = i.Brand })
            //    .ToList();
            //return request;
            return null;
        }
    }
}

 