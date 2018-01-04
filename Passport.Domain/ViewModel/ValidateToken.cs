using System;
using System.Collections.Generic;
using System.Text;

namespace Passport.Domain.ViewModel
{
    public class ValidateToken
    {
        public string AccessToken { get; set; }
        public string Issuer { get; set; }
    }
}