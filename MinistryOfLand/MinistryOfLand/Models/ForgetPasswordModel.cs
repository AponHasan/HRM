using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MinistryOfLand.Models
{
    public class ForgetPasswordModel
    {
        public int userId { get; set; }
        //[Required(ErrorMessage = "User Name Required*")]
        public string username { get; set; }
        //[Required(ErrorMessage = "Password Required*")]
        public string password { get; set; }
        public string email { get; set; }
        public string UserType { get; set; }
        public string UserImage { get; set; }
    }
}