﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HondaJP.Models.Dto
{
    public class Sgroups
    {
        public string Id { get; set; }
        public string name { get; set; }

        public override string ToString()
        {
            return name;
        }
    }
}
