using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductManagement.Core.Domain;

namespace ProductManagement.Core.Repositories
{
    public class ProductCsvRepository : IPersistenceHandler<Product> 
    {
        IRepository repository;
        List<Product> products;

        public ProductCsvRepository(IRepository repo)
        {
            repository = repo;
            products = new List<Product>();

            if (System.IO.File.Exists(repository.GetTargetSettings()))
            {
                var lines = System.IO.File.ReadAllLines(repository.GetTargetSettings());
                foreach (string line in lines)
                {
                    string[] values = line.Split(';');
                    products.Add(new Product() {
                        Number = values[0],
                        Name = values[1],
                        Price = Convert.ToDouble(values[2])
                    });
                }
            }
        }

        public void Add(Product input)
        {
            products.Add(input);

            System.IO.File.AppendAllLines(
                repository.GetTargetSettings(), 
                new string[] { string.Format("{0};{1};{2}", input.Number, input.Name, input.Price) }
            );
            
        }

        public void Edit(Product input)
        {
            throw new NotImplementedException();
        }

        public Product Get(Func<Product, bool> criteria)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetAll(Func<Product, bool> criteria)
        {
            return products;
        }

        public void Remove(Product input)
        {
            throw new NotImplementedException();
        }
    }
}
