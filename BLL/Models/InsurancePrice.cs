using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication.ExtendedProtection;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public static class InsurancePrice
    {
        public static decimal GetInsurancePrice(int DrivingExp, double Age, int EnginePower)
        {
            const int tb = 2224;
            const float kt = 1.2f;
            float kbs;
            const int ko = 1;
            float km;
            if (DrivingExp > 6)
            {
                kbs = 0.95f;
            }
            else if (DrivingExp<=6 && Age >30)
            {
                kbs = 1.35f;
            }
            else
            {
                kbs = 1.6f;
            }
            if (EnginePower <50)
            {
                km = 0.6f;
            }
            else if (EnginePower >= 50 && EnginePower <70)
            {
                km = 1;
            }
            else if (EnginePower >= 70 && EnginePower < 100)
            {
                km = 1.1f;
            }
            else if (EnginePower >= 100 && EnginePower < 120)
            {
                km = 1.2f;
            }
            else if (EnginePower >= 120 && EnginePower < 150)
            {
                km = 1.4f;
            }
            else 
            {
                km = 1.6f;
            }

            return (decimal)(tb * kt * kbs * ko * km);
        }
    }
}
