using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HondaJP.Models.Dto
{
    public class PartsGroup
    {
        public string Id { get; set; }
        public string desc { get; set; }

        public override string ToString()
        {
            return desc;
        }
    }
}
