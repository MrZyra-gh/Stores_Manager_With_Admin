using Microsoft.AspNetCore.Mvc;
using StoresManager.ActionFilters;
using StoresManager.Entities;
using StoresManager.ExtentionMethods;
using StoresManager.Repositories;
using StoresManager.ViewModels.Shared;
using StoresManager.ViewModels.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace StoresManager.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index(IndexVM model)
        {

            StoreToProductRepository repo = new StoreToProductRepository();



            model.Pager = model.Pager ?? new PagerVM();

            model.Pager.Page = model.Pager.Page <= 0
                                        ? 1
                                        : model.Pager.Page;

            model.Pager.ItemsPerPage = model.Pager.ItemsPerPage <= 0
                                        ? 10
                                        : model.Pager.ItemsPerPage;

            model.Pager.PagesCount = (int)Math.Ceiling(repo.Count() / (double)model.Pager.ItemsPerPage);

            model.Items = repo.GetAll(null, null, model.Pager.Page, model.Pager.ItemsPerPage);



            return View(model);
        }
        [HttpGet]
        public IActionResult Search(string input)
        {
            SearchVM model = new SearchVM();
            StoreToProductRepository repo = new StoreToProductRepository();
            ProductsRepository prodRepo = new ProductsRepository();
 
            model.Input= input;

            model.Pager = model.Pager ?? new PagerVM();

            model.Pager.Page = model.Pager.Page <= 0
                                        ? 1
                                        : model.Pager.Page;

            model.Pager.ItemsPerPage = model.Pager.ItemsPerPage <= 0
                                        ? 10
                                        : model.Pager.ItemsPerPage;

            model.Pager.PagesCount = (int)Math.Ceiling(repo.Count() / (double)model.Pager.ItemsPerPage);

            if(input == null)
            {
                model.Items = repo.GetAll(null, null, model.Pager.Page, model.Pager.ItemsPerPage);
            }
            else
            {
                model.Items = repo.GetAll(o => (o.Product.Name.Contains(model.Input)) || (o.Product.Brand.Contains(model.Input)), null, model.Pager.Page, model.Pager.ItemsPerPage);
            }
            /*
            List<Product> products = prodRepo.GetAll(p => (p.Name.Contains(model.Input)) || (p.Brand.Contains(model.Input)), null, model.Pager.Page, model.Pager.ItemsPerPage);

            model.Items = new List<StoreToProduct>();

            foreach (Product product in products)
            {
                model.Items.AddRange(repo.GetAll(i => i.Product == product));
            }
            */
            return View(model);

        }



        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginVM model)
        {
            if (!this.ModelState.IsValid)
                return View(model);

            UsersRepository usersRepo = new UsersRepository();
            User loggedUser = usersRepo.GetFirstOrDefault(u => u.Username == model.Username &&
                                                               u.Password == model.Password);
            if (loggedUser == null)
            {
                this.ModelState.AddModelError("authError", "Invalid username or password!");
                return View(model);
            }

            HttpContext.Session.SetObject("loggedUser", loggedUser);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignIn(SignInVM model)
        {
            
            if (!ModelState.IsValid)
                return View(model);
            UsersRepository repo = new UsersRepository();

            if (repo.GetFirstOrDefault(u => u.Username == model.Username) != null)
            {
                ModelState.AddModelError("Username", "The username is already taken.");
                return View(model);
            }

            User item = new User();
            item.Username = model.Username;
            item.Password = model.Password;
            item.FirstName = model.FirstName;
            item.LastName = model.LastName;

            
            repo.Save(item);

            return RedirectToAction("Index", "Home");
            
            
        }


        [AuthenticationFilter]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("loggedUser");

            return RedirectToAction("Login", "Home");
        }
       
    }
}
