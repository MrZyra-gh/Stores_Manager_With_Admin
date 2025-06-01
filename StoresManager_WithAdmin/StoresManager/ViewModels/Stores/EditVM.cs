using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StoresManager.ViewModels.Stores
{
    public class EditVM
    {
        public int Id {  get; set; }
        public int OwnerId { get; set; }

        [DisplayName("Chain: ")]
        [Required(ErrorMessage = "*This field is Required!")]
        public string Chain { get; set; }

        [DisplayName("Address: ")]
        [Required(ErrorMessage = "*This field is Required!")]
        public string Address { get; set; }

        //manager Info
        [DisplayName("Manager Username: ")]
        public string Username { get; set; }

        [DisplayName("Password: ")]
        public string Password { get; set; }

        [DisplayName("First Name: ")]
        public string FirstName { get; set; }

        [DisplayName("Last Name: ")]
        public string LastName { get; set; }
    }
}
