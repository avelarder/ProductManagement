using Microsoft.Practices.Unity;
using ProductManagement.Core;
using ProductManagement.Core.Domain;
using ProductManagementMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProductManagementMVC.Controllers
{
    public class ProductController : Controller
    {
        IPersistenceHandler<Product> persistanceObject;

        public ProductController(IUnityContainer container)
        {
            var repository = container.Resolve<IRepository>();
            var target = repository.GetRepository();

            persistanceObject = container.Resolve<IPersistenceHandler<Product>>(
               target == RepositoryStrategyEnum.Database ? "Database" :
               target == RepositoryStrategyEnum.XML ? "Xml" :
               target == RepositoryStrategyEnum.CSV ? "Csv" : "Memory"
            );

            AutoMapper.Mapper.Initialize(
                 cfg =>
                 {
                     cfg.CreateMap<Product, ProductModel>();
                     cfg.CreateMap<ProductModel, Product>();
                 }
             );
        }
        // GET: Product
        public ActionResult Index()
        {
            var items =  persistanceObject.GetAll();
            List<ProductModel> model = AutoMapper.Mapper.Map<List<ProductModel>>(items);
            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductModel model)
        {
            if (ModelState.IsValid)
            {
                var product = AutoMapper.Mapper.Map<Product>(model);
                persistanceObject.Add(product);
                return RedirectToAction("Index", "Product");
            }
            return View(model);
        }
    }
}