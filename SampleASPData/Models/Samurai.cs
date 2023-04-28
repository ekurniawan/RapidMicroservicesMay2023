using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleASPData.Models
{
    public class Samurai
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Quote> Quotes { get; set; }
    }
}