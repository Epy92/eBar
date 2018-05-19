using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eBarWS.Models
{
    public class LanguagesModel
    {
        public int LanguageId { get; set; }
        public string LanguageCode { get; set; }
        public string LanguageCountry { get; set; }
        public string LanguageName { get; set; }
    }
}