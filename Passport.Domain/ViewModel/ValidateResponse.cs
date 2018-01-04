using System;
using System.Collections.Generic;
using System.Text;

namespace Passport.Domain.ViewModel
{
    public class ValidateResponse
    {
        public List<string> AllowedScopes { get; set; }
        public Dictionary<string, string> Claims { get; set; }
    }
}