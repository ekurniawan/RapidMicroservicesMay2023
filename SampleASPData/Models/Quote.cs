using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleASPData.Models
{
    public class Quote
    {
        public int QuoteId { get; set; }
        public string Text { get; set; }
        public int SamuraiId { get; set; }
        public Samurai Samurai { get; set; }
    }
}