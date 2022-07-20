using System;
using PetitionApi.DTOs;
using PetitionApi.Models;

namespace PetitionApi.Interfaces
{
    public interface IUserRepository
    {
        public User? Login(UserLogin userLogin);

        public IEnumerable<User> GetUsers();

        public User? GetUserByEmail(string email);

        public User CreateUser(UserRegistration reg);
    }
}

