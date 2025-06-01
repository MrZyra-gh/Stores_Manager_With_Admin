using Microsoft.AspNetCore.Mvc;
using StoresManager.Entities;
using StoresManager.ExtentionMethods;
using StoresManager.Repositories;
using StoresManager.ViewModels.Stores;
using StoresManager.ViewModels.Shared;
using StoresManager.ActionFilters;
using Microsoft.EntityFrameworkCore;

namespace StoresManager.Controllers
{
    [AuthenticationFilter]
    public class StoresController : Controller
    {
        
        public IActionResult Index(IndexVM model)
        {
            
            User loggedUser = this.HttpContext.Session.GetObject<User>("loggedUser");

            if (loggedUser != null)
            {
                
                model.UserIsManager = UsersRepository.IsManager(loggedUser.Id);

            }
            StoresRepository repo = new StoresRepository();

            model.Items = new List<Store>();

            model.Pager = model.Pager ?? new PagerVM();

            model.Pager.Page = model.Pager.Page <= 0
                                        ? 1
                                        : model.Pager.Page;

            model.Pager.ItemsPerPage = model.Pager.ItemsPerPage <= 0
                                        ? 10
                                        : model.Pager.ItemsPerPage;
            
            model.Pager.PagesCount = (int)Math.Ceiling(
                            repo.Count(p => p.OwnerId == loggedUser.Id || p.ManagerId == loggedUser.Id) /
                            (double)model.Pager.ItemsPerPage);

            if (loggedUser.Username == "nikiv")
            {
                model.Items = repo.GetAll(null, i => i.Id, model.Pager.Page, model.Pager.ItemsPerPage);
                model.Pager.PagesCount = (int)Math.Ceiling(repo.Count() / (double)model.Pager.ItemsPerPage);

                return View(model);

            }

            model.Items = repo.GetAll(i => i.OwnerId == loggedUser.Id || i.ManagerId == loggedUser.Id, i => i.Id, model.Pager.Page, model.Pager.ItemsPerPage);  

            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            User loggedUser = this.HttpContext.Session.GetObject<User>("loggedUser");

            CreateVM model = new CreateVM();
            model.OwnerId = loggedUser.Id;

            return View(model);
        }
        [HttpPost]
        public IActionResult Create(CreateVM model)
        {
            User loggedUser = this.HttpContext.Session.GetObject<User>("loggedUser");

            if (model.OwnerId != loggedUser.Id)
            {
                ModelState.AddModelError("summaryError", "Owner impersonation attempt detected!");
                return View(model);
            }

            StoresRepository repo = new StoresRepository();
            UsersRepository usersRepo = new UsersRepository();


           
            Store item = new Store();
            item.OwnerId = model.OwnerId;
            item.Address = model.Address;
            item.Chain = model.Chain;
            if (model.Username != null && model.Password != null && model.FirstName != null && model.LastName != null)
            {
                if (usersRepo.GetFirstOrDefault(u => u.Username == model.Username) != null)
                {
                    ModelState.AddModelError("Username", "The username is already taken.");
                    return View(model);
                }

                User manager = new User();
                manager.Username = model.Username;
                manager.Password = model.Password;
                manager.FirstName = model.FirstName;
                manager.LastName = model.LastName;

                usersRepo.Save(manager);
                item.ManagerId = usersRepo.GetFirstOrDefault(u => u.Username == model.Username).Id;
            }

            repo.Save(item);

            return RedirectToAction("Index", "Stores");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            User loggedUser = this.HttpContext.Session.GetObject<User>("loggedUser");

            StoresRepository repo = new StoresRepository();
            Store item = repo.GetFirstOrDefault(p => p.Id == id);

            if (item == null)
                return RedirectToAction("Index", "Stores");

            if (item.OwnerId != loggedUser.Id)
                return RedirectToAction("Index", "Stores");

            EditVM model = new EditVM();
            model.Id = item.Id;
            model.OwnerId = item.OwnerId;
            model.Chain = item.Chain;
            model.Address = item.Address;

            if (item.ManagerId != null)
            {
                UsersRepository userRepo = new UsersRepository();
                User manager = userRepo.GetFirstOrDefault(o => o.Id == item.ManagerId);
                model.Username = manager.Username;
                model.Password = manager.Password;
                model.FirstName = manager.FirstName;
                model.LastName = manager.LastName;
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(EditVM model)
        {
            User loggedUser = this.HttpContext.Session.GetObject<User>("loggedUser");

            StoresRepository repo = new StoresRepository();
            Store item = repo.GetFirstOrDefault(p => p.Id == model.Id && loggedUser.Username != "nikiv");

            if (item.OwnerId != loggedUser.Id)
            {
                ModelState.AddModelError("summaryError", "Owner impersonation attempt detected!");
                return View(model);
            }

            if (model.OwnerId != loggedUser.Id && loggedUser.Username != "nikiv")
            {
                ModelState.AddModelError("summaryError", "Owner impersonation attempt detected!");
                return View(model);
            }

            UsersRepository usersRepo = new UsersRepository();




            item.OwnerId = model.OwnerId;
            item.Address = model.Address;
            item.Chain = model.Chain;
            if (model.Username != null && model.Password != null && model.FirstName != null && model.LastName != null)
            {
                if (usersRepo.GetFirstOrDefault(u => u.Username == model.Username) != null)
                {
                    ModelState.AddModelError("Username", "The username is already taken.");
                    return View(model);
                }

                User manager = usersRepo.GetFirstOrDefault(u => u.Username == model.Username);
                manager.Username = model.Username;
                manager.Password = model.Password;
                manager.FirstName = model.FirstName;
                manager.LastName = model.LastName;

                usersRepo.Save(manager);
            }
            else
            {
                if (item.ManagerId != null)
                {
                    User manager = new User();
                    manager.Id = (int)item.ManagerId;

                    usersRepo.Delete(manager);
                }
            }

            repo.Save(item);

            return RedirectToAction("Index", "Stores");
        }

        public IActionResult Delete(int id)
        {
            StoresRepository repo = new StoresRepository();
            Store item = new Store();
            item.Id = id;
            User loggedUser = this.HttpContext.Session.GetObject<User>("loggedUser");

            if (item.OwnerId != loggedUser.Id && loggedUser.Username != "nikiv")
            {
                ModelState.AddModelError("summaryError", "Owner impersonation attempt detected!");
                return RedirectToAction("Index", "Stores"); ;
            }

            

            repo.Delete(item);

            return RedirectToAction("Index", "Stores");
        }



    }
}
