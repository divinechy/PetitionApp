using System;
using PetitionApi.DTOs;
using PetitionApi.Interfaces;
using PetitionApi.Models;

namespace PetitionApi.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly PetitionDbContext _context;

        public UserRepository(PetitionDbContext context)
        {
            _context = context;
        }


        public IEnumerable<User> GetUsers()
        {
            return _context.Users.ToList();
        }


        public User? GetUserByEmail(string email)
        {
            return _context.Users.Where(x => x.Email == email).FirstOrDefault();
        }


        public User? Login(UserLogin userLogin)
        {
          return _context.Users.FirstOrDefault(x => x.Email == userLogin.Email && x.Password == userLogin.Password);
        }


        public User CreateUser(UserRegistration reg)
        {
            try
            {
                User user = new User
                {
                    LastName = reg.LastName,
                    FirstName = reg.FirstName,
                    Email = reg.Email,
                    Password = reg.Password,
                };

                _context.Add(user);

                _context.SaveChanges();

                return _context.Users.First(x => x.Email == reg.Email);
            }catch(Exception)
            {
             throw;
            }

        }
    }
}

