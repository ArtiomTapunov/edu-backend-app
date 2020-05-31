using System;
using System.Collections.Generic;
using System.Text;

namespace ECA.Services.ViewModels
{
    public class RegisterViewModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string PasswordConfirmation { get; set; }

        public bool HasAcceptedTerms { get; set; }
    }
}
