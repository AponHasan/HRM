using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MinistryOfLand.Models
{
    public class ServiceModelClass
    {
        public int PostingId { get; set; }
        public string Organization { get; set; }
        public string Location { get; set; }
        public string Thanka { get; set; }
        public string District { get; set; }
        public string Pfrom { get; set; }
        public string PTo { get; set; }
        public string PayScal { get; set; }
        public System.DateTime PostingDate { get; set; }
        public int UserId { get; set; }


        public int PromotionalPId { get; set; }
        public string Rank { get; set; }
        public string Promotion { get; set; }
        public string GODate { get; set; }
        public string NaturalOFPromotion { get; set; }
        

    }
}