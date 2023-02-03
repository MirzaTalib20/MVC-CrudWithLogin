using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Technology.Models
{
    public class RolesModel
    {
        public int Id { get; set; }

        [DisplayName("First Name ")]
        [Required(ErrorMessage = "First Name is required")]
        public string Role { get; set; }

    }
}