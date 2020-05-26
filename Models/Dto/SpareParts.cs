using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HondaJP.Models.Dto
{
    public class SpareParts
    {
        public int id { get; set; }
        public string name { get; set; }
        public string image_id { get; set; }

        public List<hotspots> hotspots { get; set; }
        public override string ToString()
        {
            return name;
        }
    }
}
