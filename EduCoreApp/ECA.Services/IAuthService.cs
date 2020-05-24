using ECA.Services.ViewModels;
using System;

namespace ECA.Services
{
    public interface IAuthService
    {
        UserViewModel Login(LoginViewModel viewModel);
    }
}
