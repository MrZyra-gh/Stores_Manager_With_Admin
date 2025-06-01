using Microsoft.AspNetCore.Mvc;
using StoresManager.ActionFilters;
using StoresManager.Entities;
using StoresManager.ExtentionMethods;
using StoresManager.Repositories;
using StoresManager.ViewModels.Shared;
using StoresManager.ViewModels.Offers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace StoresManager.Controllers
{
    [AuthenticationFilter]
    public class OffersController : Controller
    {
        [HttpGet]
        public IActionResult Index(IndexVM model)
        {
            UsersRepository userRepo = new UsersRepository();
            User loggedUser = this.HttpContext.Session.GetObject<User>("loggedUser");

            model.Pager = model.Pager ?? new PagerVM();

            model.Pager.Page = model.Pager.Page <= 0
                                        ? 1
                                        : model.Pager.Page;

            model.Pager.ItemsPerPage = model.Pager.ItemsPerPage <= 0
                                        ? 10
                                        : model.Pager.ItemsPerPage;


            StoresRepository storesRepo = new StoresRepository();
            StoreToProductRepository repo = new StoreToProductRepository();

            model.Pager.PagesCount = (int)Math.Ceiling (repo.Count(p => p.Store.OwnerId == loggedUser.Id || p.Store.ManagerId == loggedUser.Id) /
                            (double)model.Pager.ItemsPerPage);


            if (loggedUser.Username == "nikiv")
            {
                model.Items = repo.GetAll(null, null, model.Pager.Page, model.Pager.ItemsPerPage);
                model.Pager.PagesCount = (int)Math.Ceiling(repo.Count() / (double)model.Pager.ItemsPerPage);
                return View(model);
            }

            model.Items = repo.GetAll(i => (i.Store.OwnerId == loggedUser.Id) || (i.Store.ManagerId == loggedUser.Id));
            /*
            List<int> stores = storesRepo.GetAll(i => (i.OwnerId == loggedUser.Id) || (i.ManagerId == loggedUser.Id))
                                                          .Select(i => i.Id)
                                                          .ToList();

            model.Items = new List<StoreToProduct>();

            foreach (int id in stores)
            {
                model.Items.AddRange(repo.GetAll(i => i.StoreId == id));
            }*/
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
            User loggedUser = this.HttpContext.Session.GetObject<User>("loggedUser");

            StoreToProductRepository repo = new StoreToProductRepository();
            StoresRepository storesRepo = new StoresRepository();
            ProductsRepository productRepo = new ProductsRepository();


            Store store = storesRepo.GetFirstOrDefault(i => i.Id == model.StoreId);
            //if (model.StoreId == storesRepo.GetFirstOrDefault(i => ((i.OwnerId == loggedUser.Id) || (i.ManagerId == loggedUser.Id))).Id)
            if (store.OwnerId == loggedUser.Id || store.ManagerId == loggedUser.Id)
            {
                if (productRepo.GetFirstOrDefault(i => i.Id == model.ProductId) == null)
                {
                    ModelState.AddModelError("ProductId", "No such product found.");
                    return View(model);
                }

                StoreToProduct item = new StoreToProduct();
                item.ProductId = model.ProductId;
                item.StoreId = model.StoreId;
                item.BasePrice = model.BasePrice;
                item.LoweredPrice = model.LoweredPrice;

                repo.Save(item);

                return RedirectToAction("Index", "Offers");
            }
            else
            {
                ModelState.AddModelError("StoreId", "You don't have access to that store or no such store exists.");
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            EditVM model = new EditVM();
            model.Id = id;

            StoreToProductRepository repo = new StoreToProductRepository();
            StoreToProduct item = repo.GetFirstOrDefault(i => i.Id == id);
            model.BasePrice = item.BasePrice;
            model.LoweredPrice = item.LoweredPrice;

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(EditVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            StoreToProductRepository repo = new StoreToProductRepository();
            StoreToProduct item = repo.GetFirstOrDefault(i => i.Id == model.Id);
            item.BasePrice = model.BasePrice;
            item.LoweredPrice = model.LoweredPrice;

            repo.Save(item);

            return RedirectToAction("Index", "Offers");
        }

        
        public IActionResult Delete(int id) 
        {
            StoreToProductRepository repo = new StoreToProductRepository();
            StoreToProduct item = new StoreToProduct();
            item.Id = id;

            repo.Delete(item);
            return RedirectToAction("Index", "Offers");
        }
    }
}
