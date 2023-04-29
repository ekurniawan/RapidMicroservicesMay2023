using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProductService.Models;

namespace ProductService.Data
{
    public class ProductRepo : IProduct
    {
        private readonly AppDbContext _context;
        public ProductRepo(AppDbContext context)
        {
            _context = context;
        }
        public void Add(Product product)
        {
            try
            {
                _context.Products.Add(product);
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
                    _context.Products.Remove(result);
                    _context.SaveChanges();
                }
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<Product> GetAll()
        {
            var results = _context.Products.OrderBy(s => s.Name).ToList();
            return results;
        }

        public Product GetById(int id)
        {
            var result = _context.Products.FirstOrDefault(s => s.ProductId == id);
            if (result == null)
            {
                throw new Exception($"Product id {id} tidak ditemukan");
            }
            return result;
        }

        public IEnumerable<Product> GetByName(string name)
        {
            var results = _context.Products.Where(s => s.Name.Contains(name)).ToList();
            return results;
        }

        public void Update(int id, Product product)
        {
            try
            {
                var result = GetById(id);
                if (result != null)
                {
                    result.Name = product.Name;
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