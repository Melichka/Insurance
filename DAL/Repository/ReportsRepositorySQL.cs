
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ReportsRepositorySQL:IReportsRepository
    {
    //    private InsuranceContext db;
    //    public ReportsRepositorySQL(InsuranceContext dbcontext)
    //    {
    //        this.db = dbcontext;
    //    }
    //    public class SPResult
    //    {
    //        public string FIO { get; set; }
    //        public string OwnerPassport { get; set; }

    //        public string Brand { get; set; }

    //        public string NumberAuto { get; set; }
    //        public decimal? Price { get; set; }
    //    }



    //    //выполнить ХП
    //    public List<Report2> ExecuteSP(int pup)
    //    {

    //        System.Data.SqlClient.SqlParameter param1 = new System.Data.SqlClient.SqlParameter("@IdTypeIns", pup);
    //        InsuranceContext db = new InsuranceContext();

    //        var rez = db.Database.SqlQuery<SPResult>("proc1 @IdTypeIns", new object[] { param1 }).ToList();
    //        var data = rez.GroupBy(i => new { i.FIO, i.OwnerPassport, i.Brand, i.NumberAuto })
    //            .ToDictionary(i => i.Key, i => i.Select(j => j.Price))
    //            .Select(i => new Report2 { FIO = i.Key.FIO, OwnerPassport = i.Key.OwnerPassport, Brand = i.Key.Brand, NumberAuto = i.Key.NumberAuto, Price = String.Join(",", i.Value.Select(j => j)).ToArray().FirstOrDefault() })
    //            .ToList();
    //        return data;

    //    }

    //    public List<Report1> Report_1(int IdBrand)
    //    {
    //        InsuranceContext db = new InsuranceContext();
    //        var request = db.Insurance
    //            .Join(db.Auto, ph => (ph.IdAuto), m => m.IdAuto, (ph, m) => new { ph, m })
    //            .Where(i => (i.ph.IdAuto) == IdBrand)
    //            .Select(i => new Report1() { FIO = i.ph.FIO, Brand = i.m.Brand })
    //            .ToList();
    //        return request;

    //    }
    }
}
