using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SampleASPData.Models;

namespace SampleASPData.Data
{
    public class QuoteRepoEF : IQuote
    {
        private readonly AppDbContext _context;
        public QuoteRepoEF(AppDbContext context)
        {
            _context = context;
        }
        public void Add(Quote samurai)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Quote> GetAll()
        {
            var results = _context.Quotes.Include(s => s.Samurai);
            return results;
        }

        public Quote GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(int id, Quote samurai)
        {
            throw new NotImplementedException();
        }
    }
}