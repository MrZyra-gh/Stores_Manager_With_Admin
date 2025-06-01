
using StoresManager.Entities;
using StoresManager.ViewModels.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoresManager.ViewModels.Products
{
    public class IndexVM
    {
        public List<Product> Items { get; set; }

        public PagerVM Pager { get; set; }

        public string Input {  get; set; }

        
    }
}

