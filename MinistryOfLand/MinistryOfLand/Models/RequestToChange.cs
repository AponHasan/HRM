using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MinistryOfLand.Models
{
    public class RequestToChange
    {
        [Required(ErrorMessage = "Enter Your User Name")]
        [Display(Name = "Enter Your User Name*")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email Is Requeird")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$")]
        [Display(Name = "Enter Your E-Mail*")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Select Category")]
        [Display(Name = "Select Category*")]
        public string Category { get; set; }

        [Required(ErrorMessage = "Mobile Is Required")]
        [StringLength(13, MinimumLength = 10, ErrorMessage = "length is 10 to 13")]
        //[RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid Phone number")]
        [Display(Name = "Mobile Number*")]
        public string Mobile { get; set; }

        [Required(ErrorMessage = "Enter SomeThing What You Want")]
        [Display(Name = "Message*")]
        public string Message { get; set; } 
    }
}