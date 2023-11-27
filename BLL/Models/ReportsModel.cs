using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Report1
    {
        public string FIO { get; set; }
        public string Brand { get; set; }
    }
    public class Report2
    {
        public string FIO { get; set; }
        public string OwnerPassport { get; set; }

        public string Brand { get; set; }
        public string NumberAuto { get; set; }
        public decimal? Price { get; set; }
    }
}
