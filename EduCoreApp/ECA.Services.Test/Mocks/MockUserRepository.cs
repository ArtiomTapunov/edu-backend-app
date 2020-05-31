using ECA.DataAccess;
using ECA.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ECA.Services.Test.Mocks
{
    public class MockUserRepository : IRepository<User>
    {
        private readonly List<User> Users;
        public MockUserRepository()
        {
            Users = new List<User> {
                new User {
                    UserId = "1",
                    DateCreated = DateTime.Now,
                    Email = "john.doe@example.com",
                    FirstName = "John",
                    LastName = "Doe",
                    LastLoggedIn = DateTime.Now,
                    PasswordHash = "1000:CgToWlPECE5TKFrq5jm5KhFKA6TT8/nf:AHnvXvVr2OzrOcF7AuIDqxyJIJg=",
                    Role = "Admin"
                }
            };
        }

        public IQueryable<User> GetAll()
        {
            return Users.AsQueryable();
        }

        public void Insert(User entry)
        {
            if (entry == null)
                throw new ArgumentNullException(nameof(entry));
            Users.Add(entry);
        }

        public int SaveChanges()
        {
            return 0;
        }

        public IQueryable<User> Where(Expression<Func<User, bool>> predicate)
        {
            return Users.Where(predicate.Compile()).AsQueryable();
        }
    }
}
