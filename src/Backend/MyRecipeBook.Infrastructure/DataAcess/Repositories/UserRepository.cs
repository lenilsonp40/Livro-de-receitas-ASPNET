﻿using Microsoft.EntityFrameworkCore;
using MyRecipeBook.Domain.Entities;
using MyRecipeBook.Domain.Repositories.User;

namespace MyRecipeBook.Infrastructure.DataAcess.Repositories
{
    public class UserRepository : IUserReadOnlyRepository, IUserWriteOnlyRepository
    {
        private readonly MyRecipeBookDbContext _dbContext;

        public UserRepository(MyRecipeBookDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(User user)
        {
            await _dbContext.AddAsync(user);
            
        }

        public async Task<bool> ExistActiveUserWithEmail(string email)
        {
            return await _dbContext.Users.AnyAsync(user => user.Email.Equals(email) && user.Active);
        }

        public async Task<User?> GetEmailAndPassword(string email, string password)
        {
            return await _dbContext.Users.AsNoTracking()
                .FirstOrDefaultAsync(user => user.Active && user.Email.Equals(email) && user.Password.Equals(password));
        }
    }
}
