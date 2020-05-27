using ECA.DomainObjects;
using ECA.Services.ViewModels;
using ECA.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using ECA.Services.Errors;

namespace ECA.Services.Implemintations
{
    public class AuthService : IAuthService
    {
        private readonly IRepository<User> Repository;
        public AuthService(IRepository<User> repository)
        {
            Repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }
        public UserViewModel Login(LoginViewModel viewModel)
        {
            if (viewModel == null)
            {
                throw new ArgumentNullException(nameof(viewModel));
            }

            if(string.IsNullOrWhiteSpace(viewModel.Email))
            {
                throw new ArgumentException(nameof(viewModel));
            }

            if (string.IsNullOrWhiteSpace(viewModel.Password))
            {
                throw new ArgumentException(nameof(viewModel));
            }

            var user = Repository.Where(x => x.Email == viewModel.Email).FirstOrDefault();

            if (user == null)
            {
                var ex = new NotFoundException($"User with this email was not found.");
                ex.Data.Add("Email", viewModel.Email);
                throw ex;            
            }

            var password = HashPassword(viewModel.Password);

            if (password != user.PasswordHash)
            {
                throw new AuthenticationException();
            }

            return new UserViewModel()
            {
                UserId = user.UserId,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Role = user.Role
            };
        }

        private string HashPassword(string password)
        {
            return password;
        }
    }
}
