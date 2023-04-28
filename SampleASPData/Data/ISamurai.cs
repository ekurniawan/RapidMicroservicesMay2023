using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SampleASPData.Models;

namespace SampleASPData.Data
{
    public interface ISamurai
    {
        IEnumerable<Samurai> GetAll();
        IEnumerable<Samurai> GetByName(string name);
        Samurai GetById(int id);
        void Add(Samurai samurai);
        void Update(int id,Samurai samurai);
        void Delete(Samurai samurai);
    }
}