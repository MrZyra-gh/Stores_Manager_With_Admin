using StoresManager.ViewModels.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using StoresManager.Repositories;
using StoresManager.ViewModels.Shared;
using StoresManager.ActionFilters;
using StoresManager.Entities;

namespace StoresManager.Controllers
{
    [AuthenticationFilter]
    public class ProductsController : Controller
    {
        [HttpGet]
        public IActionResult Index(IndexVM model)
        {
            ProductsRepository repo = new ProductsRepository();

            

            model.Pager = model.Pager ?? new PagerVM();

            model.Pager.Page = model.Pager.Page <= 0
                                        ? 1
                                        : model.Pager.Page;

            model.Pager.ItemsPerPage = model.Pager.ItemsPerPage <= 0
                                        ? 10
                                        : model.Pager.ItemsPerPage;

            model.Pager.PagesCount = (int)Math.Ceiling(repo.Count() / (double)model.Pager.ItemsPerPage);

            model.Items = repo.GetAll(null, i => i.Id, model.Pager.Page, model.Pager.ItemsPerPage);
            
            return View(model);
        }
        [HttpGet]
        public IActionResult Search(string input)
        {
            SearchVM model = new SearchVM();
            ProductsRepository repo = new ProductsRepository();

            model.Input = input;

            model.Pager = model.Pager ?? new PagerVM();

            model.Pager.Page = model.Pager.Page <= 0
                                        ? 1
                                        : model.Pager.Page;

            model.Pager.ItemsPerPage = model.Pager.ItemsPerPage <= 0
                                        ? 10
                                        : model.Pager.ItemsPerPage;

            model.Pager.PagesCount = (int)Math.Ceiling(repo.Count() / (double)model.Pager.ItemsPerPage);

            model.Items = repo.GetAll(p => (p.Name.Contains(model.Input)) || (p.Brand.Contains(model.Input)), null, model.Pager.Page, model.Pager.ItemsPerPage);



            return View(model);

        }

        [HttpGet]
        public IActionResult Create()
        {
            CreateVM model = new CreateVM();

            return View(model);
        }
        
        [HttpPost]
        public IActionResult Create(CreateVM model)
        {


            ProductsRepository repo = new ProductsRepository();




            Product item = new Product();

            item.Name = model.Name;
            item.Brand = model.Brand;


            repo.Save(item);

            return RedirectToAction("Index", "Products");
        }
        [AdminAuthenticationFilter]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            EditVM model = new EditVM();
            ProductsRepository repo = new ProductsRepository();
            Product item = repo.GetFirstOrDefault(p => p.Id == id);

            if (item == null)
                return RedirectToAction("Index", "Products");


            model.Name = item.Name;
            model.Brand = item.Brand;
            model.Id = item.Id;

            return View(model);
        }
        [AdminAuthenticationFilter]
        [HttpPost]
        public IActionResult Edit(EditVM model)
        {
            ProductsRepository repo = new ProductsRepository();

            Product item = new Product();

            item.Id = model.Id;
            item.Name = model.Name;
            item.Brand = model.Brand;

            repo.Save(item);

            return RedirectToAction("Index", "Products");
        }
        [AdminAuthenticationFilter]
        public IActionResult Delete(int id)
        {
            ProductsRepository repo = new ProductsRepository();
            Product item = new Product();
            item.Id = id;

            repo.Delete(item);

            return RedirectToAction("Index", "Products");
        }

    }
}
