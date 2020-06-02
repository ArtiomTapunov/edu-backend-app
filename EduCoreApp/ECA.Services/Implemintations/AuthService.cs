using ECA.DomainObjects;
using ECA.Services.ViewModels;
using ECA.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using ECA.Services.Errors;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;
using ECA.Services.Helpers;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

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

            if (string.IsNullOrWhiteSpace(viewModel.Email))
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

            //var password = PasswordHash.HashPassword(viewModel.Password);

            if (!PasswordHash.ValidatePassword(viewModel.Password, user.PasswordHash))
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

        public UserViewModel Register(RegisterViewModel viewModel)
        {
            if (viewModel == null)
            {
                throw new ArgumentNullException(nameof(viewModel));
            }

            if (string.IsNullOrWhiteSpace(viewModel.Email))
            {
                throw new ArgumentException(nameof(viewModel));
            }

            if (string.IsNullOrWhiteSpace(viewModel.Password) || string.IsNullOrWhiteSpace(viewModel.PasswordConfirmation))
            {
                throw new ArgumentException(nameof(viewModel));
            }

            var duplicateUser = Repository.Where(x => x.Email == viewModel.Email).FirstOrDefault();
            if (duplicateUser != null)
            {
                throw new DuplicateException("This user is dubplicate");
            }

            if (viewModel.Password != viewModel.PasswordConfirmation)
            {
                throw new PasswordMismatchException();
            }

            if (PasswordStrength.CheckStrength(viewModel.Password) < PasswordStrength.PasswordScore.Medium)
            {
                throw new WeakPasswordException();
            }

            if (!viewModel.HasAcceptedTerms)
            {
                throw new TermsNotAcceptedException();
            }

            var newUser = new User
            {
                UserId = Guid.NewGuid().ToString(),
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
                Email = viewModel.Email,
                LastLoggedIn = null,
                DateCreated = DateTime.UtcNow,
                PasswordHash = PasswordHash.HashPassword(viewModel.Password),
                Role = "User"
            };

            try
            {
                Repository.Insert(newUser);
                Repository.SaveChanges();
            }
            catch(DbUpdateException)
            {
                var ex = new DbUpdateException("Unable to save changes to the DB.");
                ex.Data.Add("AddedUser", newUser);
                throw ex;
            }

            return new UserViewModel()
            {
                UserId = newUser.UserId,
                Email = newUser.Email,
                FirstName = newUser.FirstName,
                LastName = newUser.LastName,
                Role = newUser.Role
            };
        }
    }
}
