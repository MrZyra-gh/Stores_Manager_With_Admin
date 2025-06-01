
using StoresManager.Entities;
using StoresManager.ViewModels.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace StoresManager.ViewModels.Home
{
    public class IndexVM
    {
        public List<StoreToProduct> Items { get; set; }

        public PagerVM Pager { get; set; }        
        public string Input { get; set; }


    }
}

