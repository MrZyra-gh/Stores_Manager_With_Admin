using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StoresManager.ViewModels.Home
{
    public class SignInVM
    {
        [DisplayName("Username: ")]
        [Required(ErrorMessage = "*This field is Required!")]
        public string Username { get; set; }

        [DisplayName("Password: ")]
        [Required(ErrorMessage = "*This field is Required!")]
        public string Password { get; set; }

        [DisplayName("FirstName: ")]
        [Required(ErrorMessage = "*This field is Required!")]
        public string FirstName { get; set; }

        [DisplayName("LastName: ")]
        [Required(ErrorMessage = "*This field is Required!")]
        public string LastName { get; set; }

    }
}
