using StoresManager.Entities;
using StoresManager.ViewModels.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StoresManager.ViewModels.Users
{
    public class IndexVM
    {
        public List<User> Items { get; set; }

        public PagerVM Pager { get; set; }
    }
}
