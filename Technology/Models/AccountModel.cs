using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Technology.Models
{
    public class AccountModel
    {
        public int Id { get; set; }

        [DisplayName("First Name ")]
        [Required(ErrorMessage = "First Name is required")]
        public string firstName { get; set; }


        [DisplayName("Last Name")]
        [Required(ErrorMessage = "Last Name is required")]
        public string lastName { get; set; }

        [DisplayName("Gender")]
        [Required(ErrorMessage = "Gender is required")]
        public string Gender { get; set; }

        [DisplayName("Age")]
        [Required(ErrorMessage = "Age is required")]
        public int age { get; set; }



        [MaxLength(100)]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Check email address format!")]
        [Required]

        //[Index(IsUnique = true)]
        [Display(Name = "Email")]
        [EmailAddress]
        public string email { get; set; }

        [DisplayName("username")]
        [Required(ErrorMessage = "username is required")]
        public string username { get; set; }

        [DisplayName("password")]
        [Required(ErrorMessage = "password is required")]
        [DataType(DataType.Password)]
        public string password { get; set; }


        [DisplayName("confirm_password")]
        [Required(ErrorMessage = "confirm password is required")]
        [DataType(DataType.Password)]
        [Compare("password", ErrorMessage = "Password is not same")]
        public string confirm_password { get; set; }

        [DisplayName("Status")]
        [Required(ErrorMessage = "Status is required")]
        public bool Status { get; set; }

        public string RoleId { get; set; }
    }

  
}