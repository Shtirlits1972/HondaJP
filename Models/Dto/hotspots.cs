using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HondaJP.Models.Dto
{
    public class hotspots
    {
        public int id { get; set; }
        public string name { get; set; }
        public int number { get; set; }
        public int max_x { get; set; }
        public int max_y { get; set; }
        public int min_x { get; set; }
        public int min_y { get; set; }

        public override string ToString()
        {
            return $"{name}-{number}";
        }

    }
}
