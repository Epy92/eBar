﻿using System;

namespace ViewModels
{
    public class UserResetPasswordCodesModel
    {
        public int UserResetPwdId { get; set; }
        public int UserID { get; set; }
        public string ResetCode { get; set; }
        public DateTime CreationDate { get; set; }
    }
}