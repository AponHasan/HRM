using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MinistryOfLand.Models
{
    public class EducationModelClass
    {
        public int EducationalQId { get; set; }
        public string DegreeTitel { get; set; }
        public string InstituteName { get; set; }
        public string PassingYear { get; set; }
        public string PrincicpalSubject { get; set; }
        public string Result { get; set; }
        public int UserId { get; set; }

        public int LanguageId { get; set; }
        public string ReadSkil { get; set; }
        public string Unit { get; set; }
        public string SpeakSkil { get; set; }
    }
}