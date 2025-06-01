using Microsoft.AspNetCore.Mvc;
using StoresManager.ActionFilters;
using StoresManager.Entities;
using StoresManager.ExtentionMethods;
using StoresManager.Repositories;
using StoresManager.ViewModels.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StoresManager.ViewModels.Users;

namespace StoresManager.Controllers
{
    [AdminAuthenticationFilter]
    public class UsersController : Controller
    {
        [HttpGet]
        public IActionResult Index(IndexVM model)
        {
            UsersRepository repo = new UsersRepository();

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
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateVM model)
        {
            if (!ModelState.IsValid)
                return View(model);

            User item = new User();
            item.Username = model.Username;
            item.Password = model.Password;
            item.FirstName = model.FirstName;
            item.LastName = model.LastName;

            UsersRepository repo = new UsersRepository();
            repo.Save(item);

            return RedirectToAction("Index", "Users");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            UsersRepository repo = new UsersRepository();
            User item = repo.GetFirstOrDefault(u => u.Id == id);

            if (item == null)
                return RedirectToAction("Index", "Users");

            EditVM model = new EditVM();
            model.Id = item.Id;
            model.Username = item.Username;
            model.Password = item.Password;
            model.FirstName = item.FirstName;
            model.LastName = item.LastName;

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(EditVM model)
        {
            if (!ModelState.IsValid)
                return View(model);

            User item = new User();
            item.Id = model.Id;
            item.Username = model.Username;
            item.Password = model.Password;
            item.FirstName = model.FirstName;
            item.LastName = model.LastName;

            UsersRepository repo = new UsersRepository();
            repo.Save(item);

            return RedirectToAction("Index", "Users");
        }

        public IActionResult Delete(int id)
        {
            UsersRepository repo = new UsersRepository();
            User item = new User();
            item.Id = id;

            repo.Delete(item);

            return RedirectToAction("Index", "Users");
        }
    }

}
