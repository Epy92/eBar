using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eBarWS.Models
{
    public class UserResetPasswordCodesModel
    {
        public int UserResetPwdId { get; set; }
        public int UserID { get; set; }
        public string ResetCode { get; set; }
        public DateTime CreationDate { get; set; }
    }
}