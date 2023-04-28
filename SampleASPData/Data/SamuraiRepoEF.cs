using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SampleASPData.Models;

namespace SampleASPData.Data
{
    public class SamuraiRepoEF : ISamurai
    {
        private readonly AppDbContext _context;
        public SamuraiRepoEF(AppDbContext context)
        {
            _context = context;
        }
        public void Add(Samurai samurai)
        {
            try
            {
                _context.Samurais.Add(samurai);
                _context.SaveChanges();
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Delete(int id)
        {
            try
            {
                var result = GetById(id);
                if (result != null)
                {
                    _context.Samurais.Remove(result);
                    _context.SaveChanges();
                }
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<Samurai> GetAll()
        {
            var results = _context.Samurais.OrderBy(s => s.Name).ToList();
            /*var results = from s in _context.Samurais
                          orderby s.Name
                          select s;*/
            return results;
        }

        public Samurai GetById(int id)
        {
            var result = _context.Samurais.FirstOrDefault(s => s.Id == id);
            if (result == null)
            {
                throw new Exception("Samurai tidak ditemukan");
            }
            return result;
        }

        public IEnumerable<Samurai> GetByName(string name)
        {
            var results = _context.Samurais.Where(s => s.Name.Contains(name)).ToList();
            return results;
        }

        public void Update(int id, Samurai samurai)
        {
            try
            {
                var result = GetById(id);
                if (result != null)
                {
                    result.Name = samurai.Name;
                    _context.SaveChanges();
                }
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}