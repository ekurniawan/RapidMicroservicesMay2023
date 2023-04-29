using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProductService.Models;

namespace ProductService.Data
{
    public interface IProduct
    {
        IEnumerable<Product> GetAll();
        IEnumerable<Product> GetByName(string name);
        Product GetById(int id);
        void Add(Product product);
        void Update(int id, Product product);
        void Delete(int id);
    }
}