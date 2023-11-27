using DAL.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    internal class ReportsRepositoryMongo:IReportsRepository
    {
        private MongoContext db;
        public ReportsRepositoryMongo(MongoContext dbcontext)
        {
            this.db = dbcontext;
        }
        public class SPResult
        {
            public string FIO { get; set; }
            public string OwnerPassport { get; set; }

            public string Brand { get; set; }

            public string NumberAuto { get; set; }
            public decimal? Price { get; set; }
        }



        //выполнить ХП
        public List<Report2> ExecuteSP(int pup)
        {
            var builder = new FilterDefinitionBuilder<Insurance>();
            var filter = builder.Empty; // фильтр для выборки всех документов

            //проекция, Запрос к вложенным объектам
            var project = BsonDocument.Parse(
                "{'FIO': '$FIO','OwnerPassport':'$OwnerPassport','Brand':'$Auto.Brand', 'NumberAuto': '$Auto.NumberAuto','Price':'$Price','IdTypeIns':'$IdTypeIns'}");

            var fil = BsonDocument.Parse(
                "{IdTypeIns:{$eq:" + pup +"}}");

            var res = db.InsuranceCollection.Aggregate();
            var res1 = res.Match(fil).ToList();
            var res2 = res.Match(fil).Project(project).ToList();
                var res3 = res2
                .Select(i => new Report2
                {
                    FIO = i.GetValue("FIO").AsString,
                    OwnerPassport = i.GetValue("OwnerPassport").AsString,
                    Brand = "111",
                    NumberAuto = "222",
                    Price = Decimal.Parse(i.GetValue("Price").AsString)
                });
            return res3.ToList();

        }

        public List<Report1> Report_1(int IdBrand)
        {
            var request = (
                from ph in db.AutoCollection.AsQueryable()
                join n in db.InsuranceCollection.AsQueryable()
                on ph.IdAuto equals n.IdAuto 
                where ph.IdAuto==IdBrand
                select new Report1() { FIO = n.FIO, Brand = ph.Brand })
                .ToList();
            return request;

        }
    }
}
