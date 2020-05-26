using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HondaJP.Models.Dto
{
    public class ModelCar
    {
        public  string Id { get; set; }  //  
        public string model { get; set; }
        public string seo_url { get; set; }

        public override string ToString()
        {
            return model;
        }
    }
}
