using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MinistryOfLand.Models
{
    public class AwardPublicationModelClass
    {
        public int PublicationId { get; set; }
        public string BooksTitle { get; set; }
        public string Periodicals { get; set; }
        public string Monograph { get; set; }
        public string Journal { get; set; }
        public string Description { get; set; }
        public System.DateTime Date { get; set; }
        public int UserId { get; set; }


        public int AwardId { get; set; }
        public string TitleOfaward { get; set; }
        public string Ground { get; set; }

        

    }
}