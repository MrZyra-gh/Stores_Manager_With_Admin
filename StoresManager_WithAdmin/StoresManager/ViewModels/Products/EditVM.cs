using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StoresManager.ViewModels.Products
{
    public class EditVM
    {

        public int Id { get; set; }

        [DisplayName("Name: ")]
        [Required(ErrorMessage = "*This field is Required!")]
        public string Name { get; set; }

        [DisplayName("Brand: ")]
        [Required(ErrorMessage = "*This field is Required!")]
        public string Brand { get; set; }


    }
}