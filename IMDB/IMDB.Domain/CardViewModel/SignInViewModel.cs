﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDB.Domain.CardViewModel
{
    public class SignInViewModel
    {
        [DisplayName("نام کاربری")]
        [Required(ErrorMessage ="لطفا {0} را وارد نمایید")]
        public string UserName { get; set; }
        [DisplayName("رمز")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        public string Password { get; set; }
        [DisplayName("مرا به خاطر بسپار")]
        public bool RememberMe { get; set; }
    }
}
