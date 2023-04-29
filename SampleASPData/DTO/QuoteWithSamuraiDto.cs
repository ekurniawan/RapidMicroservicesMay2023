using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleASPData.DTO
{
    public class QuoteWithSamuraiDto
    {
        public int QuoteId { get; set; }
        public string Text { get; set; }
        public int SamuraiId { get; set; }
        public SamuraiReadDto Samurai { get; set; }
    }
}