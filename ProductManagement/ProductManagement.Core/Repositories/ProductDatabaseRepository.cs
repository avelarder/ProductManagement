using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductManagement.Core.Domain;

namespace ProductManagement.Core.Repositories
{
    /// <summary>
    /// Another aproach can be implemented with EntityFramework DbContext to leverage the expression parameter in GetAll method.
    /// </summary>
    public class ProductDatabaseRepository : IPersistenceHandler<Product> 
    {
        IRepository repository;

        public ProductDatabaseRepository(IRepository repo)
        {
            repository = repo;
        }

        public void Add(Product input)
        {
            using (var cnx = new System.Data.SqlClient.SqlConnection(repository.GetTargetSettings()))
            {
                var cmd = new System.Data.SqlClient.SqlCommand("INSERT INTO dbo.Products VALUES (@Number, @Tittle, @Price)", cnx);
                cmd.Parameters.Add("@Number", sqlDbType:System.Data.SqlDbType.VarChar, size: 10);
                cmd.Parameters.Add("@Tittle", sqlDbType: System.Data.SqlDbType.VarChar, size: 255);
                cmd.Parameters.Add("@Price", sqlDbType: System.Data.SqlDbType.Decimal);

                cmd.Parameters[0].Value = input.Number;
                cmd.Parameters[1].Value = input.Name;
                cmd.Parameters[2].Value = input.Price;

                if (cnx.State !=System.Data.ConnectionState.Open)
                {
                    cnx.Open();
                }

                cmd.ExecuteNonQuery();
                cmd.Dispose();
            }
        }

        public void Edit(Product input)
        {
            using (var cnx = new System.Data.SqlClient.SqlConnection(repository.GetTargetSettings()))
            {
                var cmd = new System.Data.SqlClient.SqlCommand("UPDATE dbo.Products SET Tittle=@Tittle, Price=@Price WHERE Number=@Number", cnx);
                cmd.Parameters.Add("@Number", sqlDbType: System.Data.SqlDbType.VarChar, size: 10);
                cmd.Parameters.Add("@Tittle", sqlDbType: System.Data.SqlDbType.VarChar, size: 255);
                cmd.Parameters.Add("@Price", sqlDbType: System.Data.SqlDbType.Decimal);

                cmd.Parameters[0].Value = input.Number;
                cmd.Parameters[1].Value = input.Name;
                cmd.Parameters[2].Value = input.Price;

                if (cnx.State != System.Data.ConnectionState.Open)
                {
                    cnx.Open();
                }

                cmd.ExecuteNonQuery();
                cmd.Dispose();
            }
        }

        public Product Get(Func<Product, bool> criteria)
        {
            return this.GetAll().FirstOrDefault(criteria);
        }

        public List<Product> GetAll(Func<Product, bool> criteria = null)
        {
            List<Product> retList = new List<Product>();
            using (var cnx = new System.Data.SqlClient.SqlConnection(repository.GetTargetSettings()))
            {
                var cmd = new System.Data.SqlClient.SqlCommand("SELECT * FROM dbo.Products", cnx);
                if (cnx.State != System.Data.ConnectionState.Open)
                {
                    cnx.Open();
                }

                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var product = new Product
                    {
                        Number = reader["Number"].ToString(),
                        Name = reader["Tittle"].ToString(),
                        Price = Convert.ToDouble(reader["Price"])
                    };

                    retList.Add(product);
                }
                cmd.Dispose();
            }

            return retList;
        }

        public void Remove(Product input)
        {
            using (var cnx = new System.Data.SqlClient.SqlConnection(repository.GetTargetSettings()))
            {
                var cmd = new System.Data.SqlClient.SqlCommand("DELETE dbo.Products WHERE Number=@Number", cnx);
                cmd.Parameters.Add("@Number", sqlDbType: System.Data.SqlDbType.VarChar, size: 10);
                cmd.Parameters[0].Value = input.Number;

                if (cnx.State != System.Data.ConnectionState.Open)
                {
                    cnx.Open();
                }

                cmd.ExecuteNonQuery();
                cmd.Dispose();
            }
        }
    }
}
