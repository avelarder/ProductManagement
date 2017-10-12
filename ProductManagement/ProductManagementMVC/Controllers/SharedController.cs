using ProductManagement.Core;
using ProductManagementMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProductManagementMVC.Controllers
{
    public class SharedController : Controller
    {
        IRepository repository;

        public SharedController(IRepository repo)
        {
            repository = repo;
        }
        // GET: Shared
        [ChildActionOnly]
        public PartialViewResult Repository()
        {
            RepositoryModel model = new RepositoryModel
            {
                SelectedOption = repository.GetRepository()
            };
            return PartialView("~/Views/Shared/Repository.cshtml", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Repository(RepositoryModel model)
        {
            if (ModelState.IsValid)
            {
                repository.SetRepositoryStrategy(model.SelectedOption);
                return RedirectToAction("Index", "Home");
            }
            return PartialView(model);
        }
    }
}