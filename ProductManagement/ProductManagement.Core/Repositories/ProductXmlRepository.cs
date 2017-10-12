using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductManagement.Core.Domain;
using System.Xml;
using System.Data;

namespace ProductManagement.Core.Repositories
{
    public class ProductXmlRepository : IPersistenceHandler<Product>
    {
        IRepository repository;
        DataSet datasource;

        public ProductXmlRepository(IRepository repo)
        {
            repository = repo;
            datasource = new DataSet("ProductManagement");

            var dt = new DataTable("Products");
            var pkColumns = new DataColumn("Number");
            dt.Columns.Add(pkColumns);
            dt.Columns.Add(new DataColumn("Name"));
            dt.Columns.Add(new DataColumn("Price"));

            dt.PrimaryKey = new DataColumn[] { pkColumns };

            datasource.Tables.Add(dt);

            if (System.IO.File.Exists(repository.GetTargetSettings()))
            {
                datasource.ReadXml(repository.GetTargetSettings());
            }
        }

        public void Add(Product input)
        {
            var newRow = datasource.Tables[0].NewRow();
            newRow["Number"] = input.Number;
            newRow["Name"] = input.Name;
            newRow["Price"] = input.Price;

            datasource.Tables[0].Rows.Add(newRow);
            datasource.AcceptChanges();

            datasource.WriteXml(repository.GetTargetSettings());
        }

        public void Edit(Product input)
        {
            var row = datasource.Tables[0].Rows.Find(input.Number);
            row["Name"] = input.Name;
            row["Price"] = input.Price;
            datasource.AcceptChanges();

            datasource.WriteXml(repository.GetTargetSettings());
        }

        public Product Get(Func<Product, bool> criteria)
        {
            return GetAll().FirstOrDefault(criteria);
        }

        public List<Product> GetAll(Func<Product, bool> criteria = null)
        {
            var retList = new List<Product>();
            foreach (DataRow row in datasource.Tables[0].Rows)
            {
                retList.Add(new Product()
                {
                    Number = row["Number"].ToString(),
                    Name = row["Name"].ToString(),
                    Price = Convert.ToDouble(row["Price"].ToString()),
                });
            }
            return retList;
        }

        public void Remove(Product input)
        {
            var row = datasource.Tables[0].Rows.Find(input.Number);
            datasource.Tables[0].Rows.Remove(row);
            datasource.AcceptChanges();

            datasource.WriteXml(repository.GetTargetSettings());
        }
    }
}
