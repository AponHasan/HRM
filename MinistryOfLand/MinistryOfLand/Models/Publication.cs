//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MinistryOfLand.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    public partial class Publication
    {
        
        public int PublicationId { get; set; }

        [Required(ErrorMessage = "Ente Books Title")]
        public string BooksTitle { get; set; }

        [Required(ErrorMessage = "Enter Periodicals")]
        public string Periodicals { get; set; }

        [Required(ErrorMessage = "Enter Monograph")]
        public string Monograph { get; set; }

        [Required(ErrorMessage = "Enter Journal")]
        public string Journal { get; set; }

        [Required(ErrorMessage = "Enter Description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Enter Date")]
        public System.DateTime Date { get; set; }

        [Required(ErrorMessage = "Must be Select User Id")]
        public int userId { get; set; }
    
        public virtual UserAccount UserAccount { get; set; }
    }
}
