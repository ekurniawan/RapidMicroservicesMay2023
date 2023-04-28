using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SampleASPData.Models;

namespace SampleASPData.Data
{
    public interface IQuote
    {
        IEnumerable<Quote> GetAll();
        Quote GetById(int id);
        void Add(Quote samurai);
        void Update(int id, Quote samurai);
        void Delete(int id);
    }
}