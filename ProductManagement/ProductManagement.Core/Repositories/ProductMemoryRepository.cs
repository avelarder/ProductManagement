using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductManagement.Core.Domain;
using System.Runtime.Caching;

namespace ProductManagement.Core.Repositories
{
    public class ProductMemoryRepository : IPersistenceHandler<Product>
    {
        ObjectCache storage;
        IRepository repository;

        public ProductMemoryRepository(IRepository repo)
        {
            repository = repo;
            storage = MemoryCache.Default;
        }


        public void Add(Product input)
        {
            if (!storage.Contains("local"))
            {
                storage.Add("local", new List<Product>(), new CacheItemPolicy());
            }
            var local = storage.Get("local") as List<Product>;
            local.Add(input);
            storage["local"] = local;
        }

        public void Edit(Product input)
        {
            var product = this.Get(x => x.Number == input.Number);
            product.Name = input.Name;
            product.Price = input.Price;
        }

        public Product Get(Func<Product, bool> criteria)
        {
            if (!storage.Contains("local"))
            {
                storage.Add("local", new List<Product>(), new CacheItemPolicy());
            }
            var local = storage.Get("local") as List<Product>;
            return local.FirstOrDefault(criteria);
        }

        public List<Product> GetAll(Func<Product, bool> criteria = null)
        {
            if (!storage.Contains("local"))
            {
                storage.Add("local", new List<Product>(), new CacheItemPolicy());
            }
            var local = storage.Get("local") as List<Product>;
            if (criteria == null)
            {
                return local;
            }
            else
            {
                return local.Where(criteria).ToList();
            }
        }

        public void Remove(Product input)
        {
            var local = storage.Get("local") as List<Product>;
            var item = Get(x => x.Number == input.Number);
            local.Remove(item);
            storage["local"] = local;
        }
    }
}
