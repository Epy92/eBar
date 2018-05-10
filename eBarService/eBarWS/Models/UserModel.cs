using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eBarWS.Models
{
    public class UserModel
    {
        public string Username { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public int UserTypeId { get; set; }
        public string UserPreferredLanguage { get; set; }
    }
}