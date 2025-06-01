using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StoresManager.Entities
{
    public class Product : BaseEntity
    {
         
        public string Name { get; set; }
        public string Brand { get; set; }
        
    }
}