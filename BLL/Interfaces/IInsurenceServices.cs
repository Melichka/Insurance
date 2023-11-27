
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public interface IInsuranceServices
    {
        //Создает или изменяет существующий заказ
        bool MakeInsurance(InsuranceModel orderDto);
    }
}
