using DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class AutoModel
    {

        public int Id { get; set; }

        public string Brand { get; set; }

        public string Model { get; set; }

        public int EnginePower { get; set; }

        public string NumberAuto { get; set; }

        public int? Run { get; set; }

        public string NumberPTS { get; set; }

        public int ClientId { get; set; }





        public AutoModel() { }

        public AutoModel(Auto a)
        {
            Id = a.Id;
            Brand = a.Brand;
            Model = a.Model;   
            EnginePower = a.EnginePower;
            Run = a.Run;
            NumberAuto = a.NumberAuto;
            NumberPTS = a.NumberPTS;
            ClientId = a.ClientId;

        }

    }
}

