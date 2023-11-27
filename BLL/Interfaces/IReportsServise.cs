
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public interface IReportsService
    {

        List<Report2> ExecuteSP(int pup);

        List<Report1> Report_1(int IdBrand);
    }
}
