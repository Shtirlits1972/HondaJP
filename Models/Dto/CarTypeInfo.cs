using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HondaJP.Models.Dto
{
    public class CarTypeInfo
    {
		public int pos { get; set; }
		public string model { get; set; }
		public string body { get; set; }
		public string modification { get; set; }
		public string transmission { get; set; }
		public string door { get; set; }
		public string engine { get; set; }


        public override string ToString()
        {
            return $"{model} {modification} {transmission} {engine} ";
        }
    }
}
